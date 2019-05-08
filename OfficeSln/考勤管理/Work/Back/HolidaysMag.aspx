<%@ Page Title="" Language="C#" MasterPageFile="~/Work/Back/MasterPage.master" AutoEventWireup="true"
    CodeFile="HolidaysMag.aspx.cs" Inherits="Work_Back_HolidaysMag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
             window.history.forward(1); 
            SetShowNav(4);
            var hidID = $("#hidID").val();
            if (hidID != 0) {
                $("#userCode").prop("disabled", true);
                $("#holidayType").prop("disabled", true);
                $("#dayCount").attr("readonly", "readonly");
            } 
        });

        function btnSavaHoliday() {
            var userCode = $("#userCode").val();
            var holidayType = $("#holidayType").val();
            var dayCount = $("#dayCount").val();
            var startTime = $("#startTime").val();
            var endTime = $("#endTime").val();
            var remark = $("#remark").text();
            var hidID = $("#hidID").val();
            var res = "";
            if (hidID == 0) {
                res = "holidayMag";
              }else {
                res = "holidayUpadte";
}
            if (holidayType != 0) {
                if (dayCount.length > 0 && startTime.length > 0 && endTime.length > 0) {
                    $.post("UserManger.ashx?" + Math.random(), { "op": res, "userCode": userCode, "holidayType": holidayType, "dayCount": dayCount, "startTime": startTime, "endTime": endTime, "remark": remark, "hidID": hidID }, function (msg) {
                        if (msg == "true") {
                            alert("保存成功！");
                            window.location.href = "HolidaysList.aspx";
                        } else {
                            alert(msg);
                        }
                    })
                } else {
                    alert("请输入完整信息！");
                }
            } else {
                alert("请选择假期类型！");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 40px">
        <span style="font-family: 楷体; font-size: 17px; font-weight: 600;">假期管理</span><br />
        <img src="../Images/u112_line.png" style="width: 800px;" /><br />
        <input type="hidden" id="hidID" value="<%=holidayID %>" />
    </div>
    <ul class="bulletin" style="margin: 30px; width: 700px;">
        <li style="margin-left: 30px; font-family: 宋体; font-size: 12px;"><span>员工名：</span>
            <select id="userCode" name="userCode" style="width: 110px; height: 23px; margin-left: 10px;">
                <%=PublicMethod.UserInfoList(userCode) %>
            </select>
            <span style="margin-left: 45px;">假期类型：</span>
            <select id="holidayType" name="holidayType" style="width: 110px; height: 23px;">
                <%=PublicMethod.HoildayTypeList(typeID) %>
            </select>
            <span style="margin-left: 45px;">天数：</span>
            <input type="text" name="name" style="width: 110px; height: 21px;" id="dayCount" value="<%=day%>" />
        </li>
        <li style="margin-left: 30px;"><span>假期时间：</span>
            <input type="text" id="startTime" class="Wdate" style="width: 170px;" onfocus="WdatePicker({isShowClear:true,dateFmt:'yyyy-MM-dd HH:mm:ss'})" value="<%=startTime %>" />
            —
            <input type="text" class="Wdate" id="endTime" style="width: 170px;" onfocus="WdatePicker({isShowClear:true,dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                value="<%=endTime %>" />
        </li>
        <li style="margin-left: 30px;"><span>备注：</span> </li>
        <li>
            <textarea id="remark"  name="txtContent" style="width: 88%; height: 201px;
                margin-left: 30px;"><%=remark %></textarea></li>
        <li style="margin-left: 30px;">
            <p style="margin-left: 103px;">
                <input type="button" class="cancel" value="保 存"  onclick="btnSavaHoliday()" />
                <input type="button" class="cancel" value="取 消" onclick="window.location.href='HolidaysList.aspx'" />
            </p>
        </li>
    </ul>
</asp:Content>
