namespace CW_0908
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbxID = new System.Windows.Forms.TextBox();
            this.btnSendID = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxID
            // 
            this.tbxID.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbxID.Location = new System.Drawing.Point(0, 0);
            this.tbxID.Name = "tbxID";
            this.tbxID.Size = new System.Drawing.Size(484, 20);
            this.tbxID.TabIndex = 0;
            // 
            // btnSendID
            // 
            this.btnSendID.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSendID.Location = new System.Drawing.Point(0, 20);
            this.btnSendID.Name = "btnSendID";
            this.btnSendID.Size = new System.Drawing.Size(484, 20);
            this.btnSendID.TabIndex = 1;
            this.btnSendID.Text = "Get data";
            this.btnSendID.UseVisualStyleBackColor = true;
            this.btnSendID.Click += new System.EventHandler(this.btnSendID_Click);
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 40);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(484, 371);
            this.dgvData.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 411);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btnSendID);
            this.Controls.Add(this.tbxID);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxID;
        private System.Windows.Forms.Button btnSendID;
        private System.Windows.Forms.DataGridView dgvData;
    }
}

