<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerInfo.aspx.cs" Inherits="CallCenter_CustomerInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Work/scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="artDialog4.1.7/artDialog.source.js?skin=blue" type="text/javascript"></script>
    <script src="artDialog4.1.7/plugins/iframeTools.source.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script>

    </script>
</head>
<body>
    <form id="form1" runat="server" action="CallCenter.ashx?option=editcustomer">
    <input type="hidden" name="hdnCID" value="<%=CID %>" />
    <table cellpadding="1px" cellspacing="1px" style="border: 1px solid; width: 1000px;">
        <tr>
            <td colspan="5" style="font-weight: bold; text-align: center">
                客户信息
            </td>
            <td>
                <a href="">历史来电</a>
            </td>
        </tr>
        <tr>
            <td>
                客户名称：
            </td>
            <td colspan="4">
                <input type="text" name="cName" maxlength="50" value="<%=cModel.CC_Name%>" />
            </td>
            <td>
                服务次数：<span style="color: Red"><%=intCounts %></span>
            </td>
        </tr>
        <tr>
            <td>
                隶属工程师：
            </td>
            <td>
                <select id="sltUser" name="sltUser">
                    <%=strForUser%>
                </select>
            </td>
            <td>
                部署时间：
            </td>
            <td>
                <input type="text" name="httime" class="Wdate" onfocus="WdatePicker({readOnly:true})"
                    value="<%=cModel.CC_HTTime%>" />
            </td>
            <td>
                其它联系方式：
            </td>
            <td>
                <input type="text" name="txtOtherTel" maxlength="50" value="<%=cModel.CC_OtherTel%>" />
            </td>
        </tr>
        <tr>
            <td>
                负责人：
            </td>
            <td>
                <input type="text" name="txtuserName" maxlength="50" value="<%=cModel.CC_UserName%>" />
            </td>
            <td>
                负责人电话：
            </td>
            <td>
                <input type="text" name="txtccTel" maxlength="50" value="<%=cModel.CC_Tel%>" />
            </td>
            <td>
                服务期限：
            </td>
            <td>
                <input type="text" class="Wdate" name="txtSSTime" onfocus="WdatePicker({readOnly:true})"
                    value="<%=cModel.CC_ServiceStartTime%>" />-
                <input type="text" class="Wdate" name="txtSETime" onfocus="WdatePicker({readOnly:true})"
                    value="<%=cModel.CC_ServiceEndTime%>" />
            </td>
        </tr>
        <tr>
            <td>
                邮件联系人：
            </td>
            <td>
                <input type="text" name="txtToEmailuserName" maxlength="50" value="<%=cModel.CC_EmailUserName%>" />
            </td>
            <td>
                邮件地址：
            </td>
            <td>
                <input type="text" name="txtToEmail" maxlength="50" value="<%=cModel.CC_Email%>" />
            </td>
            <td>
                邮件抄送地址：
            </td>
            <td>
                <input type="text" name="txtToEmailCCAddr" value="<%=cModel.CC_CCEmail%>" />
            </td>
        </tr>
        <tr>
            <td>
                CA版本：
            </td>
            <td>
                <input type="text" name="txtVistion" value="<%=cModel.CC_Vistion%>" />
            </td>
            <td>
                升级日期：
            </td>
            <td>
                <input type="text" name="txtUpdateTime" class="Wdate" onfocus="WdatePicker({readOnly:true})"
                    value="<%=cModel.CC_UpdateTime%>" />
            </td>
            <td>
                最后来电日期：
            </td>
            <td>
             <input type="text" value="<%=strLastServiceTime%>" disabled="disabled">
            </td>
        </tr>
          <tr>
           <td>
            备注：
            </td>
            <td colspan="5">
            <textarea name="txtRemark" style="height:60px;width:700px;"><%=cModel.CC_Remark%></textarea>
            </td>
        </tr>
        <tr style="display: none">
            <td>
            </td>
            <td colspan="5">
                <input id="Button1" type="submit" value="保存" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
