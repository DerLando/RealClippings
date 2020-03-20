using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;
using RealClippings.UI.Models;

namespace RealClippings.UI.EtoCommands
{
    /// <summary>
    /// Abstract base class for all commands acting on clipping planes
    /// </summary>
    public abstract class ClippingPlaneCommandBase : Command
    {
        protected ClippingPlaneModel[] _models = new ClippingPlaneModel[0];

        public void SetClippingPlanes(IEnumerable<ClippingPlaneModel> models)
        {
            if (models is null) return;
            _models = models.ToArray();
        }
    }
}
