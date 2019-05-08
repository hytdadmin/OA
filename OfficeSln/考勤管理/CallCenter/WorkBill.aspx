<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkBill.aspx.cs" Inherits="CallCenter_WorkBill" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工单信息</title>
    <script src="../Work/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="artDialog4.1.7/artDialog.source.js?skin=blue" type="text/javascript"></script>
    <script src="artDialog4.1.7/plugins/iframeTools.source.js" type="text/javascript"></script>
     <!-- 配置文件 -->
    <script src="../ueditor/ueditor.config.js" charset="utf-8" type="text/javascript"></script>
    <script src="../ueditor/ueditor.all.min.js" charset="utf-8" type="text/javascript"></script>
    <!--自动填充-->
    <link href="autocomplete/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script src="autocomplete/jquery.autocomplete.min.js" type="text/javascript"></script>
    <!--上传插件-->
    <script src="../Work/Scripts/ajaxfileupload.js" type="text/javascript"></script>
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
            height: 39px;
            width: 667px;
        }
        #txtSolution
        {
            height: 100px;
            width: 670px;
        }
        #txtRemark
        {
            height: 35px;
            width: 667px;
        }
    </style>
    <script type="text/javascript">
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
            , width: 620, height: 630
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
//                keystring += keyName;
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
                            keyName = '-110'; keystring = "";
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
//                keystring += keyName;
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
                            keyName = '-110'; keystring = "";
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
    <form id="workBillForm" runat="server" action="CallCenter.ashx?option=edit&hf=<%=HuiFang %>">
    <input type="hidden" name="hdnHF" value="<%=HuiFang%>" />
    <input type="hidden" name="hdnWID" value="<%=wModel.CWB_ID%>" />
    <input type="hidden" name="hdnCCID" value="<%=cModel.CC_ID%>" />
    <input type="hidden" name="htnCreater" value="<%=intCreater%>" />
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
            <td colspan="4">
                <%=cModel.CC_Name%>
            </td>
            <td>
                来电次数： <span style="color:Red;"><%=intCounts %></span>
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
        <tr style="height: 30px;">
            <td colspan="5" style="font-weight: bold; text-align: center">
                工单信息(<%=strBillCode %>)
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
                <input id="txtCallInUserName" type="text" name="txtCallInUserName" maxlength="50"
                    value="<%=wModel.CWB_CallInUserName%>" />
            </td>
            <td>
                来电人电话
            </td>
            <td>
                <input id="txtCallInTel" type="text" name="txtCallInTel" maxlength="20" value="<%=wModel.CWB_CallInTel%>" />
            </td>
            <td>
                来电人邮箱
            </td>
            <td>
                <input id="txtCallInEmail" type="text" name="txtCallInEmail" maxlength="50" value="" />
                <input id="btnClear" type="button" value="清空" title="清空来电信息" />
            </td>
        </tr>
        <tr>
            <td>
                工单类型
            </td>
            <td>
                <select id="sltWorkBillType" name="sltWorkBillType">
                    <%=    strWorkBillDrop%>
                </select>
            </td>
            <td>
                咨询类型
            </td>
            <td>
                <select id="sltServiceType" name="sltServiceType">
                    <%=    strCallServiceTypeDrop%>
                </select>
            </td>
            <td>
                工单状态
            </td>
            <td>
                <select id="sltStatus" name="sltStatus">
                    <%= strCallWorkBillStatusDrop%>
                </select>
            </td>
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
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                指定服务人
            </td>
            <td>
                <select id="sltUser" name="sltUser">
                    <%=strForUser%>
                </select>
            </td>
            <td>
                最后服务时间
            </td>
            <td>
                <%=strOptionTime%>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                问题描述<br />
                <input type="text" name="SelectFAQ" id="SelectFAQ" value="" />
            </td>
            <td colspan="5">
                <textarea id="txtDescription" name="txtDescription"> <%=wModel.CWB_Description%></textarea>
            </td>
        </tr>
     <%--   <tr>
            <td>
                解决办法
            </td>
            <td colspan="5">
                <textarea id="txtSolution" name="txtSolution"> <%=wModel.CWB_Solution%></textarea>
            </td>
        </tr>--%>
         <tr>
            <td>
                解决方案</td>
            <td colspan="4">
                  <!-- 加载编辑器的容器 -->
                <script id="txtSolution" name="txtSolution" type="text/plain" style="width:670px; height:100px;"></script>
                <div style="height:60px;"></div>
            </td>
            <td style="padding:8px;">
                <input id="file_upload" type="file" name="file_upload" style="width:150px;" />
                <br /><br /><br />&nbsp&nbsp&nbsp&nbsp<input id="ButtonFAQ" name="ButtonFAQ" type="button" value="发送邮件" />
            </td>
        </tr>
        <tr>
            <td>
                备注
            </td>
            <td colspan="5">
                <textarea id="txtRemark" name="txtRemark"> <%=wModel.CWB_Remark%></textarea>
            </td>
        </tr>
        <tr style="display:none ">
            <td>
            </td>
            <td colspan="5">
                <input id="Button1" name="Button1" type="submit" value="保存" />
            </td>
        </tr>
    </table>
         <script type="text/javascript">
             var ue = UE.getEditor('txtSolution', {
          toolbars: [
['undo','redo','bold','indent','snapscreen','italic','underline','strikethrough','formatmatch','cleardoc','simpleupload', 'insertimage', 'link',
 'emotion','spechars','help','justifyleft', 'justifyright', 'justifycenter','justifyjustify','forecolor','backcolor','fullscreen', 
 'imageleft', 'imageright', 'imagecenter']]
    });
             var strFAQList = <%=strFAQList%>;
           //ue.render('txtSolution');
           ue.ready(function() {
           ue.setContent('<%=wModel.CWB_Solution%>');  //赋值给UEditor
           });
             $().ready(function () {
                 $("#btnClear").click(function () {
                     $("#txtCallInUserName").val("");
                     $("#txtCallInTel").val("");
                     $("#txtCallInEmail").val("");
                 });
                 //开始发送邮件
                 $("#ButtonFAQ").click(function (e) {
                    $("#pageloading").show();
                    $.ajaxFileUpload
                    ({
                    url: 'FAQListManage.ashx?option=upload', //用于文件上传的服务器端请求地址
                    secureuri: false, //一般设置为false
                    fileElementId: 'file_upload', //文件上传空间的id属性  <input type="file" id="file" name="file" />
                    dataType: 'text', //返回值类型 一般设置为json
                    success: function (fileurl, status)  //服务器成功响应处理函数
                    {
                        $.ajax({
                            url: 'FAQListManage.ashx?option=SendEmail',
                            type: 'post',
                            data: {"txtCallInEmail":$("#txtCallInEmail").val(),"EContent":ue.getContent(),"uploadfile":fileurl,"CallInUserName":$("#txtCallInUserName").val()},
                            dataType: 'text',
                            cache: false,
                            success: function (Data) //成功执行方法 
                            {
                              $("#pageloading").hide();
                              art.dialog.alert('发送成功');
                            },
                            error: function () //错误执行方法
                            {
                                art.dialog.alert('发送失败');
                            }
                        })
                        
                    },
                    error: function (data, status, e)//服务器响应失败处理函数
                    {
                        alert(e);
                    }
                  })
                  return false;
                 })
                 $('#SelectFAQ').autocomplete(strFAQList, {
                     //               extraParams: { keyword: function () { return $('#txtUserName').val(); } },
                     //                                dataType: "json", 
                     max: 12,    //列表里的条目数
                     minChars: 0,    //自动完成激活之前填入的最小字符
                     width: 400,     //提示的宽度，溢出隐藏
                     scrollHeight: 300,   //提示的高度，溢出显示滚动条
                     matchContains: true,    //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                     autoFill: false,    //自动填充
                     formatItem: function (row, i, max) {
                         //                   return i + '/' + max + ':"' + row.name + '"[' + row.to + ']';
                         return i + '/' + max + ':"' + row.name;
                     },
                     formatMatch: function (row, i, max) {
                         return row.name;
                     },
                     formatResult: function (row) {
                         return row.name;
                     }
                 }).result(function (event, row, formatted) {
                     row = eval(row);
                      $.ajax({
                         url: 'FAQListManage.ashx?option=GetFaqContent',
                         type: 'GET',
                         data: "SelectID=" + row.to,
                         dataType: 'text',
                         cache: false,
                         success: function (Data) //成功执行方法 
                         {
                          var json = eval('(' + Data + ')');
                          $("#txtDescription").val(json[0].Describe);
                          ue.setContent(json[0].Content);
                         },
                         error: function () //错误执行方法
                         {
                             art.dialog.alert('读取常见问题错误');
                         }
                     })
                 });
             });
    </script>
    </form>
</body>
</html>
