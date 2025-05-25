using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Polycafe_BUS;
using Polycafe_DTO;
namespace Polycafe_GUI
{
    public partial class ThongKe : UserControl
    {
        private ThongkeNVBUS bus = new ThongkeNVBUS();
        public ThongKe()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void ClearFields()
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            comboBox1.SelectedIndex = -1;
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now;
            comboBox2.SelectedIndex = -1;
        }
        private void LoadData()
        {
            string MaNV = comboBox1.SelectedValue?.ToString();
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;

            dataGridView1.DataSource = bus.getTK(MaNV, startDate, endDate);
        }
        // In ThongKe.cs
        private void LoadDataSP()
        {
            string MaSP = comboBox2.SelectedValue?.ToString();
            DateTime startDate = dateTimePicker4.Value;
            DateTime endDate = dateTimePicker3.Value; // Note: dateTimePicker3 is endDate, dateTimePicker4 is startDate

            // Make sure to clear the DataGridView before loading new data if you're switching between employee and product stats
            dataGridView2.DataSource = null; // Clear previous data
            dataGridView2.Columns.Clear(); // Clear previous columns

            dataGridView2.DataSource = bus.getTKSP(MaSP, startDate, endDate);
        }
        private void LoadComboSP()
        {
            var listmaSP = bus.LoadmaSP();
            if (listmaSP != null)
            {
                comboBox2.DataSource = listmaSP;
                comboBox2.DisplayMember = "TenSP";
                comboBox2.ValueMember = "MaSP";
                comboBox2.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Không thể tải dữ liệu Mã sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // In ThongKe.cs
        private void LoadCombo()
        {
            try
            {
                var listmaNV = bus.LoadmaNV(); // Changed variable name for clarity
                if (listmaNV != null)
                {
                    comboBox1.DataSource = listmaNV;
                    comboBox1.SelectedIndex = -1; // Set nothing selected initially
                }
                else
                {
                    MessageBox.Show("Không thể tải dữ liệu Mã nhân viên cho ComboBox.", "Lỗi Tải ComboBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi Load dữ liệu ComboBox: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string maNV = comboBox1.SelectedValue?.ToString();
                DateTime tuNgay = dateTimePicker1.Value.Date;
                DateTime denNgay = dateTimePicker2.Value.Date;

                // Kiểm tra điều kiện ngày hợp lệ
                if (tuNgay > denNgay)
                {
                    MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy dữ liệu thống kê từ BUS
                DataTable dt = bus.getTK(maNV, tuNgay, denNgay);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu thống kê phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Gán dữ liệu cho DataGridView
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thống kê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            LoadData(); // Tải lại toàn bộ dữ liệu gốc
            ClearFields(); // Xóa các trường nhập liệu
            comboBox1.SelectedIndex = -1; // Đặt lại ComboBox
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear the DataGridView before loading new data for the selected tab
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear(); // Clear columns too to avoid issues with different column sets

            if (tabControl1.SelectedTab == tabPage1) // tab thống kê nhân viên
            {
                LoadCombo(); // Load employee combo box data
                LoadData(); // Load employee statistics
            }
            else if (tabControl1.SelectedTab == tabPage2) // tab thống kê sản phẩm
            {
                LoadComboSP(); // Load product combo box data
                LoadDataSP(); // Load product statistics
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            // Initial load based on the currently selected tab
            // Also clear both grids on initial load, in case of previous state
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView2.DataSource = null; // Clear dataGridView2
            dataGridView2.Columns.Clear();   // Clear its columns

            if (tabControl1.SelectedTab == tabPage1)
            {
                LoadCombo();
                LoadData();
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                LoadComboSP();
                LoadDataSP();
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            ClearFields(); // Clear all fields (including product date pickers and combo box)
            LoadDataSP(); // Reload product data after clearing filters
            comboBox2.SelectedIndex = -1; // Reset product combo box
        }

        // In ThongKe.cs
        private void button4_Click_1(object sender, EventArgs e)
        {
            string maSP = comboBox2.SelectedValue?.ToString();
            DateTime tuNgay = dateTimePicker4.Value.Date;
            DateTime denNgay = dateTimePicker3.Value.Date;

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dt = bus.getTKSP(maSP, tuNgay, denNgay);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu thống kê sản phẩm phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // FIX: Assign to dataGridView2 for product statistics
            dataGridView2.DataSource = dt;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}


namespace ThongKeNV_DAL
{
    public class ThongkeNVDAL
    {
        private static string connectionString = "Data Source=SD20302\\ADMINCUTE;Initial Catalog=PolyCafe;Integrated Security=True;";

        public DataTable GetNV()
        {
            DataTable dt = new DataTable();
            string query = "SELECT MaNhanVien, HoTen FROM NhanVien ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Lõi không xác định tải dữ liệu");
            }
            return dt;
        }
        public DataTable GetTK(string MaNV, DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT NV.MaNhanVien,
                    NV.HoTen,
                    PB.NgayTao,
                    PB.TrangThai,
                    Count(ChiTietPhieu.SoLuong) as [So Luong Phieu],
                    Sum(ChiTietPhieu.DonGia * ChiTietPhieu.SoLuong) as [Tong Tien]
                FROM NhanVien NV
                Inner join PhieuBanHang PB on PB.MaNhanVien = NV.MaNhanVien
                Inner join ChiTietPhieu on ChiTietPhieu.MaPhieu = PB.MaPhieu
                WHERE (@MaNV IS NULL OR NV.MaNhanVien = @MaNV)
                AND PB.NgayTao BETWEEN @TuNgay AND @DenNgay
                group by NV.MaNhanVien, 
                NV.HoTen,
                PB.TrangThai,
                PB.NgayTao";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (string.IsNullOrEmpty(MaNV))
                        {
                            cmd.Parameters.AddWithValue("@MaNV", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@MaNV", MaNV);
                        }
                        cmd.Parameters.AddWithValue("@TuNgay", startDate);
                        cmd.Parameters.AddWithValue("@DenNgay", endDate);

                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu thống kê từ CSDL.");
            }
            return dt;

        }


        public List<string> LoadMaNV()
        {
            List<string> maNV = new List<string>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT DISTINCT MaNhanVien FROM NhanVien";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        maNV.Add(reader["MaNhanVien"].ToString());
                    }
                    reader.Close(); // Đóng reader
                    return maNV;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi tải Mã nhân viên : {ex.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null; // Trả về null khi có lỗi
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi không xác định khi tải Mã nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }


        public List<SanPham_DTO> LoadMaSP()
        {
            List<SanPham_DTO> list = new List<SanPham_DTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT MaSanPham, TenSanPham FROM SanPham";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new SanPham_DTO
                    {
                        MaSP = reader["MaSanPham"].ToString(),
                        TenSP = reader["TenSanPham"].ToString()
                    });
                }
            }
            return list;
        }


        public DataTable GetTKSP(string MaSP, DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT SP.MaSanPham,
                       SP.TenSanPham,
                       PB.NgayTao,
                       SUM(CTP.SoLuong) AS SoLuongBan,
                       SUM(CTP.SoLuong * CTP.DonGia) AS TongTien
                FROM SanPham SP
                INNER JOIN ChiTietPhieu CTP ON SP.MaSanPham = CTP.MaSanPham
                INNER JOIN PhieuBanHang PB ON PB.MaPhieu = CTP.MaPhieu
                WHERE (@MaSP IS NULL OR SP.MaSanPham = @MaSP)
                  AND PB.NgayTao BETWEEN @TuNgay AND @DenNgay
                GROUP BY SP.MaSanPham, SP.TenSanPham, PB.NgayTao";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSP", string.IsNullOrEmpty(MaSP) ? DBNull.Value : (object)MaSP);
                    cmd.Parameters.AddWithValue("@TuNgay", startDate);
                    cmd.Parameters.AddWithValue("@DenNgay", endDate);

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

    }




}
