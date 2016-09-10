define(['app'], function (app) {
    app.register
        .controller('homeCtrl', ['$scope', function ($scope) {
            $scope.title = 'hello';
        }]);
});
