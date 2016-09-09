require.config({
    paths: {
        jquery: ['jquery-1.9.1.min', '//cdn.bootcss.com/jquery/1.12.4/jquery'],
        bootstrap: 'bootstrap.min',
        datepicker: 'daterangepicker'
    },
    shim: {
        metisMenu: ['bootstrap'],
        bootstrap: ['jquery', 'cfg'],
        datepicker: ['bootstrap']
    }
});

require(['css!../content/bootstrap.min', 'css!../Content/metisMenu.min'
    , 'css!../content/font-awesome.min', 'css!../content/site'
    , 'css!../content/daterangepicker', 'datepicker'
    , 'metisMenu', 'plugins/bs-btn-alert', 'plugins/bs-btn-modal'], function () {
        $('#side-menu').metisMenu();
    });

require(['app'], function () {
    angular.bootstrap(document, ['myApp']);
});