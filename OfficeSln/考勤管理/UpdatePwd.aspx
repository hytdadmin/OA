<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePwd.aspx.cs" Inherits="UpdatePwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改密码</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(".contant_form input").focus(function () {
                $(this).css("border", "1px solid #FFB334");
            }).blur(function () {
                $(this).css("border", "");
            })
            checkOldPwd();
            checkNewPwd();
            checkPwd();
        })
        //原始密码
        function checkOldPwd() {
            $("#oldPwd").blur(function () {
                if ($.trim($("#oldPwd").val()) == "") {
                    $("#spoldPwd").text("*原始密码不能为空！").css("color", "red");
                } else {
                    if ($.trim($("#oldPwd").val()).length >= 6) {
                        $("#spoldPwd").text("");
                    } else { $("#spoldPwd").text("*密码长度不能少于6位！").css("color", "red"); }
                }
            })
        }
        //新密码
        function checkNewPwd() {
            $("#newPwd").blur(function () {
                if ($.trim($("#newPwd").val()) == "") {
                    $("#spnewPwd").text("*新密码不能为空！").css("color", "red");
                } else {
                    if ($.trim($("#newPwd").val()).length >= 6) {
                        $("#spnewPwd").text("");
                    } else { $("#spnewPwd").text("*新密码长度不能少于6位！").css("color", "red"); }
                }
            })
        }
        //确认密码
        function checkPwd() {
            $("#pwd").blur(function () {
                if ($.trim($("#pwd").val()) == "") {
                    $("#sppwd").text("*原始密码不能为空！").css("color", "red");
                } else {
                    if ($.trim($("#pwd").val()).length >= 6) {
                        var password = $.trim($("#newPwd").val());
                        if ($.trim($("#pwd").val()) == password)
                            $("#sppwd").text("");
                        else
                            $("#sppwd").text("*两次密码输入不一致！").css("color", "red");
                    } else { $("#sppwd").text("*密码长度不能少于6位！").css("color", "red"); }
                }
            })
        }
        function btnSave() {
            var oldPwd = $.trim($("#oldPwd").val());
            var newPwd = $.trim($("#newPwd").val());
            var pwd = $.trim($("#pwd").val());
            //  var UserCode = $("#code").text();
            if (oldPwd.length > 0 && newPwd.length > 0 && pwd.length > 0) {
                if (pwd == newPwd) {
                    $.post("UpdatePwd.ashx?" + Math.random(), { "oldPwd": oldPwd, "newPwd": newPwd, "pwd": pwd }, function (msg) {
                        // //0 修改成功 1 旧密码错误 2修改出错
                        if (msg == 0) {
                            alert("修改成功！");
                            location.href = "work/index.aspx?" + Math.random();
                        } else if (msg == 1) {
                            $("#spoldPwd").text("*原始密码错误！").css("color", "red");
                        } else if (msg = 2) {
                            $("#sp").text("*修改失败！").css("color", "red");
                        } else {
                            return false;
                        }
                    });
                } else {
                    $("#sppwd").text("*两次密码输入不一致！").css("color", "red");
                }
            } else {
                $("#spoldPwd").text("*请输入完整信息！").css("color", "red");
                $("#spnewPwd").text("*请输入完整信息！").css("color", "red");
                $("#sppwd").text("*请输入完整信息！").css("color", "red");
            }
        }
        //取消
        function btnCancel() {
            location.href = "work/index.aspx";
        }
    </script>
</head>
<body style="background-color: #97c8e8;">
    <!---register start--->
    <div class="wrapper">
        <div class="top_head">
            <dl class="top_head_t">
                <dt>&nbsp;</dt>
                <dd>
                    <img src="img/r91.jpg" /></dd>
                <div class="clear">
                </div>
            </dl>
        </div>
        <div class="contant">
            <h2>
                <p class="c_left">
                    填写信息</p>
                <p class="c_right">
                </p>
                <div class="clear">
                </div>
            </h2>
            <div class="contant_form">
                <table width="734" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="87" height="48" align="right" valign="middle">
                            原始密码：
                        </td>
                        <td colspan="3">
                            <input type="password" value="" id="oldPwd" maxlength="20" />
                        </td>
                        <td width="389">
                            <span class="gray" id="spoldPwd">6-20位数字，字符或字母组合</span>
                        </td>
                    </tr>
                    <tr>
                        <td height="48" align="right" valign="middle">
                            新密码：
                        </td>
                        <td colspan="3">
                            <input type="password" value="" id="newPwd" maxlength="20" />
                        </td>
                        <td>
                            <span class="gray" id="spnewPwd">6-20位数字，字符或字母组合</span>
                        </td>
                    </tr>
                    <tr>
                        <td height="48" align="right" valign="middle">
                            确认新密码：
                        </td>
                        <td colspan="3">
                            <input type="password" value="" id="pwd" maxlength="20" />
                        </td>
                        <td>
                            <span class="gray" id="sppwd">6-20位数字，字符或字母组合</span>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="contant_submit">
                <a href="javascript:btnSave()">
                    <img src="img/r7.jpg" style="margin-left: 220px;" /></a> 
                    <a href="javascript:btnCancel()">
                        <img src="img/r71.jpg" style="margin-left: 10px;" /></a>
            </div>
        <div class="clear">
        </div>
    </div>
    <div class="footer">
        Copyright © 1996-2008 hyitech.com Corporation, All Rights Reserved<br />
        客户服务热线：010-62364559 电子邮箱：hytd@hyitech.com 网络实名：环宇通达<br />
        版权所有 环宇通达 京公网安备110108007208号
    </div>
    <div class="clear">
    </div>
    </div>
    <!---end----->
</body>
</html>
