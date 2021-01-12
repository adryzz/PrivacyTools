using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrivacyTools
{
    public partial class Overlay : Form
    {
        public Overlay()
        {
            InitializeComponent();
        }

        private void Overlay_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = Enabled;
        }

        private void Overlay_Load(object sender, EventArgs e)
        {
            Cursor.Hide();
        }
    }
}
