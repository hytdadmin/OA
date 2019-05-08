<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Import.aspx.cs" Inherits="Import" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<%@ register src="Work/UserControl/Top.ascx" tagname="Top" tagprefix="uc1" %>
<%@ register src="Work/UserControl/Bottom.ascx" tagname="Bottom" tagprefix="uc2" %>
<%@ register src="Work/UserControl/ChangeHeadImg.ascx" tagname="ChangeHeadImg" tagprefix="uc3" %>
<head runat="server" id="Head1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta name="author" content="环宇通达 +86-532-86100168">
    <meta name="description">
    <meta name="keywords">
    <script src="/work/scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/work/scripts/pub.js" type="text/javascript"></script>
    <link href="/work/Style/pager.css" rel="stylesheet" type="text/css" />
    <title>首页</title>
    <link href="Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="Work/Style/indexcss.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 191px;
        }
        .style2
        {
            width: 350px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            getUserUC();
            getBulletinUC();
            getSayUC();
            //定时刷新
            //            var ref = "";
            //            ref = setInterval(function () {
            //                getUserUC();
            //                getBulletinUC();
            //                getSayUC();
            //            }, 1000*60);
        });
    </script>
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
        <div class="W_main_right" style="height:670px;">
            <div style="border: 1px solid #e0e0e0; padding: 10px; margin-top: 5px;width:695px">
                <table id="Table1" cellspacing="0" cellpadding="0" border="0" width="95%" height="120px";>
                    <tr style="height:50px;">
                        <td class="style1">
                            请导入考勤Excel文件：
                        </td>
                        <td class="style2">
                            <input id="fileExcel" type="file" runat="server" name="fileExcel" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fileExcel"
                                ErrorMessage="请您选择Excel文件！" ForeColor="Red">请您选择Excel文件！</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Button ID="btnImport" runat="server" Text="导入" OnClick="btnImport_Click" Width="73px" />
                        </td>
                    </tr>
                    <tr style="margin-top: 5px">
                        <td colspan="3" >
                            <label runat="server" id="Label1" style="color: Red;">
                                (注意：导入速度比较慢，显示导入成功后再关闭些页面)</label>
                        </td>
                    </tr>
                </table>
                <label runat="server" id="lblMsg" style="color: Red;">
                </label>
            </div>
            <div style="border: 1px solid #e0e0e0; padding: 10px; margin-top: 5px;height:120px;width:695px">
                选择月份：
                <asp:TextBox Width="100px" ID="txtMonth" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM',minDate:'2008-2',maxDate:'%y-%M-%d'})"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCreate" runat="server" Text="生成考勤记录" Width="100px" OnClick="Unnamed1_Click"
                    CausesValidation="False" /><br /> <br /><br /><br />
                <label runat="server" id="Label2" style="color: Red;">
                    (注意：只有在导入考勤Excel文件后才可以执行此命令，并且每月只能执行一次)</label>
            </div>
        </div>
    </div>
    <div class="foot">
        <uc2:Bottom ID="Bottom1" runat="server" />
    </div>
    <!--发表说说-->
    <div class="divSayAlert" id="divSayAlert" style="display: none;">
        <span class="span">发表说说</span><a href="javascript:divSayAlertDisplay('none');" class="aright"><img
            src="Work/Images/close.jpg" /></a>
        <div class="divText">
            <textarea id="txtSay" class="text" onkeyup="setTextareaLength(this.id,'spanSayLenth',250)"></textarea>
        </div>
        <span class="span">还能输入<span id="spanSayLenth">250</span>字</span><a href="javascript:publishSayDiv()"
            class="aright"><img src="Work/images/send.jpg" /></a>
    </div>
    </form>
</body>
</html>
