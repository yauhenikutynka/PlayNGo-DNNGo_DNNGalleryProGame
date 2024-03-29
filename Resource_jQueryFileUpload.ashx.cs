using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Tabs;
using System.Drawing;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.FileSystem;

namespace DNNGo.Modules.DNNGalleryProGame
{
    public class Resource_jQueryFileUploadHandler : IHttpHandler
    {
		private readonly JavaScriptSerializer js = new JavaScriptSerializer();


        #region "获取DNN对象"

        public Int32 PortalId = WebHelper.GetIntParam(HttpContext.Current.Request, "PortalId", 0);
        /// <summary>
        /// 模块编号
        /// </summary>
        public Int32 ModuleId = WebHelper.GetIntParam(HttpContext.Current.Request, "ModuleId", 0);
        /// <summary>
        /// 页面编号
        /// </summary>
        public Int32 TabId = WebHelper.GetIntParam(HttpContext.Current.Request, "TabId", 0);

        /// <summary>
        /// 路径
        /// </summary>
        public String ModulePath = WebHelper.GetStringParam(HttpContext.Current.Request, "ModulePath", "");


        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo UserInfo
        {
            get { return UserController.GetCurrentUserInfo(); }
        }




        /// <summary>
        /// 是否具有该模块的编辑权限
        /// </summary>
        public Boolean IsEdit
        {
            get { return UserInfo != null && UserInfo.UserID > 0 && (IsAdministrator || DotNetNuke.Security.Permissions.ModulePermissionController.HasModuleAccess(DotNetNuke.Security.SecurityAccessLevel.Edit, "CONTENT", ModuleConfiguration)); }
            //get { return UserId > 0 && (IsAdministrator ||  PortalSecurity.HasEditPermissions(ModuleId,TabId)); }
        }


        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public Boolean IsAdministrator
        {
            get { return UserInfo.IsSuperUser || UserInfo.IsInRole("Administrators"); }
        }




        private ModuleInfo _ModuleConfiguration = new ModuleInfo();
        /// <summary>
        /// 模块信息
        /// </summary>
        public ModuleInfo ModuleConfiguration
        {
            get
            {
                if (!(_ModuleConfiguration != null && _ModuleConfiguration.ModuleID > 0) && ModuleId > 0)
                {
                    ModuleController mc = new ModuleController();
                    _ModuleConfiguration = mc.GetModule(ModuleId, TabId);

                }
                return _ModuleConfiguration;
            }
        }


        private PortalSettings _portalSettings;
        /// <summary>
        /// 站点设置
        /// </summary>
        public PortalSettings PortalSettings
        {
            get
            {
                if (!(_portalSettings != null && _portalSettings.PortalId != Null.NullInteger))
                {
                    PortalAliasInfo objPortalAliasInfo = new PortalAliasInfo();
                    objPortalAliasInfo.PortalID = PortalId;
                    _portalSettings = new PortalSettings(TabId, objPortalAliasInfo);
                }
                return _portalSettings;
            }
        }



        private TabInfo _tabInfo;
        /// <summary>
        /// 页面信息
        /// </summary>
        public TabInfo TabInfo
        {
            get
            {
                if (!(_tabInfo != null && _tabInfo.TabID > 0) && TabId > 0)
                {
                    TabController tc = new TabController();
                    _tabInfo = tc.GetTab(TabId);

                }

                return _tabInfo;


            }
        }

        #endregion

        #region "属性"
 
 

        #endregion



 
		public bool IsReusable { get { return false; } }

		public void ProcessRequest (HttpContext context) {
			context.Response.AddHeader("Pragma", "no-cache");
			context.Response.AddHeader("Cache-Control", "private, no-cache");
			HandleMethod(context);
           
		}
        
		// Handle request based on method
		private void HandleMethod (HttpContext context) {

            //没有权限
            if (!IsEdit) throw new HttpRequestValidationException("You are not permitted to access this page! :(");

            switch (context.Request.HttpMethod) {
				case "HEAD":
				case "GET":
					if (GivenFilename(context)) DeliverFile(context);
					else ListCurrentFiles(context);
					break;

				case "POST":
				case "PUT":
					UploadFile(context);
					break;

				case "DELETE":
					DeleteFile(context);
					break;

				case "OPTIONS":
					ReturnOptions(context);
					break;

				default:
					context.Response.ClearHeaders();
					context.Response.StatusCode = 405;
					break;
			}
		}

		private static void ReturnOptions(HttpContext context) {
			context.Response.AddHeader("Allow", "DELETE,GET,HEAD,POST,PUT,OPTIONS");
			context.Response.StatusCode = 200;
		}

		// Delete file from the server
		private void DeleteFile (HttpContext context) {

            Int32 PhotoID = WebHelper.GetIntParam(context.Request, "ID", 0);
            DNNGo_DNNGalleryProGame_Files PhotoItem = DNNGo_DNNGalleryProGame_Files.FindByKeyForEdit(PhotoID);
            if (PhotoItem != null && PhotoItem.ID > 0)
            {

                //要删除实际的文件
                String DeletePath = HttpContext.Current.Server.MapPath(GetPhotoPath(PhotoItem.FilePath));


                //删除文件
                if (File.Exists(DeletePath))
                {
                    try
                    {
                        File.Delete(DeletePath);
                    }
                    catch { }
                }
                //删除记录
                PhotoItem.Delete();
            }
		}

        /// <summary>
        /// 获取图片的路径
        /// </summary>
        /// <param name="media">媒体文件的实体</param>
        /// <returns></returns>
        public String GetPhotoPath(DNNGo_DNNGalleryProGame_Files media)
        {
            String PhotoPath  = String.Empty;
            if (media != null && media.ID > 0)
            {
               PhotoPath = GetPhotoPath(media.FilePath);
            }
            return PhotoPath;

        }


        /// <summary>
        /// 获取图片的路径
        /// </summary>
        /// <param name="FilePath">图片路径</param>
        /// <returns></returns>
        public String GetPhotoPath(String FilePath)
        {
            return String.Format("{0}{1}", PortalSettings.HomeDirectory, FilePath);
        }

		// Upload file to the server
		private void UploadFile (HttpContext context) {
            var statuses = new List<Resource_FilesStatus>();
			var headers = context.Request.Headers;

            String a= WebHelper.GetStringParam(context.Request,"type","");

           

            if (!String.IsNullOrEmpty(a) && a == "DELETE")
            {
                DeleteFile(context);
            }
            else
            {

                if (string.IsNullOrEmpty(headers["X-File-Name"]))
                {
                    UploadWholeFile(context, statuses);
                }
                else
                {
                    UploadPartialFile(headers["X-File-Name"], context, statuses);
                }

                WriteJsonIframeSafe(context, statuses);
            }
		}

        // Upload partial file
        private void UploadPartialFile(string fileName, HttpContext context, List<Resource_FilesStatus> statuses)
        {
   
            if (context.Request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
            var file = context.Request.Files[0];


           

            if (file != null && !String.IsNullOrEmpty(file.FileName) && file.ContentLength > 0)
            {

                //验证后缀名是否符合
                bool Valid = FileSystemUtils.CheckValidFileName(file.FileName);
                if (Valid)
                {


                    DNNGo_DNNGalleryProGame_Files PhotoItem = new DNNGo_DNNGalleryProGame_Files();

                    PhotoItem.ModuleId = WebHelper.GetIntParam(context.Request, "ModuleId", 0);
                    PhotoItem.PortalId = WebHelper.GetIntParam(context.Request, "PortalId", 0);


                    PhotoItem.FileName = file.FileName;
                    PhotoItem.FileSize = file.ContentLength / 1024;
                    PhotoItem.FileMate = WebHelper.leftx(FileSystemUtils.GetContentType(Path.GetExtension(PhotoItem.FileName).Replace(".", "")), 30);

                    PhotoItem.FileExtension = System.IO.Path.GetExtension(PhotoItem.FileName).Replace(".", "");
                    PhotoItem.Name = System.IO.Path.GetFileName(file.FileName).Replace(Path.GetExtension(PhotoItem.FileName), "");

                    PhotoItem.Status = (Int32)EnumFileStatus.Approved;

                    try
                    {
                        if (("png,gif,jpg,jpeg,bmp").IndexOf(PhotoItem.FileExtension) >= 0)
                        {
                            //图片的流
                            Image image = Image.FromStream(file.InputStream);
                            PhotoItem.ImageWidth = image.Width;
                            PhotoItem.ImageHeight = image.Height;

                            PhotoItem.Exif = Common.Serialize<EXIFMetaData.Metadata>(new EXIFMetaData().GetEXIFMetaData(image));
                        }
                    }
                    catch
                    {

                    }

                    PhotoItem.LastTime = xUserTime.UtcTime();
                    PhotoItem.LastIP = WebHelper.UserHost;
                    PhotoItem.LastUser = UserInfo.UserID;

                    //将文件存储的路径整理好
                    fileName = System.IO.Path.GetFileName(FileSystemUtils.HandleFileName(file.FileName)).Replace("." + PhotoItem.FileExtension, ""); //文件名称
                    String WebPath = String.Format("DNNGalleryProGame/uploads/{0}/{1}/{2}/", PhotoItem.LastTime.Year, PhotoItem.LastTime.Month, PhotoItem.LastTime.Day);
                    //检测文件存储路径是否有相关的文件
                    System.IO.FileInfo fInfo = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(String.Format("{0}{1}{2}.{3}", PortalSettings.HomeDirectory, WebPath, fileName, PhotoItem.FileExtension)));

                    //检测文件夹是否存在
                    if (!System.IO.Directory.Exists(fInfo.Directory.FullName))
                    {
                        System.IO.Directory.CreateDirectory(fInfo.Directory.FullName);
                    }
                    else
                    {
                        Int32 j = 1;
                        while (fInfo.Exists)
                        {
                            //文件已经存在了
                            fileName = String.Format("{0}_{1}", FileSystemUtils.HandleFileName( PhotoItem.Name), j);
                            fInfo = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(String.Format("{0}{1}{2}.{3}", PortalSettings.HomeDirectory, WebPath, fileName, PhotoItem.FileExtension)));
                            j++;
                        }
                    }

                    PhotoItem.FilePath = String.Format("{0}{1}.{2}", WebPath, fileName, PhotoItem.FileExtension);
                    PhotoItem.FileName = String.Format("{0}.{1}", fileName, PhotoItem.FileExtension);

                    try
                    {

                        if (!fInfo.Directory.Exists)
                        {
                            fInfo.Directory.Create();

                            // FileSystemUtils.AddFolder(PortalSettings, String.Format("{0}DNNGo_PhotoAlbums/{0}/{1}/"), String.Format("{0}DNNGo_PhotoAlbums/{0}/{1}/"), (int)DotNetNuke.Services.FileSystem.FolderController.StorageLocationTypes.InsecureFileSystem);
                        }



                        //构造指定存储路径
                        file.SaveAs(fInfo.FullName);
                        //FileSystemUtils.AddFile(PhotoItem.FileName, PhotoItem.PortalId, String.Format("DNNGo_PhotoAlbums\\{0}\\{1}\\", PhotoItem.ModuleId, PhotoItem.AlbumID), PortalSettings.HomeDirectoryMapPath, PhotoItem.FileMeta);
                    }
                    catch (Exception ex)
                    {

                    }

                    //给上传的相片设置初始的顺序
                    QueryParam qp = new QueryParam();
                    qp.ReturnFields = qp.Orderfld = DNNGo_DNNGalleryProGame_Files._.Sort;
                    qp.OrderType = 1;
                    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Files._.PortalId, PhotoItem.PortalId, SearchType.Equal));
                    PhotoItem.Sort = Convert.ToInt32(DNNGo_DNNGalleryProGame_Files.FindScalar(qp)) + 2;
                    Int32 PhotoId = PhotoItem.Insert();


                    statuses.Add(new Resource_FilesStatus(PhotoItem, PortalSettings, ModulePath));
                }
                else
                {
                    throw new HttpRequestValidationException("Following file was imported/uploaded, but is not an authorized filetype: "+ file.FileName);
                }
            }

        }

		// Upload entire file
        private void UploadWholeFile(HttpContext context, List<Resource_FilesStatus> statuses)
        {
 
            for (int i = 0; i < context.Request.Files.Count; i++) {
				var file = context.Request.Files[i];

                if (file != null && !String.IsNullOrEmpty(file.FileName) && file.ContentLength > 0)
                {

                    //验证后缀名是否符合
                    bool Valid = FileSystemUtils.CheckValidFileName(file.FileName);
                    if (Valid)
                    {



                        DNNGo_DNNGalleryProGame_Files PhotoItem = new DNNGo_DNNGalleryProGame_Files();

                        PhotoItem.ModuleId = WebHelper.GetIntParam(context.Request, "ModuleId", 0);
                        PhotoItem.PortalId = WebHelper.GetIntParam(context.Request, "PortalId", 0);


                        PhotoItem.FileName = file.FileName;
                        PhotoItem.FileSize = file.ContentLength / 1024;
                        PhotoItem.FileMate = WebHelper.leftx(FileSystemUtils.GetContentType(Path.GetExtension(PhotoItem.FileName).Replace(".", "")), 30);

                        PhotoItem.FileExtension = System.IO.Path.GetExtension(PhotoItem.FileName).Replace(".", "");
                        PhotoItem.Name = System.IO.Path.GetFileName(file.FileName).Replace(Path.GetExtension(PhotoItem.FileName), "");
                        PhotoItem.Status = (Int32)EnumFileStatus.Approved;

                        try
                        {
                            if (("png,gif,jpg,jpeg,bmp").IndexOf(PhotoItem.FileExtension) >= 0)
                            {
                                //图片的流
                                Image image = Image.FromStream(file.InputStream);
                                PhotoItem.ImageWidth = image.Width;
                                PhotoItem.ImageHeight = image.Height;

                                PhotoItem.Exif = Common.Serialize<EXIFMetaData.Metadata>(new EXIFMetaData().GetEXIFMetaData(image));
                            }
                        }
                        catch
                        {

                        }

                        PhotoItem.LastTime = xUserTime.UtcTime();
                        PhotoItem.LastIP = WebHelper.UserHost;
                        PhotoItem.LastUser = UserInfo.UserID;


                        //将文件存储的路径整理好
                        String fileName = FileSystemUtils.HandleFileName( System.IO.Path.GetFileName(file.FileName).Replace("." + PhotoItem.FileExtension, "")); //文件名称
                        String WebPath = String.Format("DNNGalleryProGame/uploads/{0}/{1}/{2}/", PhotoItem.LastTime.Year, PhotoItem.LastTime.Month, PhotoItem.LastTime.Day);
                        //检测文件存储路径是否有相关的文件
                        System.IO.FileInfo fInfo = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(String.Format("{0}{1}{2}.{3}", PortalSettings.HomeDirectory, WebPath, fileName, PhotoItem.FileExtension)));

                        //检测文件夹是否存在
                        if (!System.IO.Directory.Exists(fInfo.Directory.FullName))
                        {
                            System.IO.Directory.CreateDirectory(fInfo.Directory.FullName);
                        }
                        else
                        {
                            Int32 j = 1;
                            while (fInfo.Exists)
                            {
                                //文件已经存在了
                                fileName = String.Format("{0}_{1}", FileSystemUtils.HandleFileName( PhotoItem.Name), j);
                                fInfo = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(String.Format("{0}{1}{2}.{3}", PortalSettings.HomeDirectory, WebPath, fileName, PhotoItem.FileExtension)));
                                j++;
                            }
                        }

                        PhotoItem.FilePath = String.Format("{0}{1}.{2}", WebPath, fileName, PhotoItem.FileExtension);
                        PhotoItem.FileName = String.Format("{0}.{1}", fileName, PhotoItem.FileExtension);
                        try
                        {

                            if (!fInfo.Directory.Exists)
                            {
                                fInfo.Directory.Create();

                                // FileSystemUtils.AddFolder(PortalSettings, String.Format("{0}DNNGo_PhotoAlbums/{0}/{1}/"), String.Format("{0}DNNGo_PhotoAlbums/{0}/{1}/"), (int)DotNetNuke.Services.FileSystem.FolderController.StorageLocationTypes.InsecureFileSystem);
                            }

                            //构造指定存储路径
                            file.SaveAs(fInfo.FullName);
                            //FileSystemUtils.AddFile(PhotoItem.FileName, PhotoItem.PortalId, String.Format("DNNGo_PhotoAlbums\\{0}\\{1}\\", PhotoItem.ModuleId, PhotoItem.AlbumID), PortalSettings.HomeDirectoryMapPath, PhotoItem.FileMeta);
                        }
                        catch (Exception ex)
                        {

                        }

                        //给上传的相片设置初始的顺序
                        QueryParam qp = new QueryParam();
                        qp.ReturnFields = qp.Orderfld = DNNGo_DNNGalleryProGame_Files._.Sort;
                        qp.OrderType = 1;
                        qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Files._.PortalId, PhotoItem.PortalId, SearchType.Equal));
                        PhotoItem.Sort = Convert.ToInt32(DNNGo_DNNGalleryProGame_Files.FindScalar(qp)) + 2;
                        Int32 PhotoId = PhotoItem.Insert();



                        statuses.Add(new Resource_FilesStatus(PhotoItem, PortalSettings, ModulePath));
                    }
                    else
                    {
                        throw new HttpRequestValidationException("Following file was imported/uploaded, but is not an authorized filetype: " + file.FileName);
                    }
                }
                
			}
		}




        private void WriteJsonIframeSafe(HttpContext context, List<Resource_FilesStatus> statuses)
        {
			context.Response.AddHeader("Vary", "Accept");
			try {
				if (context.Request["HTTP_ACCEPT"].Contains("application/json"))
					context.Response.ContentType = "application/json";
				else
					context.Response.ContentType = "text/plain";
			} catch {
				context.Response.ContentType = "text/plain";
			}

			var jsonObj = js.Serialize(statuses.ToArray());
			context.Response.Write(jsonObj);
		}

		private static bool GivenFilename (HttpContext context) {
            return !string.IsNullOrEmpty(context.Request["PhotoID"]);
		}

		private void DeliverFile (HttpContext context) {

            DNNGo_DNNGalleryProGame_Files DataItem = DNNGo_DNNGalleryProGame_Files.FindByKeyForEdit(WebHelper.GetStringParam(context.Request, "PhotoID", "0"));
            if (DataItem != null && DataItem.ID > 0)
            {
                String Picture = GetPhotoPath(DataItem);

                if (!String.IsNullOrEmpty(Picture) && File.Exists(context.Server.MapPath(Picture)))
               {
                   context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + DataItem.FileName + "\"");
                   context.Response.ContentType = "application/octet-stream";
                   context.Response.ClearContent();
                   context.Response.WriteFile(context.Server.MapPath(Picture));
               }
               else
                   context.Response.StatusCode = 404;
            }else
                context.Response.StatusCode = 404;



            //var filename = context.Request["PhotoID"];
            //var filePath = StorageRoot + filename;

            //if (File.Exists(filePath)) {
            //    context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
            //    context.Response.ContentType = "application/octet-stream";
            //    context.Response.ClearContent();
            //    context.Response.WriteFile(filePath);
            //} else
            //    context.Response.StatusCode = 404;
		}

		private void ListCurrentFiles (HttpContext context) {
 
            //QueryParam qp = new QueryParam();
            //qp.PageSize = 0;

            //int RecordCount = 0;
            //qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Files._.AlbumID, WebHelper.GetStringParam(context.Request, "AlbumID", "0"), SearchType.Equal));

            //var files = DNNGo_DNNGalleryProGame_Files.FindAll(qp, out RecordCount).Select(f => new Resource_FilesStatus(f, PortalSettings)).ToArray();
            var files = new List<Resource_FilesStatus>();
 
            string jsonObj = js.Serialize(files);
            context.Response.AddHeader("Content-Disposition", "inline; filename=\"files.json\"");
            context.Response.Write(jsonObj);
            context.Response.ContentType = "application/json";
		}

   


    }
}