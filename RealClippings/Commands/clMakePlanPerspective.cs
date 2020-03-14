using System;
using Rhino;
using Rhino.Commands;
using Rhino.Display;

namespace RealClippings.Commands
{
    public class clMakePlanPerspective : Command
    {
        static clMakePlanPerspective _instance;
        public clMakePlanPerspective()
        {
            _instance = this;
        }

        ///<summary>The only instance of the clMakePlanPerspective command.</summary>
        public static clMakePlanPerspective Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "clMakePlanPerspective"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            var vp = doc.Views.ActiveView.ActiveViewport;
            if (!vp.IsPlanView)
            {
                RhinoApp.WriteLine("This command only works with a plan view active!");
                return Result.Failure;
            }

            var up = vp.CameraUp;
            var cameraLocation = vp.CameraLocation;
            var targetLocation = vp.CameraTarget;

            vp.SetProjection(DefinedViewportProjection.Perspective, vp.Name, false);

            vp.SetCameraLocations(targetLocation, cameraLocation);
            vp.CameraUp = up;

            doc.Views.Redraw();
            return Result.Success;
        }
    }
}