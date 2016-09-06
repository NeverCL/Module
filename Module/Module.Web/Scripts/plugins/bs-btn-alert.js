define(['jquery'], function ($) {
    function DelModal(opt) {
        this.opt = opt;
        this.$modal = {};
        this.init();
    }

    DelModal.prototype = {
        defaults: {
            msg: '确认删除?',
            url: '',
            host: cfg.host
        },
        init: function () {
            opt = $.extend(this.defaults, this.opt);
            var modal = $('<div class="modal fade" style="margin-top: 10%;"><div class="modal-dialog modal-sm"><div class="modal-content"><div class="modal-body">' + opt.msg + '</div><div class="modal-footer"><a data-dismiss="modal" class="btn btn-default btn-sm">取消</a><button class="btn btn-primary btn-sm">确认</button></div></div></div></div>');
            modal.appendTo($('body'));
            this.$modal = modal;
        },
        show: function () {
            var modal = this.$modal;
            modal.modal({
                backdrop: 'static',
                show: true,
                keyboard: false
            });

            var that = this;
            modal.find('.btn-primary:contains("确认")').on('click', function () {
                if (that.defaults.url) {
                    var url = that.defaults.host + that.defaults.url;
                    $.get(url).complete(function () {
                        modal.modal('hide');
                        $('.btn-default:contains("查询")').click();
                    });
                } else {
                    modal.modal('hide');
                }
            });

            modal.on('hidden.bs.modal', function () { this.remove(); });
        }
    };

    $(document).on('click', '.bs-btn-alert', function (e) {
        new DelModal($(e.target).data()).show();
        return false;
    });
});