using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Đảm bảo đã import
using Polycafe_BUS;
using Polycafe_DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement; // Có thể không cần thiết nếu không sử dụng VisualStyleElement

namespace Polycafe_GUI
{
    public partial class QLLoaiSanPham : UserControl
    {
        private qlLSP_BUS bus = new qlLSP_BUS();
        private DataTable originalData; // Để lưu trữ dữ liệu gốc cho chức năng tìm kiếm/đặt lại

        public QLLoaiSanPham()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void LoadData()
        {
            try
            {
                originalData = bus.get(); // Lấy dữ liệu mẫu từ CSDL

                if (originalData != null && originalData.Rows.Count > 0)
                {
                    dataGridView1.DataSource = originalData;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi truy xuất dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }


        private void LoadCombo()
        {
            try
            {
                var listmaloai = bus.Loadmaloai();
                if (listmaloai != null)
                {
                    comboBox1.DataSource = listmaloai;
                    comboBox1.SelectedIndex = -1; // Đặt không chọn gì ban đầu
                }
                else
                {
                    MessageBox.Show("Không thể tải dữ liệu Mã loại cho ComboBox.", "Lỗi Tải ComboBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi Load dữ liệu ComboBox: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            textBox4.Clear();
            textBox3.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
        }

     

        private void button1_Click(object sender, EventArgs e) // Thêm
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Vui lòng không để trống Mã loại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (bus.check(textBox2.Text))
            {
                MessageBox.Show("Mã loại đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var loai = new qlLSP
            {
                MaLoai = textBox2.Text,
                TenLoai = textBox3.Text,
                GhiChu = textBox4.Text
            };

            if (bus.add(loai))
            {
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                LoadCombo(); // Cập nhật lại combo box sau khi thêm
                ClearFields();
            }
            else
            {
                MessageBox.Show("Thêm thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e) // Cập nhật
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã loại để cập nhật", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!bus.check(textBox2.Text))
            {
                MessageBox.Show("Mã loại không tồn tại để cập nhật", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var loai = new qlLSP
            {
                MaLoai = textBox2.Text,
                TenLoai = textBox3.Text,
                GhiChu = textBox4.Text
            };

            if (bus.update(loai))
            {
                MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                LoadCombo(); // Cập nhật lại combo box sau khi cập nhật
                ClearFields();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e) // Xóa
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Vui lòng chọn Mã loại để xóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var lsp = new qlLSP { MaLoai = textBox2.Text };

            if (MessageBox.Show("Bạn có chắc muốn xóa loại sản phẩm có mã '" + textBox2.Text + "' này không?", "Xác nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (bus.delete(lsp))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    LoadCombo(); // Cập nhật lại combo box sau khi xóa
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại. Có thể Mã loại này đang được sử dụng ở nơi khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e) // Đặt lại / Hiển thị tất cả
        {
            LoadData(); // Tải lại toàn bộ dữ liệu gốc
            ClearFields(); // Xóa các trường nhập liệu
            comboBox1.SelectedIndex = -1; // Đặt lại ComboBox
        }

        private void button5_Click(object sender, EventArgs e) // Tìm kiếm
        {
            string selectedMaLoai = comboBox1.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedMaLoai))
            {
                // Đảm bảo originalData đã được tải
                if (originalData != null)
                {
                    DataView dv = new DataView(originalData);
                    dv.RowFilter = $"MaLoai = '{selectedMaLoai}'";
                    dataGridView1.DataSource = dv;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để tìm kiếm. Vui lòng tải lại dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn Mã loại để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Event handler cho DataGridView cell click để điền dữ liệu vào các TextBox
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo click vào hàng hợp lệ
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox2.Text = row.Cells["MaLoai"].Value?.ToString();
                textBox3.Text = row.Cells["TenLoai"].Value?.ToString();
                textBox4.Text = row.Cells["GhiChu"].Value?.ToString();
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy hàng hiện tại mà người dùng đã click
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox2.Text = row.Cells["MaLoai"].Value?.ToString();
                textBox3.Text = row.Cells["TenLoai"].Value?.ToString();
                textBox4.Text = row.Cells["GhiChu"].Value?.ToString();
            }
        }

        private void QLLoaiSanPham_Load_1(object sender, EventArgs e)
        {
            LoadData();
            LoadCombo();
            // Đặt AutoGenerateColumns của DataGridView thành true nếu bạn muốn nó tự động tạo cột
            // Nếu bạn đã tự định nghĩa cột trong designer, hãy đảm bảo DataPropertyName của chúng khớp với tên cột trong database.
            dataGridView1.AutoGenerateColumns = true;
        }
    }
}





