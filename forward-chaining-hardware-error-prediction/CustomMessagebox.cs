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
    public partial class CustomMessagebox : Form
    {
        public CustomMessagebox()
        {
            InitializeComponent();
        }

        private void CustomMessagebox_Load(object sender, EventArgs e)
        {

        }

       
        public void Show(string message)
        {
            lblKetqua.Text = message;
        }
    }
}
