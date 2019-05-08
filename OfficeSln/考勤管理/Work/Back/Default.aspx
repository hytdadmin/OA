<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Work_Back_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>目录文件</title>
    <script src="../../Scripts/jquery-1.10.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            font-size:12px;
        }
         .divhr
        {
                border-bottom: 1px solid #D48656;
height: 20px;
width: 80%;
        }
        .dv span
        {
            color: #489DF0;
            cursor: pointer;
            display: none;
            float:left;
        }
        #tbody li
        {
            float: left;
            list-style: none;
            margin-left: 10px;
            line-height: 20px;
        }
        #tbody li img
        {
            float: left;
            padding-top: 2px;
        }
        #tbody li span
        {
            float: left;
            display: block;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#hidName").val("");
            $.post("Defulault.ashx", { "op": "star", "name": "" }, function (msg) {
                $("#tbody").append(msg);
                dirClick();
                Show();
            })
        })
        function dirClick() {
            $(".linkDir").click(function () {
                var name = $(this).text();
                var hid = $("#hidName").val()
                $("#hidName").val(hid + "\\" + name);
                hid = hid + "\\" + name;
                $("#tbody").children().remove("");
                $(".dv label").css("display", "none");
                $(".dv span").css("display", "block");
                $.post("Defulault.ashx", { "op": "two", "name": name, "hid": hid }, function (msg) {
                    $("#tbody").append(msg);
                    dirClick();
                    Show();
                })
            })
        }
        function Show() {
            $("#tbody tr").mouseenter(function () {
                $(this).css("background-color", "#E9F5FE");
            }).mouseleave(function () {
                $(this).css("background-color", "");
            });

        }
        function prevList() {
            var hid = $("#hidName").val();
            var count = hid.lastIndexOf('\\');
            var str = hid.substr(0, count);
            if (str == "") {
                $(".dv label").css("display", "block");
                $(".dv span").css("display", "none");
                $("#hidName").val("");
                $("#tbody").children().remove("");
                $.post("Defulault.ashx", { "op": "star", "name": "" }, function (msg) {
                    $("#tbody").append(msg);
                    dirClick();
                    Show();
                })
            } else {
                $("#hidName").val(str);
                $("#tbody").children().remove("");
                $(".dv label").css("display", "none");
                $(".dv span").css("display", "block");
                $.post("Defulault.ashx", { "op": "pre", "name": name, "hid": str }, function (msg) {
                    $("#tbody").append(msg);
                    dirClick();
                    Show();
                })
            }

        }

        //全部
        function allList() {
            $("#tbody").children().remove("");
            $(".dv label").css("display", "block");
            $(".dv span").css("display", "none");
            $("#hidName").val("");
            $.post("Defulault.ashx", { "op": "star", "name": "" }, function (msg) {
                $("#tbody").append(msg);
                dirClick();
                Show();
            })
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" name="hid" value="" id="hidName" />
    <div class="dv">
        <div class="divhr">
        <ul>
        <li style="list-style:none;"> <label>
            全部文件</label><span onclick="prevList()">上一级|</span><span onclick="allList()">全部文件</span></li>
        </ul>
       </div>
        <div border="1" cellpadding="0" cellspacing="0" style="width: 1006px; border-collapse: collapse;">
            <ul id="tbody">
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
