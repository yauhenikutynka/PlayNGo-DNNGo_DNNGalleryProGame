using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel;

namespace DNNGo.Modules.DNNGalleryProGame
{
    /// <summary>
    /// 效果实体(XML & 序列化)
    /// </summary>
    [Serializable]
    [DataObject]
    [Description("滑动背景")]
    [XmlEntityAttributes("DNNGo_DNNGalleryProGame//XmlSliserEntityList//XmlSliserEntityItem")]
    public class XmlSliserEntity
    {
        #region 属性


        /// <summary>排序</summary>
        public Int32 Sort { get; set; }
  
        /// <summary>状态</summary>
        public Int32 Status { get; set; }

        /// <summary>选项集合</summary>
        public String Options { get; set; }

        /// <summary>扩展集合</summary>
        public String Extension { get; set; }


        public String Title { get; set; }
        public String FriendlyUrl { get; set; }
        public String Relations { get; set; }


        /// <summary>层的集合</summary>
        public String Layers { get; set; }


        /// <summary>分组几盒</summary>
        public String Groups { get; set; }


        private DateTime _CreateTime = xUserTime.UtcTime();
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }

        private DateTime _StartTime = xUserTime.UtcTime();
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }


        private DateTime _EndTime = xUserTime.UtcTime().AddYears(10);
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

   
 
        #endregion
 
    }
}