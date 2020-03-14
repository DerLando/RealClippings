using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;
using Rhino.UI;
using Size = Eto.Drawing.Size;

namespace RealClippings.UI.Views
{
    [Guid("A4E5FB42-0AE8-4D6B-90DD-0B3C889651E0")]
    public class ClippingPlaneManagerPanel: Panel, IPanel
    {
        // Fields
        private readonly uint _document_sn;

        // Controls
        private DynamicGroup _gB_Search = new DynamicGroup() {Title = "Search"};
        private SearchBox _sB_Search = new SearchBox();
        private ListBox _lB_ClippingPlanes = new ListBox();

        // Auto-initialized properties
        public static Guid PanelId => typeof(ClippingPlaneManagerPanel).GUID;

        public ClippingPlaneManagerPanel(uint documentSerialNumber)
        {
            //sn field
            _document_sn = documentSerialNumber;

            // Set up group boxes
            _gB_Search.Add(_sB_Search);

            // write layout
            var layout = new DynamicLayout();
            layout.Padding = 10;
            layout.Spacing = new Size(5, 5);

            layout.Add(_gB_Search.Create(layout));
            layout.Add(_lB_ClippingPlanes);
            layout.Add(null);

            Content = layout;
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
