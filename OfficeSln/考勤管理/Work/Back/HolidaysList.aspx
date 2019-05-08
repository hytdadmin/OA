<%@ Page Title="" Language="C#" MasterPageFile="~/Work/Back/MasterPage.master" AutoEventWireup="true" CodeFile="HolidaysList.aspx.cs" Inherits="Work_Back_HolidaysList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            window.history.forward(1); 
            SetShowNav(4);
        });
        
        function deleteHolidays(id) {
            if (confirm("确定删除吗？")) {
                $.ajax({
                    type: "get",
                    url: "HolidayMag.ashx",
                    data: { "op": "delHoliday", "hodId": id },
                    cache: false,
                    dataType: "text",
                    success: function (msg) {
                        if (msg == "true") {
                            location.href = "HolidaysList.aspx";
                        } else {
                            alert(msg);
                        }
                    }
                })
            } 
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div style="height: 40px; width: 863px;">
        <span style="font-family: 楷体; font-size: 17px; font-weight: 600;">假期列表</span><br />
        <img src="../Images/u112_line.png" style="width: 866px;" /><br />
    </div>
     <ul class="transaction_search" style="padding-left: 10px;width: 855px;">
        <li>
            <span>员工姓名：</span>
            <asp:TextBox runat="server" ID="txtUserName"  CssClass="short_input" style="width:120px; margin-left:10px;margin-right:10px;" MaxLength="20"></asp:TextBox>&nbsp;&nbsp;&nbsp;
            <span>假期类型：</span><asp:DropDownList ID="HolidaysType" runat="server">
            </asp:DropDownList>
&nbsp;</li>
        <li><span>假期时间：</span>
        <asp:TextBox runat="server" ID="txtStartDate" style=" margin-left:10px;width:140px;" onfocus="WdatePicker({isShowClear:true})" class="Wdate"></asp:TextBox>
            &nbsp;&nbsp;—&nbsp;&nbsp;
       <asp:TextBox Width="140px" runat="server" ID="txtEndDate" onfocus="WdatePicker({isShowClear:true})" class="Wdate"></asp:TextBox>
       <div style="float:right;padding-right: 10px;"><asp:Button runat="server" 
               ID="btnSearch" CssClass="cancel" Text="搜索" onclick="btnSearch_Click"   />
            <input type="button" class="cancel" id="btnAdd" onclick='location.href = "HolidaysMag.aspx"' value="添加假期"/>
            </div></li>
    </ul>
     <div class="transaction_list" style="width:866px">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odd-even">
            <thead>
                <tr>
                    <th width="5%" align="center">
                        序号
                    </th>
                    <th width="8%" align="center">
                        员工名
                    </th>
                    <th width="10%" align="center">
                        假期类型
                    </th>
                    <th width="15%" align="center">
                        开始日期
                    </th>
                    <th width="15%" align="center">
                        结束日期
                    </th>
                    <th width="8%" align="center">
                        天数
                    </th>
                    <th  align="center">
                        备注说明
                    </th>
                    <th width="10%" align="center">
                        操作
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server"  ID="RepeaterHoliday" 
                    onitemcommand="RepeaterHoliday_ItemCommand">
                    <ItemTemplate>
                      <tr class='<%#(Container.ItemIndex%2==0)?"even":"odd"%>'>
                       <td align="center">  
                            <asp:CheckBox ID="che" name="showIsCheck" CssClass="che1" runat="server" ToolTip='<%#Eval("ID") %>' Visible="false"  /> <%#(Container.ItemIndex+1)%>
                            </td>
                            <td align="center">
                               <%#Eval("UserName")%>
                            </td>
                            <td align="center">
                               <%#HYTD.Common.PublicEnum.GetEnumDescription<HYTD.Common.PublicEnum.HolidaysType>(Eval("HolidaysType").ToString()) %>
                            </td>
                            <td align="center">
                               <%# Convert.ToDateTime(Eval("startTime")).ToString("yyyy-MM-dd HH:mm:ss")%>
                            </td>
                             <td align="center">
                               <%#Convert.ToDateTime(Eval("endTime")).ToString("yyyy-MM-dd HH:mm:ss")%>
                            </td>
                            <td align="center">
                               <%# Convert.ToDouble(Eval("DayCount")) < 0 ? Convert.ToDouble(Eval("DayCount")) * -1 : Eval("DayCount")%>
                            </td>
                            <td align="center">
                               <%#Eval("Remark").ToString().Length > 50 ? Eval("Remark").ToString().Substring(0, 50) : Eval("Remark")%>
                            </td>
                            <td align="center">
                              <div>
                            <li style="float:left;width:39px;"><a href='HolidaysMag.aspx?op=update&id=<%#Eval("id") %>' class="buttonLink">修改</a>&nbsp;</li>
                            <li style="float:left;width:39px;">
                            <a href="#" class="buttonLink" onclick="deleteHolidays('<%#Eval("ID") %>')">删除</a>&nbsp;</li>
                            </div>
                            </td>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <div class="ad_foot" style="width:866px;" >
       <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CustomInfoHTML="" CustomInfoClass=""
            FirstPageText="&lt;&lt;" LastPageText="&gt;&gt;" NextPageText="&gt;" PrevPageText="&lt;"
            LayoutType="Table" CssClass="paginator" CurrentPageButtonClass="cpb" CurrentPageButtonStyle="border:1px #B7D3E8 solid;background:#3E74B0;"
            CustomInfoTextAlign="Center" ShowCustomInfoSection="Left" ShowPageIndexBox="Never"
            SubmitButtonText="" TextAfterPageIndexBox="" TextBeforePageIndexBox="" SubmitButtonClass=""
            OnPageChanged="AspNetPager1_PageChanged" CustomInfoSectionWidth="" PageIndexBoxType="TextBox"
            PageSize="15">
        </webdiyer:AspNetPager>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

