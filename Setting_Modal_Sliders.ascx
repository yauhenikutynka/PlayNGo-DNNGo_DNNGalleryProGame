<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Setting_Modal_Sliders.ascx.cs" Inherits="DNNGo.Modules.DNNGalleryProGame.Setting_Modal_Sliders" %>
<div class="container">
 <div class="row">
        <div class="col-sm-8">
         <div class="form-group">
                <asp:TextBox ID="txtSearch" runat="server" placeholder="Search Text Field" x-webkit-speech></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" onclick="btnSearch_Click"  resourcekey="btnSearch" />
            </div>
        </div>
        

      </div>
     <div class="row">
        <div class="col-sm-8">
        <div class="form-group">
          <button class="btn btn-primary btn-sm" type="button" onclick="return  ReturnAttachments();"> Submit </button>
          </div>
        </div>
        <div class="col-sm-3 text_right">
        	<div class="control-inline"><asp:Label ID="lblRecordCount" runat="server"></asp:Label></div>
        </div>
      </div>

      <!-- start-->
      <div class="form-group">
             <asp:GridView ID="gvEventList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvEventList_RowDataBound" OnRowCreated="gvEventList_RowCreated" OnSorting="gvEventList_Sorting" AllowSorting="true"
                        Width="98%" CellPadding="0" cellspacing="0" border="0" CssClass="table table-striped table-bordered table-hover"  GridLines="none" >
                        <Columns>
                             
                             <asp:TemplateField HeaderText="Picture">
                                <ItemTemplate>
                                    <asp:Image  runat="server" ID="imgFileName" style=" max-height:80px;max-width:80px;" />        
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" /> 
                            <asp:BoundField DataField="LastUser" HeaderText="Author" SortExpression="LastUser" /> 
                            <asp:BoundField DataField="LastTime" HeaderText="CreateTime" SortExpression="LastTime"  HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs"/> 
                             <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"/> 
                
                            
                        </Columns>
                        <PagerSettings Visible="False" />
                    </asp:GridView>
      </div>
      <!-- end--> 
      <!-- start-->
      <div class="row">
        <div class="col-sm-8">
           <ul id="paginator-EventList" class="pagination-purple"></ul>
            <script type="text/javascript">
                $(document).ready(function () {
                    $('#paginator-EventList').bootstrapPaginator({
                        bootstrapMajorVersion: 3,
                        currentPage: <%=PageIndex %>,
                        totalPages: <%=RecordPages %>,
                        numberOfPages:7,
                        useBootstrapTooltip:true,
                        onPageClicked: function (e, originalEvent, type, page) {
                            window.location.href='<%=CurrentUrl %>&PageIndex='+ page;
                        }
                    });
                });

             


            function ReturnAttachments() {
                var jsons = [];
                var checkok = false;
                $("input[type='checkbox'][title]:checked").each(function (index, domEle) { 
                    checkok = true;
  
                    jsons.push(eval($(domEle).data("json"))); 
                });
                 if (!checkok) {
                     alert("<%=ViewResourceText("lblcheckconfirm", "Please select the records needs to be Selected!")%>");
                 }
                 else {
                    window.parent.InsetRelations(jsons);
                    window.parent.$('#Relations_Modal').modal('hide');
                 }
                return false;
            }
         <!--
             function SelectAll() {
                 var e = document.getElementsByTagName("input");
                 var IsTrue;
                 if (document.getElementById("CheckboxAll").value == "0") {
                     IsTrue = true;
                     document.getElementById("CheckboxAll").value = "1"
                 }
                 else {
                     IsTrue = false;
                     document.getElementById("CheckboxAll").value = "0"
                 }
                 for (var i = 0; i < e.length; i++) {
                     if (e[i].type == "checkbox") {
                         e[i].checked = IsTrue;
                     }
                 }
             }
     
 
        // -->
            </script>

          
        </div>
      </div>
      <!-- end--> 
</div>