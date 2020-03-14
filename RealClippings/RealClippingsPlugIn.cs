using System;
using RealClippings.UI.Models;
using RealClippings.UI.Views;
using Rhino;
using Rhino.PlugIns;
using Rhino.UI;

namespace RealClippings
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class RealClippingsPlugIn : Rhino.PlugIns.PlugIn

    {
        public RealClippingsPlugIn()
        {
            Instance = this;
        }

        ///<summary>Gets the only instance of the RealClippingsPlugIn plug-in.</summary>
        public static RealClippingsPlugIn Instance
        {
            get; private set;
        }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and maintain plug-in wide options in a document.
        protected override LoadReturnCode OnLoad(ref string errorMessage)
        {
            // make sure our panel loads nice :)
            Panels.Show += PanelsOnShow;
            return base.OnLoad(ref errorMessage);
        }

        protected override void OnShutdown()
        {
            // clear event handlers
            Panels.Show -= PanelsOnShow;

            base.OnShutdown();
        }

        private void PanelsOnShow(object sender, ShowPanelEventArgs e)
        {
            // only do something if its actually for our panel
            if (e.PanelId != ClippingPlaneManagerPanel.PanelId) return;

            // get instance of our panel
            var panel = Panels.GetPanel<ClippingPlaneManagerPanel>(e.Document);

            // check if already initialized with view-model
            //if (panel.IsModelInitialized) return;

            // initialize new view-model
            panel.SetClippingPlaneListModel(new ClippingPlaneListViewModel(e.Document));
        }
    }
}