<%@ Page Language="C#" AutoEventWireup="true" CodeFile="detail.aspx.cs" Inherits="detail" %>

<%@ Register Src="~/Work/UserControl/Top.ascx" TagName="Top" TagPrefix="uc2" %>
<%@ Register Src="~/Posts/UserControl/CMleft.ascx" TagName="CMleft" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" class=" widthauto">
<head>
    <title>
        <%=title%></title>
    <link rel="stylesheet" type="text/css" href="two/style_6_common.css" />
    <link rel="stylesheet" type="text/css" href="two/style_6_forum_viewthread.css" />
    <link rel="stylesheet" id="css_widthauto" type="text/css" href="two/style_6_widthauto.css" />
    <link type="text/css" rel="stylesheet" href="two/relate_subject.css" />
    <link rel="stylesheet" id="Link1" type="text/css" href="/Posts/style/style_6_common.css" />
    <link rel="stylesheet" id="Link2" type="text/css" href="/Posts/style/style_6_forum_forumdisplay.css" />
    <link rel="stylesheet" id="Link3" type="text/css" href="/Posts/style/style_6_widthauto.css" />
    <link rel="stylesheet" id="Link4" type="text/css" href="../Styles/floatdiv.css" />
    <link rel="stylesheet" href="/Posts/style/indexcss.css" />
    <script type="text/javascript" src="/Posts/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="/Posts/js/common.js"></script>
    <script src="/Posts/xhEditor/xheditor-1.2.1.min.js" type="text/javascript"></script>
    <script src="/Posts/xhEditor/xheditor_lang/zh-cn.js" type="text/javascript"></script>
    <script src="/Posts/JS/pub.js" type="text/javascript"></script>
    <script src="/Posts/JS/jquery.form.js" type="text/javascript"></script>
    <style type="text/css">
        .fastre2
        {
            background-image: url(/Posts/images/back.gif);
            background-repeat: no-repeat;
            background-position: 0 50%;
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
    <script>
        $(document).ready(function () {
            //编辑器
            $('#elm1').xheditor({ tools: 'simple', upLinkUrl: "upload.aspx", upLinkExt: "zip,rar,txt", upImgUrl: "upload.aspx", upImgExt: "jpg,jpeg,gif,png", upFlashUrl: "upload.aspx", upFlashExt: "swf", upMediaUrl: "upload.aspx", upMediaExt: "avi" });

            $('#QuickReplyEditor').xheditor({ tools: 'simple', upLinkUrl: "upload.aspx", upLinkExt: "zip,rar,txt", upImgUrl: "upload.aspx", upImgExt: "jpg,jpeg,gif,png", upFlashUrl: "upload.aspx", upFlashExt: "swf", upMediaUrl: "upload.aspx", upMediaExt: "avi" });
            //转页
            $('input[name=custompage]').keypress(function (e) {
                if (e.which == 13) {
                    var pageNum = $(this).val();
                    if (pageNum == 0) {
                        pageNum = 1;
                    }
                    var fatherID = $("#hdFather").val();
                    var content = $("#scbar_txt").val();
                    window.location.href = "detail.aspx?pageIndex=" + pageNum + "&fatherID=" + fatherID + "&content=" + content + "&num=0";
                }
            });
        })
        //搜索
        function SearchReposts() {
            var pageNum = 1;
            var fatherID = $("#hdFather").val();
            var content = $("#scbar_txt").val();
            window.location.href = "detail.aspx?pageIndex=" + pageNum + "&fatherID=" + fatherID + "&content=" + content + "&num=0"
        }
        //添加
        function AddReposts() {
            
            var pageNum = $("#hdPageNum").val();
            if (pageNum == 0) {
                pageNum = 1;
            }
            var cID = $("#hdCID").val();
            var fatherID = $("#hdFather").val();
            var releaseFatherID = $("#hdReleaseFatherID").val();
            var content = $("#QuickReplyEditor").val();
            var reUser = $("#hdUserName").val();
            var isReFeedback = $("#hdIsReFeedback").val()
            var isFeedback = 0;
            var isAnonymity = $('input[name="isAnonymity"]:checked').val();
            //是反馈贴强制实名
            if (isReFeedback == "1") {
                isAnonymity = 0;
            }
            if (CheckReposts()) {
                $.post("Ashx/RePostsAshx.ashx", { opt: 'add', content: content, fatherID: fatherID, cID: cID, releaseFatherID: releaseFatherID, reUser: reUser, isReFeedback: isReFeedback, isFeedback: isFeedback, isAnonymity: isAnonymity
                }, function (result) {
                    if (result == "fail") {
                        alert('操作失败');
                    }
                    else {
                        alert('操作成功');
                        window.location.href = "detail.aspx?pageIndex=" + pageNum + "&fatherID=" + fatherID + "&num=0";
                    }
                });
            }
        }

        //修改
        function EditReposts() {
            var pageNum = $("#hdPageNum").val();
            var fatherID = $("#hdFather").val();
            var replyId = $("#hdReplyId").val();
            var content = $("#QuickReplyEditor").val();
            var reUser = $("#hdUserName").val();
            var isReFeedback = $("#hdIsReFeedback").val()
            var isFeedback = 0;
            var isAnonymity = $('input[name="isAnonymity"]:checked').val();
            //是反馈贴强制实名
            if (isReFeedback == "1") {
                isAnonymity = 0;
            }
            if (replyId == "0") {
                alert('请先选择 编辑对象!');
                return;
            }
            if (CheckReposts()) {
                $.post("Ashx/RePostsAshx.ashx", { opt: 'edit', content: content, fatherID: fatherID, replyId: replyId, reUser: reUser, isReFeedback: isReFeedback, isFeedback: isFeedback, isAnonymity: isAnonymity
                }, function (result) {
                    if (result == "fail") {
                        alert('修改失败');
                    }
                    else {
                        alert('修改成功');
                        window.location.href = "detail.aspx?pageIndex=" + pageNum + "&fatherID=" + fatherID + "&num=0";
                    }
                });
            }
        }
        //选择-用于回复和反馈
        function SelectReply(replyId, releaseFatherID, isReFeedback) {
            CancelEdit();
            $("#QuickReplyEditor").focus();
            $("#QuickReplyEditor").val("");
            $("#hdReleaseFatherID").val(releaseFatherID);
            $("#hdIsReFeedback").val(isReFeedback);
           
            if (releaseFatherID != 0) {//子贴选项
                $.get("Ashx/RePostsAshx.ashx", { opt: "get", replyId: replyId }, function (result) {
                    $("#hdOriginContent").html(result);
                });
            }
            else {//主题选项
                $.get("Ashx/PostsAshx.ashx", { opt: "get", id: replyId }, function (result) {
                    $("#hdOriginContent").html(result);
                });
            }
            return false;
        }
        function AddReFeedBack() {
            $("#hdIsReFeedback").val(1);
            AddReposts();
        }
        function AddReply() {
            $("#hdIsReFeedback").val(0);
            AddReposts();
        }
        //获取
        function GetReply(replyId, isAnonymity) {
            $("#hdOriginContent").html("");
            $("#QuickReplyEditor").focus();
            $("#hdReplyId").val(replyId);
            if(isAnonymity=="1"){
                $('input[name=isAnonymity]').eq(0).attr("checked",'checked');
            }
            else{
                $('input[name=isAnonymity]').eq(1).attr("checked",'checked');
            }

            $.get("Ashx/RePostsAshx.ashx", { opt: "get", replyId: replyId }, function (result) {
                $("#QuickReplyEditor").val(result);
            });
            $("#divBtnEdit").show();
            $("#divBtnOpt").hide();
            return false;
        }
        //取消编辑
        function CancelEdit() {
            $("#QuickReplyEditor").focus();
            $("#hdReplyId").val();
            $("#divBtnOpt").show();
            $("#divBtnEdit").hide();
            $("#QuickReplyEditor").val("");
            return false;
        }
        //验证
        function CheckReposts() {
            var content = $("#QuickReplyEditor").val();
            if (content.length == 0) {
                alert("回复内容不能为空");
                $("#QuickReplyEditor").focus();
                return false;
            }
            if (content.length > 70000)
            { 
                   alert("内容长度超过8000个字符");
                   $("#QuickReplyEditor").focus();
                    return false; 
            }
            var isLeagl = CheckWord(content);
            if (isLeagl == false) {
                return false;
            }
            var reUser = $("#hdUserName").val();
            if (reUser.length == 0) {
                alert("用户尚未登录");
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //首先将#back-to-top隐藏
            $("#back-to-top").hide();
            //当滚动条的位置处于距顶部200像素以下时，跳转链接出现，否则消失
            $(function () {
                $(window).scroll(function () {
                    if ($(window).scrollTop() > 200) {
                        $("#back-to-top").fadeIn(1500);
                    }
                    else {
                        $("#back-to-top").fadeOut(1500);
                    }
                });
                //当点击跳转链接后，回到页面顶部位置
                $("#back-to-top").click(function () {
                    $('body,html').animate({ scrollTop: 0 }, 1000);
                    return false;
                });
            });
        });
        function closeDivDtail() {
            $("#divzhuti").hide();
        }
    </script>
</head>
<body id="nv_forum" class="pg_viewthread">
    <input id="hdUserName" type="hidden" value='<%=userName%>' />
    <input id="hdCID" type="hidden" value="<%=cID%>" />
    <input id="hdFather" type="hidden" value='<%=fatherID%>' />
    <input id="hdReleaseFatherID" type="hidden" value="0" />
    <input id="hdIsReFeedback" type="hidden" value="0" />
    <input id="hdPageNum" type="hidden" value='<%=pageNum%>' />
    <input id="hdPageIndex" type="hidden" value='<%=pageIndex%>' />
    <input id="hdReplyId" type="hidden" value='0' />
    <link href="two/jiathis_counter.css" rel="stylesheet" type="text/css" />
    <link href="two/jiathis_share.css" rel="stylesheet" type="text/css" />
    <div class="jiathis_style" style="position: absolute; z-index: 1000000000; display: none; top: 50%; left: 50%; overflow: auto;">
    </div>
    <div class="jiathis_style" style="position: absolute; z-index: 1000000000; display: none; overflow: auto;">
    </div>
    <div id="append_parent">
    </div>
    <div id="ajaxwaitid">
    </div>
    <div class="body_bg">
        <div>
            <uc2:Top ID="Top1" runat="server" />
        </div>
        <div class="c_y">
            <div class="c_yc">
                <div class="c_ycr">
                    <div class="c_yeeib">
                        <div id="wp" class="wp">
                            <link href="two/wmff_zk.css" rel="stylesheet" type="text/css">
                            <style id="diy_style" type="text/css"></style>
                            <%--                            <div id="diynavtop" class="area">
                            </div>--%>
                            <div id="ct" class="wp cl">
                                <!-- 分页 start -->
                                <div class="pgs mtm mbm cl">
                                    <div id="pt" class="bm cl" style="width: 110px; float: left">
                                        <div class="z">
                                            <%=GetGuider()%>
                                        </div>
                                    </div>
                                    <a id="A1" href="/Posts/Publish.aspx" title="发新帖" style="float: right">
                                        <img src="../images/pn_post.png" alt="发新帖"></a>
                                    <div id="scbar" class="cl" style="float: right; margin-top: 2px">
                                        <table cellspacing="0" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td class="scbar_icon_td">
                                                    </td>
                                                    <td class="scbar_txt_td">
                                                        <input type="text" name="srchtxt" id="scbar_txt" value="<%=content %>" onblur="onBlur(this.id,'请输入搜索内容')" onfocus="onFocus(this.id,'请输入搜索内容')" autocomplete="off" x-webkit-speech="" speech="" class=" xg1" placeholder="请输入搜索内容" style="width: 300px">
                                                    </td>
                                                    <td class="scbar_type_td">
                                                    </td>
                                                    <td class="scbar_btn_td">
                                                        <button type="submit" name="searchsubmit" id="scbar_btn" sc="1" class="pn pnc" value="true" mid="" onclick="SearchReposts()">
                                                            <strong class="xi2 xs2">搜索</strong></button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <!-- 分页 end -->
                                <div id="postlist" class="pl bm" style="margin-top: 10px;">
                                    <!-- 主题      -->
                                    <%=GetBbsTitle()%>
                                    <!-- 主题帖子  -->
                                    <%=GetBbsReply()%>
                                </div>
                                <!-- 分页-->
                                <div class="pgs mtm mbm cl">
                                    <%=strtt%>
                                    <span class="pgb y" id="visitedforumstmp"><a href="index.aspx">返回列表</a></span>
                                    <%--                                    <a
                                        id="newspecialtmp" href="/Posts/Publish.aspx" title="发新帖">
                                        <img src="../images/pn_post.png" alt="发新帖">
                                    </a>--%>
                                    <div style="clear: both">
                                    </div>
                                </div>
                                <div id="diyfastposttop" class="area">
                                </div>
                                <a name="anchor"></a>
                                <div id="f_pst" class="pl bm bmw" style="margin-top: 10px;">
                                    <table cellspacing="0" cellpadding="0">
                                        <tbody>
                                            <tr>
                                                <td class="pls">
                                                </td>
                                                <td class="plc">
                                                    <span id="fastpostreturn"></span>
                                                    <div class="cl">
                                                        <div id="fastsmiliesdiv" class="y">
                                                            <div id="fastsmiliesdiv_data">
                                                                <div id="fastsmilies" style="float: left;">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="hasfsl" id="fastposteditor">
                                                            <div style="float: left; width: 60px; font-weight: bold">
                                                                编辑内容:</div>
                                                            <div id="hdOriginContent" style="float: left;">
                                                            </div>
                                                            <br />
                                                            <div class="tedt mtn" style="border: 0px; text-align: center;">
                                                                <!-- 编辑器 start -->
                                                                <textarea id="QuickReplyEditor" name="QuickReplyEditor" rows="12" cols="120" style="width: 779px;">
                                                                </textarea>
                                                                <!-- 编辑器 end -->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <p class="ptm pnpost">
                                                        <div style="float: left; margin-right: 5px;">
                                                            <%=sn %>
                                                        </div>
                                                        <%=GetButtion()%>
                                                    </p>
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
        <div class="fg">
        </div>
    </div>
    <p id="back-to-top">
        <a href="#top"><span>
            <img src="../images/scrolltop.png" /></span></a></p>
    <div id="discuz_tips" style="display: none;">
    </div>
    <div id="divzhuti" class="bm" style="display: none; left: 24%; top: 25%; width: 700px; position: fixed; border: 4px solid #B8BEC3;">
        <form method="post" id="fastpostform" action="/Posts/Ashx/Control.ashx?type=edit">
        <div class="bm_h" style="line-height: 30px; margin: 0px; padding: 0px;">
            <h2 style="width: 100px; float: left; margin-left: 10px;">
                修改主题</h2>
            <a href="javascript:void(0)" style="text-decoration: none; float: right; margin-right: 25px; text-decoration: none;" onclick="closeDivDtail()">关闭</a>
        </div>
        <div class="bm_c">
            <div id="Div1" style="margin: -5px 0 5px">
            </div>
            <div class="pbt cl">
                <div class="ftid">
                    <select id="s1" name="Cid">
                        <%=CategoryList()%>
                    </select>
                </div>
                <input type="text" id="subject" name="subject" class="px" tabindex="11" style="width: 25em" onkeyup="setTextareaLength(this.id,'checklen',80)" value="<%=postModel.Title %>" />
                <span>还可输入 <strong id="checklen">80</strong> 个字符</span>
            </div>
            <div class="cl">
                <div id="Div2" class="y">
                    <div id="Div3">
                        <div id="Div4">
                        </div>
                    </div>
                </div>
                <div class="hasfsl" id="Div5" style="margin-right: 0px;">
                    <div class="tedt" style="border: 0px;">
                        <textarea id="elm1" name="txtContent" rows="12" style="width: 100%;"><%=postModel.Description%></textarea>
                    </div>
                </div>
            </div>
            <p class="ptm pnpost">
                <input type="radio" value="1" name="radioname" />匿名
                <input type="radio" value="0" name="radioname" checked="checked" />实名
                <button type="submit" style="border: 0" onclick="return CheckSub()" name="topicsubmit" id="fastpostsubmit" value="topicsubmit" tabindex="13" class="pn pnc">
                    <strong>保存帖子</strong></button>
            </p>
            <input type="hidden" id="pid" name="pid" value="<%=postModel.ID%>" />
            <input type="hidden" id="loginname" name="loginname" value="<%=loginname %>" />
        </div>
        </form>
    </div>
    <script>
        function CheckSub() {
            var backcheck = true;
            var zhuti_type = $("#s1").val();
            if (zhuti_type == "0") { alert("请选择主题分类"); return false; }
            var subtitle = $("#subject").val();
            if (subtitle == "") { alert("请填写标题"); return false; }
            var txtinfo = $("#elm1").val();
            if (txtinfo == "") { alert("请填写内容"); return false; }
            if (txtinfo.length > 20000) { alert("内容长度超过20000个字符"); return false; }
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
        function showedit() {
            $("#divzhuti").show();
        }
    </script>
</body>
</html>
