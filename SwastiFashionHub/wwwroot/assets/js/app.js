'use strict';

var titleName = "Minible - Admin & Dashboard Template";

function initMetisMenu() {
    //metis menu
    $("#side-menu").metisMenu();
}

function initLeftMenuCollapse() {
    var currentSIdebarSize = document.body.getAttribute('data-sidebar-size');
    $(window).on('load', function () {

        $('.switch').on('switch-change', function () {
            toggleWeather();
        });

        if (window.innerWidth >= 1024 && window.innerWidth <= 1366) {
            document.body.setAttribute('data-sidebar-size', 'sm');
            updateRadio('sidebar-size-small')
        }
    });

    $('.vertical-menu-btn').on('click', function (event) {
        event.preventDefault();
        $('body').toggleClass('sidebar-enable');
        if ($(window).width() >= 992) {
            if (currentSIdebarSize == null) {
                (document.body.getAttribute('data-sidebar-size') == null || document.body.getAttribute('data-sidebar-size') == "lg") ? document.body.setAttribute('data-sidebar-size', 'sm') : document.body.setAttribute('data-sidebar-size', 'lg')
            } else if (currentSIdebarSize == "md") {
                (document.body.getAttribute('data-sidebar-size') == "md") ? document.body.setAttribute('data-sidebar-size', 'sm') : document.body.setAttribute('data-sidebar-size', 'md')
            } else {
                (document.body.getAttribute('data-sidebar-size') == "sm") ? document.body.setAttribute('data-sidebar-size', 'lg') : document.body.setAttribute('data-sidebar-size', 'sm')
            }
        }
    });
}

function initActiveMenu() {
    // === following js will activate the menu in left side bar based on url ====
    $("#sidebar-menu a").each(function () {
        var pageUrl = window.location.href.split(/[?#]/)[0];
        if (this.href == pageUrl) {
            $(this).addClass("active");
            $(this).parent().addClass("mm-active"); // add active to li of the current link
            $(this).parent().parent().addClass("mm-show");
            $(this).parent().parent().prev().addClass("mm-active"); // add active class to an anchor
            $(this).parent().parent().parent().addClass("mm-active");
            $(this).parent().parent().parent().parent().addClass("mm-show"); // add active to li of the current link
            $(this).parent().parent().parent().parent().parent().addClass("mm-active");
        }
    });
}

function initMenuItemScroll() {
    // focus active menu in left sidebar
    $(document).ready(function () {
        if ($("#sidebar-menu").length > 0 && $("#sidebar-menu .mm-active .active").length > 0) {
            var activeMenu = $("#sidebar-menu .mm-active .active").offset().top;
            if (activeMenu > 300) {
                activeMenu = activeMenu - 300;
                $(".vertical-menu .simplebar-content-wrapper").animate({ scrollTop: activeMenu }, "slow");
            }
        }
    });
}

function initHoriMenuActive() {
    $(".navbar-nav a").each(function () {
        var pageUrl = window.location.href.split(/[?#]/)[0];
        if (this.href == pageUrl) {
            $(this).addClass("active");
            $(this).parent().addClass("active");
            $(this).parent().parent().addClass("active");
            $(this).parent().parent().parent().addClass("active");
            $(this).parent().parent().parent().parent().addClass("active");
            $(this).parent().parent().parent().parent().parent().addClass("active");
        }
    });
}

function initFullScreen() {
    $('[data-bs-toggle="fullscreen"]').on("click", function (e) {
        e.preventDefault();
        $('body').toggleClass('fullscreen-enable');
        if (!document.fullscreenElement && /* alternative standard method */ !document.mozFullScreenElement && !document.webkitFullscreenElement) {  // current working methods
            if (document.documentElement.requestFullscreen) {
                document.documentElement.requestFullscreen();
            } else if (document.documentElement.mozRequestFullScreen) {
                document.documentElement.mozRequestFullScreen();
            } else if (document.documentElement.webkitRequestFullscreen) {
                document.documentElement.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
            }
        } else {
            if (document.cancelFullScreen) {
                document.cancelFullScreen();
            } else if (document.mozCancelFullScreen) {
                document.mozCancelFullScreen();
            } else if (document.webkitCancelFullScreen) {
                document.webkitCancelFullScreen();
            }
        }
    });
    document.addEventListener('fullscreenchange', exitHandler);
    document.addEventListener("webkitfullscreenchange", exitHandler);
    document.addEventListener("mozfullscreenchange", exitHandler);
    function exitHandler() {
        if (!document.webkitIsFullScreen && !document.mozFullScreen && !document.msFullscreenElement) {
            console.log('pressed');
            $('body').removeClass('fullscreen-enable');
        }
    }
}

function initRightSidebar() {
    // right side-bar toggle
    $('.right-bar-toggle').on('click', function (e) {
        $('body').toggleClass('right-bar-enabled');
    });

    $(document).on('click', 'body', function (e) {
        if ($(e.target).closest('.right-bar-toggle, .right-bar').length > 0) {
            return;
        }

        $('body').removeClass('right-bar-enabled');
        return;
    });
}

function initDropdownMenu() {
    if (document.getElementById("topnav-menu-content")) {
        var elements = document.getElementById("topnav-menu-content").getElementsByTagName("a");
        for (var i = 0, len = elements.length; i < len; i++) {
            elements[i].onclick = function (elem) {
                if (elem.target.getAttribute("href") === "#") {
                    elem.target.parentElement.classList.toggle("active");
                    elem.target.nextElementSibling.classList.toggle("show");
                }
            }
        }
        window.addEventListener("resize", updateMenu);
    }
}

function updateMenu() {
    var elements = document.getElementById("topnav-menu-content").getElementsByTagName("a");
    for (var i = 0, len = elements.length; i < len; i++) {
        if (elements[i].parentElement.getAttribute("class") === "nav-item dropdown active") {
            elements[i].parentElement.classList.remove("active");
            elements[i].nextElementSibling.classList.remove("show");
        }
    }
}

function initComponents() {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    });

    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
    var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl)
    });

    // Counter Up
    var delay = $(this).attr('data-delay') ? $(this).attr('data-delay') : 100; //default is 100
    var time = $(this).attr('data-time') ? $(this).attr('data-time') : 1200; //default is 1200
    $('[data-plugin="counterup"]').each(function (idx, obj) {
        $(this).counterUp({
            delay: delay,
            time: time
        });
    });
}

function initPreloader() {
    $(window).on('load', function () {
        $('#status').fadeOut();
        $('#preloader').delay(350).fadeOut('slow');
    });
}

function initSettings() {
    if (window.sessionStorage) {
        var alreadyVisited = sessionStorage.getItem("is_visited");
        if (!alreadyVisited) {
            sessionStorage.setItem("is_visited", "layout-ltr");
        } else {
            $("#" + alreadyVisited).prop('checked', true);
            // changeDirection(alreadyVisited);
        }
    }

    if (window.sessionStorage) {
        var alreadyVisited = sessionStorage.getItem("is_visited");
        if (!alreadyVisited) {
            var curMode = "light-mode-switch";
            sessionStorage.setItem("is_visited", curMode);
            $(".right-bar input:checkbox").prop('checked', false);
            $("#" + curMode).prop('checked', true);
            updateThemeSetting(curMode);
        } else {
            $(".right-bar input:checkbox").prop('checked', false);
            $("#" + alreadyVisited).prop('checked', true);
            updateThemeSetting(alreadyVisited);
        }
    }
    $("#light-mode-switch, #dark-mode-switch, #rtl-mode-switch").on("change", function (e) {
        updateThemeSetting(e.target.id);
    });
}

function updateThemeSetting(id) {
    if ($("#light-mode-switch").prop("checked") == true && id === "light-mode-switch") {
        $("html").removeAttr("dir");
        $("#dark-mode-switch").prop("checked", false);
        $("#rtl-mode-switch").prop("checked", false);
        $("#bootstrap-style").attr('href', 'assets/css/bootstrap.min.css');
        $("#app-style").attr('href', 'assets/css/app.min.css');
        sessionStorage.setItem("is_visited", "light-mode-switch");
    } else if ($("#dark-mode-switch").prop("checked") == true && id === "dark-mode-switch") {
        $("html").removeAttr("dir");
        $("#light-mode-switch").prop("checked", false);
        $("#rtl-mode-switch").prop("checked", false);
        $("#bootstrap-style").attr('href', 'assets/css/bootstrap-dark.min.css');
        $("#app-style").attr('href', 'assets/css/app-dark.min.css');
        sessionStorage.setItem("is_visited", "dark-mode-switch");
    } else if ($("#rtl-mode-switch").prop("checked") == true && id === "rtl-mode-switch") {
        $("#light-mode-switch").prop("checked", false);
        $("#dark-mode-switch").prop("checked", false);
        $("#app-style").attr('href', 'assets/css/app-rtl.min.css');
        $("html").attr("dir", 'rtl');
        sessionStorage.setItem("is_visited", "rtl-mode-switch");
    }
}

function updateRadio(radioId) {
    document.getElementById(radioId).checked = true;
}

function ManageLayout(
    _layoutValue,
    _layoutModeValue,
    _layoutWidthValue,
    _layoutTopbarThemeValue,
    _layoutLeftSidebarTypeValue,
    _layoutLeftSidebarColorValue,
    _layoutDirectionValue) {

    var bodyTag = document.getElementsByTagName("BODY")[0];

    if (bodyTag.hasAttribute("data-topbar"))
        bodyTag.removeAttribute("data-topbar");

    if (bodyTag.hasAttribute("data-layout"))
        bodyTag.removeAttribute("data-layout");

    if (bodyTag.hasAttribute("data-sidebar"))
        bodyTag.removeAttribute("data-sidebar");

    if (bodyTag.hasAttribute("data-sidebar-size"))
        bodyTag.removeAttribute("data-sidebar-size");

    if (bodyTag.hasAttribute("data-keep-enlarged"))
        bodyTag.removeAttribute("data-keep-enlarged");

    if (bodyTag.hasAttribute("data-layout-size"))
        bodyTag.removeAttribute("data-layout-size");

    if (bodyTag.classList.contains("vertical-collpsed"))
        bodyTag.classList.remove("vertical-collpsed");

    if (_layoutValue == 'LAYOUT_VERTICAL') {
        switch (_layoutLeftSidebarColorValue) {
            case "LAYOUT_LEFTSIDEBAR_LIGHT": {
                bodyTag.setAttribute("data-sidebar", "light");
                break;
            }
            case "LAYOUT_LEFTSIDEBAR_DARK": {
                bodyTag.setAttribute("data-sidebar", "dark");
                break;
            }
            case "LAYOUT_LEFTSIDEBAR_COLORED": {
                bodyTag.setAttribute("data-sidebar", "colored");
                break;
            }
            default:
        }

        switch (_layoutLeftSidebarTypeValue) {
            case "LAYOUT_LEFTSIDEBAR_COMPACT": {
                bodyTag.setAttribute("data-sidebar-size", "small");
                break;
            }
            case "LAYOUT_LEFTSIDEBAR_ICON": {
                bodyTag.setAttribute("data-sidebar-size", "sm");
                break;
            }
            default: {
                bodyTag.setAttribute("data-sidebar-size", "lg");
                break;
            }
        }
    }
    else {
        bodyTag.setAttribute("data-layout", "horizontal");
    }

    switch (_layoutModeValue) {
        case "LAYOUT_MODE_LIGHT": {
            bodyTag.setAttribute('data-layout-mode', 'light');
            break;
        }
        case "LAYOUT_MODE_DARK": {
            bodyTag.setAttribute('data-layout-mode', 'dark');
            break;
        }
        default:
    }

    switch (_layoutTopbarThemeValue) {
        case "LAYOUT_TOPBAR_LIGHT": {
            bodyTag.setAttribute("data-topbar", "light");
            break;
        }
        case "LAYOUT_TOPBAR_DARK": {
            bodyTag.setAttribute("data-topbar", "dark");
            break;
        }
        case "LAYOUT_TOPBAR_COLORED": {
            bodyTag.setAttribute("data-topbar", "colored");
            break;
        }
    }
    switch (_layoutWidthValue) {
        case "LAYOUT_WIDTH_FLUID": {
            bodyTag.setAttribute("data-layout-size", "fluid");
            break;
        }
        case "LAYOUT_WIDTH_BOXED": {
            bodyTag.setAttribute("data-layout-size", "boxed");
            break;
        }
    }
    switch (_layoutDirectionValue) {
        case "LAYOUT_DIRECTION_LTR": {
            document.getElementsByTagName("html")[0].removeAttribute("dir");
            document.getElementById('bootstrap-style').setAttribute('href', 'assets/css/bootstrap.min.css');
            document.getElementById('app-style').setAttribute('href', 'assets/css/app.min.css');
            break;
        }
        case "LAYOUT_DIRECTION_RTL": {
            document.getElementById('bootstrap-style').setAttribute('href', 'assets/css/bootstrap-rtl.min.css');
            document.getElementById('app-style').setAttribute('href', 'assets/css/app-rtl.min.css');
            document.getElementsByTagName("html")[0].setAttribute("dir", "rtl");
            break;
        }
    }
}

function initAuthBackground() {
    //metis menu
    $("body").addClass("authentication-bg");
}

function removeAuthBackground() {
    //metis menu
    $("body").removeClass("authentication-bg");
}

function SetUrlTitle(title) {
    document.title = title + " | " + titleName;
}

function init() {
    initActiveMenu();
    initMenuItemScroll();
    initDropdownMenu();
    initComponents();
    initSettings();
    initPreloader();
    Waves.init();
    removeAuthBackground();
}

function initMenus() {
    initMetisMenu();
    initLeftMenuCollapse();
    initFullScreen();
    initHoriMenuActive();
    initRightSidebar();
}


//Spinner
function hideSpinner() {
    document.getElementById('spinner').classList.add("hide")
}

function showSpinner() {
    document.getElementById('spinner').classList.remove("hide")
}