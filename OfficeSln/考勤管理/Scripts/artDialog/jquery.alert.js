/*
* Written by:     haocy
* function:       对话框效果js库
* Created Date:   2011-11-29
*/
if (typeof (effect) == 'undefined') effect = { author: 'haocy', version: '1.0.0' };

effect.Dialog = {
    open: function (url, closecallback) {
        return art.dialog({
            id: 'Showopen',
            icon: 'succeed',
            fixed: true,
            lock: true,
            resize: false,
            content: msg,
            ok: true,
            close: closecallback
        });
    },
    show: function (msg) {
        return art.dialog({
            id: 'Show',
            icon: 'succeed',
            fixed: true,
            lock: true,
            resize: false,
            content: msg,
            ok: true
        });
    },
    alert: function (msg, fCallback) {
        return art.dialog({
            id: 'Alert',
            icon: 'warning',
            fixed: true,
            lock: true,
            resize: false,
            content: msg,
            ok: true,
            close: fCallback
        });
    }, confirmNew: function (msg, obj, confirmCallback, cancelCallback) {
        return art.dialog({
            id: 'Confirm',
            icon: 'question',
            fixed: true,
            lock: false,
            resize: false,
            content: msg,
            //            follow: $(obj),
            ok: function (here) {
                return confirmCallback.call(this, here);
            },
            cancel: function (here) {
                return cancelCallback && cancelCallback.call(this, here);
            }
        });
    },
    confirm: function (msg, confirmCallback, cancelCallback) {
        return art.dialog({
            id: 'Confirm',
            icon: 'question',
            fixed: true,
            lock: true,
            resize: false,
            content: msg,
            ok: function (here) {
                return confirmCallback.call(this, here);
            },
            cancel: function (here) {
                return cancelCallback && cancelCallback.call(this, here);
            }
        });
    },
    confirm: function (msg, islock, confirmCallback, cancelCallback) {
        return art.dialog({
            id: 'Confirm',
            icon: 'question',
            fixed: true,
            lock: islock,
            resize: false,
            content: msg,
            ok: function (here) {
                return confirmCallback.call(this, here);
            },
            cancel: function (here) {
                return cancelCallback && cancelCallback.call(this, here);
            }
        });
    }
};
function jDivShowNoLock(obj, txtTitle, closeCallback) {
    var auiId = obj + '_aui';
    if (art.dialog.list[auiId] == undefined) {
        if ($("#" + obj).length > 0) {
            art.dialog({
                id: auiId,
                title: txtTitle,
                fixed: true,
                lock: false,
                resize: false,
                padding: '0px',
                content: $('#' + obj)[0],
                follow: $("#" + obj),
                close: closeCallback

            });
        }
    }
    else {
        art.dialog.list[auiId].title(txtTitle);
        art.dialog.list[auiId].show();
    }
}
function jDivShow(obj, txtTitle, okfunction, closeCallback, cancelText) {
    
    if (cancelText == undefined) {
        cancelText = "取消";
    }
    var auiId = obj + '_aui';
    if (art.dialog.list[auiId] == undefined) {
        if ($("#" + obj).length > 0) {
            art.dialog({
                id: auiId,
                title: txtTitle,
                fixed: true,
                lock: true,
              //  background: "#FFFFFF",
               // opacity: 0.4,
                resize: false,
                padding: '0px',
                content: $('#' + obj)[0],
                close: closeCallback,
                ok: okfunction,
                cancelVal: cancelText,
                cancel: true //为true等价于function(){}
            });
        }
    }
    else {
        art.dialog.list[auiId].title(txtTitle);
        art.dialog.list[auiId].show();
    }
}
function jDivShowDown(obj, txtTitle, closeCallback) {
    var auiId = obj + '_aui';
    if (art.dialog.list[auiId] == undefined) {
        if ($("#" + obj).length > 0) {
            art.dialog({
                id: auiId,
                title: txtTitle,
                fixed: true,
                lock: false,
                resize: false,
                padding: '0px',
                content: $('#' + obj)[0],
                close: closeCallback,
                width: 320,
                height: 240,
                left: '100%',
                top: '100%',
                drag: false
            });
        }
    }
    else {
        art.dialog.list[auiId].title(txtTitle);
        art.dialog.list[auiId].show();
    }
}
function jDivShowObj(obj, txtTitle, closeCallback) {
    var auiId = obj + '_aui';
    if (art.dialog.list[auiId] == undefined) {
        if ($(obj).length > 0) {
            art.dialog({
                id: auiId,
                title: txtTitle,
                fixed: true,
                lock: true,
                resize: false,
                padding: '0px',
                content: $(obj)[0],
                close: closeCallback
            });
        }
    }
    else {
        art.dialog.list[auiId].title(txtTitle);
        art.dialog.list[auiId].show();
    }
}
function jOpenShowObj(obj, txtTitle, closeCallback) {
    var auiId = obj + '_aui';
    if (art.dialog.list[auiId] == undefined) {
        if ($(obj).length > 0) {
            $.dialog.open('/Tab/EditSoftInfo.aspx?did=' + did,
                    {
                        fixed: true,
                        resize: false,
                        lock: true,
                        title: "软件管理",
                        padding: '0px 0px',
                        width: 630,
                        heigth: 600,
                        close: closeCallback
                    });
        }
    }
    else {
        art.dialog.list[auiId].title(txtTitle);
        art.dialog.list[auiId].show();
    }
}


function jDivHide(obj) {
    var curDialog = art.dialog.list[obj + '_aui'];
    if (curDialog != null) {
        curDialog.hide();
    }
}
