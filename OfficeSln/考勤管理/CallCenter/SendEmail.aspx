<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendEmail.aspx.cs" Inherits="CallCenter_SendEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>一键发送客户邮件</title>
    <script src="/Work/scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-weight:bold">
    </div>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="发正式邮件" />
    &nbsp;服务月份<asp:TextBox ID="txtDateForZS" runat="server" class="Wdate" onfocus="WdatePicker({readOnly:true})" ToolTip="指定要发送月份的任意一天即可" Text=""></asp:TextBox>
    &nbsp;抄送：<asp:TextBox ID="txtDateForZSCC" runat="server"   Text="yangchao@hyitech.com"></asp:TextBox>
    &nbsp;密送：<asp:TextBox ID="txtDateForZSBCC" runat="server"  Text=""></asp:TextBox>
    <p>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="发测试邮件" />
          &nbsp;服务月份<asp:TextBox ID="txtDateForCS" runat="server" class="Wdate" onfocus="WdatePicker({readOnly:true})" ToolTip="指定要发送月份的任意一天即可" Text=""></asp:TextBox>
        &nbsp;抄送：<asp:TextBox ID="txtDateForCSCC" runat="server"  Text="yangchao@hyitech.com"></asp:TextBox>
    &nbsp;密送：<asp:TextBox ID="txtDateForCSBCC" runat="server"  Text=""></asp:TextBox>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" 
            Text="发测试邮件(群发)" />   &nbsp;服务月份<asp:TextBox ID="txtDateForQFS" runat="server" class="Wdate" onfocus="WdatePicker({readOnly:true})" ToolTip="指定要发送月份的任意一天即可" Text=""></asp:TextBox>
        &nbsp;抄送：<asp:TextBox ID="txtDateForQFSCC" runat="server"  Text="yangzhisong@hyitech.com"></asp:TextBox>
    &nbsp;密送：<asp:TextBox ID="txtDateForQFSBCC" runat="server"  Text=""></asp:TextBox>
    </p>
    </form>
</body>
</html>
