using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;


using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using DotNetNuke.Common.Utilities;

namespace DNNGo.Modules.DNNGalleryProGame
{
    public class baseController :   IPortable
    {
        #region "Optional Interfaces"

 




        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="ModuleID">The Id of the module to be exported</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------

        public string ExportModule(int ModuleID)
        {
            ImportExportHelper ieHelper = new ImportExportHelper();
            ieHelper.ModuleID = ModuleID;
            return ieHelper.Export();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="ModuleID">The ID of the Module being imported</param>
        /// <param name="Content">The Content being imported</param>
        /// <param name="Version">The Version of the Module Content being imported</param>
        /// <param name="UserID">The UserID of the User importing the Content</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------

        public void ImportModule(int ModuleID, string Content, string Version, int UserId)
        {

            ImportExportHelper ieHelper = new ImportExportHelper();
            ieHelper.ModuleID = ModuleID;
            ieHelper.UserId = UserId;

            ieHelper.Import(Content);

             
        }

        #endregion



 
    }
}