;(function($){
    $.fn.selectForK13 = function(options){
        var defaults = {
            "width": "",  //设置宽度
            "id": "",  //追加id
            "class": ""  //追加class
        }
        var options = $.extend(defaults, options);

        return this.each(function(){
            $(this)
                .wrap("<div class='K13_select'></div>")  //模块包裹
                .before("<div class='K13_select_checked'>" + $(this).children("option:checked").text() + "</div><div class='K13_select_list'><ol></ol></div>")  // 插入结构及默认选中的内容
                .children("option")  //将默认选项内容添加至自定义结构中
                    .each(function () {
                        $(this)
                            .parent("select").siblings(".K13_select_list").children("ol")
                                .append("<li>" + $(this).text() + "</li>");
                    })
                .end()  //返回之前操作
                .hide()  //隐藏select
                .parent(".K13_select")
                    .click(function () {  //模拟点击下拉菜单
                        $(this).css("z-index","10000").children(".K13_select_list").slideToggle("fast");
                    })
                    .mouseleave(function () {  //鼠标一开下拉关闭
                        $(this).css("z-index","9999").children(".K13_select_list").slideUp("fast");
                    })
                .children(".K13_select_list").find("li")
                    .click(function () {  //点击模拟下拉选项，改变select选项值和显示选中内容
                        $(this)
                            .parents(".K13_select_list").siblings(".K13_select_checked")
                                .text($(this).text())
                            .siblings("select").children("option").eq($(this).index())
                                .attr("selected",true)
                            .siblings("option")
                                .attr("selected",false);
                               if($(this).parents(".K13_select_list").siblings(".K13_select_checked")
                                .text($(this).text()).siblings("select").attr("id")!="s1")
                               getinfopost();
                    });

            if (options.width) {
                $(this).parent(".K13_select").css("width",options.width);
            }

            if (options.id) {
                $(this).parent(".K13_select").attr("id",options.id)
            }

//            if (options.class) {
//                $(this).parent(".K13_select").addClass(options.class);
//            }

        })     
    };
})(jQuery);