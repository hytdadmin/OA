<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="showDialog/showDialog.js" type="text/javascript"></script>
    <link href="showDialog/showDialog.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
    <script type="text/javascript">
        var str = "<span>dddddd</span><br/><input type=\"button\" value=\"aa\"/>";
        showDialog("window", str, "提示信息", 490);
    </script>
</html>
