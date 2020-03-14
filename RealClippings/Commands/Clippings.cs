using Rhino;
using Rhino.Commands;
using Rhino.UI;

namespace RealClippings.Commands
{
    public class Clippings : Command
    {
        public Clippings()
        {
            // register clipping plane manager panel
            Panels.RegisterPanel(PlugIn, typeof(UI.Views.ClippingPlaneManagerPanel), "Clippings", null);

            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static Clippings Instance
        {
            get; private set;
        }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName
        {
            get { return "Clippings"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            var panelId = UI.Views.ClippingPlaneManagerPanel.PanelId;
            var visible = Panels.IsPanelVisible(panelId);

            var prompt = (visible)
                ? "ClippingPlane panel is visible"
                : "ClippingPlane panel is hidden";

            RhinoApp.WriteLine(prompt);

            // toggle visible
            if (!visible)
            {
                Panels.OpenPanel(panelId);
            }
            else Panels.ClosePanel(panelId);
            return Result.Success;
        }
    }
}
