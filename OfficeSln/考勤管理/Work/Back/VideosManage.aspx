<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideosManage.aspx.cs" MasterPageFile="~/Work/Back/MasterPage.master" Inherits="Work_Back_VideosManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <input type='button' id='reflash' value='更新视频内容' />
    </div>
     <div>
        <div>
            <p>&nbsp;</p>

            <h1 style='font-size:large;'>视频内容授权</h1>
        </div>
        <hr />
        <div id='UserInfoContainer'></div>   
    </div>
    <div style='width:766px; margin-top:100px; display:block;float:left;'>
        <h1 style='font-size:large;'>视频类别</h1>
        <hr style='width:766px;' />
        <div id='VideoCategory'></div>

    </div>
    
    
    <style>
        .Users
        {
            width:100px;
            background-color:Gray
            }
        .group
        {
            float:left;
            border-width:1px;
            border-color:black;
            border-style:solid;
            margin:5px;
            text-align:center
            }
         
        .Category
        {
            float:left;
            margin:10px;
            width:100px;
            
            }
            
         .Category li
        {
            height:20px;
            color:White;
            cursor:pointer;
            }
            
        .Departement
        {
            float:left;
            margin:10px;
            width:100px;
            
            }
        .Departement li
        {
            height:20px;
            color:White;
            cursor:pointer
            }
         
        .Categoryhead
        {
            background-color:Green;
            width:100px;
            height:20px;
            color: White
            }
            
        .Departementhead
        {
            background-color:Yellow;
            width:100px;
            height:20px
            }
        .VideoCategory
        {
            margin-top:10px;
            margin-bottom:10px;
            }
        
    </style>
    <script src="../Scripts/jquery-1.10.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var InitDepartMentData;
            var InitCategoryData;

            $.ajax({
                url: '../UserControl/VideoManage.ashx',
                async: false,
                type: 'POST',
                data: 'action=Department',
                success: function (result) {
                    InitDepartMentData = $.parseJSON(result);
                },
                error: function () {

                }
            });

            $.ajax({
                url: '../UserControl/VideoManage.ashx',
                type: 'POST',
                async: false,
                data: 'action=Category',
                success: function (result) {
                    console.log(result);
                    InitCategoryData = $.parseJSON(result);

                },
                error: function () {

                }
            });

            $.ajax({
                url: '../UserControl/VideoManage.ashx',
                type: 'POST',
                async: false,
                data: 'action=GetCategories',
                success: function (result) {
                    console.log(result);
                    var Categories = $.parseJSON(result);
                    InitCategories(Categories);

                },
                error: function () {

                }
            });

            $("#reflash").click(function () {
                $.ajax({
                    url: '../UserControl/VideoManage.ashx',
                    type: 'POST',
                    data: 'action=reflash',
                    success: function (result) {
                        alert("视频已经更新成功");

                        window.location.reload(true);
                    },
                    error: function () {

                    }
                });
            });


            function InitDepartMentUsers(obj, CategoryID) {

                var Users = [];
                Users.push("<div class='Departement'>");
                if (obj) {
                    for (var department in obj) {
                        Users.push("<p class='Departementhead'>" + obj[department]['RoleName'] + "</p><ul CategoryID='" + CategoryID + "' departmentname='" + obj[department]['RoleName'] + "' name='departmentUsers' class='Users'>")
                        for (var user in obj[department]['Users']) {
                            if (CategoryID == obj[department]['Users'][user]['CategoryID'])
                                Users.push("<li title='双击点选人员' departmentname='" + obj[department]['RoleName'] + "' CategoryID='" + CategoryID + "' UserCode = '" + obj[department]['Users'][user]['UserCode'] + "'>" + obj[department]['Users'][user]['UserName'] + "</li>")
                        }
                        Users.push("</ul>")
                    }

                }
                Users.push("</div>");
                console.log(Users.join(''));
                return Users.join('');
            }

            function InitCategoryUsers(obj) {
                if (obj) {

                    for (var category in obj) {
                        var cg = [];
                        cg.push("<div class='Category'><p class='Categoryhead'>" + (obj[category]['Description'] == null ? obj[category]['VideoCategoryName'] : obj[category]['Description']) + "</p><ul CategoryID='" + obj[category]['ID'] + "' name='categoryUsers' class='Users'>");
                        for (var user in obj[category]['Users']) {
                            if (obj[category]['Users'][user]['UserName'] != null)
                                cg.push("<li title='双击点选人员' departmentname='" + obj[category]['Users'][user]['RoleName'] + "' CategoryID='" + obj[category]['ID'] + "' UserCode='" + obj[category]['Users'][user]['UserCode'] + "' >" + obj[category]['Users'][user]['UserName'] + "</li>")
                        }
                        cg.push('</ul></div>')




                        cg.unshift("<div><div class='group'>视&nbsp;频&nbsp;分&nbsp;类&nbsp;/&nbsp;部&nbsp;门&nbsp;人&nbsp;员<div>");

                        cg.push(InitDepartMentUsers(InitDepartMentData, obj[category]['ID']));


                        cg.push("</div>");

                        $("#UserInfoContainer").append(cg.join(''));


                    }

                }
            }

            InitCategoryUsers(InitCategoryData);

            function AjaxAddUser(obj) {
                var UserCode = $(obj).attr("UserCode");
                var CategoryID = $(obj).attr("CategoryID");
                var self = obj;
                $.ajax({
                    url: '../UserControl/VideoManage.ashx',
                    async: false,
                    type: 'POST',
                    data: 'action=AddUser&UserCode=' + UserCode + "&CategoryID=" + CategoryID,
                    success: function (result) {
                        if (result == "true")
                            Addsuccess(self);
                        else
                            fail();
                    },
                    error: function () {
                        fail();
                    }
                });
            }

            function AjaxDelUser(obj) {
                var UserCode = $(obj).attr("UserCode");
                var CategoryID = $(obj).attr("CategoryID");
                var self = obj;
                $.ajax({
                    url: '../UserControl/VideoManage.ashx',
                    async: false,
                    type: 'POST',
                    data: 'action=DelUser&UserCode=' + UserCode + "&CategoryID=" + CategoryID,
                    success: function (result) {
                        if (result == "true")
                            DelSuccess(self);
                        else
                            fail();
                    },
                    error: function () {
                        fail();
                    }
                });
            }



            $("ul[name=departmentUsers] li").on("dblclick", function () {
                AjaxAddUser(this);
            });

            $("ul[name=categoryUsers] li").on("dblclick", function () {
                AjaxDelUser(this);
            });





            function Addsuccess(obj) {
                var categoryID = $(obj).attr("CategoryID");

                $(obj).clone(false).dblclick(function () { AjaxDelUser(this); }).appendTo($("ul[name = categoryUsers][categoryid =" + categoryID + "]"));
                $(obj).remove();

            }

            function DelSuccess(obj) {
                var departmentname = $(obj).attr("departmentname");
                var categoryid = $(obj).attr("CategoryID");
                $(obj).clone(false).dblclick(function () { AjaxAddUser(this); }).appendTo($("ul[name = departmentUsers][departmentname = " + departmentname + "][CategoryID = " + categoryid + "]"));
                $(obj).remove();
            }

            function fail() {
                alert('删除失败，请刷新页面，重新尝试删除');
            }

            function InitCategories(obj) {
                if (obj) {
                    var categories = [];
                    categories.push("<ul id='VideoCategories'>");
                    for (var item in obj) {
                        categories.push("<li class='VideoCategory' CategoryID='" + obj[item]['ID'] + "' VideoCategoryName ='" + obj[item]['VideoCategoryName'] + "' >视频分类默认名称：<span>" + obj[item]['VideoCategoryName'] + "</span>&nbsp;&nbsp;别名：<input type=text value='" + (obj[item]['Description'] == null ? '' : obj[item]['Description']) + "'/></li>");
                    }
                    categories.push("</ul>");
                    $("#VideoCategory").append(categories.join(''));
                }

            }

            $("#VideoCategories input").blur(function () {
                var Description = $(this).val();
                var CategoryID = $(this).parent().attr('CategoryID');
                var VideoCategoryName = $(this).parent().attr('VideoCategoryName');
                if (Description.trim() != '') {
                    $.ajax({
                        url: '../UserControl/VideoManage.ashx',
                        async: false,
                        type: 'POST',
                        data: 'action=UpdateCategory&VideoCategoryName=' + VideoCategoryName + '&CategoryID=' + CategoryID + '&Description=' + Description,
                        success: function (result) {
                            if (result == "true")

                                window.location.reload();
                        },
                        error: function () {
                            fail();
                        }
                    });
                }
            });



        });


        
    </script>
</asp:Content>
