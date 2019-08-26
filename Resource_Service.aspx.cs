
using System;
using System.Web;
using System.Collections.Generic;
using DotNetNuke.Entities.Users;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;
using System.IO;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Xml;
using DotNetNuke.Common;
using System.Web.Script.Serialization;
using DotNetNuke.Entities.Tabs;

namespace DNNGo.Modules.DNNGalleryProGame
{
    /// <summary>
    /// 资源文件(主要用于一些请求的服务)
    /// 1.文件上传
    /// </summary>
    public partial class Resource_Service : BasePage
    {

        #region "属性"
        /// <summary>
        /// 功能
        /// 文件上传 FileUpload
        /// </summary>
        private String Token = WebHelper.GetStringParam(HttpContext.Current.Request, "Token", "").ToLower();


        #endregion


    


        protected override void Page_Init(System.Object sender, System.EventArgs e)
        {
            if (!String.IsNullOrEmpty(Token))
            {
    
                 if (Token.ToLower() == "deletelayeritem")
                {
                    //调用基类Page_Init，主要用于权限验证
                    base.Page_Init(sender, e);
                }
                else if (Token.ToLower() == "checkedlayeritem")
                {
                    //调用基类Page_Init，主要用于权限验证
                    base.Page_Init(sender, e);
                }
                else if (Token.ToLower() == "layerlistsort")
                {
                    //调用基类Page_Init，主要用于权限验证
                    base.Page_Init(sender, e);
                }
            


            }
           
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Token))
                {
                    if (Token.ToLower() == "thumbnail")
                    {
                        PushThumbnail();
                    }
                    else if (Token.ToLower() == "ajaxsliders")
                    {
                        PushAjaxSliders();
                    }
                    else if (Token.ToLower() == "layerlisthtml")
                    {
                        PushLayerListHtml();
                    }
                    else if (Token.ToLower() == "deletelayeritem")
                    {
                        DeleteLayerItem();
                    }
                    else if (Token.ToLower() == "checkedlayeritem")
                    {
                        CheckedLayerItem();
                    }
                    else if (Token.ToLower() == "layerlistsort")
                    {
                        LayerListSort();
                    }
                    else if (Token.ToLower() == "picturelist")
                    {
                        PushPictureList();
                    }
                    else if (Token.ToLower() == "pictureitem")
                    {
                        PushPictureItem();
                    }
                    else if (Token.ToLower() == "custommodels")
                    {
                        //推送自定义字段的信息
                        PushCustomModels();
                    }
                    else if (Token.ToLower() == "download")
                    {
                        FileDownLoads();//下载文件
                    }
                    else if (Token.ToLower() == "heats")
                    {
                        ClickHeats();//热度
                    }
                     
                }
            }
        }


        #region "缩略图的生成"
        /// <summary>
        /// 缩略图的生成
        /// </summary>
        private void PushThumbnail()
        {
            //根据ID查询出缩略的方式
            Int32 ItemID = WebHelper.GetIntParam(Request, "ID", 0);
            Int32 Width = WebHelper.GetIntParam(Request, "width", 200);
            Int32 height = WebHelper.GetIntParam(Request, "height", 200);
            String Mode = WebHelper.GetStringParam(Request, "mode", "AUTO");
            String PhotoType = WebHelper.GetStringParam(Request, "Type", "p").ToLower();

            String ImagePath = MapPath(String.Format("{0}/Resource/images/no_image.png", TemplateSourceDirectory));



            if (ItemID > 0)
            {
                DNNGo_DNNGalleryProGame_Slider ContentItem = DNNGo_DNNGalleryProGame_Slider.FindByID(ItemID);
                if (ContentItem != null && ContentItem.ID > 0)
                {


                    //if (PhotoType == "p")
                    //    ImagePath = ViewLinkUrl(ContentItem.Picture, ContentItem.PortalId);
                    //else
                    //    ImagePath = ViewLinkUrl(ContentItem.Thumbnails, ContentItem.PortalId);

                }
            }
            GenerateThumbnail.PushThumbnail(Server.UrlDecode(ImagePath), Width, height, Mode);


        }



        #endregion

        #region "前台效果所有的方法"


        /// <summary>
        /// Layer列表的HTML生成
        /// </summary>
        public void PushAjaxSliders()
        {

            EffectDBEntity EffectDB = Setting_EffectDB;

            QueryParam qp = new QueryParam();
            qp.OrderType = 0;
            qp.PageIndex = WebHelper.GetIntParam(Request, "PageIndex", 2);

            if (qp.PageIndex <= 1)
            {
                qp.PageSize = WebHelper.GetIntParam(Request, "FirstScreen", 10);
                if (qp.PageSize <= 0)
                {
                    qp.PageSize = WebHelper.GetIntParam(Request, "LoadDisplay", 10);
                }
            }
            else
            {
                qp.PageSize = WebHelper.GetIntParam(Request, "LoadDisplay", 10);
                qp.FirstScreen = WebHelper.GetIntParam(Request, "FirstScreen", 10);
            }

               



            qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.ModuleId, Settings_ModuleID, SearchType.Equal));
            qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.Status, (Int32)EnumStatus.Activated, SearchType.Equal));

            Int32 Search_GroupID = WebHelper.GetIntParam(HttpContext.Current.Request, "GroupID", 0);
            if (Search_GroupID > 0)
            {
                qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.ID, DNNGo_DNNGalleryProGame_Slider_Group.BuilderArticleIDs(Search_GroupID), SearchType.In));
            }

            int RecordCount = 0;
            List<DNNGo_DNNGalleryProGame_Slider> SliderList = new List<DNNGo_DNNGalleryProGame_Slider>();


            Int32 Sortby = Settings_Sortby;
            String Orderfld = WebHelper.GetStringParam(Request, "Order", "");
            Int32 OrderType = WebHelper.GetIntParam(Request, "OrderBy", -1);

            if (!String.IsNullOrEmpty(Orderfld))
            {
                if (Orderfld.ToLower() == "title")
                {
                    Sortby = OrderType == 1 ? (Int32)EnumSortby.Title_Desc : (Int32)EnumSortby.Title;
                }
                else if (Orderfld.ToLower() == "starttime")
                {
                    Sortby = OrderType == 1 ? (Int32)EnumSortby.Time_Desc : (Int32)EnumSortby.Time;
                }
               

            }


            if (Sortby == (Int32)EnumSortby.Time)
            {
                qp.Orderfld = DNNGo_DNNGalleryProGame_Slider._.StartTime;
                SliderList = DNNGo_DNNGalleryProGame_Slider.FindAll(qp, out RecordCount);
            }
            else if (Sortby == (Int32)EnumSortby.Time_Desc)
            {
                qp.OrderType = 1;
                qp.Orderfld = DNNGo_DNNGalleryProGame_Slider._.StartTime;
                SliderList = DNNGo_DNNGalleryProGame_Slider.FindAll(qp, out RecordCount);
            }
            else if (Sortby == (Int32)EnumSortby.Title)
            {
                qp.Orderfld = DNNGo_DNNGalleryProGame_Slider._.Title;
                SliderList = DNNGo_DNNGalleryProGame_Slider.FindAll(qp, out RecordCount);
            }
            else if (Sortby == (Int32)EnumSortby.Title_Desc)
            {
                qp.OrderType = 1;
                qp.Orderfld = DNNGo_DNNGalleryProGame_Slider._.Title;
                SliderList = DNNGo_DNNGalleryProGame_Slider.FindAll(qp, out RecordCount);
            }
            else if (Sortby == (Int32)EnumSortby.Random)
            {
                qp.Orderfld = " newid() ";
                SliderList = DNNGo_DNNGalleryProGame_Slider.FindRandomAll(qp, out RecordCount);
            }
            else
            {
                qp.Orderfld = DNNGo_DNNGalleryProGame_Slider._.Sort;
                SliderList = DNNGo_DNNGalleryProGame_Slider.FindAll(qp, out RecordCount);
            }
          
            

            if (qp.Pages < qp.PageIndex) //索引数大于总页面数时不返回记录
            {
                SliderList = new List<DNNGo_DNNGalleryProGame_Slider>();
            }


            Dictionary<String, Object> jsonSliders = new Dictionary<string, Object>();
        

            TemplateFormat xf = new TemplateFormat();

 


            foreach (var SliderItem in SliderList)
            {
                int index = SliderList.IndexOf(SliderItem); //index 为索引值

                Dictionary<String, Object> jsonSlider = new Dictionary<String, Object>();


                
                jsonSlider.Add("ID", SliderItem.ID);
                jsonSlider.Add("Sort", SliderItem.Sort);
                jsonSlider.Add("CreateTime", SliderItem.CreateTime);
                jsonSlider.Add("StartTime", SliderItem.StartTime);
                jsonSlider.Add("EndTime", SliderItem.EndTime);

                jsonSlider.Add("CreateTimeUS", SliderItem.CreateTime.ToString("MM/dd/yyy HH:mm:ss"));
                jsonSlider.Add("StartTimeUS", SliderItem.StartTime.ToString("MM/dd/yyy HH:mm:ss"));
                jsonSlider.Add("EndTimeUS", SliderItem.EndTime.ToString("MM/dd/yyy HH:mm:ss"));

                jsonSlider.Add("Status", SliderItem.Status == 1 ? "checked=\"checked\"" : "");

                jsonSlider.Add("Pages", qp.Pages);

                foreach (var SliderSetting in Setting_SliderSettingDB)
                {
                    String SliderName = SliderSetting.Name;
                    String SliderValue = xf.ViewSliderSettingT<String>(SliderItem, SliderName, SliderSetting.DefaultValue);

                    if (SliderSetting.ControlType == EnumControlType.Urls.ToString())
                    {
                        jsonSlider.Add(SliderName, ViewLinkUrl(SliderValue, false));
                    }
                    else
                    {
                        jsonSlider.Add(SliderName, SliderValue);
                    }
                }

                //加载分组
                if (EffectDB.Groups)
                {
                    jsonSlider.Add("Groups", xf.ToGroups(SliderItem));
                }
                //取出Layers数据
                if (EffectDB.Layers)
                {
                    Dictionary<String, Object> jsonLayers = new Dictionary<String, Object>();
                    List<DNNGo_DNNGalleryProGame_Layer> LayerList = SliderItem.LayerList;
                    if (LayerList != null && LayerList.Count > 0)
                    {
                        foreach (var Layer in LayerList)
                        {
                            int indexLayer = LayerList.IndexOf(Layer);
                            Dictionary<String, Object> jsonLayer = new Dictionary<String, Object>();
                            foreach (var LayerSetting in Setting_LayerSettingDB)
                            {
                                String LayerName = LayerSetting.Name;
                                String LayerValue = xf.ViewLayerSettingT<String>(Layer, LayerName, LayerSetting.DefaultValue);

                                if (LayerSetting.ControlType == EnumControlType.Urls.ToString())
                                {
                                    jsonLayer.Add(LayerName, ViewLinkUrl(LayerValue, false));
                                }
                                else
                                {
                                    jsonLayer.Add(LayerName, LayerValue);
                                }
                            }
                            jsonLayer.Add("ID", Layer.ID);
                            jsonLayer.Add("Sort", Layer.Sort);
                            jsonLayer.Add("Clicks", Layer.Clicks);
                            jsonLayer.Add("Views", Layer.Views);
                            jsonLayer.Add("Heats", Layer.Heats);
                            jsonLayer.Add("CreateTime", Layer.CreateTime);


                            jsonLayers.Add((10000 - indexLayer).ToString(), jsonLayer);
                        }


                    }
                    jsonSlider.Add("Layers", jsonLayers);
                }

                //取出扩展项
                Dictionary<String, Object> jsonExtension = new Dictionary<String, Object>();
                if (!String.IsNullOrEmpty(SliderItem.Extension))
                {
                    List<KeyValueEntity> ExtensionSettings = ConvertTo.Deserialize<List<KeyValueEntity>>(SliderItem.Extension);
                    foreach (KeyValueEntity kv in ExtensionSettings)
                    {
                        if (!jsonExtension.ContainsKey(kv.Key))
                        {
                            jsonExtension.Add(kv.Key, kv.Value);
                        }
                    }
                }
                jsonSlider.Add("Extensions", jsonExtension);

 

                jsonSliders.Add(index.ToString(), jsonSlider);

            }

            //转换数据为json
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            Response.Clear();
            Response.Write(jsSerializer.Serialize(jsonSliders));
            Response.End();

        }







        #endregion

        #region "Urls控件用的方法"



        /// <summary>
        /// 推送图片列表数据
        /// </summary>
        public void PushPictureList()
        {
            QueryParam qp = new QueryParam();
            qp.Orderfld = DNNGo_DNNGalleryProGame_Layer._.ID;
            qp.OrderType = 1;

            qp.PageIndex = WebHelper.GetIntParam(Request, "PageIndex", 1);
            qp.PageSize = WebHelper.GetIntParam(Request, "PageSize", Int32.MaxValue);
            qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Files._.Status, (Int32)EnumStatus.Activated, SearchType.Equal));
            qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Files._.PortalId, PortalId, SearchType.Equal));


            int RecordCount = 0;
            List<DNNGo_DNNGalleryProGame_Files> fileList = DNNGo_DNNGalleryProGame_Files.FindAll(qp, out RecordCount);

            Dictionary<String, Object> jsonLayers = new Dictionary<string, Object>();

            TemplateFormat xf = new TemplateFormat();

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            foreach (var fileItem in fileList)
            {
                int index = fileList.IndexOf(fileItem); //index 为索引值

                Dictionary<String, Object> jsonLayer = new Dictionary<String, Object>();

                jsonLayer.Add("Pages", qp.Pages);


                jsonLayer.Add("ID", fileItem.ID);

                jsonLayer.Add("CreateTime", fileItem.LastTime);

                jsonLayer.Add("Name", WebHelper.leftx(fileItem.Name, 20, "..."));
                jsonLayer.Add("FileName", fileItem.Name);
                jsonLayer.Add("Extension", fileItem.FileExtension);


                String ThumbnailUrl = ViewLinkUrl(String.Format("MediaID={0}", fileItem.ID));
                jsonLayer.Add("ThumbnailUrl", ThumbnailUrl);
                jsonLayer.Add("FileUrl", GetPhotoPath(fileItem.FilePath));

                jsonLayer.Add("Thumbnail", String.Format("<img style=\"border-width:0px; max-height:60px;max-width:80px;\"  src=\"{0}\"  /> ", ThumbnailUrl));

                //判断当前文件是否为图片
                if (!String.IsNullOrEmpty(fileItem.FileExtension) && ("gif,jpg,jpeg,bmp,png").IndexOf(fileItem.FileExtension, StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    jsonLayer.Add("IsPicture", true);
                }
                else
                {
                    jsonLayer.Add("IsPicture", false);
                }



                jsonLayer.Add("Json", jsSerializer.Serialize(jsonLayer));

                jsonLayers.Add(index.ToString(), jsonLayer);

            }

            //转换数据为json

            Response.Clear();
            Response.Write(jsSerializer.Serialize(jsonLayers));
            Response.End();
        }


        /// <summary>
        /// 推送图片列表数据
        /// </summary>
        public void PushPictureItem()
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            Dictionary<String, Object> jsonLayer = new Dictionary<String, Object>();

            Int32 MediaID = WebHelper.GetIntParam(Request, "MediaID", 0);
            if (MediaID > 0)
            {

                DNNGo_DNNGalleryProGame_Files PictureItem = DNNGo_DNNGalleryProGame_Files.FindByKeyForEdit(MediaID);
                if (PictureItem != null && PictureItem.ID > 0)
                {
                    jsonLayer.Add("ID", PictureItem.ID);
                    jsonLayer.Add("CreateTime", PictureItem.LastTime);
                    jsonLayer.Add("Name", PictureItem.Name);
                    jsonLayer.Add("Extension", PictureItem.FileExtension);
                    String ThumbnailUrl = ViewLinkUrl(String.Format("MediaID={0}", PictureItem.ID));
                    jsonLayer.Add("ThumbnailUrl", ThumbnailUrl);
                    jsonLayer.Add("FileUrl", GetPhotoPath(PictureItem.FilePath));


                    jsonLayer.Add("Thumbnail", String.Format("<img style=\"border-width:0px; max-height:60px;max-width:80px;\"  src=\"{0}\"  /> ", ThumbnailUrl));
                }

            }

            //转换数据为json
            Response.Clear();
            Response.Write(jsSerializer.Serialize(jsonLayer));
            Response.End();
        }

        /// <summary>
        /// 显示URL控件存放的值
        /// </summary>
        /// <param name="UrlValue"></param>
        /// <returns></returns>
        public String ViewLinkUrl(String UrlValue)
        {
            return ViewLinkUrl(UrlValue, true);
        }

        /// <summary>
        /// 显示URL控件存放的值
        /// </summary>
        /// <param name="UrlValue"></param>
        /// <param name="IsPhotoExtension">是否显示扩展名图片</param>
        /// <returns></returns>
        public String ViewLinkUrl(String UrlValue, Boolean IsPhotoExtension)
        {
            String DefaultValue = String.Empty;
            if (!String.IsNullOrEmpty(UrlValue) && UrlValue != "0")
            {
                if (UrlValue.IndexOf("FileID=", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    int FileID = 0;
                    if (int.TryParse(UrlValue.Replace("FileID=", ""), out FileID) && FileID > 0)
                    {
                  
                        var fi = DotNetNuke.Services.FileSystem.FileManager.Instance.GetFile(FileID);
                        if (fi != null && fi.FileId > 0)
                        {
                            DefaultValue = string.Format("{0}{1}{2}", DNNGalleryProGame_PortalSettings.HomeDirectory, fi.Folder, Server.UrlPathEncode(fi.FileName));
                        }
                    }
                }
                else if (UrlValue.IndexOf("MediaID=", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    DefaultValue = String.Format("{0}Resource/images/no_image.png", ModulePath);

                    int MediaID = 0;
                    if (int.TryParse(UrlValue.Replace("MediaID=", ""), out MediaID) && MediaID > 0)
                    {
                        DNNGo_DNNGalleryProGame_Files Multimedia = DNNGo_DNNGalleryProGame_Files.FindByID(MediaID);
                        if (Multimedia != null && Multimedia.ID > 0)
                        {
                            if (IsPhotoExtension)
                            {
                                DefaultValue = Server.UrlPathEncode(GetPhotoExtension(Multimedia.FileExtension, Multimedia.FilePath));// String.Format("{0}{1}", bpm.DNNGalleryProGame_PortalSettings.HomeDirectory, Multimedia.FilePath);
                            }
                            else
                            {
                                DefaultValue = Server.UrlPathEncode(GetPhotoPath(Multimedia.FilePath));
                            }
                        }
                    }
                }
                else if (UrlValue.IndexOf("TabID=", StringComparison.CurrentCultureIgnoreCase) == 0)
                {

                    DefaultValue = Globals.NavigateURL(Convert.ToInt32(UrlValue.Replace("TabID=", "")), false, DNNGalleryProGame_PortalSettings, Null.NullString, "", "");

                }
                else
                {
                    DefaultValue = UrlValue;
                }
            }
            return DefaultValue;

        }

        #endregion

        #region "后台管理用的方法"



        /// <summary>
        /// 排序列表
        /// </summary>
        public void LayerListSort()
        {
            Int32 ReturnString = 0;

            Int32 SliderID = WebHelper.GetIntParam(Request, "SliderID", 0);

            if (SliderID > 0)
            {

                String SortList = WebHelper.GetStringParam(Request, "json", "");
                if (!String.IsNullOrEmpty(SortList))
                {
                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

                    try
                    {
                        object[] Sorts = jsSerializer.DeserializeObject(SortList) as object[];

                        if (Sorts != null && Sorts.Length > 0)
                        {
                            foreach (object so in Sorts)
                            {
                                Dictionary<string, object> SortDict = so as Dictionary<string, object>;

                                if (SortDict != null && SortDict.Count == 2)
                                {
                                    String setClause = String.Format(" {0} = {1} ", DNNGo_DNNGalleryProGame_Layer._.Sort, SortDict[DNNGo_DNNGalleryProGame_Layer._.Sort]);
                                    String whereClause = String.Format(" {0} = {1} ", DNNGo_DNNGalleryProGame_Layer._.ID, SortDict[DNNGo_DNNGalleryProGame_Layer._.ID]);

                                    ReturnString += DNNGo_DNNGalleryProGame_Layer.Update(setClause, whereClause);


                                }


                            }
                        }

                    }
                    catch
                    {

                    }

                }


            }
            Response.Clear();
            Response.Write(ReturnString);
            Response.End();
        }



        /// <summary>
        /// 选中激活LayerItem
        /// </summary>
        public void CheckedLayerItem()
        {
            String ReturnString = "0";

            Int32 SliderID = WebHelper.GetIntParam(Request, "SliderID", 0);

            if (SliderID > 0)
            {
                Int32 LayerID = WebHelper.GetIntParam(Request, "LayerID", 0);
                if (LayerID > 0)
                {
                    DNNGo_DNNGalleryProGame_Layer Layer = DNNGo_DNNGalleryProGame_Layer.FindByKeyForEdit(LayerID);


                    Layer.Status = WebHelper.GetBooleanParam(Request, "checked", false) ? (Int32)EnumStatus.Activated : (Int32)EnumStatus.Hidden;


                    ReturnString = Layer.Update().ToString();
                }
            }
            Response.Clear();
            Response.Write(ReturnString);
            Response.End();
        }


        /// <summary>
        /// 删除LayerItem
        /// </summary>
        public void DeleteLayerItem()
        {
            String ReturnString = "0";

            Int32 SliderID = WebHelper.GetIntParam(Request, "SliderID", 0);

            if (SliderID > 0)
            {
                Int32 LayerID = WebHelper.GetIntParam(Request, "LayerID", 0);
                if (LayerID > 0)
                {
                    DNNGo_DNNGalleryProGame_Layer Layer = new DNNGo_DNNGalleryProGame_Layer();
                    Layer.ID = LayerID;

                    ReturnString = Layer.Delete().ToString();
                }
            }
            Response.Clear();
            Response.Write(ReturnString);
            Response.End();
        }





        /// <summary>
        /// Layer列表的HTML生成
        /// </summary>
        public void PushLayerListHtml()
        {
            Int32 SliderID = WebHelper.GetIntParam(Request, "SliderID", 0);

            if (SliderID > 0)
            {
                QueryParam qp = new QueryParam();
                qp.Orderfld = DNNGo_DNNGalleryProGame_Layer._.Sort;
                qp.OrderType = 0;
                qp.PageIndex = WebHelper.GetIntParam(Request, "PageIndex", 1);
                qp.PageSize = WebHelper.GetIntParam(Request, "PageSize", Int32.MaxValue);

                Int32 LayerID = WebHelper.GetIntParam(Request, "LayerID", 0);
                if (LayerID > 0)
                {
                    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Layer._.ID, LayerID, SearchType.Equal));
                }

                qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Layer._.SliderID, SliderID, SearchType.Equal));


                int RecordCount = 0;
                List<DNNGo_DNNGalleryProGame_Layer> LayerList = DNNGo_DNNGalleryProGame_Layer.FindAll(qp, out RecordCount);

                Dictionary<String, Object> jsonLayers = new Dictionary<string, Object>();

                TemplateFormat xf = new TemplateFormat();


                foreach (var LayerItem in LayerList)
                {
                    int index = LayerList.IndexOf(LayerItem); //index 为索引值

                    Dictionary<String, Object> jsonLayer = new Dictionary<String, Object>();

                    jsonLayer.Add("ID", LayerItem.ID);
                    jsonLayer.Add("Sort", LayerItem.Sort);
                    jsonLayer.Add("CreateTime", LayerItem.CreateTime);
                    jsonLayer.Add("Status", LayerItem.Status == 1 ? "checked=\"checked\"" : "");
                    jsonLayer.Add("Title", WebHelper.leftx(xf.ViewLayerSettingT<String>(LayerItem, "Title", ""), 30, "..."));


                    String LayerType = xf.ViewLayerSettingT<String>(LayerItem, "LayerType", "");
                    jsonLayer.Add("LayerType", String.IsNullOrEmpty(LayerType) ? "Common" : LayerType);

                    jsonLayer.Add("Edit", String.Format("{0}Resource_Item.aspx?ModuleId={1}&PortalId={2}&TabId={3}&SliderID={4}&LayerID={5}&LayerType={6}", ModulePath, ModuleId, PortalId, TabId, SliderID, LayerItem.ID, LayerType));



                    String Thumbnail = xf.ViewLayerSettingT<String>(LayerItem, "Thumbnail", "");
                    if (String.IsNullOrEmpty(Thumbnail))
                    {
                        Thumbnail = xf.ViewLayerSettingT<String>(LayerItem, "Picture", "");
                    }

                    if (!String.IsNullOrEmpty(Thumbnail))
                    {
                        jsonLayer.Add("Thumbnail", String.Format("<img style=\"border-width:0px; max-height:60px;max-width:80px;\"  src=\"{0}\" onError=\"this.src='{1}Resource/images/no_image.png'\"  /> ", ViewLinkUrl(Thumbnail), ModulePath));
                    }
                    else
                    {
                        jsonLayer.Add("Thumbnail", "");
                    }
                    jsonLayers.Add( (10000-  index).ToString(), jsonLayer);

                }

                //转换数据为json
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                Response.Clear();
                Response.Write(jsSerializer.Serialize(jsonLayers));
                Response.End();

            }




        }


        #endregion


        #region "创建文章自定义模型的JSON信息"
        /// <summary>
        /// 创建文章自定义模型的JSON信息
        /// </summary>
        public void PushCustomModels()
        {
            Dictionary<String, Object> jsonCustomModels = new Dictionary<String, Object>();

            //读取出该事件的信息
            Int32 SliderID = WebHelper.GetIntParam(HttpContext.Current.Request, "SliderID", 0);
            if (SliderID > 0)
            {
                DNNGo_DNNGalleryProGame_Slider EventItem = DNNGo_DNNGalleryProGame_Slider.FindByKeyForEdit(SliderID);

                if (EventItem != null && EventItem.ID > 0 && !String.IsNullOrEmpty(EventItem.Extension))
                {
                    //取出扩展项
                    List<KeyValueEntity> ExtensionSettings = ConvertTo.Deserialize<List<KeyValueEntity>>(EventItem.Extension);

                    foreach (KeyValueEntity k in ExtensionSettings)
                    {
                        if (!jsonCustomModels.ContainsKey(k.Key))
                        {

                            Dictionary<String, Object> jsonItem = new Dictionary<String, Object>();
                            jsonItem.Add("Name", k.Key);
                            jsonItem.Add("Value", k.Value);
                            jsonItem.Add("Index", jsonCustomModels.Count);

                            jsonCustomModels.Add(jsonCustomModels.Count.ToString(), jsonItem);
                        }
                    }
                }
            }

            //提取预设值的自定义模型
            String CustomModels = ViewXmlSettingT<String>("CustomModels", "");
            if (!String.IsNullOrEmpty(CustomModels))
            {

                //提取共用属性名
                List<String> ExtensionKeys = Common.GetList(CustomModels, "\r\n"); ;
                foreach (String k in ExtensionKeys)
                {
                    if (!String.IsNullOrEmpty(k) && !jsonCustomModels.ContainsKey(k))
                    {
                        Dictionary<String, Object> jsonItem = new Dictionary<String, Object>();
                        jsonItem.Add("Name", k);
                        jsonItem.Add("Value", "");
                        jsonItem.Add("Index", jsonCustomModels.Count);

                        jsonCustomModels.Add(jsonCustomModels.Count.ToString(), jsonItem);
                    }
                }
            }


            Dictionary<String, Object> jsonNull = new Dictionary<String, Object>();
            jsonNull.Add("Name", "");
            jsonNull.Add("Value", "");
            jsonNull.Add("Index", jsonCustomModels.Count);
            jsonCustomModels.Add(jsonCustomModels.Count.ToString(), jsonNull);




            //转换数据为json
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            Response.Clear();
            Response.Write(jsSerializer.Serialize(jsonCustomModels));
            Response.End();

        }


        #endregion

        #region "媒体库文件下载"

        /// <summary>
        /// 媒体库文件下载
        /// </summary>
        public void FileDownLoads()
        {
            Int32 SliderID = WebHelper.GetIntParam(Request, "SliderID", 0);
            Int32 LayerID = WebHelper.GetIntParam(Request, "LayerID", 0);
            String SettingName = WebHelper.GetStringParam(Request, "SettingName", "");

            if (!String.IsNullOrEmpty(SettingName))
            {

                String SettingValue = String.Empty;

                DNNGo_DNNGalleryProGame_Slider SliderItem = new DNNGo_DNNGalleryProGame_Slider();
                if (LayerID > 0)
                {
                    DNNGo_DNNGalleryProGame_Layer LayerItem = DNNGo_DNNGalleryProGame_Layer.FindByKeyForEdit(LayerID);
                    if (LayerItem != null && LayerItem.ID > 0)
                    {
                        SliderItem = DNNGo_DNNGalleryProGame_Slider.FindByKeyForEdit(SliderID);
                        if (SliderItem != null && SliderItem.ID > 0)
                        {
                            //背景下载技术
                            SliderItem.Clicks += 1;
                            SliderItem.Update();
                        }
                        //层下载技术
                        LayerItem.Clicks += 1;
                        LayerItem.Update();
                        //取出层中的下载链接
                        SettingValue = ViewLayerSettingT<String>(LayerItem, SettingName, "");
                    }
                    else
                    {
                        //没有找到文件记录
                    }
                }
                else if (SliderID > 0)
                {
                    SliderItem = DNNGo_DNNGalleryProGame_Slider.FindByKeyForEdit(SliderID);
                    if (SliderItem != null && SliderItem.ID > 0)
                    {
                        SliderItem.Clicks += 1;
                        SliderItem.Update();
                        //取出背景中的下载链接
                        SettingValue = ViewSliderSettingT<String>(SliderItem, SettingName, "");
                    }
                }

                if (!String.IsNullOrEmpty(SettingValue))
                {
                    String DownLoadUrl = HttpUtility.UrlDecode( ViewLinkUrl(SettingValue,false));
                    if (!String.IsNullOrEmpty(DownLoadUrl))
                    {
                        //记录下载信息到日志表里面***暂时忽略


                        if (SettingValue.IndexOf("MediaID=", StringComparison.CurrentCultureIgnoreCase) == 0)
                        {
                            String ServerPath = MapPath(DownLoadUrl);

                            if (false)
                            {
                                FileSystemUtils.DownloadFile(ServerPath, Path.GetFileName(ServerPath));//下载媒体库中的文件 
                            }
                            else
                            {
                                Response.Redirect(DownLoadUrl);
                            }


                        }
                        else
                        {
                            Response.Redirect(DownLoadUrl);//除了媒体库中的文件，其他一律跳转
                        }
                    }
                    else
                    {
                        //下载链接不存在
                    }

                }
                else
                {
                    //没有取到下载链接
                }


            }
            else
            {
                //没有找到文件参数
            }




        }
        #endregion

        #region "为背景项进行点赞"

        /// <summary>
        /// 点赞
        /// </summary>
        public void ClickHeats()
        {
            Int32 SliderID = WebHelper.GetIntParam(Request, "SliderID", 0);
            Int32 LayerID = WebHelper.GetIntParam(Request, "LayerID", 0);
            Int32 Heat = WebHelper.GetIntParam(Request, "Heat", 1);//热度+1 或者 -1


            Int32 Heats = 0;

            DNNGo_DNNGalleryProGame_Slider SliderItem = new DNNGo_DNNGalleryProGame_Slider();
            if (LayerID > 0)
            {
                DNNGo_DNNGalleryProGame_Layer LayerItem = DNNGo_DNNGalleryProGame_Layer.FindByKeyForEdit(LayerID);
                if (LayerItem != null && LayerItem.ID > 0)
                {
                    SliderItem = DNNGo_DNNGalleryProGame_Slider.FindByKeyForEdit(SliderID);
                    if (SliderItem != null && SliderItem.ID > 0)
                    {
                        //背景下载技术
                        SliderItem.Heats += Heat;
                        SliderItem.Update();

                        
                    }
                    //层下载技术
                    LayerItem.Heats += Heat;
                    LayerItem.Update();

                    Heats = LayerItem.Heats;

                }
                else
                {
                    //没有找到文件记录
                }
            }
            else if (SliderID > 0)
            {
                SliderItem = DNNGo_DNNGalleryProGame_Slider.FindByKeyForEdit(SliderID);
                if (SliderItem != null && SliderItem.ID > 0)
                {
                    SliderItem.Heats += Heat;
                    SliderItem.Update();

                    Heats = SliderItem.Heats;
                }
            }


            Response.Clear();
            Response.Write(Heats);
            Response.End();

        }


        #endregion

    }
}