define(['angular'], function () {
    angular.module('ng.bs.pager', []).constant('pagerCfg', {
        size: 10,
        index: 1,
        previousText: '«',
        nextText: '»'
    }).directive('pager', ['pagerCfg', function (cfg) {
        return {
            template: tmpPager,
            replace: true,
            scope: {
                index: '@',
                size: '@',
                total: '@',
                count: '@',
                action: '&'
            },
            link: pageLink
        }

        function tmpPager(el, attrs) {
            return '<div class="form-inline">' +
                 '<label>共<span ng-bind="count"></span>条数据,每页<span ng-bind="size"></span>条数据</label>' +
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
                 '<li ng-click="actionLi(index - 0 + 1)" ng-class="{disabled:index>=total}" ng-hide="count==0">' +
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
            scope.previousText = cfg.previousText;
            scope.nextText = cfg.nextText;
        }

        function buildItem(scope, item) {
            return {
                value: item,
                action: function () {
                    scope.actionLi(item);
                }
            }
        }

        function actionLi(item) {
            var scope = this;
            if (item < 1 || item > scope.total)
                return;
            scope.index = item;
            scope.action({
                index: item,
                size: scope.size
            });
        }

        function build(scope) {
            initScope(scope);
            var pages = [];
            for (var i = 0; i < scope.total; i++) {
                pages.push(buildItem(scope, i + 1));
            }
            scope.pages = pages;
        }
    }]);
});