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
    public partial class fNhan : Form
    {
        private XuLyDuLieu xuLyDuLieu;
        public fNhan()
        {
            InitializeComponent();
        }
        
        public fNhan(string gui)
        {
            InitializeComponent();
            txbNhantuformGui.Text = gui;
        }
        //public fNhan(XuLyDuLieu xuLyDuLieu)
        //{
        //    this.xuLyDuLieu = xuLyDuLieu;
        //    InitializeComponent();
        //}

        private void btnGui_Click(object sender, EventArgs e)
        {
            fGui gui = new fGui(txbXuly.Text);
            
            gui.Show();

        }

        private void txbXuly_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fGui gui = new fGui(txbXuly.Text);
                
                gui.Show();
            }
        }


    }
}
