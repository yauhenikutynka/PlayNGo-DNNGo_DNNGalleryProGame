using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Services.Localization;


namespace DNNGo.Modules.DNNGalleryProGame
{
    public partial class Resource_Item1 : BasePage
    {
        /// <summary>
        /// 控件名
        /// </summary>
        public String ControlToken = WebHelper.GetStringParam(HttpContext.Current.Request, "ControlToken", "Item");



        #region "事件"

        /// <summary>
        /// 页面载入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    
                }

                //绑定容器中的控件
                BindContainer();
 
            }
            catch (Exception exc) //Module failed to load
            {
                //DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, exc);
            }
        }




        /// <summary>
        /// 绑定控件到容器
        /// </summary>
        public void BindContainer()
        {
            //加载相应的控件
            BaseModule ManageContent = new BaseModule();

            String ControlName = "Resource_Item.ascx";
            if (!String.IsNullOrEmpty(ControlToken) && ControlToken.IndexOf("icons", StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                ControlName = "Resource_SelectIcons.ascx";
            }

            string ContentSrc = ResolveClientUrl(string.Format("{0}/{1}", this.TemplateSourceDirectory, ControlName));
            if (System.IO.File.Exists(MapPath(ContentSrc)))
            {
                ManageContent = (BaseModule)LoadControl(ContentSrc);
                ManageContent.ModuleConfiguration = ModuleConfiguration;
                ManageContent.LocalResourceFile = Localization.GetResourceFile(this, string.Format("{0}.resx", ControlName));
                phContainer.Controls.Add(ManageContent);
            }
        }

        protected override void Page_Init(System.Object sender, System.EventArgs e)
        {
            //调用基类Page_Init，主要用于权限验证
            base.Page_Init(sender, e);
        }

        #endregion





    }
}