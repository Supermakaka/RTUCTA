var app = (function (parent) {

    var me = {};
    parent.mmenu = me;

    function initMenu() {

        var leftMenu = $("#left-menu");

        $.when(

            leftMenu.mmenu({

                classes: "mm-white"

            }).on("opening.mm", function () {

                menuOpened = true;
                $('.menu-tooltip').tooltipster('disable');

            }).on("closing.mm", function () {

                menuOpened = false;
                $('.menu-tooltip').tooltipster('enable');

            })

        ).done(function () {

            menuOpened = false;

            $('.menu-tooltip').tooltipster({
                delay: 50,
                animation: 'slide',
                offsetX: -230,
                theme: 'tooltipster-custom',
                position: 'right',
            });

            $('.modal-wrap').appendTo('body');

            $(window).on('resize', function () {
                toggleMenuDisplayMode();
            });

            $(window).scroll(function (event) {
                var scroll = $(window).scrollTop();

                if (scroll > 50) {
                    $(".mm-menu .mm-list").addClass("scrolled-down");
                }
                else {
                    $(".mm-menu .mm-list").removeClass("scrolled-down");
                }
            });

            toggleMenuDisplayMode();

            //leftMenu.jScrollPane({
            //    autoReinitialise: true,
            //    autoReinitialiseDelay: 100,
            //    verticalGutter: 2
            //});
        });
    };

    function identifyMenuDisplayMode() {

        Modernizr.mq('(min-width: 1250px) and (max-width: 1349px)') ? $('html').addClass('mm-iconbar-mode') : $('html').removeClass('mm-iconbar-mode');

        Modernizr.mq('(min-width: 1350px)') ? $('html').addClass('mm-sidemenu-mode') : $('html').removeClass('mm-sidemenu-mode');

        Modernizr.mq('(min-width: 1500px)') ? $('html').addClass('wide') : $('html').removeClass('wide');

    }

    function toggleMenuDisplayMode() {

        identifyMenuDisplayMode();

        $('.menu-tooltip').tooltipster('disable');

        if ($('html').hasClass('mm-iconbar-mode') && !menuOpened)
            $('.menu-tooltip').tooltipster('enable');
    }

    me.onReady = function () {
        initMenu();
    };

    return parent;
})(app || {});