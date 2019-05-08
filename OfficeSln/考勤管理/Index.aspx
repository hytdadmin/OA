<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<%@ Register Src="Work/UserControl/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="Work/UserControl/Bottom.ascx" TagName="Bottom" TagPrefix="uc2" %>
<%@ Register Src="Work/UserControl/PublishSay.ascx" TagName="PublishSay" TagPrefix="uc6" %>
<%@ Register Src="Work/UserControl/NavigateUC.ascx" TagName="NavigateUC" TagPrefix="uc5" %>
<%@ Register Src="Work/UserControl/PublishSayAlert.ascx" TagName="PublishSayAlert"
    TagPrefix="uc4" %>
<%@ Register Src="Work/UserControl/ChangeHeadImg.ascx" TagName="ChangeHeadImg" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta name="author" content="环宇通达 +86-532-86100168">
    <meta name="description">
    <meta name="keywords">
    <link href="/Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="/work/style/indexcss.css" rel="stylesheet" type="text/css">
    <script src="/work/scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="Work/Scripts/pub.js" type="text/javascript"></script>
    <link href="/work/Style/pager.css" rel="stylesheet" type="text/css" />
    <title>首页</title>
    <style type="text/css">
        .window
        {
            width: 250px;
            background-color: #d0def0;
            position: absolute;
            padding: 2px;
            margin: 5px;
            display: none;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            getUserUC();
            getBulletinUC();
            getSayUC();
            kuaisutongdao();
            //定时刷新
            //            var ref = "";
            //            ref = setInterval(function () {
            //                getUserUC();
            //                getBulletinUC();
            //                getSayUC();
            //            }, 1000*60);

        });
    </script>
    <script type="text/javascript">
        function UpdatePwd() {
            var UserCode = $("#spUserCode").text();
            location.href = "UpdatePwd.aspx?" + Math.random();
        }
        function exit() {
            if (confirm("是否返回登陆页面！")) {

                location.href = "Login.aspx?exti=0";
            }

        }
        function Input_Click() {
            location.href = "Import.aspx?" + Math.random();
        }
    </script>
    <script type="text/javascript">
        //修改密码
        function UpdatePwd() {
            var UserCode = $("#spUserCode").text();
            location.href = "UpdatePwd.aspx?" + Math.random();
        }
        var hidRemark = "";
        var hidDate = "";
        var c_idx = 0;
        var hidName = 0;
        //备注说名
        $(document).ready(function () {
            $(".dvSpan").each(function (idx) {
                $(this).click(function (e) {
                    popCenterWindow(e, this);
                    var lab = $(this).prev().text();
                    var strlab = lab.split("-");
                    $("#hidDate").val(strlab[0]);
                    hidDate = strlab[0];
                    hidName = strlab[1];
                    c_idx = idx;
                });
            });
        });
        //定义弹出居中窗口的方法 
        function popCenterWindow(ev, obj) {
            //计算弹出窗口的左上角Y的偏移量 
            var mousePos = mouseCoords(ev);
            var popX = mousePos.x-250;
            var popY = mousePos.y + 10;
            var txtCount1 = $.trim($(obj).html());
            //设定窗口的位置 
            $("#center").css("top", popY).css("left", popX).slideToggle("slow");
            if ($.trim($(obj).html()) == "备注说明") {
                txtCount1 = "";
                $("#txtRemark").val("");
            } else {
                $("#txtRemark").val($.trim($(obj).html()));
                $("#spFontCount").text(21 - txtCount1.length);
            }

        }
        //鼠标位置
        function mouseMove(ev) {
            ev = ev || window.event;
            var mousePos = mouseCoords(ev);
        }
        function mouseCoords(ev) {
            if (ev.pageX || ev.pageY) {
                return { x: ev.pageX, y: ev.pageY };
            }
            return {
                x: ev.clientX + document.body.scrollLeft - document.body.clientLeft,
                y: ev.clientY + document.body.scrollTop - document.body.clientTop
            };
        }
        //检查字体的数
        function checkFontCount() {
            var txtCount = $("#txtRemark").val();
            if (txtCount.length > 21) {
                $("#txtRemark").val(txtCount.substring(0, 21));
                $("#spFontCount").text(0);
            } else {
                $("#spFontCount").text(21 - txtCount.length);
            }
        }
        //提交
        function sedRemark() {

            $("#hidRk").val($("#txtRemark").val());
            hidRemark = $("#hidRk").val();
            $.post("/UpdateRemark.ashx", { "remark": hidRemark, "date": hidDate, "userName": hidName }, function (msg) {
                if (msg == 1) {
                    $(".dvSpan").eq(c_idx).text("备注说明").css("color", "#ABABAB");
                    $("#hidDate").val("");
                    $("#center").slideToggle("slow");
                } else if (msg == 0) {
                    $("#center").slideToggle("slow");
                    $("#hidDate").val("");
                    $(".dvSpan").eq(c_idx).text(hidRemark).css("color", "#CC0000");

                }
            })

        }
        //关闭窗口
        function closeRemark() {
            $("#center").slideToggle("slow");
            $("#hidDate").val("");
        }
    </script>
    <!--[if IE]>
	<script src="Scripts/html5.js"></script>
<![endif]-->
    <!--[if IE 6]>
	<script type="text/javascript" src="Scripts/png-ie.js"></script>
	<script>
		DD_belatedPNG.fix('.png_bg');
	</script>
<![endif]-->
</head>
<body>
    <uc3:ChangeHeadImg ID="ChangeHeadImg1" runat="server" />
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc1:Top ID="Top1" runat="server" />
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
            <div style="border: 0px solid #e0e0e0; padding: 10px; margin-top: 5px;">
                选择月份：
                <asp:TextBox Width="100px" ID="txtMonth" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM',minDate:'2008-2',maxDate:'%y-%M-%d'})"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                选择人员：
                <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="UserName" DataValueField="UserName">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCreate" runat="server" Text="查 询" Width="100px" OnClick="Unnamed1_Click"
                    CausesValidation="False" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <%=InputData%>
            </div>
            <section class="content wrap clearfix" style="width: 720px">
	<section class="content_box clearfix">
		<article>
			<div class="timecard_box" style="height:720px;">
						<table border="0" cellspacing="0" cellpadding="0" style="margin-top:10px;width:700px">
						<thead style="height:30px">
						  <tr>
							<td>星期一</td>
							<td>星期二</td>
							<td>星期三</td>
							<td>星期四</td>
							<td>星期五</td>
							<td>星期六</td>
							<td>星期日</td>
						  </tr>
						</thead>
						<tbody>
                           <%=DateList%>
						</tbody>
						</table>
			
			</div>
		</article>
		
	
	</section>
</section>
        </div>
    </div>
    <div class="foot">
        <uc2:Bottom ID="Bottom1" runat="server" />
    </div>
    <uc4:PublishSayAlert ID="PublishSayAlert1" runat="server" />
    <!--备注说明-->
    <div class="window" id="center">
        <div id="title" class="title" style="margin-left: 20px; margin-top: 10px; margin-bottom: 10px;">
            <label>
                备注说明</label>
            <span style="margin-left: 155px; font-size: 16px; cursor: pointer;" onclick="closeRemark()">
                ×</span><br />
            <br />
            <textarea style="width: 210px; height: 50px; font-size: 13px;" id="txtRemark" onkeyup="checkFontCount()"></textarea><br />
            <br />
            <label>
                还能输入<span style="color: #0A8CD2; font-size: 14px; width: 18px;" id="spFontCount">21</span>个字</label>
            <input type="button" value="保 存" style="margin-left: 73px; width: 48px;" onclick="sedRemark()" />
            <input type="hidden" name="hidRemark" id="hidRk" />
            <input type="hidden" name="hidDate" id="hidDate" />
             <input type="hidden" name="hidUserCode" id="hidUserCode" />
        </div>
    </div>
    <div id='pageloading' style='position: absolute; background-color: black; opacity: 0.3; z-index: 1200; top: 0px; left: 0px; width: 100%; height: 10000px; display: none'>
                <img src='/images/MVA_loading.gif' style='position: fixed; top: 40%; left: 50%' />
    </div>
    </form>
    <script src="Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</body>
</html>
 