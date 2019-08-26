using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;

namespace DNNGo.Modules.DNNGalleryProGame
{
    public partial class Resource_Masters : BasePage
    {
        /// <summary>
        /// 加载页面的参数
        /// </summary>
        public String MasterToken = WebHelper.GetStringParam(HttpContext.Current.Request, "Master", "");


        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
               

                if (!(Request.UrlReferrer != null && !String.IsNullOrEmpty(Request.UrlReferrer.ToString()) && Request.UrlReferrer.ToString() != CurrentUrl))
                {
                    Response.Redirect(Globals.NavigateURL(TabId));
                }

                //载入模块
                LoadModule(String.Format("{0}.ascx", MasterToken), ref phPlaceHolder);

            //}
        }


        protected override void Page_Init(System.Object sender, System.EventArgs e)
        {
            //调用基类Page_Init，主要用于权限验证
            base.Page_Init(sender, e);
        }
    }
}