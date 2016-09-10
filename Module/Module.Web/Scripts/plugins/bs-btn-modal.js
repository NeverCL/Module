(function (root, factory) {
    if (typeof define === 'function' && define.amd) {
        define(['jquery'], factory);
    } else factory();
}(window, function ($) {
    'use strict';
    function BtnModal(opt) {
        this.opt = opt;
        this.$modal = {};
        this.init();
    }

    BtnModal.prototype = {
        defaults: {
            title: '',
            url: '',
            html: '',
            size: ''
        },
        init: function () {
            var opt = $.extend(this.defaults, this.opt);
            if (!opt.html) {
                $.ajax({
                    url: opt.url,
                    async: false,
                    success: function (rst) {
                        opt.html = rst;
                    }
                });
            }
            this.$modal = $('<div class="modal fade"><div class="modal-dialog ' + opt.size + '"><div class="modal-content"><div class="modal-header"><a class="close" data-dismiss="modal"><span>&times;</span></a><h4 class="modal-title">' + opt.title + '</h4></div><div class="modal-body">' + opt.html + '</div></div></div></div>');
            this.opt = opt;
        },
        show: function () {
            var modal = this.$modal;
            modal.appendTo($('body'));
            modal.modal({
                backdrop: 'static',
                show: true,
                keyboard: false
            });
            modal.on('hidden.bs.modal', function () { this.remove(); });
        }
    };

    $.extend({
        btnModal: function (opt) {
            new BtnModal(opt).show();
        }
    });

    $(document).on('click', '.bs-btn-modal', function (e) {
        new BtnModal($(e.target).data()).show();
        return false;
    });
}));