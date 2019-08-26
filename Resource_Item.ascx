<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Resource_Item.ascx.cs" Inherits="DNNGo.Modules.DNNGalleryProGame.Resource_Item" %>
<!-- start: PAGE HEADER -->
<%-- <div class="row">
    <div class="col-sm-12">
        <!-- start: PAGE TITLE & BREADCRUMB -->
        <div class="page-header">
            <h1><i class="fa fa-plus"></i> <%=ViewResourceText("Header_Title", "Skin Options")%></h1>
        </div>
        <!-- end: PAGE TITLE & BREADCRUMB -->
    </div>
</div>--%>
<!-- end: PAGE HEADER -->
        <div class="row"> 
          <!-- start: PAGE CONTENT -->
          <div class="col-sm-12">
            <ul id="myTab_ul_tabs" class="ul_tabs nav nav-tabs tab-bricky">
              <asp:Literal runat="server" ID="liNavTabsHTML"></asp:Literal>
            </ul>
            <div class="tab-content">

                <asp:Repeater ID="RepeaterCategories" runat="server" OnItemDataBound="RepeaterCategories_ItemDataBound">
                    <ItemTemplate>
                     <div class="tab-pane <%#(Container.ItemIndex==0)?"in active":""%>" id="tabs-left-<%#FormatName( Eval("Key"))%>">
                        <div id="accordion<%#FormatName( Eval("Key"))%>">
                          <asp:Repeater ID="RepeaterGroup" runat="server" OnItemDataBound="RepeaterGroup_ItemDataBound">
                            <ItemTemplate>
                                 <div class="panel panel-default small-bottom">
                                    <div class="panel-heading"> <i class="fa fa-external-link-square"></i> <%# Eval("Key")%>
                                      <div class="panel-tools"> <a class="btn btn-xs btn-link panel-collapse collapses <%#(Container.ItemIndex==0)?"expand":"collapsed"%>" data-toggle="collapse" data-parent="#accordion<%#FormatName( Eval("Parent"))%>" href="#options_<%#FormatName( Eval("Key"))%>"></a> </div>
                                    </div>
                                    <div id="options_<%#FormatName( Eval("Key"))%>" class="panel-collapse <%#(Container.ItemIndex==0)?"in":"collapse"%>" style="height: <%#(Container.ItemIndex == 0)?"auto":"0px"%>;">
                                      <div class="panel-body">
                                       
                                        <div class="form-horizontal  form-patch">
                                        <div class="col-md-12">
                                            <asp:Repeater ID="RepeaterOptions" runat="server" OnItemDataBound="RepeaterOptions_ItemDataBound">
                                                <ItemTemplate>
                                                       <div class="form-group">
                                                        <asp:Literal ID="liTitle" runat="server"></asp:Literal> <asp:Label ID="lbRequired" runat="server" Text="" CssClass="symbol required"></asp:Label>
                                                      
                                                            <asp:PlaceHolder ID="ThemePH" runat="server" ></asp:PlaceHolder>
                                                            <asp:Literal ID="liHelp" runat="server"></asp:Literal>
                                                      
                                                      </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                          </div>  
                                            </div>
                                         </div>
                                    </div>
                                  </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                  </div>
                 </ItemTemplate>
                </asp:Repeater>
            </div>

<br />

            <div class="row"> <div class="col-sm-12"> 

                  <div class="form-group">
                         <asp:Button CssClass="btn btn-primary" lang="Submit" ID="cmdUpdate" resourcekey="cmdUpdate"
                            runat="server" Text="Update" OnClick="cmdUpdate_Click"></asp:Button>&nbsp;
                        <asp:Button CssClass="input_button btn" lang="Submit" ID="cmdReset" resourcekey="cmdReset" runat="server"
                            Text="Reset" OnClick="cmdReset_Click"></asp:Button>&nbsp;
                        
                        
                        </div>
                  </div>
            </div>


          </div>
        </div>

        <!-- end: PAGE CONTENT--> 
        <div id="UrlLink_Modal" class="modal fade" tabindex="-1" data-width="820"
    data-height="400" style="display: none;">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
            &times;
        </button>
        <h4 class="modal-title">
            <i class='fa fa-folder-open'></i>Set Picture</h4>
    </div>
    <div class="modal-body">
        <iframe id="UrlLink_Iframe" width="100%" height="100%" style="border-width: 0px;">
        </iframe>
    </div>
</div>