<%@ Page Title="公告管理" Language="C#" MasterPageFile="~/Work/Back/MasterPage.master" AutoEventWireup="true" CodeFile="BulletinList.aspx.cs" Inherits="Work_Back_BulletinList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="../../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="../style/pager.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //比较开始结束日期
        function CheckCompareTime() {
            var s = document.getElementById("<%=txtStartDate.ClientID %>").value;
            var e = document.getElementById("<%=txtEndDate.ClientID %>").value;
            return CompareTimes(s, e);
        }
        $(document).ready(function () {
            SetShowNav(1);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <asp:UpdatePanel runat="server" ID="updatePanle1">
            <ContentTemplate>
    <ul class="transaction_search" style="padding-left: 10px;width: 755px;">
        <li>
            <span>发布标题：</span>
            <asp:TextBox runat="server" ID="txtTitle"  CssClass="short_input" style="width:120px; margin-left:10px;margin-right:10px;" MaxLength="20"></asp:TextBox>&nbsp;&nbsp;&nbsp;
            <span>发布部门：</span>
            <asp:DropDownList runat="server" id="ddlDep">
            </asp:DropDownList>
        </li>
        <li><span>添加时间：</span>
        <asp:TextBox runat="server" ID="txtStartDate" style=" margin-left:10px;width:120px;" onfocus="WdatePicker({isShowClear:true})" class="Wdate"></asp:TextBox>
            &nbsp;&nbsp;-&nbsp;&nbsp;
       <asp:TextBox Width="120px" runat="server" ID="txtEndDate" onfocus="WdatePicker({isShowClear:true})" class="Wdate"></asp:TextBox>
       <div style="float:right;padding-right: 10px;"><asp:Button runat="server" ID="btnSearch" CssClass="cancel" Text="搜索" 
            onclick="btnSearch_Click" OnClientClick="return  CheckCompareTime();" />
            <input type="button" class="cancel" id="btnAdd" onclick='location.href = "EditBulletin.aspx"' value="添加公告"/>
            <asp:Button runat="server" ID="btnDel" CssClass="cancel" Text="批量删除" 
            onclick="btnDel_Click"/></div></li>
    </ul>
    <div class="transaction_list">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odd-even">
            <thead>
                <tr>
                    <th width="5%" align="center">
                        <input type="checkbox"  onclick="return selectall(this)"  ToolTip="全选" />
                    </th>
                    <th width="3%" align="left">
                        ID
                    </th>
                    <th align="left">
                        标题
                    </th>
                    <th width="12%" align="center">
                        发布部门
                    </th>
                    <th width="10%" align="center">
                        发布人
                    </th>
                    <th width="10%" align="center">
                        发布时间
                    </th>
                    <th width="10%" align="center">
                        操作
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="repShow_ItemCommand">
                    <FooterTemplate>
                <tr class="even" style="display: <%#bool.Parse((Repeater1.Items.Count==0).ToString())?"table-row":"none"%>;">
                    <td colspan="7" align="center"><asp:Image ID="Image1" ImageUrl="../Images/information.png" runat="server" CssClass="result_info_img_p" style="vertical-align: middle;"
                               /><asp:Label ID="lblEmptyZP"
                                    Text="结果为空" runat="server"></asp:Label></td></tr>
                    </FooterTemplate>
                    <ItemTemplate>
                        <tr class='<%#(Container.ItemIndex%2==0)?"even":"odd"%>'>
                            <td align="center">
                                <asp:CheckBox ID="che" name="showIsCheck" CssClass="che1" runat="server" ToolTip='<%#Eval("id") %>' />
                            </td>
                            <td align="left">
                                <%#Container.ItemIndex+1 %></td>
                            <td align="left">
                                 <%#Eval("title")%>
                            </td>
                            <td align="center">
                                <%#Eval("PubDep")%>
                            </td>
                            <td align="center">
                                <%#Eval("PubName")%>
                            </td>
                            <td align="center">
                                <%#Convert.ToDateTime(Eval("PublishTime")).ToString("yyyy-MM-dd")%>
                            </td>
                            <td align="center">
                            <div>
                            <li style="float:left;width:29px;"><a href='EditBulletin.aspx?id=<%#Eval("id") %>' class="buttonLink">修改</a>&nbsp;</li>
                            <li style="float:left;width:29px;"><asp:LinkButton runat="server" CssClass="buttonLink" Text="删除" ID="lbtnDel" OnClientClick="return confirm('确定要删除吗?');" CommandName="delete"></asp:LinkButton>&nbsp;</li>
                            </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <div class="ad_foot" >
       <webdiyer:aspnetpager ID="AspNetPager1" runat="server" 
          CustomInfoHTML="" 
          CustomInfoClass="" FirstPageText="&lt;&lt;" 
          LastPageText="&gt;&gt;" NextPageText="&gt;" PrevPageText="&lt;"
          LayoutType="Table" 
          CssClass="paginator" CurrentPageButtonClass="cpb" 
          CurrentPageButtonStyle="border:1px #B7D3E8 solid;background:#3E74B0;" CustomInfoTextAlign="Center" 
          ShowCustomInfoSection="Left" ShowPageIndexBox="Never" SubmitButtonText="" 
          TextAfterPageIndexBox="" 
          TextBeforePageIndexBox="" SubmitButtonClass="" onpagechanged="AspNetPager1_PageChanged" 
                          CustomInfoSectionWidth="" PageIndexBoxType="TextBox"></webdiyer:aspnetpager></div>
            
                  </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>

