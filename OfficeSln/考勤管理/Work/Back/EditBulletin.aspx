<%@ Page Title="编辑公告" Language="C#" MasterPageFile="~/Work/Back/MasterPage.master" AutoEventWireup="true" CodeFile="EditBulletin.aspx.cs" Inherits="Work_Back_EditBulletin" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script type="text/javascript" src='<%=ResolveUrl("~/xhEditor/xheditor-1.2.1.min.js") %>'></script>
<script type="text/javascript" src='<%=ResolveUrl("~/xhEditor/xheditor_lang/zh-cn.js") %>'></script>

    <script type="text/javascript">
        $(function () {
            $('#' + '<%=elm1.ClientID %>').xheditor({ upLinkUrl: "../../upload.aspx", upLinkExt: "zip,rar,txt", upImgUrl: "../../upload.aspx", upImgExt: "jpg,jpeg,gif,png", upFlashUrl: "../../upload.aspx", upFlashExt: "swf", upMediaUrl: "../../upload.aspx", upMediaExt: "avi" });
        })
    </script>
    <script type="text/javascript">
        function Check() {
            var issueName = document.getElementById('<%=txtTitle.ClientID %>').value;
            if (issueName.length == 0) {
                alertMess("请输入公告标题");
                $("#"+'<%=txtTitle.ClientID %>').focus();
                return false;
            }
            return true;
        }
        function alertMess(str) {
//            document.getElementById("divMessage").innerHTML = str;
            //            document.getElementById("pnl11").style.display = "block";
            alert(str);
        }

        $(document).ready(function () {
            SetShowNav(1);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ul class="bulletin" style="padding-top: 22px;">
    编辑公告
    </ul>
    <ul class="bulletin">
        <li><span>标题：<span style="color: Red;">*</span></span>
            <asp:TextBox runat="server" ID="txtTitle" CssClass="medium1_input" MaxLength="100"></asp:TextBox>
        </li>
        <li><span>内容：<span style="color: Red;">*</span></span> </li>
        <li>
      <textarea id="elm1" runat="server" name="txtContent" rows="12" cols="80" style="width: 90%; height:280px;"></textarea></li>
        <li>
            <p style="margin-left: 103px;">
                <asp:Button runat="server" ID="btnSave" CssClass="submit" OnClientClick="return Check()" Text="保存" OnClick="btnSave_Click" />
                <input type="button" class="cancel" value="取消" onclick="window.location.href='BulletinList.aspx'"/>
            </p>
        </li>
    </ul>
</asp:Content>
