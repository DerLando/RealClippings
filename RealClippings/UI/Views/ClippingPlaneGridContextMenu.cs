using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;
using RealClippings.UI.EtoCommands;
using RealClippings.UI.Models;

namespace RealClippings.UI.Views
{
    public class ClippingPlaneGridContextMenu : ContextMenu
    {
        private readonly GridView _gV_parent;
        private readonly MakePlanViewCommand _planViewCommand = new MakePlanViewCommand();

        public ClippingPlaneGridContextMenu(GridView parent)
        {
            _gV_parent = parent;

            _gV_parent.SelectedRowsChanged += On_Parent_SelectedRowChanged;

            InitializeCommands();

            // Selection

            // Visuals
            Items.Add(_planViewCommand.CreateMenuItem());
            Items.Add(new SeparatorMenuItem());
        }

        private void On_Parent_SelectedRowChanged(object sender, EventArgs e)
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            var model = TryGetParentSelectedModel();

            _planViewCommand.SetClippingPlane(model);
        }

        private ClippingPlaneModel TryGetParentSelectedModel()
        {
            if (_gV_parent.SelectedItem is null) return null;

            return (ClippingPlaneModel) _gV_parent.SelectedItem;
        }
    }
}
