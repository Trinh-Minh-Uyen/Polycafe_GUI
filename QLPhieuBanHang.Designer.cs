using System.Drawing;
using System.Windows.Forms;

namespace Polycafe_GUI
{
    partial class QLPhieuBanHang
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvCards = new System.Windows.Forms.DataGridView();
            this.dgvSaleInvoiceDetails = new System.Windows.Forms.DataGridView();
            this.dgvSaleInvoices = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.rdoPaid = new System.Windows.Forms.RadioButton();
            this.btnResetInvoice = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDeleteInvoice = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.rdoPending = new System.Windows.Forms.RadioButton();
            this.cboEmployeeId = new System.Windows.Forms.ComboBox();
            this.btnUpdateInvoice = new System.Windows.Forms.Button();
            this.btnAddInvoice = new System.Windows.Forms.Button();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.dtpCreatedDate = new System.Windows.Forms.DateTimePicker();
            this.cboCardId = new System.Windows.Forms.ComboBox();
            this.txtInvoiceId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboProductId = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboFind = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnResetDetail = new System.Windows.Forms.Button();
            this.btnDeleteDetail = new System.Windows.Forms.Button();
            this.btnUpdateDetail = new System.Windows.Forms.Button();
            this.btnAddDetail = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCards)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleInvoiceDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleInvoices)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvCards);
            this.groupBox2.Controls.Add(this.dgvSaleInvoiceDetails);
            this.groupBox2.Controls.Add(this.dgvSaleInvoices);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox2.Location = new System.Drawing.Point(6, 273);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(1112, 551);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Quản lý";
            // 
            // dgvCards
            // 
            this.dgvCards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCards.Location = new System.Drawing.Point(5, 299);
            this.dgvCards.Margin = new System.Windows.Forms.Padding(2);
            this.dgvCards.Name = "dgvCards";
            this.dgvCards.RowHeadersWidth = 51;
            this.dgvCards.Size = new System.Drawing.Size(551, 248);
            this.dgvCards.TabIndex = 7;
            this.dgvCards.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCards_CellClick);
            // 
            // dgvSaleInvoiceDetails
            // 
            this.dgvSaleInvoiceDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSaleInvoiceDetails.Location = new System.Drawing.Point(560, 299);
            this.dgvSaleInvoiceDetails.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSaleInvoiceDetails.Name = "dgvSaleInvoiceDetails";
            this.dgvSaleInvoiceDetails.RowHeadersWidth = 51;
            this.dgvSaleInvoiceDetails.Size = new System.Drawing.Size(548, 248);
            this.dgvSaleInvoiceDetails.TabIndex = 6;
            this.dgvSaleInvoiceDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSaleInvoiceDetails_CellClick);
            // 
            // dgvSaleInvoices
            // 
            this.dgvSaleInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSaleInvoices.Location = new System.Drawing.Point(4, 15);
            this.dgvSaleInvoices.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSaleInvoices.Name = "dgvSaleInvoices";
            this.dgvSaleInvoices.RowHeadersWidth = 51;
            this.dgvSaleInvoices.Size = new System.Drawing.Size(1104, 280);
            this.dgvSaleInvoices.TabIndex = 5;
            this.dgvSaleInvoices.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSaleInvoices_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.rdoPaid);
            this.groupBox1.Controls.Add(this.btnResetInvoice);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnDeleteInvoice);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.rdoPending);
            this.groupBox1.Controls.Add(this.cboEmployeeId);
            this.groupBox1.Controls.Add(this.btnUpdateInvoice);
            this.groupBox1.Controls.Add(this.btnAddInvoice);
            this.groupBox1.Controls.Add(this.txtTotalAmount);
            this.groupBox1.Controls.Add(this.dtpCreatedDate);
            this.groupBox1.Controls.Add(this.cboCardId);
            this.groupBox1.Controls.Add(this.txtInvoiceId);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(6, 100);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(555, 168);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin phiếu";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label10.Location = new System.Drawing.Point(300, 27);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 22);
            this.label10.TabIndex = 39;
            this.label10.Text = "Thành tiền";
            // 
            // rdoPaid
            // 
            this.rdoPaid.AutoSize = true;
            this.rdoPaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.rdoPaid.Location = new System.Drawing.Point(399, 87);
            this.rdoPaid.Margin = new System.Windows.Forms.Padding(2);
            this.rdoPaid.Name = "rdoPaid";
            this.rdoPaid.Size = new System.Drawing.Size(141, 26);
            this.rdoPaid.TabIndex = 19;
            this.rdoPaid.TabStop = true;
            this.rdoPaid.Text = "Đã thanh toán";
            this.rdoPaid.UseVisualStyleBackColor = true;
            // 
            // btnResetInvoice
            // 
            this.btnResetInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnResetInvoice.Location = new System.Drawing.Point(401, 136);
            this.btnResetInvoice.Margin = new System.Windows.Forms.Padding(2);
            this.btnResetInvoice.Name = "btnResetInvoice";
            this.btnResetInvoice.Size = new System.Drawing.Size(102, 28);
            this.btnResetInvoice.TabIndex = 33;
            this.btnResetInvoice.Text = "Quay lại";
            this.btnResetInvoice.UseVisualStyleBackColor = true;
            this.btnResetInvoice.Click += new System.EventHandler(this.btnResetInvoice_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label7.Location = new System.Drawing.Point(300, 58);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 22);
            this.label7.TabIndex = 26;
            this.label7.Text = "Trạng thái";
            // 
            // btnDeleteInvoice
            // 
            this.btnDeleteInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnDeleteInvoice.Location = new System.Drawing.Point(285, 136);
            this.btnDeleteInvoice.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteInvoice.Name = "btnDeleteInvoice";
            this.btnDeleteInvoice.Size = new System.Drawing.Size(89, 28);
            this.btnDeleteInvoice.TabIndex = 32;
            this.btnDeleteInvoice.Text = "Xóa";
            this.btnDeleteInvoice.UseVisualStyleBackColor = true;
            this.btnDeleteInvoice.Click += new System.EventHandler(this.btnDeleteInvoice_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label6.Location = new System.Drawing.Point(10, 78);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 20);
            this.label6.TabIndex = 25;
            this.label6.Text = "Mã NV";
            // 
            // rdoPending
            // 
            this.rdoPending.AutoSize = true;
            this.rdoPending.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.rdoPending.Location = new System.Drawing.Point(399, 57);
            this.rdoPending.Margin = new System.Windows.Forms.Padding(2);
            this.rdoPending.Name = "rdoPending";
            this.rdoPending.Size = new System.Drawing.Size(139, 26);
            this.rdoPending.TabIndex = 18;
            this.rdoPending.TabStop = true;
            this.rdoPending.Text = "Chờ xác nhận";
            this.rdoPending.UseVisualStyleBackColor = true;
            // 
            // cboEmployeeId
            // 
            this.cboEmployeeId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cboEmployeeId.FormattingEnabled = true;
            this.cboEmployeeId.Location = new System.Drawing.Point(107, 70);
            this.cboEmployeeId.Margin = new System.Windows.Forms.Padding(2);
            this.cboEmployeeId.Name = "cboEmployeeId";
            this.cboEmployeeId.Size = new System.Drawing.Size(179, 21);
            this.cboEmployeeId.TabIndex = 20;
            // 
            // btnUpdateInvoice
            // 
            this.btnUpdateInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnUpdateInvoice.Location = new System.Drawing.Point(149, 136);
            this.btnUpdateInvoice.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateInvoice.Name = "btnUpdateInvoice";
            this.btnUpdateInvoice.Size = new System.Drawing.Size(99, 28);
            this.btnUpdateInvoice.TabIndex = 31;
            this.btnUpdateInvoice.Text = "Cập nhật";
            this.btnUpdateInvoice.UseVisualStyleBackColor = true;
            this.btnUpdateInvoice.Click += new System.EventHandler(this.btnUpdateInvoice_Click);
            // 
            // btnAddInvoice
            // 
            this.btnAddInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnAddInvoice.Location = new System.Drawing.Point(31, 136);
            this.btnAddInvoice.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddInvoice.Name = "btnAddInvoice";
            this.btnAddInvoice.Size = new System.Drawing.Size(87, 28);
            this.btnAddInvoice.TabIndex = 30;
            this.btnAddInvoice.Text = "Thêm";
            this.btnAddInvoice.UseVisualStyleBackColor = true;
            this.btnAddInvoice.Click += new System.EventHandler(this.btnAddInvoice_Click);
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtTotalAmount.Location = new System.Drawing.Point(399, 23);
            this.txtTotalAmount.Margin = new System.Windows.Forms.Padding(2);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(137, 20);
            this.txtTotalAmount.TabIndex = 35;
            // 
            // dtpCreatedDate
            // 
            this.dtpCreatedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dtpCreatedDate.Location = new System.Drawing.Point(107, 99);
            this.dtpCreatedDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpCreatedDate.Name = "dtpCreatedDate";
            this.dtpCreatedDate.Size = new System.Drawing.Size(229, 21);
            this.dtpCreatedDate.TabIndex = 17;
            // 
            // cboCardId
            // 
            this.cboCardId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cboCardId.FormattingEnabled = true;
            this.cboCardId.Location = new System.Drawing.Point(107, 46);
            this.cboCardId.Margin = new System.Windows.Forms.Padding(2);
            this.cboCardId.Name = "cboCardId";
            this.cboCardId.Size = new System.Drawing.Size(179, 21);
            this.cboCardId.TabIndex = 16;
            this.cboCardId.SelectedIndexChanged += new System.EventHandler(this.cboCardId_SelectedIndexChanged);
            // 
            // txtInvoiceId
            // 
            this.txtInvoiceId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtInvoiceId.Location = new System.Drawing.Point(107, 23);
            this.txtInvoiceId.Margin = new System.Windows.Forms.Padding(2);
            this.txtInvoiceId.Name = "txtInvoiceId";
            this.txtInvoiceId.Size = new System.Drawing.Size(179, 20);
            this.txtInvoiceId.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(10, 52);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Mã thẻ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(11, 99);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ngày tạo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(10, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mã phiếu";
            // 
            // cboProductId
            // 
            this.cboProductId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cboProductId.FormattingEnabled = true;
            this.cboProductId.Location = new System.Drawing.Point(133, 43);
            this.cboProductId.Margin = new System.Windows.Forms.Padding(2);
            this.cboProductId.Name = "cboProductId";
            this.cboProductId.Size = new System.Drawing.Size(144, 21);
            this.cboProductId.TabIndex = 21;
            this.cboProductId.SelectedIndexChanged += new System.EventHandler(this.cboProductId_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label5.Location = new System.Drawing.Point(30, 43);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 22);
            this.label5.TabIndex = 10;
            this.label5.Text = "Sản phẩm";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(16, 14);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(138, 53);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(457, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 32);
            this.label1.TabIndex = 7;
            this.label1.Text = "Quản lý bán hàng";
            // 
            // cboFind
            // 
            this.cboFind.FormattingEnabled = true;
            this.cboFind.Location = new System.Drawing.Point(496, 62);
            this.cboFind.Margin = new System.Windows.Forms.Padding(2);
            this.cboFind.Name = "cboFind";
            this.cboFind.Size = new System.Drawing.Size(234, 21);
            this.cboFind.TabIndex = 30;
            this.cboFind.SelectedIndexChanged += new System.EventHandler(this.cboFind_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label11.Location = new System.Drawing.Point(395, 57);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 25);
            this.label11.TabIndex = 30;
            this.label11.Text = "Tìm kiếm";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnResetDetail);
            this.groupBox3.Controls.Add(this.btnDeleteDetail);
            this.groupBox3.Controls.Add(this.btnUpdateDetail);
            this.groupBox3.Controls.Add(this.btnAddDetail);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.cboProductId);
            this.groupBox3.Controls.Add(this.nudQuantity);
            this.groupBox3.Controls.Add(this.txtUnitPrice);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox3.Location = new System.Drawing.Point(566, 100);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(552, 168);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Chi tiết";
            // 
            // btnResetDetail
            // 
            this.btnResetDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnResetDetail.Location = new System.Drawing.Point(420, 114);
            this.btnResetDetail.Margin = new System.Windows.Forms.Padding(2);
            this.btnResetDetail.Name = "btnResetDetail";
            this.btnResetDetail.Size = new System.Drawing.Size(103, 25);
            this.btnResetDetail.TabIndex = 40;
            this.btnResetDetail.Text = "Quay lại";
            this.btnResetDetail.UseVisualStyleBackColor = true;
            this.btnResetDetail.Click += new System.EventHandler(this.btnResetDetail_Click);
            // 
            // btnDeleteDetail
            // 
            this.btnDeleteDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnDeleteDetail.Location = new System.Drawing.Point(311, 114);
            this.btnDeleteDetail.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteDetail.Name = "btnDeleteDetail";
            this.btnDeleteDetail.Size = new System.Drawing.Size(91, 24);
            this.btnDeleteDetail.TabIndex = 40;
            this.btnDeleteDetail.Text = "Xóa";
            this.btnDeleteDetail.UseVisualStyleBackColor = true;
            this.btnDeleteDetail.Click += new System.EventHandler(this.btnDeleteDetail_Click);
            // 
            // btnUpdateDetail
            // 
            this.btnUpdateDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnUpdateDetail.Location = new System.Drawing.Point(420, 75);
            this.btnUpdateDetail.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateDetail.Name = "btnUpdateDetail";
            this.btnUpdateDetail.Size = new System.Drawing.Size(103, 26);
            this.btnUpdateDetail.TabIndex = 40;
            this.btnUpdateDetail.Text = "Cập nhật";
            this.btnUpdateDetail.UseVisualStyleBackColor = true;
            this.btnUpdateDetail.Click += new System.EventHandler(this.btnUpdateDetail_Click);
            // 
            // btnAddDetail
            // 
            this.btnAddDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnAddDetail.Location = new System.Drawing.Point(311, 75);
            this.btnAddDetail.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddDetail.Name = "btnAddDetail";
            this.btnAddDetail.Size = new System.Drawing.Size(91, 26);
            this.btnAddDetail.TabIndex = 40;
            this.btnAddDetail.Text = "Thêm";
            this.btnAddDetail.UseVisualStyleBackColor = true;
            this.btnAddDetail.Click += new System.EventHandler(this.btnAddDetail_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label9.Location = new System.Drawing.Point(32, 97);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 22);
            this.label9.TabIndex = 38;
            this.label9.Text = "Đơn giá ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label8.Location = new System.Drawing.Point(295, 38);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 25);
            this.label8.TabIndex = 37;
            this.label8.Text = "Số lượng";
            // 
            // nudQuantity
            // 
            this.nudQuantity.Location = new System.Drawing.Point(396, 42);
            this.nudQuantity.Margin = new System.Windows.Forms.Padding(2);
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(127, 23);
            this.nudQuantity.TabIndex = 36;
            this.nudQuantity.ValueChanged += new System.EventHandler(this.nudQuantity_ValueChanged);
            // 
            // txtUnitPrice
            // 
            this.txtUnitPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtUnitPrice.Location = new System.Drawing.Point(133, 100);
            this.txtUnitPrice.Margin = new System.Windows.Forms.Padding(2);
            this.txtUnitPrice.Name = "txtUnitPrice";
            this.txtUnitPrice.ReadOnly = true;
            this.txtUnitPrice.Size = new System.Drawing.Size(144, 20);
            this.txtUnitPrice.TabIndex = 34;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnExport.Location = new System.Drawing.Point(962, 58);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(113, 25);
            this.btnExport.TabIndex = 32;
            this.btnExport.Text = "Xuất Phiếu";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // QLPhieuBanHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cboFind);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "QLPhieuBanHang";
            this.Size = new System.Drawing.Size(1125, 826);
            this.Load += new System.EventHandler(this.QLPhieuBanHang_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCards)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleInvoiceDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleInvoices)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox2;
        private DataGridView dgvSaleInvoices;
        private GroupBox groupBox1;
        private RadioButton rdoPaid;
        private DateTimePicker dtpCreatedDate;
        private ComboBox cboCardId;
        private Label label5;
        private TextBox txtInvoiceId;
        private Label label4;
        private Label label3;
        private Label label2;
        private PictureBox pictureBox1;
        private Label label1;
        private RadioButton rdoPending;
        private ComboBox cboProductId;
        private ComboBox cboEmployeeId;
        private Label label7;
        private Label label6;
        private ComboBox cboFind;
        private Label label11;
        private GroupBox groupBox3;
        private Label label10;
        private TextBox txtTotalAmount;
        private Label label9;
        private Label label8;
        private Button btnResetInvoice;
        private Button btnDeleteInvoice;
        private NumericUpDown nudQuantity;
        private Button btnAddInvoice;
        private Button btnUpdateInvoice;
        private TextBox txtUnitPrice;
        private Button btnAddDetail;
        private DataGridView dgvSaleInvoiceDetails;
        private Button btnResetDetail;
        private Button btnDeleteDetail;
        private Button btnUpdateDetail;
        private Button btnExport;
        private DataGridView dgvCards;
    }
}
