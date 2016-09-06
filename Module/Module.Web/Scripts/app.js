require.config({
    paths: {
        angular: 'angular.min',
        uiRoute: 'angular-ui-router.min',
        lang: 'i18n/angular-locale_zh-cn',
        ngAnimate: 'angular-animate.min'
    },
    shim: {
        uiRoute: ['lang'],
        lang: ['angular'],
        ngAnimate: ['lang']
    }
});

define(['ngAnimate', 'uiRoute'], function () {
    var app = angular.module("myApp", ['ngAnimate', 'ui.router'])
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
    return app;
});