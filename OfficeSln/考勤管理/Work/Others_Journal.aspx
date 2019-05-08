<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Others_Journal.aspx.cs" Inherits="Work_Others_Journal" %>

<%@ Register Src="UserControl/Top.ascx" TagName="Top" TagPrefix="uc2" %>
<%@ Register Src="UserControl/NavigateUC.ascx" TagName="NavigateUC" TagPrefix="uc5" %>
<%@ Register Src="UserControl/PublishSay.ascx" TagName="PublishSay" TagPrefix="uc6" %>
<%@ Register Src="UserControl/Bottom.ascx" TagName="Bottom" TagPrefix="uc7" %>
<%@ Register Src="UserControl/PublishSayAlert.ascx" TagName="PublishSayAlert" TagPrefix="uc1" %>
<%@ Register Src="UserControl/ChangeHeadImg.ascx" TagName="ChangeHeadImg" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta name="author" content="环宇通达 +86-532-86100168">
    <meta name="description">
    <meta name="keywords">
    <link href="style/indexcss.css" rel="stylesheet" type="text/css">
    <script src="scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jqueryPager/jquery-1.2.6.pack.js" type="text/javascript"></script>
    <link href="Scripts/jqueryPager/pagination.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jqueryPager/jquery.pagination.js" type="text/javascript"></script>
    <script src="scripts/pub.js" type="text/javascript"></script>
    <link href="Style/pager.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="Scripts/Others_Journal.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <title>其他人日志</title>
    <script type="text/javascript">
        $(document).ready(function () {
            InitDataOl(0);
            getUserUC();
            getBulletinUC();
            getSayUC();
            kuaisutongdao();
            //定时刷新
            var ref = "";
            ref = setInterval(function () {
                getBulletinUC();
                getSayUC();
            }, 1000 * 60);
        });
    </script>
</head>
<body>
    <uc3:ChangeHeadImg ID="ChangeHeadImg1" runat="server" />
    <form id="form1" runat="server">
    <div id="divload" style="top: 70%; right: 30%; position: absolute; padding: 0px;
        margin: 0px; z-index: 999">
        <img src="Scripts/jqueryPager/spinner3-greenie.gif" />
    </div>
    <uc2:Top ID="Top1" runat="server" />
    <div class="W_main">
        <div class="W_main_left">
            <!--用户信息开始-->
            <div class="div_user" id="div_user">
            </div>
            <!--用户信息结束-->
            <div class="split">
            </div>
            <!--快速通道开始-->
            <div class="dv_tongdao" id="dv_tongdao">
            </div>
            <div class="split">
            </div>
            <!--公告列表开始-->
            <div class="div_Bulletins" id="div_Bulletins">
            </div>
            <!--公告列表结束-->
            <div class="split">
            </div>
            <!--大家正在说开始-->
            <div class="divleft_say" id="divleft_say">
            </div>
            <!--大家正在说结束-->
        </div>
        <!--右侧信息开始-->
        <div class="W_main_right">
            <!--发布说说-->
            <div class="divSay">
                <uc6:PublishSay ID="PublishSay1" runat="server" />
            </div>
            <!--右侧中间导航-->
            <div class="right_nav">
                <uc5:NavigateUC ID="NavigateUC1" runat="server" Defaultvalue="Others" />
                <div class="right_nav_search">
                    <input class="right_nav_search_txt" id="txtKeyVal" type="text" value="输入关键字..." onblur="onBlur(this.id,'输入关键字...')"
                        onfocus="onFocus(this.id,'输入关键字...')" />
                    <a href="javascript:InitData(0);">
                        <img src="Images/nav_search.jpg" /></a>
                </div>
            </div>
            <div class="clear">
            </div>
            <!--右侧内容-->
            <div class="right_content">
                <div id="right_content">
                </div>
                <div class="pager">
                    <!-- 分页 -->
                    <div id="Pagination" class="digg">
                    </div>
                </div>
            </div>
            <!--右侧信息结束-->
        </div>
    </div>
    <div class="foot">
        <uc7:Bottom ID="Bottom1" runat="server" />
    </div>
    <uc1:PublishSayAlert ID="PublishSayAlert1" runat="server" />
    </form>
</body>
</html>
