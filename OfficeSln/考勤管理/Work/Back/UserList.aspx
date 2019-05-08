<%@ Page Title="" Language="C#" MasterPageFile="~/Work/Back/MasterPage.master" AutoEventWireup="true"
    CodeFile="UserList.aspx.cs" Inherits="Work_Back_UserList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="../Style/pager.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
      <%--.dv input{ width: 105px; height: 23px;line-height: 25px;vertical-align: middle;margin-left:20px;}
        -- .select{ width: 85px; height: 23px;line-height: 25px;vertical-align: middle; }--%>
        #btnSerch{margin-left:30px;height:26px;width:80px;}
        .tb  td{border:1px solid #aac7cc;line-height:22px; color:#323232;font-size:12px; padding:2px;padding-left:4px;}
        .tb  th{border:1px solid #aac7cc;line-height:22px; color:#323232;font-size:12px; padding:2px;padding-left:4px;}
    </style>
    <script type="text/javascript">
        $(function () {
            $("#tbody tr").mouseenter(function () {
                $(this).css("background-color", "#E9F5FE");
            }).mouseleave(function () {
                $(this).css("background-color", "");
            });
                SetShowNav(3);
        })
        function delUser(id) {
            if (confirm('确实要禁用该用户吗?')) {
                $.get("UserManger.ashx", { "op": "del", "userID": id }, function (data) {
                    if (data == 1) {
                        location.href = "UserList.aspx";
                    } else {
                        alert(data);
                    }
                });
            }
            else return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 40px">
        <span style="font-family: 楷体; font-size: 17px; font-weight: 600;">员工管理</span><br />
        <img src="../Images/u112_line.png" style="width: 1000px;" /><br />
    </div>
    <div>
        <div class="dv">
            <%--    <label>用户名:</label><input type="text" name="userName"  value="<%=userName %>" /> 
        <label style="margin-left:50px;">真实姓名:</label><input type="text" name="trueName"  value="<%=trueName %>"  />
        <label style="margin-left:50px; ">部门:</label>
        <select class="select">
            <%=PublicMethod.DepartmentList(DepartmentId)%>
        </select>
        <input type="submit" name="name" value="搜 索" id="btnSerch" /><br />--%>
            <span>用户名：</span>
            <asp:TextBox runat="server" ID="txtUsere" CssClass="short_input" Style="width: 120px;
                margin-left: 10px; margin-right: 10px;" MaxLength="20"></asp:TextBox>&nbsp;&nbsp;&nbsp;
            <span style="margin-left:30px;">真实姓名：</span>
            <asp:TextBox runat="server" ID="txtTrueName" CssClass="short_input" Style="width: 120px;
                margin-left: 10px; margin-right: 10px;" MaxLength="20"></asp:TextBox>&nbsp;&nbsp;&nbsp;
            <span style="margin-left:30px;">所属部门：</span>
            <asp:DropDownList runat="server" ID="ddlDep">
            </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" ID="btnSearch" CssClass="cancel" Text="搜索" Width="60px"
                Height="25px" OnClick="btnSearch_Click" />
        </div>
        <table class="tb" style="width: 1000px; margin-top: 20px; border-collapse: collapse;">
            <thead>
                <tr>
                    <th align="center">
                        序号
                    </th>
                    <th align="center">
                        用户名
                    </th>
                    <th align="center">
                        姓名
                    </th>
                    <th align="center">
                        职位
                    </th>
                    <th align="center">
                        部门
                    </th>
                    <th align="center">
                        管理员
                    </th>
                    <th align="center">
                        状态
                    </th>
                    <th align="center">
                        入职时间
                    </th>
                    <th align="center">
                        电话
                    </th>
                    <th align="center">
                        邮箱
                    </th>
                    <th align="center">
                        操作
                    </th>
                </tr>
            </thead>
            <tbody id="tbody">
                <%=UserList%>
            </tbody>
        </table>
    </div>
    <div class="ad_foot" style="width:900px;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="" CustomInfoClass=""
            FirstPageText="&lt;&lt;" LastPageText="&gt;&gt;" NextPageText="&gt;" PrevPageText="&lt;"
            LayoutType="Table" CssClass="paginator" CurrentPageButtonClass="cpb" CurrentPageButtonStyle="border:1px #B7D3E8 solid;background:#3E74B0;"
            CustomInfoTextAlign="Center" ShowCustomInfoSection="Left" ShowPageIndexBox="Never"
            SubmitButtonText="" TextAfterPageIndexBox="" TextBeforePageIndexBox="" SubmitButtonClass=""
            OnPageChanged="AspNetPager1_PageChanged" CustomInfoSectionWidth="" PageIndexBoxType="TextBox"
            PageSize="15">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>
