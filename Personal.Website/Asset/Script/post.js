$(function () {
    if (window.location.pathname.match('^/blog/[0-9]{4}$') || window.location.pathname.match('^/blog/[0-9]{4}/')) {
        var year = window.location.pathname.split("/", 3)[2];

        $(['.blog-header-year li a[data-year="', year, '"]'].join('')).parent().addClass("active");
    }
    
    if (window.location.pathname.match('^/blog/[0-9]{1,2}$')) {
        var month = window.location.pathname.split("/", 3)[2];

        $(['.blog-header-month li a[data-month="', month, '"]'].join('')).parent().addClass("active");
    }
    
    if (window.location.pathname.match('^/blog/[0-9]{4}/[0-9]{1,2}$') || window.location.pathname.match('^/blog/[0-9]{4}/[0-9]{1,2}/')) {
        var month = window.location.pathname.split("/", 4)[3];

        $(['.blog-header-month li a[data-month="', month, '"]'].join('')).parent().addClass("active");
    }
    
    $('.animate').not('#loading').hide();

    if ($('div[data-src]').length === 0) {
        $('#loading').toggle(
                {
                    effect: 'slide',
                    duration: 250,
                    direction: 'left',
                    complete: function () {
                        $('.animate').showSquare(0, function () { });
                    }
                });
        return;
    }

    $('div[data-src]').imageLoader({
        allImagesLoaded: function () {
            $('#loading').toggle(
                {
                    effect: 'slide',
                    duration: 250,
                    direction: 'left',
                    complete: function () {

                        $('.animate').showSquare(0, function (){});
                    }
                });
        }
    });
});

(function () {
    var disqus_shortname = document.domain == 'www.jdibble.co.uk' ? 'jdibble' : 'test-jdibble';

    var dsq = document.createElement('script'); dsq.type = 'text/javascript'; dsq.async = true;
    dsq.src = '//' + disqus_shortname + '.disqus.com/embed.js';
    (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);
})();

window.___gcfg = { lang: 'en-GB' };

(function () {
    var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
    po.src = 'https://apis.google.com/js/platform.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
})();

(function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "https://platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } }(document, "script", "twitter-wjs"));