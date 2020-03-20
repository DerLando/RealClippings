using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;
using Rhino;
using Rhino.Display;

namespace RealClippings.UI.EtoCommands
{
    /// <summary>
    /// Command to construct a plan view for the currently selected Clipping plane
    /// </summary>
    public class MakePlanViewCommand : ClippingPlaneCommandBase
    {
        public MakePlanViewCommand()
        {
            MenuText = "Set view";
            ToolTip = "Creates an individual plan view for each of the selected clipping planes";
        }

        protected override void OnExecuted(EventArgs e)
        {
            base.OnExecuted(e);

            var doc = RhinoDoc.ActiveDoc;
            doc.Views.RedrawEnabled = false;

            if (_models is null) return;

            foreach (var clippingPlaneModel in _models)
            {
                var view = doc.Views.Add(clippingPlaneModel.Name, DefinedViewportProjection.None, doc.Views.ActiveView.Bounds, true);
                var plane = clippingPlaneModel.Plane;
                plane.Flip();
                clippingPlaneModel.AddClippedViewport(view.ActiveViewport);
                view.ActiveViewport.SetToPlanView(plane.Origin, plane.XAxis, plane.YAxis, true);
                view.ActiveViewport.ZoomBoundingBox(clippingPlaneModel.GetBoundingBox(view.ActiveViewport.ConstructionPlane()));
                view.ActiveViewport.Name = clippingPlaneModel.Name;
            }

            doc.Views.RedrawEnabled = true;
            doc.Views.Redraw();
        }
    }
}
