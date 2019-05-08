

//发表工作日志
function publishJournalDivO() {
    var maxLength = 500;
    var contents = $("#txtJournal").val();
    if (checkJournal()) {
        $.get("UserControl/PublishJournal.ashx", { contents: contents }, function (result) {
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
            else if (result == "yes") {
                $("#txtJournal").val("");
                meJournalOKByChange(contents);
            } else {
                alert("日志发布失败，请重试");
                $("#txtJournal").focus();
                return;
            }
        });
    }
}



//日志列表


//分页
function InitDataO(pageindx,depId) {
    var keyValue = $("#txtKeyVal").val();
    var tbody = "";
    var searchDate = $("#search_date").val();
    //$("#divFinanceBydesktop").html("");
    $.ajax({
        type: "POST", //用POST方式传输
        url: 'UserControl/getJournalListOthers.ashx', //目标地址
        data: "p=" + (pageindx + 1) + "&keyValue=" + keyValue + "&searchDate=" + searchDate + "&depId=" + depId,
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
                    callback: pageselectCallbackO,
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

//点击分页
function pageselectCallbackO(page_id, jq) {
    //  alert(page_id);
    InitDataO(page_id, $(".depB").attr("id"));
}


