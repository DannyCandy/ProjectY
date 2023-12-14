using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hardware_prediction_expert_system
{
    public partial class HPES : Form
    {
        public HPES()
        {
            InitializeComponent();
        }

        private void HPES_Load(object sender, EventArgs e)
        {
            // Chọn TabPage1 là mặc định
            FormDescription fd = new FormDescription();
            
            fd.TopLevel = false;
            fd.FormBorderStyle = FormBorderStyle.None;
            fd.WindowState = FormWindowState.Maximized;
            fd.Dock = DockStyle.Fill;
            tabPage1.Controls.Add(fd);
            fd.Show();
        }
    }
}
