using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polycafe_GUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SetupSideMenu();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Hiển thị mặc định
            DisplayUserControl(new QLNhanVien()); 
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Nếu button này không liên quan đến menu thì giữ nguyên.
        }

        private void SetupSideMenu()
        {
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is Button button)
                {
                    button.Click += MenuItem_Click;
                }
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                panel1.Controls.Clear();
                UserControl newContentControl = null;

                if (clickedButton.Text == "Nhân viên")  //Form hiển thị khi nhấn chọn
                {
                    newContentControl = new QLNhanVien();
                }
                else if (clickedButton.Text == "Sản phẩm") //Thêm else if chuyển sang user control khác
                {
                    newContentControl = new QuanLySanPham();
                }
                else if (clickedButton.Text == "Loại sản phẩm") //Thêm else if chuyển sang user control khác
                {
                    newContentControl = new QLLoaiSanPham();
                }
                else if (clickedButton.Text == "Phiếu bán hàng") //Thêm else if chuyển sang user control khác
                {
                    newContentControl = new QLPhieuBanHang();
                }
                else if (clickedButton.Text == "Thẻ lưu động") //Thêm else if chuyển sang user control khác
                {
                    newContentControl = new QLTheLuuDong();
                }
                else if (clickedButton.Text == "Cài đặt") //Thêm else if chuyển sang user control khác
                {
                    newContentControl = new CaiDat();
                }
                else if (clickedButton.Text == "Thống kê") //Thêm else if chuyển sang user control khác
                {
                    newContentControl = new ThongKe();
                }

                if (newContentControl != null)
                {
                    DisplayUserControl(newContentControl);
                }
            }
        }

        private void DisplayUserControl(UserControl control)
        {
            control.Dock = DockStyle.Fill;
            panel1.Controls.Add(control);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Thêm code xử lý cho button1 nếu cần
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Thêm code xử lý cho button2 nếu cần
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            // Hiển thị MessageBox xác nhận
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Kiểm tra kết quả từ MessageBox
            if (result == DialogResult.Yes)
            {
                // Nếu người dùng chọn Yes, ẩn Form hiện tại và hiển thị Form Đăng nhập
                this.Hide(); // Ẩn MainForm
                Login login = new Login();
                login.Show(); // Hiển thị LoginForm
            }
        }
    }
}
