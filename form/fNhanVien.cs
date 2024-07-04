using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace WindowsFormsApp
{
    public partial class fNhanVien : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QLCF"].ConnectionString;
        ErrorProvider errorProvider = new ErrorProvider();
        DataView dv = new DataView();

        public fNhanVien()
        {
            InitializeComponent();
            dgvNV.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvNV_CellFormatting);
            btnKhoiTao.Click += new EventHandler(btnKhoiTao_Click);           

            btnSearch.Enabled = true;

            // Đăng ký sự kiện TextChanged và Validating cho các ô nhập liệu
            txbMaNV.TextChanged += new EventHandler(txbMaNV_TextChanged);
            txbHoTenNV.TextChanged += new EventHandler(txbHoTenNV_TextChanged);
            txbSDTNV.TextChanged += new EventHandler(txbSDTNV_TextChanged);
            txbEmailNV.TextChanged += new EventHandler(txbEmailNV_TextChanged);

            txbMaNV.Validating += new CancelEventHandler(txbMaNV_Validating);
            txbHoTenNV.Validating += new CancelEventHandler(txbHoTenNV_Validating);
            txbSDTNV.Validating += new CancelEventHandler(txbSDTNV_Validating);
            txbEmailNV.Validating += new CancelEventHandler(txbEmailNV_Validating);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadData();
            CheckInputFields();
        }


        private void LoadData()
        {
            string querySelect = "Select_NhanVien"; // stored procedure

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = querySelect;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter())
                        {
                            adapter.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                adapter.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {
                                    dv = dt.DefaultView;
                                    dgvNV.AutoGenerateColumns = false;
                                    dgvNV.DataSource = dv;
                                }
                                else
                                {
                                    MessageBox.Show("Không tồn tại bản ghi nào");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void dgvNV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvNV.Columns[e.ColumnIndex].Name == "GioiTinh")
            {
                if (e.Value != null)
                {
                    bool gioiTinh = (bool)e.Value;
                    e.Value = gioiTinh ? "Nam" : "Nữ";
                    e.FormattingApplied = true;
                }
            }
        }

        private void CheckInputFields()
        {
            bool isAddValid = true;
            bool isInitValid = false;

            if (!string.IsNullOrEmpty(txbMaNV.Text) &&
                !string.IsNullOrEmpty(txbHoTenNV.Text) &&
                !string.IsNullOrEmpty(txbSDTNV.Text) && System.Text.RegularExpressions.Regex.IsMatch(txbSDTNV.Text, @"^\d+$") &&
                !string.IsNullOrEmpty(txbEmailNV.Text) && System.Text.RegularExpressions.Regex.IsMatch(txbEmailNV.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                isAddValid = true;
            }
            else
            {
                isAddValid = false;
            }

            if (!string.IsNullOrEmpty(txbMaNV.Text) ||
                !string.IsNullOrEmpty(txbHoTenNV.Text) ||
                !string.IsNullOrEmpty(txbSDTNV.Text) ||
                !string.IsNullOrEmpty(txbEmailNV.Text))
            {
                isInitValid = true;
            }
            else
            {
                isInitValid = false;
            }

            btnAdd.Enabled = isAddValid;
            btnKhoiTao.Enabled = isInitValid;
        }


        private void txbMaNV_TextChanged(object sender, EventArgs e)
        {
            CheckInputFields();
        }

        private void txbHoTenNV_TextChanged(object sender, EventArgs e)
        {
            CheckInputFields();
        }

        private void txbSDTNV_TextChanged(object sender, EventArgs e)
        {
            CheckInputFields();
        }

        private void txbEmailNV_TextChanged(object sender, EventArgs e)
        {
            CheckInputFields();
        }

        private void txbMaNV_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbMaNV.Text))
            {
                errorProvider.SetError(txbMaNV, "Mã nhân viên không được để trống");
            }
            else
            {
                errorProvider.SetError(txbMaNV, null);
            }
            CheckInputFields();
        }

        private void txbHoTenNV_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbHoTenNV.Text))
            {
                errorProvider.SetError(txbHoTenNV, "Họ tên không được để trống");
            }
            else
            {
                errorProvider.SetError(txbHoTenNV, null);
            }
            CheckInputFields();
        }

        private void txbSDTNV_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbSDTNV.Text) || !System.Text.RegularExpressions.Regex.IsMatch(txbSDTNV.Text, @"^\d+$"))
            {
                errorProvider.SetError(txbSDTNV, "Số điện thoại không hợp lệ");
            }
            else
            {
                errorProvider.SetError(txbSDTNV, null);
            }
            CheckInputFields();
        }

        private void txbEmailNV_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbEmailNV.Text) || !System.Text.RegularExpressions.Regex.IsMatch(txbEmailNV.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorProvider.SetError(txbEmailNV, "Email không hợp lệ");
            }
            else
            {
                errorProvider.SetError(txbEmailNV, null);
            }
            CheckInputFields();
        }


        private void btnKhoiTao_Click(object sender, EventArgs e)
        {
            // Xóa giá trị trong các ô nhập liệu
            txbMaNV.Text = string.Empty;
            txbMaNV.ReadOnly = false;
            txbHoTenNV.Text = string.Empty;
            txbSDTNV.Text = string.Empty;
            txbEmailNV.Text = string.Empty;
            dtpNgaySinh.Value = DateTime.Now;
            rb_nam.Checked = false;
            rb_nu.Checked = false;

            CheckInputFields(); // Kiểm tra lại trạng thái của nút
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void dgvNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Lấy dữ liệu từ dòng được chọn trong DataGridView
            int index = dgvNV.CurrentRow.Index;
            txbMaNV.Text = dv[index]["id"].ToString();
            txbMaNV.ReadOnly = true;
            txbHoTenNV.Text = dv[index]["TenNV"].ToString();
            txbSDTNV.Text = dv[index]["SDTNV"].ToString();
            txbEmailNV.Text = dv[index]["emailNV"].ToString();
            dtpNgaySinh.Value = Convert.ToDateTime(dv[index]["NgaySinh"]);
            rb_nam.Checked = (bool)(dv[index]["GioiTinh"]);
            rb_nu.Checked = !(bool)(dv[index]["GioiTinh"]);

            // Ẩn nút "Thêm mới", hiển thị nút "Sửa"
            btnAdd.Enabled = false;
            btnEdit.Enabled = true;
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            int index = dgvNV.CurrentRow.Index;
            int id = Convert.ToInt32(dv[index]["id"]);

            try
            {
                DialogResult dialogResult = MessageBox.Show("Có chắc muốn xoá mã NV " + id + " không?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dialogResult == DialogResult.Yes)
                {
                    bool success = DeleteNhanVien(id);

                    if (success)
                    {
                        MessageBox.Show("Xóa nhân viên thành công.");
                        LoadData(); // Load lại dữ liệu sau khi xóa
                    }
                    else
                    {
                        MessageBox.Show("Xóa nhân viên thất bại. Vui lòng thử lại.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private bool DeleteNhanVien(int id)
        {
            string strDelete = "Delete_NhanVien";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();

                try
                {
                    adapter.DeleteCommand = new SqlCommand(strDelete, conn);
                    adapter.DeleteCommand.CommandType = CommandType.StoredProcedure;
                    adapter.DeleteCommand.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    int rowsAffected = adapter.DeleteCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Lỗi SQL: " + ex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                    return false;
                }
            }
        }




        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các ô nhập liệu trên form
            int id = Convert.ToInt32(txbMaNV.Text);
            string tenNV = txbHoTenNV.Text;
            string sdtNV = txbSDTNV.Text;
            string emailNV = txbEmailNV.Text;
            bool gioiTinh = rb_nam.Checked; // True nếu là Nam, False nếu là Nữ
            DateTime ngaySinh = dtpNgaySinh.Value;

            // Thực hiện thêm nhân viên vào cơ sở dữ liệu
            string connectionString = ConfigurationManager.ConnectionStrings["QLCF"].ConnectionString;
            bool success = ThemNhanVien(connectionString, id, tenNV, sdtNV, emailNV, gioiTinh, ngaySinh);

            if (success)
            {
                MessageBox.Show("Thêm nhân viên thành công.");
                
                LoadData(); // Ví dụ, load lại dữ liệu từ cơ sở dữ liệu
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại. Vui lòng thử lại.");
            }
        }

        private static bool ThemNhanVien(string connectionString, int id, string tenNV, string sdtNV, string emailNV, bool gioiTinh, DateTime ngaySinh)
        {
            string strInsert = "Insert_NhanVien";

            // Khởi tạo SqlDataAdapter với chuỗi kết nối
            SqlDataAdapter adapter = new SqlDataAdapter();

            try
            {
                // Tạo SqlConnection từ chuỗi kết nối
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Tạo SqlCommand cho Insert_NhanVien stored procedure
                    adapter.InsertCommand = new SqlCommand(strInsert, conn);
                    adapter.InsertCommand.CommandType = CommandType.StoredProcedure;

                    // Thêm các tham số cho InsertCommand
                    adapter.InsertCommand.Parameters.AddWithValue("@id", id);
                    adapter.InsertCommand.Parameters.AddWithValue("@TenNV", tenNV);
                    adapter.InsertCommand.Parameters.AddWithValue("@SDTNV", sdtNV);
                    adapter.InsertCommand.Parameters.AddWithValue("@emailNV", emailNV);
                    adapter.InsertCommand.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    adapter.InsertCommand.Parameters.AddWithValue("@NgaySinh", ngaySinh);

                    // Mở kết nối và thực thi thêm mới
                    conn.Open();
                    int rowsAffected = adapter.InsertCommand.ExecuteNonQuery();

                    // Trả về true nếu thêm thành công, false nếu không
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Lỗi SQL: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
                return false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các ô nhập liệu trên form
            int id = Convert.ToInt32(txbMaNV.Text);
            string tenNV = txbHoTenNV.Text;
            string sdtNV = txbSDTNV.Text;
            string emailNV = txbEmailNV.Text;
            bool gioiTinh = rb_nam.Checked; // True nếu là Nam, False nếu là Nữ
            DateTime ngaySinh = dtpNgaySinh.Value;

            // Thực hiện cập nhật nhân viên vào cơ sở dữ liệu
            string connectionString = ConfigurationManager.ConnectionStrings["QLCF"].ConnectionString;
            bool success = UpdateNhanVien(connectionString, id, tenNV, sdtNV, emailNV, gioiTinh, ngaySinh);

            if (success)
            {
                MessageBox.Show("Cập nhật nhân viên thành công.");

                LoadData(); // Ví dụ, load lại dữ liệu từ cơ sở dữ liệu
            }
            else
            {
                MessageBox.Show("Cập nhật nhân viên thất bại. Vui lòng thử lại.");
            }
        }

        private static bool UpdateNhanVien(string connectionString, int id, string tenNV, string sdtNV, string emailNV, bool gioiTinh, DateTime ngaySinh)
        {
            string strUpdate = "Update_NhanVien";

            // Khởi tạo SqlDataAdapter với chuỗi kết nối
            SqlDataAdapter adapter = new SqlDataAdapter();

            try
            {
                // Tạo SqlConnection từ chuỗi kết nối
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Tạo SqlCommand cho Update_NhanVien stored procedure
                    adapter.UpdateCommand = new SqlCommand(strUpdate, conn);
                    adapter.UpdateCommand.CommandType = CommandType.StoredProcedure;

                    // Thêm các tham số cho UpdateCommand
                    adapter.UpdateCommand.Parameters.AddWithValue("@id", id);
                    adapter.UpdateCommand.Parameters.AddWithValue("@TenNV", tenNV);
                    adapter.UpdateCommand.Parameters.AddWithValue("@SDTNV", sdtNV);
                    adapter.UpdateCommand.Parameters.AddWithValue("@emailNV", emailNV);
                    adapter.UpdateCommand.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    adapter.UpdateCommand.Parameters.AddWithValue("@NgaySinh", ngaySinh);

                    // Mở kết nối và thực thi cập nhật
                    conn.Open();
                    int rowsAffected = adapter.UpdateCommand.ExecuteNonQuery();

                    // Trả về true nếu cập nhật thành công, false nếu không
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Lỗi SQL: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
                return false;
            }
        }

        public void PerformSearch(string searchText)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Search_NhanVien", conn)) // Thay đổi tên stored procedure tương ứng
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TenNV", searchText);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                dv = dt.DefaultView;
                                dgvNV.DataSource = dv;
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy nhân viên có tên: " + searchText);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm(this);
            searchForm.ShowDialog();
        }
    }
}
