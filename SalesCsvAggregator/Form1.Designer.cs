namespace SalesCsvAggregator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtInputPath = new TextBox();
            btnBrowse = new Button();
            btnAggregate = new Button();
            lblStatus = new Label();
            dgvResult = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvResult).BeginInit();
            SuspendLayout();
            // 
            // txtInputPath
            // 
            txtInputPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtInputPath.Location = new Point(12, 12);
            txtInputPath.Name = "txtInputPath";
            txtInputPath.ReadOnly = true;
            txtInputPath.Size = new Size(628, 23);
            txtInputPath.TabIndex = 0;
            // 
            // btnBrowse
            // 
            btnBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBrowse.Location = new Point(662, 12);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(126, 23);
            btnBrowse.TabIndex = 1;
            btnBrowse.Text = "CSVを選択";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnAggregate
            // 
            btnAggregate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAggregate.Location = new Point(12, 380);
            btnAggregate.Name = "btnAggregate";
            btnAggregate.Size = new Size(139, 58);
            btnAggregate.TabIndex = 2;
            btnAggregate.Text = "集計開始";
            btnAggregate.UseVisualStyleBackColor = true;
            btnAggregate.Click += btnAggregate_Click;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 47);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 15);
            lblStatus.TabIndex = 3;
            // 
            // dgvResult
            // 
            dgvResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResult.Location = new Point(12, 74);
            dgvResult.Name = "dgvResult";
            dgvResult.Size = new Size(776, 300);
            dgvResult.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvResult);
            Controls.Add(lblStatus);
            Controls.Add(btnAggregate);
            Controls.Add(btnBrowse);
            Controls.Add(txtInputPath);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvResult).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtInputPath;
        private Button btnBrowse;
        private Button btnAggregate;
        private Label lblStatus;
        private DataGridView dgvResult;
    }
}
