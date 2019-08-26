using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using System.Data;

 
 
 

namespace DNNGo.Modules.DNNGalleryProGame
{
 
    
    /// <summary>分组数据</summary>
    public partial class DNNGo_DNNGalleryProGame_Group : Entity<DNNGo_DNNGalleryProGame_Group> 
    {
        #region 对象操作
        static DNNGo_DNNGalleryProGame_Group()
        {
            // 用于引发基类的静态构造函数，所有层次的泛型实体类都应该有一个
            DNNGo_DNNGalleryProGame_Group entity = new DNNGo_DNNGalleryProGame_Group();
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
        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}标签数据数据……", typeof(DNNGo_DNNGalleryProGame_Group).Name);

        //    DNNGo_DNNGalleryProGame_Group user = new DNNGo_DNNGalleryProGame_Group();
        //    user.Name = "admin";
        //    user.Password = DataHelper.Hash("admin");
        //    user.DisplayName = "管理员";
        //    user.RoleID = 1;
        //    user.IsEnable = true;
        //    user.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}标签数据数据！", typeof(DNNGo_DNNGalleryProGame_Group).Name);
        //}
        #endregion

        #region 扩展属性
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DNNGo_DNNGalleryProGame_Group FindByID(Int32 id)
        {
            return Find(_.ID, id);
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="SliderID"></param>
        /// <returns></returns>
        public static List<DNNGo_DNNGalleryProGame_Group> FindAllBySliderID(Int32 SliderID)
        {
            QueryParam qp = new QueryParam();
            int RecordCount = 0;
            String IDs = String.Format("SELECT {0} FROM {1} WHERE {2} = {3}",
                DNNGo_DNNGalleryProGame_Slider_Group._.GroupID,
                DNNGo_DNNGalleryProGame_Slider_Group.Meta.TableName,
                DNNGo_DNNGalleryProGame_Slider_Group._.SliderID, SliderID);
            qp.Where.Add(new SearchParam(DNNGo_DNNGalleryProGame_Group._.ID, IDs, SearchType.In));
            return DNNGo_DNNGalleryProGame_Group.FindAll(qp, out RecordCount);

        }


        public static String FindGroupStringBySliderID(Int32 SliderID)
        {
            StringBuilder sb = new StringBuilder();

            List<DNNGo_DNNGalleryProGame_Group> list = FindAllBySliderID(SliderID);
            if (list != null && list.Count > 0)
            {
                foreach (DNNGo_DNNGalleryProGame_Group item in list)
                {
                    if (!String.IsNullOrEmpty(item.Name))
                    {
                        sb.AppendFormat(" {0}", item.Name.Replace(" ", "").Replace(",", " "));
                    }
                }
            }


            return sb.ToString();
        }

        public static String FindGroupsBySliderID(Int32 SliderID)
        {
            StringBuilder sb = new StringBuilder();

            List<DNNGo_DNNGalleryProGame_Group> list = FindAllBySliderID(SliderID);
            if (list != null && list.Count > 0)
            {
                foreach (DNNGo_DNNGalleryProGame_Group item in list)
                {
                    if (!String.IsNullOrEmpty(item.Name))
                    {
                        sb.AppendFormat("{0},", item.Name);
                    }
                }
            }


            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 根据方案编号查询该方案下所有的字段
        /// </summary>
        /// <param name="ProjectID">方案编号</param>
        /// <returns></returns>
        public static List<DNNGo_DNNGalleryProGame_Group> FindAllByModuleID(object ModuleID)
        {
            QueryParam qp = new QueryParam();
            int RecordCount = 0;
            qp.Orderfld = _.Sort;
            qp.OrderType = 0;
            qp.Where.Add(new SearchParam(_.ModuleId, ModuleID, SearchType.Equal));
            //  qp.Where.Add(new SearchParam(DNNGo_xForm_ExtendField._.IsDelete.ColumnName, (Int32)EnumIsDelete.Normal, SearchType.Equal));
            return FindAll(qp, out RecordCount);
        }

        #endregion



        #region 扩展操作

        



        /// <summary>
        /// 移动字段
        /// </summary>
        /// <param name="objTab">待移动的字段</param>
        /// <param name="type">移动类型</param>
        /// <param name="ProjectID"></param>
        public static void MoveField(DNNGo_DNNGalleryProGame_Group objTab, EnumMoveType type, object ModuleID)
        {
            List<DNNGo_DNNGalleryProGame_Group> siblingTabs = FindAllByModuleID(ModuleID);
            int siblingCount = siblingTabs.Count;
            int tabIndex = 0;
            UpdateTabOrder(siblingTabs, 2);
            switch (type)
            {
                case EnumMoveType.Up:
                    tabIndex = GetIndexOfTab(objTab, siblingTabs);
                    if (tabIndex > 0)
                    {
                        DNNGo_DNNGalleryProGame_Group swapTab = siblingTabs[tabIndex - 1];
                        SwapAdjacentTabs(objTab, swapTab);
                    }
                    break;
                case EnumMoveType.Down:
                    tabIndex = GetIndexOfTab(objTab, siblingTabs);
                    if (tabIndex < siblingCount - 1)
                    {
                        DNNGo_DNNGalleryProGame_Group swapTab = siblingTabs[tabIndex + 1];
                        SwapAdjacentTabs(swapTab, objTab);
                    }
                    break;
            }

        }

        private static void SwapAdjacentTabs(DNNGo_DNNGalleryProGame_Group firstTab, DNNGo_DNNGalleryProGame_Group secondTab)
        {
            firstTab.Sort -= 2;
            firstTab.Update();
            secondTab.Sort += 2;
            secondTab.Update();
        }


        private static void UpdateTabOrder(List<DNNGo_DNNGalleryProGame_Group> tabs, int increment)
        {
            int tabOrder = 1;
            for (int index = 0; index <= tabs.Count - 1; index++)
            {
                DNNGo_DNNGalleryProGame_Group objTab = tabs[index];
                objTab.Sort = tabOrder;
                objTab.Update();
                tabOrder += increment;
            }
        }


        private static void UpdateTabOrder(List<DNNGo_DNNGalleryProGame_Group> tabs, int startIndex, int endIndex, int increment)
        {
            for (int index = startIndex; index <= endIndex; index++)
            {
                DNNGo_DNNGalleryProGame_Group objTab = tabs[index];
                objTab.Sort += increment;
                objTab.Update();
            }
        }

        private static int GetIndexOfTab(DNNGo_DNNGalleryProGame_Group objTab, List<DNNGo_DNNGalleryProGame_Group> tabs)
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