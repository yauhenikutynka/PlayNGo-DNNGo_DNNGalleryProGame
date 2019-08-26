using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
using DotNetNuke.Services.Localization;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Common.Utilities;
using System.Threading;
using System.Web.UI.HtmlControls;

namespace DNNGo.Modules.DNNGalleryProGame
{
    public partial class Setting_ManagerItem : BaseModule
    {





        #region "属性"
        /// <Description>
        /// 项编号
        /// </Description>
        public Int32 SliderID = WebHelper.GetIntParam(HttpContext.Current.Request, "ID", 0);


        private DNNGo_DNNGalleryProGame_Slider _SliderItem;
        /// <Description>
        /// 文章项
        /// </Description>
        public DNNGo_DNNGalleryProGame_Slider SliderItem
        {
            get
            {
                if (!(_SliderItem != null && _SliderItem.ID > 0))
                {
                    if (SliderID > 0)
                        _SliderItem = DNNGo_DNNGalleryProGame_Slider.FindByKeyForEdit(SliderID);
                    else
                        _SliderItem = new DNNGo_DNNGalleryProGame_Slider();
                }
                return _SliderItem;
            }
        }

        private List<KeyValueEntity> _ItemSettings;
        /// <Description>
        /// 封装的参数集合
        /// </Description>
        public List<KeyValueEntity> ItemSettings
        {
            get
            {
                if (!(_ItemSettings != null && _ItemSettings.Count > 0))
                {
                    if (SliderItem != null && SliderItem.ID > 0 && !String.IsNullOrEmpty(SliderItem.Options))
                    {
                        try
                        {
                            _ItemSettings = ConvertTo.Deserialize<List<KeyValueEntity>>(SliderItem.Options);
                        }
                        catch
                        {
                            _ItemSettings = new List<KeyValueEntity>();
                        }
                    }
                    else
                        _ItemSettings = new List<KeyValueEntity>();
                }
                return _ItemSettings;
            }
        }

        public String PostLayerResource(String _Token)
        {
            return String.Format("{0}Resource_Service.aspx?ModuleId={1}&PortalId={2}&TabId={3}&SliderID={4}&Token={5}&LayerID=", ModulePath, ModuleId, PortalId, TabId,  SliderID,_Token);
        }
 
        

        /// <Description>
        /// 提示操作类
        /// </Description>
        MessageTips mTips = new MessageTips();
        #endregion


        #region "事件"

        /// <Description>
        /// 页面加载
        /// </Description>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定页面项
                BindPageItem();
                // 绑定方案项
                BindDataItem();
            }
            //绑定设置参数到页面
            BindGroupsToPage("Left");
            BindGroupsToPage("Right");
            //绑定关联数据
            BindRelationsToPage();



        }

       
        /// <Description>
        /// 更新文章
        /// </Description>
        protected void cmdPublish_Click(object sender, EventArgs e)
        {
            try
            {
                // 设置方案项

                DNNGo_DNNGalleryProGame_Slider Article = new DNNGo_DNNGalleryProGame_Slider();
                Int32 SaveResult = SaveDataItem(-1, ref Article);

                if (SaveResult > 0)
                {
                    mTips.LoadMessage("SaveSliderSuccess", EnumTips.Success, this);
                    if (SliderID == 0)
                    {
                        Response.Redirect(xUrl("ID", Article.ID.ToString(), "AddNew"), false);
                    }
                    else
                    {

                      
                        Response.Redirect(xUrl("ManagerList"), false);
                    }
                }
                else
                {
                    mTips.IsPostBack = false;
                    mTips.LoadMessage("SaveSliderError", EnumTips.Success, this, new String[] { "" });
                }
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }

        /// <summary>
        /// 保存草稿
        /// </summary>
        protected void cmdSaveDraft_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 ArticleStatus = (Int32)EnumStatus.Hidden;
                DNNGo_DNNGalleryProGame_Slider Article = new DNNGo_DNNGalleryProGame_Slider();
                Int32 SaveResult = SaveDataItem(ArticleStatus, ref Article);



                if (SaveResult >0)
                {
                    mTips.LoadMessage("SaveSliderSuccess", EnumTips.Success, this, new String[] { "" });
                    Response.Redirect(xUrl("ID", Article.ID.ToString(), "AddNew"), false);
                }
                else
                {
                    mTips.IsPostBack = false;
                    mTips.LoadMessage("SaveSliderError", EnumTips.Success, this, new String[] { "" });
                }
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }


        /// <Description>
        /// 取消
        /// </Description>
        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {

                Response.Redirect(xUrl("ManagerList"), false);
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }
 

 


        protected void RepeaterOptions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SettingEntity ThemeSetting = e.Item.DataItem as SettingEntity;

                if (ThemeSetting != null && !String.IsNullOrEmpty(ThemeSetting.Name))
                {
                    Boolean IsRightLayout = !String.IsNullOrEmpty(ThemeSetting.Layout) && ThemeSetting.Layout.IndexOf("Right", StringComparison.CurrentCultureIgnoreCase) >= 0;

                    KeyValueEntity KeyValue = ItemSettings.Find(r1 => r1.Key == ThemeSetting.Name);
                    if (KeyValue != null && !String.IsNullOrEmpty(KeyValue.Key))
                    {
                        ThemeSetting.DefaultValue = KeyValue.Value.ToString();
                    }

                    //构造输入控件
                    PlaceHolder ThemePH = e.Item.FindControl("ThemePH") as PlaceHolder;

                    #region "创建控件"


                    ControlHelper ctl = new ControlHelper(this);

                    ThemePH.Controls.Add((Control)ctl.ViewControl(ThemeSetting));
                    #endregion

                    Literal liTitle = e.Item.FindControl("liTitle") as Literal;


                    liTitle.Text = String.Format("<label class=\"col-sm-{2} control-label\" for=\"{1}\">{0}:</label>", !String.IsNullOrEmpty(ThemeSetting.Alias) ? ThemeSetting.Alias : ThemeSetting.Name, ctl.ViewControlID(ThemeSetting), IsRightLayout ? 3 : 2);

                    if (!String.IsNullOrEmpty(ThemeSetting.Description))
                    {
                        Literal liHelp = e.Item.FindControl("liHelp") as Literal;
                        liHelp.Text = String.Format("<span class=\"help-block\"><i class=\"fa fa-info-circle\"></i> {0}</span>", ThemeSetting.Description);
                    }
                }

                
            }
        }


        /// <Description>
        /// 分组绑定事件
        /// </Description>
        protected void RepeaterGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater RepeaterOptions = e.Item.FindControl("RepeaterOptions") as Repeater;
                KeyValueEntity GroupItem = e.Item.DataItem as KeyValueEntity;
                int OptionCount = 0;
                BindOptionsToPage(RepeaterOptions, GroupItem.Key, out OptionCount);

                if (OptionCount == 0)
                {
                    e.Item.Visible = false;
                }

            }
        }

        #endregion

        #region "方法"
        /// <Description>
        /// 绑定页面项
        /// </Description>
        private void BindPageItem()
        {

            EffectDBEntity EffectDB = Setting_EffectDB;
 

            hlAddGroups.NavigateUrl = xUrl("Groups");
            hlAddGroups.Attributes.Add("onclick", String.Format("return confirm('{0}');", Localization.GetString("hlAddGroups.Confirm", this.LocalResourceFile)));
            divGroups.Visible = EffectDB.Groups;
            divCustomModels.Visible = EffectDB.CustomModels;
            divNumbers.Visible = EffectDB.Downloads;

            //增加权限用户
            DotNetNuke.Security.Roles.RoleController rc = new DotNetNuke.Security.Roles.RoleController();
            WebHelper.BindList(cblPermissionsRoles, rc.GetPortalRoles(PortalId), "RoleName", "RoleName");
            

            //构造增加按钮
            if (EffectDB.Layers && SliderID > 0)
            {

                liTitle_Layers.Text = String.Format("{0} List", EffectDB.LayerName);


                StringBuilder AddNewBuilder = new StringBuilder();
                //通用按钮
                AddNewBuilder.Append(AddNewLayerHtml(""));

                if (!String.IsNullOrEmpty(EffectDB.LayerType))
                {
                    List<String> AddNewList = Common.GetList(EffectDB.LayerType);
                    foreach (String AddNew in AddNewList)
                    {
                        if (!String.IsNullOrEmpty(AddNew))
                        {
                            AddNewBuilder.Append(AddNewLayerHtml(AddNew));
                        }
                    }
                 
                }

                liAddNewLink_List.Text = AddNewBuilder.ToString();

                div_Layers.Visible = true;
              
            }
            else
            {
                div_Layers.Visible = false;
            }


 


            ////插入用户按钮的连接
            //hlCreateUser.NavigateUrl = String.Format("{0}Resource_UserList.aspx?ModuleId={1}&PortalId={2}&UserId={3}&HomeDirectory={4}&TB_iframe=true&keepThis=true&height=400&width=600", ModulePath, ModuleId, PortalId, UserId, HttpUtility.UrlEncode(PortalSettings.HomeDirectory));

            //绑定状态代码
            WebHelper.BindList(ddlArticleStatus, typeof(EnumStatus));
 
        }

        /// <summary>
        /// 增加Layer按钮
        /// </summary>
        /// <param name="AddNew"></param>
        /// <returns></returns>
        public String AddNewLayerHtml(String AddNew)
        {
            StringBuilder AddNewBuilder = new StringBuilder();

            String AddNewText = String.IsNullOrEmpty(AddNew) ? "" : String.Format(" [{0}]", AddNew);

            String data_href = String.Format("data-href=\"{0}Resource_Item.aspx?ModuleId={1}&PortalId={2}&TabId={3}&SliderID={4}&LayerType={5}\"", ModulePath, ModuleId, PortalId, TabId, SliderID, AddNew);
            AddNewBuilder.AppendFormat("<a href=\"#AddLayer_Modal\" {0} class=\"add_Layer  btn btn-xs btn-bricky tooltips\" data-placement=\"top\" data-toggle=\"modal\" data-original-title=\"Add New {1} {2}\"><i class='fa fa-plus'></i> Add New {1} {2}</a>", data_href, Setting_EffectDB.LayerName, AddNewText);
            AddNewBuilder.AppendLine(" ");

            return AddNewBuilder.ToString();
        }



        /// <Description>
        /// 绑定数据项
        /// </Description>
        private void BindDataItem()
        {
            DNNGo_DNNGalleryProGame_Slider Article = SliderItem;

            //验证文章是否存在
            if (SliderID > 0 && (Article == null || SliderID != Article.ID))
            {
                //需要给出提示,载入文章错误
                mTips.LoadMessage("LoadingGalleryError", EnumTips.Error, this, new String[] { "" });
                Response.Redirect(xUrl("ManagerList"), false);
            }

            if (Article == null) Article = new DNNGo_DNNGalleryProGame_Slider();
 

            if (divGroups.Visible) BindTreeGroups(Article);


            cbPermissionsAllUsers.Checked = Article.Per_AllUsers == 0 ? true : false;

            //List<String> Per_Roles = Common.GetList(Article.Per_Roles, "|");
            WebHelper.SelectedListMultiByValue(cblPermissionsRoles, Article.Per_Roles);


            //文章状态
            liArticleStatus.Text = EnumHelper.GetEnumTextVal(Article.Status, typeof(EnumStatus));
            WebHelper.SelectedListByValue(ddlArticleStatus, Article.Status);//管理员看到的文章状态

            //发布时间和结束时间
            if (SliderID > 0 && Article != null && Article.ID > 0)
            {
                liStartDateTime.Text = Article.StartTime.ToString("MM/dd/yyyy hh:mm tt", new CultureInfo("en-US", false));//Thread.CurrentThread.CurrentCulture
                liDisableDateTime.Text = Article.EndTime.ToString("MM/dd/yyyy hh:mm tt", new CultureInfo("en-US", false));//Thread.CurrentThread.CurrentCulture
            }
            txtStartDate.Text = Article.StartTime.ToString("MM/dd/yyyy", new CultureInfo("en-US", false));
            txtStartTime.Text = Article.StartTime.ToString("hh:mm tt", new CultureInfo("en-US", false));

            txtDisableDate.Text = Article.EndTime.ToString("MM/dd/yyyy", new CultureInfo("en-US", false));
            txtDisableTime.Text = Article.EndTime.ToString("hh:mm tt", new CultureInfo("en-US", false));


            txtHeatNumber.Text = Article.Heats.ToString();
            txtDownloadNumber.Text = Article.Clicks.ToString();


            //关联集合
            hfRelations.Value = Article.Relations;


        }

        /// <Description>
        /// 保存文章
        /// </Description>
        /// <param name="ArticleStatus">文章状态(为-1的时候取选项的值)</param>
        private Int32 SaveDataItem(Int32 ArticleStatus, ref DNNGo_DNNGalleryProGame_Slider Article)
        {



            int ResultArticle = 0;

            Article = SliderItem;



            //数字更新
            Article.Heats = WebHelper.GetIntParam(Request, txtHeatNumber.UniqueID, 0);
            Article.Clicks = WebHelper.GetIntParam(Request, txtDownloadNumber.UniqueID, 0);

            //权限
            Article.Per_AllUsers = cbPermissionsAllUsers.Checked ? 0 : 1;

    
            String textStr, idStr = String.Empty;
            WebHelper.GetSelected(cblPermissionsRoles,out textStr,out idStr);
            Article.Per_Roles = idStr;

            //文章的发布状态
            Article.Status = Convert.ToInt32(ddlArticleStatus.Items[ddlArticleStatus.SelectedIndex].Value);

            //设置属性
            SetItemSettings(ref Article);
            Article.Extension = SetExtensionSettings();


            //关联集合
            Article.Relations = hfRelations.Value;

            //更新项
            Article.LastIP = WebHelper.UserHost;
            Article.LastTime = xUserTime.UtcTime();
            Article.LastUser = UserId;

            //发布状态和时间
            DateTime oTime = xUserTime.LocalTime();
            string[] expectedFormats = { "G", "g", "f", "F" };
            string StartDate = WebHelper.GetStringParam(Request, txtStartDate.UniqueID, oTime.ToString("MM/dd/yyyy"));
            string StartTime = WebHelper.GetStringParam(Request, txtStartTime.UniqueID, oTime.ToString("hh:mm tt"));
            if (DateTime.TryParseExact(String.Format("{0} {1}", StartDate, StartTime), "MM/dd/yyyy hh:mm tt", new CultureInfo("en-US", false), DateTimeStyles.AllowWhiteSpaces, out oTime))
            {
                if (oTime.Second == 0) oTime = oTime.AddSeconds(DateTime.Now.Second);//补秒

                Article.StartTime = oTime;
            }
            //发布状态和时间
            DateTime EndTime = xUserTime.LocalTime().AddYears(10);
            string DisableDate = WebHelper.GetStringParam(Request, txtDisableDate.UniqueID, EndTime.ToString("MM/dd/yyyy"));
            string DisableTime = WebHelper.GetStringParam(Request, txtDisableTime.UniqueID, EndTime.ToString("hh:mm tt"));
            if (DateTime.TryParseExact(String.Format("{0} {1}", DisableDate, DisableTime), "MM/dd/yyyy hh:mm tt", new CultureInfo("en-US", false), DateTimeStyles.AllowWhiteSpaces, out EndTime))
            {
                if (EndTime.Second == 0) EndTime = EndTime.AddSeconds(DateTime.Now.Second);//补秒

                Article.EndTime = EndTime;
            }

            //创建用户改为可以选择
            //Article.CreateUser = WebHelper.GetIntParam(Request, txtCreateUser.UniqueID, UserId);


            if (ArticleStatus == -1)//如果没有指定状态就取控件的
            {
                Article.Status = WebHelper.GetIntParam(Request, ddlArticleStatus.UniqueID, (Int32)EnumStatus.Activated);
            }
            else
            {
                Article.Status = ArticleStatus;
            }


            if (Article.ID > 0)
            {
                //更新
                ResultArticle = Article.Update();
            }
            else
            {
                //新增
                Article.CreateTime = xUserTime.UtcTime();
                Article.CreateUser = UserId;

                Article.ModuleId = ModuleId;
                Article.PortalId = PortalId;

                QueryParam Sqp = new QueryParam();
                Sqp.ReturnFields = Sqp.Orderfld = DNNGo_DNNGalleryProGame_Slider._.Sort;
                Sqp.OrderType = 1;
                Sqp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.ModuleId, ModuleId, SearchType.Equal));
                Article.Sort = Convert.ToInt32(DNNGo_DNNGalleryProGame_Slider.FindScalar(Sqp)) + 2;

                Article.ID = ResultArticle = Article.Insert();
            }


 

            if (ResultArticle > 0)
            {
                if (divGroups.Visible)
                {
                    //更新分类项
                    String Categorys = WebHelper.GetStringParam(Request, String.Format("post$groups${0}", ModuleId), "");
                    ManagedThreadPool.QueueUserWorkItem(new WaitCallback(ThreadUpdateGroups), new KeyValueEntity(Article.ID.ToString(), Categorys));
                }


              
        

                ////操作成功
                //mTips.LoadMessage("SaveArticleSuccess", EnumTips.Success, this, new String[] { Article.Title });

                ////操作成功需要跳转到首页
                //Response.Redirect(xUrl("Posts"));




            }
            else
            {
          
                //mTips.IsPostBack = false;

                ////操作失败
                //mTips.LoadMessage("SaveArticleError", EnumTips.Success, this, new String[] { Article.Title });

                ////操作失败就留在本页面

            }
            return ResultArticle;


        }
 


         


 


 

        /// <Description>
        /// 绑定树分类
        /// </Description>
        private void BindTreeGroups(DNNGo_DNNGalleryProGame_Slider Article)
        {
            List<Int32> SelectList = new List<Int32>();
            StringBuilder select = new StringBuilder();
            if (Article != null && Article.ID > 0)
            {
                //填充分类的关系
                List<DNNGo_DNNGalleryProGame_Slider_Group> Relationships = DNNGo_DNNGalleryProGame_Slider_Group.FindAllByArticleID(Article.ID);
                foreach (DNNGo_DNNGalleryProGame_Slider_Group Groups in Relationships)
                {
                    SelectList.Add(Groups.GroupID);
                    select.AppendFormat("{0},", Groups.GroupID);
                }
            }

            //绑定所有分类到页面
            QueryParam qp = new QueryParam();
            qp.Orderfld = DNNGo_DNNGalleryProGame_Group._.Sort;
            qp.OrderType = 0;
            int RecordCount = 0;
            qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Group._.ModuleId, ModuleId, SearchType.Equal));
            List<DNNGo_DNNGalleryProGame_Group> lst = DNNGo_DNNGalleryProGame_Group.FindAll(qp, out RecordCount);
          
 
            //拼接顶级分类的方法
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id=\"Groups-all\" class=\"tabs-panel\">");
            String ttString = CreateGroupsJson(lst, 0, SelectList);
            if (!String.IsNullOrEmpty(ttString))
            {
                sb.Append(ttString);
            }
            sb.Append("</div>");
            liGroups.Text = sb.ToString();

 

        }


        /// <Description>
        /// 拼接分类的JSon数据
        /// </Description>
        /// <param name="lst">分类列表</param>
        /// <param name="ParentID">上级编号</param>
        /// <returns>Json数据</returns>
        private String CreateGroupsJson(List<DNNGo_DNNGalleryProGame_Group> lst, Int32 ParentID, List<Int32> SelectList)
        {
            //筛选数据并排列
            QueryParam qp = new QueryParam();
            qp.Orderfld = DNNGo_DNNGalleryProGame_Group._.Sort;
            qp.OrderType = 0;
            qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Group._.ModuleId, ModuleId, SearchType.Equal));
            int RecordCount = 0;

            List<DNNGo_DNNGalleryProGame_Group> TempList = DNNGo_DNNGalleryProGame_Group.FindAll(qp, out RecordCount);
            //TempList = TempList.Sort(DNNGo_DNNGalleryProGame_Group._.Sort, false);


            //取出数据进行拼接
            StringBuilder sb = new StringBuilder();
            if (ParentID == 0)
                sb.Append("<ul id=\"Groupschecklist\" class=\"Categories_list list:Groups Groupschecklist form-no-clear\">");
            else
                sb.Append("<ul class=\"children\">");

            foreach (DNNGo_DNNGalleryProGame_Group Groups in TempList)
            {


                sb.AppendFormat("<li id=\"Groups-{0}\">", Groups.ID);
                sb.Append("<label class=\"checkbox-inline selectit\">");
                sb.AppendFormat("<input value=\"{0}\" class=\"square-green\" type=\"checkbox\" name=\"{1}\" id=\"post_groups_{2}\" {4} />{3}</label>",
                    Groups.ID,
                    String.Format("post$groups${0}", ModuleId),
                    String.Format("post_groups_{0}", ModuleId),
                    Groups.Name,
                    SelectList.Contains(Groups.ID) ? "checked=\"checked\"" : "");

                //String ttString = CreateGroupsJson(lst, Groups.ID, SelectList);
                //if (!String.IsNullOrEmpty(ttString))
                //{
                //    sb.Append(ttString);
                //}
                sb.Append("</li>");
            }
            sb.Append("</ul>");

            if (TempList.Count > 0)
                return sb.ToString();
            else
                return String.Empty;
        }



        /// <Description>
        /// 绑定选项分组框到页面
        /// </Description>
        private void BindGroupsToPage(String Layout)
        {

            Repeater RepeaterGroup = FindControl(String.Format("RepeaterGroup_{0}", Layout)) as Repeater;
            HtmlGenericControl divOptions = FindControl(String.Format("divOptions_{0}", Layout)) as HtmlGenericControl;

            if (RepeaterGroup != null && divOptions != null)
            {
                //获取效果参数
                List<SettingEntity> ItemSettingDB = Setting_SliderSettingDB.FindAll(r=>r.Layout == Layout);
                if (Layout != "Right")
                {
                    ItemSettingDB = Setting_SliderSettingDB.FindAll(r => r.Layout != "Right");
                }


                if (ItemSettingDB != null && ItemSettingDB.Count > 0)
                {

                    List<KeyValueEntity> Items = new List<KeyValueEntity>();
                    foreach (SettingEntity ItemSetting in ItemSettingDB)
                    {
                        if (!Items.Exists(r1 => r1.Key == ItemSetting.Group))
                        {
                            Items.Add(new KeyValueEntity(ItemSetting.Group, ""));
                        }
                    }

                    if (Items != null && Items.Count > 0)
                    {
                        //绑定参数项
                        RepeaterGroup.DataSource = Items;
                        RepeaterGroup.DataBind();
                    }
                    divOptions.Visible = true;
                }
            }
        }




        /// <Description>
        /// 绑定选项集合到页面
        /// </Description>
        private void BindOptionsToPage(Repeater RepeaterOptions, String Group, out int OptionCount)
        {
            OptionCount = 0;
            //获取效果参数
            List<SettingEntity> ItemSettingDB = Setting_SliderSettingDB;

            if (ItemSettingDB != null && ItemSettingDB.Count > 0)
            {
                ItemSettingDB = ItemSettingDB.FindAll(r1 => r1.Group == Group);
                OptionCount = ItemSettingDB.Count;
                //绑定参数项
                RepeaterOptions.DataSource = ItemSettingDB;
                RepeaterOptions.DataBind();
            }
        }

        /// <Description>
        /// 拼接数据项的设置参数
        /// </Description>
        /// <returns></returns>
        public void SetItemSettings(ref DNNGo_DNNGalleryProGame_Slider Article)
        {
            //获取效果参数
            List<SettingEntity> ItemSettingDB = Setting_SliderSettingDB;
            List<KeyValueEntity> list = new List<KeyValueEntity>();

            if (ItemSettingDB != null && ItemSettingDB.Count > 0)
            {
                ControlHelper ControlItem = new ControlHelper(ModuleId);

                foreach (SettingEntity ri in ItemSettingDB)
                {
                    KeyValueEntity item = new KeyValueEntity();
                    item.Key = ri.Name;
                    item.Value = ControlHelper.GetWebFormValue(ri, this);
                    list.Add(item);
                }
            }
           

            if (list.Exists(r => r.Key == "Attribute1"))
            {
               KeyValueEntity Attribute1 = list.Find(r => r.Key == "Attribute1");
               Int32 Attribute1_Value = 0;
               if (int.TryParse(Attribute1.Value.ToString(), out Attribute1_Value))
               {
                   Article.Attribute1 = Attribute1_Value;
               }
            }

            if (list.Exists(r => r.Key == "Attribute2"))
            {
                KeyValueEntity Attribute2 = list.Find(r => r.Key == "Attribute2");
                Article.Attribute2 = Attribute2.Value.ToString();
            }

            if (list.Exists(r => r.Key == "Title"))
            {
                KeyValueEntity TitleEntity = list.Find(r => r.Key == "Title");
                Article.Title = TitleEntity.Value.ToString();
            }


            String FriendlyUrl = String.Empty;
            if (list.Exists(r => r.Key == "FriendlyUrl"))
            {
                KeyValueEntity TitleEntity = list.Find(r => r.Key == "FriendlyUrl");
                FriendlyUrl = TitleEntity.Value.ToString();

                if (String.IsNullOrEmpty(FriendlyUrl) && !String.IsNullOrEmpty(Article.Title))
                {
                    FriendlyUrl = Common.FriendlySlug(Article.Title);
                }

                Article.FriendlyUrl = FriendlyUrl;
                TitleEntity.Value = FriendlyUrl;
                list[list.FindIndex(r => r.Key == "FriendlyUrl")] = TitleEntity;

            }




            Article.Options = ConvertTo.Serialize<List<KeyValueEntity>>(list);

        }


        /// <summary>
        /// 拼接扩展数据项的设置参数
        /// </summary>
        /// <returns></returns>
        public String SetExtensionSettings()
        {
            List<KeyValueEntity> list = new List<KeyValueEntity>();
            String Names = Request.Form[String.Format("Model$Name${0}", ModuleId)];
            String Values = Request.Form[String.Format("Model$Value${0}", ModuleId)];

            if (!String.IsNullOrEmpty(Names) && !String.IsNullOrEmpty(Values))
            {
                List<String> NameList = Common.GetList(Names,",");
                List<String> ValueList = Common.GetList(Values,",");

                for (Int32 i = 0; i < NameList.Count && i < ValueList.Count; i++)
                {
                    KeyValueEntity kv = new KeyValueEntity();
                    if (!String.IsNullOrEmpty(NameList[i]))
                    {
                        kv.Key = NameList[i];
                        kv.Value = ValueList[i];
                        list.Add(kv);
                    }
                }



            }
            return ConvertTo.Serialize<List<KeyValueEntity>>(list);
        }



        #endregion


        #region "==关联列表函数及事件集合=="

        /// <summary>
        /// 绑定动态模块到页面
        /// </summary>
        private void BindRelationsToPage()
        {
            //绑定动态模块的模态窗口 Resource_Attachments|Manager_Modal_SelectDownloadFiles
            hlSelectRelations.Attributes.Add("data-href", String.Format("{0}Resource_Masters.aspx?PortalId={1}&TabId={2}&ModuleId={3}&language={4}&Master=Setting_Modal_Sliders&SliderID={5}", ModulePath, PortalId, TabId, ModuleId, language,SliderID));



            List<GridItem> Items = new List<GridItem>();

            if (SliderItem != null && SliderItem.ID > 0 && !String.IsNullOrEmpty(SliderItem.Relations))
            {



                List<String> Relations = Common.GetList(SliderItem.Relations);


                if (Relations != null && Relations.Count > 0)
                {
                    TemplateFormat xf = new TemplateFormat(this);


                    foreach (var RelationId in Relations)
                    {
                        DNNGo_DNNGalleryProGame_Slider Slider = DNNGo_DNNGalleryProGame_Slider.FindByKeyForEdit(RelationId);
                        if (Slider != null && Slider.ID > 0)
                        {



                            Items.Add(new GridItem() {
                                ID = Slider.ID,
                                Title = Slider.Title,
                                Picture = xf.ViewPictureUrl(Slider),
                                Groups = xf.ToGroups(Slider)
                            });
                        }
                    }
                }
            }

            if (Items != null && Items.Count > 0)
            {
                //绑定参数项
                RepeaterRelations.DataSource = Items;
                RepeaterRelations.DataBind();
            }

        }

        /// <summary>
        /// 分组绑定事件
        /// </summary>
        protected void RepeaterRelations_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {


            }
        }

        #endregion


        #region "异步执行需要调用的方法"



        /// <Description>
        /// 利用线程执行更新文章分类关系的方法
        /// </Description>
        /// <param name="Article"></param>
        public void ThreadUpdateGroups(object KeyValue)
        {
            KeyValueEntity KeyValueItem = KeyValue as KeyValueEntity;
         
            if (KeyValueItem != null && !String.IsNullOrEmpty(KeyValueItem.Key))
            {
                DNNGo_DNNGalleryProGame_Slider_Group.Update(int.Parse(KeyValueItem.Key), KeyValueItem.Value.ToString());
            }
        }




        #endregion


       


    }


    public class GridItem
    {
        public Int32 ID
        {
            get;
            set;
        }

        public String Title
        {
            get;
            set;
        }


        public String Picture
        {
            get;
            set;
        }


        public String Groups
        {
            get;
            set;
        }
    }

}