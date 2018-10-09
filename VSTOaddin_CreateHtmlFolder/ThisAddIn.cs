using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace VSTOaddin_CreateHtmlFolder
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            CreateHtmlFolder();
        }

        private void CreateHtmlFolder()
        {
            Outlook.MAPIFolder newView = null;
            string viewName = "HtmlView";
            Outlook.MAPIFolder inBox = (Outlook.MAPIFolder)
                this.Application.ActiveExplorer().Session.GetDefaultFolder(Outlook
                .OlDefaultFolders.olFolderInbox);
            Outlook.Folders searchFolders = (Outlook.Folders)inBox.Folders;
            bool foundView = false;
            foreach (Outlook.MAPIFolder searchFolder in searchFolders)
            {
                if (searchFolder.Name == viewName)
                {
                    newView = inBox.Folders[viewName];
                    foundView = true;
                }
            }
            //Outlook.OlDefaultFolders.olFolderInbox
            if (!foundView)
            {
                newView = (Outlook.MAPIFolder)inBox.Folders.
                    Add("HtmlView", Outlook.OlDefaultFolders.olFolderInbox );
                newView.WebViewURL = "https://www.microsoft.com";
                newView.WebViewOn = true;
            }
            Application.ActiveExplorer().SelectFolder(newView);
            Application.ActiveExplorer().CurrentFolder.Display();
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
