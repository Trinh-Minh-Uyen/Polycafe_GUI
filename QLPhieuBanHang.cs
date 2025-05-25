using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Polycafe_DTO;
using Polycafe_BUS;

namespace Polycafe_GUI
{
    public partial class QLPhieuBanHang : UserControl
    {
        private bool _isBindingData = false; // Biến cờ (Kiểm tra liên kết của Thẻ và Phiếu)

        public QLPhieuBanHang()
        {
            InitializeComponent(); // Hàm khởi tạo component của Form
            _saleInvoiceBLL = new SaleInvoiceBLL();
            SetupDataGridViews();
            dgvSaleInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSaleInvoiceDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCards.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void QLPhieuBanHang_Load(object sender, EventArgs e)
        {
            LoadInitialData();
            ClearInvoiceFields();
            ClearDetailFields();
        }

        private SaleInvoiceBLL _saleInvoiceBLL;
        private List<SaleInvoiceDTO> _currentInvoiceList; // Danh sách phiếu đang hiển thị trên dgvSaleInvoices
        private List<SaleInvoiceDetailDTO> _currentDetailList; // Danh sách chi tiết đang hiển thị trên dgvSaleInvoiceDetails

        // --- Cấu hình DataGridViews ---
        private void SetupDataGridViews()
        {
            // DataGridView cho Phiếu Bán Hàng
            dgvSaleInvoices.AutoGenerateColumns = false; // Tự động tạo cột = false để tự định nghĩa
            dgvSaleInvoices.Columns.Clear();
            dgvSaleInvoices.Columns.Add(new DataGridViewTextBoxColumn() { Name = "InvoiceIdCol", HeaderText = "Mã Phiếu", DataPropertyName = "InvoiceId", ReadOnly = true });
            dgvSaleInvoices.Columns.Add(new DataGridViewTextBoxColumn() { Name = "CardOwnerNameCol", HeaderText = "Chủ Thẻ", DataPropertyName = "CardOwnerName", ReadOnly = true });
            dgvSaleInvoices.Columns.Add(new DataGridViewTextBoxColumn() { Name = "EmployeeNameCol", HeaderText = "Nhân Viên", DataPropertyName = "EmployeeName", ReadOnly = true });
            dgvSaleInvoices.Columns.Add(new DataGridViewTextBoxColumn() { Name = "CreatedDateCol", HeaderText = "Ngày Tạo", DataPropertyName = "CreatedDate", ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } }); dgvSaleInvoices.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "StatusCol", HeaderText = "Trạng Thái", DataPropertyName = "Status", ReadOnly = true });
            // Ẩn các cột MaThe, MaNhanVien nếu không muốn hiển thị trực tiếp
            // Thêm cột ẩn để lưu MaThe và MaNhanVien nếu cần
            dgvSaleInvoices.Columns.Add(new DataGridViewTextBoxColumn() { Name = "CardIdHiddenCol", DataPropertyName = "CardId", Visible = false });
            dgvSaleInvoices.Columns.Add(new DataGridViewTextBoxColumn() { Name = "EmployeeIdHiddenCol", DataPropertyName = "EmployeeId", Visible = false });
            dgvSaleInvoices.AllowUserToAddRows = false; // Không tự động thêm dòng để tránh phát sinh lỗi

            // DataGridView cho Chi Tiết Phiếu
            dgvSaleInvoiceDetails.AutoGenerateColumns = false;
            dgvSaleInvoiceDetails.Columns.Clear();
            dgvSaleInvoiceDetails.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ProductIdCol", HeaderText = "Mã SP", DataPropertyName = "ProductId", ReadOnly = true });
            dgvSaleInvoiceDetails.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ProductNameCol", HeaderText = "Tên SP", DataPropertyName = "ProductName", ReadOnly = true });
            dgvSaleInvoiceDetails.Columns.Add(new DataGridViewTextBoxColumn() { Name = "QuantityCol", HeaderText = "Số Lượng", DataPropertyName = "Quantity", ReadOnly = true });
            dgvSaleInvoiceDetails.Columns.Add(new DataGridViewTextBoxColumn() { Name = "UnitPriceCol", HeaderText = "Đơn Giá", DataPropertyName = "UnitPrice", ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvSaleInvoiceDetails.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LineAmountCol", HeaderText = "Thành Tiền", DataPropertyName = "LineAmount", ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            // Thêm cột ẩn cho ID chi tiết để phục vụ việc xóa/sửa
            dgvSaleInvoiceDetails.Columns.Add(new DataGridViewTextBoxColumn() { Name = "DetailIdHiddenCol", DataPropertyName = "Id", Visible = false });
            dgvSaleInvoiceDetails.AllowUserToAddRows = false; // Không tự động thêm dòng để tránh phát sinh lỗi

            // DataGridView cho Thẻ Lưu Động
            dgvCards.AutoGenerateColumns = false;
            dgvCards.Columns.Clear();
            dgvCards.Columns.Add(new DataGridViewTextBoxColumn() { Name = "CardIdCol_Cards", HeaderText = "Mã Thẻ", DataPropertyName = "CardId", ReadOnly = true });
            dgvCards.Columns.Add(new DataGridViewTextBoxColumn() { Name = "OwnerNameCol_Cards", HeaderText = "Chủ Thẻ", DataPropertyName = "OwnerName", ReadOnly = true });
            dgvCards.Columns.Add(new DataGridViewTextBoxColumn() { Name = "CurrentInvoiceIdCol_Cards", HeaderText = "Mã Phiếu LH", DataPropertyName = "CurrentInvoiceId", ReadOnly = true });
            dgvCards.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "StatusCol_Cards", HeaderText = "Trạng Thái", DataPropertyName = "Status", ReadOnly = true });
            dgvCards.AllowUserToAddRows = false; // Không tự động thêm dòng để tránh phát sinh lỗi
        }

        // --- Phương thức tải dữ liệu ban đầu cho ComboBoxes và DataGridViews ---
        private void LoadInitialData()
        {
            // Load ComboBox Thẻ Lưu Động
            List<CardDTO> cards = _saleInvoiceBLL.GetAllCards();
            cboCardId.DataSource = cards;
            cboCardId.DisplayMember = "CardId"; // Hiển thị MaThe trên ComboBox
            cboCardId.ValueMember = "CardId";   // Giá trị thực của ComboBox là MaThe
            cboCardId.SelectedIndex = -1; // Chọn không có gì mặc định

            // Load ComboBox Nhân Viên
            List<EmployeeDto> employees = _saleInvoiceBLL.GetAllEmployees();
            cboEmployeeId.DataSource = employees;
            cboEmployeeId.DisplayMember = "EmployeeId"; // Hiển thị MaNhanVien
            cboEmployeeId.ValueMember = "EmployeeId";
            cboEmployeeId.SelectedIndex = -1;

            // Load ComboBox Sản Phẩm (cho chi tiết phiếu)
            List<ProductDTO> products = _saleInvoiceBLL.GetAllProducts();
            cboProductId.DataSource = products;
            cboProductId.DisplayMember = "ProductName"; // Hiển thị TenSanPham
            cboProductId.ValueMember = "ProductId";   // Giá trị thực là MaSanPham
            cboProductId.SelectedIndex = -1;

            // Đặt ngày tạo là ngày hiện tại
            dtpCreatedDate.Value = DateTime.Now;

            // Load danh sách phiếu bán hàng vào dgvSaleInvoices
            LoadSaleInvoicesToDataGridView();

            // Load dgvCards
            LoadCardsToDataGridView();

            // Load MaPhieu vào ComboBox Tìm kiếm (cboFind) ---
            LoadInvoiceIdsToFindComboBox();
        }

        // --- Phương thức tải danh sách phiếu bán hàng vào DataGridView ---
        private void LoadSaleInvoicesToDataGridView()
        {
            _currentInvoiceList = _saleInvoiceBLL.GetAllSaleInvoices();
            dgvSaleInvoices.DataSource = _currentInvoiceList;
            dgvSaleInvoices.Refresh(); // Cập nhật hiển thị DataGridView
        }

        // --- Phương thức tải chi tiết phiếu vào DataGridView Chi tiết ---
        private void LoadSaleInvoiceDetailsToDataGridView(string invoiceId)
        {
            _currentDetailList = _saleInvoiceBLL.GetInvoiceDetails(invoiceId);
            dgvSaleInvoiceDetails.DataSource = _currentDetailList;
            dgvSaleInvoiceDetails.Refresh();

            // Cập nhật tổng số lượng và tổng tiền của phiếu chính
            SaleInvoiceDTO currentInvoice = _saleInvoiceBLL.GetSaleInvoiceById(invoiceId);
            if (currentInvoice != null)
            {
                //txtQuantity.Text = currentInvoice.TotalQuantity.ToString();
                txtTotalAmount.Text = currentInvoice.TotalAmount.ToString("N0"); // Định dạng số tiền
            }
            else
            {
                nudQuantity.Text = "0";
                txtTotalAmount.Text = "0";
            }
        }

        // --- Phương thức tải dữ liệu vào dgvCards ---
        private void LoadCardsToDataGridView()
        {
            List<CardDTO> activeCards = _saleInvoiceBLL.GetActiveCardsWithLinkedInvoice();
            dgvCards.DataSource = activeCards;
        }

        // --- Phương thức tải danh sách MaPhieu vào cboFind ---
        private void LoadInvoiceIdsToFindComboBox()
        {
            // Lấy tất cả phiếu để lấy danh sách MaPhieu
            // Bạn có thể tạo một phương thức riêng trong BLL/DAL để chỉ lấy MaPhieu nếu danh sách GetAllSaleInvoices quá lớn
            // Nhưng với mục đích này, việc lấy MaPhieu từ _currentInvoiceList (đã load cho dgvSaleInvoices) là hiệu quả.
            if (_currentInvoiceList != null && _currentInvoiceList.Any())
            {
                // Tạo một danh sách các chuỗi MaPhieu
                List<string> invoiceIds = _currentInvoiceList.Select(inv => inv.InvoiceId).ToList();

                // Thêm một mục "Tất cả phiếu" hoặc "Chọn Mã phiếu" nếu muốn
                invoiceIds.Insert(0, "-- Chọn Mã phiếu --"); // Hoặc một chuỗi gợi ý khác

                cboFind.DataSource = invoiceIds;
                cboFind.SelectedIndex = 0; // Chọn mục gợi ý mặc định
            }
            else
            {
                cboFind.DataSource = null;
            }
        }

        // --- Xóa nội dung các trường nhập liệu phiếu chính ---
        private void ClearInvoiceFields()
        {
            txtInvoiceId.Clear();
            cboCardId.SelectedIndex = 0;
            cboEmployeeId.SelectedIndex = 0;
            dtpCreatedDate.Value = DateTime.Now;
            rdoPending.Checked = true;
            // txtTotalQuantity.Text = "0";
            txtTotalAmount.Text = "0";
            txtInvoiceId.Enabled = true; // Cho phép nhập Mã phiếu khi thêm mới
            dgvSaleInvoiceDetails.DataSource = null;
            _currentDetailList = new List<SaleInvoiceDetailDTO>();

            // Kích hoạt lại các nút khi ở chế độ thêm mới ---
            btnUpdateInvoice.Enabled = true; // Cho phép cập nhật (nếu là phiếu mới, nút này sẽ chưa có tác dụng cho đến khi nhấn "Thêm")
            btnDeleteInvoice.Enabled = true;
            btnAddDetail.Enabled = true; // Cho phép thêm chi tiết
            btnUpdateDetail.Enabled = true;
            btnDeleteDetail.Enabled = true;

            // Vô hiệu hóa nút Xuất Phiếu khi reset form
            btnExport.Enabled = false;

            // Reset cboFind về mục gợi ý
            cboFind.SelectedIndex = 0;
        }

        // --- Xóa nội dung các trường nhập liệu chi tiết phiếu ---
        private void ClearDetailFields()
        {
            cboProductId.SelectedIndex = -1;
            nudQuantity.Value = 1; // Mặc định số lượng là 1
            txtUnitPrice.Text = "0";
            //txtLineAmount.Text = "0";
        }

        // --- Sự kiện chọn dòng trên dgvSaleInvoices ---
        private void dgvSaleInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _isBindingData = true; // Bật cờ trước khi gán dữ liệu

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSaleInvoices.Rows[e.RowIndex];
                string invoiceId = row.Cells["InvoiceIdCol"].Value.ToString();

                // Lấy thông tin phiếu đầy đủ từ BLL
                SaleInvoiceDTO selectedInvoice = _saleInvoiceBLL.GetSaleInvoiceById(invoiceId);

                if (selectedInvoice != null)
                {
                    txtInvoiceId.Text = selectedInvoice.InvoiceId;
                    cboCardId.SelectedValue = selectedInvoice.CardId ?? string.Empty;
                    cboCardId.SelectedValue = selectedInvoice.CardId ?? string.Empty;
                    cboCardId.SelectedValue = selectedInvoice.CardId ?? string.Empty;
                    cboEmployeeId.SelectedValue = selectedInvoice.EmployeeId;
                    dtpCreatedDate.Value = selectedInvoice.CreatedDate;
                    if (selectedInvoice.Status)
                    {
                        rdoPaid.Checked = true;
                    }
                    else
                    {
                        rdoPending.Checked = true;
                    }
                    // txtTotalQuantity.Text = selectedInvoice.TotalQuantity.ToString();
                    txtTotalAmount.Text = selectedInvoice.TotalAmount.ToString("N0");

                    txtInvoiceId.Enabled = false; // Không cho phép sửa Mã phiếu khi đang cập nhật

                    // --- THÊM LOGIC KIỂM TRA TRẠNG THÁI VÀ ĐIỀU CHỈNH NÚT ---
                    bool isPaid = selectedInvoice.Status; // True nếu đã thanh toán, False nếu chờ xác nhận

                    // Nút Cập nhật phiếu chính
                    btnUpdateInvoice.Enabled = !isPaid; // Chỉ cho phép cập nhật nếu KHÔNG phải đã thanh toán (Trạng thái = 0)
                                                        // Nút Xóa phiếu chính
                    btnDeleteInvoice.Enabled = !isPaid; // Chỉ cho phép xóa nếu KHÔNG phải đã thanh toán (Trạng thái = 0)

                    // Các nút thao tác với chi tiết cũng có thể bị vô hiệu hóa
                    btnAddDetail.Enabled = !isPaid;
                    btnUpdateDetail.Enabled = !isPaid;
                    btnDeleteDetail.Enabled = !isPaid;

                    // Kích hoạt/vô hiệu hóa nút Xuất Phiếu
                    btnExport.Enabled = isPaid; // Chỉ kích hoạt nếu đã thanh toán (Trạng thái = 1)

                    // Load chi tiết của phiếu này vào dgvSaleInvoiceDetails
                    LoadSaleInvoiceDetailsToDataGridView(invoiceId);
                }

                _isBindingData = false; // Tắt cờ sau khi hoàn thành gán dữ liệu
            }
        }

        // --- Sự kiện chọn dòng trên dgvSaleInvoiceDetails ---
        private void dgvSaleInvoiceDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSaleInvoiceDetails.Rows[e.RowIndex];
                string productId = row.Cells["ProductIdCol"].Value.ToString();
                int quantity = Convert.ToInt32(row.Cells["QuantityCol"].Value);
                decimal unitPrice = Convert.ToDecimal(row.Cells["UnitPriceCol"].Value);
                decimal lineAmount = Convert.ToDecimal(row.Cells["LineAmountCol"].Value);

                // Điền thông tin vào các control của chi tiết
                cboProductId.SelectedValue = productId; // Chọn đúng sản phẩm trong combobox
                nudQuantity.Value = quantity;
                txtUnitPrice.Text = unitPrice.ToString();
                //txtLineAmount.Text = lineAmount.ToString();
            }
        }

        // --- Sự kiện chọn sản phẩm trong ComboBox Sản Phẩm (cboProductId) ---
        private void cboProductId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProductId.SelectedValue != null && cboProductId.SelectedValue is string selectedProductId)
            {
                // Lấy đơn giá từ BLL
                decimal unitPrice = _saleInvoiceBLL.GetProductUnitPrice(selectedProductId);
                txtUnitPrice.Text = unitPrice.ToString("N0"); // Hiển thị đơn giá

                // Tính lại thành tiền dòng
                CalculateLineAmount();
            }
            else
            {
                txtUnitPrice.Text = "0";
                //txtLineAmount.Text = "0";
            }
        }

        // --- Sự kiện thay đổi số lượng trên NumericUpDown (nudQuantity) ---
        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            CalculateLineAmount();
        }

        // --- Hàm tính toán Thành tiền của dòng chi tiết ---
        private void CalculateLineAmount()
        {
            if (decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice) && nudQuantity.Value > 0)
            {
                decimal lineAmount = unitPrice * nudQuantity.Value;
                //txtLineAmount.Text = lineAmount.ToString("N0");
            }
            else
            {
                //txtLineAmount.Text = "0";
            }
        }

        // --- Các sự kiện nút cho Phiếu Bán Hàng chính ---
        private void btnAddInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ Form để tạo SaleInvoiceDTO
                SaleInvoiceDTO newInvoice = new SaleInvoiceDTO
                {
                    InvoiceId = txtInvoiceId.Text.Trim(),
                    CardId = cboCardId.SelectedValue?.ToString(), // Có thể null
                    EmployeeId = cboEmployeeId.SelectedValue?.ToString(), // Có thể null
                    CreatedDate = dtpCreatedDate.Value,
                    Status = rdoPaid.Checked // True nếu Đã thanh toán, False nếu Chờ xác nhận
                                             // InvoiceDetails sẽ được thêm sau hoặc trong quá trình cập nhật
                };

                // Nếu đây là thêm phiếu mới, thì các chi tiết sẽ là rỗng ban đầu hoặc từ dgvDetail nếu đã nhập
                // Giả định khi thêm phiếu mới, ChiTietPhieu sẽ được thêm sau
                // Hoặc nếu bạn muốn thêm chi tiết ngay lúc này, bạn cần lấy từ dgvSaleInvoiceDetails
                // Cách đơn giản nhất là thêm phiếu trước, sau đó người dùng thêm chi tiết.
                // Nếu bạn muốn lưu chi tiết cùng lúc, bạn cần lấy dữ liệu từ dgvSaleInvoiceDetails:
                foreach (DataGridViewRow row in dgvSaleInvoiceDetails.Rows)
                {
                    if (row.IsNewRow) continue; // Bỏ qua dòng mới nếu có
                    SaleInvoiceDetailDTO detail = new SaleInvoiceDetailDTO
                    {
                        ProductId = row.Cells["ProductIdCol"].Value.ToString(),
                        Quantity = Convert.ToInt32(row.Cells["QuantityCol"].Value),
                        UnitPrice = Convert.ToDecimal(row.Cells["UnitPriceCol"].Value),
                        LineAmount = Convert.ToDecimal(row.Cells["LineAmountCol"].Value)
                        // Id không cần vì là tự sinh
                    };
                    newInvoice.InvoiceDetails.Add(detail);
                }

                if (_saleInvoiceBLL.AddSaleInvoice(newInvoice))
                {
                    MessageBox.Show("Thêm phiếu bán hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSaleInvoicesToDataGridView(); // Tải lại danh sách phiếu
                    LoadCardsToDataGridView(); // Tải lại danh sách Thẻ
                    ClearInvoiceFields(); // Xóa các trường nhập liệu
                }
                else
                {
                    MessageBox.Show("Thêm phiếu bán hàng thất bại. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSaleInvoices.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một phiếu để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy MaPhieu của phiếu đang chọn
                string selectedInvoiceId = txtInvoiceId.Text.Trim(); // Hoặc từ dgvSaleInvoices.SelectedRows[0].Cells["InvoiceIdCol"].Value.ToString();

                SaleInvoiceDTO updatedInvoice = new SaleInvoiceDTO
                {
                    InvoiceId = selectedInvoiceId,
                    CardId = cboCardId.SelectedValue?.ToString(),
                    EmployeeId = cboEmployeeId.SelectedValue?.ToString(),
                    CreatedDate = dtpCreatedDate.Value,
                    Status = rdoPaid.Checked
                };

                // Lấy chi tiết từ DataGridView Chi tiết để cập nhật cùng
                foreach (DataGridViewRow row in dgvSaleInvoiceDetails.Rows)
                {
                    if (row.IsNewRow) continue;
                    SaleInvoiceDetailDTO detail = new SaleInvoiceDetailDTO
                    {
                        Id = Convert.ToInt32(row.Cells["DetailIdHiddenCol"].Value), // Phải lấy Id của chi tiết cũ
                        InvoiceId = selectedInvoiceId,
                        ProductId = row.Cells["ProductIdCol"].Value.ToString(),
                        Quantity = Convert.ToInt32(row.Cells["QuantityCol"].Value),
                        UnitPrice = Convert.ToDecimal(row.Cells["UnitPriceCol"].Value),
                        LineAmount = Convert.ToDecimal(row.Cells["LineAmountCol"].Value)
                    };
                    updatedInvoice.InvoiceDetails.Add(detail);
                }

                if (_saleInvoiceBLL.UpdateSaleInvoice(updatedInvoice))
                {
                    MessageBox.Show("Cập nhật phiếu bán hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSaleInvoicesToDataGridView(); // Tải lại danh sách phiếu
                    LoadCardsToDataGridView(); // Tải lại danh sách Thẻ
                    ClearInvoiceFields(); // Xóa các trường nhập liệu
                }
                else
                {
                    MessageBox.Show("Cập nhật phiếu bán hàng thất bại. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInvoiceId.Text == "Mã phiếu" || string.IsNullOrEmpty(txtInvoiceId.Text))
                {
                    MessageBox.Show("Vui lòng chọn phiếu cần xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string invoiceIdToDelete = txtInvoiceId.Text;

                // Xác nhận từ người dùng trước khi xóa
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa phiếu '{invoiceIdToDelete}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (_saleInvoiceBLL.DeleteSaleInvoice(invoiceIdToDelete))
                    {
                        MessageBox.Show("Xóa phiếu bán hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInvoiceFields();
                        LoadSaleInvoicesToDataGridView(); // Tải lại dgvSaleInvoices
                        LoadInvoiceIdsToFindComboBox(); // Tải lại ComboBox tìm kiếm
                        LoadCardsToDataGridView(); // Tải lại dgvCards để cập nhật trạng thái thẻ
                    }
                    else
                    {
                        MessageBox.Show("Xóa phiếu bán hàng thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa phiếu bán hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetInvoice_Click(object sender, EventArgs e)
        {
            ClearInvoiceFields(); // Xóa các trường nhập liệu phiếu chính và chi tiết
            LoadSaleInvoicesToDataGridView(); // Load lại toàn bộ danh sách phiếu
        }


        // --- Các sự kiện nút cho Chi Tiết Phiếu ---

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem phiếu chính đã được chọn/nhập chưa
                if (string.IsNullOrEmpty(txtInvoiceId.Text))
                {
                    MessageBox.Show("Vui lòng chọn hoặc nhập Mã Phiếu trước khi thêm chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboProductId.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn Sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (nudQuantity.Value <= 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaleInvoiceDetailDTO newDetail = new SaleInvoiceDetailDTO
                {
                    InvoiceId = txtInvoiceId.Text.Trim(),
                    ProductId = cboProductId.SelectedValue.ToString(),
                    ProductName = cboProductId.Text, // Lấy tên sản phẩm từ Text của ComboBox
                    Quantity = (int)nudQuantity.Value,
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    LineAmount = (int)nudQuantity.Value * decimal.Parse(txtUnitPrice.Text)
                };

                // Thêm chi tiết vào danh sách hiện tại của DGV và cập nhật DGV
                _currentDetailList.Add(newDetail);
                dgvSaleInvoiceDetails.DataSource = null; // Xóa DataSource tạm thời
                dgvSaleInvoiceDetails.DataSource = _currentDetailList; // Gán lại để refresh
                dgvSaleInvoiceDetails.Refresh();

                // Tính toán lại tổng tiền và tổng số lượng của phiếu chính trên UI
                nudQuantity.Text = _currentDetailList.Sum(d => d.Quantity).ToString();
                txtTotalAmount.Text = _currentDetailList.Sum(d => d.LineAmount).ToString("N0");

                ClearDetailFields(); // Xóa các trường nhập liệu chi tiết
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm chi tiết phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSaleInvoiceDetails.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một chi tiết để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cboProductId.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn Sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (nudQuantity.Value <= 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy ID chi tiết từ cột ẩn
                int detailIdToUpdate = Convert.ToInt32(dgvSaleInvoiceDetails.SelectedRows[0].Cells["DetailIdHiddenCol"].Value);

                // Tìm chi tiết trong danh sách hiện tại và cập nhật
                SaleInvoiceDetailDTO existingDetail = _currentDetailList.FirstOrDefault(d => d.Id == detailIdToUpdate);
                if (existingDetail != null)
                {
                    existingDetail.ProductId = cboProductId.SelectedValue.ToString();
                    existingDetail.ProductName = cboProductId.Text;
                    existingDetail.Quantity = (int)nudQuantity.Value;
                    existingDetail.UnitPrice = decimal.Parse(txtUnitPrice.Text);
                    //existingDetail.LineAmount = decimal.Parse(txtLineAmount.Text);

                    dgvSaleInvoiceDetails.DataSource = null; // Xóa DataSource tạm thời
                    dgvSaleInvoiceDetails.DataSource = _currentDetailList; // Gán lại để refresh
                    dgvSaleInvoiceDetails.Refresh();

                    // Tính toán lại tổng tiền và tổng số lượng của phiếu chính trên UI
                    nudQuantity.Text = _currentDetailList.Sum(d => d.Quantity).ToString();
                    txtTotalAmount.Text = _currentDetailList.Sum(d => d.LineAmount).ToString("N0");

                    ClearDetailFields();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy chi tiết cần cập nhật trong danh sách tạm thời.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật chi tiết phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSaleInvoiceDetails.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một chi tiết để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int detailIdToDelete = Convert.ToInt32(dgvSaleInvoiceDetails.SelectedRows[0].Cells["DetailIdHiddenCol"].Value);

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiết này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Xóa chi tiết khỏi danh sách hiện tại của DGV
                    SaleInvoiceDetailDTO detailToRemove = _currentDetailList.FirstOrDefault(d => d.Id == detailIdToDelete);
                    if (detailToRemove != null)
                    {
                        _currentDetailList.Remove(detailToRemove);
                        dgvSaleInvoiceDetails.DataSource = null; // Xóa DataSource tạm thời
                        dgvSaleInvoiceDetails.DataSource = _currentDetailList; // Gán lại để refresh
                        dgvSaleInvoiceDetails.Refresh();

                        // Tính toán lại tổng tiền và tổng số lượng của phiếu chính trên UI
                        nudQuantity.Text = _currentDetailList.Sum(d => d.Quantity).ToString();
                        txtTotalAmount.Text = _currentDetailList.Sum(d => d.LineAmount).ToString("N0");

                        ClearDetailFields();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy chi tiết cần xóa trong danh sách tạm thời.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa chi tiết phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetDetail_Click(object sender, EventArgs e)
        {
            ClearDetailFields();
            // Không tải lại toàn bộ chi tiết, chỉ reset các trường nhập liệu
            // dgvSaleInvoiceDetails vẫn giữ nguyên các chi tiết của phiếu đang chọn
        }

        private void cboFind_SelectedIndexChanged(object sender, EventArgs e)
        {
            _isBindingData = true; // Bật cờ trước khi gán dữ liệu

            if (cboFind.SelectedItem != null && cboFind.SelectedItem is string selectedInvoiceId)
            {
                if (selectedInvoiceId == "-- Chọn Mã phiếu --")
                {
                    ClearInvoiceFields();
                    dgvSaleInvoices.DataSource = null;
                    dgvSaleInvoiceDetails.DataSource = null;
                    LoadSaleInvoicesToDataGridView();
                    // KHI RESET HOẶC CHỌN MỤC GỢI Ý, CÁC NÚT NÊN ĐƯỢC KÍCH HOẠT LẠI ĐỂ CHO PHÉP THÊM MỚI
                    btnUpdateInvoice.Enabled = true; // Cho phép cập nhật (nếu là phiếu mới, nút này sẽ chưa có tác dụng)
                    btnDeleteInvoice.Enabled = true;
                    btnAddDetail.Enabled = true; // Cho phép thêm chi tiết (khi thêm phiếu mới)
                    btnUpdateDetail.Enabled = true;
                    btnDeleteDetail.Enabled = true;
                    return;
                }

                SaleInvoiceDTO selectedInvoice = _saleInvoiceBLL.GetSaleInvoiceById(selectedInvoiceId);

                if (selectedInvoice != null)
                {
                    txtInvoiceId.Text = selectedInvoice.InvoiceId;
                    cboCardId.SelectedValue = selectedInvoice.CardId ?? string.Empty;
                    cboEmployeeId.SelectedValue = selectedInvoice.EmployeeId;
                    dtpCreatedDate.Value = selectedInvoice.CreatedDate;
                    if (selectedInvoice.Status)
                    {
                        rdoPaid.Checked = true;
                    }
                    else
                    {
                        rdoPending.Checked = true;
                    }
                    // txtTotalQuantity.Text = selectedInvoice.TotalQuantity.ToString();
                    txtTotalAmount.Text = selectedInvoice.TotalAmount.ToString("N0");

                    txtInvoiceId.Enabled = false;

                    // --- THÊM LOGIC KIỂM TRA TRẠNG THÁI VÀ ĐIỀU CHỈNH NÚT (TƯƠNG TỰ CellClick) ---
                    bool isPaid = selectedInvoice.Status;

                    btnUpdateInvoice.Enabled = !isPaid;
                    btnDeleteInvoice.Enabled = !isPaid;
                    btnAddDetail.Enabled = !isPaid;
                    btnUpdateDetail.Enabled = !isPaid;
                    btnDeleteDetail.Enabled = !isPaid;

                    // Kích hoạt/vô hiệu hóa nút Xuất Phiếu
                    btnExport.Enabled = isPaid;

                    LoadSaleInvoiceDetailsToDataGridView(selectedInvoice.InvoiceId);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy phiếu có mã này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearInvoiceFields();
                    // Đảm bảo các nút được kích hoạt lại khi không tìm thấy phiếu hoặc reset
                    btnUpdateInvoice.Enabled = true;
                    btnDeleteInvoice.Enabled = true;
                    btnAddDetail.Enabled = true;
                    btnUpdateDetail.Enabled = true;
                    btnDeleteDetail.Enabled = true;
                }
            }

            _isBindingData = false; // Tắt cờ sau khi hoàn thành gán dữ liệu
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSaleInvoices.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một phiếu bán hàng để xuất hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy MaPhieu từ dòng được chọn
                string invoiceId = dgvSaleInvoices.SelectedRows[0].Cells["InvoiceIdCol"].Value.ToString();

                // Lấy toàn bộ thông tin phiếu và chi tiết từ BLL
                SaleInvoiceDTO invoiceToExport = _saleInvoiceBLL.GetSaleInvoiceById(invoiceId);

                if (invoiceToExport == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin phiếu để xuất hóa đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra trạng thái phiếu (đảm bảo đã thanh toán)
                if (!invoiceToExport.Status)
                {
                    MessageBox.Show("Chỉ có thể xuất hóa đơn cho các phiếu đã thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mở hộp thoại SaveFileDialog để người dùng chọn nơi lưu file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text file (*.txt)|*.txt";
                saveFileDialog.FileName = $"HoaDon_{invoiceToExport.InvoiceId}.txt"; // Tên file mặc định
                saveFileDialog.Title = "Lưu hóa đơn";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    // Bắt đầu tạo nội dung hóa đơn
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("**************************************************");
                    sb.AppendLine("                 HÓA ĐƠN BÁN HÀNG                 ");
                    sb.AppendLine("**************************************************");
                    sb.AppendLine($"Mã Phiếu:           {invoiceToExport.InvoiceId}");
                    sb.AppendLine($"Mã Nhân Viên:       {invoiceToExport.EmployeeId} ({invoiceToExport.EmployeeName})");
                    sb.AppendLine($"Mã Thẻ:             {(string.IsNullOrEmpty(invoiceToExport.CardId) ? "N/A" : invoiceToExport.CardId + " (" + invoiceToExport.CardOwnerName + ")")}");
                    sb.AppendLine($"Ngày Tạo:           {invoiceToExport.CreatedDate:dd/MM/yyyy HH:mm}");
                    sb.AppendLine($"Trạng Thái:         {(invoiceToExport.Status ? "Đã thanh toán" : "Chờ xác nhận")}");
                    sb.AppendLine("--------------------------------------------------");
                    sb.AppendLine("Chi tiết sản phẩm:");
                    sb.AppendLine("--------------------------------------------------");
                    sb.AppendLine(string.Format("{0,-15} {1,10} {2,10} {3,12}", "Tên Sản phẩm", "Đơn Giá", "Số Lượng", "Thành Tiền"));
                    sb.AppendLine("--------------------------------------------------");

                    foreach (var detail in invoiceToExport.InvoiceDetails)
                    {
                        sb.AppendLine(string.Format("{0,-15} {1,9} {2,8:N0} {3,13:N0}",
                            // detail.ProductId,
                            detail.ProductName,
                            detail.UnitPrice,
                            detail.Quantity,
                            detail.LineAmount));
                    }

                    sb.AppendLine("--------------------------------------------------");
                    sb.AppendLine($"Tổng Số Lượng:      {invoiceToExport.TotalQuantity}");
                    sb.AppendLine($"Tổng Tiền:          {invoiceToExport.TotalAmount:N0} VNĐ");
                    sb.AppendLine("**************************************************");
                    sb.AppendLine("Cảm ơn quý khách đã mua hàng!");
                    sb.AppendLine("**************************************************");

                    // Ghi nội dung vào file
                    File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8); // Sử dụng UTF8 để tránh lỗi font tiếng Việt

                    MessageBox.Show($"Hóa đơn đã được xuất thành công tới:\n{filePath}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboCardId_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nếu đang trong quá trình binding dữ liệu từ DGV/ComboBox khác, bỏ qua kiểm tra này
            if (_isBindingData)
            {
                return;
            }

            // Chỉ kiểm tra khi có mục được chọn và không phải là index -1 (khi reset)
            if (cboCardId.SelectedIndex != -1 && cboCardId.SelectedValue != null)
            {
                string selectedCardId = cboCardId.SelectedValue.ToString();
                // Kiểm tra xem thẻ này có đang được kích hoạt bởi phiếu khác không
                if (_saleInvoiceBLL.IsCardActive(selectedCardId))
                {
                    MessageBox.Show("Thẻ này đang được kích hoạt bởi một phiếu khác và không thể sử dụng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboCardId.SelectedIndex = -1; // Reset lại ComboBox thẻ
                }
            }
        }

        private void dgvCards_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _isBindingData = true; // Bật cờ trước khi gán dữ liệu

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCards.Rows[e.RowIndex];
                string selectedCardId = row.Cells["CardIdCol_Cards"].Value.ToString();
                string currentLinkedInvoiceId = row.Cells["CurrentInvoiceIdCol_Cards"].Value?.ToString();

                // Chỉ xử lý nếu có một phiếu liên kết hiện tại
                if (!string.IsNullOrEmpty(currentLinkedInvoiceId))
                {
                    // Tải thông tin phiếu đó vào các trường
                    SaleInvoiceDTO linkedInvoice = _saleInvoiceBLL.GetSaleInvoiceById(currentLinkedInvoiceId);

                    if (linkedInvoice != null)
                    {
                        txtInvoiceId.Text = linkedInvoice.InvoiceId;
                        cboCardId.SelectedValue = linkedInvoice.CardId;
                        cboEmployeeId.SelectedValue = linkedInvoice.EmployeeId;
                        dtpCreatedDate.Value = linkedInvoice.CreatedDate;
                        if (linkedInvoice.Status)
                        {
                            rdoPaid.Checked = true;
                        }
                        else
                        {
                            rdoPending.Checked = true;
                        }
                        //txtTotalQuantity.Text = linkedInvoice.TotalQuantity.ToString();
                        txtTotalAmount.Text = linkedInvoice.TotalAmount.ToString("N0");

                        txtInvoiceId.Enabled = false; // Khóa Mã phiếu

                        // Điều chỉnh trạng thái nút (như đã làm ở các phần trước)
                        bool isPaid = linkedInvoice.Status;
                        btnUpdateInvoice.Enabled = !isPaid;
                        btnDeleteInvoice.Enabled = !isPaid;
                        btnAddDetail.Enabled = !isPaid;
                        btnUpdateDetail.Enabled = !isPaid;
                        btnDeleteDetail.Enabled = !isPaid;
                        btnExport.Enabled = isPaid;

                        // Load chi tiết phiếu liên kết vào dgvSaleInvoiceDetails
                        LoadSaleInvoiceDetailsToDataGridView(currentLinkedInvoiceId);
                    }
                }
                else
                {
                    // Nếu thẻ không liên kết với phiếu nào (trường hợp Status = 0)
                    ClearInvoiceFields(); // Xóa các trường của phiếu chính
                    MessageBox.Show("Thẻ này không liên kết với phiếu đang chờ xác nhận nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            _isBindingData = false; // Tắt cờ sau khi hoàn thành gán dữ liệu
        }
        
    }
}

