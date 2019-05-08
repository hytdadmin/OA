<%@ Page Title="" Language="C#" MasterPageFile="~/Work/Back/MasterPage.master" AutoEventWireup="true"
    CodeFile="UpdateUser.aspx.cs" Inherits="Work_Back_UpdateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        .tab
        {
            width: 800px;
            margin-top: 10px;
            margin-bottom: 10px;
            height: 350px;
        }
        .tab label
        {
            width: 100px;
            font-size: 15px;
        }
        .tab input
        {
            width: 200px;
            font-size: 15px;
            height: 23px;
            line-height: 23px;
            vertical-align: middle;
        }
        #btnUpdate input
        {
            width: 100px;
            font-size: 15px;
            height: 27px;
        }
    </style>
    <script type="text/javascript">
        function btnUpdate() {
            var userCode = $("#userCode").val();
            var userName = $("#userName").val();
            var isAdmin = $("input[name='rdAdmin']:checked").val();
            var tel = $("#tel").val();
            var email = $("#email").val();
            var seletSatus = $("#seletSatus").val();
            var seletDepartment = $("#seletDepartment").val();
            var selectPosition = $("#selectPosition").val();
            var entyTime = $("#entyTime").val();
            $.post("UserManger.ashx?op=update&uid=<%=user.UserID%>", { "userCode": userCode, "userName": userName, "isAdmin": isAdmin, "tel": tel, "email": email, "seletSatus": seletSatus, "seletDepartment": seletDepartment,"selectPosition": selectPosition,"entyTime":entyTime }, function (msg) {
                //1修改成功 2修改失败
                if (msg == 1) {
                    alert("修改成功！");
                    window.location.href = "UserList.aspx";
                } else
                    alert(msg);
            });
        }
        function btnCanel() {
            window.location.href = "UserList.aspx";
        }

        $(document).ready(function () {
            SetShowNav(3);
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 40px">
        <span style="font-family: 楷体; font-size: 17px; font-weight: 600;">修改员工信息</span><br />
        <img src="../Images/u112_line.png" style="width: 800px;" /><br />
    </div>
    <div>
        <table class="tab">
            <tr>
                <td align="center">
                    <label>
                        用户名:</label>
                </td>
                <td>
                    <input type="text" value="<%=user.UserCode %>" id="userCode" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <label>
                        姓 名:</label>
                </td>
                <td>
                    <input type="text" id="userName" value="<%=user.UserName %>" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <label>
                        管理员:</label>
                </td>
                <td>
                   <%=isAdmin %>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <label>
                        电 话:</label>
                </td>
                <td>
                    <input type="text" id="tel" value="<%=user.Tel %>" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <label>
                        邮 箱:</label>
                </td>
                <td>
                    <input type="text" id="email" value="<%= user.Email %>" />
                </td>
            </tr>
             <tr>
                <td align="center">
                    <label>
                        入职时间:</label>
                </td>
                <td>
                    <input id="entyTime" class="Wdate" type="text" style="width:200px;" onfocus="WdatePicker({isShowClear:true})" name="entyTime" value="<%=EntyTime %>" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <label>
                        状 态:</label>
                </td>
                <td>
                    <select style="height: 23px;" id="seletSatus">
                        <%=PublicMethod.PublicStatus(Convert.ToInt32(user.UserStatus))%>
                    </select>
                </td>
            </tr>
              <tr>
                <td align="center">
                    <label>
                        职 位:</label>
                </td>
                <td>
                     <select style="height: 23px;" id="selectPosition">
                        <%=PublicMethod.PositionList(Convert.ToInt32(user.PosiId))%>
                    </select>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <label>
                        所属部门:</label>
                </td>
                <td>
                    <select style="height: 23px;" id="seletDepartment">
                        <%=PublicMethod.DepartmentList(Convert.ToInt32(user.DepartmentId)) %>
                    </select>
                </td>
            </tr>
            <tr id="btnUpdate">
                <td align="center">
                </td>
                <td>
                    <input type="button" name="name" value="修 改" onclick="btnUpdate()" />
                    <input type="button" name="name" value="取 消" onclick="btnCanel()" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
