<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CMleft.ascx.cs" Inherits="UserControl_CMleft" %>
<script src="../js/common.js" type="text/javascript"></script>
<script type="text/javascript">
    var ttp = request("typecid");
    $("#forumleftside").find("dd").removeClass();
    $().ready(function () {
        $("#forumleftside").find("dd").each(function (e) {
            if ($(this).find("a").attr("tp") == ttp) {
                $(this).addClass("bdl_a");
            }
            if (ttp >= 1000) {
                $(this).find("a").attr("href", $(this).find("a").attr("url"))
            }
        });
    });
</script>
<div id="sd_bdl" class="bdl" style="width: 130px; margin-left: -145px">
    <div id="diyleftsidetop" class="area">
    </div>
    <div class="tbn" id="forumleftside">
        <h2 class="bdl_h">
            意见分类</h2>
        <%=strTyleInfo%>
    </div>
    <div id="diyleftsidebottom" class="area">
    </div>
</div>
