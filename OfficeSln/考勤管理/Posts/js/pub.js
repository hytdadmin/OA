
$(document).ready(function () {
    //左侧导航点击事件   添加选中样式
    $("#lf_1 a").click(function () {
        $(this).parents("dl").each(function () {
            $('dd', this).removeClass("bdl_a");
        });

        $(this).parents("dd").addClass("bdl_a");
        getinfopost();
    });
    //列表分类样式(全部问题、热点问题、已反馈问题等)
    $("#thread_types a").click(function () {
        $(this).parents("ul").each(function () {
            $('li', this).removeClass("xw1");
            $('li', this).removeClass("a");
        });

        $(this).parents("li").addClass("xw1 a");
        getinfopost();
    });


});


//textarea最大字数限制，还能输入字数
function setTextareaLength(id, setValId, length) {
    var len = $("#" + id).val().length;
    if (len > length) {
        $("#" + id).val($("#" + id).val().substring(0, length));
        return;
    }
    $("#" + setValId).html((length - $("#" + id).val().length) + "");
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
    return sum;
}
//js截取字符串，中英文都能用  
//如果给定的字符串大于指定长度，截取指定长度返回，否者返回源字符串。  
//字符串，长度  
  
/** 
 * js截取字符串，中英文都能用 
 * @param str：需要截取的字符串 
 * @param len: 需要截取的长度 
 */  
function cutstr(str,len,type)  
{  
	var str_length = 0;  
	var str_len = 0;  
	str_cut = new String();  
	str_len = str.length;  
	for(var i = 0; i < str_len; i++)  
	{  
		a = str.charAt(i);  
        str_length++;  
        if(escape(a).length > 4)  
        {  
         	//中文字符的长度经编码之后大于4  
         	str_length++;  
   		}  
    	str_cut = str_cut.concat(a);  
    	if(str_length>=len) {
    	    if (type == "yes")
    	        str_cut = str_cut.concat("...");
         	return str_cut;  
      	}  
	}  
    //如果给定字符串小于指定长度，则返回源字符串；  
    if(str_length < len){  
     	return  str;  
	}
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