/// <binding Clean='build' />
const del = require('del');
const gulp = require('gulp');
const npmdist = require('gulp-npm-dist');
const rename = require('gulp-rename');

const paths = {
    base: {
        base: {
            dir: './'
        },
        node: {
            dir: './node_modules'
        }
    },
    src: {
        base: {
            dir: './',
            files: './**/*'
        },
        libs: {
            dir: './wwwroot/assets/libs'
        }
    }
};

gulp.task('copy:libs', function () {
  return gulp
      .src(npmdist(), { base: paths.base.node.dir })
      .pipe(rename(function (path) {
          path.dirname = path.dirname.replace(/\/dist/, '').replace(/\\dist/, '');
      }))
      .pipe(gulp.dest(paths.src.libs.dir));
});

gulp.task('clean:skote', function (callback) {
  del.sync(paths.src.libs.dir);
  callback();
});

gulp.task('build', gulp.series(gulp.parallel('clean:skote', 'copy:libs')));
gulp.task('default', gulp.series(gulp.parallel('clean:skote', 'copy:libs')));
