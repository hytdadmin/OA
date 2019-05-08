<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登录</title>
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <style type="text/css">
        .cl1 input
        {
            width: 155px;
            height: 25px;
            line-height: 25px;
            vertical-align: middle;
        }
        .head img
        {
            width: 270px;
            height: 65px;
            margin: 12px 0 10px 110px;
        }
        .body
        {

            background:url(Image/Login/back1.png) repeat-x top;
            height: 392px;
            width:1280px;
            margin: 15px auto;
        }
        .bdLogin
        {
            background-image: url(Image/Login/withe.png);
            height: 272px;
            width: 359px;
            position:absolute;
            margin-top:70px;
            right:160px;
             float:right;
            z-index:50;
        }
        .body img
        {
            margin: 30px 50px 0 0;
        }
        *
        {
            margin: 0px;
            padding: 0px;
        }
        .dvUser
        {
            margin: 10px 0 0 25px;
        }
        .dvPwd
        {
            margin: 16px 0 0 25px;
        }
        .dvUser input
        {
            margin-left: 10px;
            padding-left:8px;
            width: 285px;
            height: 36px;
            vertical-align: middle;
            line-height: 36px;
            font-size: 12px;
            font-weight: 300;
            color: #8C8C8C;
        }
        .dvPwd input
        {
            margin-left: 10px;
            padding-left:8px;
            width: 285px;
            height: 36px;
            vertical-align: middle;
            line-height: 36px;
            font-size: 12px;
            font-weight: 300;
            color: #8C8C8C;
        }
        .dvUser label
        {
            font-size: 20px;
            font-weight: 500;
            color: #58595B;
            font-family: 微软雅黑;
        }
        .dvPwd label
        {
            font-size: 20px;
            font-weight: 500;
            color: #58595B;
            font-family: 微软雅黑;
            margin: 16px 0 0 0;
        }
        .dvbtn img
        {
            margin: 16px 0 0 37px;
            width: 160px;
            height: 42px;
        }
        body
        {
            background-color: #FCFCFC;
        }
        .foot
        {
            margin:0 auto;
            text-align:center;
            margin-top:50px;
        }
        .dvlab label
        {
            font-size: 12px;
            font-weight: 400;
            color: #8C8C8C;
            font-family: 微软雅黑;
            margin: 0 0 0 37px;
        }
        .end p
        {
            font-size: 14px;
            font-weight: 400;
            color: #8C8C8C;
            font-family: 楷体;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            loginFoucns();
            $(document).keydown(function (e) {
                if (e.keyCode == 13) {
                    ImgClick();
                }
            });
        })

        function ImgClick() {
            var user = $.trim($("#user").val());
            var pwd = $.trim($("#password").val());
//            var user = '81';
//            var pwd = '123.com';
            if (user.length != 0 && pwd.length == 0 && user != "输入个人员工号") {
                $("#spRemark").text("请输入个人密码！").css("display", "block");
                $("#pwd").focus();
                
            }
            if ((user.length == 0 || user == "输入个人员工号") && pwd.length != 0) {
                $("#spRemark").text("请输入个人员工号！").css("display", "block");
                $("#user").focus();
            }
            if ((user.length == 0||user == "输入个人员工号") && pwd.length == 0 ) {
                $("#spRemark").text("请输入完整信息！").css("display", "block");
                $("#user").focus();
            }
            if (user.length != 0 && pwd.length != 0 && user!= "输入个人员工号") {
                $.post("Login.ashx?" + Math.random(), { "user": user, "pwd": pwd }, function (msg) {
                    if (msg == 0) {
                        location.href = "work/index.aspx?" + Math.random();
                    } else {
                        $("#spRemark").text(msg).css("display", "block");
                        $("#pwd").focus();
                        $("#user").css("border", "1px solid #229BD7");
                    } 
                });
            } 
        }
        function loginFoucns() {
            $("#user").focus(function () {
                var user = $.trim($("#user").val());
                if (user == "输入个人员工号") {
                    $("#user").val("");
                }
                $(this).css("border", "1px solid #229BD7").css("color", "#383838");
            }).blur(function () {
                var user = $.trim($("#user").val());
                if (user.length == 0) {
                    $("#user").val("输入个人员工号");
                    $(this).css("border-color", "#A4C9E3").css("color", "#8C8C8C");
                } else {
                    $(this).css("border-color", "#A4C9E3");
                }
            });
            $("#pwd").focus(function () {
                var pwd = $.trim($("#pwd").val());
                $(this).css("border", "1px solid #0A8CD2").css("color", "#383838").css("display", "none");
                $("#password").css("display", "block").css("border", "1px solid #0A8CD2").css("color", "#383838").focus();
            })
            $("#password").blur(function () {
                var password = $.trim($("#password").val());
                if (password.length == 0) {
                    $("#pwd").val("输入个人密码");
                    $(this).css("display", "none");
                    $("#pwd").css("display", "block").css("border", "1px solid #0A8CD2").css("border-color", "#A4C9E3").css("color", "#8C8C8C");
                } else {
                    $(this).css("border-color", "#A4C9E3");
                }
            }).focus(function () {
                $(this).css("border-color", "#0A8CD2");
            });
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="head">
        <img src="Image/Login/logo.png" />
    </div>
    <div class="bdLogin">
            <div style="margin: 18px 0 0 35px; height: 18px;">
                <span style="font-size: 13px; font-weight: 400; color: #DD0000; font-family: 微软雅黑;
                    display: none;" id="spRemark">您还没有输入密码！</span></div>
            <div class="dvUser">
                <%--            <label>用户名</label>--%>
                <input type="text" name="user" value="输入个人员工号" id="user" /></div>
            <div class="dvPwd">
                <%--<label>密 &nbsp 码</label>--%>
                <input type="text" name="pwd" value="输入个人密码" id="pwd" />
                <input type="password" name="password" value="" id="password" style="display: none" /></div>
            <div class="dvbtn">
                <img src="Image/Login/btnlogin1.png" id="img" onclick="ImgClick()" style="cursor: pointer;" /></div>
            <div class="dvlab" style="margin: 17px 0 0 0;">
                <label>
                    *用户名为公司员工号</label><br />
                <!--<label>
                    *登录初始密码为(123456或123.com)</label>-->
            </div>
        </div>
    <div style="width:100%; margin-top:15px">
    <img  src="Image/Login/back3.jpg" style=" width:100%; height:390px;  border:0px; z-index:-1" id="bdimg"/>
        
    </div>
    
    <div class="foot">
        <div style="width: 350px;display:inline-block">
            <img src="Image/Login/attemd.png" style="float: left" />
            <div style="float: left; margin: 0 0 0 10px;">
                <div style="font-size: 20px; font-weight: 700; color: #606060; font-family: 微软雅黑;text-align:left">
                    考勤</div>
                <div style="font-size: 15px; font-weight: 400; color: #8C8C8C; font-family: 微软雅黑;">
                    个人考勤\备注说明\他人考勤</div>
            </div>
        </div>
        <div style="width: 350px; display:inline-block">
            <img src="Image/Login/log.png" style="float: left" />
            <div style="float: left; margin: 0 0 0 10px;">
                <div style="font-size: 20px; font-weight: 700; color: #606060; font-family: 微软雅黑;text-align:left">
                    日志</div>
                <div style="font-size: 15px; font-weight: 400; color: #8C8C8C; font-family: 微软雅黑;">
                    工作日志\发表说说\通知公告</div>
            </div>
        </div>
        <div style="width: 350px;display:inline-block">
            <img src="Image/Login/ca.png" style="float: left" />
            <div style="float: left; margin: 0 0 0 10px;">
                <div style="font-size: 20px; font-weight: 700; color: #606060; font-family: 微软雅黑;text-align:left">
                    CA平台</div>
                <div style="font-size: 15px; font-weight: 400; color: #8C8C8C; font-family: 微软雅黑;">
                    登录平台\查看平台\测试平台</div>
            </div>
        </div>
    </div>
    <div class="end" style="margin:0 auto;margin-top:100px; width:700px;">
        <p>
            客户服务热线：010-62364559  电子邮箱：hytd@hyitech.com  网络实名：环宇通达</p>
        <p style="margin-left: 85px;margin-top:5px;" >
            版权所有 环宇通达 京公网安备11010802009408号
        </p>
    </div>
    </form>
</body>
</html>
