using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Services.Localization;

namespace DNNGo.Modules.DNNGalleryProGame
{
    public partial class Setting_ManagerLists : BaseModule
    {


        #region "属性"

        /// <summary>
        /// 提示操作类
        /// </summary>
        MessageTips mTips = new MessageTips();

        /// <summary>
        /// 当前页码
        /// </summary>
        public Int32 PageIndex = WebHelper.GetIntParam(HttpContext.Current.Request, "PageIndex", 1);

        /// <summary>
        /// 文章状态
        /// </summary>
        public Int32 ArticleStatus = WebHelper.GetIntParam(HttpContext.Current.Request, "Status", (Int32)EnumStatus.Activated);

 

        /// <summary>
        /// 文章搜索_标题
        /// </summary>
        public String Search_Title = WebHelper.GetStringParam(HttpContext.Current.Request, "SearchText", "");

        /// <summary>
        /// 总页码数
        /// </summary>
        public Int32 RecordPages
        {
            get;
            set;
        }

        /// <summary>
        /// 当前页面URL(不包含分页)
        /// </summary>
        public String CurrentUrl
        {
            get
            {

                List<String> urls = new List<String>();

                if (ArticleStatus != 1)
                {
                    urls.Add(String.Format("Status={0}", ArticleStatus));
                }

                if (!String.IsNullOrEmpty(Orderfld))
                {
                    urls.Add(String.Format("sort_f={0}", Orderfld));
                }

                if(GroupID >0)
                {
                    urls.Add(String.Format("GroupID{0}={1}", ModuleId, GroupID));

                }

                if (OrderType > 0)
                {
                    urls.Add(String.Format("sort_t={0}", OrderType));
                }

                if (!String.IsNullOrEmpty(Search_Title))
                {
                    urls.Add(String.Format("SearchText={0}", Search_Title));
                    txtSearch.Text = Search_Title;
                }

                return xUrl("", "", "ManagerList", urls.ToArray());
            }
        }


        /// <summary>
        /// 排序字段
        /// </summary>
        public string Orderfld = WebHelper.GetStringParam(HttpContext.Current.Request, "sort_f", DNNGo_DNNGalleryProGame_Slider._.Sort);


        /// <summary>
        /// 排序类型 1:降序 0:升序
        /// </summary>
        public int OrderType = WebHelper.GetIntParam(HttpContext.Current.Request, "sort_t",0);



        #endregion



        #region "方法"

        /// <summary>
        /// 绑定列表
        /// </summary>
        private void BindDataList()
        {
            QueryParam qp = new QueryParam();
            qp.OrderType = OrderType;
            qp.Orderfld = Orderfld;

            #region "分页的一系列代码"


            int RecordCount = 0;
            int pagesize = qp.PageSize = 10;
            qp.PageIndex = PageIndex;


            #endregion

            //查询的方法
            qp.Where = BindSearch();

            List<DNNGo_DNNGalleryProGame_Slider> Articles = DNNGo_DNNGalleryProGame_Slider.FindAll(qp, out RecordCount);
            qp.RecordCount = RecordCount;
            RecordPages = qp.Pages;
            lblRecordCount.Text = String.Format("{0} {2} / {1} {3}", RecordCount, RecordPages, ViewResourceText("Title_Items", "Items"), ViewResourceText("Title_Pages", "Pages"));


         


            hlAllArticle.Text = String.Format("{1} ({0})", DNNGo_DNNGalleryProGame_Slider.FindCountByStatus(Settings_ModuleID, -1), ViewResourceText("hlAllArticle", "All"));
            hlActivatedArticle.Text = String.Format("{1} ({0})", DNNGo_DNNGalleryProGame_Slider.FindCountByStatus(Settings_ModuleID, (Int32)EnumStatus.Activated), ViewResourceText("hlActivatedArticle", "Activated"));
            hlHiddenArticle.Text = String.Format("{1} ({0})", DNNGo_DNNGalleryProGame_Slider.FindCountByStatus(Settings_ModuleID, (Int32)EnumStatus.Hidden), ViewResourceText("hlHiddenArticle", "Hidden"));
            hlRecycleBinArticle.Text = String.Format("{1} ({0})", DNNGo_DNNGalleryProGame_Slider.FindCountByStatus(Settings_ModuleID, (Int32)EnumStatus.RecycleBin), ViewResourceText("hlRecycleBinArticle", "Recycle Bin"));



            //ctlPagingControl.TotalRecords = RecordCount;

            //if (RecordCount <= pagesize)
            //{
            //    ctlPagingControl.Visible = false;

            //}

            //如果不需要显示图片的效果
            List<SettingEntity> ItemSettings = Setting_SliderSettingDB;
            if (ItemSettings.Exists(r => r.Name == "Title"))
            {
                gvArticleList.Columns[1].Visible = true;
            }

            if (ItemSettings.Exists(r => r.Name == "Picture" || r.Name == "Thumbnails"))
            {
                gvArticleList.Columns[2].Visible = true;
            }


            if (Setting_EffectDB.Groups)
            {
                gvArticleList.Columns[3].Visible = true;
                divGroupFilter.Visible = true;
            }

            gvArticleList.DataSource = Articles;
            gvArticleList.DataBind();
            BindGridViewEmpty<DNNGo_DNNGalleryProGame_Slider>(gvArticleList, new DNNGo_DNNGalleryProGame_Slider());

 

           
        }



        /// <summary>
        /// 绑定页面项
        /// </summary>
        private void BindPageItem()
        {

            hlAllArticle.NavigateUrl = xUrl("Status", "-1", "ManagerList", String.Format("GroupID{0}={1}", ModuleId, GroupID));
            hlActivatedArticle.NavigateUrl = xUrl("Status", ((Int32)EnumStatus.Activated).ToString(), "ManagerList", String.Format("GroupID{0}={1}", ModuleId, GroupID));
            hlHiddenArticle.NavigateUrl = xUrl("Status", ((Int32)EnumStatus.Hidden).ToString(), "ManagerList", String.Format("GroupID{0}={1}", ModuleId, GroupID));
            hlRecycleBinArticle.NavigateUrl = xUrl("Status", ((Int32)EnumStatus.RecycleBin).ToString(), "ManagerList", String.Format("GroupID{0}={1}", ModuleId, GroupID));

            switch (ArticleStatus)
            {
                case -1: hlAllArticle.CssClass = "btn btn-default active"; break;
                case (Int32)EnumStatus.Activated: hlActivatedArticle.CssClass = "btn btn-default active"; break;
                case (Int32)EnumStatus.Hidden: hlHiddenArticle.CssClass = "btn btn-default active"; break;
                case (Int32)EnumStatus.RecycleBin: hlRecycleBinArticle.CssClass = "btn btn-default active"; break;
                default: hlActivatedArticle.CssClass = "btn btn-default active"; break;
            }




            hlAddNewLink.NavigateUrl = xUrl("AddNew");
 

        }


        /// <summary>
        /// 绑定查询的方法
        /// </summary>
        private List<SearchParam> BindSearch()
        {
            List<SearchParam> Where = new List<SearchParam>();
            Where.Add(new SearchParam("ModuleId", Settings_ModuleID, SearchType.Equal));
 
            //筛选文章的状态
            if (ArticleStatus >= 0)
            {
                Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.Status, ArticleStatus, SearchType.Equal));
            }

            if (GroupID > 0)
            {
                String ArticleIDs = DNNGo_DNNGalleryProGame_Slider_Group.FindArticleIDsByCategoryIDs(GroupID.ToString());
                if (!String.IsNullOrEmpty(ArticleIDs))
                {
                    Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.ID, ArticleIDs, SearchType.In));
                }
                else
                {
                    Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.ID, 0, SearchType.Equal));
                }
            }


            //Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Slider._.t, ArticleStatus, SearchType.Equal));



            return Where;
        }

        /// <summary>
        /// 绑定分组列表
        /// </summary>
        public void BindGroupList()
        {
            Int32 RecordCount = 0;
            System.Text.StringBuilder GroupString = new System.Text.StringBuilder();
            QueryParam qp = new QueryParam();
            qp.Orderfld = DNNGo_DNNGalleryProGame_Group._.Sort;
            qp.OrderType = 0;
            qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Group._.ModuleId, Settings_ModuleID, SearchType.Equal));
          
            List<DNNGo_DNNGalleryProGame_Group> GroupList = DNNGo_DNNGalleryProGame_Group.FindAll(qp, out RecordCount);
            DNNGo_DNNGalleryProGame_Group ThisAllGroup = new DNNGo_DNNGalleryProGame_Group();
            ThisAllGroup.ID = 0;
            ThisAllGroup.Name = "All";

            GroupList.Insert(0,ThisAllGroup);

            foreach (DNNGo_DNNGalleryProGame_Group GroupItem in GroupList)
            {
                String activeClass = GroupItem.ID == GroupID ? "active" : "";
                

                String LinkHref = xUrl(String.Format("GroupID{0}", ModuleId), GroupItem.ID.ToString(), "ManagerList",string.Format("Status={0}", ArticleStatus));
                GroupString.AppendFormat("<a href=\"{0}\" class=\"btn btn-default {1}\">{2}</a>", LinkHref, activeClass, GroupItem.Name).AppendLine();

            }

            liGroups.Text = GroupString.ToString();


        }



        #endregion


        #region "事件"

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindDataList();
                    BindPageItem();


                    if (Setting_EffectDB.Groups)
                    {
                        BindGroupList();
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }

        }

        /// <summary>
        /// 列表行创建
        /// </summary>
        protected void gvArticleList_RowCreated(object sender, GridViewRowEventArgs e)
        {

            Int32 DataIDX = 0;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //增加check列头全选
                TableCell cell = new TableCell();
                cell.Width = Unit.Pixel(5);
                cell.Text = "<label> <input id='CheckboxAll' value='0' type='checkbox' class='input_text' onclick='SelectAll()'/></label>";
                e.Row.Cells.AddAt(0, cell);


                foreach (TableCell var in e.Row.Cells)
                {
                    if (var.Controls.Count > 0 && var.Controls[0] is LinkButton)
                    {
                        string Colume = ((LinkButton)var.Controls[0]).CommandArgument;
                        if (Colume == Orderfld)
                        {
                            LinkButton l = (LinkButton)var.Controls[0];
                            l.Text += string.Format("<i class=\"fa {0}{1}\"></i>", Orderfld == "Title" ? "fa-sort-alpha-" : "fa-sort-amount-", (OrderType == 0) ? "asc" : "desc");
                        }
                    }
                }

            }
            else
            {
                //增加行选项
                DataIDX = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID"));
                TableCell cell = new TableCell();
                cell.Width = Unit.Pixel(5);
                cell.Text = string.Format("<label> <input name='Checkbox' id='Checkbox' value='{0}' type='checkbox' type-item=\"true\" class=\"input_text\" /></label>", DataIDX);
                e.Row.Cells.AddAt(0, cell);

            }


        }

        /// <summary>
        /// 列表行绑定
        /// </summary>
        protected void gvArticleList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //还原出数据
                DNNGo_DNNGalleryProGame_Slider Article = e.Row.DataItem as DNNGo_DNNGalleryProGame_Slider;

                if (Article != null && Article.ID > 0)
                {

                    HyperLink hlEdit = e.Row.FindControl("hlEdit") as HyperLink;
                    HyperLink hlMobileEdit = e.Row.FindControl("hlMobileEdit") as HyperLink;
                    LinkButton btnRemove = e.Row.FindControl("btnRemove") as LinkButton;
                    LinkButton btnMobileRemove = e.Row.FindControl("btnMobileRemove") as LinkButton;

                    //移动分类按钮
                    LinkButton lbSortUp = e.Row.FindControl("lbSortUp") as LinkButton;
                    LinkButton lbSortDown = e.Row.FindControl("lbSortDown") as LinkButton;
                    LinkButton lbMobileSortUp = e.Row.FindControl("lbMobileSortUp") as LinkButton;
                    LinkButton lbMobileSortDown = e.Row.FindControl("lbMobileSortDown") as LinkButton;
                    lbSortUp.CommandArgument =
                         lbSortDown.CommandArgument =
                          lbMobileSortUp.CommandArgument =
                           lbMobileSortDown.CommandArgument = Article.ID.ToString();
                     
                    //设置按钮的CommandArgument
                    btnRemove.CommandArgument = btnMobileRemove.CommandArgument = Article.ID.ToString();
                    //设置删除按钮的提示
                    if (Article.Status == (Int32)EnumStatus.RecycleBin)
                    {
                        btnRemove.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteItem") + "');");
                        btnMobileRemove.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteItem") + "');");
                    }
                    else
                    {
                        btnRemove.Attributes.Add("onClick", "javascript:return confirm('" + ViewResourceText("DeleteRecycleItem", "Are you sure to move it to recycle bin?") + "');");
                        btnMobileRemove.Attributes.Add("onClick", "javascript:return confirm('" + ViewResourceText("DeleteRecycleItem", "Are you sure to move it to recycle bin?") + "');");
                    }

                    hlEdit.NavigateUrl = hlMobileEdit.NavigateUrl = xUrl("ID", Article.ID.ToString(), "AddNew");
 

                    //文章状态
                    e.Row.Cells[7].Text = EnumHelper.GetEnumTextVal(Article.Status, typeof(EnumStatus));

                    //格式化2种时间为短日期格式
                    e.Row.Cells[5].Text = Article.StartTime.ToShortDateString();
                    e.Row.Cells[6].Text = Article.EndTime.ToShortDateString();

                    //显示分组
                    if (Setting_EffectDB.Groups)
                    {
                        e.Row.Cells[4].Text = DNNGo_DNNGalleryProGame_Group.FindGroupsBySliderID(Article.ID);
                    }

                    if (!String.IsNullOrEmpty(Article.Options))
                    {
                        if (gvArticleList.Columns[1].Visible || gvArticleList.Columns[2].Visible)
                        {

                            List<KeyValueEntity> kvList = new List<KeyValueEntity>();
                            try
                            {
                                kvList = ConvertTo.Deserialize<List<KeyValueEntity>>(Article.Options);

                            }
                            catch
                            { }


                            if(kvList!= null && kvList.Count >0)
                            {
                                if (kvList.Exists(r => r.Key == "Title"))
                                {
                                    KeyValueEntity rowEntity = kvList.Find(r => r.Key == "Title");
                                    if (rowEntity != null && !String.IsNullOrEmpty(rowEntity.Key))
                                    {
                                        e.Row.Cells[2].Text = Convert.ToString(rowEntity.Value);
                                    }

                                }

                                TemplateFormat tf = new TemplateFormat(this);
                                Image imgPicture = e.Row.FindControl("imgPicture") as Image;
                                if (kvList.Exists(r => r.Key == "Thumbnails"))
                                {
                                    KeyValueEntity rowEntity = kvList.Find(r => r.Key == "Thumbnails");
                                    if (rowEntity != null && !String.IsNullOrEmpty(rowEntity.Key))
                                    {
                                        imgPicture.ImageUrl = tf.ViewLinkUrl(Convert.ToString(rowEntity.Value));
                                    }

                                }
                                else if (kvList.Exists(r => r.Key == "Picture"))
                                {
                                    KeyValueEntity rowEntity = kvList.Find(r => r.Key == "Picture");
                                    if (rowEntity != null && !String.IsNullOrEmpty(rowEntity.Key))
                                    {
                                        imgPicture.ImageUrl = tf.ViewLinkUrl(Convert.ToString(rowEntity.Value));
                                    }
                                }
                                else
                                {
                                    imgPicture.Visible = false;
                                }


                            }

                            
                        }

                    }
 
                }
            }
        }

        /// <summary>
        /// 列表排序
        /// </summary>
        protected void gvArticleList_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (Orderfld == e.SortExpression)
            {
                if (OrderType == 0)
                {
                    OrderType = 1;
                }
                else
                {
                    OrderType = 0;
                }
            }
            Orderfld = e.SortExpression;
            //BindDataList();
            Response.Redirect(CurrentUrl);
        }


        /// <summary>
        /// 列表上的项删除事件
        /// </summary>
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton btnRemove = (LinkButton)sender;

                if (btnRemove != null && !String.IsNullOrEmpty(btnRemove.CommandArgument))
                {

                    mTips.IsPostBack = true;

                    DNNGo_DNNGalleryProGame_Slider Article = DNNGo_DNNGalleryProGame_Slider.FindByKeyForEdit(btnRemove.CommandArgument);

                    if (Article != null && Article.ID > 0)
                    {

                        if (Article.Status == (Int32)EnumStatus.RecycleBin)
                        {
                            if (Article.Delete() > 0)
                            {
                                //删除相关的的层
                                DNNGo_DNNGalleryProGame_Layer.Deletes(Article);
 
                                //操作成功
                                mTips.LoadMessage("DeleteGallerySuccess", EnumTips.Success, this);
                            }
                            else
                            {
                                //操作失败
                                mTips.LoadMessage("DeleteGalleryError", EnumTips.Success, this);
                            }
                        }
                        else
                        {
                            Article.Status = (Int32)EnumStatus.RecycleBin;
                            if (Article.Update() > 0)
                            {
                                //移动到回收站操作成功
                                mTips.LoadMessage("DeleteGallerySuccess", EnumTips.Success, this);
                            }
                            else
                            {
                                //移动到回收站操作失败
                                mTips.LoadMessage("DeleteGalleryError", EnumTips.Success, this);
                            }
                        }

                        BindDataList();
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }


        /// <summary>
        /// 搜索按钮事件
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Search_Title = HttpUtility.UrlEncode(txtSearch.Text.Trim());
                Response.Redirect(CurrentUrl, false);
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }

        /// <summary>
        /// 状态应用按钮事件
        /// </summary>
        protected void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 Status = WebHelper.GetIntParam(Request, ddlStatus.UniqueID, -1);

                if (Status >= 0)
                {
                    string Checkbox_Value = WebHelper.GetStringParam(Request, "Checkbox", "");
                    string[] Checkbox_Value_Array = Checkbox_Value.Split(',');
                    Int32 IDX = 0;
                    for (int i = 0; i < Checkbox_Value_Array.Length; i++)
                    {
                        if (Int32.TryParse(Checkbox_Value_Array[i], out IDX))
                        {
                            DNNGo_DNNGalleryProGame_Slider Article = DNNGo_DNNGalleryProGame_Slider.FindByKeyForEdit(IDX);
                            if (Article != null && Article.ID > 0)
                            {

                                if (Article.Status == (Int32)EnumStatus.RecycleBin && Status == (Int32)EnumStatus.RecycleBin)
                                {
                                    if (Article.Delete() > 0)
                                    {
                                        //删除相关的的层
                                        DNNGo_DNNGalleryProGame_Layer.Deletes(Article);
                                    }
                                }
                                else
                                {
                                    Article.Status = Status;
                                    if (Article.Update() > 0)
                                    {
                                    }
                                }
                            }
                        }
                    }
                    BindDataList();

                    mTips.IsPostBack = true;
                    mTips.LoadMessage("ApplyStatusSuccess", EnumTips.Success, this, new String[] { EnumHelper.GetEnumTextVal(Status, typeof(EnumStatus)) });
                }
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }


        protected void lbSort_Click(object sender, EventArgs e)
        {
            LinkButton ImgbutSort = (LinkButton)sender;
            if (ImgbutSort != null)
            {
                //查出当前要排序的字段
                DNNGo_DNNGalleryProGame_Slider objC = DNNGo_DNNGalleryProGame_Slider.FindByKeyForEdit(ImgbutSort.CommandArgument);

                mTips.IsPostBack = true;//回发时就要触发
                if (ImgbutSort.ToolTip == "up")
                {
                    DNNGo_DNNGalleryProGame_Slider.MoveField(objC, EnumMoveType.Up, ModuleId, ArticleStatus);
                    //字段上移成功
                    mTips.LoadMessage("UpMoveGallerySuccess", EnumTips.Success, this, new String[] { "" });

                }
                else
                {
                    DNNGo_DNNGalleryProGame_Slider.MoveField(objC, EnumMoveType.Down, ModuleId, ArticleStatus);
                    //字段下移成功

                    mTips.LoadMessage("DownMoveGallerySuccess", EnumTips.Success, this, new String[] { "" });
                }
                //绑定一下
                BindDataList();
            }


        }


        #endregion



















    }
}