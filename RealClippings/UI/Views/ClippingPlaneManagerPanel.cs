using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;
using RealClippings.UI.Models;
using Rhino.UI;
using Size = Eto.Drawing.Size;

namespace RealClippings.UI.Views
{
    [Guid("A4E5FB42-0AE8-4D6B-90DD-0B3C889651E0")]
    public class ClippingPlaneManagerPanel: Panel, IPanel
    {
        // Fields
        private readonly uint _document_sn;
        private ClippingPlaneListViewModel _clipping_plane_list_model;

        // Controls
        private SearchBox _sB_Search = new SearchBox{PlaceholderText = "Name"};
        private GridView _gV_ClippingPlanes = new GridView();

        // Auto-initialized properties
        public static Guid PanelId => typeof(ClippingPlaneManagerPanel).GUID;
        public bool IsModelInitialized => !(_clipping_plane_list_model is null);

        public ClippingPlaneManagerPanel(uint documentSerialNumber)
        {
            //sn field
            _document_sn = documentSerialNumber;

            // Set up context menu
            _gV_ClippingPlanes.ContextMenu = new ClippingPlaneGridContextMenu(_gV_ClippingPlanes);

            // Set up grid view
            _gV_ClippingPlanes.ShowHeader = true;
            _gV_ClippingPlanes.AllowMultipleSelection = true;

            #region Grid columns

            // name
            _gV_ClippingPlanes.Columns.Add(new GridColumn
            {
                HeaderText = "Name",
                DataCell = new TextBoxCell("Name"),
                Editable = true,
            });

            // active
            _gV_ClippingPlanes.Columns.Add(new GridColumn
            {
                HeaderText = "Active",
                DataCell = new CheckBoxCell("IsActive"),
                Editable = true,
            });

            #endregion

            // write layout
            var layout = new DynamicLayout();
            layout.Padding = 10;
            layout.Spacing = new Size(5, 5);

            layout.Add(_sB_Search);
            layout.Add(_gV_ClippingPlanes);
            layout.Add(null);

            Content = layout;
        }

        public void SetClippingPlaneListModel(ClippingPlaneListViewModel model)
        {
            _clipping_plane_list_model = model;

            // set up bindings for grid view
            _gV_ClippingPlanes.DataContext = _clipping_plane_list_model;
            _gV_ClippingPlanes.DataStore = _clipping_plane_list_model.Planes;
            _gV_ClippingPlanes.SelectedItemBinding.BindDataContext((ClippingPlaneListViewModel avm) =>
            avm.SelectedClippingPlane);

            // redraw
            _gV_ClippingPlanes.Invalidate();
        }

        #region IPanel methods

        public void PanelShown(uint documentSerialNumber, ShowPanelReason reason)
        {
            // Called when the panel tab is made visible, in Mac Rhino this will happen
            // for a document panel when a new document becomes active, the previous
            // documents panel will get hidden and the new current panel will get shown.
            Rhino.RhinoApp.WriteLine($"Panel shown for document {documentSerialNumber}, this serial number {_document_sn} should be the same");
        }

        public void PanelHidden(uint documentSerialNumber, ShowPanelReason reason)
        {
            // Called when the panel tab is hidden, in Mac Rhino this will happen
            // for a document panel when a new document becomes active, the previous
            // documents panel will get hidden and the new current panel will get shown.
            Rhino.RhinoApp.WriteLine($"Panel hidden for document {documentSerialNumber}, this serial number {_document_sn} should be the same");
        }

        public void PanelClosing(uint documentSerialNumber, bool onCloseDocument)
        {
            // Called when the document or panel container is closed/destroyed
            Rhino.RhinoApp.WriteLine($"Panel closing for document {documentSerialNumber}, this serial number {_document_sn} should be the same");
        }

        #endregion IPanel methods
    }
}
