using System;
using System.Collections.Generic;
using System.Web;
using DotNetNuke.Common;
using System.IO;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using System.Collections;
using DotNetNuke.Common.Utilities;
using System.Web.Script.Serialization;
using System.Text;

namespace DNNGo.Modules.DNNGalleryProGame
{
    public class ImportExportHelper
    {

        #region "==属性=="
        /// <summary>
        /// 导入时的图片列表
        /// </summary>
        private List<KeyValueEntity> ImportPictureList = new List<KeyValueEntity>();

        /// <summary>
        /// 模块编号
        /// </summary>
        public Int32 ModuleID
        {
            get;
            set;
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        public Int32 UserId
        {
            get;
            set;
        }




        private ModuleInfo _moduleInfo = new ModuleInfo();
        /// <summary>
        /// 模块信息
        /// </summary>
        public ModuleInfo ModuleInfo
        {
            get {
                if (!(_moduleInfo != null && _moduleInfo.ModuleID > 0) && ModuleID >0)
                {
                    ModuleController mc= new ModuleController();
                    _moduleInfo = mc.GetModule(ModuleID);
                }
                return _moduleInfo; }
        }

        private PortalInfo _portalInfo = new PortalInfo();
        /// <summary>
        /// 站点信息
        /// </summary>
        public PortalInfo portalInfo
        {
            get
            {
                if (!(_portalInfo != null && _portalInfo.PortalID > 0) && ModuleID > 0)
                {
                    PortalController pc = new PortalController();
                    _portalInfo = pc.GetPortal(ModuleInfo.PortalID);
                    
                }
                return _portalInfo;
            }
        }
        private PortalSettings _DNNGalleryProGame_PortalSettings = new PortalSettings();
        /// <summary>
        /// 获取站点配置
        /// </summary>
        public PortalSettings DNNGalleryProGame_PortalSettings
        {
            get
            {
                if (!(_DNNGalleryProGame_PortalSettings != null && _DNNGalleryProGame_PortalSettings.PortalId > 0))
                {

                    _DNNGalleryProGame_PortalSettings = new PortalSettings(portalInfo.PortalID);

                        DotNetNuke.Entities.Portals.PortalAliasController pac = new PortalAliasController();
                        ArrayList PortalAlias = pac.GetPortalAliasArrayByPortalID(portalInfo.PortalID);
                        if (PortalAlias != null && PortalAlias.Count > 0)
                        {
                            _DNNGalleryProGame_PortalSettings.PortalAlias = (PortalAliasInfo)PortalAlias[0];
                        }
                        else
                        {

                            _DNNGalleryProGame_PortalSettings.PortalAlias = new PortalAliasInfo();
                            _DNNGalleryProGame_PortalSettings.PortalAlias.PortalID = portalInfo.PortalID;
                        }
                }
                return _DNNGalleryProGame_PortalSettings;
            }
        }



        private Hashtable _DNNGalleryProGame_Settings = new Hashtable();
        /// <summary>
        /// 获取模块配置(可以获取其他模块配置)
        /// </summary>
        public Hashtable DNNGalleryProGame_Settings
        {
            get
            {
                if (!(_DNNGalleryProGame_Settings != null && _DNNGalleryProGame_Settings.Count > 0))
                {
                    _DNNGalleryProGame_Settings = new ModuleController().GetModule(ModuleID).ModuleSettings;
                }
                return _DNNGalleryProGame_Settings;
            }
        }


        private String _ModulePath = String.Empty;
        /// <summary>
        /// 模块路径
        /// </summary>
        public String ModulePath
        {
            get {
                if (String.IsNullOrEmpty(_ModulePath))
                {
                    ModuleController mc = new ModuleController();
                    ModuleInfo mInfo = mc.GetModule(ModuleID);
                    _ModulePath = "~/DesktopModules/DNNGo_DNNGalleryProGame/";
                    if (mInfo != null && mInfo.ModuleID > 0)
                    {
                        bool propertyNotFound = false;
                        _ModulePath = String.Format("~/DesktopModules/{0}/", mInfo. GetProperty("FolderName", "", null, null, DotNetNuke.Services.Tokens.Scope.DefaultSettings, ref propertyNotFound));
                    }
                }


                return _ModulePath;
            }
        }
        


        private List<SettingEntity> _Setting_EffectSettingDB = new List<SettingEntity>();
        /// <summary>
        /// 获取绑定效果设置项
        /// </summary>
        public List<SettingEntity> Setting_EffectSettingDB
        {
            get
            {
                if (!(_Setting_EffectSettingDB != null && _Setting_EffectSettingDB.Count > 0))
                {
                    String EffectSettingDBPath = HttpContext.Current.Server.MapPath(String.Format("{0}Effects/{1}/EffectSetting.xml", ModulePath, Settings_EffectName));
                    if (File.Exists(EffectSettingDBPath))
                    {
                        XmlFormat xf = new XmlFormat(EffectSettingDBPath);
                        _Setting_EffectSettingDB = xf.ToList<SettingEntity>();
                    }
                }
                return _Setting_EffectSettingDB;
            }
        }


        private List<SettingEntity> _Setting_SliderSettingDB = new List<SettingEntity>();
        /// <summary>
        /// 获取绑定数据设置项(非效果)
        /// </summary>
        public List<SettingEntity> Setting_SliderSettingDB
        {
            get
            {
                if (!(_Setting_SliderSettingDB != null && _Setting_SliderSettingDB.Count > 0))
                {
                    String ItemSettingDBPath = HttpContext.Current.Server.MapPath(String.Format("{0}Effects/{1}/SliderSetting.xml", ModulePath, Settings_EffectName));
                    if (File.Exists(ItemSettingDBPath))
                    {
                        XmlFormat xf = new XmlFormat(ItemSettingDBPath);
                        _Setting_SliderSettingDB = xf.ToList<SettingEntity>();
                    }
                }
                return _Setting_SliderSettingDB;
            }
        }

        private List<SettingEntity> _Setting_LayerSettingDB = new List<SettingEntity>();
        /// <summary>
        /// 获取绑定数据设置项(非效果)
        /// </summary>
        public List<SettingEntity> Setting_LayerSettingDB
        {
            get
            {
                if (!(_Setting_LayerSettingDB != null && _Setting_LayerSettingDB.Count > 0))
                {
                    String LayerSettingDBPath = HttpContext.Current.Server.MapPath(String.Format("{0}Effects/{1}/LayerSetting.xml", ModulePath, Settings_EffectName));
                    if (File.Exists(LayerSettingDBPath))
                    {
                        XmlFormat xf = new XmlFormat(LayerSettingDBPath);
                        _Setting_LayerSettingDB = xf.ToList<SettingEntity>();
                    }
                }
                return _Setting_LayerSettingDB;
            }
        }

        /// <summary>
        /// 获取绑定的效果名称
        /// </summary>
        public String Settings_EffectName
        {
            get { return DNNGalleryProGame_Settings["DNNGalleryProGame_EffectName"] != null ? Convert.ToString(DNNGalleryProGame_Settings["DNNGalleryProGame_EffectName"]) : "Effect_01_AnythingSlider"; }
        }


        #endregion 


        #region "==公用方法=="

        /// <summary>
        /// 导出
        /// </summary>
        public String Export()
        {

            String PostContent = String.Empty;

            //查询字段的数据,填充待导出的XML实体
            QueryParam qp = new QueryParam();
            qp.OrderType = 0;
            Int32 RecordCount = 0;
            qp.Where.Add(new SearchParam("ModuleId", ModuleID, SearchType.Equal));
            List<DNNGo_DNNGalleryProGame_Slider> ArticleList = DNNGo_DNNGalleryProGame_Slider.FindAll(qp, out RecordCount);

            if (ArticleList != null && ArticleList.Count > 0)
            {
                List<XmlSliserEntity> xmlContentList = new List<XmlSliserEntity>();
                List<GallerySettingsEntity> xmlSettingList = new List<GallerySettingsEntity>();
                List<GalleryGroupEntity> xmlGroupList = new List<GalleryGroupEntity>();


                //查询出所有的配置项
                List<SettingEntity> EffectSettingDB = Setting_EffectSettingDB;
                if (EffectSettingDB != null && EffectSettingDB.Count > 0)
                {
                    foreach (SettingEntity SettingItem in EffectSettingDB)
                    {
                        String SettingValue = ViewXmlSetting(SettingItem.Name, SettingItem.DefaultValue).ToString();
                        xmlSettingList.Add(new GallerySettingsEntity(EffectSettingsFormat(Settings_EffectName, SettingItem.Name), SettingValue));
                    }

                    foreach (String key in DNNGalleryProGame_Settings.Keys)
                    {
                        if (!xmlSettingList.Exists(r1 => r1.SettingName == key) && key.IndexOf("Gallery") != 0)
                        {
                            xmlSettingList.Add(new GallerySettingsEntity(key, Convert.ToString(DNNGalleryProGame_Settings[key])));
                        }
                    }
                }




                foreach (DNNGo_DNNGalleryProGame_Slider ContentItem in ArticleList)
                {
                    xmlContentList.Add(EntityToXml(ContentItem));
                }

                foreach (DNNGo_DNNGalleryProGame_Group GroupItem in DNNGo_DNNGalleryProGame_Group.FindAllByModuleID(ModuleID))
                {
                    xmlGroupList.Add(new GalleryGroupEntity(GroupItem));
                }

                XmlFormat xf = new XmlFormat(HttpContext.Current.Server.MapPath(String.Format("{0}Resource/xml/SliderEntity.xml", ModulePath)));
                //将字段列表转换成XML的实体
                PostContent = xf.ToXml<XmlSliserEntity>(xmlContentList, xmlSettingList, xmlGroupList);
            }

            return PostContent;
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        public Int32 Import(HttpPostedFile hpfile)
        {
            return Import(hpfile.InputStream);
        }


        public Int32 Import(String XmlContent)
        {
            int r = 0;
            if (!String.IsNullOrEmpty(XmlContent))
            {
                r = Import(new MemoryStream(Encoding.ASCII.GetBytes(XmlContent)));
            }

            return r;
        }



        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="XmlContent"></param>
        /// <returns></returns>
        public Int32 Import(Stream XmlContent)
        {
            //插入成功的数量
            Int32 InsertResult = 0;


            if (XmlContent != null && XmlContent.Length >0)
            {
               
                //将XML转换为实体
                XmlFormat xf = new XmlFormat();
                xf.XmlDoc.Load(XmlContent);
                List<XmlSliserEntity> XmlContentList = xf.ToList<XmlSliserEntity>();
                List<GallerySettingsEntity> XmlSettingList = xf.ToList<GallerySettingsEntity>();
                List<GalleryGroupEntity> XmlGroupList = xf.ToList<GalleryGroupEntity>();


                //插入分组的记录
                foreach (GalleryGroupEntity XmlGroupItem in XmlGroupList)
                {
                    QueryParam qp = new QueryParam();
                    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Group._.ModuleId, ModuleID, SearchType.Equal));
                    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Group._.Name, XmlGroupItem.Name, SearchType.Equal));
                    if (DNNGo_DNNGalleryProGame_Group.FindCount(qp) == 0)
                    {
                        DNNGo_DNNGalleryProGame_Group ContentItem = new DNNGo_DNNGalleryProGame_Group();
                        ContentItem.Name = XmlGroupItem.Name;
                        ContentItem.Description = XmlGroupItem.Description;
                        ContentItem.QuoteCount = Convert.ToInt32(XmlGroupItem.QuoteCount);
                        ContentItem.Sort = Convert.ToInt32(XmlGroupItem.Sort);

                        ContentItem.ModuleId = ModuleID;
                        ContentItem.PortalId = DNNGalleryProGame_PortalSettings.PortalId;
                        ContentItem.Insert();
                    }
                }


                //插入内容的记录
                foreach (XmlSliserEntity XmlContentItem in XmlContentList)
                {
                    DNNGo_DNNGalleryProGame_Slider ContentItem = XmlToEntity(XmlContentItem);
                    ContentItem.ID = ContentItem.Insert();
                    if (ContentItem.ID > 0)
                    {
                        InsertResult++;
                        ImportLayers(ContentItem, Common.XmlDecode(XmlContentItem.Layers));


                        //插入分组
                        DNNGo_DNNGalleryProGame_Slider_Group.InsertItem(ContentItem, XmlContentItem.Groups);
                    }

                }



                //移除掉部分设置
                XmlSettingList.RemoveAll(r => r.SettingName == "DNNGalleryProGame_CopyOfOtherModule");
                XmlSettingList.RemoveAll(r => r.SettingName == "DNNGalleryProGame_CopyOfPortal");
                XmlSettingList.RemoveAll(r => r.SettingName == "DNNGalleryProGame_TabID");
                XmlSettingList.RemoveAll(r => r.SettingName == "DNNGalleryProGame_ModuleID");

                //插入设置的记录
                foreach (GallerySettingsEntity XmlSettingItem in XmlSettingList)
                {
                    UpdateModuleSetting(ModuleID, XmlSettingItem.SettingName, XmlSettingItem.SettingValue);
                }

            }

            return InsertResult;
        }


        #endregion

     


        #region "数据转换XML & Entity"


        /// <summary>
        /// Gallery数据转XML实体
        /// </summary>
        /// <param name="ContentItem"></param>
        /// <returns></returns>
        public XmlSliserEntity EntityToXml(DNNGo_DNNGalleryProGame_Slider ContentItem)
        {
            XmlSliserEntity ContentXml = new XmlSliserEntity();

            ContentXml.Sort = ContentItem.Sort;
            ContentXml.Status = ContentItem.Status;

            //将Options提取出来处理后再还原
            ContentXml.Options =Common.XmlEncode( ConvertOptions(ContentItem.Options, Setting_SliderSettingDB));
            ContentXml.Groups = DNNGo_DNNGalleryProGame_Group.FindGroupsBySliderID(ContentItem.ID);

            ContentXml.Extension = ContentItem.Extension;

            ContentXml.Title = ContentItem.Title;
            ContentXml.FriendlyUrl = ContentItem.FriendlyUrl;
            ContentXml.Relations = ContentItem.Relations;

            //提取所有的Layers信息
            ContentXml.Layers = Common.XmlEncode(ConvertLayers(ContentItem));

            ContentXml.StartTime = ContentItem.StartTime;
            ContentXml.EndTime = ContentItem.EndTime;
            ContentXml.CreateTime = ContentItem.CreateTime;

            return ContentXml;



        }

        /// <summary>
        /// Gallery数据转XML实体
        /// </summary>
        /// <param name="ContentItem"></param>
        /// <returns></returns>
        public XmlLayerEntity EntityToXml(DNNGo_DNNGalleryProGame_Layer ContentItem)
        {
            XmlLayerEntity ContentXml = new XmlLayerEntity();

            ContentXml.Sort = ContentItem.Sort;
            ContentXml.Status = ContentItem.Status;
            ContentXml.CreateTime = ContentItem.CreateTime;




            //将Options提取出来处理后再还原
            ContentXml.Options = ConvertOptions(ContentItem.Options, Setting_LayerSettingDB);

            return ContentXml;



        }



        /// <summary>
        /// 相册XML转数据实体
        /// </summary>
        /// <returns></returns>
        public DNNGo_DNNGalleryProGame_Slider XmlToEntity(XmlSliserEntity ContentXml)
        {
            DNNGo_DNNGalleryProGame_Slider ContentItem = new DNNGo_DNNGalleryProGame_Slider();


            ContentItem.Sort = ContentXml.Sort;
            ContentItem.Status = ContentXml.Status;



            ContentItem.Options = ConvertOptions_XML(Common.XmlDecode(ContentXml.Options),Setting_SliderSettingDB);

            ContentItem.Extension = ContentXml.Extension;
            ContentItem.CreateTime = ContentXml.CreateTime;
            ContentItem.StartTime = ContentXml.StartTime;
            ContentItem.EndTime = ContentXml.EndTime;

            ContentItem.Title = ContentXml.Title;
            ContentItem.FriendlyUrl = ContentXml.FriendlyUrl;
            ContentItem.Relations = ContentXml.Relations;

            ContentItem.ModuleId = ModuleID;
            ContentItem.PortalId = DNNGalleryProGame_PortalSettings.PortalId;

            ContentItem.LastIP = WebHelper.UserHost;
            ContentItem.LastTime = xUserTime.UtcTime();
            ContentItem.LastUser = UserId;
            return ContentItem;
        }



        /// <summary>
        /// 导入项
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="ContentEntity"></param>
        /// <returns></returns>
        public Int32 ImportLayers(DNNGo_DNNGalleryProGame_Slider Content, String ItemList)
        {
            Int32 Result = 0;
            if (Content != null && Content.ID > 0 && !String.IsNullOrEmpty(ItemList))
            {
                //还原出项的列表
                XmlFormat xf = new XmlFormat();
                xf.XmlDoc.LoadXml(Common.XmlDecode(ItemList));

                List<XmlLayerEntity> list = xf.ToList<XmlLayerEntity>();

                foreach (XmlLayerEntity itemEntity in list)
                {
                    DNNGo_DNNGalleryProGame_Layer item = new DNNGo_DNNGalleryProGame_Layer();

                    item.Options = ConvertOptions_XML(Common.XmlDecode(itemEntity.Options), Setting_LayerSettingDB);
                    item.Status = itemEntity.Status;
                    item.Sort = itemEntity.Sort;

                    item.CreateTime = itemEntity.CreateTime;

                    item.ModuleId = Content.ModuleId;
                    item.PortalId = Content.PortalId;
                    item.LastTime = Content.LastTime;
                    item.LastUser = Content.LastUser;
                    item.LastIP = Content.LastIP;
                    item.SliderID = Content.ID;

                    //添加项
                    if (item.Insert() > 0)
                    {
                        Result++;
                    }

                }
            }
            return Result;
        }


        /// <summary>
        /// 相册XML转数据实体
        /// </summary>
        /// <returns></returns>
        public DNNGo_DNNGalleryProGame_Layer XmlToEntity(XmlLayerEntity ContentXml)
        {
            DNNGo_DNNGalleryProGame_Layer ContentItem = new DNNGo_DNNGalleryProGame_Layer();


            ContentItem.Sort = ContentXml.Sort;
            ContentItem.Status = ContentXml.Status;
            ContentItem.CreateTime = ContentXml.CreateTime;


            ContentItem.Options = Common.XmlDecode(ContentXml.Options);


            ContentItem.ModuleId = ModuleID;
            ContentItem.PortalId = DNNGalleryProGame_PortalSettings.PortalId;

            ContentItem.LastIP = WebHelper.UserHost;
            ContentItem.LastTime = xUserTime.UtcTime();
            ContentItem.LastUser = UserId;
            return ContentItem;
        }




        /// <summary>
        /// 转换选项集合
        /// </summary>
        /// <param name="Options"></param>
        /// <returns></returns>
        public String ConvertOptions(String Options, List<SettingEntity> ItemSettings)
        {
            String String_Options = String.Empty;
            if (!String.IsNullOrEmpty(Options))
            {
                //读取当前相关的
                List<KeyValueEntity> ItemOptions = ConvertTo.Deserialize<List<KeyValueEntity>>(Options);

                Dictionary<String, Object> Dicts = new Dictionary<String, Object>();


                foreach (var ItemSetting in ItemSettings)
                {
                    if (ItemOptions.Exists(r => r.Key == ItemSetting.Name))
                    {
                        KeyValueEntity ItemOption = ItemOptions.Find(r => r.Key == ItemSetting.Name);

                        if (ItemSetting.ControlType == EnumControlType.Urls.ToString())
                        {
                            ItemOption.Value = ViewLinkUrl(ItemOption.Value.ToString());
                        }
                        Dicts.Add(ItemOption.Key, ItemOption.Value);
                    }
                }
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                String_Options = jsSerializer.Serialize(Dicts);

            }
            return String_Options;
        }

        /// <summary>
        /// 把JSON的参数处理后转换成XML
        /// </summary>
        /// <param name="JsonOptions"></param>
        /// <returns></returns>
        public String ConvertOptions_XML(String JsonOptions, List<SettingEntity> ItemSettings)
        {
            String String_Options = String.Empty;
            if (!String.IsNullOrEmpty(JsonOptions))
            {
                List<KeyValueEntity> ItemOptions = new List<KeyValueEntity>();

                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

                Dictionary<String, Object> Dicts = jsSerializer.DeserializeObject(JsonOptions) as Dictionary<String, Object>;

                if (Dicts != null && Dicts.Count > 0)
                {
                    foreach (var d in Dicts.Keys)
                    {
                        KeyValueEntity item = new KeyValueEntity();
                        item.Key = d;
                        item.Value = Dicts[d];
                        String ValueUrls = item.Value.ToString();

                        //需要检测该值是否为图片
                        if (!String.IsNullOrEmpty(ValueUrls) && ItemSettings.Exists(r => r.Name == d) )
                        {
                            SettingEntity sEntity = ItemSettings.Find(r => r.Name == d);
                            if (sEntity.ControlType == EnumControlType.Urls.ToString())
                            {
                                string extension = System.IO.Path.GetExtension(ValueUrls);//扩展名 “.aspx”
                                if (NeedExtension(extension))
                                {
                                    item.Value = ImportPicture(ValueUrls);
                                }
                            }
                        }


                        ItemOptions.Add(item);
                    }


                    String_Options = ConvertTo.Serialize<List<KeyValueEntity>>(ItemOptions);
                }

            }
            return String_Options;
        }


        public String ConvertLayers(DNNGo_DNNGalleryProGame_Slider SliderItem)
        {
            String Layers_XML = String.Empty;
            if (SliderItem != null && SliderItem.ID > 0)
            {
                QueryParam qp = new QueryParam();
                int RecordCount = 0;
                qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Layer._.SliderID, SliderItem.ID, SearchType.Equal));
                List<DNNGo_DNNGalleryProGame_Layer> list = DNNGo_DNNGalleryProGame_Layer.FindAll(qp, out RecordCount);
                if (list != null && list.Count > 0)
                {
                    List<XmlLayerEntity> LayerList = new List<XmlLayerEntity>();
                    foreach (var item in list)
                    {
                        LayerList.Add(EntityToXml(item));
                    }
                    XmlFormat xf = new XmlFormat(HttpContext.Current.Server.MapPath(String.Format("{0}Resource/xml/LayerEntity.xml", ModulePath)));
                    Layers_XML = xf.ToXml<XmlLayerEntity>(LayerList);
                }
            }
            return Layers_XML;
        }


        /// <summary>
        /// 转换URL链接
        /// </summary>
        /// <param name="UrlValue"></param>
        /// <returns></returns>
        public  String ViewLinkUrl(String UrlValue)
        {
            String DefaultValue = String.Empty;
            if (!String.IsNullOrEmpty(UrlValue))
            {
                if (UrlValue.IndexOf("FileID=", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    var fi = DotNetNuke.Services.FileSystem.FileManager.Instance.GetFile(Convert.ToInt32(UrlValue.Replace("FileID=", "")));
                    DefaultValue = string.Format("{0}{1}{2}", DNNGalleryProGame_PortalSettings.HomeDirectory, fi.Folder,HttpContext.Current. Server.UrlPathEncode(fi.FileName));
                    //DefaultValue = string.Format("{0}{1}{2}", bpm.PortalSettings.HomeDirectory, fi.Folder, fi.FileName);
                }
                else if (UrlValue.IndexOf("TabID=", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    DefaultValue = Globals.NavigateURL(Convert.ToInt32(UrlValue.Replace("TabID=", "")), false, DNNGalleryProGame_PortalSettings, Null.NullString, "", "");
                }
                else if (UrlValue.IndexOf("MediaID=", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    int MediaID = 0;
                    if (int.TryParse(UrlValue.Replace("MediaID=", ""), out MediaID) && MediaID > 0)
                    {
                        DNNGo_DNNGalleryProGame_Files Multimedia = DNNGo_DNNGalleryProGame_Files.FindByID(MediaID);
                        if (Multimedia != null && Multimedia.ID > 0)
                        {
                            DefaultValue = HttpContext.Current.Server.UrlPathEncode(String.Format("{0}{1}", DNNGalleryProGame_PortalSettings.HomeDirectory, Multimedia.FilePath));// String.Format("{0}{1}", bpm.DNNGalleryProGame_PortalSettings.HomeDirectory, Multimedia.FilePath);
                        }

                        if (!String.IsNullOrEmpty(DefaultValue))
                        {
                            if (DefaultValue.ToLower().IndexOf("http://") < 0)
                            {
                                DefaultValue = string.Format("http://{0}{1}", WebHelper.GetHomeUrl(), DefaultValue);
                            }

                        }
                    }
                }
                else
                {
                    DefaultValue = UrlValue;
                }

              


            }
            return DefaultValue;

        }

         

        /// <summary>
        /// 导入图片到媒体库
        /// </summary>
        /// <param name="PictureUrl">图片地址</param>
        /// <returns></returns>
        public String ImportPicture(String PictureUrl)
        {
            String Picture = "";
            if (!String.IsNullOrEmpty(PictureUrl))
            {
                //查看该图片是否已经存在过
                KeyValueEntity PictureTemp = ImportPictureList.Find(r1 => r1.Key == PictureUrl);
                if (PictureTemp != null && !String.IsNullOrEmpty(PictureTemp.Key))
                {
                    Picture = PictureTemp.Value.ToString();
                }
                else
                {

                    DNNGo_DNNGalleryProGame_Files PhotoItem = new DNNGo_DNNGalleryProGame_Files();

                    //将图片的URL转换为相应的文件名，文件后缀等内容
                    PhotoItem.FileName = System.IO.Path.GetFileName(PictureUrl);//文件名 “Default.aspx”

                    if (!String.IsNullOrEmpty(PhotoItem.FileName) && PhotoItem.FileName.IndexOf(".") > 0)
                    {
                        PhotoItem.FileExtension = System.IO.Path.GetExtension(PhotoItem.FileName).Replace(".", "");
                        PhotoItem.Name = PhotoItem.FileName.Replace(System.IO.Path.GetExtension(PictureUrl), "");

                        //判断哪些文件需要被下载
                        if (NeedExtension(PhotoItem.FileExtension))
                        {
                            String FullFile = String.Format("{0}DNNGalleryProGame\\uploads\\{1}\\{2}\\{3}\\{4}", DNNGalleryProGame_PortalSettings.HomeDirectoryMapPath, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, PhotoItem.FileName);
                            FileInfo file = new FileInfo(FullFile);

                            int ExistsCount = 1;
                            //如果有重复的文件，需要改变文件名
                            while (file.Exists)
                            {
                                PhotoItem.FileName = String.Format("{0}_{1}.{2}", PhotoItem.Name, ExistsCount, PhotoItem.FileExtension);
                                FullFile = String.Format("{0}DNNGalleryProGame\\uploads\\{1}\\{2}\\{3}\\{4}", DNNGalleryProGame_PortalSettings.HomeDirectoryMapPath, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, PhotoItem.FileName);
                                file = new FileInfo(FullFile);
                                ExistsCount++;
                            }

                            try
                            {

                                if (!file.Directory.Exists)
                                {
                                    file.Directory.Create();
                                }

                                //下载图片
                                WebClientX web = new WebClientX();
                                web.DownloadFile(new Uri(PictureUrl), FullFile);

                                file = new FileInfo(FullFile);
                                if (file.Exists)
                                {

                                    PhotoItem.ModuleId = ModuleID;
                                    PhotoItem.PortalId = DNNGalleryProGame_PortalSettings.PortalId;
                                    PhotoItem.FileSize = Convert.ToInt32(file.Length / 1024);
                                    PhotoItem.FileMate = FileSystemUtils.GetContentType(Path.GetExtension(PhotoItem.FileName).Replace(".", ""));
                                    PhotoItem.Status = (Int32)EnumFileStatus.Approved;
                                    PhotoItem.FilePath = String.Format("DNNGalleryProGame/uploads/{0}/{1}/{2}/{3}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, PhotoItem.FileName);


                                    try
                                    {
                                        if (("png,gif,jpg,jpeg,bmp").IndexOf(PhotoItem.FileExtension) >= 0)
                                        {
                                            //图片的流
                                            System.Drawing.Image image = System.Drawing.Image.FromFile(file.FullName);
                                            PhotoItem.ImageWidth = image.Width;
                                            PhotoItem.ImageHeight = image.Height;

                                            PhotoItem.Exif = Common.Serialize<EXIFMetaData.Metadata>(new EXIFMetaData().GetEXIFMetaData(image));
                                        }
                                    }
                                    catch
                                    {

                                    }

                                    PhotoItem.LastTime = DateTime.Now;
                                    PhotoItem.LastIP = WebHelper.UserHost;
                                    //PhotoItem.LastUser = DNNGalleryProGame_PortalSettings.UserInfo.UserID;
                                    PhotoItem.ID = PhotoItem.Insert();

                                    //返回图片的路径
                                    Picture = String.Format("MediaID={0}", PhotoItem.ID);
                                    ImportPictureList.Add(new KeyValueEntity(PictureUrl, Picture));
                                }

                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {
                            Picture = PictureUrl;
                        }
                    }
                    else
                    {
                        Picture = PictureUrl;
                    }

                }

            }


            return Picture;
        }



        /// <summary>
        /// 需要的扩展文件(一般是图片文件被下载)
        /// </summary>
        /// <param name="Extension"></param>
        /// <returns></returns>
        private  Boolean NeedExtension(String Extension)
        {
            Boolean Result = false;
            if (!String.IsNullOrEmpty(Extension))
            {
                if (Extension.IndexOf("jpg", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    Result = true;
                }
                else if (Extension.IndexOf("png", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    Result = true;
                }
                else if (Extension.IndexOf("jpeg", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    Result = true;
                }
                else if (Extension.IndexOf("gif", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    Result = true;
                }
                else if (Extension.IndexOf("bmp", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    Result = true;
                }
                else if (Extension.IndexOf("tiff", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    Result = true;
                }
            }
            return Result;
        }




 



        #endregion

        #region "更新模块设置"


        /// <summary>
        /// 更新当前模块的设置
        /// </summary>
        /// <param name="SettingName"></param>
        /// <param name="SettingValue"></param>
        public void UpdateModuleSetting(string SettingName, string SettingValue)
        {
            UpdateModuleSetting(ModuleID, SettingName, SettingValue);
        }


        /// <summary>
        /// 更新模块设置
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <param name="SettingName"></param>
        /// <param name="SettingValue"></param>
        public void UpdateModuleSetting(int ModuleId, string SettingName, string SettingValue)
        {
            ModuleController controller = new ModuleController();

            controller.UpdateModuleSetting(ModuleId, SettingName, SettingValue);
        }

        /// <summary>
        /// 效果参数保存名称格式化
        /// </summary>
        /// <param name="EffectName">效果名</param>
        /// <param name="ThemeName">主题名</param>
        /// <returns></returns>
        public String EffectSettingsFormat(String EffectName, String ThemeName)
        {
            return String.Format("Gallery{0}_{1}", EffectName, ThemeName);
        }

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewXmlSetting(String Name, object DefaultValue)
        {
            String SettingKey = EffectSettingsFormat(Settings_EffectName, Name);
            return DNNGalleryProGame_Settings[SettingKey] != null ? ConvertTo.FormatValue(DNNGalleryProGame_Settings[SettingKey].ToString(), DefaultValue.GetType()) : DefaultValue;
        }

        #endregion

    }
}