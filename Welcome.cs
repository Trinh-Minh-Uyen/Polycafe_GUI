using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Polycafe_GUI
{
    public partial class Welcome : Form
    {

        public Welcome()
        {
            InitializeComponent();
            //Cấu hình kiểu cho thanh progress bar
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            //Thực hiện thao tác load chờ 3 giây
            Task.Delay(1500).ContinueWith(t =>
            {
                Invoke(new Action(() =>
                {
                    // Mở form chính
                    Login login = new Login();
                    login.Show();

                    // Đóng form Welcome
                    this.Hide(); // hoặc this.Close(); nếu không cần nữa
                }));
            });

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void Welcome_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Ngăn chặn người dùng ngắt ứng dụng
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }

        private void Welcome_Load(object sender, EventArgs e)
        {

        }
    }
}
