$(function() {
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
    
    $(document).delegate('*[data-toggle="lightbox"]', 'click', function (event) {
        event.preventDefault();
        $(this).ekkoLightbox();
    });

    $('div[data-src]').imageLoader();
});

(function () {
    var disqus_shortname = document.domain == 'www.jdibble.co.uk' ? 'jdibble' : 'test-jdibble';

    var dsq = document.createElement('script'); dsq.type = 'text/javascript'; dsq.async = true;
    dsq.src = '//' + disqus_shortname + '.disqus.com/embed.js';
    (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);
})();