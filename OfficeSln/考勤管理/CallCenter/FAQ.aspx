<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="FAQ.aspx.cs" Inherits="CallCenter_FAQ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工单信息</title>
    <script src="../Work/Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="artDialog4.1.7/artDialog.source.js?skin=blue" type="text/javascript"></script>
    <script src="artDialog4.1.7/plugins/iframeTools.source.js" type="text/javascript"></script>
    <!-- 配置文件 -->
    <script src="../ueditor/ueditor.config.js" charset="utf-8" type="text/javascript"></script>
    <script src="../ueditor/ueditor.all.min.js" charset="utf-8" type="text/javascript"></script>

    <style type="text/css">
        table
        {
            border-collapse: collapse;
            border: none;
        }
        td
        {
            border: solid #000 1px;
            padding-top:10px;
        }
    </style>
</head>
<body>                              
    <form id="FAQListForm" runat="server" action="FAQListManage.ashx">
    <input type="hidden" name="option" value="<%=strOperation%>" />
    <input type="hidden" name="txtFID" value="<%=cModel.CF_ID%>" />
    <input type="hidden" name="txtContent" value="" />
    <table cellpadding="1px" cellspacing="1px" style="border: 1px solid; width: 600px;">
        <tr>
            <td>
                标题
            </td>
            <td>
                 <input id="txtErrorList" type="text" name="txtErrorList" maxlength="20" value=" <%=cModel.CF_ErrorList%>" /></div>
            </td>
        </tr>
        <tr>
            <td>
               描述
            </td>
            <td>
                 <input id="txtDescribe" type="text" name="txtDescribe" maxlength="20" value=" <%=cModel.CF_Describe%>" />
            </td>
        </tr>
        <tr>
            <td>
               类型
            </td>
            <td>
             <select name="txtSoftTypeID" id="txtSoftTypeID">
                                <%=GetCategory(cModel.CF_SoftTypeID)%>
                            </select>
               <%--<input id="txtSoftTypeID" type="text" name="txtSoftTypeID" maxlength="20" value="<%=cModel.CF_SoftTypeID%>" />--%>
            </td>
        </tr>
        <tr>
          <td>
               内容
          </td>
          <td style="padding-top:0px">
              <!-- 加载编辑器的容器 -->
              <script id="txtContent" name="txtContent" type="text/plain" style="width: 720px; height: 200px;"></script>
             <%-- <input id="txtContent" type="text" name="txtContent" maxlength="20" value="<%=cModel.CF_Content%>" />--%>
           </td>
        </tr>
        
        <tr>
            <td>
              <input id="btnClear" type="button" value="清空" title="清空信息" />
            </td>
            <td>
              <input id="Button1" name="Button1" type="hidden" value="保存" />
            </td>
        </tr>
    </table>
     <script type="text/javascript">
         var ue = UE.getEditor('txtContent');
         var strContent='<%=cModel.CF_Content%>';
         ue.ready(function () {
             //设置编辑器的内容
             ue.setContent(strContent);
         });
         $().ready(function () {
             $("#btnClear").click(function () {
                 $("#txtFID").val("");
                 $("#txtErrorList").val("");
                 $("#txtDescribe").val("");
                 $("#txtSoftTypeID").val("");
                 ue.setContent("");
             });
         });
         //这段要放在文本编辑器的实例化之后
//         function uptext() {
//             alert($("#txtFID").val());
//             if ($("#txtTitle").val()=="") {
//                 alert('请先填写标题!');
//             }
//             else if ($("#txtSoftTypeID") == "") {
//                 alert('请选择标题分类!');
//             }
//             else if (UE.getEditor('txtContent') == "") {
//                 alert('请先填写内容!');
//             } else {
//                 document.setweb.txtContent.value = UE.getEditor('txtContent').getContent();
//                 document.setweb.submit();
//             }
//         }
    </script>
    </form>
</body>
</html>
