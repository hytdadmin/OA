//bulletins.aspx页面
///<reference path='jquery-1.7.2.min.js'>

//分页
    function InitDataDown(pageindx) {
        

        var CategoryName = arguments[1] || '';

        pageindx = pageindx ? pageindx : 1;
        
        var tbody = "";
        //$("#divFinanceBydesktop").html("");
        $.ajax({
            type: "POST", //用POST方式传输
            url: 'UserControl/VideoCenter.ashx', //目标地址
            data: "p=" + (pageindx) + "&CategoryName=" + CategoryName,
            beforeSend: function () { $("#divload").show(); $("#Pagination").hide(); }, //发送数据之前
            complete: function () { $("#divload").hide(); $("#Pagination").show(); }, //接收数据完毕
            success: function (result) {
                if (result == "sessionError") {
                    ashxVerifySession();
                } else if (result != "parError") {

                   
                    eval("var obj = " + result);

                    var Categories = CreateHtml(obj)

                    $("#right_content").html(Categories.title + "</br>" + Categories.body);


                    var totalpage = obj[0] ? obj[0]['RowCount'] : 0;
                    $("#Pagination").html(PageDivide(totalpage, pageindx));
                }
                //clearPage();
                //如果没有数据
                //            if ($("#pagecount").val() > 0) {
                //                $("#Pagination").pagination($("#pagecount").val(), {
                //                    callback: pageselectCallbackDown,
                //                    prev_text: '<',
                //                    next_text: '>',
                //                    items_per_page: $("#pageSize").val(),
                //                    num_display_entries: 6,
                //                    current_page: pageindx,
                //                    num_edge_entries: 2
                //                });
                //            }
            }
        });



    }

    function CreateHtml(obj) {
        var title = [];
        var body = [];
        title.push("<div><ul><li style='float:left;list-style-type:none'>| <a href='javascript:loadVideos();'>全部</a></li>");
        body.push("<div>");
        for (var p in obj) {
            title.push("<li style='float:left;list-style-type:none'>| <a href=javascript:loadVideos('" + obj[p].VideoCategoryName + "');>" + (obj[p].Description == null ? obj[p].VideoCategoryName : obj[p].Description ) + "</a></li>");
             
            for (var video in obj[p]['Videos']) {
                body.push("<br/><p onclick= javascript:showdiv('" + obj[p]['Videos'][video]["Url"].replace(/\\/g,"/") + "'); style='cursor: pointer'>" + obj[p]['Videos'][video]['Name'] + "</p></br>")
            }

            
        }
        title.push("</ul></div>");
        body.push("</div>");



        return {
            title:title.join(''),
            body:body.join('')
        }

    }

    function PageDivide(totalrow,current) {
        var page = [];
        var i = Math.ceil(totalrow / 10);
        var first = ''
        var last =''
        for(var j =1;j<=i;j++) {
            if (j == current) {
                if (j == 1)
                    first = '';
                else
                    first = "<span onclick=javascript:pageselectCallbackDown('" + (j - 1) + "')>&lsaquo;&lsaquo;<span>";
                page.push("<span class='disable'>" + j + "</span>");
                if (j == i)
                    last = '';
                else
                    last = "<span onclick=javascript:pageselectCallbackDown('" + (j + 1) + "')>&rsaquo;&rsaquo;</span>";
            }
            else {
                page.push("<a class='current' onclick=javascript:pageselectCallbackDown('" + j + "')>" + j + "</a>")
            }
                
        }
        page.unshift(first);
        page.push(last);
        return page.join('');
    }





    //点击分页
    function pageselectCallbackDown(page_id, jq) {
        //  alert(page_id);
        InitDataDown( + page_id);
    }

   



    //遮罩层
    function showdiv(url) {
        flowplayer("player",
                        {
                            src: "../Work/Components/flowplayer-3.2.18.swf",
                            height: '500px',
                            width: '700px'
                        },
                        {
                            clip: {
                                url:url,
                                autoplay:true
                            }
                                
                            
                        }
                   );
       
        document.getElementById("bg").style.display = "block";
        document.getElementById("show").style.display = "block";
    }
    function hidediv() {
        document.getElementById("bg").style.display = 'none';
        document.getElementById("show").style.display = 'none';
    }


    function loadVideos(CategoryName)
    {
        var categoryID = CategoryName ? CategoryName : '';
        InitDataDown(1, CategoryName);

    }