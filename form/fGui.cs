using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    

    public partial class fGui : Form
    {
        private XuLyDuLieu xuLyDuLieu;
        public fGui()
        {
            InitializeComponent();
        }
        public fGui(string nhan)
        {
            InitializeComponent();
            txbNhantuformNhan.Text = nhan;
        }

        private void btnGui_Click(object sender, EventArgs e)
        {
            fNhan nhan = new fNhan(txbXuly.Text);
            
            nhan.Show();
        }

        private void txbXuly_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fNhan nhan = new fNhan(txbXuly.Text);
                
                nhan.Show();
            }
        }
    }
}
