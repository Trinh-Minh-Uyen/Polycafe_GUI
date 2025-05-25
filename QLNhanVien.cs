using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Polycafe_BUS;
using Polycafe_DTO;

namespace Polycafe_GUI
{
    public partial class QLNhanVien : UserControl
    {
        private EmployeeBLL employeeBLL;
        private string selectedEmployeeId = null; // Biến để lưu trữ Mã NV của dòng đang chọn trên DataGridView

        public QLNhanVien()
        {
            InitializeComponent();
            employeeBLL = new EmployeeBLL(); // Khởi tạo BLL
            LoadEmployeeData(); // Tải dữ liệu khi form khởi tạo
            SetupComboBoxRole(); // Cài đặt ComboBox vai trò
            ResetForm(); // Đặt lại trạng thái ban đầu của form
            dgvEmployee.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        // Phương thức tải dữ liệu nhân viên vào DataGridView
        private void LoadEmployeeData()
        {
            List<EmployeeDTO> employees = employeeBLL.GetAllEmployees();
            dgvEmployee.DataSource = employees;

            // Tùy chỉnh hiển thị DataGridView (nếu cần)
            dgvEmployee.Columns["EmployeeId"].HeaderText = "Mã NV";
            dgvEmployee.Columns["FullName"].HeaderText = "Họ Tên";
            dgvEmployee.Columns["Email"].HeaderText = "Email";
            dgvEmployee.Columns["Password"].HeaderText = "Mật Khẩu"; // Có thể ẩn cột này trong thực tế
            dgvEmployee.Columns["Role"].HeaderText = "Vai Trò";
            dgvEmployee.Columns["Status"].HeaderText = "Trạng Thái";
        }

        // Cài đặt ComboBox Vai trò
        private void SetupComboBoxRole()
        {
            // Clear items cũ nếu có
            cboRole.Items.Clear();

            // Thêm các cặp giá trị hiển thị (text) và giá trị thực (value)
            // Với 1 là Quản lý, 0 là Nhân viên
            cboRole.DisplayMember = "Text";
            cboRole.ValueMember = "Value";

            cboRole.Items.Add(new { Text = "Quản lý", Value = true }); // Tương ứng với 1 trong DB
            cboRole.Items.Add(new { Text = "Nhân viên", Value = false }); // Tương ứng với 0 trong DB

            cboRole.SelectedIndex = 0; // Chọn "Quản lý" làm mặc định
        }

        // Phương thức đọc dữ liệu từ các controls trên form vào DTO
        private EmployeeDTO GetEmployeeDataFromForm()
        {
            // Đảm bảo Mã NV không bị khoảng trắng đầu cuối
            string employeeId = txtEmployeeId.Text.Trim();
            bool role = (bool)((dynamic)cboRole.SelectedItem).Value;
            bool status = rdoActive.Checked; // Nếu rdoActive checked thì Status là true (Hoạt động), ngược lại là false (Không hoạt động)

            return new EmployeeDTO
            (
                employeeId,
                txtEmployeeName.Text.Trim(),
                txtEmail.Text.Trim(),
                txtPassword.Text.Trim(),
                role,
                status
            );
        }

        // Phương thức hiển thị dữ liệu từ DTO lên các controls trên form
        private void DisplayEmployeeDataOnForm(EmployeeDTO employee)
        {
            if (employee != null)
            {
                txtEmployeeId.Text = employee.EmployeeId;
                txtEmployeeName.Text = employee.FullName;
                txtEmail.Text = employee.Email;
                txtPassword.Text = employee.Password;

                // Chọn vai trò trong combobox
                foreach (var item in cboRole.Items)
                {
                    if (((dynamic)item).Value == employee.Role)
                    {
                        cboRole.SelectedItem = item;
                        break;
                    }
                }

                // Chọn trạng thái
                rdoActive.Checked = employee.Status;
                rdoInactive.Checked = !employee.Status;
            }
        }

        // Phương thức xóa trắng các controls nhập liệu và reset trạng thái form
        private void ResetForm()
        {
            txtEmployeeId.Clear();
            txtEmployeeName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            cboRole.SelectedIndex = 0; // Đặt về "Quản lý"
            rdoActive.Checked = true; // Đặt về "Hoạt động"
            rdoInactive.Checked = false;

            txtFind.Clear(); // Xóa nội dung tìm kiếm
            selectedEmployeeId = null; // Không có dòng nào được chọn
            txtEmployeeId.ReadOnly = false; // Cho phép nhập Mã NV khi thêm mới
            btnAdd.Enabled = true; // Cho phép nút Thêm
            btnUpdate.Enabled = false; // Vô hiệu hóa nút Cập nhật
            btnDelete.Enabled = false; // Vô hiệu hóa nút Xóa

            LoadEmployeeData(); // Tải lại DataGridView
        }

        private void QLNhanVien_Load(object sender, EventArgs e)
        {

        }

        // Sự kiện Click của nút Thêm (btnAdd)
        private void btnAdd_Click(object sender, EventArgs e)
        {
            EmployeeDTO newEmployee = GetEmployeeDataFromForm();
            string result = employeeBLL.AddEmployee(newEmployee);

            if (result == "SUCCESS")
            {
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
            }
            else
            {
                MessageBox.Show("Lỗi: " + result, "Lỗi thêm nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện Click của nút Cập nhật (btnUpdate)
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeId == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần cập nhật từ danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EmployeeDTO updatedEmployee = GetEmployeeDataFromForm();
            // Đảm bảo EmployeeId của DTO là của dòng đang được chọn, không phải từ textbox (nếu textbox bị sửa)
            updatedEmployee.EmployeeId = selectedEmployeeId;

            string result = employeeBLL.UpdateEmployee(updatedEmployee);

            if (result == "SUCCESS")
            {
                MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
            }
            else
            {
                MessageBox.Show("Lỗi: " + result, "Lỗi cập nhật nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện Click của nút Xoá (btnDelete)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeId == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa từ danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên có Mã NV: {selectedEmployeeId} không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                string result = employeeBLL.DeleteEmployee(selectedEmployeeId);
                if (result == "SUCCESS")
                {
                    MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Lỗi: " + result, "Lỗi xóa nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Sự kiện Click của nút Quay lại (btnReset)
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        // Sự kiện Click của nút Tìm kiếm (btnFind)
        private void btnFind_Click(object sender, EventArgs e)
        {
            string keyword = txtFind.Text.Trim();
            List<EmployeeDTO> searchResults = employeeBLL.SearchEmployees(keyword);

            if (searchResults != null && searchResults.Count > 0)
            {
                dgvEmployee.DataSource = searchResults;
                MessageBox.Show($"Tìm thấy {searchResults.Count} kết quả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dgvEmployee.DataSource = null; // Xóa dữ liệu cũ nếu không tìm thấy
                MessageBox.Show("Không tìm thấy nhân viên nào phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Sự kiện CellClick của DataGridView (dgvEmployee)
        // Khi người dùng click vào một hàng trong DataGridView, load dữ liệu lên các controls nhập liệu
        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo click vào một hàng hợp lệ, không phải header
            {
                // Lấy EmployeeId từ cột đầu tiên của hàng được chọn
                selectedEmployeeId = dgvEmployee.Rows[e.RowIndex].Cells["EmployeeId"].Value.ToString();

                // Lấy đối tượng EmployeeDTO từ DataSource (nếu có thể) hoặc tự tạo lại từ các cell
                // Cách an toàn hơn: Lấy tất cả thông tin từ các cell của hàng đó
                EmployeeDTO employee = new EmployeeDTO
                (
                    dgvEmployee.Rows[e.RowIndex].Cells["EmployeeId"].Value.ToString(),
                    dgvEmployee.Rows[e.RowIndex].Cells["FullName"].Value.ToString(),
                    dgvEmployee.Rows[e.RowIndex].Cells["Email"].Value.ToString(),
                    dgvEmployee.Rows[e.RowIndex].Cells["Password"].Value.ToString(),
                    (bool)dgvEmployee.Rows[e.RowIndex].Cells["Role"].Value,
                    (bool)dgvEmployee.Rows[e.RowIndex].Cells["Status"].Value
                );

                DisplayEmployeeDataOnForm(employee);

                // Khi một dòng được chọn, vô hiệu hóa việc sửa Mã NV và kích hoạt nút Cập nhật/Xóa
                txtEmployeeId.ReadOnly = true;
                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvEmployee_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Mã hoá Mật khẩu trong DataGridView            
            if (dgvEmployee.Columns[e.ColumnIndex].Name == "Password" && e.Value != null)
            {
                e.Value = new string('*', e.Value.ToString().Length > 0 ? e.Value.ToString().Length : 1);
                e.FormattingApplied = true;
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}





