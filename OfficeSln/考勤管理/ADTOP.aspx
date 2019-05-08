<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ADTOP.aspx.cs" Inherits="ADTOP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script>
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
<body style="margin: 0; padding: 0;">
    <form id="form1" runat="server" style="">
    <input type="hidden" id="hdnV" name="hdnV" value="1" />
    <div id="divImg" style="width:900px;height:200px;border:1px solid red;">
     <p>重要通知</p>
     <p style="text-indent:2em">本着为公司负责服务客户的态度，现在就CA部门售后服务，增加一个获取用户满意度渠道的方式，即网站右下角的满意度调查连接，满意度调查页面有几个
     示例调查内容，不是最终版本，为了更好更客观获取用户真实意见，达到自我完善、查漏补缺，以便更好的为用户提供优质服务，同时提升公司整体形象的目的，希望各位同事对满意
     度调查内容积极提出自己的建议。</p>
    </div>
    <iframe id="iframeA" name="iframeA" src="" width="0" height="0" style="display: none;">
    </iframe>
    <script type="text/javascript">
        function sethash() {
            var hashH = $("#divImg").height() + 30; //document.documentElement.scrollHeight; //获取自身高度
            var urlC = "http://" + request("url") + "/agent.html"; //设置iframeA的src
            document.getElementById("iframeA").src = urlC + "#" + hashH; //将高度作为参数传递
        }
        window.onload = sethash;
    </script>
    </form>
</body>
</html>
