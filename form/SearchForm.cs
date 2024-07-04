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
    public partial class SearchForm : Form
    {
        private fNhanVien parentForm;

        // Constructor mặc định
        public SearchForm()
        {
            InitializeComponent();
        }

        // Constructor nhận đối số (nếu cần thiết)
        public SearchForm(fNhanVien parent)
        {
            InitializeComponent();
            this.parentForm = parent;
        }

        // Thêm phương thức để thiết lập form cha (nếu cần thiết)
        public void SetParentForm(fNhanVien parent)
        {
            this.parentForm = parent;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txbSearch.Text.Trim();
            parentForm.PerformSearch(searchText); // Gọi phương thức PerformSearch của Form2
            this.Close(); // Đóng form tìm kiếm sau khi tìm kiếm
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            string searchText = txbSearch.Text.Trim();
            parentForm.PerformSearch(searchText); // Gọi phương thức PerformSearch của Form2
            this.Close(); // Đóng form tìm kiếm sau khi tìm kiếm
        }
    }
}
