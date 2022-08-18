namespace CW_1608
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
            this.pbx_Screen = new System.Windows.Forms.PictureBox();
            this.vlw_Screenshots = new System.Windows.Forms.ListView();
            this.btn_Start = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Screen)).BeginInit();
            this.SuspendLayout();
            // 
            // pbx_Screen
            // 
            this.pbx_Screen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbx_Screen.BackColor = System.Drawing.Color.Transparent;
            this.pbx_Screen.Location = new System.Drawing.Point(12, 12);
            this.pbx_Screen.Name = "pbx_Screen";
            this.pbx_Screen.Size = new System.Drawing.Size(704, 365);
            this.pbx_Screen.TabIndex = 0;
            this.pbx_Screen.TabStop = false;
            // 
            // vlw_Screenshots
            // 
            this.vlw_Screenshots.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vlw_Screenshots.BackColor = System.Drawing.Color.Silver;
            this.vlw_Screenshots.HideSelection = false;
            this.vlw_Screenshots.Location = new System.Drawing.Point(12, 383);
            this.vlw_Screenshots.Name = "vlw_Screenshots";
            this.vlw_Screenshots.Size = new System.Drawing.Size(591, 96);
            this.vlw_Screenshots.TabIndex = 1;
            this.vlw_Screenshots.UseCompatibleStateImageBehavior = false;
            this.vlw_Screenshots.SelectedIndexChanged += new System.EventHandler(this.vlw_Screenshots_SelectedIndexChanged);
            // 
            // btn_Start
            // 
            this.btn_Start.BackColor = System.Drawing.Color.Transparent;
            this.btn_Start.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Start.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btn_Start.Location = new System.Drawing.Point(609, 383);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(107, 96);
            this.btn_Start.TabIndex = 2;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 491);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.vlw_Screenshots);
            this.Controls.Add(this.pbx_Screen);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Screen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbx_Screen;
        private System.Windows.Forms.ListView vlw_Screenshots;
        private System.Windows.Forms.Button btn_Start;
    }
}

