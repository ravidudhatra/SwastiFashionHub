/*
Template Name: Minible - Admin & Dashboard Template
Author: Themesbrand
Website: https://themesbrand.com/
Contact: themesbrand@gmail.com
File: Session Timeout Js File
*/

function initTimeout() {
    $.sessionTimeout({
        keepAliveUrl: 'pages-starter.html',
        logoutButton: 'Logout',
        logoutUrl: 'AuthLogin',
        redirUrl: 'AuthLockScreen',
        warnAfter: 3000,
        redirAfter: 30000,
        countdownMessage: 'Redirecting in {timer} seconds.'
    });

    $('#session-timeout-dialog  [data-dismiss=modal]').attr("data-bs-dismiss", "modal");
}