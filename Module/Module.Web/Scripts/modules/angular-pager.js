//增加 超过10页自动省略算法
//分页页面业务逻辑参考网易音乐
(function (root, factory) {
    if (typeof root.define === 'function' && root.define.amd) {
        root.define(['angular', 'datepicker'], factory);
    } else factory();
}(window, function () {
    'use strict';
    angular.module('ng.bs.pager', []).constant('pagerCfg', {
        size: 15,
        index: 1,
        previousText: '«',
        nextText: '»',
        omitText: '...'
    }).directive('pager', ['pagerCfg', function (cfg) {
        return {
            template: tmpPager,
            replace: true,
            scope: {
                index: '@',//第几页 从1开始
                size: '@',//每页大小
                total: '@',//总分页数
                count: '@',//总数据量
                action: '&'//执行方法
            },
            link: pageLink
        }

        function tmpPager(el, attrs) {
            return '<div class="form-inline" ng-show="count!=-1">' +
                 '<label>{{"共条"+count+"数据,每页"+size+"条数据"}}</label>' +
                 '<ul class="pagination pull-right" style="margin:0;">' +
                 '<li ng-click="actionLi(index-1)" ng-class="{disabled:index<=1}" ng-hide="count==0">' +
                 '<a>' +
                 '<span ng-bind="previousText"></span>' +
                 '</a>' +
                 '</li>' +
                 '<li ng-repeat="item in pages" ng-click="item.action()" ng-class="{active:item.value==index}">' +
                 '<a>' +
                 '<span ng-bind="item.value"></span>' +
                 '</a>' +
                 '</li>' +
                 '<li ng-click="actionLi(index-0+1)" ng-class="{disabled:index>=total}" ng-hide="count==0">' +
                 '<a>' +
                 '<span ng-bind="nextText"></span>' +
                 '</a>' +
                 '</li>' +
                 '</ul>' +
                 '</div>';
        }

        function pageLink(scope) {
            scope.$watchCollection('[index,size,total,count]', function () {
                build(scope);
            });
        }

        function initScope(scope) {
            scope.actionLi = actionLi;
            scope.size = scope.size || cfg.size;
            scope.index = scope.index || cfg.index;
            if (scope.count)
                scope.total = Math.ceil(scope.count / scope.size);
            else
                scope.count = -1;
            scope.previousText = cfg.previousText;
            scope.nextText = cfg.nextText;
        }

        function buildItem(scope, item) {
            return {
                value: item,
                action: function () {
                    if (item != cfg.omitText)
                        scope.actionLi(item);
                }
            }
        }

        function actionLi(item) {
            var scope = this;
            if (item < 1 || item > scope.total)
                return;
            scope.action({
                index: item,
                size: scope.size
            });
            scope.index = item;
        }

        function build(scope) {
            initScope(scope);
            //1. 10页以内全显示
            //2. 超过10页 显示靠近的9页 及相反的1页
            var pages = [];
            if (!scope.total)
                return;
            if (scope.total <= 10) {
                for (var i = 0; i < scope.total; i++) {
                    pages.push(buildItem(scope, i + 1));
                }
            } else {
                pages.push(buildItem(scope, 1));
                if (scope.index > 5)
                    pages.push(buildItem(scope, cfg.omitText));
                var len = 7;
                var dir = scope.total / 2 > scope.index;
                var pageArr = [];
                for (var i = 0; i < len; i++) {
                    if (dir) {
                        var index = scope.index - 3 + i;
                        if (index > 1)
                            pageArr.push(buildItem(scope, index));
                        else
                            len++;
                    } else {
                        var index = scope.index - i + 3;
                        if (index < scope.total)
                            pageArr.push(buildItem(scope, index));
                        else
                            len++;
                    }
                }
                pages = pages.concat(pageArr.sort(sortNumber));
                if (scope.index < scope.total - 5)
                    pages.push(buildItem(scope, cfg.omitText));
                pages.push(buildItem(scope, scope.total));
            }
            scope.pages = pages;
        }

        function sortNumber(a, b) {
            return a.value - b.value;
        }
    }]);
}))