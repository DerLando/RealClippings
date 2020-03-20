using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using RealClippings.UI.Models;
using Rhino;
using Rhino.Commands;

namespace RealClippings.Commands.Hidden
{
    [Guid("A5C4B8C1-1345-4B8A-94A7-B21D30A12DF2"), CommandStyle(Style.Hidden)]
    public class clSelectClippingPlanes : Command
    {
        private ClippingPlaneModel[] _models = new ClippingPlaneModel[0];

        static clSelectClippingPlanes _instance;
        public clSelectClippingPlanes()
        {
            _instance = this;
        }

        ///<summary>The only instance of the clSelectClippingPlanes command.</summary>
        public static clSelectClippingPlanes Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "clSelectClippingPlanes"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            if (_models.Length == 0) return Result.Nothing;

            doc.Objects.Select(from model in _models select model.Guid);

            var suffix = "";
            if (_models.Length != 1) suffix = "s";
            RhinoApp.WriteLine($"{_models.Length} clipping plane{suffix} selected");

            doc.Views.Redraw();
            return Result.Success;
        }

        public void SetClippingPlanes(IEnumerable<ClippingPlaneModel> models)
        {
            _models = models is null ? (new ClippingPlaneModel[0]) : models.ToArray();
        }
    }
}