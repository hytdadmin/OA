<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerList.aspx.cs" Inherits="CallCenter_CustomerList" %>

<%@ Register Src="../Work/UserControl/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="../Work/UserControl/ChangeHeadImg.ascx" TagName="ChangeHeadImg"
    TagPrefix="uc2" %>
<%@ Register Src="../Work/UserControl/Bottom.ascx" TagName="Bottom" TagPrefix="uc3" %>
<%@ Register Src="UserRights.ascx" TagName="UserRights" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta name="author" content="环宇通达 +86-532-86100168">
    <meta name="description">
    <meta name="keywords">
    <link href="/Work/style/indexcss.css" rel="stylesheet" type="text/css">
    <link href="/Work/Style/pager.css" rel="stylesheet" type="text/css" />
    <link href="/Work/Scripts/jqueryPager/pagination.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/posts/style/style_6_common.css" />
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_forum_forumdisplay.css" />
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_widthauto.css" />
    <link rel="stylesheet" href="/Posts/style/reset.css" />
    <link rel="stylesheet" href="/Posts/style/selectForK13.css" />
    <link rel="stylesheet" href="/Posts/style/indexcss.css" />
    <link href="autocomplete/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script src="/Work/scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/Work/Scripts/jqueryPager/jquery-1.2.6.pack.js" type="text/javascript"></script>
    <script src="/Work/Scripts/jqueryPager/jquery.pagination.js" type="text/javascript"></script>
    <script src="/Work/scripts/pub.js" type="text/javascript"></script>
    <script src="/Work/scripts/Says.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="artDialog4.1.7/artDialog.source.js?skin=blue" type="text/javascript"></script>
    <script src="artDialog4.1.7/plugins/iframeTools.source.js" type="text/javascript"></script>
    <script src="autocomplete/jquery.autocomplete.min.js" type="text/javascript"></script>
    <title>客户列表</title>
    <script type="text/javascript">
    var  isSave = 0;
        function returnPage() {
            window.location.replace(window.location.href.replace("page", "fy_none"));
        }
        function EditCustomerInfo(cid) {
            art.dialog.open('CustomerInfo.aspx?hf=0&cid=' + cid
            ,
            { title: '客户信息',
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
                    iframe.document.getElementById('form1').submit();
                    isSave = 1;
                    return false;
                },
                cancel: function () { if(isSave == 1) parent.location.reload(); }
            , width: 1020, height: 480
            }, false);
        }
         var Customers = <%=strCumstomers %>;
        $(function () {
            $('#txtCustomerName').autocomplete(Customers, {
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
                //                $('#txtCustomerName').val(row[0].name);
                //                alert(row[0].to);
            });
        }); 
            function searchWorkBill() {
            var url = "CustomerList.aspx";
            var urlE = "?sltType=" + $("#sltType").val();
            if ($.trim($("#txtCustomerName").val()).length > 0) {
                urlE = urlE + "&cname=" + $.trim($("#txtCustomerName").val());
            }
            if ($.trim($("#txtUserName").val()).length > 0) {
                urlE = urlE + "&uname=" + $.trim($("#txtUserName").val());
            }
            if ($.trim($("#txtOwnerName").val()).length > 0) {
                urlE = urlE + "&owner=" + $.trim($("#txtOwnerName").val());
            }
            window.location.href = url + urlE;
        }

         $().ready(function () {

            if (request("cname") != "") {
                $("#txtCustomerName").val(decodeURI(request("cname")));
            }
            if (request("uname") != "") {
                $("#txtUserName").val(decodeURI(request("uname")));
            }
            if (request("owner") != "") {
                $("#txtOwnerName").val(decodeURI(request("owner")));
            }

            if (request("owner") != "") {
               $("#txtOwnerName").val(decodeURI(request("owner")));
               }
        });
        function request(paras) {
            var url = location.href;
            var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
            var paraObj = {}
            for (i = 0; j = paraString[i]; i++) {
                paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
            }
            var returnValue = paraObj[paras.toLowerCase()];
            if (typeof (returnValue) == "undefined") {
                return "";
            } else {
                return returnValue;
            }
        }
    </script>
</head>
<body>
    <uc2:ChangeHeadImg ID="ChangeHeadImg2" runat="server" />
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divload" style="top: 70%; right: 30%; position: absolute; padding: 0px;
        margin: 0px; z-index: 999">
        <img src="/Scripts/jqueryPager/spinner3-greenie.gif" />
    </div>
    <uc1:Top ID="Top1" runat="server" />
    <div class="mu_nv" style="margin-top: 30px;">
    </div>
    <div style="clear: both">
    </div>
    <div class="drag" style="width: 1200px; margin: 0 auto; background-color: rgb(255, 255, 255)">
        <div id="Div1" class="toplist">
            <ul>
                <uc4:UserRights ID="UserRights1" runat="server" />
            </ul>
            <div style="clear: both">
            </div>
        </div>
        <div id="diy4" class="area">
            <input type="hidden" name="typecid" value="1110" />
            <table class="tblog" style="margin-left: 20px">
                <tr>
                    <td class="tblogtd">
                        客户类型:
                    </td>
                    <td colspan="5">
                        <select name="sltType">
                            <%=GetCategory(-1)%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="tblogtd">
                        客户名称:
                    </td>
                    <td class="tblogtd_c">
                        <input type="text" id="txtCustomerName" name="txtCustomerName" value="" />
                    </td>
                    <td class="tblogtd">
                        联系人:
                    </td>
                    <td class="tblogtd_c">
                        <input type="text" id="txtUserName" name="txtUserName" value="" />
                    </td>
                    <td class="tblogtd">
                        我的客户：
                    </td>
                    <td class="tblogtd_c">
                        <input type="text" name="txtOwnerName" id="txtOwnerName" style="width: 105px" value="" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: right">
                        <input id="Button1" type="button" value="查询" style="margin-right: 20px; height: 22px;
                            width: 50px" onclick="searchWorkBill()" />
                    </td>
                </tr>
            </table>
            <div id="pgt" class="bm bw0 pgs cl">
                <div class="pgt">
                    <div class="pg">
                        <%=strtt%>
                        <span id="fd_page_top1"></span><span class="pgb y"><a href="javascript:;" onclick="returnPage();">
                            第一页</a></span>
                    </div>
                </div>
            </div>
        </div>
        <div id="threadlist" class="tl bm bmw">
            <div class="th">
                <table cellspacing="0" cellpadding="0">
                    <tbody>
                        <tr>
                            <td class="num">
                                所属工程师
                            </td>
                            <td class="by">
                                客户名称
                            </td>
                            <td class="num">
                                联系人
                            </td>
                            <td class="num">
                                联系电话
                            </td>
                            <td class="num">
                                注册时间
                            </td>
                            <td class="num">
                                服务开始时间
                            </td>
                            <td class="num">
                                服务结束时间
                            </td>
                            <td class="num">
                                状态
                            </td>
                            <td class="num">
                                操作
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
        <div class="bm bw0 pgs cl">
            <div class="pg">
                <%=strtt %>
                <span id="Span1"></span><span class="pgb y"><a href="javascript:;" onclick="returnPage();">
                    第一页</a></span>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
