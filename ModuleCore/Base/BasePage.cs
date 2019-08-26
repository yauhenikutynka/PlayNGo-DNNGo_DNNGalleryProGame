using System;
using System.Collections.Generic;
using System.Web;
using DotNetNuke.Entities.Users;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Host;

using DotNetNuke.Entities.Tabs;
using System.IO;
using DotNetNuke.Services.Localization;
using System.Collections;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Common;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Security;
using DotNetNuke.Entities.Controllers;
using System.Web.UI.WebControls;

namespace DNNGo.Modules.DNNGalleryProGame
{
    public class BasePage : DotNetNuke.Framework.PageBase
    {


        #region "获取DNN对象"

        /// <summary>
        /// 模块编号
        /// </summary>
        public Int32 ModuleId = WebHelper.GetIntParam(HttpContext.Current.Request, "ModuleId", 0);

        public Int32 PortalId = WebHelper.GetIntParam(HttpContext.Current.Request, "PortalId", 0);
        public Int32 TabId = WebHelper.GetIntParam(HttpContext.Current.Request, "TabId", 0);


        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo UserInfo
        {
            get { return PortalSettings.UserInfo; }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId
        {
            get
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    return UserInfo.UserID;
                }
                else
                {
                    return Null.NullInteger;
                }
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
                    _tabInfo = tc.GetTab(TabId,PortalId);

                }

                return _tabInfo;


            }
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
                    _ModuleConfiguration = mc.GetModule(ModuleId,TabId);

                }
                return _ModuleConfiguration;
            }
        }

        private String _BaseModuleName = String.Empty;
        /// <summary>
        /// 基础模块名
        /// </summary>
        public String BaseModuleName
        {
            get
            {
                if (String.IsNullOrEmpty(_BaseModuleName))
                {
                    _BaseModuleName = ModuleProperty("ModuleName");
                }
                return _BaseModuleName;
            }
            set { _BaseModuleName = value; }
        }

        /// <summary>
        /// 模块地址
        /// </summary>
        public string ModulePath
        {
            get { return this.TemplateSourceDirectory + "/"; }
        }


        public String QueryString
        {
            get { return String.Format("{0}&ModulePath={1}", WebHelper.GetScriptNameQueryString, HttpUtility.UrlEncode(ModulePath)); }
        }

        private Hashtable _settings = new Hashtable();
        /// <summary>
        /// 模块设置
        /// </summary>
        public Hashtable Settings
        {
            get
            {
                return ModuleConfiguration.ModuleSettings;
            }
        }


        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public Boolean IsAdministrator
        {
            get { return UserInfo.IsSuperUser || UserInfo.IsInRole("Administrators"); }
        }

        /// <summary>
        /// 管理员锁
        /// (检索目录下有无admindisplay.lock)
        /// </summary>
        public Boolean AdministratorLock
        {
            get { return File.Exists(MapPath(String.Format("{0}admindisplay.lock", ModulePath))); }
        }
        /// <summary>
        /// 显示管理员选项
        /// </summary>
        public Boolean DisplayAdminOption
        {
            get
            {
                Boolean display = true;
                if (AdministratorLock && !IsAdministrator)
                {
                    display = false;
                }
                return display;
            }
        }
 


     
        /// <summary>
        /// 语言
        /// </summary>
        public String language
        {
            get { return WebHelper.GetStringParam(Request, "language", PortalSettings.DefaultLanguage); }
        }


        /// <summary>
        /// 是否具有该模块的编辑权限
        /// </summary>
        public Boolean IsEdit
        {
            get { return UserId > 0 && (IsAdministrator || DotNetNuke.Security.Permissions.ModulePermissionController.HasModuleAccess(DotNetNuke.Security.SecurityAccessLevel.Edit, "CONTENT", ModuleConfiguration)); }
            //get { return UserId > 0 && (IsAdministrator ||  PortalSecurity.HasEditPermissions(ModuleId,TabId)); }
        }

        /// <summary>
        /// 验证登陆状态(没有登陆跳转到登陆页面)
        /// </summary>
        public void VerificationLogin()
        {
            //没有登陆的用户
            if (!(UserId > 0))
            {
                Response.Redirect(Globals.NavigateURL(PortalSettings.LoginTabId, "Login", "returnurl=" + HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl)));

            }
        }

        /// <summary>
        /// 验证作者状态(不是作者跳转到登陆页面)
        /// </summary>
        public void VerificationAuthor()
        {
            //没有登陆的用户
            if (!(UserId > 0))
            {
                Response.Redirect(Globals.NavigateURL(PortalSettings.LoginTabId, "Login", "returnurl=" + HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl)));
            }
            else if (!IsEdit)
            {
                Response.Redirect(Globals.NavigateURL(TabId));
            }



          
        }



        private string _CrmVersion = String.Empty;
        /// <summary>
        /// 引用文件版本
        /// </summary>
        public string CrmVersion
        {
            get
            {
                if (string.IsNullOrEmpty(_CrmVersion))
                {
                    var ModuleVersion = ModuleProperty("Version");
                    string setting = GetHostSetting("CrmVersion");
                    if (!string.IsNullOrEmpty(setting))
                    {
                        _CrmVersion = String.Format("{0}.{1}", ModuleVersion, setting);
                    }
                }
                return _CrmVersion;
            }
        }

        private string GetHostSetting(string key, string defaultValue ="")
        {
            return HostController.Instance.GetString(key, defaultValue); ;
        }



        #endregion

        #region "模块设置与绑定"
        /// <summary>
        /// 设置绑定的模块编号
        /// </summary>
        public Int32 Settings_ModuleID
        {
            get { return Settings_CopyOfOtherModule ? (Settings["DNNGalleryProGame_ModuleID"] != null ? Convert.ToInt32(Settings["DNNGalleryProGame_ModuleID"]) : ModuleId) : ModuleId; }
        }


        /// <summary>
        /// 设置绑定的页面编号
        /// </summary>
        public Int32 Settings_TabID
        {
            get { return Settings_CopyOfOtherModule ? (Settings["DNNGalleryProGame_TabID"] != null ? Convert.ToInt32(Settings["DNNGalleryProGame_TabID"]) : TabId) : TabId; }
        }

        /// <summary>
        /// 设置绑定的站点编号
        /// </summary>
        public Int32 Settings_PortalID
        {
            get { return Settings_CopyOfOtherModule ? (Settings["DNNGalleryProGame_CopyOfPortal"] != null ? Convert.ToInt32(Settings["DNNGalleryProGame_CopyOfPortal"]) : PortalId) : PortalId; }
        }


        /// <summary>
        /// 排序方式
        /// </summary>
        public Int32 Settings_Sortby
        {
            get { return Settings["DNNGalleryProGame_Sortby"] != null ? Convert.ToInt32(Settings["DNNGalleryProGame_Sortby"]) : 0; }
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
                    if (Settings_CopyOfOtherModule && Settings_ModuleID != ModuleId)
                    {
                        _DNNGalleryProGame_PortalSettings = new PortalSettings(Settings_PortalID);

                        DotNetNuke.Entities.Portals.PortalAliasController pac = new PortalAliasController();
                        ArrayList PortalAlias = pac.GetPortalAliasArrayByPortalID(Settings_PortalID);
                        if (PortalAlias != null && PortalAlias.Count > 0)
                        {
                            _DNNGalleryProGame_PortalSettings.PortalAlias = (PortalAliasInfo)PortalAlias[0];
                        }
                        else
                        {

                            _DNNGalleryProGame_PortalSettings.PortalAlias = new PortalAliasInfo();
                            _DNNGalleryProGame_PortalSettings.PortalAlias.PortalID = Settings_PortalID;
                        }


                    }
                    else
                    {
                        _DNNGalleryProGame_PortalSettings = PortalController.GetCurrentPortalSettings();
                    }
                }
                return _DNNGalleryProGame_PortalSettings;
            }
        }


        /// <summary>
        /// 数据是否来自于其他模块
        /// </summary>
        public Boolean Settings_CopyOfOtherModule
        {
            get { return Settings["DNNGalleryProGame_CopyOfOtherModule"] != null ? Convert.ToBoolean(Settings["DNNGalleryProGame_CopyOfOtherModule"]) : false; }
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
                    if (Settings_CopyOfOtherModule && Settings_ModuleID != ModuleId)
                    {
                        ModuleInfo m = ModuleController.Instance.GetModule(Settings_ModuleID, Settings_TabID,true);
                        _DNNGalleryProGame_Settings = m.ModuleSettings;
                    }
                    else
                    {
                        _DNNGalleryProGame_Settings = Settings;
                    }
                }
                return _DNNGalleryProGame_Settings;
            }
        }

     

        #endregion

        #region "新的后台URL"

        /// <summary>
        /// URL转换默认名
        /// </summary>
        /// <returns></returns>
        public String xUrlToken()
        {
            return "ManagerList";
        }


        public string xUrl()
        {
            return xUrl("", "", xUrlToken());
        }
        public string xUrl(string ControlKey)
        {
            return xUrl("", "", ControlKey);
        }
        public string xUrl(string KeyName, string KeyValue)
        {
            return xUrl(KeyName, KeyValue, xUrlToken());
        }
        public string xUrl(string KeyName, string KeyValue, string ControlKey)
        {
            string[] parameters = new string[] { };
            return xUrl(KeyName, KeyValue, ControlKey, parameters);
        }
        public string xUrl(string KeyName, string KeyValue, string ControlKey, params string[] AdditionalParameters)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            String language = WebHelper.GetStringParam(Request, "language", PortalSettings.DefaultLanguage);

            sb.AppendFormat("{0}Index_Manager.aspx?PortalId={1}&TabId={2}&ModuleId={3}&language={4}", ModulePath, PortalId, TabId, ModuleId, language);

            string key = ControlKey;
            if (string.IsNullOrEmpty(key))
            {
                sb.AppendFormat("&Token={0}", xUrlToken());
            }
            else
            {
                sb.AppendFormat("&Token={0}", key);
            }
            if (!string.IsNullOrEmpty(KeyName) && !string.IsNullOrEmpty(KeyValue))
            {
                sb.AppendFormat("&{0}={1}", KeyName, KeyValue);
            }

            if (AdditionalParameters != null && AdditionalParameters.Length > 0)
            {
                foreach (String parameter in AdditionalParameters)
                {
                    sb.AppendFormat("&{0}", parameter);
                }
            }
            return sb.ToString();

        }


        /// <summary>
        /// 是否开启SSL
        /// </summary>
        public Boolean IsSSL
        {
            get { return TabInfo.IsSecure || WebHelper.GetSSL; }
        }

        /// <summary>
        /// 当前页面地址
        /// </summary>
        public String CurrentUrl
        {
            get { return String.Format("{2}://{0}{1}", WebHelper.GetScriptName, WebHelper.GetScriptUrl, IsSSL ? "https" : "http"); }
        }


        #endregion


        #region "基础配置属性"




        /// <summary>
        /// 获取绑定的效果名称
        /// </summary>
        public String Settings_EffectName
        {
            get { return Settings["DNNGalleryProGame_EffectName"] != null ? Convert.ToString(Settings["DNNGalleryProGame_EffectName"]) : GetDefaultEffectName(); }
        }


        /// <summary>
        /// 获取绑定的效果主题名称
        /// </summary>
        public String Settings_EffectThemeName
        {
            get { return Settings["DNNGalleryProGame_EffectThemeName"] != null ? Convert.ToString(Settings["DNNGalleryProGame_EffectThemeName"]) : GetDefaultThemeName(); }
        }

        public Boolean designMode
        {
            get { return DesignMode; }
        }



        private EffectDBEntity _Setting_EffectDB = new EffectDBEntity();
        /// <summary>
        /// 获取绑定效果内容
        /// </summary>
        public EffectDBEntity Setting_EffectDB
        {
            get
            {
                if (!(_Setting_EffectDB != null && !String.IsNullOrEmpty(_Setting_EffectDB.Name)))
                {
                    String EffectDBPath = Server.MapPath(String.Format("{0}Effects/{1}/EffectDB.xml", ModulePath, Settings_EffectName));
                    if (File.Exists(EffectDBPath))
                    {
                        XmlFormat xf = new XmlFormat(EffectDBPath);
                        _Setting_EffectDB = xf.ToItem<EffectDBEntity>();
                    }
                }
                return _Setting_EffectDB;
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
                    String EffectSettingDBPath = Server.MapPath(String.Format("{0}Effects/{1}/EffectSetting.xml", ModulePath, Settings_EffectName));
                    if (File.Exists(EffectSettingDBPath))
                    {
                        XmlFormat xf = new XmlFormat(EffectSettingDBPath);
                        _Setting_EffectSettingDB = xf.ToList<SettingEntity>();
                    }
                }
                return _Setting_EffectSettingDB;
            }
        }

        //public List<SettingEntity> GetSetting_EffectSettingDB(String )




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
                    String ItemSettingDBPath = Server.MapPath(String.Format("{0}Effects/{1}/SliderSetting.xml", ModulePath, Settings_EffectName));
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
                    String ItemSettingDBPath = Server.MapPath(String.Format("{0}Effects/{1}/LayerSetting.xml", ModulePath, Settings_EffectName));
                    if (File.Exists(ItemSettingDBPath))
                    {
                        XmlFormat xf = new XmlFormat(ItemSettingDBPath);
                        _Setting_LayerSettingDB = xf.ToList<SettingEntity>();
                    }
                }
                return _Setting_LayerSettingDB;
            }
        }



        #endregion

        #region "设置默认的效果与主题"

        /// <summary>
        /// 获取默认的效果名
        /// </summary>
        /// <returns></returns>
        public String GetDefaultEffectName()
        {
            //构造效果存放路径
            String EffectDirPath = String.Format("{0}Effects/", Server.MapPath(ModulePath));
            DirectoryInfo EffectDir = new DirectoryInfo(EffectDirPath);
            if (!EffectDir.Exists) EffectDir.Create();//不存在就创建
            //获取当前所有的目录
            DirectoryInfo[] EffectDirs = EffectDir.GetDirectories();

            if (EffectDirs != null && EffectDirs.Length > 0)
            {
                UpdateModuleSetting("DNNGalleryProGame_EffectName", EffectDirs[0].Name);
                return EffectDirs[0].Name;
            }

            return String.Empty;

        }

        /// <summary>
        /// 获取默认的主题名
        /// </summary>
        /// <returns></returns>
        public String GetDefaultThemeName()
        {

            //绑定效果的主题
            String EffectDirPath = String.Format("{0}Effects/{1}/Themes/", Server.MapPath(ModulePath), Settings_EffectName);
            DirectoryInfo EffectDir = new DirectoryInfo(EffectDirPath);
            if (!EffectDir.Exists) EffectDir.Create();
            DirectoryInfo[] ThemeDirs = EffectDir.GetDirectories();
            if (ThemeDirs != null && ThemeDirs.Length > 0)
            {
                UpdateModuleSetting("DNNGalleryProGame_EffectThemeName", ThemeDirs[0].Name);
                return ThemeDirs[0].Name;
            }

            return String.Empty;
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
            UpdateModuleSetting(Settings_ModuleID, SettingName, SettingValue);
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

        #endregion

        #region "获取各种配置 模块/背景/层"

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public object ViewXmlSetting(String Name, object DefaultValue)
        {
            String SettingKey = EffectSettingsFormat(Settings_EffectName, Name);
            return DNNGalleryProGame_Settings[SettingKey] != null ? DNNGalleryProGame_Settings[SettingKey] : DefaultValue;
        }

        /// <summary>
        /// 读取XML参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewXmlSettingT<T>(String Name, object DefaultValue)
        {
            return (T)Convert.ChangeType(ViewXmlSetting(Name, DefaultValue), typeof(T));
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
        /// 读取数据项参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewSliderSettingT<T>(DNNGo_DNNGalleryProGame_Slider DataItem, String Name, T DefaultValue)
        {
            return (T)Convert.ChangeType(ViewSliderSetting(DataItem, Name, DefaultValue), typeof(T));
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
        /// 读取数据项参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="Name">参数名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public T ViewLayerSettingT<T>(DNNGo_DNNGalleryProGame_Layer DataItem, String Name, T DefaultValue)
        {
            return (T)Convert.ChangeType(ViewLayerSetting(DataItem, Name, DefaultValue), typeof(T));
        }

        #endregion

        #region "绑定页面标题和帮助"

        /// <summary>
        /// 显示标题
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public String ViewTitle(String Title, String DefaultValue)
        {
            return ViewTitle(Title, DefaultValue, "");
        }

        /// <summary>
        /// 显示标题
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewTitle(String Title, String DefaultValue, String ControlName)
        {
            return ViewTitle(Title, DefaultValue, ControlName, "");
        }

        /// <summary>
        /// 显示标题
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public String ViewTitle(String Title, String DefaultValue, String ControlName, String ClassName)
        {
            String Content = ViewResourceText(Title, DefaultValue);
            return ViewSpan(Content, ControlName, ClassName);
        }

        /// <summary>
        /// 显示帮助
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public String ViewHelp(String Title, String DefaultValue)
        {
            String Content = ViewResourceText(Title, DefaultValue, "Help");
            return ViewSpan(Content, "", "span_help");
        }

        /// <summary>
        /// 显示内容框
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="ControlName"></param>
        /// <param name="ClassName"></param>
        /// <returns></returns>
        public String ViewSpan(String Content, String ControlName, String ClassName)
        {
            if (!String.IsNullOrEmpty(ControlName))
            {
                System.Web.UI.Control c = FindControl(ControlName);
                if (c != null && !String.IsNullOrEmpty(c.ClientID))
                {
                    ControlName = c.ClientID;
                }
                else
                {
                    ControlName = String.Empty;
                }
            }

            return String.Format("<label  {2}><span {1} >{0}</span></label>",
                Content,
                !String.IsNullOrEmpty(ClassName) ? String.Format("class=\"{0}\"", ClassName) : "",
              !String.IsNullOrEmpty(ControlName) ? String.Format("for=\"{0}\"", ControlName) : ""
                );
        }




        /// <summary>
        /// 显示资源文件内容
        /// </summary>
        /// <param name="Title"></param>
        /// <returns></returns>
        public String ViewResourceText(String Title)
        {
            return ViewResourceText(Title, "");
        }

        /// <summary>
        /// 显示资源文件内容
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public String ViewResourceText(String Title, String DefaultValue)
        {
            return ViewResourceText(Title, DefaultValue, "Text");
        }

        /// <summary>
        /// 显示资源文件内容
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="DefaultValue"></param>
        /// <param name="TextType"></param>
        /// <returns></returns>
        public String ViewResourceText(String Title, String DefaultValue, String TextType)
        {
            String _Title = Localization.GetString(String.Format("{0}.{1}", Title, TextType), this.LocalResourceFile);
            if (String.IsNullOrEmpty(_Title))
            {
                _Title = DefaultValue;
            }
            return _Title;
        }




        /// <summary>
        /// 计算页面执行的时间
        /// </summary>
        /// <param name="TimeStart">开始时间</param>
        public String InitTimeSpan(DateTime TimeStart)
        {
            //查询数据库所花的时间
            System.DateTime endTime =DateTime.Now;
            System.TimeSpan ts = endTime - TimeStart;
            String RunTime = string.Format("{0}秒{1}毫秒", ts.Seconds, ts.Milliseconds);
            TimeStart = endTime = DateTime.Now;
            return RunTime;
        }






        /// <summary>
        /// 显示字段标题
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="ClassName"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewTitleSpan(String Content, String ClassName, String ControlName)
        {

            return String.Format("<label  {2}><span {1} >{0}</span></label>",
                        Content,
                        !String.IsNullOrEmpty(ClassName) ? String.Format("class=\"{0}\"", ClassName) : "",
                      !String.IsNullOrEmpty(ControlName) ? String.Format("for=\"{0}\"", ControlName) : ""
                        );
        }
        #endregion

        #region "验证脚本多语言"
        /// <summary>
        /// 获取当前验证引擎语言文件的URL
        /// </summary>
        /// <returns></returns>
        public String ViewValidationEngineLanguage()
        {
            String VEL = String.Format("{0}Resource/js/jquery.validationEngine-en.js?cdv={1}", ModulePath, CrmVersion);
            String language = WebHelper.GetStringParam(Request, "language", PortalSettings.DefaultLanguage).ToLower(); ;
            if (!String.IsNullOrEmpty(language) && language != "en-us")
            {
                //先判断这个语言文件是否存在
                String webJS = String.Format("{0}Resource/plugins/validation/jquery.validationEngine-{1}.js", ModulePath, language);
                String serverJS = MapPath(webJS);
                if (File.Exists(serverJS))
                {
                    VEL = String.Format("{0}?cdv={1}", webJS, CrmVersion);
                }
                else if (language.IndexOf("-") >= 0)
                {
                    String lTemp = language.Remove(language.IndexOf("-"));
                    webJS = String.Format("{0}Resource/plugins/validation/jquery.validationEngine-{1}.js", ModulePath, lTemp);
                    serverJS = MapPath(webJS);
                    if (File.Exists(serverJS))
                    {
                        VEL = String.Format("{0}?cdv={1}", webJS, CrmVersion);
                    }
                }
            }
            return VEL;
        }

        #endregion


        #region "获取文件后缀名和路径"

        /// <summary>
        /// 根据后缀名显示图标
        /// </summary>
        /// <param name="FileExtension">文件后缀</param>
        /// <param name="FilePath">文件路径</param>
        /// <returns></returns>
        public String GetPhotoExtension(String FileExtension, String FilePath)
        {
            FileExtension = FileExtension.ToLower();

            //先判断是否是图片格式的
            if (FileExtension == "jpg")
                return GetPhotoPath(FilePath);
            else if (FileExtension == "png")
                return GetPhotoPath(FilePath);
            else if (FileExtension == "jpeg")
                return GetPhotoPath(FilePath);
            else if (FileExtension == "gif")
                return GetPhotoPath(FilePath);
            else if (FileExtension == "bmp")
                return GetPhotoPath(FilePath);
            else if (FileExtension == "mp4")
                return GetFileIcon("video.jpg");
            else if (FileExtension == "ogv")
                return GetFileIcon("video.jpg");
            else if (FileExtension == "webm")
                return GetFileIcon("video.jpg");
            else if (FileExtension == "mp3")
                return GetFileIcon("audio.jpg");
            else if (FileExtension == "wma")
                return GetFileIcon("audio.jpg");
            else if (FileExtension == "zip")
                return GetFileIcon("zip.jpg");
            else if (FileExtension == "rar")
                return GetFileIcon("zip.jpg");
            else if (FileExtension == "7z")
                return GetFileIcon("zip.jpg");
            else if (FileExtension == "xls")
                return GetFileIcon("Document.jpg");
            else if (FileExtension == "txt")
                return GetFileIcon("text.jpg");
            else if (FileExtension == "cs")
                return GetFileIcon("code.jpg");
            else if (FileExtension == "html")
                return GetFileIcon("code.jpg");
            else if (FileExtension == "pdf")
                return GetFileIcon("pdf.jpg");
            else if (FileExtension == "doc")
                return GetFileIcon("Document.jpg");
            else if (FileExtension == "docx")
                return GetFileIcon("Document.jpg");
            else
                return GetFileIcon("Unknown type.jpg");
        }

        /// <summary>
        /// 获取图片的路径
        /// </summary>
        /// <param name="FilePath">图片路径</param>
        /// <returns></returns>
        public String GetPhotoPath(String FilePath)
        {
            return String.Format("{0}{1}", DNNGalleryProGame_PortalSettings.HomeDirectory, FilePath);
        }

        /// <summary>
        /// 获取文件图标
        /// </summary>
        /// <param name="IconName">图标文件</param>
        /// <returns></returns>
        public String GetFileIcon(String IconName)
        {
            return String.Format("{0}Resource/images/crystal/{1}", ModulePath, IconName);
        }

        #endregion

        #region "Page_Init 权限验证"
        /// <summary>
        /// 关于权限验证
        /// </summary>
        protected virtual void Page_Init(System.Object sender, System.EventArgs e)
        {

            //如果不是此模块,则会抛出异常,提示非法入侵
            if (!(("DNNGo.DNNGalleryProGame").IndexOf(BaseModuleName, StringComparison.CurrentCultureIgnoreCase) >= 0))
            {
                Response.Redirect(Globals.NavigateURL(TabId), true);
            }

            //没有登陆的用户
            if (!(UserId > 0))
            {
                Response.Redirect(Globals.NavigateURL(PortalSettings.LoginTabId, "Login", "returnurl=" + HttpUtility.UrlEncode(HttpContext.Current.Request.RawUrl)), true);
            }
            else if (!ModulePermissionController.HasModuleAccess(SecurityAccessLevel.Edit, "CONTENT", ModuleConfiguration))
            {
                Response.Redirect(Globals.NavigateURL(TabId), true);
            }
        }
        #endregion

        #region "载入模块"
        /// <summary>
        /// 载入模块
        /// </summary>
        /// <param name="ModuleSrc"></param>
        /// <param name="phContainer"></param>
        public void LoadModule(String ModuleSrc, ref PlaceHolder phContainer)
        {
            BaseModule ManageContent = new BaseModule();
            ManageContent.ID = ModuleSrc.Replace(".ascx", "");
            String ContentSrc = ResolveClientUrl(string.Format("{0}/{1}", this.TemplateSourceDirectory, ModuleSrc));
            ManageContent = (BaseModule)LoadControl(ContentSrc);
            ManageContent.ModuleConfiguration = this.ModuleConfiguration;
            ManageContent.LocalResourceFile = Localization.GetResourceFile(this, string.Format("{0}.resx", ModuleSrc));
            phContainer.Controls.Add(ManageContent);
        }
        #endregion


        #region "获取模块信息属性DNN920"

        /// <summary>
        /// 获取模块信息属性DNN920
        /// </summary>
        /// <param name="m">模块信息</param>
        /// <param name="Name">属性名</param>
        /// <returns></returns>
        public String ModuleProperty(ModuleInfo m,String Name)
        {
            bool propertyNotFound = false;
            return m.GetProperty(Name, "", null, UserInfo, DotNetNuke.Services.Tokens.Scope.DefaultSettings, ref propertyNotFound);
        }

        /// <summary>
        /// 获取模块信息属性DNN920
        /// </summary>
        /// <param name="Name">属性名</param>
        /// <returns></returns>
        public String ModuleProperty( String Name)
        {
            return ModuleProperty(ModuleConfiguration,Name);
        }


        #endregion


    }
}