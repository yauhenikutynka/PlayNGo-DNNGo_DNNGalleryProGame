<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Setting_ManagerItem.ascx.cs"
    Inherits="DNNGo.Modules.DNNGalleryProGame.Setting_ManagerItem" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<script src="<%=ModulePath %>Resource/plugins/nestable/jquery.nestable.js"></script>
<!-- start: PAGE HEADER -->
<div class="row">
    <div class="col-sm-12">
        <!-- start: PAGE TITLE & BREADCRUMB -->
        <div class="page-header">
            <h1>
                <i class="fa fa-plus"></i>
                <%=ViewResourceText("Header_Title", "Add New Slider")%>
                <%--<small>overview &amp; stats </small>--%></h1>
        </div>
        <!-- end: PAGE TITLE & BREADCRUMB -->
    </div>
</div>
<!-- end: PAGE HEADER -->
<!-- start: PAGE CONTENT -->
<div class="row">
    <div class="col-sm-8">
        <div runat="server" id="divOptions_Left">
            <asp:Repeater ID="RepeaterGroup_Left" runat="server" OnItemDataBound="RepeaterGroup_ItemDataBound">
                <ItemTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-external-link-square"></i>
                            <%#Eval("key")%>
                            <div class="panel-tools">
                                <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <asp:Repeater ID="RepeaterOptions" runat="server" OnItemDataBound="RepeaterOptions_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="form-group">
                                            <asp:Literal ID="liTitle" runat="server"></asp:Literal>
                                            <div class="col-sm-10">
                                                <asp:PlaceHolder ID="ThemePH" runat="server"></asp:PlaceHolder>
                                                <asp:Literal ID="liHelp" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <!-- end: TEXT AREA PANEL -->
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <!-- start: TEXT AREA PANEL -->
        <div class="panel panel-default" id="div_Layers" runat="server">
            <div class="panel-heading">
                <i class="fa fa-external-link-square"></i> <asp:Literal ID="liTitle_Layers" runat="server"></asp:Literal> <%--<%=ViewResourceText("Title_Layers", "Layers")%>--%>
                <div class="panel-tools">
                    <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-12">
                        <asp:Literal ID="liAddNewLink_List" runat="server"></asp:Literal>
                    </div>
                </div>
               
                <div class="dd handlelist" id="nestable_div">
                     <div class="listtitle" >
                          <div class="row">
                            <div class="col-xs-1">ID</div>
                            <div class="col-xs-5">Title</div>
                            <div class="col-xs-2">Images </div>
                            <div class="col-xs-1 hidden-xs"><%--Type--%></div>
                            <div class="col-xs-1 hidden-xs">Activate</div>
                            <div class="col-xs-2 last">Action</div>
                          </div>
                     </div>
                    <ol class="dd-list listbox" id="nestable_ol">
                    </ol>
                    <asp:HiddenField ID="nestable_output" runat="server"  />
                 </div>
                 

                 
                
                 
                 
                 
                 
                 
                    
            </div>
        </div>



        

        <%--Relations--%>
        <div class="panel panel-default">
            <div class="panel-heading">
                <i class="fa fa-external-link-square"></i> <%=ViewResourceText("Title_Relations", "Related Games List")%>
                <div class="panel-tools">
                    <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                </div>
            </div>
            <div class="panel-body">

                <div class="form-group">
                    <asp:HyperLink ID="hlSelectRelations" runat="server" data-toggle="modal" CssClass="btn btn-xs btn-bricky btn-relations" NavigateUrl="#Relations_Modal"  Text="<i class='fa clip-list'></i> Select Relations" resourcekey="hlSelectRelations"></asp:HyperLink>
                </div>
                <div class="form-group">
                    <asp:HiddenField ID="hfRelations" runat="server" />
                    <div class="dd" id="nestable-relations">
						<ol class="dd-list" id="nestable-ol-relations">
                            <asp:Repeater ID="RepeaterRelations" runat="server" OnItemDataBound="RepeaterRelations_ItemDataBound">
                                <ItemTemplate>
                                 	<li class="dd-item dd3-item" data-id="<%#Eval("ID")%>">
										<div class="dd-handle dd3-handle"></div>
										<div class="dd3-content">
											<div class="row">
                                                <div class="col-sm-11">
                                                    <ul class="clearfix col3_box">
                                                        <li>
                                                            <div class="tit">Title</div>
                                                            <div class="conbox"><%#Eval("Title")%></div>
                                                        </li>
                                                            
                                                         <li><div class="tit">Picture</div>
                                                            <div class="conbox"><img src="<%#Eval("Picture")%>"></div>
                                                        </li>
                                                         <li><div class="tit">Group</div>
                                                            <div class="conbox"><%#Eval("Groups")%></div>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-sm-1" style="text-align:left; line-height: 70px;">
                                                    <a class="btn btn-xs btn-link  btn-detele" href="javascript:;" data-id="<%#Eval("ID")%>">
											            <i class="fa fa-times"></i>
										            </a>
                                                </div>
											</div>
										</div>          
									</li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ol>
                    </div>
                </div>
            </div>
        </div>





    </div>
    <div class="col-sm-4">
        <!-- start: SELECT BOX PANEL -->
        <!--Start-->
        <div class="panel panel-default">
            <div class="panel-heading">
                <i class="fa fa-external-link-square"></i>
                <%=ViewResourceText("Title_Publish", "Publish")%>
                <div class="panel-tools">
                    <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                </div>
            </div>
            <div class="panel-body buttons-widget">
     

                <ul class="Edit_List" id="accordion">
                    <li>
                        <p>
                            <i class="fa clip-grid-5"></i>&nbsp;&nbsp;<%=ViewResourceText("Title_Status", "Status")%>:
                            <b>
                                <asp:Label ID="liArticleStatus" runat="server" Text="Draft"></asp:Label></b>&nbsp;&nbsp;<a
                                    href="#Status" data-toggle="collapse" data-parent="#accordion"><i class="fa fa-pencil"></i>[<%=ViewResourceText("Title_Edit", "Edit")%>]</a></p>
                        <div class="collapse" id="Status">
                            <div class="row form-group">
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlArticleStatus" runat="server" CssClass="form-control form-trumpet">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-6 text_right">
                                    <a id="link_ArticleStatus" class="btn btn-default btn-ms2" href="#Status" data-toggle="collapse"
                                        data-parent="#accordion">
                                        <%=ViewResourceText("Title_OK", "OK")%>
                                    </a>&nbsp;<a href="#Status" data-toggle="collapse" data-parent="#accordion"><%=ViewResourceText("Title_Cancel", "Cancel")%></a>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <p>
                            <i class="fa clip-calendar-3"></i>&nbsp;&nbsp;<%=ViewResourceText("Title_Start", "Start")%>:
                            <b>
                                <asp:Label ID="liStartDateTime" runat="server" Text="Immediately"></asp:Label></b>&nbsp;<a
                                    href="#Start" data-toggle="collapse" data-parent="#accordion"><i class="fa fa-pencil"></i>[<%=ViewResourceText("Title_Edit", "Edit")%>]</a></p>
                        <div class="panel-collapse collapse" id="Start">
                            <div class="row form-group">
                                <div class="col-md-6 input-group">
                                    <asp:TextBox ID="txtStartDate" runat="server" data-date-format="mm/dd/yyyy" data-date-viewmode="years"
                                        CssClass="form-control date-picker"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>
                                <div class="col-md-5 input-group input-append bootstrap-timepicker">
                                    <asp:TextBox ID="txtStartTime" runat="server" CssClass="form-control time-picker"></asp:TextBox>
                                    <span class="input-group-addon add-on"><i class="fa fa-clock-o"></i></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <a id="link_StartDateTime" class="btn btn-default btn-ms2" href="#Start" data-toggle="collapse"
                                    data-parent="#accordion">
                                    <%=ViewResourceText("Title_OK", "OK")%>
                                </a>&nbsp;<a href="#Start" data-toggle="collapse" data-parent="#accordion"><%=ViewResourceText("Title_Cancel", "Cancel")%></a>
                            </div>
                        </div>
                    </li>
                    <li>
                        <p>
                            <i class="clip-stopwatch"></i>&nbsp;&nbsp;<%=ViewResourceText("Title_Disable", "Disable")%>:
                            <b>
                                <asp:Label ID="liDisableDateTime" runat="server" Text="None"></asp:Label></b>&nbsp;<a
                                    href="#DisableDateTime" data-toggle="collapse" data-parent="#accordion"><i class="fa fa-pencil"></i>[<%=ViewResourceText("Title_Edit", "Edit")%>]</a></p>
                        <div class="panel-collapse collapse" id="DisableDateTime">
                            <div class="row form-group">
                                <div class="col-md-6 input-group">
                                    <asp:TextBox ID="txtDisableDate" runat="server" data-date-format="mm/dd/yyyy" data-date-viewmode="years"
                                        CssClass="form-control date-picker"></asp:TextBox>
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>
                                <div class="col-md-5 input-group input-append bootstrap-timepicker">
                                    <asp:TextBox ID="txtDisableTime" runat="server" CssClass="form-control time-picker"></asp:TextBox>
                                    <span class="input-group-addon add-on"><i class="fa fa-clock-o"></i></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <a id="link_DisableDateTime" class="btn btn-default btn-ms2" href="#DisableDateTime"
                                    data-toggle="collapse" data-parent="#accordion">
                                    <%=ViewResourceText("Title_OK", "OK")%>
                                </a>&nbsp;<a href="#DisableDateTime" data-toggle="collapse" data-parent="#accordion"><%=ViewResourceText("Title_Cancel", "Cancel")%></a>
                            </div>
                        </div>
                    </li>
                </ul>
                <div class="row">
                    <br />
                    <div class="col-sm-5">
                        <asp:Button CssClass="btn btn-light-grey btn-sm" ID="cmdSaveDraft" resourcekey="cmdSaveDraft"
                            runat="server" Text="Save Draft" CausesValidation="False" OnClick="cmdSaveDraft_Click"
                            OnClientClick="CancelValidation();"></asp:Button>
                    </div>
                    <div class="col-sm-7 text_right">
                        <asp:Button CssClass="btn btn-primary btn-sm" lang="Submit" ID="cmdPublish" resourcekey="cmdPublish"
                            runat="server" Text="Publish" OnClick="cmdPublish_Click"></asp:Button>&nbsp;
                        <asp:Button CssClass="btn btn-primary btn-sm" ID="cmdCancel" resourcekey="cmdCancel"
                            runat="server" Text="Cancel" CausesValidation="False" OnClick="cmdCancel_Click"
                            OnClientClick="CancelValidation();"></asp:Button>&nbsp;
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-default" id="divNumbers" runat="server" visible="false">
            <div class="panel-heading">
                <i class="fa fa-external-link-square"></i>
                <%=ViewResourceText("Title_Numbers", "Number Settings")%>
                <div class="panel-tools">
                    <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                </div>
            </div>
            <div class="panel-body buttons-widget form-horizontal">
                  <div class="form-group">
                       <%=ViewControlTitle("lblDownloadNumber", "Downloads", "txtDownloadNumber", ":", "col-sm-3 control-label")%>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtDownloadNumber" runat="server" CssClass="form-control validate[required,custom[integer]]" Width="100"></asp:TextBox>
                        </div>
                  </div>

                    <div class="form-group">
                       <%=ViewControlTitle("lblHeatNumber", "Heats", "txtHeatNumber", ":", "col-sm-3 control-label")%>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtHeatNumber" runat="server" CssClass="form-control validate[required,custom[integer]]"  Width="100"></asp:TextBox>
                        </div>
                  </div>
            </div>
        </div>


        <!--Categories-->
        <div class="panel panel-default" runat="server" id="divGroups" visible="false">
            <div class="panel-heading">
                <i class="fa fa-external-link-square"></i>
                <%=ViewResourceText("Title_Groups", "Groups")%>
                <div class="panel-tools">
                    <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                </div>
            </div>
            <div class="panel-body buttons-widget">
                <div class="checkbox">
                    <asp:Literal runat="server" ID="liGroups"></asp:Literal></div>
                <asp:HyperLink runat="server" ID="hlAddGroups" resourcekey="hlAddGroups" Text="Add new Groups"></asp:HyperLink>
            </div>
        </div>
        
        <!--Custom models-->
        <div class="panel panel-default"  runat="server" id="divCustomModels" visible="false">
            <div class="panel-heading">
                <i class="fa fa-external-link-square"></i><%=ViewResourceText("Title_CustomModels", "Custom models")%>
                <div class="panel-tools">
                    <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                </div>
            </div>
            <div class="panel-body buttons-widget">
               <div id="div_CustomModels" class="form-horizontal">
                    <div class="form-group">
                        <div class="col-sm-5"><%=ViewResourceText("Title_Name", "Name")%></div>
                        <div class="col-sm-5"><%=ViewResourceText("Title_Value", "Value")%></div>
                    </div>
              </div>
               
            <code> <span class="fa fa-close" style="color:red"></span></code> Value: <code>&lt;span class=&quot;fa fa-close&quot;&gt;&lt;/span&gt;</code> <br />
            <code> <span class="fa fa-check" style="color:green"></span></code> Value: <code>&lt;span class=&quot;fa fa-check&quot;&gt;&lt;/span&gt;</code>
                
            </div>
            
        </div>


       <div class="panel panel-default"  >
            <div class="panel-heading">
                <i class="fa fa-external-link-square"></i>
                <%=ViewResourceText("Title_Permissions", "Permissions")%>
                <div class="panel-tools">
                    <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                </div>
            </div>
            <div class="panel-body buttons-widget form-horizontal">
                  <div class="form-group">
                       <%=ViewControlTitle("lblPermissionsAllUsers", "All Users", "cbPermissionsAllUsers", ":", "col-sm-3 control-label")%>
                        <div class="col-sm-9">
                             <div class="checkbox-inline">
                                <asp:CheckBox ID="cbPermissionsAllUsers" runat="server" CssClass="auto"/>
                            </div>
                        </div>
                  </div>

                  <div class="form-group">
                       <%=ViewControlTitle("lblPermissionsRoles", "Permission Roles", "cblPermissionsRoles", ":", "col-sm-3 control-label")%>
                        <div class="col-sm-9">
                            <div class="checkbox-inline">
                                <asp:CheckBoxList ID="cblPermissionsRoles" runat="server" CssClass="auto"></asp:CheckBoxList>
                            </div>
                        </div>
                  </div>
            </div>
        </div>



        <!-- Layout Right Options -->
        <div runat="server" id="divOptions_Right">
            <asp:Repeater ID="RepeaterGroup_Right" runat="server" OnItemDataBound="RepeaterGroup_ItemDataBound">
                <ItemTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-external-link-square"></i>
                            <%#Eval("key")%>
                            <div class="panel-tools">
                                <a href="#" class="btn btn-xs btn-link panel-collapse collapses"></a>
                            </div>
                        </div>
                        <div class="panel-body buttons-widget form-horizontal">
                            <asp:Repeater ID="RepeaterOptions" runat="server" OnItemDataBound="RepeaterOptions_ItemDataBound">
                                <ItemTemplate>
                                    <div class="form-group">
                                        <div class="row">
                                            <asp:Literal ID="liTitle" runat="server"></asp:Literal>
                                            <div class="col-sm-9">
                                                <asp:PlaceHolder ID="ThemePH" runat="server"></asp:PlaceHolder>
                                                <asp:Literal ID="liHelp" runat="server"></asp:Literal>
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
        <!-- end: SELECT BOX PANEL -->
    </div>
    <!-- end: PAGE CONTENT-->
</div>



<div id="Relations_Modal" class="modal fade" tabindex="-1" data-width="820"
    data-height="600" style="display: none;">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
            &times;
        </button>
        <h4 class="modal-title">
            <i class='fa fa-folder-open'></i>Select Related Games List</h4>
    </div>
    <div class="modal-body">
        <iframe id="Relations_Iframe" width="100%" height="100%" style="border-width: 0px;"></iframe>
    </div>
</div>




<div id="AddLayer_Modal" class="modal fade" tabindex="-1" data-width="820" data-height="520" style="display: none;">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
            &times;
        </button>
        <h4 class="modal-title">
            <i class='fa fa-folder-open'></i>Set Layers</h4>
    </div>
    <div class="modal-body">
        <iframe id="AddLayer_Iframe" width="100%" height="99%" style="border-width: 0px;">
        </iframe>
    </div>
</div>
<script id="scriptnestable" type="text/x-jquery-tmpl">
<li class="list-item" data-id="${ID}" data-sort="${Sort}">
        <div class="handle"></div>
        <div class="row">
            <div class="col-xs-1">${ID}</div>
            <div class="col-xs-5">${Title}</div>
            <div class="col-xs-2">{{html Thumbnail}}</div>
            <div class="col-xs-1 hidden-xs"><%--${LayerType}--%></div>
            <div class="col-xs-1 hidden-xs buttons-widget">
                <div class="make-switch switch-small" data-on="success" data-off="warning">
					<input type="checkbox" ${Status} />
				</div>
            </div>
            <div class="col-xs-2 last">
                <a href="#AddLayer_Modal" data-id="${ID}" data-href="${Edit}" class="edit_Layer  btn btn-xs btn-teal tooltips" data-placement="top" data-toggle="modal" data-original-title="Edit"><i class="fa fa-edit"></i></a> 
                <a href="javascript:;" data-id="${ID}" class="delete_Layer btn btn-xs btn-bricky tooltips" data-placement="top" data-original-title="Remove"><i class="fa fa-times fa fa-white"></i></a>
            </div>
        </div>
</li>
</script>
<script id="script-Relations" type="text/x-jquery-tmpl">
<li class="dd-item dd3-item" data-id="${ID}">
	<div class="dd-handle dd3-handle"></div>
	<div class="dd3-content">
		<div class="row">
            <div class="col-sm-11">
                <ul class="clearfix col3_box">
                    <li>
                        <div class="tit">Title</div>
                        <div class="conbox">${Title}</div>
                    </li>
                                                            
                        <li><div class="tit">Picture</div>
                        <div class="conbox"><img src="${Picture}"></div>
                    </li>
                        <li><div class="tit">Group</div>
                        <div class="conbox">${Groups}</div>
                    </li>
                </ul>
            </div>
            <div class="col-sm-1" style="text-align:left; line-height: 70px;">
                <a class="btn btn-xs btn-link  btn-detele" href="javascript:;" data-id="${ID}">
					<i class="fa fa-times"></i>
				</a>
            </div>
		</div>
	</div>          
</li>
</script>


<script type="text/javascript">
    var LayerList = function () {
            var BindLayerItem = function (item) {
            $("a.edit_Layer[data-id=" + item.ID + "]").click(function () { $("#AddLayer_Iframe").attr("src", $(this).attr("data-href")); });
            $("a.delete_Layer[data-id=" + item.ID + "]").click(function (e) {
                if (confirm("<%= Localization.GetString("DeleteItem") %>")) {
                    $("li.list-item[data-id=" + $(this).attr("data-id") + "]").fadeOut("fast", function () {
                        $.get("<%=PostLayerResource("DeleteLayerItem") %>" + $(this).attr("data-id") +"&time=" + new Date().toTimeString(), function (data) {
                            if (data == "1") {
                                $(this).empty();
                            }
                        });
                    });
                }
            });
            $("li.list-item[data-id=" + item.ID + "]").find(".make-switch").bootstrapSwitch().on('switch-change', function (e, data) {
                $.get("<%=PostLayerResource("CheckedLayerItem") %>" + item.ID+"&time=" + new Date().toTimeString(),{  checked: data.value});
            });
        };
        var PostLayerItem = function (LayerID){
                $.getJSON("<%=PostLayerResource("LayerListHtml") %>" + LayerID +"&time=" + new Date().toTimeString(), function (data) {
                    $.each(data, function (i, item) {
                        if($("li.list-item[data-id=" + item.ID + "]").is("li"))
                        { 
                            $("#scriptnestable").tmpl(item).replaceAll("li.list-item[data-id=" + item.ID + "]");
                        }else
                        {
                                $("#scriptnestable").tmpl(item).appendTo('#nestable_ol');
                        }
                        BindLayerItem(item);
                    });
                });
                $('#AddLayer_Modal').modal('hide');
            };
            return {
            Init: function (item) {
                BindLayerItem(item);
            },
            Post: function (LayerID) {
                PostLayerItem(LayerID);
            }
        };
    }();


    function InsetRelations(jsons) {
        var IDs = jQuery('#<%=hfRelations.ClientID %>').val() + "";

        $.each(jsons, function (i, json) {
            if ( !( json.ID !== '' && IDs.indexOf(json.ID + ",") >= 0 )) {
                $("#nestable-ol-relations").append($("#script-Relations").tmpl(json));
                IDs = IDs + (json.ID + ",");
            }
        });

        jQuery('#<%=hfRelations.ClientID %>').val(IDs);
    }

    jQuery(function ($) {
            $("a.add_Layer").click(function () { $("#AddLayer_Iframe").attr("src", $(this).attr("data-href")); });
            $.getJSON("<%=PostLayerResource("LayerListHtml") %>&time=" + new Date().toTimeString(), function (data) {
                if(data != null)
                {
                    $.each(data, function (i,item) {
                        $("#scriptnestable").tmpl(item).appendTo('#nestable_ol');
                        LayerList.Init(item);
               
                    });
                }
            });

        $("a.btn-relations").click(function () { $("#Relations_Iframe").attr("src", $(this).attr("data-href")); });

        $("#nestable-ol-relations").on("click", 'a.btn-detele', function (event) {
            if (confirm("<%=Localization.GetString("DeleteItem")%>"))
            {
                var id = $(this).data("id");
                $("#nestable-ol-relations li[data-id='" + id + "']").hide("fast", function () {
                    $(this).remove();
                });
                jQuery('#<%=hfRelations.ClientID %>').val(jQuery('#<%=hfRelations.ClientID %>').val().replace(id + ",", ""));
            }
        });


        $('#nestable-relations').nestable({ maxDepth: 1, group: 0 }).on('change', function (e) {
            var list = e.length ? e : $(e.target), IDs='';
            if (window.JSON) {
                var jsons = list.nestable('serialize');
                $.each(jsons, function (name, value) {
                    IDs = IDs + (value.id + ",");
                });
                jQuery('#<%=hfRelations.ClientID %>').val(IDs);


            }
        });
            
    });
</script>


<!-- start: Custom models-->
<script id="scriptCustomModels" type="text/x-jquery-tmpl">
    <div class="form-group" data-name="${Name}">
        <div class="col-sm-5"><input type="text" class="form-control" name="Model$Name$<%=ModuleId %>" value="${Name}"/> </div>
        <div class="col-sm-5"><input type="text" class="form-control" name="Model$Value$<%=ModuleId %>" value="${Value}"/></div>
        <div class="col-sm-2">
            {{if Name == "" }}
                <a class="btn btn-xs btn-bricky _plus"><i class="fa fa-plus"></i></a>
                <a class="btn btn-xs btn-bricky _close" style="display:none"><i class="fa fa-close"></i></a>
             {{else}}
                <a class="btn btn-xs btn-bricky _close" data-name="${Name}"><i class="fa fa-close"></i></a>
             {{/if}}
        </div>
    </div>
</script> 
<script type="text/javascript">
    jQuery(function ($) {
        var strJSON = "{Name:'',Value:''}";
        function bindrows(item) {
            $("div#div_CustomModels").append($("#scriptCustomModels").tmpl(item)).find("a._close").one("click", function () { $(this).parent().parent().remove(); });
            $("div#div_CustomModels .form-group:last-child").find("a._plus").one("click", function () {
                $(this).hide().parent().find("a._close").show();
                bindrows(eval("(" + strJSON + ")"));
            });
        }
        $.getJSON("<%=PostLayerResource("CustomModels") %>", function (data) {
            jQuery.each(data, function (i, item) {
                bindrows(item);
               
            });
            //bindrows(eval("(" + strJSON + ")"));
        });
        
    });
</script>
<!-- end: Custom models-->

<script type="text/javascript">


/**/
    var UINestable = function () {
        //function to initiate jquery.nestable
        var updateOutput = function (e) {
                var lst = [];
                $('#nestable_ol li').each(function (i) {
                    if($(this).attr("data-sort") != (i+1)*2)
                    {
                        lst.push({ID:$(this).attr("data-id"),Sort:(i+1)*2});
                    }
                });
               $.post("<%=PostLayerResource("LayerListSort") %>&time=" + new Date().toTimeString(), { json: window.JSON.stringify(lst) },function(data){});
        };
        var runNestable = function () {
            $('#nestable_div').nestable({ maxDepth: 1, handleClass: "handle", listClass: "dd-list", itemClass: "list-item", group: 0 }).on('change', updateOutput);
            //updateOutput($('#nestable_div').data('output', $('#<%=nestable_output.ClientID %>')));
        };
        return {
            //main function to initiate template pages
            init: function () {
                runNestable();
            }
        };
    } ();

    jQuery(document).ready(function () {
	  UINestable.init();
    });
	

    jQuery(function ($) {
        $("#link_ArticleStatus").click(function () { $('#<%=liArticleStatus.ClientID %>').text($("#<%=ddlArticleStatus.ClientID %>").find("option:selected").text()); });
        $("#link_StartDateTime").click(function () { $('#<%=liStartDateTime.ClientID %>').text($("#<%=txtStartDate.ClientID %>").val() + "  " + $("#<%=txtStartTime.ClientID %>").val()); });
        $("#link_DisableDateTime").click(function () { $('#<%=liDisableDateTime.ClientID %>').text($("#<%=txtDisableDate.ClientID %>").val() + "  " + $("#<%=txtDisableTime.ClientID %>").val()); });

      

    });
	
</script>
