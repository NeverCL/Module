(function (root, factory) {
    if (typeof root.define === 'function' && root.define.amd) {
        root.define(['angular', 'file'], factory);
    } else factory();
}(window, function () {
    'use strict';
    angular.module('ng.bs.file', []).directive('file', function () {
        return {
            require: 'ngModel',
            restrict: 'C',
            link: fileLink
        };
        function fileLink(scope, ele, attr, ngModel) {
            $(ele).fileinput({ uploadUrl: cfg.hostName + 'UploadFile.ashx' });
            if (attr.multiple) {
                //todo
            } else {
                $(ele).on('fileuploaded', function (evt, data) { var rst = data.response; ngModel.$setViewValue(rst[0]); });
            }
        }
    });
}));