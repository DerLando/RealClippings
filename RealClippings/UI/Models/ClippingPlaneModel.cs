using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.Display;
using Rhino.DocObjects;
using Rhino.Geometry;

namespace RealClippings.UI.Models
{
    public class ClippingPlaneModel
    {
        private readonly RhinoDoc _doc;
        private readonly Guid _guid;
        private readonly Guid[] _viewportIds;

        private bool _isActive;
        private string _name;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                SetPlaneActive(_isActive);
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                SetName(_name);
            }
        }

        public Plane Plane => GetPlane();

        public ClippingPlaneModel(RhinoDoc doc, ClippingPlaneObject clippingPlaneObject)
        {
            _doc = doc;
            _guid = clippingPlaneObject.Id;
            _viewportIds = clippingPlaneObject.ClippingPlaneGeometry.ViewportIds();

            _isActive = _viewportIds.Length > 0;
            _name = clippingPlaneObject.Attributes.Name is null ? "unnamed" : clippingPlaneObject.Attributes.Name;
        }

        private ClippingPlaneObject GetClippingPlaneObject()
        {
            return (ClippingPlaneObject)_doc.Objects.Find(_guid);
        }

        private Plane GetPlane()
        {
            return GetClippingPlaneObject().ClippingPlaneGeometry.Plane;
        }

        private void SetPlaneActive(bool isActive)
        {
            var clippingObject = GetClippingPlaneObject();
            foreach (var viewportId in _viewportIds)
            {
                if (isActive) clippingObject.ClippingPlaneGeometry.AddClipViewportId(viewportId);
                else clippingObject.ClippingPlaneGeometry.RemoveClipViewportId(viewportId);
            }

            clippingObject.CommitChanges();
        }

        private void SetName(string name)
        {
            var clippingObject = GetClippingPlaneObject();
            clippingObject.Attributes.Name = name;

            clippingObject.CommitChanges();
        }

        public void AddClippedViewport(RhinoViewport vp)
        {
            var clippingObject = GetClippingPlaneObject();
            clippingObject.AddClipViewport(vp, true);
            clippingObject.CommitChanges();
        }

        public BoundingBox GetBoundingBox()
        {
            return GetClippingPlaneObject().ClippingPlaneGeometry.GetBoundingBox(Plane);
        }
    }
}
