<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Satisfaction.aspx.cs" Inherits="CallCenter_Satisfaction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工单信息</title>
    <script src="../Work/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="artDialog4.1.7/artDialog.source.js?skin=blue" type="text/javascript"></script>
    <script src="artDialog4.1.7/plugins/iframeTools.source.js" type="text/javascript"></script>
    <style type="text/css">
        table
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            border-collapse: collapse;
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
            width: 358px;
        }
        #txtSolution
        {
            height: 71px;
            width: 361px;
        }
        #txtRemark
        {
            height: 85px;
            width: 383px;
        }
        #txtRemark0
        {
            height: 85px;
            width: 383px;
        }
        .style4
        {
            height: 25px;
        }
        .style5
        {
            height: 22px;
        }
        .style6
        {
            height: 20px;
        }
        .style7
        {
            height: 27px;
        }
        .style8
        {
            height: 21px;
        }
        .style9
        {
            height: 28px;
        }
        .style10
        {
            height: 66px;
        }
        .style12
        {
            height: 22px;
            width: 111px;
        }
        .style13
        {
            height: 25px;
            width: 111px;
        }
        .style14
        {
            height: 20px;
            width: 111px;
        }
        .style15
        {
            height: 21px;
            width: 111px;
        }
        .style16
        {
            height: 28px;
            width: 111px;
        }
        .style17
        {
            height: 27px;
            width: 111px;
        }
        .style18
        {
            height: 66px;
            width: 111px;
        }
        .style19
        {
            width: 111px;
        }
        .style20
        {
            height: 25px;
            width: 119px;
        }
        .style21
        {
            height: 20px;
            width: 119px;
        }
        .style22
        {
            height: 21px;
            width: 119px;
        }
        .style23
        {
            height: 28px;
            width: 119px;
        }
        .style24
        {
            height: 27px;
            width: 119px;
        }
        .style26
        {
            height: 25px;
            width: 104px;
        }
        .style27
        {
            height: 20px;
            width: 104px;
        }
        .style28
        {
            height: 21px;
            width: 104px;
        }
        .style29
        {
            height: 28px;
            width: 104px;
        }
        .style30
        {
            height: 27px;
            width: 104px;
        }
        .style31
        {
            width: 104px;
        }
        .style33
        {
            width: 385px;
        }
        .style34
        {
            height: 22px;
            width: 385px;
        }
        .style35
        {
            height: 25px;
            width: 385px;
        }
        .style36
        {
            height: 20px;
            width: 385px;
        }
        .style37
        {
            height: 27px;
            width: 385px;
        }
        .style38
        {
            height: 21px;
            width: 385px;
        }
        .style39
        {
            height: 28px;
            width: 385px;
        }
        .style41
        {
            width: 104px;
        }
        .style43
        {
            width: 119px;
        }
        .style44
        {
            height: 78px;
            width: 385px;
        }
        .style45
        {
            height: 78px;
        }
        .style46
        {
            height: 69px;
            width: 104px;
        }
        .style47
        {
            height: 69px;
        }
        .style48
        {
            height: 69px;
            width: 119px;
        }
        .style49
        {
            height: 69px;
            width: 385px;
        }
    </style>
    <script type="text/javascript">
        $().ready(function () {
            $("#btnClear").click(function () {
                $("#txtCallInUserName").val("");
                $("#txtCallInTel").val("");
                $("#txtCallInEmail").val("");
            });
        });
        function ShowWorkBill(wid) {
            art.dialog.open('ServiceHistoryListForCustomer.aspx?wid=' + wid
            ,
            { title: '服务记录',
                // 在open()方法中，init会等待iframe加载完毕后执行
                init: function () {
                    var iframe = this.iframe.contentWindow;
                    var top = art.dialog.top; // 引用顶层页面window对象
                    var username = iframe.document.getElementById('Button1');
                    username.value = 'guest';
                    //                    top.document.title = '测试';
                },
                ok: function () {
                    var iframe = this.iframe.contentWindow;
                    var top = art.dialog.top; // 引用顶层页面window对象
                    var username = iframe.document.getElementById('Button1').click();
                    username.click();
                },
                cancel: true
            , width: 620, height: 530
            }, false);
        }

        $().ready(function () {
            var re = /^[A-Za-z0-9]*$/;
            var keystring = ""; //记录按键的字符串       
            var anArray = Array('0xC004F038', '0xC004F074', '0xC004F035', '0xC004F025', '0xC004C020', '0xC004C003', '0x8007232B', '0x8007007B', '0xC004F038');
            $("#txtDescription").keypress(function (e) {
                var currKey = 0, CapsLock = 0, e = e || event;
                currKey = e.keyCode || e.which || e.charCode;
                CapsLock = currKey >= 65 && currKey <= 90;
                keyName = String.fromCharCode(currKey);
                keystring += keyName;
                //                alert(keystring);
                var str = $("#txtDescription").val() + keyName;
                var strNew = $("#txtDescription").val();
                var intlength = str.length;
                if (intlength >= 3) {
                    str = str.substring(str.length - 3);
                    strNew = strNew.substring(0, strNew.length - 2);
                }
                //                if (!(currKey < 48 || currKey > 57) && currKey != 46) {
                if (re.test(keyName) == true) {
                    $.each(anArray, function (n, value) {
                        if (value.indexOf(str) > 0) {
                            $("#txtDescription").val(strNew + value)
                            keyName = '-110';
                        }
                    });
                }
                if (keyName == '-110')
                    return false;
            });

            $("#txtSolution").keypress(function (e) {
                var currKey = 0, CapsLock = 0, e = e || event;
                currKey = e.keyCode || e.which || e.charCode;
                CapsLock = currKey >= 65 && currKey <= 90;
                keyName = String.fromCharCode(currKey);
                keystring += keyName;
                //                alert(keystring);
                var str = $("#txtSolution").val() + keyName;
                var strNew = $("#txtSolution").val();
                var intlength = str.length;
                if (intlength >= 3) {
                    str = str.substring(str.length - 3);
                    strNew = strNew.substring(0, strNew.length - 2);
                }
                if (re.test(keyName) == true) {
                    $.each(anArray, function (n, value) {
                        if (value.indexOf(str) > 0) {
                            $("#txtSolution").val(strNew + value)
                            keyName = '-110';
                        }
                    });
                }
                if (keyName == '-110')
                    return false;
            });
        });

    </script>
</head>
<body>
    <form id="workBillForm" runat="server" action="CallCenter.ashx?option=manyidu">
    <input type="hidden" name="hdnHF" value="<%=HuiFang%>" />
    <input type="hidden" name="hdnWID" value="<%=wModel.CWB_ID%>" />
    <input type="hidden" name="hdnCCID" value="<%=cModel.CC_ID%>" />
    <input type="hidden" name="htnCreater" value="<%=intCreater%>" />
    <input type="hidden" name="htnOperater" value="<%=intOperater%>" />
    <table cellpadding="1px" cellspacing="1px" 
        
        
        style="border-style: solid; border-color: inherit; border-width: 1px; width: 1129px; height: 432px;">
        <tr>
            <td colspan="5" style="font-weight: bold; text-align: center">
                客户信息
            </td>
            <td class="style33">
                <a href="">历史来电</a>
            </td>
        </tr>
        <tr>
            <td class="style12" >
                客户名称
            </td>
            <td colspan="4" class="style5">
                <%=cModel.CC_Name%>
            </td>
            <td class="style34">
                来电次数： <span style="color:Red;"><%=intCounts %></span>
            </td>
        </tr>
        <tr>
            <td class="style13">
                部署工程师
            </td>
            <td class="style26">
            </td>
            <td class="style4">
                部署时间
            </td>
            <td class="style4">
            </td>
            <td class="style20">
            </td>
            <td class="style35">
            </td>
        </tr>
        <tr>
            <td class="style14">
                负责人
            </td>
            <td class="style27">
                <%=cModel.CC_UserName%>
            </td>
            <td class="style6">
                负责人电话
            </td>
            <td class="style6">
                <%=cModel.CC_Tel%>
            </td>
            <td class="style21">
                服务期限
            </td>
            <td class="style36">
                <%=cModel.CC_ServiceStartTime%>-
                <%=cModel.CC_ServiceEndTime%>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="font-weight: bold; text-align: center" class="style7" >
                工单信息(<%=strBillCode %>)
            </td>
            <td class="style37">
                <a href="javascript:;" onclick="ShowWorkBill(<%=WID %>)">服务记录</a>
            </td>
        </tr>
        <tr>
            <td class="style15" >
                来电人姓名
            </td>
            <td class="style28">
               <%=wModel.CWB_CallInUserName%>
            </td>
            <td class="style8">
                来电人电话
            </td>
            <td class="style8">
               <%=wModel.CWB_CallInTel%>
            </td>
            <td class="style22">
                来电人邮箱
            </td>
            <td class="style38">
               <%=wModel.CWB_CallInEmail%>
            </td>
        </tr>
        <tr>
            <td class="style16">
                工单类型
            </td>
            <td class="style29">
                <select id="sltWorkBillType" name="sltWorkBillType">
                    <%=    strWorkBillDrop%>
                </select>
            </td>
            <td class="style9">
                咨询类型
            </td>
            <td class="style9">
                <select id="sltServiceType" name="sltServiceType">
                    <%=    strCallServiceTypeDrop%>
                </select>
            </td>
            <td class="style23">
                满意度调查状态
            </td>
            <td class="style39">
                <select id="sltStatus" name="sltStatus">
                    <%= strCallWorkBillStatusDrop%>
                </select>
            </td>
        </tr>
        <tr>
            <td class="style17" >
                创建人
            </td>
            <td class="style30">
                <%=strUserName%>
            </td>
            <td class="style7">
                创建时间
            </td>
            <td class="style7">
                <%=strCreateTime%>
            </td>
            <td class="style24">
                指定服务人
            </td>
            <td class="style37">
                <select id="sltUser" name="sltUser">
                    <%=strForUser%>
                </select>最后服务时间<%=strOptionTime%>
            </td>
        </tr>
        <tr>
            <td class="style18">
                问题描述
            </td>
            <td colspan="2" class="style10">
                <div style=" max-width:260px;">
                    <%=wModel.CWB_Description%></div>
            </td>
            <td class="style10">
                解决办法
            </td>
            <td colspan="2" class="style10">
                <div style=" max-width:260px;">
                    <%=wModel.CWB_Solution%></div>
            </td>
        </tr>
        <tr>
            <td rowspan="3" class="style19">
                客户满意度&nbsp;
            </td>
            <td class="style31">
                客户满意度
            </td>
            <td colspan="4">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                    Width="383px">
                    <asp:ListItem Value="1">非常满意</asp:ListItem>
                    <asp:ListItem Value="2">满意</asp:ListItem>
                    <asp:ListItem Selected="True" Value="3">一般</asp:ListItem>
                    <asp:ListItem Value="4">不满意</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="style41" rowspan="2">
                备注
            </td>
            <td colspan="2" class="style45"><%=sModel.CS_Remark%>
            </td>
            <td class="style43" rowspan="2">
                客户意见
            </td>
            <td class="style44"><%=sModel.CS_Satisfaction%>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="style47">
                <textarea id="txtBZ" name="txtBZ" cols="50" rows="5"> </textarea>
            </td>
            <td class="style49">
                <textarea id="txtYJ" name="txtYJ" cols="50" rows="5"> </textarea>
            </td>
        </tr>
        <tr style="display:none ">
            <td class="style19" >
            </td>
            <td colspan="5">
                <input id="Button1" name="Button1" type="submit" value="保存" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
