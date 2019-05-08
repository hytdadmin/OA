<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Publish.aspx.cs" Inherits="Posts_Publish" %>

<%@ Register Src="~/Work/UserControl/Top.ascx" TagName="Top" TagPrefix="uc2" %>
<%@ Register Src="~/Posts/UserControl/CMleft.ascx" TagName="CMleft" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>环宇通达 - 意见与反馈</title>
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_common.css" />
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_forum_forumdisplay.css" />
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_widthauto.css" />
     <link rel="stylesheet" href="/Posts/style/reset.css" />
    <link rel="stylesheet" href="/Posts/style/selectForK13.css" />
    <link rel="stylesheet" href="/Posts/style/indexcss.css" />
    <script type="text/javascript" src="/Posts/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/Posts/js/common.js"></script>
    <script src="/Posts/js/pub.js" type="text/javascript"></script>
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
                "width": "100px"
            });
            $('#elm1').xheditor({ tools: 'simple', upLinkUrl: "upload.aspx", upLinkExt: "zip,rar,txt", upImgUrl: "upload.aspx", upImgExt: "jpg,jpeg,gif,png", upFlashUrl: "upload.aspx", upFlashExt: "swf", upMediaUrl: "upload.aspx", upMediaExt: "avi" });
        })         
        function CheckSub() {
            
            var backcheck = true;
            var zhuti_type = $("#s1").val();
            if (zhuti_type == "0") { alert("请选择主题分类"); return false; }
            var subtitle = $("#subject").val();
            if (subtitle == "") { alert("请填写标题"); return false; }
            var txtinfo = $("#elm1").val();
            if (txtinfo == "") { alert("请填写内容"); return false; }
            if (txtinfo.length > 70000) { alert("内容长度超过4000个汉字"); return false; }
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

            });

            return backcheck;
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
        <div class="mu_nv">
        </div>
        <div class="c_y">
            <div class="c_yc">
                <div class="c_ycr">
                    <div class="c_yeeib">
                        <div id="wp" class="wp">
                            <a href="javascript:;" class="wmff_wxyunnav"></a>
                            <style id="diy_style" type="text/css"></style>
                            <div id="diynavtop" class="area">
                            </div>
                            <div class="drag">
                                <div id="diy4" class="area">
                                </div>
                            </div>
                            <div id="diyfastposttop" class="area" style="height: 10px">
                            </div>
                            <div class="boardnav">
                                <div id="ct" class="wp cl">
                                    <div class="mn">
                                        <div id="" class="bm">
                                            <div class="bm_h">
                                                <h2 style="width: 100px; float: left">
                                                    编辑内容</h2>
                                                <div class="bm bw0 pgs cl" style="float: right; width: 200px;">
                                                    <span id="fd_page_bottom"></span><span class="pgb y" id="Span1"><a href="index.aspx">返回列表</a></span>
                                                </div>
                                            </div>
                                            <div class="bm_c">
                                                <form method="post" id="fastpostform" action="/Posts/Ashx/Control.ashx?type=add">
                                                <div id="fastpostreturn" style="margin: -5px 0 5px">
                                                </div>
                                                <div class="pbt cl" style="float: left">
                                                    <div style="width: 115px; float: left">
                                                        请选择意见的分类：
                                                    </div>
                                                    <div class="ftid">
                                                        <select id="s1" name="Cid">
                                                            <%=CategoryList("2")%>
                                                        </select>
                                                    </div>
                                                    <div style="width: 142px; float: left">
                                                        请输入意见的标题名称：
                                                    </div>
                                                    <div style="width: 300px; float: left;">
                                                        <input type="text" id="subject" name="subject" class="px" value="" tabindex="11" style="width: 25em" onkeyup="setTextareaLength(this.id,'checklen',80)" />
                                                    </div>
                                                    <div style="width: 110px; float: left; padding-left: 15px">
                                                        <span>还可输入 <strong id="checklen">80</strong> 个字符</span></div>
                                                    <div style="clear: both">
                                                    </div>
                                                </div>
                                                <div style="clear: both">
                                                </div>
                                                <div class="cl">
                                                    <div id="fastsmiliesdiv" class="y">
                                                        <div id="fastsmiliesdiv_data">
                                                            <div id="fastsmilies">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="hasfsl" id="fastposteditor">
                                                        <div class="tedt">
                                                            <textarea id="elm1" name="txtContent" rows="12" style="width: 110%;"></textarea>
                                                        </div>
                                                    </div>
                                                </div>
                                                <p class="ptm pnpost">
                                                    <input type="radio" value="1" name="radioname" />匿名
                                                    <input type="radio" value="0" name="radioname" checked="checked" />实名 &nbsp;&nbsp;
                                                    <button type="submit" style="border: 0" onclick="return CheckSub()" name="topicsubmit" id="fastpostsubmit" value="topicsubmit" tabindex="13" class="pn pnc">
                                                        <strong>发表</strong></button>
                                                </p>
                                                <input type="hidden" id="loginname" name="loginname" value="<%=loginname %>" />
                                                </form>
                                            </div>
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
