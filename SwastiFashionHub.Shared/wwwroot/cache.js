
export function runCaptcha(key) {
    return new Promise((resolve, reject) => {
        grecaptcha.ready(function () {
            grecaptcha.execute(`${key}`, { action: 'submit' }).then(function (token) {
                resolve(token);
            });
        });
    });
}
