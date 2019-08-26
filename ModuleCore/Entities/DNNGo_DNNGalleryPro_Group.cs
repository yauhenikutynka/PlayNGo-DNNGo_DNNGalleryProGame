using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace DNNGo.Modules.DNNGalleryProGame
{
	/// <summary>
	/// 分组
	/// </summary>
	[Serializable]
	[DataObject]
	[Description("分组")]
	[BindTable("DNNGo_DNNGalleryProGame_Group", Description = "分组", ConnName = "SiteSqlServer")]
	public partial class DNNGo_DNNGalleryProGame_Group : Entity<DNNGo_DNNGalleryProGame_Group>
	{
		#region 属性
		private Int32 _ID;
		/// <summary>
		/// 分组编号
		/// </summary>
		[Description("分组编号")]
		[DataObjectField(true, true, false, 10)]
		[BindColumn("ID", Description = "分组编号", DefaultValue = "", Order = 1)]
		public Int32 ID
		{
			get { return _ID; }
			set { if (OnPropertyChange("ID", value)) _ID = value; }
		}

		private String _Name;
		/// <summary>
		/// 组名
		/// </summary>
		[Description("组名")]
		[DataObjectField(false, false, false, 100)]
		[BindColumn("Name", Description = "组名", DefaultValue = "", Order = 2)]
		public String Name
		{
			get { return _Name; }
			set { if (OnPropertyChange("Name", value)) _Name = value; }
		}

		private String _Description;
		/// <summary>
		/// 描述
		/// </summary>
		[Description("描述")]
		[DataObjectField(false, false, true, 512)]
		[BindColumn("Description", Description = "描述", DefaultValue = "", Order = 3)]
		public String Description
		{
			get { return _Description; }
			set { if (OnPropertyChange("Description", value)) _Description = value; }
		}

		private Int32 _QuoteCount;
		/// <summary>
		/// 引用数量
		/// </summary>
		[Description("引用数量")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("QuoteCount", Description = "引用数量", DefaultValue = "", Order = 4)]
		public Int32 QuoteCount
		{
			get { return _QuoteCount; }
			set { if (OnPropertyChange("QuoteCount", value)) _QuoteCount = value; }
		}

		private Int32 _Sort;
		/// <summary>
		/// 排序
		/// </summary>
		[Description("排序")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("Sort", Description = "排序", DefaultValue = "", Order = 5)]
		public Int32 Sort
		{
			get { return _Sort; }
			set { if (OnPropertyChange("Sort", value)) _Sort = value; }
		}

		private Int32 _ModuleId;
		/// <summary>
		/// 模块编号
		/// </summary>
		[Description("模块编号")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("ModuleId", Description = "模块编号", DefaultValue = "", Order = 6)]
		public Int32 ModuleId
		{
			get { return _ModuleId; }
			set { if (OnPropertyChange("ModuleId", value)) _ModuleId = value; }
		}

		private Int32 _PortalId;
		/// <summary>
		/// 
		/// </summary>
		[Description("")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("PortalId", Description = "", DefaultValue = "", Order = 7)]
		public Int32 PortalId
		{
			get { return _PortalId; }
			set { if (OnPropertyChange("PortalId", value)) _PortalId = value; }
		}


        private String _Options;
        /// <summary>选项集合</summary>
        [DisplayName("选项集合")]
        [Description("选项集合")]
        [DataObjectField(false, false, true, 1073741823)]
        [BindColumn(15, "Options", "选项集合", null, "ntext", 0, 0, true)]
        public virtual String Options
        {
            get { return _Options; }
            set { if (OnPropertyChange("Options", value)) { _Options = value; } }
        }

        #endregion

        #region 获取/设置 字段值
        /// <summary>
        /// 获取/设置 字段值。
        /// 一个索引，基类使用反射实现。
        /// 派生实体类可重写该索引，以避免反射带来的性能损耗
        /// </summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
		{
			get
			{
				switch (name)
				{
					case "ID" : return _ID;
					case "Name" : return _Name;
					case "Description" : return _Description;
					case "QuoteCount" : return _QuoteCount;
					case "Sort" : return _Sort;
					case "ModuleId" : return _ModuleId;
					case "PortalId" : return _PortalId;
                    case "Options": return _Options;
                    default: return base[name];
				}
			}
			set
			{
				switch (name)
				{
					case "ID" : _ID = Convert.ToInt32(value); break;
					case "Name" : _Name = Convert.ToString(value); break;
					case "Description" : _Description = Convert.ToString(value); break;
					case "QuoteCount" : _QuoteCount = Convert.ToInt32(value); break;
					case "Sort" : _Sort = Convert.ToInt32(value); break;
					case "ModuleId" : _ModuleId = Convert.ToInt32(value); break;
					case "PortalId" : _PortalId = Convert.ToInt32(value); break;
                    case "Options": _Options = Convert.ToString(value); break;
                    default: base[name] = value; break;
				}
			}
		}
		#endregion

		#region 字段名
		/// <summary>
		/// 取得分组字段名的快捷方式
		/// </summary>
		public class _
		{
			///<summary>
			/// 分组编号
			///</summary>
			public const String ID = "ID";

			///<summary>
			/// 组名
			///</summary>
			public const String Name = "Name";

			///<summary>
			/// 描述
			///</summary>
			public const String Description = "Description";

			///<summary>
			/// 引用数量
			///</summary>
			public const String QuoteCount = "QuoteCount";

			///<summary>
			/// 排序
			///</summary>
			public const String Sort = "Sort";

			///<summary>
			/// 模块编号
			///</summary>
			public const String ModuleId = "ModuleId";

			///<summary>
			/// 
			///</summary>
			public const String PortalId = "PortalId";

            ///<summary>选项集合</summary>
            public const String Options = ("Options");
        }
		#endregion
	}
}