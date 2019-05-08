//日志列表


//分页
function InitDataOl(pageindx) {
    var id = request("uid");
    if (id == null || id == "") {
        alert("参数错误");
        return;
    }
    var keyValue = $("#txtKeyVal").val();
    var searchDate = $("#search_date").val();
    var tbody = "";
    //$("#divFinanceBydesktop").html("");
    $.ajax({
        type: "POST", //用POST方式传输
        url: 'UserControl/Others_JournalList.ashx', //目标地址
        data: "p=" + (pageindx + 1) + "&keyValue=" + keyValue + "&uId=" + id+"&searchDate=" + searchDate,
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
                    callback: pageselectCallbackOl,
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
function pageselectCallbackOl(page_id, jq) {
    //  alert(page_id);
    InitDataOl(page_id);
}


