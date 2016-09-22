(function (window) {
    var cfg = {
        hostName: '../',
        host: '../api/',
        userUrl: '..'
    };

    cfg.debug = 'localweb';//localweb/release

    if (cfg.debug === 'localweb') {
        //todo
        cfg.hostName = '';
        cfg.host = '';
    }

    window.cfg = cfg;
})(window)