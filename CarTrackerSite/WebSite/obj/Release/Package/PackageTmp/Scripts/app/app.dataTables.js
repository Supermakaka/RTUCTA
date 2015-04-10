var app = (function (parent) {

    var me = {};
    parent.dataTables = me;

    function isFilterChanged(filter) {
        if (filter.data('prevVal') != null) {
            if (filter.val() == filter.data('prevVal'))
                return false;
        } else {
            //if there is no previous data, but filter value is empty, the value has not changed
            if (filter.val() == "")
                return false;
        }

        filter.data('prevVal', filter.val());

        return true;
    };

    function applyFilter(filter, reload) {
        //only apply the filter if value has been changed
        if (!isFilterChanged(filter))
            return;

        var tableWrapper;

        var filteringTableSelector = filter.closest(".dt-filters").attr("data-dt-table");

        if (filteringTableSelector != null)
            tableWrapper = $(filteringTableSelector).closest(".dataTables_wrapper");
        else
            tableWrapper = filter.closest(".dt-filters").parent().find(".dataTables_wrapper");

        $(tableWrapper).each(function (index) {

            var table = $(this).find(".table:last").DataTable(); //:last is workaround for datatables.scroller plug-in

            var columnName = filter.attr("data-column-name");

            if (columnName != null && columnName != "")
                table.column(columnName + ":name").search(filter.val());
            else
                table.search(filter.val());

            if (reload)
                table.draw();
        });

        if (filter.hasClass('daterange')) {
            if (filter.val() == '') {
                $(".daterangepicker li").removeClass('active');
            }
        }
    };

    me.applyDefaultFilters = function (table) {
        var t = $(table);

        if (t.data('defaultFiltersApplied') != null)
            return;

        t.closest(".dataTables_wrapper").parent().find(".dt-filter").each(function () {
            applyFilter($(this), false);
        });

        t.data('defaultFiltersApplied', true);
    };

    me.onReady = function () {
        $(".dt-filter").change(function () {

            applyFilter($(this), true);
        });

        $(".dt-filter").keyup($.debounce(150, function () {

            var filter = $(this);

            applyFilter(filter, true);
        }));
    };

    return parent;
})(app || {});