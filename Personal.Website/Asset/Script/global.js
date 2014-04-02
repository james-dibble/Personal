var _gaq = _gaq || [];
_gaq.push(['_setAccount', 'UA-26477644-1']);
_gaq.push(['_trackPageview']);
(function () {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})();

(function ($) {
    $.fn.hideSquare = function (index, originalNumberOfSquares, callback) {
        var selector = $(this);
        
        $($(selector)[index]).toggle(
            {
                effect: 'slide',
                duration: (($(selector).length / originalNumberOfSquares) * 150),
                direction: 'left',
                complete: function() {
                    $($(selector)[index]).remove();
                    
                    if (index === 0) {
                        callback();
                        return this;
                    }
                    
                    index--;

                    return $(selector).hideSquare(index, originalNumberOfSquares, callback);
                }
            });
    };
    
    $.fn.showSquare = function (index, callback) {
        var selector = $(this);

        $($(selector)[index]).toggle({
            effect: 'slide',
            duration: (($(selector).length - index) / $(selector).length) * 250,
            direction: 'left',
            complete: function() {
                if ((index + 1) >= $(selector).length) {
                    callback();
                    return this;
                }

                index++;

                return $(selector).showSquare(index, callback);
            }
        });
    };
})(jQuery);

$(function () {
    $('.nav .text').hide();

    $('.sidebar .btn').click(function () {
        $('.nav .text').stop().toggle({
            effect: 'slide',
            duration: 250,
            direction: 'left'
        });
    });
    
    $('a:not([target])').click(function (e) {
        e.preventDefault();

        var $self = $(this);

        if ($('.animate, .jumbotron').length === 0) {
            document.location = $self.attr('href');
            return true;
        }

        $('.animate, .jumbotron').hideSquare($('.animate, .jumbotron').length - 1, $('.animate, .jumbotron').length, function () {
            $('#loading').showSquare(0, function() {});
            document.location = $self.attr('href');
        });

        return false;
    });
    
    var request = "/" + window.location.pathname.split("/", 2)[1];
    $("ul.nav a[href ^=\"" + request + "\"]").first().parent().addClass("active");

});