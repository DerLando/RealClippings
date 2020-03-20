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
        private readonly SelectCommand _selectCommand = new SelectCommand();

        public ClippingPlaneGridContextMenu(GridView parent)
        {
            _gV_parent = parent;

            _gV_parent.SelectedItemsChanged += On_Parent_SelectedItemsChanged;

            InitializeCommands();

            // Selection
            Items.Add(_selectCommand.CreateMenuItem());
            Items.Add(new SeparatorMenuItem());

            // Visuals
            Items.Add(_planViewCommand.CreateMenuItem());
            Items.Add(new SeparatorMenuItem());
        }

        private void On_Parent_SelectedItemsChanged(object sender, EventArgs e)
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            var models = TryGetParentSelectedModels();

            _planViewCommand.SetClippingPlanes(models);
            _selectCommand.SetClippingPlanes(models);
        }

        private IEnumerable<ClippingPlaneModel> TryGetParentSelectedModels()
        {
            if (_gV_parent.SelectedItems is null) return null;

            return from obj in _gV_parent.SelectedItems select (ClippingPlaneModel) obj;
        }
    }
}
