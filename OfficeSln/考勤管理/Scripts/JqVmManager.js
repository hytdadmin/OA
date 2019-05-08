
//更改虚机运行状态
function ChVmStatus(VmName, Status, page, lapage) {
    startLoading('正在处理，请稍后...');
    $.ajax({
        type: "post",
        async: "false",
        url: "/UserManager/UserManager.ashx",
        data: { "op": "VmStatusCha", "VmName": VmName, "ChStatus": Status },
        complete: function () { stopLoading() },
        success: function (data) {
            if (data == "true") {

                if (page != "nopage") {
                    art.dialog({
                        title: '成功',
                        content: '操作成功',
                        lock: true,
                        background: '#FFF', // 背景色
                        opacity: 0.00,	// 透明度
                        icon: 'succeed',
                        ok: function () {
                            window.open(location.href, "_self");
                            return true;
                        }
                    });

                }
                if (page == "nopage") {
                    art.dialog({
                        title: '成功',
                        content: '操作成功',
                        lock: true,
                        background: '#FFF', // 背景色
                        opacity: 0.00,	// 透明度
                        icon: 'succeed',
                        ok: function () {
                            GetVmStatusInfo(VmName, lapage);
                            return true;
                        }
                    });
                }
            }
            else {
                AlertInfo("消息", "warning", "虚机操作失败，请重试！");
            }
        },
        error: function () {
            AlertInfo("消息", "warning", "虚机操作失败，请重试！");
        }
    });
}

function GetVmStatusInfo(vmname, lapage) {

    if (vmname == "") {
        $("#PerfIframeId").attr("src", "/UserManager/VmPerformance.aspx?type=f");
    }
    else {
        $.ajax({
            type: "post",
            dataType: "json",
            async: "false",
            url: "/UserManager/UserManager.ashx",
            data: { "op": "VmManager", "VmName": vmname },
            success: function (data) {
                if (data != "" && data != null) {
                    var Json_list = data;
                    $("#VmRunStatus").html(Json_list.VmRunStatus);
                    $("#SysAttr").html(Json_list.SysAttr);
                    $("#VmID").val(Json_list.VmID);
                    $("#hdsize").val(Json_list.VmHdSize);
                    //       alert(Json_list.VmExpirationDate);
                    if (Json_list.VmExpirationDate == "-1") {
                        $("#UnRun").attr("style", "display:");
                        $("#Run").attr("style", "display:none");
                        $("#PerfIframeId").attr("src", "/UserManager/VmPerformance.aspx?type=f");
                    }
                    else if (Json_list.VmExpirationDate == "WillExpire") {
                        $("#UnRun").attr("style", "display:");
                        $("#UnRun_Span_id").html("虚机还有 " + Json_list.Expire_DTime + " 天到期，请");
                        $("#Run").attr("style", "display:none");
                        if (Json_list.VmExpirationDate != "丢失" && Json_list.VmExpirationDate != "关机" && Json_list.VmExpirationDate != "到期") {
                            if (lapage == "1") {
                                $("#PerfIframeId").attr("src", "/UserManager/VmPerformance.aspx?type=t");
                            }

                        }
                        else
                            $("#PerfIframeId").attr("src", "/UserManager/VmPerformance.aspx?type=f");
                    }
                    else {
                        $("#UnRun").attr("style", "display:none");
                        $("#Run").attr("style", "display:");
                        $("#VmRunS_span").html(Json_list.VmExpirationDate);
                        if (Json_list.VmExpirationDate != "丢失" && Json_list.VmExpirationDate != "关机" && Json_list.VmExpirationDate != "到期") {
                            if (lapage == "1") {
                                $("#PerfIframeId").attr("src", "/UserManager/VmPerformance.aspx?type=t");
                            }

                        }
                        else {
                            $("#PerfIframeId").attr("src", "/UserManager/VmPerformance.aspx?type=f");
                        }
                    }
                    $('#sp_vmDName').text(Json_list.Vm_DisplayName);
                    $('#sp_vmCpu').text(Json_list.VmCpu);
                    $('#sp_vmNc').text(Json_list.VmMemory / 1024.0);
                    $('#sp_vmYp').text(Json_list.VmHdSize);

                }
                $('#IPInfo').val($("#sp_vmIP").text());

                //虚机升级图片显示
                var hidstatus = $("#hidstatus").val();
                if (hidstatus == "0") {
                    $("#upgrad_Img").attr("src", "/Themes/images/tupiana1.png");
                }
                else if (hidstatus == "1") {
                    $("#upgrad_Img").attr("src", "/Themes/images/tupiana2.png");
                }
                else if (hidstatus == "2") {
                    $("#upgrad_Img").attr("src", "/Themes/images/tupiana3.png");
                } else {
                    $("#upgrad_Img").attr("src", "/Themes/images/tupiana.png");
                }

            }
        });
    }

}

//虚机延期
function VmProLongTime() {

    var Time = $("#UseTime").val();
    var Vmid = $("#VmID").val();
    if (Time == "") {
        AlertInfo("消息", "warning", "请选择虚机延期时间");
        return false;
    }
    else {
        $.ajax({
            type: "post",
            url: "/UserManager/UserManager.ashx",
            data: { "op": "VmPLTime", "Time": Time, "Vmid": Vmid },
            success: function (data) {
                if (data == "true") {
                    AlertInfo("消息", "succeed", "延期申请成功，请耐心等待管理员审核！");
                }
                else if (data == "trueT") {
                    AlertInfo("消息", "succeed", "延期成功，请刷新页面查看状态");
                }
                else if (data == "false") {
                    AlertInfo("消息", "warning", "延期失败");
                }
                else if (data == "NoMoney") {
                    AlertInfo("消息", "warning", "账户余额不足，请联系管理员");
                }
                else if (data == "Vm_Upgrade") {
                    AlertInfo("消息", "warning", "此虚机正在升级中！请升级完成后再延期");
                }
                else {
                    AlertInfo("提醒", "warning", "您的申请已提交，请等待管理员审核！");
                }
            }
        });
    }
}
function ClearSession() {
    $.post("/UserManager/UserManager.ashx?op=session");
}
//租户审核虚机续期
function VmPlTen(Time, plid, VmDName, status) {
    alert(status);
    var resTxt = "";
    if (status == "2") {
        jDivShow('UpgradeRes', '拒绝原因', function () {
            resTxt = $('#RefUpgrad').text();
            VmPlTent(Time, plid, VmDName, status, resTxt);
        });

    } else {
        art.dialog({
            title: '虚机延期处理',
            lock: true,
            content: '此操作将给虚机 ' + VmDName + ' 延长使用时间 ' + Time + ' 个月',
            background: '#FFF', // 背景色
            opacity: 0.00,	// 透明度
            icon: 'question',
            ok: function () {
                VmPlTent(Time, plid, VmDName, status, resTxt)
            },
            cancel: true
        });
    }

}

function VmPlTent(Time, plid, VmDName, status, Bec) {
    $.ajax({
        type: "post",
        url: "/Tenant/TenantManger.ashx",
        data: { "op": "pl", "Time": Time, "plid": plid, "status": status, "Bec": Bec },
        success: function (data) {
            if (data == "True") {
                AlertApprove("成功", "succeed", "处理已成功", "/Tenant/WaitMatter.aspx");
            }
            else if (data == "NoMoney") {
                AlertInfo("提示", "warning", "账户余额不足，请充值");
            }
            else {
                AlertInfo("失败", "warning", "延期失败，请重试！");
            }
        }
    });
}


//获取截图
function GetVmImages(vmimagepath, vmname, vmsys, vmrunstatus) {
    //alert("vmimagepath:" + vmimagepath + " vmname:" + vmname + " vmsys:" + vmsys + " vmrunstatus:" + vmrunstatus);

    $("#VmImagesImg").attr("src", "" + vmimagepath + "?" + Math.random() + "&imagename=" + vmname + "&SysName=" + vmsys + "&VmStatus=" + vmrunstatus);

}
//连接远程桌面
function LinkVm(VmIp, system) {

    if (system.indexOf("Linux") > -1 && VmIp != "") {
        BackLoading();
        window.open("../UserManager/RdpControlLx.ashx?server=" + VmIp, "_self");
    }
    else if (VmIp != "") {
        BackLoading();
        window.open("../UserManager/RdpControl.ashx?server=" + VmIp, "_self");
        /*
        var bro = $.browser;
        if (bro.mozilla) {
            window.open("../RdpWeb/default.aspx?server=" + VmIp);
        }
        else {
            window.open("../UserManager/RdpControl.ashx?server=" + VmIp, "_self");

        }
        */
    }

    else
        AlertInfo("警告", "warning", "无法获取虚机ip，远程连接失败！");
}
//返回用户修改的头像
function UserImages(Path) {
    $("#User_Images").attr("src", Path);
}
function ImagesPathy() {
    var p = $("#User_Images").attr("src");
    $("#ximages").attr("src", p);
}
function AlertInfo(title, icon, info) {
    art.dialog({
        title: title,
        lock: true,
        content: info,
        background: '#FFF', // 背景色
        opacity: 0.00,	// 透明度
        icon: icon,
        ok: function () {
            return true
        }
    });
}

function AlertApprove(title, icon, info, url) {
    art.dialog({
        title: title,
        content: info,
        lock: true,
        background: '#FFF', // 背景色
        opacity: 0.00,	// 透明度
        icon: icon,
        ok: function () {
            window.location.href = url;
            return true;
        }
    });
}

function ChangeDateFormat(cellval)//json时间转换为正常时间
{
    try {
        var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        return date.getFullYear() + "-" + month + "-" + currentDate;
    } catch (e) {
        return "";
    }
}