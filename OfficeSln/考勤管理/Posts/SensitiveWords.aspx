<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SensitiveWords.aspx.cs" Inherits="Posts_SensitiveWords" %>

<%@ Register Src="~/Work/UserControl/Top.ascx" TagName="Top" TagPrefix="uc2" %>
<%@ Register Src="~/Posts/UserControl/CMleft.ascx" TagName="CMleft" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>意见与反馈 - 敏感词管理</title>
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_common.css" />
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_forum_forumdisplay.css" />
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_widthauto.css" />
    <link href="/Posts/style/reset.css" rel="stylesheet" type="text/css" />
    <link href="/Posts/style/selectForK13.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/Posts/style/indexcss.css" />
    <script type="text/javascript" src="/Posts/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/Posts/js/common.js"></script>
    <script src="/Posts/JS/pub.js" type="text/javascript"></script>
    <style>
        #ulWords
        {
            float: left;
        }
        #ulWords li
        {
            float: left;
            margin-top: 3px;
            text-decoration: none;
            list-style: none;
            width: 200px;
            height: 22px;
            vertical-align: middle;
        }
        #ulWords li a
        {
            color: Red;
            text-decoration: none;
            font-size: 12px;
            padding-bottom: 5px;
        }
        #ulWords li a sup
        {
            width: 20px;
            margin-left: 3px;
        }
    </style>
    <script type="text/javascript">
        $().ready(function () {
            $("#scbar_btn").click(function () {
                Search($.trim($("#scbar_txt").val()));
            });
            $("#submit").click(function () {
                if ($.trim($("#keywords").val()).length > 0) {
                    $.get("/Posts/Ashx/sensitivewords.ashx", { "option": "add", "kw": $.trim($("#keywords").val()) }, function (data) {
                        if (data != "0") {
                            $("#keywords").val("");
                            $(data).appendTo($("#ulWords"));
                        }
                        else
                            alert("失败");
                    });
                }
                else {
                    $("#error").html("请输入敏感词！");
                }
            });
            $("#keywords").focus(function () {
                $("#error").html("");
            });

            $("#ulWords li a").live("click", function () {
                if (window.confirm('你确认删除吗?')) {
                    var obj = $(this).parent();
                    $.get("/Posts/Ashx/sensitivewords.ashx", { "option": "del", "id": $(this).attr("id") },
                    function (data) {
                        if (data == "1") {
                            alert("删除成功");
                            obj.remove();
                        }
                        else
                            alert("失败");
                    });
                }

            });
        });
        function getinfopost() { };
    </script>
</head>
<body id="nv_forum" class="pg_forumdisplay">
    <form id="form1" runat="server">
    <div id="append_parent">
        <div id="typeid_fast_ctrl_menu" class="sltm" style="display: none; width: 80px;">
            <ul>
                <li class="current">选择主题分类</li></ul>
        </div>
    </div>
    <div id="ajaxwaitid">
    </div>
    <div class="body_bg">
         <uc2:Top ID="Top1" runat="server" />
        <div class="mu_nv">
        </div>
        <div class="c_y">
            <div class="c_yt" style="display: none">
                <div class="c_yt2">
                    <div class="c_r">
                        <div class="c_l">
                            <div class="scb_sty">
                                <div class="l">
                                    <div class="r">
                                        <div class="cl" id="scbar">
                                            <table cellspacing="0" cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td class="scbar_icon_td">
                                                        </td>
                                                        <td class="scbar_txt_td">
                                                            <input type="text" speech="" x-webkit-speech="" autocomplete="off" value="请输入搜索内容" id="scbar_txt" name="srchtxt" class=" xg1" onblur="onBlur(this.id,'请输入搜索内容')" onfocus="onFocus(this.id,'请输入搜索内容')">
                                                        </td>
                                                        <td class="scbar_type_td">
                                                        </td>
                                                        <td class="scbar_btn_td">
                                                            <button value="true" class="pn pnc" sc="1" id="scbar_btn" name="searchsubmit" type="submit" mid="">
                                                                <strong class="xi2 xs2">搜索</strong></button>
                                                        </td>
                                                        <td class="scbar_hot_td">
                                                            <div id="scbar_hot">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="c_yc">
                <div class="c_ycr">
                    <div class="c_yeeib">
                        <div id="wp" class="wp">
                            <style type="text/css">
                                .tl cite
                                {
                                    padding-top: 6px;
                                }
                                .tl .by
                                {
                                    width: 125px;
                                }
                                .pstyle img
                                {
                                    width: 25px;
                                    height: 25px;
                                    float: left;
                                    border: 1px solid #ccc;
                                    padding: 2px;
                                    margin: 0px;
                                    background-color: #fff;
                                    margin: 3px 3px 0 0;
                                }
                                .plstyle img
                                {
                                    width: 35px;
                                    height: 35px;
                                    float: left;
                                    border: 1px solid #ccc;
                                    padding: 2px;
                                    margin: 0px;
                                    background-color: #fff;
                                    margin: 3px 3px 0 0;
                                }
                            </style>
                            <div id="diynavtop" class="area">
                            </div>
                            <div class="wp">
                                <div id="diy1" class="area">
                                </div>
                            </div>
                            <div class="boardnav">
                                <div id="ct" class="wp cl" style="margin-left: 145px">
                                    <uc1:CMleft ID="CMleft1" runat="server" />
                                    <div class="mn">
                                        <div id="pt" class="bm cl">
                                            <div class="z">
                                                <a href="/posts/index.aspx" class="nvhm" title="首页">意见与反馈</a><em>&#187;</em><a href="#">敏感词管理</a>
                                            </div>
                                        </div>
                                        <div id="pgt" class="bm bw0 pgs cl" style="height: 23px;">
                                            <span id="fd_page_top"></span>&nbsp;
                                            <input type="text" id="keywords" size="30px" maxlength="20" name="keywords" style="height: 22px; margin-right: 6px;" /><input id="submit" type="button" value="添加" style="width: 30px; height: 20px;" /><span id="error" style="color: Red"></span></div>
                                        <div style="clear: both">
                                        </div>
                                        <div id="threadlist" class="tl bm bmw" style="height: inherit">
                                            <ul id="ulWords">
                                                <%=strWords %>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="wp mtn">
                                <div id="diy3" class="area">
                                </div>
                            </div>
                            <div class="fg">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="c_yb" style="display: none">
                <div class="c_yb2">
                    <div class="c_r">
                        <div class="c_l">
                        </div>
                    </div>
                </div>
            </div>
            <div style="height: 30px; background-color: #FFFFFF; width: 970px; margin-left: auto; margin-right: auto">
            </div>
            <div class="bottomcssNew">
                <div class="footer">
                    环宇通达 版权所有 2012 咨询电话：57624000
                </div>
            </div>
        </div>
    </div>
    <span id="scrolltop" style="left: auto; right: 0px; visibility: hidden;">回顶部</span>
    <div id="discuz_tips" style="display: none;">
    </div>
    </form>
</body>
</html>
