<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyColleague2.aspx.cs" Inherits="MyColleague2" %>

<%@ Register Src="UserControl/Top.ascx" TagName="Top" TagPrefix="uc2" %>
<%@ Register Src="UserControl/NavigateUC.ascx" TagName="NavigateUC" TagPrefix="uc5" %>
<%@ Register Src="UserControl/PublishSay.ascx" TagName="PublishSay" TagPrefix="uc6" %>
<%@ Register Src="UserControl/Bottom.ascx" TagName="Bottom" TagPrefix="uc7" %>
<%@ Register src="UserControl/PublishSayAlert.ascx" tagname="PublishSayAlert" tagprefix="uc1" %>
<%@ Register src="UserControl/ChangeHeadImg.ascx" tagname="ChangeHeadImg" tagprefix="uc3" %>
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
    <title>我的同事</title>
    <script type="text/javascript">

        $(document).ready(function () {
            getUserUC();
            getBulletinUC();
            getSayUC();
            InitData(0, 0);
            //定时刷新
            var ref = "";
            ref = setInterval(function () {
                getBulletinUC();
                getSayUC();
            }, 1000*60);
        });
        //分页
        function InitData(pageindx, depid) {
            var keyValue = $("#txtKeyVal").val();
            var tbody = "";
            $.ajax({
                type: "POST", //用POST方式传输
                url: 'UserControl/MyColleage.ashx', //目标地址
                data: "p=" + (pageindx + 1) + "&keyValue=" + keyValue + "&depid=" + depid,
                beforeSend: function () { $("#divload").show(); $("#Pagination").hide(); }, //发送数据之前
                complete: function () { $("#divload").hide(); $("#Pagination").show(); }, //接收数据完毕
                success: function (result) {
                    if (result == "sessionError") {
                        ashxVerifySession();
                    } else if (result != "parError") {
                        $("#div_mycolleague").html(result);
                    }
                    //如果没有数据
                    if ($("#pagecount").val() > 0) {

                        $("#Pagination").pagination($("#pagecount").val(), {
                            callback: pageselectCallback,
                            prev_text: '<',
                            next_text: '>',
                            items_per_page: $("#pageSize").val(),
                            num_display_entries: 6,
                            current_page: pageindx,
                            num_edge_entries: 2
                        });
                    }
                }
            });
        }
    </script>
    <style type="text/css">
        .dep
        {
            color: #0A8CD2;
            margin-left: 5px;
        }
    </style>
</head>
<body>
    <uc3:ChangeHeadImg ID="ChangeHeadImg1" runat="server" />
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                <uc5:NavigateUC ID="NavigateUC1" runat="server" Defaultvalue="Me" />
                <div class="right_nav_search">
                    <input class="right_nav_search_txt" id="txtKeyVal" type="text" value="输入关键字..." onblur="onBlur(this.id,'输入关键字...')" onfocus="onFocus(this.id,'输入关键字...')" />
                    <a href="javascript:InitData(0);">
                        <img src="Images/nav_search.jpg" /></a>
                </div>
            </div>
            <div class="clear">
            </div>
            <!--右侧内容-->
            <div class="right_content">
                <div class="right_content_title">
                    全部日志<asp:Label runat="server" ID="lblCount"></asp:Label>条</div>
                全部丨研发部丨销售部丨<a onclick="InitData(0,2)">技术部</a>丨市场部丨财务部
                <div class="right_conten_mr" id="div_mycolleague">
                </div>
                <div class="pager">
                </div>
                <!-- 分页 -->
                <div id="Pagination" class="digg">
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
