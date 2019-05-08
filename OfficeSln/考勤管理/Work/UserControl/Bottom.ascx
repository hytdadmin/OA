<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Bottom.ascx.cs" Inherits="Work_UserControl_Bottom" %>
<script type="text/javascript">
        $(document).ready(function () {
            addVisit();
            showVisit();
            //定时刷新
            var refBottom = "";
            refBottom = setInterval(function () {
                addVisit();
                showVisit();
            }, 1000 * 60 * 5);
        });
</script>
  <%--<p>Copyright © 1996-2008 hyitech.com Corporation, All Rights Reserved</p>
  <p>客户服务热线：010-62364559 电子邮箱：hytd@hyitech.com 网络实名：环宇通达</p>
  <p>版权所有 环宇通达 京公网安备11010802009408号 </p>--%>
  <br />
  <p>环宇通达内网系统&nbsp;&nbsp;<asp:Literal runat="server" ID="litMes"></asp:Literal>&nbsp;&nbsp;在线人数：<span id="spanShowVisit"></span></p>