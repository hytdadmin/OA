//bulletins.aspx页面



//分页
function InitDataBulletinList(pageindx) {
    var keyValue = $("#txtKeyVal").val();
    var tbody = "";
    //$("#divFinanceBydesktop").html("");
    $.ajax({
        type: "POST", //用POST方式传输
        url: 'UserControl/GetBulletinList.ashx', //目标地址
        data: "p=" + (pageindx + 1) + "&keyValue=" + keyValue,
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
                    callback: pageselectCallbackBulletinList,
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
function pageselectCallbackBulletinList(page_id, jq) {
    //  alert(page_id);
    InitDataBulletinList(page_id);
}


//bulletinDetail.aspx

//获取公告详情
function getBulletinDeatil() {
    var id = request("id");
    if (id == null || id == "") {
        alert("参数错误");
        return;
    }
    $.get("/Work/UserControl/GetBulletinDetail.ashx", { id: id }, function (result) {
        if (result == "sessionError") {
            ashxVerifySession();
        } else if (result == "parError")
            alert("参数错误");
        else {
            $("#right_content").html(result);
        }
    });
}

