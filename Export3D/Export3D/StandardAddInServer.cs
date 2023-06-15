using Inventor;
using InvAddIn.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Windows;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;
using System.Resources;

namespace Export3D
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("200b2674-6344-406a-8588-825ae4cd2325")]
    public partial class StandardAddInServer : Inventor.ApplicationAddInServer
    {

        // Inventor application object.
        private Inventor.Application m_inventorApplication;


        // Icons objects
        object plus16obj, plus128obj;


        // guid string
        string addInGuid = "200b2674-6344-406a-8588-825ae4cd2325";

        // UI members
        CommandManager cmdMan; //inside the Inventor already available
        ControlDefinitions ctrlDefs;
        CommandCategory cmdCat;
        Ribbon assemblyRibbon;
        RibbonTab placeTab;
        ButtonDefinition applyButton;
        RibbonPanel panel;
        CommandControl controlApplyBreak;
        CommandControl controlSettingsBreak;

        //add-in model
       
          IInventorHandler handler;

        public StandardAddInServer()
        {
        }

        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // This method is called by Inventor when it loads the addin.
            // The AddInSiteObject provides access to the Inventor Application object.
            // The FirstTime flag indicates if the addin is loaded for the first time.

            // Initialize AddIn members.
            m_inventorApplication = addInSiteObject.Application;

            // TODO: Add ApplicationAddInServer.Activate implementation.
            // e.g. event initialization, command creation etc.

            // Initialize add-in model
           
            handler = new InventorHandler(m_inventorApplication);

            // get icon objects
            getIcons();

            // modify the ribbon
            modifyRibbon();

        }



        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated

            // TODO: Add ApplicationAddInServer.Deactivate implementation

            // Release objects.
            m_inventorApplication = null;
            handler = null;
            plus16obj = null;
            plus128obj = null;

            cmdMan = null;
            ctrlDefs = null;
            cmdCat.Delete();
            cmdCat = null;
            assemblyRibbon = null;
            placeTab = null;
            applyButton.Delete();
            applyButton = null;            
            panel.Delete();
            panel = null;
            controlApplyBreak.Delete();
            controlApplyBreak = null;
            controlSettingsBreak.Delete();
            controlSettingsBreak = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExecuteCommand(int commandID)
        {
            // Note:this method is now obsolete, you should use the 
            // ControlDefinition functionality for implementing commands.
        }

        public object Automation
        {
            // This property is provided to allow the AddIn to expose an API 
            // of its own to other programs. Typically, this  would be done by
            // implementing the AddIn's API interface in a class and returning 
            // that class object through this property.

            get
            {
                // TODO: Add ApplicationAddInServer.Automation getter implementation
                return null;
            }
        }


        /// <summary>
        /// gets icons from embedded resources and converts them to objects 
        /// </summary>
        private void getIcons()
        {
            //get current assembly
            Assembly thisDll = Assembly.GetExecutingAssembly();

            //get icon streams
            Stream plus16stream = thisDll.GetManifestResourceStream("InvAddIn.resources.plus16.ico");
            Stream plus128stream = thisDll.GetManifestResourceStream("InvAddIn.resources.plus128.ico");



            //get icons
            Icon plus16icon = new Icon(plus16stream);
            Icon plus128icon = new Icon(plus128stream);

            //convert to objects
            plus16obj = AxHostConverter.ImageToPictureDisp(plus16icon.ToBitmap());
            plus128obj = AxHostConverter.ImageToPictureDisp(plus128icon.ToBitmap());
        }

        /// <summary>
        /// modifies Drawing ribbon by adding two buttons used by the add-in
        /// </summary>
        private void modifyRibbon()
        {
            //get Command manager
            cmdMan = m_inventorApplication.CommandManager;

            //get control definitions
            ctrlDefs = cmdMan.ControlDefinitions;

            //define command category for add-in's buttons
            cmdCat = cmdMan.CommandCategories.Add("Export3D", "Autodesk:CmdCategory:Export3D", addInGuid);

            //get 'Assembly' ribbon
            assemblyRibbon = m_inventorApplication.UserInterfaceManager.Ribbons["Assembly"];

            //get 'Place Assembly' tab from 'Assembly' ribbon
            placeTab = assemblyRibbon.RibbonTabs["id_TabTools"];

            //define 'Export3D' button
            applyButton = ctrlDefs.AddButtonDefinition("Export3D", "Autodesk:Export3D:Export3D", CommandTypesEnum.kQueryOnlyCmdType, addInGuid, "Export3D description", "Export3D tooltip", plus16obj, plus128obj, ButtonDisplayEnum.kAlwaysDisplayText);
            applyButton.OnExecute += new ButtonDefinitionSink_OnExecuteEventHandler(customAction);
            cmdCat.Add(applyButton);

          
            //define panel in 'Place Views' tab
            panel = placeTab.RibbonPanels.Add("Export3D", "Autodesk:Export3D:Export3DPanel", addInGuid);
            controlApplyBreak = panel.CommandControls.AddButton(applyButton, true, true);
           
        }


        private void customAction(NameValueMap options)
        {
            handler.Export3D();
        }

        #endregion

    }
}
