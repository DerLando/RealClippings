using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;
using RealClippings.UI.Models;

namespace RealClippings.UI.EtoCommands
{
    public abstract class ClippingPlaneCommandBase : Command
    {
        protected ClippingPlaneModel _model = null;

        public void SetClippingPlane(ClippingPlaneModel model)
        {
            _model = model;
        }
    }
}
