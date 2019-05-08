<%@ Page Title="" Language="C#" MasterPageFile="~/Work/Back/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddUser.aspx.cs" Inherits="Work_Back_AddUser" %>

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
            var seletDepartment = $("#seletDepartment").val();
            var pwd = $("#pwd").val();
            var newPwd = $("#newPwd").val();
            var selectPosition = $("#selectPosition").val();
            var entyTime = $("#entyTime").val();
            if (pwd.length > 0 && newPwd.length > 0 && userCode.length > 0) {
                if ($.trim(pwd) == $.trim(newPwd)) {
                    $.post("UserManger.ashx?op=add", { "userCode": userCode, "pwd": pwd, "userName": userName, "isAdmin": isAdmin, "tel": tel, "email": email, "seletDepartment": seletDepartment, "selectPosition": selectPosition, "entyTime": entyTime }, function (msg) {
                        //添加成功 2添加失败
                        if (msg == 1) {
                            alert("添加成功！");
                            window.location.href = "UserList.aspx";
                        } else {
                            alert(msg);
                        }
                    });
                } else
                    alert("两次密码输入不一致！");
            } else {
                alert("用户名和密码不能为空！");
            }

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
        <span style="font-family: 楷体; font-size: 17px; font-weight: 600;">添加员工</span><br />
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
                    <input type="text" value="" id="userCode" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <label>
                        密 码:</label>
                </td>
                <td>
                    <input type="password" value="" id="pwd" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <label>
                        确认密码:</label>
                </td>
                <td>
                    <input type="password" value="" id="newPwd" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <label>
                        姓 名:</label>
                </td>
                <td>
                    <input type="text" id="userName" value="" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <label>
                        管理员:</label>
                </td>
                <td>
                    <input type='radio' name='rdAdmin' value='1' id='radio' style='width: 50px;' />是
                    <input type='radio' name='rdAdmin' value='0' id='radio' style='width: 50px;' checked='checked' />否
                </td>
            </tr>
            <tr>
                <td align="center">
                    <label>
                        电 话:</label>
                </td>
                <td>
                    <input type="text" id="tel" value="" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <label>
                        邮 箱:</label>
                </td>
                <td>
                    <input type="text" id="email" value="" />
                </td>
            </tr>
             <tr>
                <td align="center">
                    <label>
                        入职时间:</label>
                </td>
                <td>
                    <input id="entyTime" class="Wdate" type="text" style="width:200px;" onfocus="WdatePicker({isShowClear:true})" name="entyTime" />
                </td>
            </tr>
             <tr>
                <td align="center">
                    <label>
                        职 位:</label>
                </td>
                <td>
                     <select style="height: 23px;" id="selectPosition">
                        <%=PublicMethod.PositionList(Convert.ToInt32(0)) %>
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
                        <%=PublicMethod.DepartmentList(Convert.ToInt32(0)) %>
                    </select>
                </td>
            </tr>
            <tr id="btnUpdate">
                <td align="center">
                </td>
                <td>
                    <input type="button" name="name" value="添 加" onclick="btnUpdate()" />
                    <input type="button" name="name" value="取 消" onclick="btnCanel()" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
