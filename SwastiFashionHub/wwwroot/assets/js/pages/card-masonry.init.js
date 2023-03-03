/*
Template Name: Minible - Admin & Dashboard Template
Author: Themesbrand
Website: https://themesbrand.com/
Contact: themesbrand@gmail.com
File: card masonry init js
*/

function initMasonry() {
    var container = document.querySelector('#masonry');
    var masonry = new Masonry(container, {
        itemSelector: '.col-sm-6',
        percentPosition: true
    });
}