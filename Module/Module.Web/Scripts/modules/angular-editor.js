(function (root, factory) {
    if (typeof root.define === 'function' && root.define.amd) {
        root.define(['ume', 'angular'], factory);
    } else factory(UM);
}(window, function (um) {
    'use strict';
    angular.module('ng.editor', [])
        .constant('editorCfg', {
            id: 'container',
            style: {
                width: '500px',
                height: '300px'
            }
        })
        .directive('editor', ['editorCfg', function (cfg) {
            return {
                require: 'ngModel',
                replace: true,
                template: '<script type="text/plain"></script>',
                link: fileLink
            };
            function fileLink(scope, ele, attr, ngModel) {
                var idName = attr.id ? attr.id : cfg.id;
                ele.attr('id', idName);
                if (!(attr.width || attr.height))
                    ele.css(cfg.style);
                var editor = um.createEditor(idName);
                ngModel.$render = function () { editor.ready(function () { editor.setContent(ngModel.$modelValue + ''); }); };
                editor.ready(function () { editor.addListener('blur', function () { ngModel.$setViewValue(editor.getContent()); }); });
            }
        }]);
}));