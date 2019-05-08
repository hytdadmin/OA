<%@ Page Language="C#" AutoEventWireup="true" CodeFile="useracounte.aspx.cs" Inherits="CallCenter_useracounte" %>

<%@ Register src="../Work/UserControl/ChangeHeadImg.ascx" tagname="ChangeHeadImg" tagprefix="uc1" %>

<%@ Register src="../Work/UserControl/Top.ascx" tagname="Top" tagprefix="uc2" %>

<%@ Register src="UserRights.ascx" tagname="UserRights" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="/Work/style/indexcss.css" rel="stylesheet" type="text/css" />
    <link href="/Work/Scripts/jqueryPager/pagination.css" rel="stylesheet" type="text/css" />
    <link href="/Work/Style/pager.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/posts/style/style_6_common.css" />
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_forum_forumdisplay.css" />
    <link rel="stylesheet" type="text/css" href="/Posts/style/style_6_widthauto.css" />
    <link rel="stylesheet" href="/Posts/style/reset.css" />
    <link rel="stylesheet" href="/Posts/style/selectForK13.css" />
    <link rel="stylesheet" href="/Posts/style/indexcss.css" />
    <script type="text/javascript" src="http://cdn.hcharts.cn/jquery/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="http://cdn.hcharts.cn/highcharts/highcharts.js"></script>
    <script type="text/javascript" src="http://cdn.hcharts.cn/highcharts/exporting.js"></script>
    <style type="text/css">
        .style1
        {
            height: 28px;
        }
        .tbclass { border-top:1px solid #ccc;padding-top:39px;font-size:14px;}
        .tbclass tbody{}
        .tbclass tbody tr{height: 30px;}
        .tbclass tbody tr td{padding-left:20px;}
        
        .xingming{font-weight:bold;font-style:italic;color:Blue;}
        .shuzi{font-weight:bold;color:Blue;}
        .shuzihongse{font-weight:bold;color:red;}        
    </style>
    <script type="text/javascript">
        $(function () {
            $('#container').highcharts({
                title: {
                    text: '呼叫中心工单量统计图',
                    x: -20 //center
                },
                subtitle: {
                    text: '按月统计',
                    x: -20
                },
                xAxis: {
                    //categories: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
                     categories: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27', '28', '29', '30', '31']
                },
                yAxis: {
                    title: {
                        text: '日工单量（个）'
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'
                    }]
                },
                tooltip: {
                    valueSuffix: '个'
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    borderWidth: 0
                },
                series: [{
                    name: '<%=strNowMonth %>',
                    data: [<%=sbNowMonth %>]
                }, {
                    name: '<%=strPrevMonth %>',
                    data: [<% =sbPrevMonth%>]
                }, {
                    name: '<%=strOldYearMonth %>',
                    data: [<% =sbOldYearMonth%>]
                }]
            });
   
 $('#workBillTotalByYear').highcharts({
                title: {
                    text: '呼叫中心工单量统计图',
                    x: -20 //center
                },
                subtitle: {
                    text: '按年统计',
                    x: -20
                },
                xAxis: {
                    categories: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
                     //categories: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27', '28', '29', '30', '31']
                },
                yAxis: {
                    title: {
                        text: '月工单量（个）'
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'
                    }]
                },
                tooltip: {
                    valueSuffix: '个'
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    borderWidth: 0
                },
                series: [{
                    name: '<%=strNowYear %>',
                    data: [<%=sbNowYear %>]
                }, {
                    name: '<%=strPrevYear %>',
                    data: [<% =sbPrevYear%>]
                }, {
                    name: '<%=strPrevPrevYear %>',
                    data: [<% =sbPrevPrevYear%>]
                }]
            });

             $('#workBillTotalByYearTime').highcharts({
                title: {
                    text: '呼叫中心工单量统计图',
                    x: -20 //center
                },
                subtitle: {
                    text: '按时间段统计',
                    x: -20
                },
                xAxis: {
                    //categories: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
                     categories: ['00','01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23']
                },
                yAxis: {
                    title: {
                        text: '月工单量（个）'
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'
                    }]
                },
                tooltip: {
                    valueSuffix: '个'
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    borderWidth: 0
                },
                series: [{
                    name: '<%=strNowTime %>',
                    data: [<%=sbNowTime %>]
                }, {
                    name: '<%=strPrevTime %>',
                    data: [<% =sbPrevTime%>]
                }, {
                    name: '<%=strPrevYearTime %>',
                    data: [<% =sbPrevYearTime%>]
                }]
            });
        });				
    </script>
</head>
<body><uc1:ChangeHeadImg ID="ChangeHeadImg1" runat="server" />
    <form id="form1" runat="server">        
        <div id="divload" style="top: 70%; right: 30%; position: absolute; padding: 0px; display:none;
        margin: 0px; z-index: 999">
        <img src="Scripts/jqueryPager/spinner3-greenie.gif" />
    </div><uc2:Top ID="Top1" runat="server" />
     <div style="width: 1200px; margin: 0 auto;">
        <div class="drag" style="width: 1200px; margin: 0 auto; background-color: rgb(255, 255, 255)">       
            <div id="Div1" class="toplist">
            <ul> 
                <uc3:UserRights ID="UserRights1" runat="server" />
            </ul>
             <div style="clear: both">
                </div>
            </div>
            <div id="diy4" class="area">
        <table id="idtable" cellpadding="1px" cellspacing="1px" style="width: 100%;" class="tbclass">
            <tr>
                <td colspan="4" style="text-align: center;font-size:18px;">
                    <strong>                    
                    呼叫中心统计信息 </strong>
                </td>
            </tr>
            <tr>
                <td colspan="4">  <strong>      
                    工单总量统计</strong>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    呼叫中心共处理工单：<span class="shuzi">
                    <asp:Literal ID="ltlTotalCount" runat="server"></asp:Literal> </span>
                    本月工单数：
                   <span class="shuzi"> <asp:Literal ID="ltlMonthCount" runat="server"></asp:Literal> </span>
                    本日工单数：
                   <span class="shuzi"> <asp:Literal ID="ltlDateCount" runat="server"></asp:Literal></span>
                </td>
               
            </tr>
            <tr>
                <td colspan="4" >
                    <asp:Literal ID="ltlUserCount" runat="server"></asp:Literal>  【其中<asp:Literal ID="ltlUserWorkBillCount" runat="server"></asp:Literal>】 
                </td>
            </tr>
            <tr>
                <td colspan="4" > <strong>图形统计</strong></td>
            </tr>
        </table>
        <div id="container" style="min-width: 700px; height: 400px">
        </div>
        <div id="workBillTotalByYear" style="min-width: 700px; height: 400px">
        </div>
        <div id="workBillTotalByYearTime" style="min-width: 700px; height: 400px">
        </div>
        </div>
        </div>
        
    </div>
    </form>
</body>
</html>
