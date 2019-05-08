<%@ Page Title="" Language="C#" MasterPageFile="~/Work/Back/MasterPage.master" AutoEventWireup="true"
    CodeFile="PositionList.aspx.cs" Inherits="Work_Back_PositionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            window.history.forward(1);
            SetShowNav(3);
        });

        function UpdatePosit(i) {
            $(".spvon .cl_sp").eq(i).hide()
            $(".spvon .cl_inp").eq(i).show();
            $(".spvon .cl_block").eq(i).hide()
            $(".spvon .cl_none").eq(i).show();

        }
        function CanclePosit(i) {
            $(".spvon .cl_sp").eq(i).show()
            $(".spvon .cl_inp").eq(i).hide();
            $(".spvon .cl_block").eq(i).show()
            $(".spvon .cl_none").eq(i).hide();

        }
        function SavePosit(i, id) {
            var value = $(".spvon .cl_inp").eq(i).find("input").val();
            if (value.length > 0) {
                $.ajax({
                    type: "get",
                    url: "UserManger.ashx",
                    data: { "op": "setPosit", "val": value, "PosId": id },
                    dataType: "text",
                    cache: false,
                    success: function (msg) {
                        if (msg == "true") {
                            alert("修改成功！");
                            location.href = "PositionList.aspx";
                        } else {
                            alert("修改失败!");
                        }
                    }
                })
            } else {
                alert("职称名不能为空！");
            }
        }
        //删除
        function delPosition(Pid) {
            if (confirm("是否确定删除？")) {
                $.ajax({
                    type: "get",
                    url: "UserManger.ashx",
                    data: { "op": "delPosit", "PosId": Pid },
                    dataType: "text",
                    cache: false,
                    success: function (msg) {
                        if (msg == "true") {
                            alert("删除成功！");
                            location.href = "PositionList.aspx";
                        } else {
                            alert("删除失败!");
                        }
                    }
                })
            }
        }
        //添加只为名
        function btnAddPName() {
            var pName = $("#PName").val();
            if (pName.length>0) {
                $.ajax({
                    type: "get",
                    url: "UserManger.ashx",
                    dataType: "text",
                    cache: false,
                    data: { "op": "addPosit", "pName": pName },
                    success: function (msg) {
                        if (msg == "true") {
                            alert("添加成功！");
                            location.href = "PositionList.aspx";
                            return true;
                        } else if (msg=="same") {
                            alert("职位名已存在!");
                            return false;
                        } else {
                            alert("添加失败!");
                            return false;
                        }
                    }
                })
            } else {
                alert("请输入完整信息!");
                return false;
            }

        }

        function btnSubmit() {
            $("#hidPosit").val('1');
            $('#form1').submit();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="hidden" name="hidPosit" value=""  id="hidPosit" />
    <div style="height: 40px">
        <span style="font-family: 楷体; font-size: 17px; font-weight: 600;">员工管理</span><br />
        <img src="../Images/u112_line.png" style="width: 660px;" /><br />
    </div>
    <div class="search">
        <h2>
            查询条件</h2>
        <input type="hidden" name="userID" value="" />
        <input type="hidden" name="status" value="" id="status" />
        <dl class="search_1">
            <dt>职位名称</dt>
            <dd>
                <input type="text" value="<%=pName %>" name="PositName" /></dd>
        </dl>
        <div class="search_but" style="width: 304px;">
            <a href="#" onclick="$('#form1').submit();" style="float: left;">
                <img src="../../img/sp2.jpg" /></a> 
                <a href="#" onclick="jDivShow('dv_Posit','添加职位名',function(){return btnAddPName()})" style="float: left;
                    margin-left: 50px;">
                    <img src="../../img/tj.jpg" /></a>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="spvon" style="width: 620px;">
        <table width="620" border="0" cellspacing="0" cellpadding="0">
            <tr bgcolor="#3a93cf">
                <td height="40" align="center" valign="middle">
                    <span class="white">序号</span>
                </td>
                <td align="center" width="361">
                    <span class="white">职位名称</span>
                </td>
                <td align="center">
                    <span class="white">操作</span>
                </td>
            </tr>
            <%=positList%>
        </table>
        <%=PageList%>
    </div>
    <!--添加信息-->
    <div style="width: 374px; margin-top: 10px; display: none;" id="dv_Posit">
        <dl class="tcK_1">
            <dt>职位名称：</dt>
            <dd>
                <input type="text" value="" id="PName" maxlength="16" /></dd>
            <div class="clear">
            </div>
        </dl>
    </div>
</asp:Content>
