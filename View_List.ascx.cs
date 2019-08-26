using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules.Actions;
using System.Collections;
using System.Collections.Specialized;

namespace DNNGo.Modules.DNNGalleryProGame
{
    public partial class View_List : BaseModule 
    {


        #region "扩展属性"

        /// <summary>
        /// 页码
        /// </summary>
        public Int32 PageIndex
        {
            get { return WebHelper.GetIntParam(HttpContext.Current.Request, String.Format("PageIndex{0}", ModuleId), 1); }
        }

        /// <summary>
        /// 排序名称
        /// </summary>
        public String SortName
        {
            get { return WebHelper.GetStringParam(HttpContext.Current.Request, String.Format("Sort{0}", ModuleId), ""); }
        }

        /// <summary>
        /// 排序类型 1为倒序
        /// </summary>
        public Int32 SortType
        {
            get { return WebHelper.GetIntParam(HttpContext.Current.Request, String.Format("SortType{0}", ModuleId), 1); }
        }

        /// <summary>
        /// 需要筛选开始时间
        /// </summary>
        public Boolean FilterStartTime
        {
            get { return Settings["DNNGalleryProGame_FilterStartTime"] != null  ? Convert.ToBoolean(Settings["DNNGalleryProGame_FilterStartTime"]) : true; }
        }
        /// <summary>
        /// 需要筛选结束时间
        /// </summary>
        public Boolean FilterEndTime
        {
            get { return Settings["DNNGalleryProGame_FilterEndTime"] != null  ? Convert.ToBoolean(Settings["DNNGalleryProGame_FilterEndTime"]) : true; }
        }

        

        #endregion


        #region "方法"

        /// <summary>
        /// 绑定查询的方法
        /// </summary>
        private QueryParam BindSearch(QueryParam qp)
        {

            qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.ModuleId, Settings_ModuleID, SearchType.Equal));
            qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.Status, (Int32)EnumStatus.Activated, SearchType.Equal));


            if (FilterStartTime)
            {
                qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.StartTime, xUserTime.UtcTime(), SearchType.LtEqual));
            }

            if (FilterEndTime)
            {
                qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.EndTime, xUserTime.UtcTime(), SearchType.GtEqual));
            }


            if (GroupID > 0)
            {
               String ArticleIDs =  DNNGo_DNNGalleryProGame_Slider_Group.FindArticleIDsByCategoryIDs(GroupID.ToString());
               if (!String.IsNullOrEmpty(ArticleIDs))
               {
                   qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.ID, ArticleIDs, SearchType.In));
               }
            }

            if (Attribute1 >= 0)
            {
                qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.Attribute1, Attribute1, SearchType.Equal));
            }

            if (!String.IsNullOrEmpty(  Attribute2))
            {
                qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.Attribute2, Attribute2, SearchType.Equal));
            }

            //需要根据条件来查找相应的权限
            if (UserId > 0 )
            {
                if (!UserInfo.IsSuperUser)//超级管理员不限制
                {
                    qp.WhereSql.Append(" ( ");
                    //公开的
                    qp.WhereSql.Append(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.Per_AllUsers, 0, SearchType.Equal).ToSql());

                    //有角色的
                    if (UserInfo.Roles != null && UserInfo.Roles.Length > 0)
                    {
                        qp.WhereSql.Append(" OR ");
                        qp.WhereSql.Append(" ( ");

                        Int32 RoleIndex = 0;
                        foreach (var r in UserInfo.Roles)
                        {
                            if (RoleIndex > 0)
                            {
                                qp.WhereSql.Append(" OR ");
                            }

                            qp.WhereSql.Append(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.Per_Roles, String.Format(",{0},", r), SearchType.Like).ToSql());

                            //qp.WhereSql.Append(" OR ");

                            //qp.WhereSql.Append(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.Per_Roles, r, SearchType.Like).ToSql());

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

            return qp;
        }


        /// <summary>
        /// 绑定数据项到前台
        /// </summary>
        public void BindDataItem(EffectDBEntity EffectDB)
        {
            List<DNNGo_DNNGalleryProGame_Slider> SliderList = new List<DNNGo_DNNGalleryProGame_Slider>();
            Hashtable Puts = new Hashtable();
            TemplateFormat xf = new TemplateFormat(this);
            xf.PhContent = PhContent;

            //读取需要载入的参数
            QueryParam qp = new QueryParam();
            qp = BindSearch(qp);


            



            int RecordCount = 0;

            qp.OrderType = 0;

            if (EffectDB.Pager)//传入分页的数量
            {
                qp.PageSize = xf.ViewXmlSettingT<Int32>("PageSize", 9999);
                qp.PageSize = qp.PageSize <= 0 ? 9999 : qp.PageSize;
                qp.PageIndex = PageIndex;
                Puts.Add("PageIndex", PageIndex);
            }

            if (String.IsNullOrEmpty(SortName))
            {

                if (Settings_Sortby == (Int32)EnumSortby.Time)
                {
                    qp.Orderfld = DNNGo_DNNGalleryProGame_Slider._.StartTime;
                    SliderList = DNNGo_DNNGalleryProGame_Slider.FindAll(qp, out RecordCount);
                }
                else if (Settings_Sortby == (Int32)EnumSortby.Time_Desc)
                {
                    qp.OrderType = 1;
                    qp.Orderfld = DNNGo_DNNGalleryProGame_Slider._.StartTime;
                    SliderList = DNNGo_DNNGalleryProGame_Slider.FindAll(qp, out RecordCount);
                }
                else if (Settings_Sortby == (Int32)EnumSortby.Title)
                {
                    qp.Orderfld = DNNGo_DNNGalleryProGame_Slider._.Title;
                    SliderList = DNNGo_DNNGalleryProGame_Slider.FindAll(qp, out RecordCount);
                }
                else if (Settings_Sortby == (Int32)EnumSortby.Title_Desc)
                {
                    qp.OrderType = 1;
                    qp.Orderfld = DNNGo_DNNGalleryProGame_Slider._.Title;
                    SliderList = DNNGo_DNNGalleryProGame_Slider.FindAll(qp, out RecordCount);
                }
                else if (Settings_Sortby == (Int32)EnumSortby.Random)
                {
                    qp.Orderfld = " newid() ";
                    SliderList = DNNGo_DNNGalleryProGame_Slider.FindRandomAll(qp, out RecordCount);
                }
                else
                {
                    qp.Orderfld = DNNGo_DNNGalleryProGame_Slider._.Sort;
                    SliderList = DNNGo_DNNGalleryProGame_Slider.FindAll(qp, out RecordCount);
                }

            }
            else
            {
                qp.OrderType = SortType;
                qp.Orderfld = SortName;
                SliderList = DNNGo_DNNGalleryProGame_Slider.FindAll(qp, out RecordCount);
            }




            Puts.Add("ThemeName", Settings_EffectThemeName);
            Puts.Add("SliderList", SliderList);
            Puts.Add("EffectName", Settings_EffectName);

            Puts.Add("SortName", SortName);
            Puts.Add("SortType", SortType);

            if (Settings_Sortby == (Int32)EnumSortby.Random)
            {
                Puts.Add("IsRandom", true);
            }
            else
            {
                Puts.Add("IsRandom", false);
            }

            //是否开启分组
            Puts.Add("GroupList", EffectDB.Groups ? GetGroupList() : new List<DNNGo_DNNGalleryProGame_Group>());
            Puts.Add("GroupID", GroupID);

            //Puts.Add("LayerList", EffectDB.Layers ? GetLayerList() : new List<DNNGo_DNNGalleryProGame_Layer>());

            if (EffectDB.Pager && RecordCount > qp.PageSize)
            {
                Puts.Add("Pager", new Pager(qp.PageIndex, qp.PageSize, Settings_ModuleID, RecordCount, EnumPageType.DnnURL).CreateHtml());//分页
            }
            else
            {
                Puts.Add("Pager", "");
            }

            liContent.Text = ViewTemplate(EffectDB, "Effect.html", Puts, xf);


        }

        /// <summary>
        /// 分组列表
        /// </summary>
        /// <returns></returns>
        public List<DNNGo_DNNGalleryProGame_Group> GetGroupList()
        {
            QueryParam qp = new QueryParam();
            qp.Orderfld = DNNGo_DNNGalleryProGame_Group._.Sort;
            qp.OrderType = 0;
            int RecordCount = 0;
            qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Group._.ModuleId, Settings_ModuleID, SearchType.Equal));
            return DNNGo_DNNGalleryProGame_Group.FindAll(qp, out RecordCount);
        }

        /// <summary>
        /// Layer列表
        /// </summary>
        /// <returns></returns>
        public List<DNNGo_DNNGalleryProGame_Layer> GetLayerList()
        {
            QueryParam qp = new QueryParam();
            qp.Orderfld = DNNGo_DNNGalleryProGame_Layer._.Sort;
            qp.OrderType = 0;
            int RecordCount = 0;
            qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Layer._.ModuleId, Settings_ModuleID, SearchType.Equal));
            qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Layer._.Status, (Int32)EnumStatus.Activated, SearchType.Equal));
            return DNNGo_DNNGalleryProGame_Layer.FindAll(qp, out RecordCount);
        }


        #endregion



        #region "事件"
 
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
               
                

                if (!String.IsNullOrEmpty(Settings_EffectName))
                {

                    if (!String.IsNullOrEmpty(Settings_EffectThemeName))
                    {
                         EffectDBEntity EffectDB = Setting_EffectDB;
                        if (!IsPostBack)
                        {
                            //绑定数据项到前台
                            BindDataItem(EffectDB);

                        }

                        //是否支持谷歌地图
                        if (EffectDB.GoogleMap)
                        {
                            BindJavaScriptFile("GoogleMap", "https://maps.google.com/maps/api/js?sensor=false");
                        }
 
                        //需要载入当前设置效果的主题CSS文件
                        String ThemeName = String.Format("{0}_{1}", Settings_EffectName, Settings_EffectThemeName);
                        String ThemePath = String.Format("{0}Effects/{1}/Themes/{2}/Style.css", ModulePath, Settings_EffectName, Settings_EffectThemeName);
                        BindStyleFile(ThemeName, ThemePath);

                        //绑定效果中配置的样式和脚本
                        BindXmlDBToPage(EffectDB, "Effect");
                       

                    }
                    else
                    {
                        //未定义效果对应的主题
                        liContent.Text = "";
                    }
                }
                else
                {
                    //未绑定效果
                    liContent.Text = "";
                }
 
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

 

        #endregion

 
    }
}