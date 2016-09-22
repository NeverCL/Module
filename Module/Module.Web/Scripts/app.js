require.config({
    paths: {
        angular: 'angular.min',
        uiRoute: 'angular-ui-router.min',
        lang: 'i18n/angular-locale_zh-cn',
        ngAnimate: 'angular-animate.min'
    },
    shim: {
        uiRoute: ['lang'],
        angular: ['cfg', 'jquery'],
        lang: ['angular'],
        ngAnimate: ['lang']
    }
});

define(['ngAnimate', 'uiRoute', 'modules/angular-pager', 'modules/angular-common', 'modules/angular-datepicker', 'modules/angular-datepicker', 'modules/angular-file'], function () {
    var app = angular.module("myApp", ['ngAnimate', 'ui.router', 'ng.common', 'ng.bs.datepicker', 'ng.bs.pager', 'ng.editor', 'ng.bs.file'])
       .config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
           app.register = {
               controller: $controllerProvider.register,
               directive: $compileProvider.directive,
               filter: $filterProvider.register,
               service: $provide.service
           };
       });
    app.config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('home');
        $stateProvider
            .state("home", getCfg('/home', 'homeCtrl', 'home.html', 'homeCtrl'))
        ;
    });
    function getCfg(url, ctrlName, templateUrl, ctrlUrl) {
        return {
            url: url,
            controller: ctrlName,
            templateUrl: templateUrl,
            resolve: {
                loadCtrl: [
                    "$q", function ($q) {
                        var deferred = $q.defer();
                        require(['controller/' + ctrlUrl], function () { deferred.resolve(); });
                        return deferred.promise;
                    }
                ]
            }
        };
    }

    app.controller('baseCtrl', function ($scope, $http, $filter) {
        $scope.load = true;

        setInterval(function () { $scope.$apply(function () { $scope.currentTime = $filter('date')(new Date, 'yyyy-MM-dd hh:mm:ss') }) }, 1000);

        if (cfg.debug === 'localc#' || cfg.debug === 'localweb') {
            $scope.user = {
                DisplayName: '管理员',
                Roles: [{
                    Name: 'admin'
                }]
            }
            $scope.load = false;
        } else {
            $.get(cfg.host + 'SsoLogin/GetToken', function (token) {
                if (token) {
                    $.ajax({
                        url: cfg.userUrl,
                        headers: {
                            Authorization: 'Bearer ' + token
                        },
                        success: function (rst) {
                            $scope.user = rst;
                            $scope.load = false;
                        }
                    })
                } else {
                    window.location = cfg.host + 'SsoLogin/LoginUrl';
                }
            }).complete(function (rst) {
                if (rst.status === 500)
                    window.location = cfg.host + 'SsoLogin/LoginUrl';
            });
        }

        $scope.isInRole = function () {
            if (!$scope.user || !$scope.user.Roles)
                return false;
            var roles = $scope.user.Roles;
            var tmpRoles = arguments;
            for (var i = 0; i < roles.length; i++) {
                for (var j = 0; j < tmpRoles.length; j++) {
                    if (roles[i].Name === tmpRoles[j]) 
                        return true;
                }
            }
            return false;
        }
    });
    return app;
});