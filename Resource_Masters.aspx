<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resource_Masters.aspx.cs" Inherits="DNNGo.Modules.DNNGalleryProGame.Resource_Masters" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Common.Controls" Assembly="DotNetNuke" %>
<!DOCTYPE HTML>
<html lang="en" class="no-js">
<head>
<meta charset="utf-8">
<title>Resource Event Masters</title>
<!--[if lt IE 9]>
<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->
    <!-- start: MAIN CSS -->
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/bootstrap/css/bootstrap.min.css?cdv=<%=CrmVersion %>"  media="screen" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/font-awesome/css/font-awesome.min.css?cdv=<%=CrmVersion %>" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/css/font-icon.css?cdv=<%=CrmVersion %>" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/fonts/style.css?cdv=<%=CrmVersion %>" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/css/main.css?cdv=<%=CrmVersion %>" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/css/main-responsive.css?cdv=<%=CrmVersion %>" />
 
 	<link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/iCheck/skins/all.css?cdv=<%=CrmVersion %>" />

    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/perfect-scrollbar/src/perfect-scrollbar.css?cdv=<%=CrmVersion %>" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/bootstrap-modal/css/bootstrap-modal-bs3patch.css?cdv=<%=CrmVersion %>" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/bootstrap-modal/css/bootstrap-modal.css?cdv=<%=CrmVersion %>" />

    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/select2/select2.min.css?cdv=<%=CrmVersion %>" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/datepicker/css/datepicker.css?cdv=<%=CrmVersion %>" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css?cdv=<%=CrmVersion %>" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/bootstrap-switch/static/stylesheets/bootstrap-switch.css?cdv=<%=CrmVersion %>" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css?cdv=<%=CrmVersion %>" />
    <link rel="stylesheet" href="<%=ModulePath %>Resource/css/dropzone.css?cdv=<%=CrmVersion %>" />

    <link rel="stylesheet" href="<%=ModulePath %>Resource/css/theme_light.css?cdv=<%=CrmVersion %>" type="text/css" id="skin_color" />
    <!--[if IE 7]>
	    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/font-awesome/css/font-awesome-ie7.min.css?cdv=<%=CrmVersion %>" />
	<![endif]-->
    <!-- end: MAIN CSS -->
    <!-- start: CSS REQUIRED FOR THIS PAGE ONLY -->
    <link rel="stylesheet" href="<%=ModulePath %>Resource/plugins/bootstrap-social-buttons/social-buttons-3.css?cdv=<%=CrmVersion %>" />

       <!-- end: CSS REQUIRED FOR THIS PAGE ONLY -->
    <script src="<%=ModulePath %>Resource/js/jquery.min.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/js/jquery-migrate.min.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/js/jquery-ui.min.js?cdv=<%=CrmVersion %>"></script>

    <script src="<%=ModulePath %>Resource/js/jquery.tmpl.min.js?cdv=<%=CrmVersion %>"></script>
 

    
    <!-- start: MAIN JAVASCRIPTS -->
    <!--[if lt IE 9]>
		    <script src="<%=ModulePath %>Resource/plugins/respond.min.js?cdv=<%=CrmVersion %>"></script>
		    <script src="<%=ModulePath %>Resource/plugins/excanvas.min.js?cdv=<%=CrmVersion %>"></script>
    <![endif]-->

    <script src="<%=ModulePath %>Resource/plugins/bootstrap/js/bootstrap.min.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/plugins/blockUI/jquery.blockUI.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/plugins/iCheck/jquery.icheck.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/plugins/perfect-scrollbar/src/jquery.mousewheel.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/plugins/perfect-scrollbar/src/perfect-scrollbar.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/plugins/less/less-1.5.0.min.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/js/jquery.cookie.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/plugins/bootstrap-modal/js/bootstrap-modal.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/plugins/bootstrap-modal/js/bootstrap-modalmanager.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/plugins/ladda-bootstrap/dist/spin.min.js?cdv=<%=CrmVersion %>"></script>
	<script src="<%=ModulePath %>Resource/plugins/ladda-bootstrap/dist/ladda.min.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/plugins/bootstrap-paginator/src/bootstrap-paginator.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/plugins/bootstrap-colorpicker/js/bootstrap-colorpicker.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/plugins/select2/select2.full.min.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/plugins/bootstrap-switch/static/js/bootstrap-switch.min.js?cdv=<%=CrmVersion %>"></script>


 
    <script type="text/javascript" src="<%=ModulePath %>Resource/js/dropzone.js?cdv=<%=CrmVersion %>"></script>

    <script src="<%=ViewValidationEngineLanguage() %>"></script>
    <script src="<%=ModulePath %>Resource/js/jquery.validationEngine.js?cdv=<%=CrmVersion %>"></script>
    

    <!-- end: MAIN JAVASCRIPTS -->
    <script src="<%=ModulePath %>Resource/js/jquery.urls.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/js/form-elements.js?cdv=<%=CrmVersion %>"></script>
    <script src="<%=ModulePath %>Resource/js/main.js?cdv=<%=CrmVersion %>"></script>
      <script src="<%=ModulePath %>Resource/plugins/ckeditor/ckeditor.js?cdv=<%=CrmVersion %>"></script>

 
    <script type="text/javascript">var Module = {ModulePath:"<%=ModulePath %>",ModuleId: <%=ModuleId %>, TabId: <%=TabId %>, PortalId: <%=PortalId %>};</script>
</head>
<body>



<dnn:Form id="Form" runat="server" ENCTYPE="multipart/form-data">    
    <div id="PlaceHolder_container" class="container validationEngineContainer">
          <div class="container-fluid">
               <asp:PlaceHolder ID="phPlaceHolder" runat="server"></asp:PlaceHolder>

          </div>
       
    </div>
    
<script id="scriptLibraryUrls" type="text/x-jquery-tmpl">
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
    <div class="tools tools-bottom" style="bottom:0;">
        <a>${Name} ${Extension}</a>
	    <a href="${FileUrl}" target="_blank" class="right">
		    <i class="clip-link-4"></i>
	    </a>
	    <a href="javascript:;" class="hlRemoveUrlLink right">
		    <i class="fa fa-trash-o"></i>
	    </a>
    </div>
</div>
</script>
<script id="scriptLibraryImages" type="text/x-jquery-tmpl">
<li class="added" data-id="${ID}">
	<div class="inner" style="width: 80px; height: 80px; overflow: hidden;text-align: center;">
		<img data-id="${ID}" src="${ThumbnailUrl}">
	</div>
	<a href="javascript:;" class="gallery-icon-remove"  data-id="${ID}"><i class="gallery-composer-icon gallery-c-icon-close"></i></a>
</li>
</script>

    <script id="scriptImageEditor" type="text/x-jquery-tmpl">
    {{if IsPicture}}
          <div><img src="${FileUrl}" alt="${Name}" /></div>
    {{else IsFlash}}
          <div>
            <object classid="clsid:${Guid}" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0">
                <param name="allowFullScreen" value="true" />
                <param name="quality" value="high" />
                <param name="movie" value="${FileUrl}" />
                <embed allowfullscreen="true" pluginspage="http://www.macromedia.com/go/getflashplayer" quality="high" src="${FileUrl}" type="application/x-shockwave-flash">
                </embed>
             </object>
          </div>  
    {{else IsVideo}}
       <div>
          <video autoplay="true"  loop="true"  muted="true"  >
            {{if IsMp4}}
                <source src="${FileUrl}" type="video/mp4"></source>
            {{else IsM4v}}
                <source src="${FileUrl}" type="video/mp4"></source>
            {{else IsOgv}}
                <source src="${FileUrl}" type="video/ogg"></source>
            {{else IsWebm}}
                <source src="${FileUrl}" type="video/webm"></source>
            {{/if}}
                Your browser does not support the video tag. 
          </video>
       </div>
    {{else}}
        <div><a href="${FileUrl}" target="_blank" title="${Name}">${Name}</a></div>
    {{/if}}
</script>
<!-- Modal start -->
<div id="-" class="modal fade modal_copy" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
  <div class="modal-header">
    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
    <h4 class="modal-title" id="myModalLabel">Select media file</h4>
  </div>
  <div class="modal-body">
  </div>
</div>
<!-- Modal end-->






</dnn:Form>


<script type="text/javascript">
    jQuery(function (q) {
        FormElements.init();
        Main.init();



        $("#PlaceHolder_container").validationEngine({
            promptPosition: "topRight"
        });

        $("#PlaceHolder_container input[lang='Submit']").click(function () {
            if (!$('#PlaceHolder_container').validationEngine('validate')) {
                return false;
            }
        });
    });

    function CancelValidation() {
        $('#Form').validationEngine('detach');
    }
</script>
</body> 
</html>