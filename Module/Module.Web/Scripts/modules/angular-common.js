(function (root, factory) {
    if (typeof root.define === 'function' && root.define.amd) {
        root.define(['angular'], factory);
    } else factory();
}(window, function () {
    'use strict';
    angular.module('ng.common', []).filter('check', function () {
        return function (value, params) {
            params = params || ['是', '否'];
            value = value ? params[0] : params[1];
            return value;
        };
    }).filter('set', function () {
        return function (value, params) {
            return params[value];
        };
    });
}));