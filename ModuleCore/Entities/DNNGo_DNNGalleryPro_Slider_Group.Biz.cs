using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

 
 
 


namespace DNNGo.Modules.DNNGalleryProGame
{
 
    
    /// <summary>分类文章关系</summary>
    public partial class DNNGo_DNNGalleryProGame_Slider_Group : Entity<DNNGo_DNNGalleryProGame_Slider_Group> 
    {
        #region 对象操作
        static DNNGo_DNNGalleryProGame_Slider_Group()
        {
            // 用于引发基类的静态构造函数，所有层次的泛型实体类都应该有一个
            DNNGo_DNNGalleryProGame_Slider_Group entity = new DNNGo_DNNGalleryProGame_Slider_Group();
        }

        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnInsert()
        //{
        //    return base.OnInsert();
        //}

        ///// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        ///// <param name="isNew"></param>
        //public override void Valid(Boolean isNew)
        //{
        //    // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
        //    base.Valid(isNew);

        //    // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
        //    if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException(_.Name, _.Name.Description + "无效！");
        //    if (!isNew && ID < 1) throw new ArgumentOutOfRangeException(_.ID, _.ID.Description + "必须大于0！");

        //    // 在新插入数据或者修改了指定字段时进行唯一性验证，CheckExist内部抛出参数异常
        //    if (isNew || Dirtys[_.Name]) CheckExist(_.Name);
        //    if (isNew || Dirtys[_.Name] || Dirtys[_.DbType]) CheckExist(_.Name, _.DbType);
        //    if ((isNew || Dirtys[_.Name]) && Exist(_.Name)) throw new ArgumentException(_.Name, "值为" + Name + "的" + _.Name.Description + "已存在！");
        //}


        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    base.InitData();

        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    // Meta.Count是快速取得表记录数
        //    if (Meta.Count > 0) return;

        //    // 需要注意的是，如果该方法调用了其它实体类的首次数据库操作，目标实体类的数据初始化将会在同一个线程完成
        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}分类文章关系数据……", typeof(DNNGo_DNNGalleryProGame_Slider_Group).Name);

        //    DNNGo_DNNGalleryProGame_Slider_Group user = new DNNGo_DNNGalleryProGame_Slider_Group();
        //    user.Name = "admin";
        //    user.Password = DataHelper.Hash("admin");
        //    user.DisplayName = "管理员";
        //    user.RoleID = 1;
        //    user.IsEnable = true;
        //    user.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}分类文章关系数据！", typeof(DNNGo_DNNGalleryProGame_Slider_Group).Name);
        //}

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="ArticleID"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public static Int32 Initialization( Int32 ArticleID, Int32 CategoryID)
        {
            DNNGo_DNNGalleryProGame_Slider_Group ArticleItem = new DNNGo_DNNGalleryProGame_Slider_Group();
            ArticleItem.SliderID = ArticleID;
            ArticleItem.GroupID = CategoryID;
            return ArticleItem.Insert();
        }


        #endregion

        #region 扩展属性
        #endregion

        #region 扩展查询
        /// <summary>根据关系编号查找</summary>
        /// <param name="id">关系编号</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DNNGo_DNNGalleryProGame_Slider_Group FindByID(Int32 id)
        {
            return Find(_.ID, id);
        }

        /// <summary>
        /// 根据文章编号查找对象的分类关系
        /// </summary>
        /// <param name="SliderID">内容编号</param>
        /// <returns></returns>
        public static List<DNNGo_DNNGalleryProGame_Slider_Group> FindAllBySliderID(Int32 SliderID)
        {
            return FindAll(_.SliderID, SliderID);
        }

        public static List<DNNGo_DNNGalleryProGame_Slider_Group> FindAllByArticleID(int ArticleID)
        {




            return Entity<DNNGo_DNNGalleryProGame_Slider_Group>.FindAll("SliderID", ArticleID);
        }

 


        /// <summary>
        /// 组装按分类编号查文章语句
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public static String BuilderArticleIDs(Int32 GroupID)
        {
            //查询分类及下级分类编号

            if (GroupID > 0)
            {
                return String.Format("select {0} from {1} where GroupID = {2}",_.SliderID, Meta.TableName, GroupID);
            }
            return String.Empty;

        }



        public static String BuilderSliderIDsBySliderID(Int32 SliderID)
        {
            String GroupSQL = String.Format("SELECT {0} FROM {1} WHERE {2}", _.GroupID, Meta.TableName, new SearchParam(_.SliderID, SliderID, SearchType.Equal).ToSql());

            return String.Format("SELECT {0} FROM {1} WHERE {2}", _.SliderID, Meta.TableName,new SearchParam(_.GroupID, GroupSQL, SearchType.In).ToSql());
        }


        #endregion



        #region 扩展操作

        /// <summary>
        /// 更新分类关系
        /// </summary>
        /// <param name="ArticleID">文章编号</param>
        /// <param name="CategoryIDs">分类编号字符串集合</param>
        /// <returns></returns>
        public static Int32 Update(Int32 ArticleID, String CategoryIDs)
        {
            //分类编号
            List<String> CategoryIDList = Common.GetList(CategoryIDs);
            return Update(ArticleID, CategoryIDList);
        }


        /// <summary>
        /// 更新分类关系
        /// </summary>
        /// <param name="ArticleID">文章编号</param>
        /// <param name="CategoryIDList">分类编号集合</param>
        /// <returns></returns>
        public static Int32 Update(Int32 ArticleID, List<String> CategoryIDList)
        {
            Int32 Result = 0;

            //查出当前文章关联的所有分类数据
            List<DNNGo_DNNGalleryProGame_Slider_Group> CategoryList = FindAllByArticleID(ArticleID);


            //制造临时变量
            String[] tempCategoryIDList = CategoryIDList.ToArray();
            CategoryIDList.CopyTo(tempCategoryIDList);
            DNNGo_DNNGalleryProGame_Slider_Group[] tempCategoryList = CategoryList.ToArray(); CategoryList.CopyTo(tempCategoryList);

            foreach (String CategoryID in tempCategoryIDList)
            {
                //对比当前分类
                foreach (DNNGo_DNNGalleryProGame_Slider_Group Category in tempCategoryList)
                {
                    if (Category.GroupID.ToString() == CategoryID)
                    {
                        //移除两者
                        CategoryList.Remove(Category);
                        CategoryIDList.Remove(CategoryID);
                    }
                }
            }

            //剩下的列表删除
            foreach (String CategoryID in CategoryIDList)
            {
                DNNGo_DNNGalleryProGame_Slider_Group Category = new DNNGo_DNNGalleryProGame_Slider_Group();
                Category.GroupID = Convert.ToInt32(CategoryID);
                Category.SliderID = ArticleID;
                if (Category.Insert() > 0)
                {
                    Result += 1;
                    UpdateCount(Category);
                }
            }

            //剩下的ID添加
            foreach (DNNGo_DNNGalleryProGame_Slider_Group Category in CategoryList)
            {
                if (Category.Delete() > 0)
                {
                    Result += 1;
                    UpdateCount(Category);

                }
            }



            return Result;
        }

        /// <summary>
        /// 更新分类类目下的数量
        /// </summary>
        /// <param name="Category">分类关系</param>
        public static void UpdateCount(DNNGo_DNNGalleryProGame_Slider_Group Category)
        {
            DNNGo_DNNGalleryProGame_Group CategoryItem = DNNGo_DNNGalleryProGame_Group.FindByID(Category.GroupID);
            if (CategoryItem != null && CategoryItem.ID > 0)
            {
                CategoryItem.QuoteCount = DNNGo_DNNGalleryProGame_Slider_Group.FindCount(DNNGo_DNNGalleryProGame_Slider_Group._.GroupID, Category.GroupID);
                CategoryItem.Update();
            }
        }


        /// <summary>
        /// 删除文章到分类的关系
        /// </summary>
        /// <param name="ArticleID">文章编号</param>
        /// <returns></returns>
        public static Int32 DeleteByArticleID(Int32 ArticleID)
        {
            Int32 Result = 0;
            //查出当前文章关联的所有分类数据
            List<DNNGo_DNNGalleryProGame_Slider_Group> CategoryList = FindAllByArticleID(ArticleID);

            foreach (DNNGo_DNNGalleryProGame_Slider_Group Category in CategoryList)
            {
                if (Category.Delete() > 0)
                {
                    Result += 1;
                }
            }
            return Result;
        }

        /// <summary>
        /// 根据分类编号集合查询文章编号集合
        /// </summary>
        /// <param name="CategoryIDs">分类编号集合</param>
        /// <returns></returns>
        public static String FindArticleIDsByCategoryIDs(String CategoryIDs)
        {
            QueryParam qp = new QueryParam();
            int RecordCount = 0;
            qp.Where.Add(new SearchParam(_.GroupID, CategoryIDs.Trim(','), SearchType.In));
            List<DNNGo_DNNGalleryProGame_Slider_Group> CategoryList =  FindAll(qp, out RecordCount);
            String ArticleIDs = String.Empty;

            foreach (DNNGo_DNNGalleryProGame_Slider_Group ArticleID in CategoryList)
            {
                ArticleIDs += String.Format("{0}{1}",ArticleIDs != String.Empty?",":"", ArticleID.SliderID);
            }
            return ArticleIDs;
        }
        

        #endregion

        #region 业务

        /// <summary>
        /// 插入单项
        /// </summary>
        /// <param name="ContentItem"></param>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        public static Int32 InsertItem(DNNGo_DNNGalleryProGame_Slider ContentItem, String GroupName)
        {
            int RecordCount = 0;

            if (!String.IsNullOrEmpty(GroupName))
            {
                List<String> GroupNames = Common.GetList(GroupName);

                if (GroupNames != null && GroupNames.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (String GroupString in GroupNames)
                    {
                        sb.AppendFormat("'{0}',", GroupString);
                    }
                   
                    QueryParam qp = new QueryParam();
                    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Group._.ModuleId, ContentItem.ModuleId, SearchType.Equal));
                    qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Group._.Name, sb.ToString().Trim(','), SearchType.In));

                    List<DNNGo_DNNGalleryProGame_Group> Groups = DNNGo_DNNGalleryProGame_Group.FindAll(qp, out RecordCount);

                    if (Groups != null && Groups.Count > 0)
                    {
                        RecordCount = 0;
                        foreach (DNNGo_DNNGalleryProGame_Group GroupItem in Groups)
                        {
                            DNNGo_DNNGalleryProGame_Slider_Group Content_Group = new DNNGo_DNNGalleryProGame_Slider_Group();
                            Content_Group.GroupID = GroupItem.ID;
                            Content_Group.SliderID = ContentItem.ID;
                            if (Content_Group.Insert() > 0)
                            {
                                RecordCount++;
                            }
                        }
                    }
                }
            }
            return RecordCount;
        }


        #endregion
    }
}