<%@ Page Title="编辑下载中心" Language="C#" MasterPageFile="~/Work/Back/MasterPage.master" AutoEventWireup="true" CodeFile="EditDownloadCenter.aspx.cs" Inherits="Work_Back_EditDownloadCenter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        var maxsize = 2 * 1024 * 1024; //2M
        var errMsg = "上传的附件文件不能超过2M！！！";
        var tipMsg = "您的浏览器暂不支持计算上传文件的大小，确保上传文件不要超过2M，建议使用IE、FireFox、Chrome浏览器。";
        function Check() {
//            var issueName = document.getElementById('<%=txtTitle.ClientID %>').value;
//            if (issueName.length == 0) {
//                alertMess("请输入标题");
//                $("#" + '<%=txtTitle.ClientID %>').focus();
//                return false;
//            }

            return true;
        }
        function alertMess(str) {
            //            document.getElementById("divMessage").innerHTML = str;
            //            document.getElementById("pnl11").style.display = "block";
            alert(str);
        }

        $(document).ready(function () {
            SetShowNav(2);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <img id="tempimg" dynsrc="" src="" style="display:none" />
    <ul class="bulletin" style="padding-top: 22px;">
    编辑下载中心
    </ul>
    <ul class="bulletin">
        <li><span>标题：<span style="color: Red;">*</span></span>
            <asp:TextBox runat="server" ID="txtTitle" CssClass="medium1_input" MaxLength="100"></asp:TextBox>&nbsp;&nbsp;<span style="color: red;font-weight: inherit;">标题为空时自动获取上传的文件名为标题</span>
        </li>
        <li><span>文件：<span style="color: Red;">*</span></span><%--accept="application/msword,application/pdf,application/vnd.ms-powerpoint,text/plain,application/vnd.ms-excel,aplication/zip"--%>
        <div style="margin-left: 90px;"><asp:Literal runat="server" ID="litFile"></asp:Literal><asp:FileUpload ID="FileUpload1" accept="application/vnd.ms-excel,application/vnd.ms-excel.12,application/msword,application/vnd.ms-word.document.12,application/pdf,application/x-zip-compressed,application/octet-stream,application/vnd.ms-powerpoint,application/vnd.ms-powerpoint.presentation.12,text/plain" runat="server"  /><asp:Literal runat="server" ID="litType"></asp:Literal></div>
        </li>
        <li>
            <p style="margin-left: 103px;">
                <asp:Button runat="server" ID="btnSave" CssClass="submit" OnClientClick="return Check()" Text="保存" OnClick="btnSave_Click" />
                <input type="button" class="cancel" value="取消" onclick="window.location.href='DownloadCenterList.aspx'"/>
            </p>
        </li>
    </ul>
</asp:Content>

