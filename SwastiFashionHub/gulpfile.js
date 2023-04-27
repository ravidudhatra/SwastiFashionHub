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

gulp.task('copy-bootstrap-scss', function() {
  return gulp.src(paths.base.node.dir +'/bootstrap/scss/**/*')
    .pipe(gulp.dest(paths.src.libs.dir+'/bootstrap/scss'));
});

gulp.task('clean:rd', function (callback) {
  del.sync(paths.src.libs.dir);
  callback();
});

gulp.task('build', gulp.series(gulp.parallel('clean:rd', 'copy:libs','copy-bootstrap-scss')));
gulp.task('default', gulp.series(gulp.parallel('clean:rd', 'copy:libs','copy-bootstrap-scss')));
