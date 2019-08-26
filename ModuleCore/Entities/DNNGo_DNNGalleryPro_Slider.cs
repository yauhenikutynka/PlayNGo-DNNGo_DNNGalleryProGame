using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace DNNGo.Modules.DNNGalleryProGame
{
	/// <summary>
	/// 背景
	/// </summary>
	[Serializable]
	[DataObject]
	[Description("背景")]
	[BindTable("DNNGo_DNNGalleryProGame_Slider", Description = "背景", ConnName = "SiteSqlServer")]
	public partial class DNNGo_DNNGalleryProGame_Slider : Entity<DNNGo_DNNGalleryProGame_Slider>
	{
		#region 属性
		private Int32 _ID;
		/// <summary>
		/// 背景编号
		/// </summary>
		[Description("背景编号")]
		[DataObjectField(true, true, false, 10)]
		[BindColumn("ID", Description = "背景编号", DefaultValue = "", Order = 1)]
		public Int32 ID
		{
			get { return _ID; }
			set { if (OnPropertyChange("ID", value)) _ID = value; }
		}

		private String _Options;
		/// <summary>
		/// 选项集合
		/// </summary>
		[Description("选项集合")]
		[DataObjectField(false, false, true, 1073741823)]
		[BindColumn("Options", Description = "选项集合", DefaultValue = "", Order = 2)]
		public String Options
		{
			get { return _Options; }
			set { if (OnPropertyChange("Options", value)) _Options = value; }
		}

		private Int32 _Sort;
		/// <summary>
		/// 排序
		/// </summary>
		[Description("排序")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("Sort", Description = "排序", DefaultValue = "", Order = 3)]
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
		[BindColumn("ModuleId", Description = "模块编号", DefaultValue = "", Order = 4)]
		public Int32 ModuleId
		{
			get { return _ModuleId; }
			set { if (OnPropertyChange("ModuleId", value)) _ModuleId = value; }
		}

		private Int32 _PortalId;
		/// <summary>
		/// 站点编号
		/// </summary>
		[Description("站点编号")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("PortalId", Description = "站点编号", DefaultValue = "", Order = 5)]
		public Int32 PortalId
		{
			get { return _PortalId; }
			set { if (OnPropertyChange("PortalId", value)) _PortalId = value; }
		}

		private Int32 _Status = (Int32)EnumStatus.Activated;
		/// <summary>
		/// 状态
		/// </summary>
		[Description("状态")]
		[DataObjectField(false, false, false, 3)]
		[BindColumn("Status", Description = "状态", DefaultValue = "", Order = 6)]
		public Int32 Status
		{
			get { return _Status; }
			set { if (OnPropertyChange("Status", value)) _Status = value; }
		}

		private Int32 _CreateUser;
		/// <summary>
		/// 创建者
		/// </summary>
		[Description("创建者")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("CreateUser", Description = "创建者", DefaultValue = "", Order = 7)]
		public Int32 CreateUser
		{
			get { return _CreateUser; }
			set { if (OnPropertyChange("CreateUser", value)) _CreateUser = value; }
		}

		private DateTime _CreateTime = xUserTime.UtcTime();
		/// <summary>
		/// 创建时间
		/// </summary>
		[Description("创建时间")]
		[DataObjectField(false, false, false, 23)]
		[BindColumn("CreateTime", Description = "创建时间", DefaultValue = "", Order = 8)]
		public DateTime CreateTime
		{
			get { return _CreateTime; }
			set { if (OnPropertyChange("CreateTime", value)) _CreateTime = value; }
		}

        private DateTime _StartTime = xUserTime.UtcTime();
		/// <summary>
		/// 开始时间
		/// </summary>
		[Description("开始时间")]
		[DataObjectField(false, false, false, 23)]
		[BindColumn("StartTime", Description = "开始时间", DefaultValue = "getdate()", Order = 9)]
		public DateTime StartTime
		{
			get { return xUserTime.LocalTime( _StartTime); }
			set { if (OnPropertyChange("StartTime", value)) _StartTime = xUserTime.ServerTime( value ); }
		}

		private DateTime _EndTime = xUserTime.UtcTime().AddYears(10);
		/// <summary>
		/// 结束时间
		/// </summary>
		[Description("结束时间")]
		[DataObjectField(false, false, false, 23)]
		[BindColumn("EndTime", Description = "结束时间", DefaultValue = "dateadd(year,(10),getdate())", Order = 10)]
		public DateTime EndTime
		{
			get { return xUserTime.LocalTime(_EndTime); }
			set { if (OnPropertyChange("EndTime", value)) _EndTime = xUserTime.ServerTime(value); }
		}

		private Int32 _LastUser;
		/// <summary>
		/// 更新者
		/// </summary>
		[Description("更新者")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("LastUser", Description = "更新者", DefaultValue = "", Order = 11)]
		public Int32 LastUser
		{
			get { return _LastUser; }
			set { if (OnPropertyChange("LastUser", value)) _LastUser = value; }
		}

		private String _LastIP;
		/// <summary>
		/// 更新IP
		/// </summary>
		[Description("更新IP")]
		[DataObjectField(false, false, false, 50)]
		[BindColumn("LastIP", Description = "更新IP", DefaultValue = "", Order = 12)]
		public String LastIP
		{
			get { return _LastIP; }
			set { if (OnPropertyChange("LastIP", value)) _LastIP = value; }
		}

		private DateTime _LastTime = xUserTime.UtcTime();
        /// <summary>
        /// 更新时间
        /// </summary>
        [Description("更新时间")]
		[DataObjectField(false, false, false, 23)]
        [BindColumn("LastTime", Description = "更新时间", DefaultValue = "", Order = 13)]
		public DateTime LastTime
		{
			get { return _LastTime; }
			set { if (OnPropertyChange("LastTime", value)) _LastTime = value; }
		}

        private Int32 _Per_AllUsers =0;
        /// <summary>
        /// 所有用户可见(默认可见0)
        /// </summary>
        [Description("所有用户可见(默认可见0)")]
        [DataObjectField(false, false, false, 3)]
        [BindColumn("Per_AllUsers", Description = "所有用户可见(默认可见0)", DefaultValue = "0", Order = 14)]
        public Int32 Per_AllUsers
        {
            get { return _Per_AllUsers; }
            set { if (OnPropertyChange("Per_AllUsers", value)) _Per_AllUsers = value; }
        }

        private String _Per_Roles = String.Empty;
        /// <summary>
        /// 可见权限角色集合
        /// </summary>
        [Description("可见权限角色集合")]
        [DataObjectField(false, false, true, 1073741823)]
        [BindColumn("Per_Roles", Description = "可见权限角色集合", DefaultValue = "", Order = 15)]
        public String Per_Roles
        {
            get { return _Per_Roles; }
            set { if (OnPropertyChange("Per_Roles", value)) _Per_Roles = value; }
        }


        private Int32 _Attribute1 = 0;
        /// <summary>
        /// 属性1
        /// </summary>
        [Description("属性1")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("Attribute1", Description = "属性1", DefaultValue = "0", Order = 16)]
        public Int32 Attribute1
        {
            get { return _Attribute1; }
            set { if (OnPropertyChange("Attribute1", value)) _Attribute1 = value; }
        }

        private String _Attribute2 = String.Empty;
        /// <summary>
        /// 属性2
        /// </summary>
        [Description("属性2")]
        [DataObjectField(false, false, true, 1073741823)]
        [BindColumn("Attribute2", Description = "属性2", DefaultValue = "", Order = 17)]
        public String Attribute2
        {
            get { return _Attribute2; }
            set { if (OnPropertyChange("Attribute2", value)) _Attribute2 = value; }
        }


        private String _Extension = String.Empty;
        /// <summary>
        /// 扩展选项(用户自定义)
        /// </summary>
        [Description("扩展选项(用户自定义)")]
        [DataObjectField(false, false, true, 1073741823)]
        [BindColumn("Extension", Description = "扩展选项(用户自定义)", DefaultValue = "", Order = 18)]
        public String Extension
        {
            get { return _Extension; }
            set { if (OnPropertyChange("Extension", value)) _Extension = value; }
        }


        private Int32 _Clicks = 0;
        /// <summary>
        /// 点击/下载数
        /// </summary>
        [Description("点击/下载数")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("Clicks", Description = "点击/下载数", DefaultValue = "0", Order = 19)]
        public Int32 Clicks
        {
            get { return _Clicks; }
            set { if (OnPropertyChange("Clicks", value)) _Clicks = value; }
        }

        private Int32 _Views = 0;
        /// <summary>
        /// 浏览次数
        /// </summary>
        [Description("浏览次数")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("Views", Description = "浏览次数", DefaultValue = "0", Order = 20)]
        public Int32 Views
        {
            get { return _Views; }
            set { if (OnPropertyChange("Views", value)) _Views = value; }
        }

        private Int32 _Heats = 0;
        /// <summary>
        /// 热度
        /// </summary>
        [Description("热度")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("Heats", Description = "热度", DefaultValue = "0", Order = 21)]
        public Int32 Heats
        {
            get { return _Heats; }
            set { if (OnPropertyChange("Heats", value)) _Heats = value; }
        }


        private String _Title = String.Empty;
        /// <summary>
        /// 标题
        /// </summary>
        [Description("标题")]
        [DataObjectField(false, false, true, 512)]
        [BindColumn("Title", Description = "标题", DefaultValue = "", Order = 22)]
        public String Title
        {
            get { return _Title; }
            set { if (OnPropertyChange("Title", value)) _Title = value; }
        }


        private String _FriendlyUrl = String.Empty;
        /// <summary>
        /// 伪静态URL
        /// </summary>
        [Description("伪静态URL")]
        [DataObjectField(false, false, true, 512)]
        [BindColumn(32, "FriendlyUrl", "伪静态URL", null, "nvarchar(512)", 0, 0, true)]
        public String FriendlyUrl
        {
            get { return _FriendlyUrl; }
            set { if (OnPropertyChange("FriendlyUrl", value)) _FriendlyUrl = value; }
        }


        private String _Relations = String.Empty;
        /// <summary>
        /// 相关联编号
        /// </summary>
        [Description("相关联编号")]
        [DataObjectField(false, false, true, 1073741823)]
        [BindColumn(32, "Relations", "相关联编号", null, "ntext", 0, 0, true)]
        public String Relations
        {
            get { return _Relations; }
            set { if (OnPropertyChange("Relations", value)) _Relations = value; }
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
					case "Options" : return _Options;
					case "Sort" : return _Sort;
					case "ModuleId" : return _ModuleId;
					case "PortalId" : return _PortalId;
					case "Status" : return _Status;
					case "CreateUser" : return _CreateUser;
					case "CreateTime" : return _CreateTime;
					case "StartTime" : return _StartTime;
					case "EndTime" : return _EndTime;
					case "LastUser" : return _LastUser;
					case "LastIP" : return _LastIP;
					case "LastTime" : return _LastTime;
                    case "Per_AllUsers": return _Per_AllUsers;
                    case "Per_Roles": return _Per_Roles;
                    case "Attribute1": return _Attribute1;
                    case "Attribute2": return _Attribute2;
                    case "Extension": return _Extension;
                    case "Clicks": return _Clicks;
                    case "Views": return _Views;
                    case "Heats": return _Heats;
                    case "Title": return _Title;
                    case "FriendlyUrl": return _FriendlyUrl;
                    case "Relations": return _Relations;
                    default: return base[name];
				}
			}
			set
			{
				switch (name)
				{
					case "ID" : _ID = Convert.ToInt32(value); break;
					case "Options" : _Options = Convert.ToString(value); break;
					case "Sort" : _Sort = Convert.ToInt32(value); break;
					case "ModuleId" : _ModuleId = Convert.ToInt32(value); break;
					case "PortalId" : _PortalId = Convert.ToInt32(value); break;
					case "Status" : _Status = Convert.ToInt32(value); break;
					case "CreateUser" : _CreateUser = Convert.ToInt32(value); break;
					case "CreateTime" : _CreateTime = Convert.ToDateTime(value); break;
					case "StartTime" : _StartTime = Convert.ToDateTime(value); break;
					case "EndTime" : _EndTime = Convert.ToDateTime(value); break;
					case "LastUser" : _LastUser = Convert.ToInt32(value); break;
					case "LastIP" : _LastIP = Convert.ToString(value); break;
					case "LastTime" : _LastTime = Convert.ToDateTime(value); break;
                    case "Per_AllUsers": _Per_AllUsers = Convert.ToInt32(value); break;
                    case "Per_Roles": _Per_Roles = Convert.ToString(value); break;
                    case "Attribute1": _Attribute1 = Convert.ToInt32(value); break;
                    case "Attribute2": _Attribute2 = Convert.ToString(value); break;
                    case "Extension": _Extension = Convert.ToString(value); break;
                    case "Clicks": _Clicks = Convert.ToInt32(value); break;
                    case "Views": _Views = Convert.ToInt32(value); break;
                    case "Heats": _Heats = Convert.ToInt32(value); break;
                    case "Title": _Title = Convert.ToString(value); break;
                    case "FriendlyUrl": _FriendlyUrl = Convert.ToString(value); break;
                    case "Relations": _Relations = Convert.ToString(value); break;
                    default: base[name] = value; break;
				}
			}
		}
		#endregion

		#region 字段名
		/// <summary>
		/// 取得背景字段名的快捷方式
		/// </summary>
		public class _
		{
			///<summary>
			/// 背景编号
			///</summary>
			public const String ID = "ID";

			///<summary>
			/// 选项集合
			///</summary>
			public const String Options = "Options";

			///<summary>
			/// 排序
			///</summary>
			public const String Sort = "Sort";

			///<summary>
			/// 模块编号
			///</summary>
			public const String ModuleId = "ModuleId";

			///<summary>
			/// 站点编号
			///</summary>
			public const String PortalId = "PortalId";

			///<summary>
			/// 状态
			///</summary>
			public const String Status = "Status";

			///<summary>
			/// 创建者
			///</summary>
			public const String CreateUser = "CreateUser";

			///<summary>
			/// 创建时间
			///</summary>
			public const String CreateTime = "CreateTime";

			///<summary>
			/// 开始时间
			///</summary>
			public const String StartTime = "StartTime";

			///<summary>
			/// 结束时间
			///</summary>
			public const String EndTime = "EndTime";

			///<summary>
			/// 更新者
			///</summary>
			public const String LastUser = "LastUser";

			///<summary>
			/// 更新IP
			///</summary>
			public const String LastIP = "LastIP";

			///<summary>
			/// 更新时间
			///</summary>
			public const String LastTime = "LastTime";

            ///<summary>
            /// 所有用户可见(默认可见0)
            ///</summary>
            public const String Per_AllUsers = "Per_AllUsers";

            ///<summary>
            /// 可见权限角色集合
            ///</summary>
            public const String Per_Roles = "Per_Roles";


            ///<summary>
            /// 属性1
            ///</summary>
            public const String Attribute1 = "Attribute1";

            ///<summary>
            /// 属性2
            ///</summary>
            public const String Attribute2 = "Attribute2";


            ///<summary>
            /// 扩展选项(用户自定义)
            ///</summary>
            public const String Extension = "Extension";

            ///<summary>
            /// 点击/下载数
            ///</summary>
            public const String Clicks = "Clicks";

            ///<summary>
            /// 浏览次数
            ///</summary>
            public const String Views = "Views";

            ///<summary>
            /// 热度
            ///</summary>
            public const String Heats = "Heats";

            ///<summary>
            /// 标题
            ///</summary>
            public const String Title = "Title";
 
            /// <summary>
            /// 伪静态URL
            /// </summary>
            public const String FriendlyUrl = ("FriendlyUrl");


            /// <summary>
            /// 相关联数据
            /// </summary>
            public const String Relations = ("Relations");
            
        }
		#endregion
	}
}