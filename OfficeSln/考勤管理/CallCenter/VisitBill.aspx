<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VisitBill.aspx.cs" Inherits="CallCenter_VisitBill" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工单回访</title>
        <script src="/Work/scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="artDialog4.1.7/artDialog.source.js?skin=blue" type="text/javascript"></script>
    <script src="artDialog4.1.7/plugins/iframeTools.source.js" type="text/javascript"></script>
    <style type="text/css">
        table
        {
            border-collapse: collapse;
            border: none;
            width: 200px;
        }
        td
        {
            border: solid #000 1px;
        }
        #Text1
        {
            margin-bottom: 0px;
        }
        #Text2
        {
            margin-bottom: 0px;
        }
        #Text3
        {
            margin-bottom: 0px;
        }
        #Text4
        {
            margin-bottom: 0px;
        }
        #Text4
        {
            margin-bottom: 0px;
        }
        #TextArea1
        {
            height: 79px;
            width: 489px;
        }
        #TextArea2
        {
            height: 73px;
            width: 490px;
        }
        #TextArea3
        {
            height: 53px;
            width: 489px;
        }
        #txtDescription
        {
            height: 69px;
            width: 671px;
        }
        #txtSolution
        {
            height: 71px;
            width: 670px;
        }
        #txtRemark
        {
            height: 85px;
            width: 670px;
        }
    </style>
    <script type="text/javascript">
        function ShowWorkBill(wid) {
            art.dialog.open('ServiceHistoryListForCustomer.aspx?wid=' + wid
            ,
            { title: '服务记录',
                // 在open()方法中，init会等待iframe加载完毕后执行
                init: function () {
                    //                    var iframe = this.iframe.contentWindow;
                    //                    var top = art.dialog.top; // 引用顶层页面window对象
                    //                    var username = iframe.document.getElementById('Button1');
                    //                    username.value = 'guest';
                    //                    top.document.title = '测试';
                },
                //                ok: function () {
                //                    var iframe = this.iframe.contentWindow;
                //                    var top = art.dialog.top; // 引用顶层页面window对象
                //                    var username = iframe.document.getElementById('Button1').click();
                //                    username.click();
                //                },
                cancel: true
            , width: 620, height: 530
            }, false);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" action="CallCenter.ashx?option=edithf&hf=<%=HuiFang %>">
     <input type="hidden" name="hdnHF" value="<%=HuiFang%>" />
    <input type="hidden" name="hdnWID" value="<%=WID%>" />
     <input type="hidden" name="hdnVID" value="<%=vModel.CVB_ID%>" />
    <input type="hidden" name="hdnCCID" value="<%=cModel.CC_ID%>" />
    <input type="hidden" name="htnCreater" value="<%=intCreater%>" />
    <input type="hidden" name="sltUser" value="<%=intForUser%>" />
     <input type="hidden" name="htnOperater" value="<%=intOperater%>" />
    <table cellpadding="1px" cellspacing="1px" style="border: 1px solid; width: 1000px;">
        <tr>
            <td colspan="5" style="font-weight: bold; text-align: center">
                客户信息
            </td>
            <td>
                <a href="">历史来电</a>
            </td>
        </tr>
        <tr>
            <td>
                客户名称
               
            </td>
            <td colspan="4">  <%=cModel.CC_Name%> 
            </td>
            <td>
                来电次数()
            </td>
        </tr>
        <tr>
            <td>
                部署工程师
            </td>
            <td>
                
            </td>
            <td>
                部署时间
            </td>
            <td>
               
            </td>
            <td>
               
            </td>
            <td>
               
            </td>
        </tr>
        <tr>
            <td>
                负责人
            </td>
            <td>
                <%=cModel.CC_UserName%>
            </td>
            <td>
                负责人电话
            </td>
            <td>
                <%=cModel.CC_Tel%>
            </td>
            <td>
                服务期限
            </td>
            <td>
                <%=cModel.CC_ServiceStartTime%>-
                <%=cModel.CC_ServiceEndTime%>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="font-weight: bold; text-align: center">
                工单信息
            </td>
            <td>
                <a href="javascript:;" onclick="ShowWorkBill(<%=WID %>)">服务记录</a>
            </td>
        </tr>
        <tr>
            <td>
                来电人姓名
            </td>
            <td>
                <input id="txtCallInUserName" type="text" name="txtCallInUserName" maxlength="50" value="<%=vModel.CVB_CallInUserName%>" />                
            </td>
            <td>
                来电人电话
            </td>
            <td>
                <input id="txtCallInTel" type="text" name="txtCallInTel"  maxlength="20" value="<%=vModel.CVB_CallInTel%>"/>                
            </td>
            <td>
                来电人邮箱
            </td>
            <td>
                <input id="txtCallInEmail" type="text" name="txtCallInEmail"  maxlength="50"  value=" <%=vModel.CVB_CallInEmail%>"/>               
            </td>
        </tr>
        <tr>
            <td>
                工单类型
            </td>
            <td>
                <select id="sltWorkBillType" name="sltWorkBillType">
                    <%= strWorkBillDrop%>
                </select>
            </td>
            <td>
                咨询类型
            </td>
            <td>
                <select id="sltServiceType" name="sltServiceType">
                    <%= strCallServiceTypeDrop%>
                </select>
            </td>
            <td>
                工单状态
            </td>
            <td>
                <select id="sltStatus" name="sltStatus">
                    <%= strCallWorkBillStatusDrop%>
                </select></td>
        </tr>
      
        <tr>
            <td>
                创建人
            </td>
            <td>
                <%=strUserName%>
            </td>
            <td>
                创建时间
            </td>
            <td>
                <%=strCreateTime%>
            </td>
            <td>
               
                回访人:</td>
            <td>
               <%=strUserName%>(回访时间:<% =strVisitTime%>)
            </td>
        </tr>
        <tr>
            <td>
                问题描述
            </td>
            <td colspan="5">
                <textarea id="txtDescription" name="txtDescription"> <%=vModel.CVB_Description%></textarea>
            </td>
        </tr>
        <tr>
            <td>
                解决办法
            </td>
            <td colspan="5">
                <textarea id="txtSolution" name="txtSolution"> <%=vModel.CVB_Solution%></textarea>
            </td>
        </tr>
        <tr>
            <td>
                回访结果
            </td>
            <td colspan="5">
                <textarea id="txtRemark" name="txtRemark"> <%=vModel.CVB_Remark%></textarea>
            </td>
        </tr>
        <tr style="display:none">
            <td>
            </td>
            <td colspan="5">
                <input id="Button1" name="Button1" type="submit" value="保存" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
