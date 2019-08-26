using System;
using System.Collections.Generic;
using System.ComponentModel;
 

namespace DNNGo.Modules.DNNGalleryProGame
{
	/// <summary>
	/// 文件下载日志
	/// </summary>
	[Serializable]
	[DataObject]
	[Description("文件下载日志")]
	[BindTable("DNNGo_DNNGalleryProGame_FileLogs", Description = "文件下载日志", ConnName = "SiteSqlServer")]
	public partial class DNNGo_DNNGalleryProGame_FileLogs : Entity<DNNGo_DNNGalleryProGame_FileLogs>
	{
		#region 属性
		private Int32 _ID;
		/// <summary>
		/// 日志编号
		/// </summary>
		[Description("日志编号")]
		[DataObjectField(true, true, false, 10)]
		[BindColumn("ID", Description = "日志编号", DefaultValue = "", Order = 1)]
		public Int32 ID
		{
			get { return _ID; }
			set { if (OnPropertyChange("ID", value)) _ID = value; }
		}

		private Int32 _SliderID;
		/// <summary>
		/// 背景编号
		/// </summary>
		[Description("背景编号")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("SliderID", Description = "背景编号", DefaultValue = "", Order = 2)]
		public Int32 SliderID
		{
			get { return _SliderID; }
			set { if (OnPropertyChange("SliderID", value)) _SliderID = value; }
		}

		private Int32 _LayerID;
		/// <summary>
		/// 层编号
		/// </summary>
		[Description("层编号")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("LayerID", Description = "层编号", DefaultValue = "", Order = 3)]
		public Int32 LayerID
		{
			get { return _LayerID; }
			set { if (OnPropertyChange("LayerID", value)) _LayerID = value; }
		}

		private Int32 _FileID;
		/// <summary>
		/// 文件编号
		/// </summary>
		[Description("文件编号")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("FileID", Description = "文件编号", DefaultValue = "", Order = 4)]
		public Int32 FileID
		{
			get { return _FileID; }
			set { if (OnPropertyChange("FileID", value)) _FileID = value; }
		}

		private String _FileLink;
		/// <summary>
		/// 文件路径
		/// </summary>
		[Description("文件路径")]
		[DataObjectField(false, false, true, 512)]
		[BindColumn("FileLink", Description = "文件路径", DefaultValue = "", Order = 5)]
		public String FileLink
		{
			get { return _FileLink; }
			set { if (OnPropertyChange("FileLink", value)) _FileLink = value; }
		}

		private String _Options;
		/// <summary>
		/// 选项集合
		/// </summary>
		[Description("选项集合")]
		[DataObjectField(false, false, true, 1073741823)]
		[BindColumn("Options", Description = "选项集合", DefaultValue = "", Order = 6)]
		public String Options
		{
			get { return _Options; }
			set { if (OnPropertyChange("Options", value)) _Options = value; }
		}

		private Int32 _ModuleId;
		/// <summary>
		/// 模块编号
		/// </summary>
		[Description("模块编号")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("ModuleId", Description = "模块编号", DefaultValue = "", Order = 7)]
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
		[BindColumn("PortalId", Description = "站点编号", DefaultValue = "", Order = 8)]
		public Int32 PortalId
		{
			get { return _PortalId; }
			set { if (OnPropertyChange("PortalId", value)) _PortalId = value; }
		}

		private DateTime _CreateTime;
		/// <summary>
		/// 创建时间
		/// </summary>
		[Description("创建时间")]
		[DataObjectField(false, false, false, 23)]
		[BindColumn("CreateTime", Description = "创建时间", DefaultValue = "", Order = 9)]
		public DateTime CreateTime
		{
			get { return _CreateTime; }
			set { if (OnPropertyChange("CreateTime", value)) _CreateTime = value; }
		}

		private Int32 _CreateUser;
		/// <summary>
		/// 创建用户
		/// </summary>
		[Description("创建用户")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("CreateUser", Description = "创建用户", DefaultValue = "", Order = 10)]
		public Int32 CreateUser
		{
			get { return _CreateUser; }
			set { if (OnPropertyChange("CreateUser", value)) _CreateUser = value; }
		}

		private String _CreateIP;
		/// <summary>
		/// 创建IP
		/// </summary>
		[Description("创建IP")]
		[DataObjectField(false, false, false, 50)]
		[BindColumn("CreateIP", Description = "创建IP", DefaultValue = "", Order = 11)]
		public String CreateIP
		{
			get { return _CreateIP; }
			set { if (OnPropertyChange("CreateIP", value)) _CreateIP = value; }
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
					case "SliderID" : return _SliderID;
					case "LayerID" : return _LayerID;
					case "FileID" : return _FileID;
					case "FileLink" : return _FileLink;
					case "Options" : return _Options;
					case "ModuleId" : return _ModuleId;
					case "PortalId" : return _PortalId;
					case "CreateTime" : return _CreateTime;
					case "CreateUser" : return _CreateUser;
					case "CreateIP" : return _CreateIP;
					default: return base[name];
				}
			}
			set
			{
				switch (name)
				{
					case "ID" : _ID = Convert.ToInt32(value); break;
					case "SliderID" : _SliderID = Convert.ToInt32(value); break;
					case "LayerID" : _LayerID = Convert.ToInt32(value); break;
					case "FileID" : _FileID = Convert.ToInt32(value); break;
					case "FileLink" : _FileLink = Convert.ToString(value); break;
					case "Options" : _Options = Convert.ToString(value); break;
					case "ModuleId" : _ModuleId = Convert.ToInt32(value); break;
					case "PortalId" : _PortalId = Convert.ToInt32(value); break;
					case "CreateTime" : _CreateTime = Convert.ToDateTime(value); break;
					case "CreateUser" : _CreateUser = Convert.ToInt32(value); break;
					case "CreateIP" : _CreateIP = Convert.ToString(value); break;
					default: base[name] = value; break;
				}
			}
		}
		#endregion

		#region 字段名
		/// <summary>
		/// 取得文件下载日志字段名的快捷方式
		/// </summary>
		public class _
		{
			///<summary>
			/// 日志编号
			///</summary>
			public const String ID = "ID";

			///<summary>
			/// 背景编号
			///</summary>
			public const String SliderID = "SliderID";

			///<summary>
			/// 层编号
			///</summary>
			public const String LayerID = "LayerID";

			///<summary>
			/// 文件编号
			///</summary>
			public const String FileID = "FileID";

			///<summary>
			/// 文件路径
			///</summary>
			public const String FileLink = "FileLink";

			///<summary>
			/// 选项集合
			///</summary>
			public const String Options = "Options";

			///<summary>
			/// 模块编号
			///</summary>
			public const String ModuleId = "ModuleId";

			///<summary>
			/// 站点编号
			///</summary>
			public const String PortalId = "PortalId";

			///<summary>
			/// 创建时间
			///</summary>
			public const String CreateTime = "CreateTime";

			///<summary>
			/// 创建用户
			///</summary>
			public const String CreateUser = "CreateUser";

			///<summary>
			/// 创建IP
			///</summary>
			public const String CreateIP = "CreateIP";
		}
		#endregion
	}
}