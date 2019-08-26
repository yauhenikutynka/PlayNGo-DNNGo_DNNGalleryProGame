using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using DotNetNuke.Common;
using DotNetNuke.Services.Localization;
using System.Web.UI.WebControls;
using System.Collections;

using DotNetNuke.Entities.Modules;
using System.IO;
using System.Web.UI;
using System.Threading;
using DotNetNuke.Common.Utilities;
using System.Globalization;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Entities.Users;

namespace DNNGo.Modules.DNNGalleryProGame
{
    public class TemplateFormat
    {


        #region "属性"
        /// <summary>
        /// 模块基类
        /// </summary>
        private BaseModule bpm = new BaseModule();

     

        private Button _ViewButton;
        /// <summary>
        /// 触发按钮
        /// </summary>
        public Button ViewButton
        {
            get { return _ViewButton; }
            set { _ViewButton = value; }
        }



        private String _ThemeXmlName = String.Empty;
        /// <summary>
        /// 主题XML名称
        /// </summary>
        public String ThemeXmlName
        {
            get { return _ThemeXmlName; }
            set { _ThemeXmlName = value; }
        }

        private PlaceHolder _PhContent = new PlaceHolder();

        public PlaceHolder PhContent
        {
            get { return _PhContent; }
            set { _PhContent = value; }
        }

        /// <summary>
        /// 效果文件夹路径
        /// </summary>
        public String EffectPath
        {
            get { return String.Format("{0}Effects/{1}/", bpm.ModulePath, bpm.Settings_EffectName); }
        }

 

        #endregion



        #region "方法"




        #region "--关于内容与标题--"

        /// <summary>
        /// 显示标题(通过资源文件)
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="DefaultValue">资源文件未定义时默认值</param>
        /// <returns>返回值</returns>
        public String ViewTitle(String Title, String DefaultValue)
        {
            return ViewResourceText(Title, DefaultValue);
        }

        /// <summary>
        /// 显示内容
        /// </summary>
        public String ViewContent(String FieldName, DNNGo_DNNGalleryProGame_Slider DataItem)
        {
            if (DataItem != null && DataItem.ID > 0)
            {
                if ( DataItem[FieldName] != null)
                {
                    return Convert.ToString(DataItem[FieldName]);//找出一般属性
                }
            }
            return string.Empty;
        }

      

        /// <summary>
        /// 显示内容并截取数据
        /// </summary>
        /// <param name="FieldName">字段名</param>
        /// <param name="DataItem">数据项</param>
        /// <param name="Lenght">显示长度</param>
        /// <returns></returns>
        public String ViewContent(String FieldName, DNNGo_DNNGalleryProGame_Slider DataItem, Int32 Lenght)
        {
            return ViewContent(FieldName, DataItem, Lenght, "...");
        }


        /// <summary>
        /// 显示内容并截取数据
        /// </summary>
        /// <param name="FieldName">字段名</param>
        /// <param name="DataItem">数据项</param>
        /// <param name="Lenght">显示长度</param>
        /// <param name="Suffix">终止符号</param>
        /// <returns></returns>
        public String ViewContent(String FieldName, DNNGo_DNNGalleryProGame_Slider DataItem, Int32 Lenght, String Suffix)
        {
            String Content = ViewContent(FieldName, DataItem);//先取内容
            return WebHelper.leftx(Content, Lenght, Suffix);
        }

        /// <summary>
        ///  显示时间
        /// </summary>
        /// <param name="FieldName">字段名</param>
        /// <param name="DataItem">数据项</param>
        /// <param name="TimeFormat">时间格式</param>
        /// <returns></returns>
        public String ViewDateTime(String FieldName, DNNGo_DNNGalleryProGame_Slider DataItem, String TimeFormat)
        { 
            String Content = ViewContent(FieldName, DataItem);//先取内容
            DateTime Temp = xUserTime.LocalTime();
            if (DateTime.TryParse(Content, out Temp))
                return Temp.ToString(TimeFormat);
            else
                return String.Empty;

        }





        /// <summary>
        /// 显示扩展信息列表
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public List<KeyValueEntity> ViewExtensionList(DNNGo_DNNGalleryProGame_Slider DataItem)
        {
            List<KeyValueEntity> ExtensionSettings = new List<KeyValueEntity>();
            if (DataItem != null && DataItem.ID > 0 && !String.IsNullOrEmpty(DataItem.Extension))
            {
                //取出扩展项
                ExtensionSettings = ConvertTo.Deserialize<List<KeyValueEntity>>(DataItem.Extension);
            }
            return ExtensionSettings;
        }

 


 

        /// <summary>
        /// 显示URL控件存放的值
        /// </summary>
        /// <param name="UrlValue"></param>
        /// <returns></returns>
        public String ViewLinkUrl(String UrlValue,String DefaultValue, int PortalId)
        {
            if (!String.IsNullOrEmpty(UrlValue) && UrlValue != "0")
            {
                if (UrlValue.IndexOf("FileID=", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    int FileID = 0;
                    if (int.TryParse(UrlValue.Replace("FileID=", ""), out FileID) && FileID > 0)
                    {
                 
                        var fi = FileManager.Instance.GetFile(FileID);
                        if (fi != null && fi.FileId > 0)
                        {
                            DefaultValue = string.Format("{0}{1}{2}", bpm.DNNGalleryProGame_PortalSettings.HomeDirectory, fi.Folder, bpm.Server.UrlPathEncode(fi.FileName));
                        }
                    }
                }
                else if (UrlValue.IndexOf("MediaID=", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    

                    int MediaID = 0;
                    if (int.TryParse(UrlValue.Replace("MediaID=", ""), out MediaID) && MediaID > 0)
                    {
                        DNNGo_DNNGalleryProGame_Files Multimedia = DNNGo_DNNGalleryProGame_Files.FindByID(MediaID);
                        if (Multimedia != null && Multimedia.ID > 0)
                        {
                            DefaultValue = bpm.Server.UrlPathEncode(bpm.GetPhotoPath( Multimedia.FilePath));// String.Format("{0}{1}", bpm.DNNGalleryProGame_PortalSettings.HomeDirectory, Multimedia.FilePath);
                        }

  
                    }
                }
                else if (UrlValue.IndexOf("TabID=", StringComparison.CurrentCultureIgnoreCase) == 0)
                {

                    DefaultValue = Globals.NavigateURL(Convert.ToInt32(UrlValue.Replace("TabID=", "")), false, bpm.DNNGalleryProGame_PortalSettings, Null.NullString, "", "");

                }
                else
                {
                    DefaultValue = UrlValue;
                }
            }
            return bpm.FullPortalUrl( DefaultValue);
        
        }


        public String ViewLinkUrl(String UrlValue, String DefaultValue)
        {
            return ViewLinkUrl(UrlValue,DefaultValue, bpm.Settings_PortalID);
        }


        public String ViewLinkUrl(String UrlValue)
        {
            String DefaultValue = String.Empty;
            if (UrlValue.IndexOf("MediaID=", StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                DefaultValue = String.Format("{0}Resource/images/no_image.png", bpm.ModulePath);
            }
            return ViewLinkUrl(UrlValue, DefaultValue, bpm.Settings_PortalID);
        }


        /// <summary>
        /// 查找所有相关的层
        /// </summary>
        /// <param name="DataItem"></param>
        /// <param name="Layers"></param>
        /// <returns></returns>
        public List<DNNGo_DNNGalleryProGame_Layer> FindLayers(DNNGo_DNNGalleryProGame_Slider DataItem, List<DNNGo_DNNGalleryProGame_Layer> Layers)
        {
            return Layers.FindAll(r => r.SliderID == DataItem.ID);
        }


        

       
        #endregion

        #region "--关于链接跳转--"

        /// <summary>
        /// 返回到列表
        /// </summary>
        /// <returns></returns>
        public String GoUrl()
        {
            return Globals.NavigateURL(bpm.Settings_TabID);
        }


        public String GoUrl(Int32 SliderID)
        {
            return FullPortalUrl(Globals.NavigateURL(bpm.Settings_TabID, bpm.PortalSettings, "", String.Format("SliderID{0}={1}", bpm.Settings_ModuleID, SliderID)));
        }


        public String GoUrl(DNNGo_DNNGalleryProGame_Slider SliderItem)
        {
            if (!String.IsNullOrEmpty(SliderItem.FriendlyUrl))
            {
                return GoFriendlyUrl(SliderItem);
            }
            return GoUrl(SliderItem.ID);
        }


        public String GoFriendlyUrl(DNNGo_DNNGalleryProGame_Slider SliderItem)
        {
            if (!String.IsNullOrEmpty(SliderItem.FriendlyUrl))
            {
                var PortalSettings = bpm.PortalSettings;

               String FriendlyUrls = String.Empty;

                String ArticleTitle = SliderItem.FriendlyUrl;
 
                if (String.IsNullOrEmpty(ArticleTitle) && !String.IsNullOrEmpty(SliderItem.Title))
                {
                    ArticleTitle = Common.FriendlySlug(SliderItem.Title);
                }


                if (PortalSettings != null && PortalSettings.DefaultLanguage != Common.GetCurrentCulture())
                    FriendlyUrls = Globals.ApplicationURL(bpm.Settings_TabID) + "&language=" + Common.GetCurrentCulture();
                else
                    FriendlyUrls = Globals.ApplicationURL(bpm.Settings_TabID);

                return Globals.FriendlyUrl(bpm.Settings_TabInfo, FriendlyUrls, String.Format("{0}.aspx", ArticleTitle), PortalSettings);
            }
            return GoUrl(SliderItem.ID);
        }


        /// <summary>
        /// 跳转到分类筛选链接
        /// </summary>
        /// <param name="GroupItem"></param>
        /// <returns></returns>
        public String GoUrl(DNNGo_DNNGalleryProGame_Group GroupItem)
        {
            String AttributeUrl = String.Empty;
            if (bpm.Attribute1 >= 0)
            {
                AttributeUrl = String.Format("{0}_{1}={2}", "Attribute1", bpm.Settings_ModuleID, bpm.Attribute1);
            }
            else if (!String.IsNullOrEmpty(bpm.Attribute2))
            {
                AttributeUrl = String.Format("{0}_{1}={2}", "Attribute2", bpm.Settings_ModuleID, bpm.Attribute2);
            }
 
            return Globals.NavigateURL(bpm.Settings_TabID, "", String.Format("GroupID{0}={1}", bpm.Settings_ModuleID, GroupItem.ID), AttributeUrl);
        }

        public String GoAttributeUrl(String Name, Object oValue)
        {
            String GroupUrl = bpm.GroupID > 0 ? String.Format("GroupID{0}={1}", bpm.Settings_ModuleID, bpm.GroupID):"";
            return Globals.NavigateURL(bpm.Settings_TabID, "", String.Format("{0}_{1}={2}", Name, bpm.Settings_ModuleID, HttpUtility.UrlEncode( oValue.ToString())), GroupUrl);
        }

        /// <summary>
        /// 排序链接
        /// </summary>
        /// <param name="SortName">排序名称</param>
        /// <param name="SortType">排序类型</param>
        /// <returns></returns>
        public String GoSortUrl(String SortName,Int32 SortType)
        {
            String GroupUrl = bpm.GroupID > 0 ? String.Format("GroupID{0}={1}", bpm.Settings_ModuleID, bpm.GroupID) : "";
            String SortNameUrl = !String.IsNullOrEmpty(SortName) ? String.Format("Sort{0}={1}", bpm.Settings_ModuleID, SortName) : "";
            String SortTypeUrl = SortType !=0 ? String.Format("SortType{0}={1}", bpm.Settings_ModuleID, SortType) : "";
            return Globals.NavigateURL(bpm.Settings_TabID, "", SortNameUrl, SortTypeUrl, GroupUrl);
        }

 

        /// <summary>
        /// 跳转到登录页面
        /// </summary>
        /// <returns></returns>
        public String GoLogin()
        {
            return  Globals.NavigateURL(bpm.PortalSettings.LoginTabId, "Login", "returnurl=" +  HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl));
        }


        /// <summary>
        /// 跳转到文章编辑界面
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String GoEditUrl(DNNGo_DNNGalleryProGame_Slider DataItem)
        {
            return FullPortalUrl(bpm.xUrl("ArticleID", Convert.ToString(DataItem.ID), "Article"));//pmb.EditUrl("Token", "Article", "Manager", "ArticleID=" + Convert.ToString(DataItem.ID));
        }



        /// <summary>
        /// 填充为完整的URL
        /// </summary>
        public String GoFullUrl(String goUrl)
        {
            return FullPortalUrl(goUrl);
        }
        /// <summary>
        /// 填充为完整的URL
        /// </summary>
        public String GoFullUrl()
        {
            return PortalUrl;
        }


        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="LayerItem"></param>
        /// <param name="SettingName"></param>
        /// <returns></returns>
        public String GoDownloadUrl(DNNGo_DNNGalleryProGame_Layer LayerItem,String SettingName)
        {
            return String.Format("{0}Resource_Service.aspx?Token=download&PortalID={1}&TabID={2}&ModuleID={3}&SliderID={4}&LayerID={5}&SettingName={6}",
                bpm.ModulePath, 
                bpm.Settings_PortalID, 
                bpm.Settings_TabID, 
                bpm.Settings_ModuleID, 
                LayerItem.SliderID, 
                LayerItem.ID,
                SettingName);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="SliderItem"></param>
        /// <param name="SettingName"></param>
        /// <returns></returns>
        public String GoDownloadUrl(DNNGo_DNNGalleryProGame_Slider SliderItem, String SettingName)
        {
            return String.Format("{0}Resource_Service.aspx?Token=download&PortalID={1}&TabID={2}&ModuleID={3}&SliderID={4}&SettingName={5}",
                bpm.ModulePath,
                bpm.Settings_PortalID,
                bpm.Settings_TabID,
                bpm.Settings_ModuleID,
                SliderItem.ID,
                SettingName);
        }


        /// <summary>
        /// 热度的链接
        /// </summary>
        /// <param name="LayerItem"></param>
        /// <param name="Heat">热度+1 或者 -1</param>
        /// <returns></returns>
        public String GoHeatsUrl(DNNGo_DNNGalleryProGame_Layer LayerItem, Int32 Heat)
        {
            return String.Format("{0}Resource_Service.aspx?Token=heats&PortalID={1}&TabID={2}&ModuleID={3}&SliderID={4}&LayerID={5}&Heat={6}",
                bpm.ModulePath,
                bpm.Settings_PortalID,
                bpm.Settings_TabID,
                bpm.Settings_ModuleID,
                LayerItem.SliderID,
                LayerItem.ID,
                Heat);
        }

        /// <summary>
        /// 热度的链接
        /// </summary>
        /// <param name="SliderItem"></param>
        /// <param name="Heat">热度+1 或者 -1</param>
        /// <returns></returns>
        public String GoHeatsUrl(DNNGo_DNNGalleryProGame_Slider SliderItem, Int32 Heat)
        {
            return String.Format("{0}Resource_Service.aspx?Token=heats&PortalID={1}&TabID={2}&ModuleID={3}&SliderID={4}&Heat={5}",
                bpm.ModulePath,
                bpm.Settings_PortalID,
                bpm.Settings_TabID,
                bpm.Settings_ModuleID,
                SliderItem.ID,
                Heat);
        }


        private String _PortalUrl = String.Empty;
        /// <summary>
        /// 站点URL (可以在绑定的时候用到)
        /// </summary>
        public String PortalUrl
        {
            get
            {
                if (String.IsNullOrEmpty(_PortalUrl))
                {
                    if (bpm.Settings_PortalID == bpm. PortalId)
                    {
                        _PortalUrl = String.Format("{0}://{1}", bpm.IsSSL ? "https" : "http", WebHelper.GetHomeUrl());
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(bpm.DNNGalleryProGame_PortalSettings.PortalAlias.HTTPAlias))
                        {
                            _PortalUrl = String.Format("{0}://{1}", bpm.IsSSL ? "https" : "http", bpm.DNNGalleryProGame_PortalSettings.PortalAlias.HTTPAlias);
                        }
                    }
                }
                return _PortalUrl;
            }
        }

        /// <summary>
        /// 填充目标的URL
        /// </summary>
        /// <param name="_Url"></param>
        /// <returns></returns>
        public String FullPortalUrl(String _Url)
        {
            if (!String.IsNullOrEmpty(_Url))
            {
                if (_Url.ToLower().IndexOf("http://") < 0 && _Url.ToLower().IndexOf("https://") < 0)
                {
                    _Url = string.Format("{0}{1}", PortalUrl, _Url);
                }
            }
            return _Url;
        }

        #endregion


        #region "--关于模版内容格式化--"




        /// <summary>
        /// 显示随机数
        /// </summary>
        /// <param name="MinMunber">最少数</param>
        /// <param name="MaxNumber">最大数</param>
        /// <returns></returns>
        public Int32 ViewRandom(Int32 MinMunber, Int32 MaxNumber)
        {
            
            Thread.Sleep(10);
            return new Random().Next(MinMunber, MaxNumber);
        }
        /// <summary>
        /// 按索引显示数组类容
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="ArrIndex"></param>
        /// <returns></returns>
        public String ViewArrayList(ArrayList arr, Int32 ArrIndex)
        {
            if (arr != null && arr.Count > 0 && arr.Count > ArrIndex)
            {
                return Convert.ToString( arr[ArrIndex]);
            }
            return String.Empty;
        }

        /// <summary>
        /// 显示效果6的索引
        /// </summary>
        /// <param name="arr">效果集合</param>
        /// <param name="VelocityCount"></param>
        /// <returns></returns>
        public Int32 ViewEffect6VelocityCount(ArrayList arr, Int32 VelocityCount)
        {
            return arr.Count >= VelocityCount ? VelocityCount : VelocityCount % arr.Count;
        }


 

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="DefaultValue"></param>
        /// <param name="oldString"></param>
        /// <param name="newString"></param>
        /// <returns></returns>
        public String Replace(String DefaultValue, String oldString, String newString)
        {
            return DefaultValue.Replace(oldString, newString);
        }

        /// <summary>
        /// 除法运算
        /// </summary>
        /// <param name="FirstNumber">第一个数</param>
        /// <param name="LastNumber">第二个数</param>
        /// <returns></returns>
        public String Division(String FirstNumber, String LastNumber)
        {
            String d = "0";
            if (!String.IsNullOrEmpty(FirstNumber) && !String.IsNullOrEmpty(LastNumber))
            {
                if (FirstNumber.IndexOf(",") >= 0) FirstNumber = FirstNumber.Replace(",", ".");
                if (LastNumber.IndexOf(",") >= 0) LastNumber = LastNumber.Replace(",", ".");

                float f = float.Parse(FirstNumber) / float.Parse(LastNumber);

                if (f > 0f)
                {
                    d = f.ToString();
                    if (!String.IsNullOrEmpty(d) && d.IndexOf(",") >= 0)
                    {
                        d = d.Replace(",", ".");
                    }

                }


            }
            return d;
        }






        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileSize"></param>
        /// <returns></returns>
        public String ConvertFileSize(object oFileSize)
        {
            String FileSizef = "0kb";
            float FileSize = Convert.ToSingle(oFileSize);
            if (FileSize < 1 && FileSize > 0)
            {
                FileSizef = String.Format("{0}kb", Convert.ToInt32(FileSize * 1024));
            }
            else if (FileSize>1)
            {
                FileSizef = String.Format("{0:N}mb", FileSize);
            }
            else if (FileSize > 1024)
            {
                FileSizef = String.Format("{0:N}gb", Convert.ToSingle(FileSize / 1024));
            }
            return FileSizef;
        }

        /// <summary>
        /// 转换时间
        /// </summary>
        /// <param name="StringDateTime"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public String ConvertDateTitme(String StringDateTime,String format)
        {
            String ReturnTime = StringDateTime;
            //发布状态和时间
            DateTime oTime = xUserTime.LocalTime();
            string[] expectedFormats = { "G", "g", "f", "F" };
            if (DateTime.TryParseExact(StringDateTime, "MM/dd/yyyy HH:mm:ss", new CultureInfo("en-US", false), DateTimeStyles.AllowWhiteSpaces, out oTime))
            {
                ReturnTime = oTime.ToString(format, CultureInfo.InvariantCulture);
            }
            return ReturnTime;
        }


        public static String String2Json(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("&quot;"); break;
                    //case '\"':
                    //    sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        if ((c >= 0 && c <= 31) || c == 127)//在ASCⅡ码中，第0～31号及第127号(共33个)是控制字符或通讯专用字符
                        {

                        }
                        else if (c == 34)
                        {
                            sb.Append("&quot;");
                        }
                        else
                        {
 
                            sb.Append(c);
                        }
                        break;
                }
            }
            return sb.ToString();
        }


        public String ViewXmlSettingByTime(String Name, String DefaultValue)
        {
            String o =  ViewXmlSetting(Name, DefaultValue).ToString();
            if (!String.IsNullOrEmpty(o))
            {
                //判断是否需要把日期转换成带时区的
                DateTime oTime = xUserTime.UtcTime();
                string[] expectedFormats = { "G", "g", "f", "F" };
                if (DateTime.TryParseExact(o, "MM/dd/yyyy hh:mm:ss", new CultureInfo("en-US", false), DateTimeStyles.AllowWhiteSpaces, out oTime))
                {
                    o = xUserTime.LocalTime(oTime).ToString("MM/dd/yyyy hh:mm:ss", new CultureInfo("en-US", false));
                }
            }
            return o;
        }

        /// <summary>
        /// 本地时间
        /// </summary>
        /// <returns></returns>
        public DateTime LocalTime()
        {
            return xUserTime.LocalTime();
        }


        #endregion


        #region "关于ajax参数的定义"

        /// <summary>
        /// ajax的基本参数
        /// </summary>
        public String AjaxParameters
        {
            get { return String.Format(" data-moduleid=\"{0}\" data-tabid=\"{1}\" data-portalid=\"{2}\" data-modulepath=\"{3}\" ",bpm.ModuleId,bpm.TabId,bpm.PortalId,bpm.ModulePath); }
        }




        #endregion



        #region "--关于图片--"

        /// <summary>
        /// 显示图片地址
        /// </summary>
        /// <param name="DataItem"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public String PictureUrl(DNNGo_DNNGalleryProGame_Slider DataItem, String FieldName,String DefaultValue)
        {
            String _PictureUrl = Convert.ToString( ViewSliderSetting(DataItem, FieldName, ""));

            return ViewLinkUrl(_PictureUrl, DefaultValue);
        }

        /// <summary>
        /// 显示图片文件的大小 kb/mb
        /// </summary>
        /// <param name="DataItem"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public String PictureSize(DNNGo_DNNGalleryProGame_Slider DataItem, String FieldName)
        {
            String _PictureSize = "0kb";
            String _PictureUrl = Convert.ToString(ViewSliderSetting(DataItem, FieldName, ""));
            if (!String.IsNullOrEmpty(_PictureUrl) &&   _PictureUrl.IndexOf("MediaID=", StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                int MediaID = 0;
                if (int.TryParse(_PictureUrl.Replace("MediaID=", ""), out MediaID) && MediaID > 0)
                {
                    DNNGo_DNNGalleryProGame_Files Multimedia = DNNGo_DNNGalleryProGame_Files.FindByID(MediaID);
                    if (Multimedia != null && Multimedia.ID > 0 && Multimedia.FileSize > 0)
                    {
                        if (Multimedia.FileSize > (1024 * 1024))
                        {
                            _PictureSize = String.Format("{0:N}gb", Convert.ToSingle(Multimedia.FileSize) / (1024 * 1024));
                        }
                        else if (Multimedia.FileSize > 1024)
                        {
                            _PictureSize = String.Format("{0:N}mb", Convert.ToSingle(Multimedia.FileSize) / Convert.ToSingle(1024));
                        }
                        else
                        {
                            _PictureSize = String.Format("{0}kb", Multimedia.FileSize);
                        }

                        
                    }
                }
            }
            return _PictureSize;
        }

        /// <summary>
        /// 显示图片地址
        /// </summary>
        /// <param name="DataItem"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public String PictureUrl(DNNGo_DNNGalleryProGame_Layer DataItem, String FieldName, String DefaultValue)
        {
            String _PictureUrl = Convert.ToString(ViewLayerSetting(DataItem, FieldName, ""));

            return ViewLinkUrl(_PictureUrl, DefaultValue);
        }

        /// <summary>
        /// 显示图片文件的大小 kb/mb
        /// </summary>
        /// <param name="DataItem"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public String PictureSize(DNNGo_DNNGalleryProGame_Layer DataItem, String FieldName)
        {
            String _PictureSize = "0kb";
            String _PictureUrl = Convert.ToString(ViewLayerSetting(DataItem, FieldName, ""));
            if (!String.IsNullOrEmpty(_PictureUrl) && _PictureUrl.IndexOf("MediaID=", StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                int MediaID = 0;
                if (int.TryParse(_PictureUrl.Replace("MediaID=", ""), out MediaID) && MediaID > 0)
                {
                    DNNGo_DNNGalleryProGame_Files Multimedia = DNNGo_DNNGalleryProGame_Files.FindByID(MediaID);
                    if (Multimedia != null && Multimedia.ID > 0 && Multimedia.FileSize > 0)
                    {
                        if (Multimedia.FileSize > (1024 * 1024))
                        {
                            _PictureSize = String.Format("{0:N}gb", Convert.ToSingle(Multimedia.FileSize) / (1024 * 1024));
                        }
                        else if (Multimedia.FileSize > 1024)
                        {
                            _PictureSize = String.Format("{0:N}mb", Convert.ToSingle(Multimedia.FileSize) / Convert.ToSingle(1024));
                        }
                        else
                        {
                            _PictureSize = String.Format("{0}kb", Multimedia.FileSize);
                        }


                    }
                }
            }
            return _PictureSize;
        }

        /// <summary>
        /// 显示相片的缩略图片
        /// </summary>
        /// <param name="DataItem"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public String ThumbnailUrl(DNNGo_DNNGalleryProGame_Slider DataItem, object width, object height, object mode, string phototype = "p")
        {
            return String.Format("{0}Resource_Service.aspx?Token=thumbnail&PortalId={1}&TabId={2}&ID={3}&width={4}&height={5}&mode={6}&type={7}", bpm.ModulePath, bpm.Settings_PortalID, bpm.Settings_TabID, DataItem.ID, width, height, mode, phototype);
        }
        /// <summary>
        /// 显示相片的缩略图片
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public String ThumbnailUrl(DNNGo_DNNGalleryProGame_Slider DataItem)
        {
            return ThumbnailUrl(DataItem, 200, 200, "AUTO");
        }



        public String ViewPictureUrl(DNNGo_DNNGalleryProGame_Slider DataItem)
        {
            String PictureUrl = ViewSliderSettingT<String>(DataItem, "Picture", "");
            if (String.IsNullOrEmpty(PictureUrl))
            {
                PictureUrl = ViewSliderSettingT<String>(DataItem, "Thumbnails", "");
            }
 
            return ViewLinkUrl(PictureUrl,"");
        }


        #endregion




        #region "相关联的SiderList"

        /// <summary>
        /// 获取相关联的SiderList
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public List<DNNGo_DNNGalleryProGame_Slider> GetRelations(DNNGo_DNNGalleryProGame_Slider SliderItem)
        {
            List<DNNGo_DNNGalleryProGame_Slider> DataList = new List<DNNGo_DNNGalleryProGame_Slider>();

            if (SliderItem != null && SliderItem.ID > 0  )
            {
                if(!String.IsNullOrEmpty(SliderItem.Relations))
                {
                    List<String> Relations = Common.GetList(SliderItem.Relations);
                    if (Relations != null && Relations.Count > 0)
                    {
                        foreach (var RelationId in Relations)
                        {
                            DNNGo_DNNGalleryProGame_Slider Slider = DNNGo_DNNGalleryProGame_Slider.FindByKeyForEdit(RelationId);
                            if (Slider != null && Slider.ID > 0)
                            {
                                DataList.Add(Slider);
                            }
                        }
                    }
                }

                //没有数据时需要用相关分类填充
                if(!(DataList!= null && DataList.Count > 0))
                {
                    Int32 RecordCount = 0;
                    QueryParam qp = new QueryParam();
                    qp.Orderfld = DNNGo_DNNGalleryProGame_Slider._.Sort;
                    qp.OrderType = 1;
                    qp.PageSize = ViewXmlSettingT<Int32>("RelatedSize", 10);

                    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.StartTime, xUserTime.UtcTime(), SearchType.LtEqual));
                    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.EndTime, xUserTime.UtcTime(), SearchType.GtEqual));

                    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.ModuleId,bpm.Settings_ModuleID, SearchType.Equal));
                    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.Status, (Int32)EnumStatus.Activated, SearchType.Equal));
                    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.ID,DNNGo_DNNGalleryProGame_Slider_Group. BuilderSliderIDsBySliderID(SliderItem.ID), SearchType.In));
                    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.ID, SliderItem.ID, SearchType.NotIn));

                    //需要根据条件来查找相应的权限
                    if (bpm.UserId > 0)
                    {
                        if (!bpm.UserInfo.IsSuperUser)//超级管理员不限制
                        {
                            qp.WhereSql.Append(" ( ");
                            //公开的
                            qp.WhereSql.Append(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.Per_AllUsers, 0, SearchType.Equal).ToSql());

                            //有角色的
                            if (bpm.UserInfo.Roles != null && bpm.UserInfo.Roles.Length > 0)
                            {
                                qp.WhereSql.Append(" OR ");
                                qp.WhereSql.Append(" ( ");

                                Int32 RoleIndex = 0;
                                foreach (var r in bpm.UserInfo.Roles)
                                {
                                    if (RoleIndex > 0)
                                    {
                                        qp.WhereSql.Append(" OR ");
                                    }

                                    qp.WhereSql.Append(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.Per_Roles, String.Format(",{0},", r), SearchType.Like).ToSql());

                                    RoleIndex++;
                                }
                                qp.WhereSql.Append(" ) ");
                            }
                            qp.WhereSql.Append(" ) ");
                        }
                    }
                    else
                    {
                        qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.Per_AllUsers, 0, SearchType.Equal));
                    }

                    DataList = DNNGo_DNNGalleryProGame_Slider.FindAll(qp, out RecordCount);
                }

            }
            return DataList;
        }





        /// <summary>
        /// 获取相关联的SiderList IDs
        /// </summary>
        /// <param name="DataItem"></param>
        /// <returns></returns>
        public List<Int32> GetRelationIDs(DNNGo_DNNGalleryProGame_Slider SliderItem)
        {
            List<Int32> DataList = new List<Int32>();

            if (SliderItem != null && SliderItem.ID > 0)
            {
                if (!String.IsNullOrEmpty(SliderItem.Relations))
                {
                    List<String> Relations = Common.GetList(SliderItem.Relations);
                    if (Relations != null && Relations.Count > 0)
                    {
                        foreach (var sRelationId in Relations)
                        {
                            int RelationId = 0;
                            if (int.TryParse(sRelationId, out RelationId))
                            {
                                DataList.Add(RelationId);
                            }
                        }
                    }
                }

                ////没有数据时需要用相关分类填充
                //if (!(DataList != null && DataList.Count > 0))
                //{
                //    Int32 RecordCount = 0;
                //    QueryParam qp = new QueryParam();
                //    qp.ReturnFields = "ID,Sort";
                //    qp.Orderfld = DNNGo_DNNGalleryProGame_Slider._.Sort;
                //    qp.OrderType = 1;
                //    qp.PageSize = ViewXmlSettingT<Int32>("RelatedSize", 10);

                //    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.StartTime, xUserTime.UtcTime(), SearchType.LtEqual));
                //    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.EndTime, xUserTime.UtcTime(), SearchType.GtEqual));

                //    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.ModuleId, bpm.Settings_ModuleID, SearchType.Equal));
                //    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.Status, (Int32)EnumStatus.Activated, SearchType.Equal));
                //    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.ID, DNNGo_DNNGalleryProGame_Slider_Group.BuilderSliderIDsBySliderID(SliderItem.ID), SearchType.In));
                //    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.ID, SliderItem.ID, SearchType.NotIn));

                //    //需要根据条件来查找相应的权限
                //    if (bpm.UserId > 0)
                //    {
                //        if (!bpm.UserInfo.IsSuperUser)//超级管理员不限制
                //        {
                //            qp.WhereSql.Append(" ( ");
                //            //公开的
                //            qp.WhereSql.Append(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.Per_AllUsers, 0, SearchType.Equal).ToSql());

                //            //有角色的
                //            if (bpm.UserInfo.Roles != null && bpm.UserInfo.Roles.Length > 0)
                //            {
                //                qp.WhereSql.Append(" OR ");
                //                qp.WhereSql.Append(" ( ");

                //                Int32 RoleIndex = 0;
                //                foreach (var r in bpm.UserInfo.Roles)
                //                {
                //                    if (RoleIndex > 0)
                //                    {
                //                        qp.WhereSql.Append(" OR ");
                //                    }

                //                    qp.WhereSql.Append(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.Per_Roles, String.Format(",{0},", r), SearchType.Like).ToSql());

                //                    RoleIndex++;
                //                }
                //                qp.WhereSql.Append(" ) ");
                //            }
                //            qp.WhereSql.Append(" ) ");
                //        }
                //    }
                //    else
                //    {
                //        qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.Per_AllUsers, 0, SearchType.Equal));
                //    }

                //    var SliderList = DNNGo_DNNGalleryProGame_Slider.FindAll(qp, out RecordCount);
                //    if (SliderList != null && SliderList.Count > 0)
                //    {
                //        foreach (var Slider in SliderList)
                //        {
                //            DataList.Add(Slider.ID);
                //        }
                //    }


                //}

            }
            return DataList;
        }


        #endregion






        #region "--关于用户--"

        /// <summary>
        /// 显示用户信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public String ViewUser(Int32 UserID, String FieldName)
        {
            return ViewUser(UserID, FieldName, String.Empty);
        }

        /// <summary>
        /// 显示用户信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public String ViewUser(Int32 UserID, String FieldName, String DefaultValue)
        {
            UserInfo uInfo = new UserController().GetUser(bpm.PortalId, UserID);
            return ViewUser(uInfo, FieldName, DefaultValue);
        }


        /// <summary>
        /// 显示用户信息
        /// </summary>
        /// <param name="uInfo"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public String ViewUser(UserInfo uInfo, String FieldName)
        {
            return ViewUser(uInfo, FieldName, String.Empty);
        }


        /// <summary>
        /// 显示用户信息
        /// </summary>
        /// <param name="uInfo"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public String ViewUser(UserInfo uInfo, String FieldName, String DefaultValue)
        {
            String FieldValue = DefaultValue;
            if (uInfo != null && uInfo.UserID > 0 && !String.IsNullOrEmpty(FieldName))
            {

                switch (FieldName.ToLower())
                {
                    case "username": FieldValue = uInfo.Username; break;
                    case "email": FieldValue = uInfo.Email; break;
                    case "firstName": FieldValue = uInfo.FirstName; break;
                    case "lastname": FieldValue = uInfo.LastName; break;
                    case "displayname": FieldValue = uInfo.DisplayName; break;
                    default: FieldValue = DefaultValue; break;
                }
            }
            return FieldValue;
        }














        #endregion



        #region "--XML参数读取--"




        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewXmlSetting(String Name, object DefaultValue)
        {
            return bpm.ViewXmlSetting( Name, DefaultValue);
        }

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewXmlSettingT<T>(String Name, T DefaultValue)
        {
            return bpm.ViewXmlSettingT<T>( Name, DefaultValue);
        }


        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <param name="DataItem">数据项</param>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewSliderSetting(DNNGo_DNNGalleryProGame_Slider DataItem, String Name, object DefaultValue)
        {
            return bpm.ViewSliderSetting(DataItem, Name, DefaultValue);
        }


        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewSliderSettingT<T>(DNNGo_DNNGalleryProGame_Slider DataItem, String Name, T DefaultValue)
        {
            return bpm.ViewSliderSettingT<T>(DataItem, Name, DefaultValue);
        }




        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <param name="DataItem">数据项</param>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewLayerSetting(DNNGo_DNNGalleryProGame_Layer DataItem, String Name, object DefaultValue)
        {
            return bpm.ViewLayerSetting(DataItem, Name, DefaultValue);
        }


        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewLayerSettingT<T>(DNNGo_DNNGalleryProGame_Layer DataItem, String Name, T DefaultValue)
        {
            return bpm.ViewLayerSettingT<T>(DataItem, Name, DefaultValue);
        }


        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <param name="DataItem">数据项</param>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewCategorySetting(DNNGo_DNNGalleryProGame_Group DataItem, String Name, object DefaultValue)
        {
            return TemplateFormat.ViewCategorySettingByStatic(DataItem, Name, DefaultValue);
        }

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewCategorySettingT<T>(DNNGo_DNNGalleryProGame_Group DataItem, String Name, object DefaultValue)
        {
            return (T)Convert.ChangeType(ViewCategorySetting(DataItem, Name, DefaultValue), typeof(T));
        }


        /// <summary>
        /// 读取数据项参数
        /// </summary>
        /// <param name="DataItem">数据项</param>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public static object ViewCategorySettingByStatic(DNNGo_DNNGalleryProGame_Group DataItem, String Name, object DefaultValue)
        {
            object o = DefaultValue;
            if (DataItem != null && DataItem.ID > 0 && !String.IsNullOrEmpty(DataItem.Options))
            {
                try
                {
                    List<KeyValueEntity> ItemSettings = ConvertTo.Deserialize<List<KeyValueEntity>>(DataItem.Options);
                    KeyValueEntity KeyValue = ItemSettings.Find(r1 => r1.Key.ToLower() == Name.ToLower());
                    if (KeyValue != null && !String.IsNullOrEmpty(KeyValue.Key))
                    {
                        o = KeyValue.Value;
                    }

                }
                catch
                {

                }
            }
            return o;
        }




        /// <summary>
        /// 将字符串转化为列表,逗号为分隔符
        /// </summary>
        /// <param name="Items"></param>
        /// <returns></returns>
        public List<String> ToList(String Items)
        {
            List<String> list = new List<String>();
            if(!String.IsNullOrEmpty(Items))
            {
                list = Common.GetList(Items);
            }
            return list;
        }

        /// <summary>
        /// 过滤掉空格
        /// </summary>
        /// <param name="_Value"></param>
        /// <returns></returns>
        public String ToTrim(String _Value)
        {
            if (!String.IsNullOrEmpty(_Value))
            {
                return _Value.Replace(" ", "");
            }
            return String.Empty;
        }


        /// <summary>
        /// 过滤掉空格和其他符号
        /// </summary>
        /// <param name="_Value"></param>
        /// <returns></returns>
        public String ToTrim2(String _Value)
        {
            if (!String.IsNullOrEmpty(_Value))
            {
                _Value = _Value.Replace(" ", "");
                _Value = _Value.Replace(".", "");
                _Value = _Value.Replace(",", "");
                _Value = _Value.Replace("#", "");
                _Value = _Value.Replace("?", "");
                _Value = _Value.Replace("&", "");
                _Value = _Value.Replace("+", "");
                _Value = _Value.Replace("/", "");
                return _Value;
            }
            return String.Empty;
        }

        /// <summary>
        /// 加载分组,并取消空格
        /// </summary>
        /// <param name="_Value"></param>
        /// <returns></returns>
        public String ToGroups(DNNGo_DNNGalleryProGame_Slider DataItem)
        {
            StringBuilder sb = new StringBuilder();

            List<DNNGo_DNNGalleryProGame_Group> list = DNNGo_DNNGalleryProGame_Group.FindAllBySliderID(DataItem.ID);
            if (list != null && list.Count > 0)
            {
                foreach (DNNGo_DNNGalleryProGame_Group item in list)
                {
                    if (!String.IsNullOrEmpty(item.Name))
                    {
                        sb.AppendFormat(" {0}",ToTrim2(item.Name));
                    }
                }
            }

            return sb.ToString();
        }

  

        /// <summary>
        /// 是否包含分组
        /// </summary>
        /// <param name="DataItem"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public Boolean IsIncludeGroup(DNNGo_DNNGalleryProGame_Slider DataItem, Int32 GroupID)
        {
            return DataItem.GroupList.Exists(r => r.ID == GroupID);
        }



        #endregion


        #region "--关于模版--"
        /// <summary>
        /// 引用脚本文件
        /// </summary>
        /// <param name="Name">JS名称</param>
        /// <param name="FileName">JS文件(已包含主题路径)</param>
        public void IncludeScript(String Name, String FileName)
        {
            String FullFileName = String.Format("{0}{1}", ThemePath, FileName);
            bpm.BindJavaScriptFile(Name, FullFileName);
        }

        /// <summary>
        /// 引用脚本文件
        /// </summary>
        /// <param name="Name">JS名称</param>
        /// <param name="FileName">JS文件(已包含主题路径)</param>
        public void IncludeResourceScript(String Name, String FileName)
        {
            String FullFileName = String.Format("{0}Resource/{1}", bpm.ModulePath, FileName);
            bpm.BindJavaScriptFile(Name, FullFileName);
        }


 

        private String _ThemePath = String.Empty;
        /// <summary>
        /// 当前模版路径
        /// </summary>
        public String ThemePath
        {
            get {
                if (String.IsNullOrEmpty(_ThemePath))
                {


                    _ThemePath = String.Format("{0}Effects/{1}/", bpm.ModulePath, bpm.Settings_EffectName);

                }
                return _ThemePath;
            }
        }


        #endregion



        #endregion



        #region "构造"

        /// <summary>
        /// 显示资源文件内容
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public String ViewResourceText(String Title, String DefaultValue)
        {
            String _Title = Localization.GetString(String.Format("{0}.Text", Title), bpm.LocalResourceFile);
            if (String.IsNullOrEmpty(_Title))
            {
                _Title = DefaultValue;
            }
            return _Title;
        }


        public TemplateFormat(BaseModule _bpm)
        {
            bpm = _bpm;
        }

        public TemplateFormat()
        {
            
        }

        #endregion

    }
}