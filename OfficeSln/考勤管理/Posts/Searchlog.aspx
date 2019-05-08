<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Searchlog.aspx.cs" Inherits="Posts_test" %>

<%@ Register Src="~/Work/UserControl/Top.ascx" TagName="Top" TagPrefix="uc2" %>
<%@ Register Src="~/Posts/UserControl/CMleft.ascx" TagName="CMleft" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>意见与反馈 - 日志查询</title>
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_common.css" />
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_forum_forumdisplay.css" />
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_widthauto.css" />
    <link rel="stylesheet" href="/Posts/style/indexcss.css" />
    <link href="/CallCenter/autocomplete/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Posts/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/Posts/js/common.js"></script>
    <script src="/Posts/JS/pub.js" type="text/javascript"></script>
    <link href="/Posts/style/reset.css" rel="stylesheet" type="text/css" />
    <link href="/Posts/style/selectForK13.css" rel="stylesheet" type="text/css" />
    <script src="My97DatePicker/WdatePicker.js" type="text/javascript"></script>
        <script src="/CallCenter/autocomplete/jquery.autocomplete.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("input[name=niming]").each(function () {
                if ('<%=niming %>' == $(this).attr("value"))
                    $(this).attr("checked", "checked");
            });
            $("input[name=fankui]").each(function () {
                if ('<%=fankui %>' == $(this).attr("value"))
                    $(this).attr("checked", "checked");
            });
            $("#scbar_btn").click(function () {
                Search($.trim($("#scbar_txt").val()));
            });
            $(this).keydown(function (e) {
                var key = window.event ? e.keyCode : e.which;
                if (key.toString() == "13") {
                    return false;
                }
            });
        });
        function getinfopost()
        { }
                  var Users = <%=strUsers %>;
        $(function () {
            $('#sendUser').autocomplete(Users, {
                //               extraParams: { keyword: function () { return $('#txtUserName').val(); } }, 
                //                                dataType: "json", 
                max: 12,    //列表里的条目数
                minChars: 0,    //自动完成激活之前填入的最小字符
                width: 400,     //提示的宽度，溢出隐藏
                scrollHeight: 300,   //提示的高度，溢出显示滚动条
                matchContains: true,    //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                autoFill: false,    //自动填充
                formatItem: function (row, i, max) {
                    //                   return i + '/' + max + ':"' + row.name + '"[' + row.to + ']';
                    return i + '/' + max + ':"' + row.name;
                },
                formatMatch: function (row, i, max) {
                    return row.name;
                },
                formatResult: function (row) {
                    return row.name;
                }
            }).result(function (event, row, formatted) {
                row = eval(row);
                //                $('#txtCustomerName').val(row[0].name);
                //                alert(row[0].to);
            });
            $('#backUser').autocomplete(Users, {
                //               extraParams: { keyword: function () { return $('#txtUserName').val(); } },
                //                                dataType: "json", 
                max: 12,    //列表里的条目数
                minChars: 0,    //自动完成激活之前填入的最小字符
                width: 400,     //提示的宽度，溢出隐藏
                scrollHeight: 300,   //提示的高度，溢出显示滚动条
                matchContains: true,    //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                autoFill: false,    //自动填充
                formatItem: function (row, i, max) {
                    //                   return i + '/' + max + ':"' + row.name + '"[' + row.to + ']';
                    return i + '/' + max + ':"' + row.name;
                },
                formatMatch: function (row, i, max) {
                    return row.name;
                },
                formatResult: function (row) {
                    return row.name;
                }
            }).result(function (event, row, formatted) {
                row = eval(row);
                //                $('#txtCustomerName').val(row[0].name);
                //                alert(row[0].to);
            });
        });
    </script>
</head>
<body id="nv_forum" class="pg_forumdisplay">
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
                                                            <input type="text" speech="" x-webkit-speech="" autocomplete="off" value="请输入搜索内容"
                                                                id="scbar_txt" name="srchtxt" class=" xg1" onblur="onBlur(this.id,'请输入搜索内容')"
                                                                onfocus="onFocus(this.id,'请输入搜索内容')" />
                                                        </td>
                                                        <td class="scbar_type_td">
                                                        </td>
                                                        <td class="scbar_btn_td">
                                                            <button value="true" class="pn pnc" sc="1" id="scbar_btn" name="searchsubmit" type="button"
                                                                mid="">
                                                                <strong class="xi2 xs2">搜索</strong></button>
                                                            <input type="hidden" value="" id="hdnNM" />
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
                            <a href="javascript:;" class="wmff_wxyunnav"></a>
                            <div id="diynavtop" class="area">
                            </div>
                            <div class="wp">
                                <div id="diy1" class="area">
                                </div>
                            </div>
                            <div class="boardnav" style="margin-left: 145px">
                                <div id="ct" class="wp cl">
                                    <uc1:CMleft ID="CMleft1" runat="server" />
                                    <form id="form1" method="get">
                                    <div class="mn">
                                        <div id="pt" class="bm cl">
                                            <div class="z">
                                                <a href="/posts/index.aspx" class="nvhm" title="首页">意见与反馈</a><em>&#187;</em><a href="#">日志查询</a></div>
                                        </div>
                                        <div class="drag">
                                            <div id="diy4" class="area">
                                                <input type="hidden" name="typecid" value="1110" />
                                                <table class="tblog">
                                                    <tr>
                                                        <td class="tblogtd">
                                                            类别:
                                                        </td>
                                                        <td colspan="3">
                                                            <select name="select">
                                                                <%=GetCategory(Convert.ToInt32(type))%>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tblogtd">
                                                            主题:
                                                        </td>
                                                        <td class="tblogtd_c">
                                                            <input type="text" name="title" value="<%=txt %>" />
                                                        </td>
                                                        <td class="tblogtd">
                                                            内容:
                                                        </td>
                                                        <td class="tblogtd_c">
                                                            <input type="text" name="remark" value="<%=remark %>" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tblogtd">
                                                            IP:
                                                        </td>
                                                        <td class="tblogtd_c">
                                                            <input type="text" name="ip" value="<%=ip %>" />
                                                        </td>
                                                        <td class="tblogtd">
                                                            时间:
                                                        </td>
                                                        <td align="left" class="tblogtd_c">
                                                            <input class="Wdate" type="text" onclick="WdatePicker()" name="time" value="<%=time %>">
                                                            -
                                                            <input class="Wdate" type="text" onclick="WdatePicker()" name="endtime" value="<%=endtime %>">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tblogtd">
                                                            发问人:
                                                        </td>
                                                        <td class="tblogtd_c">
                                                            <input type="text" name="sendUser" id="sendUser" value="<%=sendUser %>" />
                                                        </td>
                                                        <td class="tblogtd">
                                                            跟帖/反馈人:
                                                        </td>
                                                        <td class="tblogtd_c">
                                                            <input type="text" name="backUser" id="backUser" value="<%=backUser %>" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tblogtd">
                                                            是否匿名:
                                                        </td>
                                                        <td>
                                                            <input type="radio" name="niming" value="-1" />全部
                                                            <input type="radio" name="niming" value="1" />是
                                                            <input type="radio" name="niming" value="0" />否
                                                        </td>
                                                        <td class="tblogtd">
                                                            是否反馈:
                                                        </td>
                                                        <td>
                                                            <input type="radio" name="fankui" value="-1" />全部
                                                            <input type="radio" name="fankui" value="1" />是
                                                            <input type="radio" name="fankui" value="0" />否
                                                        </td>
                                                        <tr>
                                                            <td colspan="4">
                                                                <div style="float: right;">
                                                                    <input type="submit" name="name" value="查 询" style="width: 60px;" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tr>
                                                </table>
                                                <div id="pgt" class="bm bw0 pgs cl">
                                                    <div class="pgt">
                                                        <div class="pg">
                                                            <%=strtt%>
                                                            <span id="fd_page_top1"></span><span class="pgb y"><a href="javascript:;" onclick="returnPage();">
                                                                第一页</a></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="threadlist" class="tl bm bmw">
                                                <div class="th">
                                                    <table cellspacing="0" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td class="by">
                                                                    主题
                                                                </td>
                                                                <td class="by">
                                                                    内容
                                                                </td>
                                                                <td class="num">
                                                                    发问人[匿名]
                                                                </td>
                                                                <td class="num">
                                                                    跟帖\反馈人[匿名]
                                                                </td>
                                                                <td class="num">
                                                                    是否反馈
                                                                </td>
                                                                <td class="num">
                                                                    时间
                                                                </td>
                                                                <td class="num">
                                                                    IP
                                                                </td>
                                                                <td class="num" style="width: 30px">
                                                                    操作
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="bm_c">
                                                    <div id="forumnew" style="display: none">
                                                    </div>
                                                    <table summary="forum_26" cellspacing="0" cellpadding="0" id="tdSerach">
                                                        <%=script %>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="bm bw0 pgs cl">
                                                <div class="pg">
                                                    <%=strtt %>
                                                    <span id="Span1"></span><span class="pgb y"><a href="javascript:;" onclick="returnPage();">
                                                        第一页</a></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </form>
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
                <div style="height: 30px; background-color: #FFFFFF; width: 970px; margin-left: auto;
                    margin-right: auto">
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
</body>
</html>
