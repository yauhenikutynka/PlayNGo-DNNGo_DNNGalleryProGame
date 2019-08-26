using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DNNGo.Modules.DNNGalleryProGame
{
    public partial class Resource_Item : BaseModule
    {
        #region "====属性===="
        /// <Description>
        /// 层编号
        /// </Description>
        public Int32 LayerID = WebHelper.GetIntParam(HttpContext.Current.Request, "LayerID", 0);

        /// <Description>
        /// 项编号
        /// </Description>
        public Int32 SliderID = WebHelper.GetIntParam(HttpContext.Current.Request, "SliderID", 0);


        public String LayerType = WebHelper.GetStringParam(HttpContext.Current.Request, "LayerType", "");


        private DNNGo_DNNGalleryProGame_Layer _LayerItem;
        /// <Description>
        /// 层
        /// </Description>
        public DNNGo_DNNGalleryProGame_Layer LayerItem
        {
            get
            {
                if (!(_LayerItem != null && _LayerItem.ID > 0))
                {
                    if (LayerID > 0)
                        _LayerItem = DNNGo_DNNGalleryProGame_Layer.FindByKeyForEdit(LayerID);
                    else
                        _LayerItem = new DNNGo_DNNGalleryProGame_Layer();
                }
                return _LayerItem;
            }
        }

        private List<KeyValueEntity> _LayerItems;
        /// <Description>
        /// 封装的参数集合
        /// </Description>
        public List<KeyValueEntity> LayerItems
        {
            get
            {
                if (!(_LayerItems != null && _LayerItems.Count > 0))
                {
                    if (LayerItem != null && LayerItem.ID > 0 && !String.IsNullOrEmpty(LayerItem.Options))
                    {
                        try
                        {
                            _LayerItems = ConvertTo.Deserialize<List<KeyValueEntity>>(LayerItem.Options);
                        }
                        catch
                        {
                            _LayerItems = new List<KeyValueEntity>();
                        }
                    }
                    else
                        _LayerItems = new List<KeyValueEntity>();
                }
                return _LayerItems;
            }
        }


        /// <Description>
        /// 提示操作类
        /// </Description>
        MessageTips mTips = new MessageTips();
        #endregion


        #region "====方法===="

 


        /// <summary>
        /// 绑定顶部的菜单列表
        /// </summary>
        public void BindMenuList()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            List<String> TempList = new List<String>();

            List<SettingEntity> LayerOptions = Setting_LayerSettingDB;
            LayerOptions = LayerOptions.FindAll(r => String.IsNullOrEmpty(r.Layout) || ( String.IsNullOrEmpty(LayerType) && String.IsNullOrEmpty( r.Layout))
             || (  !String.IsNullOrEmpty(LayerType) &&   r.Layout.IndexOf(LayerType, StringComparison.CurrentCultureIgnoreCase) >= 0    ));

            if (LayerOptions != null && LayerOptions.Count > 0)
            {
                foreach (SettingEntity item in LayerOptions)
                {
                    if (!TempList.Exists(r1 => r1 == item.Categories))
                    {
                        String active = TempList.Count == 0 ? "active" : "";
                        sb.AppendFormat("<li class=\"{0}\"><a href=\"#tabs-left-{1}\"  data-toggle=\"tab\"  title=\"{3}\">{2}</a></li>", active, FormatName(item.Categories), item.Categories, item.Categories).AppendLine();
                        TempList.Add(item.Categories);
                    }
                }
            }
            liNavTabsHTML.Text = sb.ToString();
        }










        /// <summary>
        /// 绑定选项分组框到页面
        /// </summary>
        private void BindCategoriesToPage()
        {
            //获取效果参数
            List<SettingEntity> LayerOptions = Setting_LayerSettingDB;
            LayerOptions = LayerOptions.FindAll(r => String.IsNullOrEmpty(r.Layout) || (String.IsNullOrEmpty(LayerType) && String.IsNullOrEmpty(r.Layout))
      || (!String.IsNullOrEmpty(LayerType) && r.Layout.IndexOf(LayerType, StringComparison.CurrentCultureIgnoreCase) >= 0));


            List<KeyValueEntity> Items = new List<KeyValueEntity>();

            foreach (SettingEntity ItemSetting in LayerOptions)
            {
                if (!Items.Exists(r1 => r1.Key == ItemSetting.Categories))
                {
                    Items.Add(new KeyValueEntity(ItemSetting.Categories, ""));
                }
            }

            if (!(Items != null && Items.Count > 0))
            {
                Items.Add(new KeyValueEntity("Basic Categories", ""));
            }
            //绑定参数项
            RepeaterCategories.DataSource = Items;
            RepeaterCategories.DataBind();

        }



        /// <summary>
        /// 绑定选项分组框到页面
        /// </summary>
        private void BindGroupToPage(Repeater RepeaterGroup, String Categories, out int OptionCount)
        {
            OptionCount = 0;
            //获取效果参数
            List<SettingEntity> LayerOptions = Setting_LayerSettingDB;
            LayerOptions = LayerOptions.FindAll(r => String.IsNullOrEmpty(r.Layout) || (String.IsNullOrEmpty(LayerType) && String.IsNullOrEmpty(r.Layout))
        || (!String.IsNullOrEmpty(LayerType) && r.Layout.IndexOf(LayerType, StringComparison.CurrentCultureIgnoreCase) >= 0));


            List<KeyValueEntity> Items = new List<KeyValueEntity>();


            LayerOptions = LayerOptions.FindAll(r1 => r1.Categories == Categories);
            OptionCount = LayerOptions.Count;

            foreach (SettingEntity ItemSetting in LayerOptions)
            {
                if (!Items.Exists(r1 => r1.Key == ItemSetting.Group))
                {
                    Items.Add(new KeyValueEntity(ItemSetting.Group, "", Categories));
                }
            }

            if (!(Items != null && Items.Count > 0))
            {
                Items.Add(new KeyValueEntity("Basic Options", ""));
            }

            //绑定参数项
            RepeaterGroup.DataSource = Items;
            RepeaterGroup.DataBind();

        }




        /// <summary>
        /// 绑定选项集合到页面
        /// </summary>
        private void BindOptionsToPage(Repeater RepeaterOptions, String Group, out int OptionCount)
        {
            OptionCount = 0;
            //获取效果参数
            List<SettingEntity> LayerOptions = Setting_LayerSettingDB;
            LayerOptions = LayerOptions.FindAll(r => String.IsNullOrEmpty(r.Layout) || (String.IsNullOrEmpty(LayerType) && String.IsNullOrEmpty(r.Layout))
          || (!String.IsNullOrEmpty(LayerType) && r.Layout.IndexOf(LayerType, StringComparison.CurrentCultureIgnoreCase) >= 0));


            if (LayerOptions != null && LayerOptions.Count > 0)
            {
                LayerOptions = LayerOptions.FindAll(r1 => r1.Group == Group);
                OptionCount = LayerOptions.Count;
                //绑定参数项
                RepeaterOptions.DataSource = LayerOptions;
                RepeaterOptions.DataBind();
            }
        }






        /// <Description>
        /// 拼接数据项的设置参数
        /// </Description>
        /// <returns></returns>
        public String SetItemSettings()
        {
            //获取效果参数
            List<SettingEntity> LayerSettingDB = Setting_LayerSettingDB;
            List<KeyValueEntity> list = new List<KeyValueEntity>();

            if (LayerSettingDB != null && LayerSettingDB.Count > 0)
            {
                ControlHelper ControlItem = new ControlHelper(this);

                foreach (SettingEntity ri in LayerSettingDB)
                {
                    KeyValueEntity item = new KeyValueEntity();
                    item.Key = ri.Name;
                    item.Value = ControlHelper.GetWebFormValue(ri, this);
                    list.Add(item);
                }
            }
            //添加层的类型
            list.Add(new KeyValueEntity("LayerType", LayerType));

            return ConvertTo.Serialize<List<KeyValueEntity>>(list);
        }

        /// <summary>
        /// 设置数据项
        /// </summary>
        /// <returns></returns>
        public DNNGo_DNNGalleryProGame_Layer SetDataItem()
        {
            Int32 LayerResult = 0;

            DNNGo_DNNGalleryProGame_Layer Layer  = LayerItem;

            Layer.Options = SetItemSettings();


            Layer.LastIP = WebHelper.UserHost;
            Layer.LastTime = xUserTime.UtcTime();
            Layer.LastUser = UserId;

            if (Layer.ID > 0)
            {
               LayerResult=  Layer.Update();
            }
            else
            {

                Layer.CreateTime = xUserTime.UtcTime();
                Layer.CreateUser = UserId;

                Layer.ModuleId = ModuleId;
                Layer.PortalId = PortalId;

                Layer.SliderID = SliderID;

                Layer.Status = (Int32)EnumStatus.Activated;


                QueryParam Sqp = new QueryParam();
                Sqp.ReturnFields = Sqp.Orderfld = DNNGo_DNNGalleryProGame_Layer._.Sort;
                Sqp.OrderType = 1;
                Sqp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Layer._.ModuleId, ModuleId, SearchType.Equal));
                Layer.Sort = Convert.ToInt32(DNNGo_DNNGalleryProGame_Layer.FindScalar(Sqp)) + 2;


                Layer.ID = Layer.Insert();
            }



            return Layer;
        }



        #endregion






        #region "====事件===="


        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               

            }
            catch (Exception exc) //Module failed to load
            {
                ProcessModuleLoadException(exc);
            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void Page_Init(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {



                    BindMenuList();

                    cmdReset.Attributes.Add("onClick", "javascript:return confirm('" + ViewResourceText("Reset_Confirm", "Are you sure to reset the skin settings?") + "');");

                }
                BindCategoriesToPage();

                //加载脚本
                //LoadViewScript();

            }
            catch (Exception exc) //Module failed to load
            {
                ProcessModuleLoadException(exc);
            }
        }

        /// <summary>
        /// 更新内容
        /// </summary>
        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {

               DNNGo_DNNGalleryProGame_Layer Layer =SetDataItem();

                if (Layer != null && Layer.ID > 0)
                {

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.AppendFormat("<script>window.parent.LayerList.Post({0});</script>", Layer.ID).AppendLine();
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PostLayerItem", sb.ToString());
                    //Response.Write(sb.ToString());
                    //Response.End();
                }

                //mTips.LoadMessage("SaveOptionsSuccess", EnumTips.Success, this, new String[] { });

               //Response.Redirect(WebHelper.GetScriptUrl, false);
 

            }
            catch (Exception exc)
            {
                ProcessModuleLoadException(exc);
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        protected void cmdReset_Click(object sender, EventArgs e)
        {
            try
            {


                Response.Redirect(WebHelper.GetScriptUrl, false);
            }
            catch (Exception exc)
            {
                ProcessModuleLoadException(exc);
            }
        }


        /// <summary>
        /// 取消
        /// </summary>
        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception exc)
            {
                ProcessModuleLoadException(exc);
            }
        }

        #endregion


        #region "===参数列表三层嵌套代码==="


        protected void RepeaterOptions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SettingEntity ThemeSetting = (e.Item.DataItem as SettingEntity).Clone();

                KeyValueEntity KeyValue = LayerItems.Find(r1 => r1.Key == ThemeSetting.Name);
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
                liTitle.Text = String.Format("<label class=\"control-label\" for=\"{1}\">{0}:</label>", !String.IsNullOrEmpty(ThemeSetting.Alias) ? ThemeSetting.Alias : ThemeSetting.Name, ctl.ViewControlID(ThemeSetting));

                Label lbRequired = e.Item.FindControl("lbRequired") as Label;
                if (ThemeSetting.ControlType == EnumControlType.FileUpload.ToString())
                {
                    lbRequired.Visible = ThemeSetting.Required && String.IsNullOrEmpty(ThemeSetting.DefaultValue);
                }
                else
                {
                    lbRequired.Visible = ThemeSetting.Required;
                }

                if (!String.IsNullOrEmpty(ThemeSetting.Description))
                {
                    Literal liHelp = e.Item.FindControl("liHelp") as Literal;
                    liHelp.Text = String.Format("<span class=\"help-block\"><i class=\"fa fa-info-circle\"></i> {0}</span>", ThemeSetting.Description);
                }
            }
        }


        /// <summary>
        /// 分组绑定事件
        /// </summary>
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


        /// <summary>
        /// 分组绑定事件
        /// </summary>
        protected void RepeaterCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater RepeaterGroup = e.Item.FindControl("RepeaterGroup") as Repeater;
                KeyValueEntity CategoriesItem = e.Item.DataItem as KeyValueEntity;
                int OptionCount = 0;
                BindGroupToPage(RepeaterGroup, CategoriesItem.Key, out OptionCount);

                if (OptionCount == 0)
                {
                    e.Item.Visible = false;
                }

            }
        }





        #endregion



  
    }
}