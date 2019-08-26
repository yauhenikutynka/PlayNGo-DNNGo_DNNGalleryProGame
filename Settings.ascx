<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="DNNGo.Modules.DNNGalleryProGame.Settings" %>
<!-- start: PAGE HEADER -->
<div class="row">
    <div class="col-sm-12">
        <!-- start: PAGE TITLE & BREADCRUMB -->
        
        <div class="page-header">
            <h1>
                <i class="fa fa-wrench"></i>
                <%=ViewResourceText("Header_Title", "General Settings")%></h1>
        </div>
        <!-- end: PAGE TITLE & BREADCRUMB -->
    </div>
</div>
<!-- end: PAGE HEADER -->
<!-- start: PAGE CONTENT -->
<div class="row">
    <div class="col-sm-12">
 




        <div class="panel panel-default">
            <div class="panel-heading">
                <i class="fa fa-external-link-square"></i>
                <%=ViewTitle("lblDataSettings", "Data Settings")%>
                <div class="panel-tools">
                    <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                </div>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <%=ViewControlTitle("lblCopyOfOtherModule", "Copy Of Other Module", "cbCopyOfOtherModule", ":", "col-sm-3 control-label")%>
                        <div class="col-sm-5">
                            <div class="control-inline auto checkbox-inline">
                                <asp:CheckBox ID="cbCopyOfOtherModule" runat="server" />
                             </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <%=ViewControlTitle("lblPortals", "Portals", "ddlPortals", ":", "col-sm-3 control-label")%>
                        <div class="col-sm-5">
                            <div class="control-inline">
                                <asp:DropDownList ID="ddlPortals" runat="server" CssClass="form-control input_text validate[required]"
                                    OnSelectedIndexChanged="ddlPortals_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <%=ViewControlTitle("lblTabModule", "Tab & Module", "ddlTabModule", ":", "col-sm-3 control-label")%>
                        <div class="col-sm-5">
                            <div class="control-inline">
                                <asp:DropDownList ID="ddlTabModule" runat="server" CssClass="form-control input_text validate[required]"></asp:DropDownList>
                                
                                </div>
                        </div>
                    </div>
                    
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <i class="fa fa-external-link-square"></i>
                <%=ViewTitle("lblDataFilter", "Data Filter")%>
                <div class="panel-tools">
                    <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                </div>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <%=ViewControlTitle("lblSortby", "Sort by", "ddlSortby", ":", "col-sm-3 control-label")%>
                        <div class="col-sm-5">
                            <div class="control-inline">
                               <asp:DropDownList ID="ddlSortby" runat="server"  CssClass="form-control input_text validate[required]"></asp:DropDownList>
                            </div>
                            <asp:Panel ID="divSortbyHelp" runat="server" Visible="false">
                                <span class="help-block"><i class="fa fa-info-circle"></i> <%=ViewResourceText("Sortby", "In the current effect, it is invalid to select [ Title ASC ] and [ Title DESC ]","Help")%></span>
                            </asp:Panel>
                        </div>
                    </div>
                    
                    
                   <div class="form-group">
                        <%=ViewControlTitle("lblFilterStartTime", "Filter Start Time", "cbFilterStartTime", ":", "col-sm-3 control-label")%>
                        <div class="col-sm-5">
                            <div class="control-inline auto checkbox-inline">
                               <asp:CheckBox ID="cbFilterStartTime" runat="server"  />
                               <%=ViewHelp("lblFilterStartTime", "Requires 'Start Time' as a condition of data filter.")%>
                            </div>
                        </div>
                    </div>


                  <div class="form-group">
                        <%=ViewControlTitle("lblFilterEndTime", "Filter End Time", "cbFilterEndTime", ":", "col-sm-3 control-label")%>
                        <div class="col-sm-5">
                            <div class="control-inline auto checkbox-inline">
                               <asp:CheckBox ID="cbFilterEndTime" runat="server"  />
                               <%=ViewHelp("lblFilterEndTime", "Requires 'End Time' as a condition of data filter.")%>
                            </div>
                        </div>
                    </div>
                   
                </div>
            </div>
        </div>
    </div>
</div>
<!-- end: PAGE CONTENT-->
<div class="row">
    <div class="col-sm-2">
    </div>
    <div class="col-sm-10">
        <asp:Button CssClass="btn btn-primary" lang="Submit" ID="cmdUpdate" resourcekey="cmdUpdate"
            runat="server" Text="Update" OnClick="cmdUpdate_Click"></asp:Button>&nbsp;
        <asp:Button CssClass="btn btn-default" ID="cmdCancel" resourcekey="cmdCancel" runat="server"
            Text="Cancel" CausesValidation="False" OnClick="cmdCancel_Click" OnClientClick="CancelValidation();">
        </asp:Button>&nbsp;
    </div>
</div>

