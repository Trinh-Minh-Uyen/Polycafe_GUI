using Polycafe_BUS;
using Polycafe_DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO; // Make sure this is included for Path.GetFileName
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Polycafe_GUI
{
    public partial class QuanLySanPham : UserControl
    {
        private string imagePath;

        // Define the base path where your images are stored.
        // Make sure this path exists and your application has read/write permissions to it.
        // If you want to store only the filename in the database, you MUST ensure the image files
        // are physically present in this directory.
        private const string BaseImagePath = "C:\\Users\\ADMIN\\OneDrive\\Tài liệu\\Dự Án Mẫu_SOF2052\\ASM_Project\\QuanLyBanHang\\Dự Án";

        public QuanLySanPham()
        {
            InitializeComponent();
            LoadData(); // Initial load of product data into the DataGridView
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        public void LoadData()
        {
            SanPhamBUS sanPhamBUS = new SanPhamBUS();
            DataTable dt = sanPhamBUS.GetAllSanPham();
            dataGridView1.DataSource = dt;
            comBoLoaiSP.DataSource = sanPhamBUS.GetLoaiSanPham();
            comBoLoaiSP.DisplayMember = "TenLoai";
            comBoLoaiSP.ValueMember = "MaLoai";
            comBofound.DataSource = dt; // Add this line to set the DataSource
            comBofound.DisplayMember = "MaSanPham";
            comBofound.ValueMember = "MaSanPham";
        }


        // Removed LoadCombo() method as it was incorrectly trying to bind employee data
        // to a product search combobox, and comBofound seems to be used as a text input for search.

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // This event handler can remain empty if you don't need specific logic
            // when the picture box itself is clicked, beyond selecting an image.
        }

        // Empty event handlers like these can be removed if they don't contain any logic.
        private void label7_Click(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) { }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string maSanPham = txtMaSP.Text.Trim();
                string tenSanPham = txtTenSP.Text.Trim();
                string maLoai = comBoLoaiSP.SelectedValue?.ToString();
                decimal donGia = decimal.TryParse(txtDonGia.Text, out decimal dg) ? dg : 0;
                string trangThai = radioButton1.Checked ? "1" : "0";

                // Get filename from pictureBox1.Tag.
                // If pictureBox1.Tag contains the full path, we extract just the filename
                // to store in the database, assuming you want to use BaseImagePath for loading.
                string hinhAnh = (pictureBox1.Tag != null) ? Path.GetFileName(pictureBox1.Tag.ToString()) : string.Empty;

                if (string.IsNullOrEmpty(maSanPham) || string.IsNullOrEmpty(tenSanPham) || string.IsNullOrEmpty(maLoai))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SanPhamDTO sanPham = new SanPhamDTO
                {
                    MaSanPham = maSanPham,
                    TenSanPham = tenSanPham,
                    MaLoai = maLoai,
                    DonGia = donGia,
                    TrangThai = trangThai,
                    HinhAnh = hinhAnh // This now contains only the filename
                };

                SanPhamBUS sanPhamBUS = new SanPhamBUS();
                if (sanPhamBUS.AddSanPham(sanPham))
                {
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Reload data to show the new product
                    ClearForm(); // Clear input fields after successful add
                }
                else
                {
                    MessageBox.Show("Thêm sản phẩm thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Chọn hình ảnh",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imagePath = ofd.FileName; // This is the full path selected by the user
                pictureBox1.Image = Image.FromFile(imagePath);
                // Store the full image path in the Tag property for later use (e.g., extracting filename for DB)
                pictureBox1.Tag = imagePath;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string maSanPham = txtMaSP.Text.Trim();
                string tenSanPham = txtTenSP.Text.Trim();
                string maLoai = comBoLoaiSP.SelectedValue?.ToString();
                decimal donGia = decimal.TryParse(txtDonGia.Text, out decimal dg) ? dg : 0;
                string trangThai = radioButton1.Checked ? "1" : "0";

                // Similar to Add, extract filename from Tag
                string hinhAnh = (pictureBox1.Tag != null) ? Path.GetFileName(pictureBox1.Tag.ToString()) : string.Empty;

                if (string.IsNullOrEmpty(maSanPham))
                {
                    MessageBox.Show("Vui lòng nhập mã sản phẩm để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SanPhamDTO sp = new SanPhamDTO
                {
                    MaSanPham = maSanPham,
                    TenSanPham = tenSanPham,
                    MaLoai = maLoai,
                    DonGia = donGia,
                    TrangThai = trangThai,
                    HinhAnh = hinhAnh // This now contains only the filename
                };

                SanPhamBUS bus = new SanPhamBUS();
                if (bus.UpdateSanPham(sp))
                {
                    MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo");
                    LoadData(); // Reload data to show the updated product
                    ClearForm(); // Clear input fields after successful update
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật sản phẩm: " + ex.Message);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSP.Text.Trim();

            if (string.IsNullOrEmpty(maSP))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm cần xóa.", "Thông báo");
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SanPhamBUS bus = new SanPhamBUS();
                if (bus.DeleteSanPham(maSP))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadData(); // Reload data after deletion
                    ClearForm(); // Clear input fields after successful delete
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // comBofound is treated as a simple text input for the search keyword.
            string keyword = comBofound.Text.Trim();
            SanPhamBUS bus = new SanPhamBUS();
            DataTable dt = bus.SearchSanPham(keyword); // Call the corrected SearchSanPham
            dataGridView1.DataSource = dt;

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sản phẩm nào với từ khóa này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtMaSP.Text = row.Cells["MaSanPham"].Value.ToString();
                txtTenSP.Text = row.Cells["TenSanPham"].Value.ToString();
                comBoLoaiSP.SelectedValue = row.Cells["MaLoai"].Value.ToString();
                txtDonGia.Text = row.Cells["DonGia"].Value.ToString();

                // Get filename from DB and construct full path
                // Assuming 'HinhAnh' column in DB stores only the filename (e.g., "caphe_den.jpg")
                string imageFileNameFromDB = row.Cells["HinhAnh"].Value.ToString();
                string fullPathToLoad = Path.Combine(BaseImagePath, imageFileNameFromDB);

                if (row.Cells["TrangThai"].Value.ToString() == "1")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                try
                {
                    // Store the full path in pictureBox1.Tag, so if the user clicks "Update"
                    // without changing the image, the correct filename can be retrieved.
                    pictureBox1.Tag = fullPathToLoad;
                    pictureBox1.Image = Image.FromFile(fullPathToLoad);
                }
                catch (Exception ex)
                {
                    // Provide a more informative error message
                    MessageBox.Show($"Không tìm thấy hình ảnh tại đường dẫn: {fullPathToLoad}\n(Kiểm tra xem file '{imageFileNameFromDB}' có tồn tại trong thư mục '{BaseImagePath}' không.)\nLỗi chi tiết: {ex.Message}", "Lỗi tải ảnh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pictureBox1.Image = null; // Clear the image if not found
                    pictureBox1.Tag = null; // Clear the tag as well
                }
            }
        }

        // Optional: A helper method to clear input fields
        private void ClearForm()
        {
            txtMaSP.Text = string.Empty;
            txtTenSP.Text = string.Empty;
            txtDonGia.Text = string.Empty;
            comBoLoaiSP.SelectedIndex = -1; // Deselect any item
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            pictureBox1.Image = null; // Clear the displayed image
            pictureBox1.Tag = null; // Clear the stored path
        }

        private void QuanLySanPham_Load(object sender, EventArgs e)
        {
            // LoadData() is already called in the constructor, so calling it here again might be redundant
            // unless you have specific reasons for a full reload on Load event.
            // If you want to ensure data is fresh when the control is shown/re-shown, keep it.
            // If it's a UserControl, its constructor might be called once, and Load event multiple times.
            // So, keeping LoadData here is generally safer for ensuring fresh data.
            LoadData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            txtMaSP.Text = string.Empty;
            txtTenSP.Text = string.Empty;
            txtDonGia.Text = string.Empty;
            comBoLoaiSP.SelectedIndex = -1; // Deselect any item
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            pictureBox1.Image = null; // Clear the displayed image
            pictureBox1.Tag = null; // Clear the stored path
            comBofound.Text = string.Empty; // Clear the search input
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

