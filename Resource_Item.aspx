<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resource_Item.aspx.cs" EnableViewStateMac="false" 
    Inherits="DNNGo.Modules.DNNGalleryProGame.Resource_Item1" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Common.Controls" Assembly="DotNetNuke" %>
 <!DOCTYPE html>
<asp:literal id="skinDocType" runat="server"></asp:literal>
<!-- Template Name: Clip-One - Responsive Admin Template build with Twitter Bootstrap 3 Version: 1.0 Author: ClipTheme -->
<!--[if IE 8]><html class="ie8 no-js" lang="en"><![endif]-->
<!--[if IE 9]><html class="ie9 no-js" lang="en"><![endif]-->
<!--[if !IE]><!-->
<html lang="en" class="no-js">
	<!--<![endif]-->
<!-- start: HEAD -->
<head id="Head" runat="server">
    
    <title></title>
    <!-- start: META -->
    <!--[if IE]><meta http-equiv='X-UA-Compatible' content="IE=edge,IE=9,IE=8,chrome=1" /><![endif]-->
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimum-scale=1.0, maximum-scale=1.0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type"/>
    <meta content="text/javascript" http-equiv="Content-Script-Type"/>
    <meta content="text/css" http-equiv="Content-Style-Type"/>
    <meta id="MetaRefresh" runat="Server" http-equiv="Refresh" name="Refresh" />
    <meta id="MetaDescription" runat="Server" name="DESCRIPTION" />
    <meta id="MetaKeywords" runat="Server" name="KEYWORDS" />
    <meta id="MetaCopyright" runat="Server" name="COPYRIGHT" />
    <meta id="MetaGenerator" runat="Server" name="GENERATOR" />
    <meta id="MetaAuthor" runat="Server" name="AUTHOR" />
    <meta name="RESOURCE-TYPE" content="DOCUMENT" />
    <meta name="DISTRIBUTION" content="GLOBAL" />
    <meta id="MetaRobots" runat="server" name="ROBOTS" />
    <meta name="REVISIT-AFTER" content="1 DAYS" />
    <meta name="RATING" content="GENERAL" />
    <meta http-equiv="PAGE-ENTER" content="RevealTrans(Duration=0,Transition=1)" />
    <style type="text/css" id="StylePlaceholder" runat="server"></style>
    <script type="text/javascript">var Module = {ModulePath:"<%=ModulePath %>",ModuleId: <%=ModuleId %>, TabId: <%=TabId %>, PortalId: <%=PortalId %>,QueryString:"<%=QueryString %>"}</script>
    <asp:placeholder id="CSS" runat="server" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
</head>
<body id="Body" runat="server" >

     <!-- start: MAIN CSS -->
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/bootstrap/css/bootstrap.min.css"  media="screen" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/fonts/style.css" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/css/main.css" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/css/main-responsive.css" />
 
 
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/perfect-scrollbar/src/perfect-scrollbar.css" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/bootstrap-modal/css/bootstrap-modal-bs3patch.css" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/bootstrap-modal/css/bootstrap-modal.css" />

    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/datepicker/css/datepicker.css">
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css">
       <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css">

    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/iCheck/skins/all.css">

    <!--<link rel="stylesheet/less" type="text/css" href="<%=ModulePath %>Resource/css/styles.less" />-->
    <link rel="stylesheet" href="<%=ModulePath %>Resource/css/theme_light.css" type="text/css" id="skin_color" />
    <!--[if IE 7]>
		    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/font-awesome/css/font-awesome-ie7.min.css" />
		    <![endif]-->
    <!-- end: MAIN CSS -->
    <!-- start: CSS REQUIRED FOR THIS PAGE ONLY -->
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/bootstrap-social-buttons/social-buttons-3.css" />
    <!-- end: CSS REQUIRED FOR THIS PAGE ONLY -->
    <script src="<%=ModulePath %>Resource/js/jquery.min.js"></script>
    <script src="<%=ModulePath %>Resource/js/jquery-migrate.min.js"></script>
    <script src="<%=ModulePath %>Resource/js/jquery-ui.min.js"></script>

    <script src="<%=ModulePath %>Resource/js/jquery.tmpl.min.js"></script>

    
    <!-- start: MAIN JAVASCRIPTS -->
    <!--[if lt IE 9]>
		    <script src="<%=ModulePath %>Resource/plugins/respond.min.js"></script>
		    <script src="<%=ModulePath %>Resource/plugins/excanvas.min.js"></script>
    <![endif]-->

    <script src="<%=ModulePath %>Resource/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script src="<%=ModulePath %>Resource/plugins/blockUI/jquery.blockUI.js"></script>
    <script src="<%=ModulePath %>Resource/plugins/iCheck/jquery.icheck.min.js"></script>
    <script src="<%=ModulePath %>Resource/plugins/perfect-scrollbar/src/jquery.mousewheel.js"></script>
    <script src="<%=ModulePath %>Resource/plugins/perfect-scrollbar/src/perfect-scrollbar.js"></script>
    <script src="<%=ModulePath %>Resource/plugins/less/less-1.5.0.min.js"></script>
    <script src="<%=ModulePath %>Resource/js/jquery.cookie.js"></script>

    <script src="<%=ModulePath %>Resource/plugins/bootstrap-modal/js/bootstrap-modal.js"></script>
    <script src="<%=ModulePath %>Resource/plugins/bootstrap-modal/js/bootstrap-modalmanager.js"></script>


    <script src="<%=ModulePath %>Resource/plugins/bootstrap-paginator/src/bootstrap-paginator.js"></script>
        <script src="<%=ModulePath %>Resource/plugins/bootstrap-colorpicker/js/bootstrap-colorpicker.js"></script>

   <script src="<%=ModulePath %>Resource/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
<script src="<%=ModulePath %>Resource/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js"></script>

        <script src="<%=ModulePath %>Resource/plugins/select2/select2.full.min.js"></script>



    <script src="<%=ModulePath %>Resource/js/jquery.validationEngine-en.js"></script>
    <script src="<%=ModulePath %>Resource/js/jquery.validationEngine.js"></script>
    
  <%--  <script src="<%=ModulePath %>Resource/js/jquery.autoGrowInput.js"></script>--%>
 <%--   <script src="<%=ModulePath %>Resource/js/jquery.tagedit.js"></script>--%>
    <!-- end: MAIN JAVASCRIPTS -->

 
     <link rel="stylesheet" href="<%=ModulePath %>Resource/css/dropzone.css" />
     <script type="text/javascript" src="<%=ModulePath %>Resource/js/dropzone.js"></script>

    <script src="<%=ModulePath %>Resource/js/form-elements.js"></script>

     <script src="<%=ModulePath %>Resource/js/main.js"></script>

     <script src="<%=ModulePath %>Resource/plugins/ckeditor/ckeditor.js?cdv=<%=CrmVersion %>"></script>

     <script src="<%=ModulePath %>Resource/js/jquery.urls.js"></script>

<dnn:Form id="Form" runat="server" ENCTYPE="multipart/form-data" >
 <asp:Label ID="SkinError" runat="server" CssClass="NormalRed" Visible="False"></asp:Label>
 

 
<script id="scriptLibrary" type="text/x-jquery-tmpl">
<li class="list-item dd-item dd3-item" data-id="${ID}" data-sort="${Sort}">
        <div class="row">
            <div class="col-xs-1">${ID}</div>
            <div class="col-xs-3">{{html Thumbnail}}</div>
            <div class="col-xs-4">${Name}</div>
            <div class="col-xs-2">${Extension}</div>
            <div class="col-xs-2 last">
                <a href="javascript:;" data-id="${ID}"  data-MediaID="MediaID=${ID}" data-src="${ThumbnailUrl}" class="Select_Thumbnail btn btn-xs btn-bricky tooltips" data-placement="top" data-original-title="Select"><i class="fa fa-plus"></i></a>
                <div id="div_json${ID}" style="display:none;">${Json}</div>
            </div>
        </div>
</li>
</script>
<script id="scriptImage" type="text/x-jquery-tmpl">
<div class="wrap-image" data-id="${ID}" data-url="${ThumbnailUrl}" data-name="${Name}">
    <a class="group${ID} cboxElement" title="${Name}">
        <img class="imgUrlLink img-responsive" id="imgUrlLink" src="${ThumbnailUrl}" /> 
    </a>
    <div class="tools tools-bottom">
        <a>${Name} ${Extension}</a>
	    <a href="${ThumbnailUrl}" target="_blank" class="right">
		    <i class="clip-link-4"></i>
	    </a>
	    <a href="javascript:;" class="hlRemoveUrlLink right">
		    <i class="fa fa-trash-o"></i>
	    </a>
    </div>
</div>
</script>
 <script id="scriptImageEditor" type="text/x-jquery-tmpl">
        {{if IsPicture}}
          <div><img src="${FileUrl}" alt="${FileName}" /></div>
        {{else}}
            <div><a href="${FileUrl}" target="_blank" title="${FileName}">${Name}</a></div>
        {{/if}}
</script>
<!-- Modal start -->
<div id=""  class="modal fade modal_copy" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
  <div class="modal-header">
    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
    <h4 class="modal-title" id="myModalLabel">Select media file</h4>
  </div>
  <div class="modal-body">
  </div>
</div>
<!-- Modal end-->

  <!-- start: PAGE -->
     <div id="PlaceHolder_container" class="container"> 
        <asp:Label ID="lblMessage"  runat="server" CssClass="LI_Message"></asp:Label>
        <!--load UserControl-->
        <asp:PlaceHolder  ID="phContainer" runat="server"></asp:PlaceHolder>
    </div>
    <!-- end: PAGE --> 









 




 <input id="ScrollTop" runat="server" name="ScrollTop" type="hidden" />
<input id="__dnnVariable" runat="server" name="__dnnVariable" type="hidden" />
</dnn:Form>
</body>


<script type="text/javascript">
    jQuery(function (q) {
        FormElements.init();
        Main.init();
        $("#Form").validationEngine({
            promptPosition: "topRight"
        });

        $("#PlaceHolder_container validationEngineContainer").click(function () {
            if (!$('#PlaceHolder_container validationEngineContainer').validationEngine('validate')) {
                return false;
            }
        });

        //tinymce.init({
        //    selector: "textarea.tinymce",
        //    convert_urls: false,
        //    plugins: [
		//        "advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker",
		//        "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
		//        "save table contextmenu directionality template paste textcolor"
	    //    ]
        //});
    });

    function CancelValidation() {
        $('#Form').validationEngine('detach');
    }
</script>
 
<!-- end: BODY -->
</html>

 
