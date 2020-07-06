using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACSMinCapture
{
    static class AllControls
    {
        public static IEnumerable<System.Windows.Forms.Control> All(this System.Windows.Forms.Control.ControlCollection controls)
        {
            foreach (System.Windows.Forms.Control control in controls)
            {
                foreach (System.Windows.Forms.Control grandChild in control.Controls.All())
                    yield return grandChild;

                yield return control;
            }
        }
    }
}
