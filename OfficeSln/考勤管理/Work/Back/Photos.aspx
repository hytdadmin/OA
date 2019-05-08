<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Photos.aspx.cs" Inherits="Work_Back_Photos" %>

<%@ Register Src="../UserControl/Top.ascx" TagName="Top" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/Bottom.ascx" TagName="Bottom" TagPrefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>图片展</title>
    <link href="../style/indexcss.css" rel="stylesheet" type="text/css">
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <script src="../../Scripts/jquery-1.10.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            font-size:12px;
        }
         .divhr
        {
                border-bottom: 1px solid #D48656;
              height:30px;
              font-size:20px;
width: 95%;
margin:10px auto;
        }
        .dv span
        {
            color: #489DF0;
            cursor: pointer;
            display: none;
            float:left;
        }
        #contentpic li
        {
            float: left;
            list-style: none;
            margin-left: 10px;
            line-height: 20px;
        }
        #contentpic li img
        {
            float: left;
            padding-top: 2px;
        }
        #contentpic li span
        {
            float: left;
            display: block;
        }
        .contentpic{ clear:both; overflow:hidden;}
        ul#tilses{display: block;width: 100%;clear: both;overflow: hidden;}
        ul#tilses li{clear:left; margin-top:5px;}
        
    </style>
  <!-- CSS Reset -->
  <link rel="stylesheet" href="../Wookmark/css/reset.css">

  <!-- colorbox styles -->
  <link rel="stylesheet" href="../Wookmark/css/colorbox.css">

    <script src="../Scripts/pub.js" type="text/javascript"></script>
  <!-- Global CSS for the page and contentpic -->
  <link rel="stylesheet" href="../Wookmark/css/main.css">
    <script type="text/javascript">
        $(function () {
            $("#hidName").val("");
            $.post("Photos.ashx", { "op": "star", "name": "" }, function (msg) {
                $("#contentpic").append(msg);
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
                $("#contentpic").children().remove("");
                $(".dv label").css("display", "none");
                $(".dv span").css("display", "block");
                $.post("Photos.ashx", { "op": "two", "name": name, "hid": hid }, function (msg) {
                    $("#contentpic").append(msg);
                    dirClick();
                    Show();
                })
            })
        }
        function Show() {
            $("#contentpic tr").mouseenter(function () {
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
                $("#contentpic").children().remove("");
                $.post("Photos.ashx", { "op": "star", "name": "" }, function (msg) {
                    $("#contentpic").append(msg);
                    dirClick();
                    Show();
                })
            } else {
                $("#hidName").val(str);
                $("#contentpic").children().remove("");
                $(".dv label").css("display", "none");
                $(".dv span").css("display", "block");
                $.post("Photos.ashx", { "op": "pre", "name": name, "hid": str }, function (msg) {
                    $("#contentpic").append(msg);
                    dirClick();
                    Show();
                })
            }

        }

        //全部
        function allList() {
            $("#contentpic").children().remove("");
            $(".dv label").css("display", "block");
            $(".dv span").css("display", "none");
            $("#hidName").val("");
            $.post("Photos.ashx", { "op": "star", "name": "" }, function (msg) {
                $("#contentpic").append(msg);
                dirClick();
                Show();
            })
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:Top ID="Top1" runat="server" />
    <div class="W_main">
    <div id="container" style="background-color: #FFFFFF;padding: 10px 0px;min-height: 500px;">
    <input type="hidden" name="hid" value="" id="hidName" />
    <div class="dv">
    <div class="divhr">
        <label>全部文件</label><span onclick="prevList()" style="padding-right:25px;">上一级|</span><span onclick="allList()">全部文件</span></div>
    
      <%--<ul id="tiles">
        <!-- These are our grid blocks -->
        <!-- End of grid blocks -->
      </ul>--%>
      <div id="contentpic" class="contentpic">
      </div>

    
    </div>
    <footer>

    </footer>
  </div>
  </div>
  <!-- include jQuery -->
  <script src="../Wookmark/libs/jquery.min.js"></script>

  <!-- Include the imagesLoaded plug-in -->
  <script src="../Wookmark/libs/jquery.imagesloaded.js"></script>

  <!-- include colorbox -->
  <script src="../Wookmark/libs/jquery.colorbox-min.js"></script>

  <!-- Include the plug-in -->
  <script src="../Wookmark/libs/jquery.wookmark.js"></script>

  <!-- Once the page is loaded, initalize the plug-in. -->
  <script type="text/javascript">
      
  </script>
    <div class="foot">
        <uc7:Bottom ID="Bottom1" runat="server" />
    </div>
    </form>
</body>
</html>
