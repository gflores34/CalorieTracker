using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalorieTracker
{
    internal class CustomColorTable : ProfessionalColorTable
    {
        public CustomColorTable()
        {
            base.UseSystemColors = false;
        }

        public override Color ToolStripBorder
        {
            get { return Color.FromArgb(0, 0, 0); }
        }
        public override Color ToolStripDropDownBackground
        {
            get { return Color.FromArgb(64, 64, 64); }
        }
        public override Color ToolStripGradientBegin
        {
            get { return Color.FromArgb(64, 64, 64); }
        }
        public override Color ToolStripGradientEnd
        {
            get { return Color.FromArgb(64, 64, 64); }
        }
        public override Color ToolStripGradientMiddle
        {
            get { return Color.FromArgb(64, 64, 64); }
        }
    }
}
