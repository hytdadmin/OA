<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Top.ascx.cs" Inherits="Work_UserControl_Top" %>
<script src="/showDialog/showDialog.js" type="text/javascript"></script>
<link href="/showDialog/showDialog.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .tb
    {
        margin-left: 35px;
        margin-top: 20px;
    }
    .cltr input
    {
        width: 150px;
        height: 22px;
        vertical-align: middle;
        line-height: 21px;
        margin-left: 10px;
    }
    .btnLogin
    {
        width: 156px;
        height: 30px;
    }
    .black_overlay
    {
        display: none;
        position: absolute;
        top: 0%;
        left: 0%;
        width: 100%;
        height: 100%;
        background-color: black;
        z-index: 1001;
        -moz-opacity: 0.8;
        opacity: .80;
        filter: alpha(opacity=80);
    }
    .white_content
    {
        display: none;
        position: absolute;
        top: 30%;
        left: 40%;
        width: 300px;
        height: 200px;
        padding: 16px;
        border: 10px solid #C1E7F3;
        background-color: white;
        z-index: 1002;
        overflow: auto;
    }
</style>
<script type="text/javascript">
    //提交
    function updatePwd() {
        var oldPwd = $("#oldPwd").val();
        var newPwd = $("#newPwd").val();
        var newPwd1 = $("#newPwd1").val();
        if (oldPwd.length != 0 && newPwd.length != 0 && newPwd1.length != 0) {
            if ($.trim(newPwd) == $.trim(newPwd1)) {
                $.post("/work/UserControl/UpdatePwd.ashx", { "oldPwd": oldPwd, "newPwd": newPwd, "newPwd1": newPwd1 }, function (msg) {
                    //0修改成功 1原始密码错误 2修改失败
                    if (msg == 0) {
                        document.getElementById('light').style.display = 'none';
                        document.getElementById('fade').style.display = 'none';
                        alert("修改成功！");
                    } else if (msg == 1) {
                        $("#sp").text("*原始密码输入有误！").css("display", "block");
                    } else {
                        alert("修改失败！");
                    }
                })
            } else {
                $("#sp").text("*两次密码输入不一致！").css("display", "block");
            }

        } else {
            $("#sp").text("*请输入完整信息！").css("display", "block");
        }
    }

    //关闭弹出层
    function closeDiv() {
        document.getElementById("light").style.display = "none";
        document.getElementById('fade').style.display = "none";
        $("#sp").text("").css("display", "none");
        $("#oldPwd").val("");
        $("#newPwd").val("");
        $("#newPwd1").val("");
    }
</script>
<div class="nav">
    <ul>
        <li id="nav7" class="nav_select" style="text-align: right;"><a href="/work/Index.aspx" style="width: 60px; background: url(../images/logo.png) no-repeat;">
            <img style="height: 33px;" src='<%= ResolveUrl("~/Work/images/logo.png")%>' /></a></li>
        <li id="nav6" style="text-align: left; padding-left: 0px; width: 100px;"><a style="width: 88px;" href="/work/Index.aspx">环宇通达</a></li>
        <li id="nav5" style="font-size: 13px; width: 80px;margin-left:50px;"><a href="/work/Index.aspx">首页</a></li>
        <li id="nav4" style="font-size: 13px; width: 80px;"><a href="/Index.aspx">考勤</a></li>
        <%--<li id="nav11" style="font-size: 13px; width: 75px;"><a href="/work/Back/Photos.aspx">图片展</a></li>--%>
        <li id="nav3" style="font-size: 13px; width: 80px;margin-left:30px;"><%=caPath%></li>
        <%--<li id="nav10" style="font-size: 13px; width: 90px;"><a href="http://192.168.10.146:8028/mantisbt/login_page.php" target="_blank">Mantis</a></li>--%>
      <%--  <li id="Li1" style="font-size: 13px; width: 110px;"><a href="../../Posts/index.aspx" target="_self">问题与反馈</a></li>--%>
        <li id="Li2" style="font-size: 13px; width: 110px;margin-left:50px;"><a href="/CallCenter/WorkBillList.aspx<%=sUrl %>">Ca呼叫中心</a></li>
         <li id="nav2" style="width: 90px; font-size: 13px;margin-left:80px;">欢迎，<span class="nav_loginname"><asp:Label runat="server" ID="lblName"></asp:Label></span></li>
       <%-- <li style="width: 40px;"><a style="width: 40px;font-size: 13px;" target="_blank" href="https://mail.hyitech.com/owa">邮箱</a></li>--%>
        <li id="nav8" style="width: 60px;"><a style="width: 60px;font-size: 13px; cursor: pointer;" href="javascript:void(0)" onclick="document.getElementById('light').style.display='block';document.getElementById('fade').style.display='block'">修改密码</a></li>
        <li runat="server" visible="false" id="nav9" style="width: 30px;font-size: 13px;"><a style="font-size: 13px; width: 40px;" target="_blank" href='<%= ResolveUrl("~/Work/Back/BulletinList.aspx")%>'>后台</a></li>
        <%--<li id="nav1"><a style="font-size:13px;width: 60px;" href="javascript:exit();">退出</a></li>--%>
           
        <li id="nav1" style="width: 30px;"><a style="font-size: 13px; width: 40px;" href="javascript:exit();">退出</a></li>
    </ul>
</div>
<div id="light" class="white_content">
    <a href="javascript:void(0)" style="margin-left: 265px;" onclick="closeDiv()">关闭</a>
    <table class="tb">
        <tr class="cltr">
            <td>
                <label>
                    旧 密 码</label>
            </td>
            <td>
                <input type="password" id="oldPwd" />
            </td>
        </tr>
        <tr class="cltr">
            <td>
                <label>
                    新 密 码</label>
            </td>
            <td>
                <input type="password" id="newPwd" />
            </td>
        </tr>
        <tr class="cltr">
            <td>
                <label>
                    确认密码</label>
            </td>
            <td>
                <input type="password" id="newPwd1" />
            </td>
        </tr>
        <tr style="height: 20px;">
            <td colspan="3" align="right">
                <span id="sp" style="font-size: 15px; color: Red; display: none;">dsdasdasdsa</span>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <input type="button" id="btnSend" value="提 交" class="btnLogin" onclick="updatePwd()" />
            </td>
        </tr>
    </table>
</div>
<div id="fade" class="black_overlay">
</div>
<p id="back-to-top">
    <a href="#top"><span></span>回到顶部</a></p>
