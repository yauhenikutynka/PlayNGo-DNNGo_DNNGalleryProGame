using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace DNNGo.Modules.DNNGalleryProGame
{
	/// <summary>
	/// 背景分组关系
	/// </summary>
	[Serializable]
	[DataObject]
	[Description("背景分组关系")]
	[BindTable("DNNGo_DNNGalleryProGame_Slider_Group", Description = "背景分组关系", ConnName = "SiteSqlServer")]
	public partial class DNNGo_DNNGalleryProGame_Slider_Group : Entity<DNNGo_DNNGalleryProGame_Slider_Group>
	{
		#region 属性
		private Int32 _ID;
		/// <summary>
		/// 关系编号
		/// </summary>
		[Description("关系编号")]
		[DataObjectField(true, true, false, 10)]
		[BindColumn("ID", Description = "关系编号", DefaultValue = "", Order = 1)]
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

		private Int32 _GroupID;
		/// <summary>
		/// 
		/// </summary>
		[Description("")]
		[DataObjectField(false, false, false, 10)]
		[BindColumn("GroupID", Description = "", DefaultValue = "", Order = 3)]
		public Int32 GroupID
		{
			get { return _GroupID; }
			set { if (OnPropertyChange("GroupID", value)) _GroupID = value; }
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
					case "GroupID" : return _GroupID;
					default: return base[name];
				}
			}
			set
			{
				switch (name)
				{
					case "ID" : _ID = Convert.ToInt32(value); break;
					case "SliderID" : _SliderID = Convert.ToInt32(value); break;
					case "GroupID" : _GroupID = Convert.ToInt32(value); break;
					default: base[name] = value; break;
				}
			}
		}
		#endregion

		#region 字段名
		/// <summary>
		/// 取得背景分组关系字段名的快捷方式
		/// </summary>
		public class _
		{
			///<summary>
			/// 关系编号
			///</summary>
			public const String ID = "ID";

			///<summary>
			/// 背景编号
			///</summary>
			public const String SliderID = "SliderID";

			///<summary>
			/// 
			///</summary>
			public const String GroupID = "GroupID";
		}
		#endregion
	}
}