using System;
using System.Collections.Generic;
using System.Web;

namespace DNNGo.Modules.DNNGalleryProGame
{
    /// <summary>
    /// 效果展示数据
    /// </summary>
    [XmlEntityAttributes("DNNGo_DNNGalleryProGame//EffectDB")]
    public class EffectDBEntity
    {

        private String _Name = String.Empty;
        /// <summary>
        /// 效果名称
        /// </summary>
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }


        private String _Description = String.Empty;
        /// <summary>
        /// 效果描述
        /// </summary>
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }


        private String _Version = String.Empty;
        /// <summary>
        /// 版本号
        /// </summary>
        public String Version
        {
            get { return _Version; }
            set { _Version = value; }
        }


        private String _Thumbnails = String.Empty;
        /// <summary>
        /// 缩略图
        /// </summary>
        public String Thumbnails
        {
            get { return _Thumbnails; }
            set { _Thumbnails = value; }
        }


        private String _EffectScript = String.Empty;
        /// <summary>
        /// 效果附带脚本
        /// </summary>
        public String EffectScript
        {
            get { return _EffectScript; }
            set { _EffectScript = value; }
        }


        private String _GlobalScript = String.Empty;
        /// <summary>
        /// 全局附带脚本
        /// </summary>
        public String GlobalScript
        {
            get { return _GlobalScript; }
            set { _GlobalScript = value; }
        }

 
        private String _DemoUrl = String.Empty;
        /// <summary>
        /// 演示地址
        /// </summary>
        public String DemoUrl
        {
            get { return _DemoUrl; }
            set { _DemoUrl = value; }
        }


        private String _LayerType = "";
        /// <summary>
        /// 层的类型(通用,文本,视频等等，可以自定义)
        /// </summary>
        public String LayerType
        {
            get { return _LayerType; }
            set { _LayerType = value; }
        }


        private String _LayerName = "Layer";
        /// <summary>
        /// 层的名称，默认为Layer
        /// </summary>
        public String LayerName
        {
            get { return _LayerName; }
            set { _LayerName = value; }
        }


        
        private Boolean _Groups = false;
        /// <summary>
        /// 是否分组
        /// </summary>
        public Boolean Groups
        {
            get { return _Groups; }
            set { _Groups = value; }
        }

        private Boolean _Downloads = false;
        /// <summary>
        /// 是否下载
        /// </summary>
        public Boolean Downloads
        {
            get { return _Downloads; }
            set { _Downloads = value; }
        }


        private Boolean _CustomModels = false;
        /// <summary>
        /// 是否支持自定义模型
        /// </summary>
        public Boolean CustomModels
        {
            get { return _CustomModels; }
            set { _CustomModels = value; }
        }

        
        private Boolean _Responsive = false;
        /// <summary>
        /// 是否响应式
        /// </summary>
        public Boolean Responsive
        {
            get { return _Responsive; }
            set { _Responsive = value; }
        }



        private Boolean _Sliders = true;
        /// <summary>
        /// 是否拥滑动背景
        /// </summary>
        public Boolean Sliders
        {
            get { return _Sliders; }
            set { _Sliders = value; }
        }
        
        private Boolean _Layers = false;
        /// <summary>
        /// 是否拥有层(子集)
        /// </summary>
        public Boolean Layers
        {
            get { return _Layers; }
            set { _Layers = value; }
        }

        private Boolean _Pager = false;
        /// <summary>
        /// 是否带有翻页
        /// </summary>
        public Boolean Pager
        {
            get { return _Pager; }
            set { _Pager = value; }
        }


        private Boolean _Ajax = false;
        /// <summary>
        /// 是否支持Ajax
        /// </summary>
        public Boolean Ajax
        {
            get { return _Ajax; }
            set { _Ajax = value; }
        }

        private Boolean _GoogleMap = false;
        /// <summary>
        /// 是否支持谷歌地图
        /// </summary>
        public Boolean GoogleMap
        {
            get { return _GoogleMap; }
            set { _GoogleMap = value; }
        }


    }
}