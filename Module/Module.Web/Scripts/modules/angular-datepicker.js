(function (root, factory) {
    if (typeof root.define === 'function' && root.define.amd) {
        root.define(['jquery', 'moment', 'angular', 'datepicker'], function (jquery, moment) {
            factory(jquery, moment);
        });
    } else factory(root.jQuery);
}(window, function ($, moment) {
    'use strict';
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
            require: 'ngModel',
            link: dateLink
        }
        function dateLink(scope, ele, attrs, modelCtrl) {
            var picker = $(ele);
            var opts = $.extend(cfg, picker.data());
            if (attrs.single === 'false')
                opts.singleDatePicker = false;
            if (attrs.date) {
                if (attrs.date === 'time') {
                    opts.locale.format = 'YYYY-MM-DD HH:mm';
                    opts.timePicker = true;
                    opts.timePicker24Hour = true;
                }
                if (attrs.date === 'times') {
                    opts.locale.format = 'YYYY-MM-DD HH:mm:ss';
                    opts.timePicker = true;
                    opts.timePicker24Hour = true;
                    opts.timePickerSeconds = true;
                }
            }
            if (attrs.format)
                opts.locale.format = attrs.format;
            picker.daterangepicker(opts, function (start, end) {
                if (this.element.is('input') && !this.singleDatePicker) {
                    this.element.val(start.format(this.locale.format) + this.locale.separator + end.format(this.locale.format));
                    this.element.trigger('change');
                } else if (this.element.is('input')) {
                    this.element.val(start.format(this.locale.format));
                    this.element.trigger('change');
                }
                var val = start.format(opts.locale.format);
                modelCtrl.$setViewValue(val);
            });

            modelCtrl.$formatters.push(function (objValue) {
                var f = function (date) {
                    if (!moment.isMoment(date)) {
                        return moment(date).format(opts.locale.format);
                    } else {
                        return date.format(opts.locale.format);
                    }
                };
                if (opts.singleDatePicker && objValue) {
                    return f(objValue);
                } else if (objValue && objValue.startDate) {
                    return [f(objValue.startDate), f(objValue.endDate)].join(opts.locale.separator);
                } else {
                    return '';
                }
            });
        }
    }]);
}));