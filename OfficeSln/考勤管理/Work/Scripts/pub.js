//添加输入但没有点击发布的日志内容
document.onclick = function (e) {
    //    if ($("#txtJournal").length == 0 || $("#txtJournal").val().length == 0)
    //        return;
    //    var txt = encodeURI($("#txtJournal").val());
    //    var evt = e || event;
    //    var srcEl = evt.target || evt.srcElement;
    //    if (srcEl.tagName != "A") { return; }

    //    var url = srcEl.getAttribute('href');
    //    if (url && url.indexOf(".aspx") > 0) {
    //        //location = url + "&p=mm_pid";
    //        url += url.indexOf("?") > 0 ? "&val=" + txt : "?val=" + txt;
    //        srcEl.setAttribute("href", url);
    //    }
    //点击其他地方隐藏表情框
    if ($("#ExpressionShow").css("display") == "block") {
        $("#ExpressionShow").css("display", "none");
    }
    //点击其他地方，把页面链接地址添加已输入的工作内容，这样输入半截的工作内容不会丢失
    if ($("#txtJournal").length != 0 && $("#txtJournal").val().length != 0) {
        var txt = encodeURI($("#txtJournal").val());
        var evt = e || event;
        var srcEl = evt.target || evt.srcElement;
        if (srcEl.tagName != "A") { return; }

        var url = srcEl.getAttribute('href');
        if (url && url.indexOf(".aspx") > 0) {
            //location = url + "&p=mm_pid";
            url += url.indexOf("?") > 0 ? "&val=" + txt : "?val=" + txt;
            srcEl.setAttribute("href", url);
        }
    }
}
$(document).ready(function () {
    //ctrl+enter发表日志
    $('#txtJournal').keypress(function (e) {
        if (e.ctrlKey && e.which == 13 || e.which == 10) {
            publishJournalDiv();
        }
    });
    $(document).keyup(function (e) {
        var key = e.which;
        if (key == 27) {
            $("#SD_close").click();
        }
    });

    //返回顶部开始
    //首先将#back-to-top隐藏
    $("#back-to-top").hide();
    //当滚动条的位置处于距顶部100像素以下时，跳转链接出现，否则消失
    $(function () {
        $(window).scroll(function () {
            if ($(window).scrollTop() > 100) {
                $("#back-to-top").fadeIn(1500);
            }
            else {
                $("#back-to-top").fadeOut(1500);
            }
        });
        //当点击跳转链接后，回到页面顶部位置
        $("#back-to-top").click(function () {
            $('body,html').animate({ scrollTop: 0 }, 1000);
            return false;
        });
    });
    //返回顶部结束

    //记录输入但没有保存的工作日志信息
    if ($("#txtJournal").length == 0 || $("#txtJournal").val().length != 0)
        return;
    $("#txtJournal").val(decodeURI(request("val")));
    setTextareaLength("txtJournal", "spanLenth", 500);
    $("#txtJournal").focus();
});
//显示修改头像div
function ChangeHeadImg() {
    $.get("/Work/UserControl/ChangeHeadImg.htm", { contents: "" }, function (result) {
        showDialog("window", result, "修改头像", 500);
        $("#image").attr("src", $("#userDiv_headImg").attr("src"));
        $('#flUpload').val("");
        $("#changeHeadImg").css("display", "block");
    });

}

//修改头像div中确定按钮
function ChangeHeadImgOK() {
    //当真实修改了头像
    if ($("#image").attr("src") != $("#userDiv_headImg").attr("src")) {
        var src = $("#image").attr("src");
        var newimg = src.substring(src.lastIndexOf("/") + 1, src.length);
        $.get("/Work/UserControl/PicUploadHander.ashx", { typeId: 1, hedImg: newimg }, function (result) {
            if (result == "sessionError") {
                ashxVerifySession();
            } else if (result == "parError") {
                alert("请选择一个图片文件，再点击确定");
                return;
            }
            else if (result == "false") {
                alert("修改头像失败，请重试");
                return;
            }
            else if (result == "true") {
                $("#userDiv_headImg").attr("src", $("#image").attr("src"));
                $("#SD_close").click();
            }
        });
    }
    //$("#changeHeadImg").css("display", "none");
}


//textarea最大字数限制，还能输入字数
function setTextareaLength(id, setValId, length) {
    var len = fucCheckLength($("#" + id).val());
    if (len > length) {
        //$("#" + id).val($("#" + id).val().substring(0, length));
        $("#" + id).val(cutstr($("#" + id).val(), length * 2, "no"));
        //return;
    }
    $("#" + setValId).html(Math.ceil(length - fucCheckLength($("#" + id).val())) + "");
}

//退出
function exit() {
    if (confirm("是否返回登陆页面！")) {
        location.href = "/Login.aspx?exti=0";
    }

}

//一般处理程序中session失效，退出
function ashxVerifySession(str) {
    //alert('会话过期，请重新登录');
    location.href = "../Login.aspx?exti=0";
}


//=====================================文本框获取、失去焦点
function onFocus(obj, str) {
    if ($("#" + obj).val() == str) {
        $("#" + obj).val("");
    }
}
function onBlur(obj, str) {
    if ($("#" + obj).val() == "") {
        $("#" + obj).val(str);
    }
}


/*
页面公共部门、用户控件开始
*/
//用户信息
function getUserUC() {
    $.get("/Work/UserControl/UserUC.ashx", { typeId: 1 }, function (result) {
        if (result == "sessionError") {
            ashxVerifySession();
        }
        else {
            $("#div_user").html(result);
            if (getUrlName().indexOf("ndex.aspx") != -1) {
                $(".img").attr("src", $("#userDiv_headImg").attr("src"));
            }
            if ($(".W_main").height() > 600)
                $(".W_main_left").css("height", $(".W_main").height());
            //            else {
            //                //$(".W_main").css("height", "600px");
            //                $(".W_main_left").css("height", "600px");
            //            }
        }
    });
}


//通知公告
function getBulletinUC() {
    $.get("/Work/UserControl/BulletinUC.ashx", { typeId: 1 }, function (result) {
        if (result == "sessionError") {
            ashxVerifySession();
        }
        else {
            $("#div_Bulletins").html(result);
            $("#div_Bulletins").css("border-top", "1px solid #D48656");
        }
    });
}

//大家正在说
function getSayUC() {
    $.get("/Work/UserControl/SayUC.ashx", { typeId: 1 }, function (result) {
        if (result == "sessionError") {
            ashxVerifySession();
        }
        else {
            $("#divleft_say").html(result);
            $("#div_Bulletins").css("border-bottom", "1px solid #D48656");
        }
    });
}

////点击分页
//function pageselectCallbackSay(page_id, jq) {
//    //  alert(page_id);
//    getSayUC(page_id);
//}

////分页
//function getSayUC(pageindx) {
//    $.ajax({
//        type: "POST", //用POST方式传输
//        url: '/Work/UserControl/SayUC.ashx', //目标地址
//        data: "sayp=" + (pageindx + 1) ,
//        success: function (result) {
//            if (result == "sessionError") {
//                ashxVerifySession();
//            } else if (result != "parError") {
//                $("#divleft_say").html(result);
//            }
//            clearPage();
//            //如果没有数据
//            if ($("#pagecountSay").val() > 0) {
//                $("#PaginationSay").pagination($("#pagecountSay").val(), {
//                    callback: pageselectCallbackSay,
//                    prev_text: '<',
//                    next_text: '>',
//                    items_per_page: $("#pageSizeSay").val(),
//                    num_display_entries: 0,
//                    current_page: pageindx,
//                    num_edge_entries: 0
//                });
//            }
//        }
//    });
//}

//发表说说
function divSayAlertDisplay(dis) {
    //    //$("#divSayAlert").css("display", dis);
    //    var str = "<div class=\"divText\">            <textarea id=\"txtSay\" class=\"text\" onkeyup=\"setTextareaLength(this.id,'spanSayLenth',50)\"></textarea>        </div>        <a href=\"javascript:publishSayDiv()\" class=\"aright\"><img src='/Work/images/send.jpg' /></a><div class=\"skpe\"><span class=\"span\"><a href=\"javascript:showExpDiv('block')\"  id=\"apic\"><img src=\"../../Image/Expression/bq.jpg\"/></a>  还可以输入<span id=\"spanSayLenth\">50</span>字</span></div>";
    //    //var divStr = "<div class=\"divSayAlert\" id=\"divSayAlert\">" + $("#divSayAlert").html() + "</div>";
    //    var divStr = "<div class=\"divSayAlert\" id=\"divSayAlert\">" + str + "</div>";
    $.get("/Work/UserControl/PublishSayAlert.htm", { contents: "" }, function (result) {
        showDialog("window", result, "发表说说", 460);
        $("#spanSayLenth").html(50);
        if (dis == "block")
            $("#txtSay").focus();
    });

}
//发表说说
function publishSayDiv() {
    var maxLength = 50;
    var contents = $("#txtSay").val();
    if (checkSayDiv()) {
        //contents = EscapeJs(contents);
        $.get("/Work/UserControl/PublishSayAlert.ashx", { contents: contents }, function (result) {
            if (result == "sessionError") {
                ashxVerifySession();
            } else if (result == "parError") {
                alert("说说内容不能为空");
                $("#txtSay").focus();
                return;
            } else if (result == "lengthError") {
                alert("输入的字数过多，最多可输入" + maxLength + "字");
                $("#txtSay").focus();
                return;
            }
            else if (result == "yes") {
                sayOKByChange(contents);
                $("#txtSay").val("");
                $("#SD_close").click();
            } else {
                alert("说说发布失败，请重试");
                $("#txtSay").focus();
                return;
            }
        });
    }
}

//发表说说验证
function checkSayDiv() {
    var maxLength = 50;
    var content = $("#txtSay").val();
    if (content.length == 0) {
        alert("说说内容不能为空");
        $("#txtSay").focus();
        return false;
    }
    if (fucCheckLength(content) > maxLength) {
        alert("输入的字数过多，最多可输入" + maxLength + "字");
        $("#txtSay").focus();
        return false;
    }
    return true;
}

//说说发布成功后，用户信息控件和大家正在说控件信息修改
function sayOKByChange(contents) {
    //用户信息更新最新说说
    var userSay = contents;
    //$("#userDiv_Say").html(EscapeJs(cutstr(userSay, 30, "yes")));

    $.get("/Work/UserControl/Expression.ashx", { typeId: 1, PicType: "showPic", picStr: EscapeJs(cutstr(userSay, 30, "yes")) }, function (result) {
        $("#userDiv_Say").html(result);
    });
    //大家正在说更新最新说说
    //var userAllSay = EscapeJs(contents).replace(/\n/g, "<br>"); ;
    $.get("/Work/UserControl/Expression.ashx", { typeId: 1, PicType: "showPic", picStr: EscapeJs(contents).replace(/\n/g, "<br>") }, function (result) {
        var addLi = "<li class=\"divleft_say_list_li\"><span class=\"divleft_say_list_title\">" + $("#userDiv_userName").html() + "</span>： <span class=\"divleft_say_list_content\">" + GetstrByName(result) + "</span> </li>";
        $("#divleft_say_ul").html(addLi + $("#divleft_say_ul").html());
        $("#divleft_say_ul li:last").remove();
    });
}

//发表工作日志
function publishJournalDiv() {
    var maxLength = 500;
    var contents = $("#txtJournal").val();
    if (checkJournal()) {
        //contents = EscapeJs(contents);
        $.get("/Work/UserControl/PublishJournal.ashx", { contents: contents }, function (result) {
            if (result == "sessionError") {
                ashxVerifySession();
            } else if (result == "parError") {
                alert("日志内容不能为空");
                $("#txtJournal").focus();
                return;
            } else if (result == "lengthError") {
                alert("输入的字数过多，最多可输入" + maxLength + "字");
                $("#txtJournal").focus();
                return;
            }
            else if (result > 0) {
                $("#txtJournal").val("");
                $("#spanLenth").html("500");
                meJournalOKByChange(contents, result);
            } else {
                alert("日志发布失败，请重试");
                $("#txtJournal").focus();
                return;
            }
        });
    }
}

//发表工作日志验证
function checkJournal() {
    var maxLength = 500;
    var content = $("#txtJournal").val();
    if (content.length == 0) {
        alert("日志内容不能为空");
        $("#txtJournal").focus();
        return false;
    }
    if (fucCheckLength(content) > maxLength) {
        alert("输入的字数过多，最多可输入" + maxLength + "字");
        $("#txtJournal").focus();
        return false;
    }
    return true;
}


/**
页面公共部门、用户控件开始
*/

//工作日志发布成功后，日志列表修改
function meJournalOKByChange(contents, addId) {
    contents = EscapeJs(contents).replace(/\n/g, "<br>");
    if (getUrlName().indexOf("ndex.aspx") != -1) {
        //我自己的页面 日志列表更新
        //var addLi = "<dl id=\"dl" + addId + "\"><dd class=\"del\"><a href=\"javascript:DelMyJournal('" + addId + "')\">删除</a></dd><dd>" + contents + "</dd><dd style=\"text-indent: 0em;\"><span>刚刚</span></dd></dl>";
        var addLi = "<dl id=\"dl" + addId + "\">";
        addLi += "<table>";
        addLi += "<tr>";
        addLi += "<td rowspan=\"3\" class=\"tdimg\"><img src=\"" + $("#userDiv_headImg").attr("src") + "\" class=\"img\"/></td>";
        addLi += "<td class=\"tdtitle\"><span name=\"spanNameAll\"></span></td>";
        addLi += "</tr>";
        addLi += "<tr>";
        addLi += "<td class=\"tdcontent\">" + contents + "</td>";
        addLi += "</tr>";
        addLi += "<tr>";
        addLi += "<td class=\"tddate\" style=\"width: 100%;\">5分钟内<a href=\"javascript:DelMyJournal('" + addId + "')\" style=\"float:right;color: #0A8CD2;\">删除</a></td>";
        addLi += "</tr>";
        addLi += "</table>";
        addLi += "</dl>";


        if ($("#right_conten_mr dl").length >= 10) {
            $("#right_conten_mr dl:last").remove();
        }
        $("#right_conten_mr").html(addLi + $("#right_conten_mr").html());
        //列表数量+1
        $("#lblCount").html($("#lblCount").html() * 1 + 1);
        //分页列表数量+1
    } else if (getUrlName().indexOf("Others.aspx") != -1) {
        //var addLi = "<dl><dd class=\"userName\">" + $("#userDiv_userName").html() + "</dd><dd>" + contents + "</dd><dd style=\"text-indent: 0em;\"><span>" + getDate() + "</span></dd></dl>";
        var addLi = "<table><tr><td rowspan=\"3\" class=\"tdimg\"><img src=\"" + $("#userDiv_headImg").attr("src") + "\" class=\"img\"/></td><td class=\"tdtitle\">" + $("#userDiv_userName").html() + "</td></tr><tr><td class=\"tdcontent\">" + contents + "</td></tr><tr><td class=\"tddate\">5分钟内</td></tr></table>";
        if ($("#right_conten_mr dl").length >= 10) {
            $("#right_conten_mr dl:last").remove();
        }
        $("#right_conten_mr").html(addLi + $("#right_conten_mr").html());
        //列表数量+1
        $("#lblCount").html($("#lblCount").html() * 1 + 1);
    }
}

//日志列表

//点击分页
function pageselectCallback(page_id, jq) {
    //  alert(page_id);
    InitData(page_id);
}

//分页
function InitData(pageindx) {
    var keyValue = $("#txtKeyVal").val();
    var tbody = "";
    var searchDate = $("#search_date").val();
    $("#divFinanceBydesktop").html("");
    $.ajax({
        type: "POST", //用POST方式传输
        url: '/Work/UserControl/getJournalList.ashx', //目标地址
        data: "p=" + (pageindx + 1) + "&keyValue=" + keyValue + "&searchDate=" + searchDate,
        beforeSend: function () { $("#divload").show(); $("#Pagination").hide(); }, //发送数据之前
        complete: function () { $("#divload").hide(); $("#Pagination").show(); }, //接收数据完毕
        success: function (result) {
            if (result == "sessionError") {
                ashxVerifySession();
            } else if (result != "parError") {
                $("#right_content").html(result);
            }
            clearPage();
            //如果没有数据
            if ($("#pagecount").val() > 0) {
                $("#Pagination").pagination($("#pagecount").val(), {
                    callback: pageselectCallback,
                    prev_text: '<',
                    next_text: '>',
                    items_per_page: $("#pageSize").val(),
                    num_display_entries: 6,
                    current_page: pageindx,
                    num_edge_entries: 2
                });
            }
        }
    });
}

//重置分页
function clearPage() {
    $("#Pagination").pagination(0, {
        callback: "",
        prev_text: '<',
        next_text: '>',
        items_per_page: 0,
        num_display_entries: 6,
        current_page: 0,
        num_edge_entries: 2
    });
}

//我的日志中删除事件
function DelMyJournal(id) {
    if (id == null || id == 0) {
        alert("参数错误");
        return;
    } var flag = confirm("确定要删除吗？");
    if (flag == null || flag == false) {
        return;
    }
    $.get("/Work/UserControl/DelJournal.ashx", { id: id }, function (result) {
        if (result == "sessionError") {
            ashxVerifySession();
        } else if (result == "parError")
            alert("参数错误");
        else if (result == "true") {
            //$("#right_content").html(result);
            $("#dl" + id).remove();
            var count = $("#lblCount").html() * 1 - 1 == 0 ? "0" : $("#lblCount").html() * 1 - 1;
            $("#lblCount").html(count);
        } else if (result == "false")
            alert("删除失败，请重新操作");
    });
}



//日期处理
//补0函数
function appendZero(s) {
    return ("00" + s).substr((s + "").length);
}

//月日不加0
function getDate() {
    var d = new Date();
    return d.getFullYear() + "-" + d.getMonth() + 1 + "-" + d.getDate();
}

//月日加0
function getDate() {
    var d = new Date();
    return d.getFullYear() + "-" + appendZero(d.getMonth() + 1) + "-" + appendZero(d.getDate())
}

//获取当前页面名称
function getUrlName() {
    var strUrl = window.location.href;
    var arrUrl = strUrl.split("/");
    var strPage = arrUrl[arrUrl.length - 1];
    return strPage;
}

//获取页面参数
function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (var i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}
//尖括号转移，防止脚本注入
function EscapeJs(str) {
    str = str.replace(/</g, "&lt;");
    str = str.replace(/>/g, "&gt;");
    return str;
}


//验证字节长度
function fucCheckLength(strTemp) {
    var i, sum;
    sum = 0;
    for (i = 0; i < strTemp.length; i++) {
        if ((strTemp.charCodeAt(i) >= 0) && (strTemp.charCodeAt(i) <= 255)) {
            sum = sum + 1;
        } else {
            sum = sum + 2;
        }
    }
    return sum / 2;
}
//js截取字符串，中英文都能用  
//如果给定的字符串大于指定长度，截取指定长度返回，否者返回源字符串。  
//字符串，长度  

/** 
* js截取字符串，中英文都能用 
* @param str：需要截取的字符串 
* @param len: 需要截取的长度 
*/
function cutstr(str, len, type) {
    var str_length = 0;
    var str_len = 0;
    str_cut = new String();
    str_len = str.length;
    for (var i = 0; i < str_len; i++) {
        a = str.charAt(i);
        str_length++;
        if (escape(a).length > 4) {
            //中文字符的长度经编码之后大于4  
            str_length++;
        }
        str_cut = str_cut.concat(a);
        if (str_length >= len) {
            if (type == "yes")
                str_cut = str_cut.concat("...");
            return str_cut;
        }
    }
    //如果给定字符串小于指定长度，则返回源字符串；  
    if (str_length < len) {
        return str;
    }
}

//@人名添加颜色
function GetstrByName(str) {
    var strbu;
    if (str.indexOf("@") >= 0) {
        str += " ";

        var strGroup = str.split('@');
        for (var i = 0; i < strGroup.length; i++) {
            if (i == 0) {
                strbu = strGroup[0];
                continue;
            }
            var s = strGroup[i];
            var len = s.indexOf(" ");
            if (len > 0 && len <= 3)//人名在三个以内
            {
                strbu += "<span style=\"color:#0A8CD2\">@" + s.substring(0, s.indexOf(" ")) + "</span>" + s.substring(s.indexOf(" "));
            }
            else {
                strbu += "@" + s;
            }
        }
        //str = str.length > 0 && str.substring(0, str.length) == " " ? str.substring(0, str.length) : str;
    }
    else
        strbu = str;
    return strbu;
}


//添加访问记录
function addVisit() {
    $.get("/Work/UserControl/VisitManage.ashx", { type: "add", pageName: getUrlName() }, function (result) {
        if (result == "sessionError") {
            ashxVerifySession();
        }
    });
}
//显示在线人数
function showVisit() {
    $.get("/Work/UserControl/VisitManage.ashx", { type: "show" }, function (result) {
        if (result == "sessionError") {
            ashxVerifySession();
        } else {
            $("#spanShowVisit").html(result + "");
        }
    });
}

//快速通道
function kuaisutongdao() {
    var html = "<div> <span class=\"sp_kuaisutongdao\">快速通道</span></div>" + "<div> <ul class=\"td_ul\"><li><a target=\"\" href=\"/Index.aspx\">" +
    "<img  alt=\"个人考勤\" src=\"../../Work/tupian/kaoqin.jpg\"></a></li><li><a href=\"../../Posts/index.aspx\" target=\"_self\">" +
    "<img  alt=\"问题与反馈\" src=\"../../Work/tupian/wentiyufankui.jpg\"></a></li><li><a target=\"_blank\" href=\"https://mail.hyitech.com/owa\">" +
    "<img  alt=\"邮箱\" src=\"../../Work/tupian/youxiang.jpg\"></a></li><li><a target=\"_blank\" href=\"http://192.168.10.146:8028/mantisbt/login_page.php\">" +
    "<img  alt=\"Mantis\" src=\"../../Work/tupian/mantis.jpg\"></a></li></li><li><a target=\"\" href=\"/work/Back/Photos.aspx\">" +
    "<img  alt=\"图片展\" src=\"../../Work/tupian/tupianzhan.jpg\"></a></li><div style=\"clear:both;\"></div></ul></div>";
    $("#dv_tongdao").html(html);
    
}