<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myd.aspx.cs" Inherits="myd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="myd/q.css" rel="stylesheet" type="text/css">
    <link href="myd/newsolid_38.css" rel="stylesheet" type="text/css">
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script language="javascript">

        var IP = "";
        var CustomerID = "0";
        function submitInfo() {
            //问题是否解决
            var intIsSolve = 0;
            intIsSolve = $("#divquestion1").find("input[type=radio]:checked").val();
            //服务是否满意
            var intServiceEvaluation = 0;
            intServiceEvaluation = $("#divquestion2").find("input[type=radio]:checked").val();
            //效率是否满意
            var intServiceEfficiency = 0;
            intServiceEfficiency = $("#divquestion3").find("input[type=radio]:checked").val();
            //态度是否满意
            var intServiceAttitude = 0;
            intServiceAttitude = $("#divquestion4").find("input[type=radio]:checked").val();
            //工程师编号             
            var strServiceUserCode = 0;
            strServiceUserCode = $("#divquestion5").find("input[type=radio]:checked").val();
            if (strServiceUserCode == 4)
                strServiceUserCode = $("#divquestion5").find("input[id=userName]").val();
            //期望有何改进
            var strImprovement = 0;
            strImprovement = $("#divquestion6").find("textarea").val();
            var info1 = intIsSolve + "$$" + intServiceEvaluation + "$$" + intServiceEfficiency + "$$"
            + intServiceAttitude + "$$" + strServiceUserCode + "$$" + strImprovement + "$$" + IP + "|<% =strCustomerIP %>"  + "$$" + CustomerID;
//            alert("intIsSolve:" + intIsSolve + " intServiceEvaluation:" + intServiceEvaluation + " intServiceEfficiency:" + intServiceEfficiency
//            + " intServiceAttitude:" + intServiceAttitude + " strServiceUserCode:" + strServiceUserCode + " strImprovement:" + strImprovement);

            $.getJSON("/CallCenter/CallCenter.ashx", { option: "myd", info: info1 }, function (data) {
                //回调函数中对返回值进行一系列操作
                alert(10);
                alert(data);
            });
        }

        jQuery(function ($) {
            var url = 'http://chaxun.1616.net/s.php?type=ip&output=json&callback=?&q=' + Math.random();
            $.getJSON(url, function (data) {
                IP = data.Ip;
            });
        });
        $().ready(function () {
            $("#submit_button").click(function () {
                //                submitInfo();
                //问题是否解决
                var intIsSolve = 0;
                intIsSolve = $("#divquestion1").find("input[type=radio]:checked").val();
                //服务是否满意
                var intServiceEvaluation = 0;
                intServiceEvaluation = $("#divquestion2").find("input[type=radio]:checked").val();
                //效率是否满意
                var intServiceEfficiency = 0;
                intServiceEfficiency = $("#divquestion3").find("input[type=radio]:checked").val();
                //态度是否满意
                var intServiceAttitude = 0;
                intServiceAttitude = $("#divquestion4").find("input[type=radio]:checked").val();
                //工程师编号             
                var strServiceUserCode = 0;
                strServiceUserCode = $("#divquestion5").find("input[type=radio]:checked").val();
                if (strServiceUserCode == 4)
                    strServiceUserCode = $("#divquestion5").find("input[id=userName]").val();
                //期望有何改进
                var strImprovement = 0;
                strImprovement = $("#divquestion6").find("textarea").val();
                var info1 = intIsSolve + "$$" + intServiceEvaluation + "$$" + intServiceEfficiency + "$$"
            + intServiceAttitude + "$$" + strServiceUserCode + "$$" + strImprovement + "$$" + IP + "|<% =strCustomerIP %>" + "$$" + CustomerID;
                //            alert("intIsSolve:" + intIsSolve + " intServiceEvaluation:" + intServiceEvaluation + " intServiceEfficiency:" + intServiceEfficiency
                //            + " intServiceAttitude:" + intServiceAttitude + " strServiceUserCode:" + strServiceUserCode + " strImprovement:" + strImprovement);

                $.post("/CallCenter/CallCenter.ashx", { option: "myd", info: info1 }, function (data) {
                    //回调函数中对返回值进行一系列操作
                    if (data != "") {
                        alert(data);
                    }
                });
                // alert("您的意见已提交，感谢您的参与！");
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divNotRun" style="height: 100px; text-align: center; display: none;">
    </div>
    <div id="jqContent" class="" style="text-align: left;">
        <div id="headerCss" style="overflow-x: hidden; overflow-y: hidden;">
            <div id="ctl00_header">
            </div>
        </div>
        <div id="mainCss">
            <div id="mainInner">
                <div id="box">
                    <style>
                        .interface
                        {
                            text-align: left;
                            border: solid 1px #ff9900;
                            background: #FDEADA;
                            color: #333333;
                            vertical-align: middle;
                            margin: 20px auto;
                            width: 760px;
                            height: 32px;
                            line-height: 32px;
                            padding: 0 4px;
                        }
                        html
                        {
                            overflow-x: hidden;
                            overflow-y: auto;
                        }
                    </style>
                    <div class="survey" style="margin: 0px auto;">
                        <div id="ctl00_ContentPlaceHolder1_JQ1_divHead" class="surveyhead" style="border: 0px;">
                            <h1 id="ctl00_ContentPlaceHolder1_JQ1_h1Name" style="position: relative;">
                                <span id="ctl00_ContentPlaceHolder1_JQ1_lblQuestionnaireName">环宇通达正版化售后服务满意度调查</span>
                                <span id="ctl00_ContentPlaceHolder1_JQ1_lblMobile" style="position: absolute; right: 0;
                                    top: -15px;"></span>
                            </h1>
                            <div style="clear: both;">
                            </div>
                            <div id="ctl00_ContentPlaceHolder1_JQ1_divDec" class="surveydescription">
                                <span id="ctl00_ContentPlaceHolder1_JQ1_lblQuestionnaireDescription" style="vertical-align: middle;">
                                    亲爱的朋友您好，感谢您在百忙之中抽空完成此问卷，帮助我们了解您对正版华产品的使用和服务效果。本问卷填写一律不记名，请放心评价。<br>
                                    对您的参与我们表示十分感谢，祝您万事顺意，合家美满。<br>
                                    <br>
                                    答题提示：<br>
                                    1. 请在题后答案的号码上打“√”，或者在“______”上填写适当的答案<br>
                                    2. 每个题目分别标记“单选”、“多选”或“跳题”回答，请注意括号内提示。<br>
                                    3. 选项无对错之分，只需根据个人情况作答。<br>
                                </span>
                            </div>
                            <div style="clear: both;">
                            </div>
                        </div>
                        <div id="ctl00_ContentPlaceHolder1_JQ1_question" class="surveycontent">
                            <div id="ctl00_ContentPlaceHolder1_JQ1_divDisplayPageNum">
                                <style type="text/css">
                                    legend
                                    {
                                        display: none;
                                    }
                                    fieldset
                                    {
                                        border: 0px;
                                        margin: 0;
                                        padding: 0;
                                    }
                                </style>
                            </div>
                            <div id="ctl00_ContentPlaceHolder1_JQ1_surveyContent">
                                <fieldset class="fieldset" id="fieldset1">
                                    <legend><span style="font: 14px"></span></legend>
                                    <div class="div_question" id="div1">
                                        <div class="div_title_question_all">
                                            <div class="div_topic_question">
                                                <b>1.</b></div>
                                            <div id="divTitle1" class="div_title_question">
                                                您的问题是否已经解决？</div>
                                            <div style="clear: both;">
                                            </div>
                                        </div>
                                        <div class="div_table_radio_question" id="divquestion1">
                                            <div class="div_table_clear_top">
                                            </div>
                                            <ul class="ulradiocheck">
                                                <li>
                                                    <input id="q1_1" name="q1" value="1" type="radio" /><label for="q1_1">是</label></li><li>
                                                        <input id="q1_2" name="q1" value="2" type="radio" /><label for="q1_2">否</label></li>
                                            </ul>
                                            <div style="clear: both;">
                                            </div>
                                            <div class="div_table_clear_bottom">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="div_question" id="div2">
                                        <div class="div_title_question_all">
                                            <div class="div_topic_question">
                                                <b>2.</b></div>
                                            <div id="divTitle2" class="div_title_question">
                                                您对工程师的服务及时性如何评价？【如：上门是否准时，远程协助回复是否及时】</div>
                                            <div style="clear: both;">
                                            </div>
                                        </div>
                                        <div class="div_table_radio_question" id="divquestion2">
                                            <div class="div_table_clear_top">
                                            </div>
                                            <ul class="ulradiocheck">
                                                <li>
                                                    <input name="q2" id="q2_1" value="1" type="radio" /><label for="q2_1">非常满意</label></li><li>
                                                        <input name="q2" id="q2_2" value="2" type="radio" /><label for="q2_2">满意</label></li><li>
                                                            <input name="q2" id="q2_3" value="3" type="radio" /><label for="q2_3">一般</label></li><li>
                                                                <input name="q2" id="q2_4" value="4" type="radio" /><label for="q2_4">不满意</label></li><div
                                                                    style="clear: both;">
                                                                </div>
                                            </ul>
                                            <div style="clear: both;">
                                            </div>
                                            <div class="div_table_clear_bottom">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="div_question" id="div3">
                                        <div class="div_title_question_all">
                                            <div class="div_topic_question">
                                                <b>3.</b></div>
                                            <div id="divTitle3" class="div_title_question">
                                                您对工程师解决问题的效率如何评价？【如：是否能准确判断问题原因，并及时有效提供解决方案】</div>
                                            <div style="clear: both;">
                                            </div>
                                        </div>
                                        <div class="div_table_radio_question" id="divquestion3">
                                            <div class="div_table_clear_top">
                                            </div>
                                            <ul class="ulradiocheck">
                                                <li>
                                                    <input id="q3_1" name="q3" value="1" type="radio" /><label for="q3_1">非常满意</label></li><li>
                                                        <input id="q3_2" name="q3" value="2" type="radio" /><label for="q3_2">满意</label></li><li>
                                                            <input id="q3_3" name="q3" value="3" type="radio" /><label for="q3_3">一般</label></li><li>
                                                                <input id="q3_4" name="q3" value="4" type="radio" /><label for="q3_4">不满意</label></li>
                                            </ul>
                                            <div style="clear: both;">
                                            </div>
                                            <div class="div_table_clear_bottom">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="div_question" id="div4">
                                        <div class="div_title_question_all">
                                            <div class="div_topic_question">
                                                <b>4.</b></div>
                                            <div id="divTitle4" class="div_title_question">
                                                您工程师服务态度如何评价？<span style="color: red;">&nbsp;</span>【如：是否耐心听取您的问题反馈，是否耐心解决您反馈的问题】</div>
                                            <div style="clear: both;">
                                            </div>
                                        </div>
                                        <div class="div_table_radio_question" id="divquestion4">
                                            <div class="div_table_clear_top">
                                            </div>
                                            <ul class="ulradiocheck">
                                                <li>
                                                    <input id="q4_1" name="q4" value="1" type="radio" /><label for="q4_1">非常满意</label></li><li>
                                                        <input id="q4_2" name="q4" value="2" type="radio" /><label for="q4_2">满意</label></li><li>
                                                            <input id="q4_3" name="q4" value="3" type="radio" /><label for="q4_3">一般</label></li><li>
                                                                <input id="q4_4" name="q4" value="4" type="radio" /><label for="q4_4">不满意</label></li>
                                            </ul>
                                            <div style="clear: both;">
                                            </div>
                                            <div class="div_table_clear_bottom">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="div_question" id="div5">
                                        <div class="div_title_question_all">
                                            <div class="div_topic_question">
                                                <b>5.</b></div>
                                            <div id="divTitle5" class="div_title_question">
                                                为您服务的工程师编号？（单选）<span style="color: red;">&nbsp;*</span></div>
                                            <div style="clear: both;">
                                            </div>
                                        </div>
                                        <div class="div_table_radio_question" id="divquestion5">
                                            <div class="div_table_clear_top">
                                            </div>
                                            <ul class="ulradiocheck">
                                                <li>
                                                    <input name="q5" id="q5_1" value="1" type="radio" /><label for="q5_1">001</label></li><li>
                                                        <input name="q5" id="q5_2" value="2" type="radio" /><label for="q5_2">002</label></li><li>
                                                            <input name="q5" id="q5_3" value="3" type="radio" /><label for="q5_3">003</label></li>
                                                <li style="width: 300px;">
                                                    <input name="q5" id="q5_4" value="4" type="radio" /><label for="q5_4">其他</label>
                                                    <input style="color: rgb(153, 153, 153); position: static;" class="underline" value="请注明..."
                                                        rel="q25_12" type="text" id="userName"></li>
                                                <div style="clear: both;">
                                                </div>
                                            </ul>
                                            <div style="clear: both;">
                                            </div>
                                            <div class="div_table_clear_bottom">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="div_question" id="div6">
                                        <div class="div_title_question_all">
                                            <div class="div_topic_question">
                                                <b>6.</b></div>
                                            <div id="divTitle6" class="div_title_question">
                                                您希望我们有何改进？<span style="color: red;">&nbsp;*</span></div>
                                            <div style="clear: both;">
                                            </div>
                                        </div>
                                        <div class="div_table_radio_question" id="divquestion6">
                                            <div class="div_table_clear_top">
                                            </div>
                                            <ul class="ulradiocheck">
                                                <li style="">
                                                    <textarea style="color: rgb(153, 153, 153); position: static; width: 500px; height: 60px;"
                                                        class="underline" value="请注明..." rel="q6_4" name="q6_4" onpropertychange="lengthChange(this);"
                                                        oninput="lengthChange(this);"></textarea></li><div style="clear: both;">
                                                        </div>
                                            </ul>
                                            <div style="clear: both;">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="register_div">
                                        <div style="display: none;" id="divpoweredby">
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div style="margin-top: 6px; clear: both;" id="submit_div">
                                <table id="submit_table" style="margin: 20px auto;">
                                    <tbody>
                                        <tr>
                                            <td id="ctl00_ContentPlaceHolder1_JQ1_tdCode" style="display: none;">
                                                <input id="yucinput" size="14" maxlength="10" onkeydown="enter_clicksub(event);"
                                                    name="yucinput" style="height: 24px; line-height: 24px; border: 1px solid #7F9DB9;"
                                                    type="text">&nbsp;&nbsp;<img id="imgCode" alt="验证码" title="看不清吗？点击可以刷新" style="vertical-align: bottom;
                                                        cursor: pointer; display: none;">
                                            </td>
                                            <td>
                                                <div id="divCaptcha" style="display: none;">
                                                    <img alt="验证码" title="看不清吗？点击可以刷新" captchaid="" instanceid="">
                                                </div>
                                            </td>
                                            <td>
                                                <input class="submitbutton" value="提交结果" onmouseout="this.className='submitbutton';"
                                                    id="submit_button" type="button">
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <div id="divPreviewQ" style="display: none;">
                                       
                                                </div>
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td align="right" valign="bottom">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <div style="clear: both;">
                                </div>
                            </div>
                            <div id="divMinTime" style="display: none; position: absolute; width: 140px; font-size: 14px;
                                color: #666666;">
                                还剩<span style="color: Red; font-weight: bold;" id="spanMinTime"></span>秒后才能继续
                            </div>
                            <div id="submit_tip" style="display: none; background-color: #f04810; color: White;
                                margin-bottom: 20px; padding: 10px">
                            </div>
                            <div style="display: none;" id="divNA">
                                <input value="1" name="divNA" id="divNA_1" type="radio"><label for="divNA_1">A.男</label><input
                                    value="2" name="divNA" id="divNA_2" type="radio"><label for="divNA_1">B.女</label>
                            </div>
                        </div>
                        <div id="ctl00_ContentPlaceHolder1_JQ1_divLeftBar" style="text-align: center; position: absolute;
                            width: 50px; padding: 8px 0px; left: 1147px; top: 328px;" class="leftbar">
                            <div id="divProgressBar">
                                <style>
                                    #loading
                                    {
                                        background: url(myd/bgProgressBg.gif) no-repeat 0px 0px;
                                        height: 120px;
                                        width: 15px;
                                        float: left;
                                        border: 1px #d6ebf7 solid;
                                    }
                                    #loadcss
                                    {
                                        display: block; /*很重要, 弄成块*/
                                        background: url(myd/ProgressBarbar.gif);
                                        background-repeat: repeat;
                                        background-attachment: fixed;
                                        text-align: center;
                                        width: 15px;
                                        line-height: 15px;
                                    }
                                </style>
                                <div style="text-align: left;">
                                    <span id="loadprogress" style="font-weight: bold; visibility: hidden;">&nbsp;&nbsp;0%</span>
                                </div>
                                <div id="ctl00_ContentPlaceHolder1_JQ1_divProgressImg" style="float: left; padding-left: 15px;
                                    visibility: hidden;">
                                    <div id="loading" title="已答题比率">
                                        <span id="loadcss" style="height: 0%; line-height: 0; font-size: 0; overflow: hidden;">
                                        </span>
                                    </div>
                                </div>
                                <div style="float: left; width: 14px; line-height: 0;" id="divSaveText">
                                </div>
                                <div class="divclear">
                                </div>
                            </div>
                            <div style="float: left; padding-left: 2px; visibility: hidden;">
                            </div>
                            <div style="clear: both;">
                            </div>
                        </div>
                        <div style="clear: both;">
                        </div>
                        <script type="text/javascript">
                            var needAvoidCrack = 0;
                        </script>
                        <script type="text/javascript" src="myd/iplookup.htm"></script>
                        <script type="text/javascript">
                            if (window.remote_ip_info) {
                                cProvince = window.remote_ip_info.province;
                                cCity = remote_ip_info.city;
                                cIp = "124.207.6.102";
                            }
                        </script>
                        <div style="clear: both;">
                        </div>
                    </div>
                </div>
            </div>
            <div style="clear: both;">
            </div>
            <div style="margin: 30px auto 0; padding-top: 30px; overflow: hidden; width: 100%;">
            </div>
        </div>
        <div id="footercss">
            <div id="footerLeft">
            </div>
            <div id="footerCenter">
            </div>
            <div id="footerRight">
            </div>
        </div>
        <div style="clear: both; height: 10px;">
        </div>
        <div style="height: 20px;">
            &nbsp;</div>
    </div>
    <div style="clear: both;">
    </div>
    ﻿<div style="display: none;">
        <a href="#" title="站长统计">站长统计</a>
    </div>
    </form>
</body>
</html>
