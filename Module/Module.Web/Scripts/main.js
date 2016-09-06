require.config({
    paths: {
        jquery: ['jquery-1.9.1.min', '//cdn.bootcss.com/jquery/1.12.4/jquery'],
        bootstrap: 'bootstrap.min'
    },
    shim: {
        metisMenu: ['bootstrap'],
        bootstrap: ['jquery']
    }
});

require(['css!../content/bootstrap.min', 'css!../Content/metisMenu.min', 'css!../content/font-awesome.min', 'css!../content/site'
, 'metisMenu'], function () {
    $('#side-menu').metisMenu();
});

require(['app'], function () {
    angular.bootstrap(document, ['myApp']);
});