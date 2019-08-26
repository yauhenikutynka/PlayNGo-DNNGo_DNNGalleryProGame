using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DNNGo.Modules.DNNGalleryProGame
{
	/// <summary>
	/// 画廊内容
	/// </summary>
	public partial class DNNGo_DNNGalleryProGame_Slider : Entity<DNNGo_DNNGalleryProGame_Slider>
	{
        #region 对象操作
        //基类Entity中包含三个对象操作：Insert、Update、Delete
        //你可以重载它们，以改变它们的行为
        //如：
        /*
		/// <summary>
		/// 已重载。把该对象插入到数据库。这里可以做数据插入前的检查
		/// </summary>
		/// <returns>影响的行数</returns>
		public override Int32 Insert()
		{
			return base.Insert();
		}
		 * */
        #endregion

        #region 扩展属性
        //TODO: 本类与哪些类有关联，可以在这里放置一个属性，使用延迟加载的方式获取关联对象

        /*
		private Category _Category;
		/// <summary>该商品所对应的类别</summary>
		public Category Category
		{
			get
			{
				if (_Category == null && CategoryID > 0 && !Dirtys.ContainKey("Category"))
				{
					_Category = Category.FindByKey(CategoryID);
					Dirtys.Add("Category", true);
				}
				return _Category;
			}
			set { _Category = value; }
		}
		 * */



        private List<DNNGo_DNNGalleryProGame_Layer> _LayerList = new List<DNNGo_DNNGalleryProGame_Layer>();
        /// <summary>
        /// ceng列表
        /// </summary>
        public List<DNNGo_DNNGalleryProGame_Layer> LayerList
        {
            get
            {
                if (!(_LayerList != null && _LayerList.Count > 0) && ID > 0)
                {
                    _LayerList = DNNGo_DNNGalleryProGame_Layer.FindAllBySliderID(ID);
                }
                return _LayerList;
            }
        }

        private List<DNNGo_DNNGalleryProGame_Group> _GroupList = new List<DNNGo_DNNGalleryProGame_Group>();
        /// <summary>
        /// 分组列表
        /// </summary>
        public List<DNNGo_DNNGalleryProGame_Group> GroupList
        {
            get
            {
                if (!(_GroupList != null && _GroupList.Count > 0) && ID > 0)
                {
                    _GroupList = DNNGo_DNNGalleryProGame_Group.FindAllBySliderID(ID);
                }
                return _GroupList;
            }
        }





        #endregion

        #region 扩展查询

        /// <summary>
        /// 根据方案编号查询该方案下所有的字段
        /// </summary>
        /// <param name="ProjectID">方案编号</param>
        /// <returns></returns>
        public static List<DNNGo_DNNGalleryProGame_Slider> FindAllByModuleID(object ModuleID,Int32 Status = -1)
        {
            QueryParam qp = new QueryParam();
            int RecordCount = 0;
            qp.Orderfld = _.Sort;
            qp.OrderType = 0;
            qp.Where.Add(new SearchParam(_.ModuleId, ModuleID, SearchType.Equal));
            if (Status > -1)
            {
                qp.Where.Add(new SearchParam(_.Status, Status, SearchType.Equal));
            }


            //  qp.Where.Add(new SearchParam(DNNGo_xForm_ExtendField._.IsDelete.ColumnName, (Int32)EnumIsDelete.Normal, SearchType.Equal));
            return FindAll(qp, out RecordCount);
        }

		/// <summary>
		/// 根据编号查找
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static DNNGo_DNNGalleryProGame_Slider FindByID(Int32 id)
		{
			return Find(_.ID, id);
			// 实体缓存
			//return Meta.Cache.Entities.Find(_.ID, id);
			// 单对象缓存
			//return Meta.SingleCache[id];
		}
		#endregion

		#region 高级查询
		/// <summary>
		/// 查询满足条件的记录集，分页、排序
		/// </summary>
		/// <param name="key">关键字</param>
		/// <param name="orderClause">排序，不带Order By</param>
		/// <param name="startRowIndex">开始行，0开始</param>
		/// <param name="maximumRows">最大返回行数</param>
		/// <returns>实体集</returns>
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static List<DNNGo_DNNGalleryProGame_Slider> Search(String key, String orderClause, Int32 startRowIndex, Int32 maximumRows)
		{
		    return FindAll(SearchWhere(key), orderClause, null, startRowIndex, maximumRows);
		}

		/// <summary>
		/// 查询满足条件的记录总数，分页和排序无效，带参数是因为ObjectDataSource要求它跟Search统一
		/// </summary>
		/// <param name="key">关键字</param>
		/// <param name="orderClause">排序，不带Order By</param>
		/// <param name="startRowIndex">开始行，0开始</param>
		/// <param name="maximumRows">最大返回行数</param>
		/// <returns>记录数</returns>
		public static Int32 SearchCount(String key, String orderClause, Int32 startRowIndex, Int32 maximumRows)
		{
		    return FindCount(SearchWhere(key), null, null, 0, 0);
		}

		/// <summary>
		/// 构造搜索条件
		/// </summary>
		/// <param name="key">关键字</param>
		/// <returns></returns>
		private static String SearchWhere(String key)
		{
            if (String.IsNullOrEmpty(key)) return null;
            key = key.Replace("'", "''");
            String[] keys = key.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

		    StringBuilder sb = new StringBuilder();
		    sb.Append("1=1");

            //if (!String.IsNullOrEmpty(name)) sb.AppendFormat(" And {0} like '%{1}%'", _.Name, name.Replace("'", "''"));

            for (int i = 0; i < keys.Length; i++)
            {
                sb.Append(" And ");

                if (keys.Length > 1) sb.Append("(");
                Int32 n = 0;
                foreach (FieldItem item in Meta.Fields)
                {
                    if (item.Property.PropertyType != typeof(String)) continue;
                    // 只要前五项
                    if (++n > 5) break;

                    if (n > 1) sb.Append(" Or ");
                    sb.AppendFormat("{0} like '%{1}%'", item.Name, keys[i]);
                }
                if (keys.Length > 1) sb.Append(")");
            }

            if (sb.Length == "1=1".Length)
                return null;
            else
                return sb.ToString();
		}






        /// <summary>
        /// 根据状态统计数量
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static Int32 FindCountByStatus(Int32 ModuleId, Int32 Status)
        {
            QueryParam qp = new QueryParam();
            qp.Where.Add(new SearchParam(_.ModuleId, ModuleId, SearchType.Equal));
            if (Status >= 0)
            {
                qp.Where.Add(new SearchParam(_.Status, Status, SearchType.Equal));
            }

            return FindCount(qp);
        }


        
        public static DNNGo_DNNGalleryProGame_Slider FindItemByFriendlyUrl(String FriendlyUrl, Int32 ModuleId)
        {
            int RecordCount = 0;
            QueryParam qp = new QueryParam();
            qp.Where.Add(new SearchParam(_.ModuleId, ModuleId, SearchType.Equal));
            qp.Where.Add(new SearchParam(_.Status, (Int32)EnumStatus.Activated, SearchType.Equal));

            qp.Where.Add(new SearchParam(_.FriendlyUrl, FriendlyUrl, SearchType.Equal));

            return FindItem(qp, out RecordCount);
        }


        #endregion

        #region 扩展操作

        /// <summary>
        /// 移动字段
        /// </summary>
        /// <param name="objTab">待移动的字段</param>
        /// <param name="type">移动类型</param>
        /// <param name="ProjectID"></param>
        public static void MoveField(DNNGo_DNNGalleryProGame_Slider objTab, EnumMoveType type, object ModuleID,Int32 Status)
        {
            List<DNNGo_DNNGalleryProGame_Slider> siblingTabs = FindAllByModuleID(ModuleID, Status); 
            int siblingCount = siblingTabs.Count;
            int tabIndex = 0;
            UpdateTabOrder(siblingTabs, 2);
            switch (type)
            {
                case EnumMoveType.Up:
                    tabIndex = GetIndexOfTab(objTab, siblingTabs);
                    if (tabIndex > 0)
                    {
                        DNNGo_DNNGalleryProGame_Slider swapTab = siblingTabs[tabIndex - 1];
                        SwapAdjacentTabs(objTab, swapTab);
                    }
                    break;
                case EnumMoveType.Down:
                    tabIndex = GetIndexOfTab(objTab, siblingTabs);
                    if (tabIndex < siblingCount - 1)
                    {
                        DNNGo_DNNGalleryProGame_Slider swapTab = siblingTabs[tabIndex + 1];
                        SwapAdjacentTabs(swapTab, objTab);
                    }
                    break;
            }

        }

        private static void SwapAdjacentTabs(DNNGo_DNNGalleryProGame_Slider firstTab, DNNGo_DNNGalleryProGame_Slider secondTab)
        {
            firstTab.Sort -= 2;
            firstTab.Update();
            secondTab.Sort += 2;
            secondTab.Update();
        }


        private static void UpdateTabOrder(List<DNNGo_DNNGalleryProGame_Slider> tabs, int increment)
        {
            int tabOrder = 1;
            for (int index = 0; index <= tabs.Count - 1; index++)
            {
                DNNGo_DNNGalleryProGame_Slider objTab = tabs[index];
                objTab.Sort = tabOrder;
                objTab.Update();
                tabOrder += increment;
            }
        }


        private static void UpdateTabOrder(List<DNNGo_DNNGalleryProGame_Slider> tabs, int startIndex, int endIndex, int increment)
        {
            for (int index = startIndex; index <= endIndex; index++)
            {
                DNNGo_DNNGalleryProGame_Slider objTab = tabs[index];
                objTab.Sort += increment;
                objTab.Update();
            }
        }

        private static int GetIndexOfTab(DNNGo_DNNGalleryProGame_Slider objTab, List<DNNGo_DNNGalleryProGame_Slider> tabs)
        {
            int tabIndex = -1;// Null.NullInteger;
            for (int index = 0; index <= tabs.Count - 1; index++)
            {
                if (tabs[index].ID == objTab.ID)
                {
                    tabIndex = index;
                    break;
                }
            }
            return tabIndex;
        }
		#endregion

		#region 业务
		#endregion
	}
}