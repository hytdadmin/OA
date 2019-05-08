// JavaScript Document
$(function () {
    /*tr隔行换色JS*/
    $('table.odd-even tbody').each(function () {
        $(this).children('tr:odd').addClass('odd');
        $(this).children('tr:even').addClass('even');
    });
    //左边菜单背景高度
    $(".left").css("height", $(window).height() - $(".content_all").height()-5);
    getUrl();
});

function getUrl() {
    (function ($) {
        $.getUrlParam = function (name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
    })(jQuery);
    //当前页面的菜单展开
    var pitemNo = $.getUrlParam('pitemNo'); //父菜单id
    if (pitemNo != null && pitemNo != "")
        $("#m"+pitemNo+"_container").css("display", "block");
}

//=====================================文本框获取、失去焦点
function onFocus(obj, str, className) {
    if (document.getElementById(obj).value == str) {
        document.getElementById(obj).value = "";
    }
    document.getElementById(obj).className = className;
}
function onBlur(obj, str, className) {
    if (document.getElementById(obj).value == "") {
        document.getElementById(obj).value = str;
    }
    document.getElementById(obj).className = className;
}


 //===============================比较开始时间和结束时间的大小（时间差是否大于指定的）
function CompareTimes(startDate, endDate) {
    //    alert(txtStar + "aaaaaaaaaaaaaaaaaaa" + txtEnd);
    //    var startDate = document.getElementById(txtStar).value;
    //    var endDate = document.getElementById(txtEnd).value;
    //    alert(startDate + ":" + endDate);
    if (startDate != "" && endDate != "") {
        var checkbegin = new Array();
        var checkend = new Array();
        var begin = startDate.substring(0, 10);
        var end = endDate.substring(0, 10);
        checkbegin = begin.split("-");
        checkend = end.split("-");
        var beginTime = new Date(checkbegin[0], (parseInt(checkbegin[1]) - 1), checkbegin[2]);
        var endTime = new Date(checkend[0], (parseInt(checkend[1]) - 1), checkend[2]);
        var sDate = endTime.getTime() - beginTime.getTime();
        var passDate = Math.floor(sDate / (24 * 3600 * 1000));
        if (passDate < 0) {
//            document.getElementById("divMessage").innerHTML = "开始日期不能大于结束日期！";
            //            document.getElementById("pnl11").style.display = "block";
            alert("开始日期不能大于结束日期");
            return false;
        }
    }
    return true;
}
