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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_ShowMessage_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Xin chào!", "Tiêu đề", 
            //    MessageBoxButtons.OK, MessageBoxIcon.Hand);
            fNhanVien form2 = new fNhanVien();
            form2.ShowDialog();
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            Console.WriteLine("Move");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Load");
        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Visible");
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Activated");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Console.WriteLine("Shown");
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Console.WriteLine("Paint");
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            Console.WriteLine("Deactivate");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("Closing");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.WriteLine("Closed");
        }
    }
}
