using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Polycafe_BUS;
using Polycafe_DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Polycafe_GUI
{
    public partial class Login : Form
    {
        private LoginBUS bus = new LoginBUS();
        public static class AuthUtil //Lưu lại thông tin khi đăng nhập thành công
        {
            public static LoginDTO CurrentUser { get; set; }
        }
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void user_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pass_TextChanged(object sender, EventArgs e)
        {

        }

        private void show_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 3;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 3;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            pass.PasswordChar = show.Checked ? '\0' : '*';
            string savedUser = Properties.Settings.Default.SavedUser;
            string savedPass = Properties.Settings.Default.SavedPass;

            if (!string.IsNullOrEmpty(savedUser) && !string.IsNullOrEmpty(savedPass))
            {
                user.Text = savedUser;
                pass.Text = savedPass;
                chkremember.Checked = true;
            }
            else
            {
                chkremember.Checked = false;
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void user_TextChanged_1(object sender, EventArgs e)
        {

        }


        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = user.Text.Trim();
            string password = pass.Text.Trim();
            if (bus.KiemTraDangNhap(username, password))
            {
                if (chkremember.Checked)
                {
                    Properties.Settings.Default.SavedUser = username;
                    Properties.Settings.Default.SavedPass = password;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.SavedUser = "";
                    Properties.Settings.Default.SavedPass = "";
                    Properties.Settings.Default.Save();
                }

            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (bus.KiemTraDangNhap(username, password))
            {
                LoginDTO nhanVien = new LoginDTO
                {
                    Email = username,
                    MatKhau = password,
                    VaiTro = bus.LayVaiTro(username)
                };
                MessageBox.Show($"Đăng nhập thành công! Vai trò: {nhanVien.VaiTro}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                

                if (nhanVien.VaiTro != null)
                {
                    try
                    {
                        AuthUtil.CurrentUser = nhanVien;
                        MainForm form = new MainForm();
                        form.Show();
                        this.Hide();  
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi mở MainForm: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền truy cập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnExit_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhận thoát chương trình?",
                                                "Thoát",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void show_CheckedChanged_1(object sender, EventArgs e)
        {
            pass.PasswordChar = show.Checked ? '\0' : '*';

        }

        private void pass_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Ngăn người dùng thoát bằng nút X mặc định
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }

        private void chkremember_CheckedChanged(object sender, EventArgs e)
        {
            if (chkremember.Checked)
            {
                Properties.Settings.Default.SavedUser = user.Text;
                Properties.Settings.Default.SavedPass = pass.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.SavedUser = "";
                Properties.Settings.Default.SavedPass = "";
                Properties.Settings.Default.Save();
            }
        }

        private void LinkForget_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string userEmail = user.Text; // Lấy email từ textbox hoặc biến lưu trữ trong LoginForm
            ForgetPassword forgetPassword = new ForgetPassword(userEmail);
            forgetPassword.Show(); // Hiển thị ForgetPassword
            this.Hide(); // Ẩn LoginForm
        }
    }
}
