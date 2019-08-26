using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel;

namespace DNNGo.Modules.DNNGalleryProGame
{
    /// <summary>
    /// 分组实体(XML & 序列化)
    /// </summary>
    [Serializable]
    [DataObject]
    [Description("分组")]
    [XmlEntityAttributes("DNNGo_DNNGalleryProGame//GalleryGroupEntityList//GalleryGroupEntityItem")]
    public class GalleryGroupEntity
    {
        #region 属性

        /// <summary>名称</summary>
        public String Name { get; set; }


        /// <summary>描述</summary>
        public String Description { get; set; }

        /// <summary>引用数</summary>
        public String QuoteCount { get; set; }

        /// <summary>排序</summary>
        public String Sort { get; set; }


        public GalleryGroupEntity()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Name"></param>
        /// <param name="_Description"></param>
        /// <param name="_QuoteCount"></param>
        /// <param name="_Sort"></param>
        public GalleryGroupEntity(String _Name, String _Description, String _QuoteCount, String _Sort)
        {
            Name = _Name;
            Description = _Description;
            QuoteCount = _QuoteCount;
            Sort = _Sort;
        }

        public GalleryGroupEntity(DNNGo_DNNGalleryProGame_Group GroupItem)
        {
            Name = GroupItem.Name;
            Description = GroupItem.Description;
            QuoteCount = GroupItem.QuoteCount.ToString();
            Sort = GroupItem.Sort.ToString();
        }


        #endregion
 
    }
}