<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Posts_index" ValidateRequest="false" %>

<%@ Register Src="~/Work/UserControl/Top.ascx" TagName="Top" TagPrefix="uc2" %>
<%@ Register Src="~/Posts/UserControl/CMleft.ascx" TagName="CMleft" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>环宇通达 - 意见与反馈</title>
    <link rel="stylesheet" type="text/css" href="/posts/style/style_6_common.css" />
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_forum_forumdisplay.css" />
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_widthauto.css" />
    <link rel="stylesheet" href="/Posts/style/reset.css" />
    <link rel="stylesheet" href="/Posts/style/selectForK13.css" />
    <link rel="stylesheet" href="/Posts/style/indexcss.css" />
    <script type="text/javascript" src="/Posts/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/Posts/js/common.js"></script>
    <script src="/posts/js/pub.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Posts/js/selectForK13.js"></script>
    <script type="text/javascript" src="/Posts/xhEditor/xheditor-1.2.1.min.js"></script>
    <script type="text/javascript" src="/Posts/xhEditor/xheditor_lang/zh-cn.js"></script>
    <script type="text/javascript">
        var tpcid = 0;
        $(function () {
            $("#top_a").html("全部类型");
            $("#sTime").selectForK13();
            $("#sOrder").selectForK13();
            $("#s1").selectForK13({
                "width": "70px"
            });
            $('#elm1').xheditor({ tools: 'simple', upLinkUrl: "upload.aspx", upLinkExt: "zip,rar,txt", upImgUrl: "upload.aspx", upImgExt: "jpg,jpeg,gif,png", upFlashUrl: "upload.aspx", upFlashExt: "swf", upMediaUrl: "upload.aspx", upMediaExt: "avi" });
            var tp = request("type_pb");
            var fk = request("fk");
            var st = request("st"); //筛选
            var px = request("px"); //排序
            var ss = request("ss");
            tpcid = request("typecid");
            if (ss != "") {
                $("#scbar_txt").val(decodeURI(ss));
            }
            if (tp != "") {
                $("#thread_types").find("li").removeClass();
                $("#thread_types").find("li").each(function () {
                    if ($(this).attr("tp") == tp) {
                        $(this).addClass("xw1 a");
                    }
                    if(fk == 1&&$(this).attr("tp")=="back")
                    { $("#thread_types").find("li").removeClass();
                    $(this).addClass("xw1 a");
                    }
                })
            }

            $("#sTime").children("option").each(function () {
                var temp_value = $(this).val();
                if (temp_value == st) {
                    $(this).attr("selected", true);
                    $(this).parent().parent().find(".K13_select_checked").html($(this).text());
                }
            });
            $("#sOrder").children("option").each(function () {
                var temp_value = $(this).val();
                if (temp_value == px) {
                    $(this).attr("selected", true);
                    $(this).parent().parent().find(".K13_select_checked").html($(this).text());
                }
            });

        })
        function CheckSub() {
           
            var backcheck = true;
            var zhuti_type = $("#s1").val();
            if (zhuti_type == "0") { alert("请选择主题分类"); return false; }
            var subtitle = $("#subject").val();
            if (subtitle == "") { alert("请填写标题"); return false; }
            var txtinfo = $("#elm1").val();
            if (txtinfo == "") { alert("请填写内容"); return false; }
            if (txtinfo.length > 1000) { alert("内容长度超过1000个字符"); return false; }
            var checkwordinfo = subtitle + txtinfo;
            $.ajax({
                type: "post",
                url: "Ashx/Control.ashx",
                data: { "info": checkwordinfo, "type": "check" },
                async: false,
                success: function (data) {
                    if (data == "false") {
                        alert("输入内容包含敏感字符，请重新输入");
                        backcheck = false;
                    }
                    else {
                        backcheck = true;
                    }
                }

            })

            return backcheck;
        }
        //收集信息(获取每个样式值放于url中)
        var fank = 0; //是否反馈
        var cid = 0; //分类ID
        function fankui() {
            fank = 1;
//            $("#thread_types").find("li").removeClass();
//            $("#thread_types").find("li[tp=back]").addClass("xw1 a");
            getinfopost();
        }
        function fenlei(id) {
            cid = id;
            getinfopost();
        }
        function getinfopost() {
            var tp = 0;
            if ($("#thread_types li[class='xw1 a']").length>0 && $("#thread_types li[class='xw1 a']").attr("tp").length > 0) //获取顶部导航属性
                tp = $("#thread_types li[class='xw1 a']").attr("tp");
            var tpcid = 0;
            if (cid != 0)
                tpcid = cid;
            else {
                if ($("#lf_1 dd[class=bdl_a]").find("a").attr("tp"))//左侧板块导航
                    tpcid = $("#lf_1 dd[class=bdl_a]").find("a").attr("tp");
            }
            var px = $("#sOrder").val(); //排序
            var st = $("#sTime").val(); //筛选
            var fk = fank; //已反馈按钮
            var sstxt = $("#scbar_txt").val(); //搜索
            var url = "index.aspx?px=" + px + "&st=" + st + "&typecid=" + tpcid + "&type_pb=" + tp + "";
            if (fk != "") {
                url += "&fk=" + fk;
            }
            if (sstxt != "请输入搜索内容") {
                url += "&ss=" + encodeURI(sstxt);
            }
            // alert("index.aspx?px=" + px + "&st=" + st + "&typecid=" + tpcid + "&type_pb=" + tp + "");
            window.location.href=url;
        }
        function aaa() {
            alert("d");
            CheckWord("dfddf");
        }

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
        <div class="mu_nv" style="margin-top:10px;">
        </div>
        <div class="c_y">
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
                            <style id="diy_style" type="text/css"></style>
                            <div id="diynavtop" class="area">
                            </div>
                            <div class="boardnav">
                                <div id="ct" class="wp cl" style="margin-left: 145px">
                                    <uc1:CMleft ID="CMleft1" runat="server" />
                                    <div class="mn">
                                        <div class="drag">
                                            <div id="diy4" class="area">
                                            </div>
                                        </div>
                                        <div style="margin-top: 4px" class="bm bw0 pgs cl">
                                            <a href="/Posts/Publish.aspx" id="newspecial" name="newspecialtmp" title="发新帖" style="float: right">
                                                <img src="/images/pn_post.png" alt="发新帖">
                                            </a>
                                            <!-- search ----------------->
                                            <div class="cl" id="scbar" style="float: right; margin-top: 2px">
                                                <%--       <form target="_blank" autocomplete="off" method="post" runat="server" id="scbar_form">--%>
                                                <table cellspacing="0" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td class="scbar_icon_td">
                                                            </td>
                                                            <td class="scbar_txt_td">
                                                                <input type="text" id="scbar_txt" speech="" x-webkit-speech="" autocomplete="off" value="请输入搜索内容" id="scbar_txt" name="srchtxt" class=" xg1" onblur="onBlur(this.id,'请输入搜索内容')" onfocus="onFocus(this.id,'请输入搜索内容')" style="width: 300px">
                                                            </td>
                                                            <td class="scbar_type_td">
                                                            </td>
                                                            <td class="scbar_btn_td">
                                                                <%--          <button value="true" class="pn pnc" sc="1" onclick="getinfopost('sstxt','')" name="searchsubmit"
                                                                type="submit" mid="">
                                                                <strong class="xi2 xs2">搜索</strong></button>--%>
                                                                <button value="true" class="pn pnc" onclick="getinfopost('sstxt','')" sc="1" id="scbar_btn" mid="">
                                                                    <strong class="xi2 xs2">搜索</strong></button>
                                                            </td>
                                                            <td class="scbar_hot_td">
                                                                <div id="scbar_hot">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <%--    </form>--%>
                                            </div>
                                            <!-- search ----------------->
                                            <div style="float: left">
                                                <div id="pt" class="bm cl">
                                                    <div class="z">
                                                        <a href="index.aspx" class="nvhm" title="意见与反馈-首页">意见与反馈</a><em>&#187;</em><a id="top_a" href="#">当前版块</a></div>
                                                </div>
                                                <div class="wp">
                                                    <div id="diy1" class="area">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <ul id="thread_types" class="ttp bm cl" style="padding: 4px 0px 0px 0px;">
                                            <li class="xw1 a" tp="all"><a onclick="getinfopost('tp','all')" href="javascript:;">全部问题</a></li>
                                            <li tp="hot"><a href="javascript:;">热点问题</a></li>
                                            <li tp="back"><a href="javascript:;">已反馈问题</a></li>
                                            <li tp="noback"><a href="javascript:;">待反馈问题</a></li>
                                            <li tp="myself"><a href="javascript:;">我的问题</a></li>
                                            <li style="float: right; width: 60px; background-image: none;" class="bm bw0 pgs cl"><span class="pgb y" style="border: 1px solid #cccccc; padding: 0;"><a href="index.aspx" style="padding-left: 0px; background: none">返回首页</a></span> </li>
                                        </ul>
                                        <div id="threadlist" class="tl bm bmw">
                                            <div class="th">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <th colspan="2">
                                                                <div class="tf">
                                                                    筛选:
                                                                    <select id="sTime">
                                                                        <option value="0" selected="selected">全部时间</option>
                                                                        <option value="1">一天</option>
                                                                        <option value="2">两天</option>
                                                                        <option value="7">一周</option>
                                                                        <option value="30">一个月</option>
                                                                        <option value="90">三个月</option>
                                                                    </select>
                                                                    排序:
                                                                    <select id="sOrder">
                                                                        <option value="0" selected="selected">默认排序</option>
                                                                        <option value="1">发帖时间</option>
                                                                        <option value="2">回复/查看</option>
                                                                        <option value="3">查看</option>
                                                                        <option value="4">最后发表</option>
                                                                        <option value="5">热门</option>
                                                                    </select></div>
                                                            </th>
                                                            <td class="by" style="text-align: center">
                                                                作者
                                                            </td>
                                                            <td class="num" style="text-align: center">
                                                                回复/查看
                                                            </td>
                                                            <td class="by" style="text-align: center">
                                                                最后发表
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="bm_c">
                                                <div id="forumnew" style="display: none">
                                                </div>
                                                <form method="post" autocomplete="off" name="moderate" id="Form2" action="#">
                                                <table summary="forum_26" cellspacing="0" cellpadding="0">
                                                    <asp:Repeater ID="Re_InfoList" runat="server">
                                                        <ItemTemplate>
                                                            <tbody>
                                                                <tr>
                                                                    <th>
                                                                        <em style="color: #336699; font-weight: bold;">[<a onclick="fenlei('<%#Eval("cid") %>')" href="javascript:;"><%#Eval("Cname") %></a>]</em> <a href="detail.aspx?pageIndex=1&fatherID=<%#Eval("id") %>&num=0" class="xst">
                                                                            <%#HYTD.Common.StringHelper.SubString(Eval("title").ToString(),25)%>
                                                                        </a><a onclick="fankui()" href="javascript:;" title="只看已反馈的">
                                                                            <%#Eval("isFeedback").ToString()=="1"?"[已反馈]":""%></a>
                                                                    </th>
                                                                    <td class="by" style="text-align: center">
                                                                        <cite>
                                                                            <%#CheckAdmin(Eval("isAnonymity").ToString(),Eval("name").ToString()) %>
                                                                        </cite><em><span>
                                                                            <%#Eval("ReleaseTime") %></span></em>
                                                                    </td>
                                                                    <td class="num" style="text-align: center">
                                                                        <%#Eval("ReCount")%><em><%#Eval("LookCount")%></em>
                                                                    </td>
                                                                    <td class="by" style="text-align: center">
                                                                        <cite>
                                                                            <%#CheckFanKuiAdmin(Eval("isAnonymity").ToString(), Eval("lastusername").ToString())%>
                                                                        </cite><em><span title="<%#Eval("LastTime") %>">
                                                                            <%#Eval("LastTime") %></span></em>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                                </form>
                                            </div>
                                        </div>
                                        <div class="bm bw0 pgs cl">
                                            <%=strtt %>
                                            <span id="fd_page_bottom"></span><span class="pgb y"><a href="javascript:;" onclick="returnPage();">第一页</a></span>
                                            <%--                                                <a href="/Posts/Publish.aspx" name="newspecialtmp" id="newspecialtmp"
                                                    title="发新问题">
                                                    <img src="/images/pn_post.png" alt="发新问题"></a>--%>
                                            <div style="clear: both">
                                            </div>
                                        </div>
                                        <div id="diyfastposttop" class="area">
                                        </div>
                                        <div id="diyforumdisplaybottom" class="area">
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
    <script type="text/javascript">
        $().ready(function () {
            if (tpcid != "" && tpcid != "0") {
                var tpcidtxt = $("#lf_1 dd[class=bdl_a]").find("a").text();
                $("#top_a").html(tpcidtxt);
            }
        });
    </script>
</body>
</html>
