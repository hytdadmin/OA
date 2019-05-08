<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServiceHistoryListForCustomer.aspx.cs"
    Inherits="CallCenter_ServiceHistoryListForCustomer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="stylesheet" type="text/css" href="/posts/style/style_6_common.css" />
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_forum_forumdisplay.css" />
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_widthauto.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="threadlist" class="tl bm bmw">
        <div class="th">
            <table cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <td class="by">
                            问题描述
                        </td>
                        <td class="by">
                            解决办法
                        </td>
                         <td class="by">
                            备注
                        </td>
                        <td class="num">
                            状态
                        </td>
                        <td class="num">
                            操作时间
                        </td>
                        <td class="num">
                            指定服务人
                        </td>
                         <td class="num">
                            操作人
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="bm_c">
            <div id="forumnew" style="display: none">
            </div>
            <table summary="forum_26" cellspacing="0" cellpadding="0" id="tdSerach">
                <%=script %>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
