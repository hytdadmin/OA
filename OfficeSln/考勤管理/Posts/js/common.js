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
function Search(info) {
    if (info != "" && info != "请输入搜索内容") {
        window.location.href = "/Posts/index.aspx?ss=" + info + "";
    }
    else {
        alert("请输入查询内容");
    }
}

function CheckWord(info) {
    var backcheck = true;
    $.ajax({
        type: "post",
        url: "/Posts/Ashx/Control.ashx",
        data: { "info": info, "type": "check" },
        async: false,
        success: function (data) {
            if (data == "false") {
                alert("输入内容包含敏感字符，请重新输入");
                backcheck = false;
            }
            else {
                backcheck = true;
            }
        }

    })
    return backcheck;
}
//删除回复
function DeleteReply(replyId) {
    var pageIndex = $("#hdPageIndex").val();
    var fatherID = $("#hdFather").val();
    if (window.confirm("你确认删除吗?")) {
        $.get("/Posts/Ashx/RePostsAshx.ashx", { opt: "delete", replyId: replyId }, function (result) {
            if (result == "fail") {
                alert('删除失败');
            }
            else {
                window.location.href = "detail.aspx?pageIndex=" + pageIndex + "&fatherID=" + fatherID + "&num=0";
            }
        });
    }
    return false;
}
//删除主题
function DeleteTopic(id) {
    if (window.confirm("你确认删除吗?")) {

        $.get("/Posts/Ashx/PostsAshx.ashx", { opt: "delete", id: id }, function (result) {
            if (result == "fail") {
                alert('删除失败');
            }
            else {
                window.location.href = "index.aspx";
            }
        });
    }
    return false;
}
//删除回复日志页面专用
function DeleteReplyforLog(replyId, tp) {
    if (window.confirm("你确认删除吗?")) {
        if (tp == 1) {
            $.get("/Posts/Ashx/PostsAshx.ashx", { "opt": "delete", "id": replyId }, function (result) {
                if (result == "fail") {
                    alert('删除失败');
                }
                else {
                    window.location.reload();
                }
            });
        }
        else {
            $.get("/Posts/Ashx/RePostsAshx.ashx", { "opt": "delete", "replyId": replyId }, function (result) {
                if (result == "fail") {
                    alert('删除失败');
                }
                else {
                    window.location.reload();
                }
            });
        }
    }
}
//恢复回复日志页面专用
function RecoverReplyforLog(replyId, tp) {
    if (window.confirm("你确认恢复吗?")) {
        if (tp == 1) {
            $.get("/Posts/Ashx/PostsAshx.ashx", { "opt": "recover", "id": replyId }, function (result) {
                if (result == "fail") {
                    alert('恢复失败');
                }
                else {
                    window.location.reload();
                }
            });
        }
        else {
            $.get("/Posts/Ashx/RePostsAshx.ashx", { "opt": "recover", "replyId": replyId }, function (result) {
                if (result == "fail") {
                    alert('恢复失败');
                }
                else {
                    window.location.reload();
                }
            });
        }
    }
}

function returnPage() {
    window.location.replace(window.location.href.replace("page", "fy_none"));
}