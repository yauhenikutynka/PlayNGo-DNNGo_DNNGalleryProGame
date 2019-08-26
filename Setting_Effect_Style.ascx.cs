using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Common;

namespace DNNGo.Modules.DNNGalleryProGame
{
    public partial class Setting_Effect_Style : BaseModule
    {


        #region "==属性=="

        /// <summary>
        /// 模块操作类
        /// </summary>
        private static ModuleController controller = new ModuleController();


        /// <summary>提示操作类</summary>
        MessageTips mTips = new MessageTips();
        #endregion


        #region "==方法=="

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindDataToPage()
        {
            EffectDBEntity XmlDB = Setting_EffectDB;
            if (!(XmlDB != null && String.IsNullOrEmpty(XmlDB.Name)))
            {


                //绑定样式表到内容框
                String StyleFile = Server.MapPath(String.Format("{0}Effects/{1}/Themes/{2}/Style.css", ModulePath, XmlDB.Name, Settings_EffectThemeName));
                using (StreamReader m_streamReader = File.OpenText(StyleFile))
                {
                    this.txtContent.Value = m_streamReader.ReadToEnd();
                    m_streamReader.Close();
                }

            }


        }


        /// <summary>
        /// 显示字段标题
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="ClassName"></param>
        /// <param name="ControlName"></param>
        /// <returns></returns>
        public String ViewTitleSpan(String Content, String ClassName, String ControlName)
        {

            return String.Format("<label  {2}><span {1} >{0}</span></label>",
                        Content,
                        !String.IsNullOrEmpty(ClassName) ? String.Format("class=\"{0}\"", ClassName) : "",
                      !String.IsNullOrEmpty(ControlName) ? String.Format("for=\"{0}\"", ControlName) : ""
                        );
        }


        /// <summary>
        /// 设置数据项
        /// </summary>
        private void SetDataItem()
        {


            //绑定样式表到内容框
            String StyleFile = Server.MapPath(String.Format("{0}Effects/{1}/Themes/{2}/Style.css", ModulePath, Settings_EffectName, Settings_EffectThemeName));
            File.WriteAllText(StyleFile, txtContent.Value, System.Text.Encoding.UTF8);

        }




        /// <summary>
        /// 设置模版参数的值
        /// </summary>
        private void SetThemeSettings()
        {



        }

        #endregion


        #region "==事件=="


        /// <summary>
        /// 页面加载事件
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //绑定数据
                    BindDataToPage();
                }
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }


        /// <summary>
        /// 更新绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // 设置需要绑定的方案项
                SetDataItem();

                mTips.LoadMessage("SaveStyleSuccess", EnumTips.Success, this, new String[] { "" });

                Response.Redirect(xUrl("Effect_Style"), false);
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }
        /// <summary>
        /// 返回
        /// </summary>
        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {

                Response.Redirect(xUrl("Effect_Options"), false);
            }
            catch (Exception ex)
            {
                ProcessModuleLoadException(ex);
            }
        }



        #endregion









    }
}