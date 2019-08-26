using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DotNetNuke.Services.Localization;
using DotNetNuke.Common;
using System.Web.Script.Serialization;
using System.Text;
 


namespace DNNGo.Modules.DNNGalleryProGame
{
    public partial class Setting_ManagerImportExport : BaseModule
    {


        #region "==属性=="

        /// <summary>
        /// 提示操作类
        /// </summary>
        MessageTips mTips = new MessageTips();


        private List<KeyValueEntity> ImportPictureList = new List<KeyValueEntity>();


        #endregion




        #region "==方法=="







        #endregion





        #region "==事件=="

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //如果查询到当前有字段时，需要给用户提示会清除掉原有的字段
                    cmdImportFormXml.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("ImportContent", this.LocalResourceFile) + "');");
                }
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// 导出数据到XML
        /// </summary>
        protected void cmdExportToXml_Click(object sender, EventArgs e)
        {
            try
            {

                ImportExportHelper ieHelper = new ImportExportHelper();
                ieHelper.ModuleID = ModuleId;
                ieHelper.UserId = UserId;

                StringBuilder PostContent = new StringBuilder();
                PostContent.Append(  ieHelper.Export());
                if (PostContent != null && PostContent.Length >0 )
                {

                    PostContent.AppendLine()
                        .AppendLine().Append("                                          ")
                        .AppendLine()
                        .AppendLine()
                        .AppendLine()
                        .AppendLine()
                       .AppendLine().Append("                                           ");

                     Response.Clear();
                     Response.ClearContent();
                     Response.AddHeader("Content-disposition", "attachment;filename=DNNGalleryProGame.xml");
                     Response.AddHeader("content-length", PostContent.Length.ToString());
                     Response.ContentEncoding = Encoding.UTF8;
                     Response.ContentType = "text/xml";
                     Response.Write(PostContent);
                     Response.Flush();
                     Response.Close();
                     Response.End();
                     
                    
                }
                else
                {
                    //没有可导出的文章条目
                    mTips.IsPostBack = true;
                    mTips.LoadMessage("ExportContentError", EnumTips.Success, this, new String[] { "" });
                }


                
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, ex);
            }
            
        }

        /// <summary>
        /// 从XML导入数据
        /// </summary>
        protected void cmdImportFormXml_Click(object sender, EventArgs e)
        {
            
            try
            {
           
                HttpPostedFile hpfile = fuImportFormXml.PostedFile;



                if (hpfile.ContentLength > 0)
                {

                    if (Path.GetExtension(hpfile.FileName).IndexOf(".xml", StringComparison.CurrentCultureIgnoreCase) >= 0)
                    {
                        ImportExportHelper ieHelper = new ImportExportHelper();
                        ieHelper.ModuleID = ModuleId;
                        ieHelper.UserId = UserId;

                        Int32 InsertResult = ieHelper.Import(hpfile);
 
                        //提示
                        mTips.LoadMessage("ImportContentSuccess", EnumTips.Success, this, new String[] { InsertResult.ToString() });

                        //跳转
                        Response.Redirect(xUrl("ManagerList"));
                    }
                    else
                    {
                        //上传文件的后缀名错误
                        mTips.IsPostBack = true;
                        mTips.LoadMessage("ImportContentExtensionError", EnumTips.Success, this, new String[] { "" });
                    }
                }
                else
                {
                    //为上传任何数据
                    mTips.IsPostBack = true;
                    mTips.LoadMessage("ImportContentNullError", EnumTips.Success, this, new String[] { "" });
                }
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, ex);
            }
    
        

 

        }

        #endregion




  



    }
}