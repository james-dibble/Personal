﻿$(function () {
    $('.animate, .jumbotron').hide();

    $('div[data-src]').imageLoader({
        allImagesLoaded: function () {
            $('#loading').toggle(
                {
                    effect: 'slide',
                    duration: 250,
                    direction: 'left',
                    complete: function () {
                        $('.animate').showSquare(0, function() {
                            $('.jumbotron').delay(800).slideDown(2000, 'easeOutBack');
                        });
                    }
                });
        }
    });
});