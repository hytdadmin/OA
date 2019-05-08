<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PublishSay.ascx.cs" Inherits="Work_UserControl_PublishSay" %>
      <div style="height:23px;">
        <div class="divSay_l">来，说说你的工作内容</div>
        <div class="divSay_f">发言请遵守社区公约，还可以输入<span id="spanLenth">500</span>字</div>
      </div>
      <div class="divSay_txt">
      <textarea id="txtJournal" class="txt_say" onkeyup="setTextareaLength(this.id,'spanLenth',500)"></textarea>
      </div>
      <div class="divSay_btn">
      <%--<asp:ImageButton runat="server" ID="imgBtnSay" OnClientClick="return checkSay()"  
              ImageUrl="~/Work/Images/btn_say.jpg" onclick="imgBtnSay_Click"/>--%>
              <a href="javascript:publishJournalDiv();"><img src="images/btn_say.jpg" /></a>
      </div>
      