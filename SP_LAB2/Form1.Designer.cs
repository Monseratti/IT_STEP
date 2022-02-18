namespace SP_LAB2
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
            this.availableProcesses = new System.Windows.Forms.ListBox();
            this.executingProcesses = new System.Windows.Forms.ListBox();
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this._CloseWindow = new System.Windows.Forms.Button();
            this._Refresh = new System.Windows.Forms.Button();
            this.RunNotepad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // availableProcesses
            // 
            this.availableProcesses.FormattingEnabled = true;
            this.availableProcesses.Location = new System.Drawing.Point(12, 12);
            this.availableProcesses.Name = "availableProcesses";
            this.availableProcesses.Size = new System.Drawing.Size(271, 303);
            this.availableProcesses.TabIndex = 0;
            // 
            // executingProcesses
            // 
            this.executingProcesses.FormattingEnabled = true;
            this.executingProcesses.Location = new System.Drawing.Point(410, 12);
            this.executingProcesses.Name = "executingProcesses";
            this.executingProcesses.Size = new System.Drawing.Size(271, 303);
            this.executingProcesses.TabIndex = 1;
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(289, 37);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(115, 25);
            this.Start.TabIndex = 2;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(289, 68);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(115, 25);
            this.Stop.TabIndex = 3;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // _CloseWindow
            // 
            this._CloseWindow.Location = new System.Drawing.Point(289, 99);
            this._CloseWindow.Name = "_CloseWindow";
            this._CloseWindow.Size = new System.Drawing.Size(115, 25);
            this._CloseWindow.TabIndex = 4;
            this._CloseWindow.Text = "Close Window";
            this._CloseWindow.UseVisualStyleBackColor = true;
            this._CloseWindow.Click += new System.EventHandler(this.CloseWindow_Click);
            // 
            // _Refresh
            // 
            this._Refresh.Location = new System.Drawing.Point(289, 130);
            this._Refresh.Name = "_Refresh";
            this._Refresh.Size = new System.Drawing.Size(115, 25);
            this._Refresh.TabIndex = 5;
            this._Refresh.Text = "Refresh";
            this._Refresh.UseVisualStyleBackColor = true;
            this._Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // RunNotepad
            // 
            this.RunNotepad.Location = new System.Drawing.Point(289, 161);
            this.RunNotepad.Name = "RunNotepad";
            this.RunNotepad.Size = new System.Drawing.Size(115, 25);
            this.RunNotepad.TabIndex = 6;
            this.RunNotepad.Text = "Run Notepad";
            this.RunNotepad.UseVisualStyleBackColor = true;
            this.RunNotepad.Click += new System.EventHandler(this.RunNotepad_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 329);
            this.Controls.Add(this.RunNotepad);
            this.Controls.Add(this._Refresh);
            this.Controls.Add(this._CloseWindow);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.executingProcesses);
            this.Controls.Add(this.availableProcesses);
            this.MaximumSize = new System.Drawing.Size(710, 368);
            this.MinimumSize = new System.Drawing.Size(710, 368);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox availableProcesses;
        private System.Windows.Forms.ListBox executingProcesses;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button _CloseWindow;
        private System.Windows.Forms.Button _Refresh;
        private System.Windows.Forms.Button RunNotepad;
    }
}

