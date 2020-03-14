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
    public class MakePlanViewCommand : ClippingPlaneCommandBase
    {
        public MakePlanViewCommand()
        {
            MenuText = "Set view";
            ToolTip = "Creates a plan view for the selected clipping plane";
        }

        protected override void OnExecuted(EventArgs e)
        {
            base.OnExecuted(e);

            var doc = RhinoDoc.ActiveDoc;
            doc.Views.RedrawEnabled = false;

            var view = doc.Views.Add(_model.Name, DefinedViewportProjection.None, doc.Views.ActiveView.Bounds, true);
            var plane = _model.Plane;
            plane.Flip();
            _model.AddClippedViewport(view.ActiveViewport);
            view.ActiveViewport.SetToPlanView(plane.Origin, plane.XAxis, plane.YAxis, true);
            view.ActiveViewport.ZoomBoundingBox(_model.GetBoundingBox(view.ActiveViewport.ConstructionPlane()));
            view.ActiveViewport.Name = _model.Name;

            doc.Views.RedrawEnabled = true;
            doc.Views.Redraw();
        }
    }
}
