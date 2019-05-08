<%@ Page Title="" Language="C#" MasterPageFile="~/Work/Back/MasterPage.master" AutoEventWireup="true"
    CodeFile="HolidayStatistics.aspx.cs" Inherits="Work_Back_HolidayStatistics" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            window.history.forward(1);
            SetShowNav(4);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 40px; width: 863px;">
        <span style="font-family: 楷体; font-size: 17px; font-weight: 600;">假期统计</span><br />
        <img src="../Images/u112_line.png" style="width: 866px;" /><br />
    </div>
    <ul class="transaction_search" style="padding-left: 10px; width: 855px; height: 60px">
        <li style="width: 800px;">
            <p style="width: 230px; margin-left: 20px; float: left;">
                <span style="font-size:13px;">员工姓名：</span>
                <select style="width:120px;height:23px;" name="slectCode">
                   <%=PublicMethod.UserInfoList(userCode)%>
                </select></p>
          
            <p style="float: right; padding-right: 10px; width: 220px; margin-left: 220px;">
                <input type="button" value="搜索" style="width: 80px; height: 30px;"  onclick="$('#form1').submit()"/></p>
        </li>
    </ul>
    <div class="transaction_list" style="width: 866px">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odd-even">
            <thead>
                <tr>
                    <th width="10%" align="center">
                        序号
                    </th>
                    <th width="18%" height="40" align="center">
                        员工名
                    </th>
                    <th width="13%" align="center">
                        年假(天)
                    </th>
                    <th width="13%" align="center">
                        加班(天)
                    </th>
                    <th width="13%" align="center">
                        调休(天)
                    </th>
                    <th width="13%" align="center">
                        已用年假(天)
                    </th>
                    <th align="center">
                        可用假期(天)
                    </th>
                </tr>
            </thead>
            <tbody>
                <%=holidayList%>
            </tbody>
        </table>
    </div>
    <div class="ad_foot" style="width: 866px;">
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
