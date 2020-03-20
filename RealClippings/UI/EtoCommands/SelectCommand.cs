using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;

namespace RealClippings.UI.EtoCommands
{
    public class SelectCommand : ClippingPlaneCommandBase
    {
        public SelectCommand()
        {
            MenuText = "Select";
            ToolTip = "Select clipping planes in model";
        }

        protected override void OnExecuted(EventArgs e)
        {
            base.OnExecuted(e);

            if (_models.Length == 0) return;

            Commands.Hidden.clSelectClippingPlanes.Instance.SetClippingPlanes(_models);
            RhinoApp.RunScript("clSelectClippingPlanes", true);
        }
    }
}
