using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.DocObjects;

namespace RealClippings.UI.Models
{
    public class ClippingPlaneListViewModel
    {
        private int _selectedClippingPlane;

        public ObservableCollection<ClippingPlaneModel> Planes { get; set; } = new ObservableCollection<ClippingPlaneModel>();

        public int SelectedClippingPlane
        {
            get => _selectedClippingPlane;
            set { _selectedClippingPlane = value; }
        }
        public ClippingPlaneListViewModel(RhinoDoc doc)
        {
            var clippingPlanes = doc.Objects.GetObjectsByType<ClippingPlaneObject>();
            foreach (var clippingPlaneObject in clippingPlanes)
            {
                Planes.Add(new ClippingPlaneModel(doc, clippingPlaneObject));
            }
        }
    }
}
