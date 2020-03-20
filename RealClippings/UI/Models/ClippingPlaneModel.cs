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
        private HashSet<Guid> _viewportIds;

        private bool _isActive;
        private string _name;

        /// <summary>
        /// Bounded property notifying if the model is currently clipping any viewports
        /// </summary>
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                SetPlaneActive(_isActive);
            }
        }

        /// <summary>
        /// Bounded property for the name of the model
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                SetName(_name);
            }
        }

        /// <summary>
        /// The Plane associated with the models ClippingPlane
        /// </summary>
        public Plane Plane => GetPlane();

        /// <summary>
        /// The Guid associated with the models ClippingPlane
        /// </summary>
        public Guid Guid => _guid;

        public ClippingPlaneModel(RhinoDoc doc, ClippingPlaneObject clippingPlaneObject)
        {
            _doc = doc;
            _guid = clippingPlaneObject.Id;
            _viewportIds = new HashSet<Guid>(clippingPlaneObject.ClippingPlaneGeometry.ViewportIds());

            _isActive = _viewportIds.Count > 0;
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

        /// <summary>
        /// Adds a Viewport to be clipped to the models ClippingViewport List
        /// </summary>
        /// <param name="vp">The viewport to be clipped</param>
        public void AddClippedViewport(RhinoViewport vp)
        {
            if (_viewportIds.Add(vp.Id))
            {
                var clippingObject = GetClippingPlaneObject();
                clippingObject.AddClipViewport(vp, true);
                clippingObject.CommitChanges();
            }
        }

        /// <summary>
        /// Gets the bounding box of the models Geometry
        /// </summary>
        /// <param name="plane"></param>
        /// <returns></returns>
        public BoundingBox GetBoundingBox(Plane plane)
        {
            var bbox = GetClippingPlaneObject().Geometry.GetBoundingBox(Plane);
            bbox.Transform(Transform.Translation(Plane.Origin - plane.Origin));

            return bbox;
        }
    }
}
