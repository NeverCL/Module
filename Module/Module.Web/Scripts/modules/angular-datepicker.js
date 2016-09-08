(function (root, factory) {
    if (typeof define === 'function' && define.amd) {
        define(['jquery', 'angular', 'datepicker'], function (jquery) {
            factory(jquery);
        });
    } else factory(root.jQuery);
}(window, function ($) {
    angular.module('ng.bs.datepicker', []).constant('datepickerCfg', {
        "locale": {
            "format": "YYYY-MM-DD",
            "separator": " - ",
            "applyLabel": "确定",
            "cancelLabel": "取消",
            "fromLabel": "From",
            "toLabel": "To",
            "customRangeLabel": "Custom",
            "daysOfWeek": [
                "日",
                "一",
                "二",
                "三",
                "四",
                "五",
                "六"
            ],
            "monthNames": [
                "1月",
                "2月",
                "3月",
                "4月",
                "5月",
                "6月",
                "7月",
                "8月",
                "9月",
                "10月",
                "11月",
                "12月"
            ],
            "firstDay": 1
        },
        singleDatePicker: true
    }).directive('date', ['datepickerCfg', function (cfg) {
        return {
            scope: {
                format: '@',
                single: '@'
            },
            link: dateLink
        }
        function dateLink(scope, ele) {
            var picker = $(ele);
            var opt = $.extend(cfg, picker.data());
            if (scope.format)
                opt.locale.format = scope.format;
            if (scope.single === 'false')
                opt.singleDatePicker = false;
            picker.daterangepicker(opt);
        }
    }]);
}));