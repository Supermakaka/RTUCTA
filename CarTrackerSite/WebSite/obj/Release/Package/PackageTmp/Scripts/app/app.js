
var app = (function (me) {

    var menuOpened = false;

    function initForms() {
        $('.on-error-jump-to-tab').submit(function () {

            var form = $(this);
            var firstError = form.find(".tab-content .input-validation-error:first");

            if (firstError.length > 0)
                form.find('.nav-tabs a[href="#' + firstError.closest(".tab-pane").attr("id") + '"]').tab('show');
        });
    };

    function initCheckboxes() {
        $('input:not(.radio-button)').iCheck({
            checkboxClass: 'icheckbox_square-red',
            radioClass: 'iradio_square-red'
        });
    };

    function initTooltips() {
        $('.show-tooltip').tooltipster({
            animation: 'fade',
            theme: 'tooltipster-custom',
            touchDevices: false,
            functionBefore: function (origin, continueTooltip) {

                var dataPosition = origin.attr('data-tooltip-position');

                //customizing position
                if (dataPosition != null && origin.tooltipster('option', 'position') != dataPosition)
                    origin.tooltipster('option', 'position', dataPosition);

                continueTooltip();
            }
        });
    };

    function showNotification(text) {

        setTimeout(
            function () {
                noty({
                    layout: 'topCenter',
                    theme: 'custom',
                    type: 'success',
                    text: text, // can be html or string
                    template: '<div class="noty_message"><span class="noty_text"></span><div class="noty_close"></div></div>',
                    animation: {
                        open: 'animated fadeIn',
                        close: 'animated fadeOut'
                    },
                    timeout: 3000, // delay for closing event. Set false for sticky notifications
                    closeWith: ['click'],
                    callback: {
                        onShow: function () { },
                        afterShow: function () { },
                        onClose: function () { },
                        afterClose: function () { },
                        onCloseClick: function () { },
                    },
                })
            },
            400
        );
    };

    function showError(text) {

        setTimeout(
            function () {
                noty({
                    layout: 'topCenter',
                    theme: 'custom',
                    type: 'error',
                    text: text, // can be html or string
                    template: '<div class="noty_message"><span class="noty_text"></span><div class="noty_close"></div></div>',
                    animation: {
                        open: 'animated fadeIn',
                        close: 'animated fadeOut'
                    },
                    timeout: 5000,
                    closeWith: ['click', 'button'],
                    callback: {
                        onShow: function () { },
                        afterShow: function () { },
                        onClose: function () { },
                        afterClose: function () { },
                        onCloseClick: function () { },
                    },
                })
            },
            400
        );
    };

    function initAjax() {
        $.ajaxSetup({
            cache: false,
            error: function (jqXHR, exception) {
                if (jqXHR.status === 0) {
                    showError('This resource is not available. Please verify your connection.');
                } else if (jqXHR.status == 404) {
                    showError('Requested page is not found.');
                } else if (exception === 'timeout') {
                    showError('Request timed out.');
                } else if (exception === 'abort') {
                    showError('Request aborted.');
                } else {
                    showError(jqXHR.responseText);
                }
            }
        });
    };

    me.onReady = function () {
        initAjax();
        initForms();
        initCheckboxes();
        initTooltips();
    };

    me.initTooltips = initTooltips;
    me.showNotification = showNotification;
    me.showError = showError;

    return me;
})(app || {});