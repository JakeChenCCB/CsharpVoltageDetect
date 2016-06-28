namespace VoltageDetect
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPortUart = new System.IO.Ports.SerialPort(this.components);
            this.comboBoxUart = new System.Windows.Forms.ComboBox();
            this.buttonUartSw = new System.Windows.Forms.Button();
            this.timerComRefresh = new System.Windows.Forms.Timer(this.components);
            this.textBoxDisplay = new System.Windows.Forms.TextBox();
            this.labelUint = new System.Windows.Forms.Label();
            this.labelValue = new System.Windows.Forms.Label();
            this.comboBoxUart1 = new System.Windows.Forms.ComboBox();
            this.buttonUart1Sw = new System.Windows.Forms.Button();
            this.textBoxDisplay1 = new System.Windows.Forms.TextBox();
            this.labelValue1 = new System.Windows.Forms.Label();
            this.labelUint1 = new System.Windows.Forms.Label();
            this.serialPortUart1 = new System.IO.Ports.SerialPort(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxVolAndCrt = new System.Windows.Forms.TextBox();
            this.timerSecond = new System.Windows.Forms.Timer(this.components);
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonAutoSave = new System.Windows.Forms.Button();
            this.buttonDeleteLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serialPortUart
            // 
            this.serialPortUart.BaudRate = 2400;
            this.serialPortUart.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortUart_DataReceived);
            // 
            // comboBoxUart
            // 
            this.comboBoxUart.FormattingEnabled = true;
            this.comboBoxUart.Location = new System.Drawing.Point(103, 26);
            this.comboBoxUart.Name = "comboBoxUart";
            this.comboBoxUart.Size = new System.Drawing.Size(121, 20);
            this.comboBoxUart.TabIndex = 0;
            // 
            // buttonUartSw
            // 
            this.buttonUartSw.Location = new System.Drawing.Point(244, 26);
            this.buttonUartSw.Name = "buttonUartSw";
            this.buttonUartSw.Size = new System.Drawing.Size(75, 23);
            this.buttonUartSw.TabIndex = 1;
            this.buttonUartSw.Text = "Open";
            this.buttonUartSw.UseVisualStyleBackColor = true;
            this.buttonUartSw.Click += new System.EventHandler(this.buttonUartSw_Click);
            // 
            // timerComRefresh
            // 
            this.timerComRefresh.Enabled = true;
            this.timerComRefresh.Tick += new System.EventHandler(this.timerComRefresh_Tick);
            // 
            // textBoxDisplay
            // 
            this.textBoxDisplay.Location = new System.Drawing.Point(25, 55);
            this.textBoxDisplay.Multiline = true;
            this.textBoxDisplay.Name = "textBoxDisplay";
            this.textBoxDisplay.ReadOnly = true;
            this.textBoxDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDisplay.Size = new System.Drawing.Size(294, 128);
            this.textBoxDisplay.TabIndex = 2;
            // 
            // labelUint
            // 
            this.labelUint.AutoSize = true;
            this.labelUint.Font = new System.Drawing.Font("宋体", 48F, System.Drawing.FontStyle.Bold);
            this.labelUint.Location = new System.Drawing.Point(369, 119);
            this.labelUint.Name = "labelUint";
            this.labelUint.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelUint.Size = new System.Drawing.Size(160, 64);
            this.labelUint.TabIndex = 3;
            this.labelUint.Text = "Uint";
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Font = new System.Drawing.Font("宋体", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelValue.Location = new System.Drawing.Point(325, 55);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(193, 64);
            this.labelValue.TabIndex = 4;
            this.labelValue.Text = "Value";
            // 
            // comboBoxUart1
            // 
            this.comboBoxUart1.FormattingEnabled = true;
            this.comboBoxUart1.Location = new System.Drawing.Point(103, 203);
            this.comboBoxUart1.Name = "comboBoxUart1";
            this.comboBoxUart1.Size = new System.Drawing.Size(121, 20);
            this.comboBoxUart1.TabIndex = 5;
            // 
            // buttonUart1Sw
            // 
            this.buttonUart1Sw.Location = new System.Drawing.Point(244, 201);
            this.buttonUart1Sw.Name = "buttonUart1Sw";
            this.buttonUart1Sw.Size = new System.Drawing.Size(75, 23);
            this.buttonUart1Sw.TabIndex = 6;
            this.buttonUart1Sw.Text = "Open";
            this.buttonUart1Sw.UseVisualStyleBackColor = true;
            this.buttonUart1Sw.Click += new System.EventHandler(this.buttonUart1Sw_Click);
            // 
            // textBoxDisplay1
            // 
            this.textBoxDisplay1.Location = new System.Drawing.Point(25, 230);
            this.textBoxDisplay1.Multiline = true;
            this.textBoxDisplay1.Name = "textBoxDisplay1";
            this.textBoxDisplay1.ReadOnly = true;
            this.textBoxDisplay1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDisplay1.Size = new System.Drawing.Size(294, 128);
            this.textBoxDisplay1.TabIndex = 7;
            // 
            // labelValue1
            // 
            this.labelValue1.AutoSize = true;
            this.labelValue1.Font = new System.Drawing.Font("宋体", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelValue1.Location = new System.Drawing.Point(325, 230);
            this.labelValue1.Name = "labelValue1";
            this.labelValue1.Size = new System.Drawing.Size(193, 64);
            this.labelValue1.TabIndex = 8;
            this.labelValue1.Text = "Value";
            // 
            // labelUint1
            // 
            this.labelUint1.AutoSize = true;
            this.labelUint1.Font = new System.Drawing.Font("宋体", 48F, System.Drawing.FontStyle.Bold);
            this.labelUint1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.labelUint1.Location = new System.Drawing.Point(369, 294);
            this.labelUint1.Name = "labelUint1";
            this.labelUint1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelUint1.Size = new System.Drawing.Size(160, 64);
            this.labelUint1.TabIndex = 9;
            this.labelUint1.Text = "Uint";
            this.labelUint1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // serialPortUart1
            // 
            this.serialPortUart1.BaudRate = 2400;
            this.serialPortUart1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortUart1_DataReceived);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "Voltage";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "Current";
            // 
            // textBoxVolAndCrt
            // 
            this.textBoxVolAndCrt.Location = new System.Drawing.Point(597, 55);
            this.textBoxVolAndCrt.Multiline = true;
            this.textBoxVolAndCrt.Name = "textBoxVolAndCrt";
            this.textBoxVolAndCrt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxVolAndCrt.Size = new System.Drawing.Size(396, 303);
            this.textBoxVolAndCrt.TabIndex = 12;
            // 
            // timerSecond
            // 
            this.timerSecond.Enabled = true;
            this.timerSecond.Interval = 1000;
            this.timerSecond.Tick += new System.EventHandler(this.timerSecond_Tick);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(999, 55);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 13;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonAutoSave
            // 
            this.buttonAutoSave.Location = new System.Drawing.Point(1000, 85);
            this.buttonAutoSave.Name = "buttonAutoSave";
            this.buttonAutoSave.Size = new System.Drawing.Size(75, 23);
            this.buttonAutoSave.TabIndex = 14;
            this.buttonAutoSave.Text = "AutoSave";
            this.buttonAutoSave.UseVisualStyleBackColor = true;
            this.buttonAutoSave.Click += new System.EventHandler(this.buttonAutoSave_Click);
            // 
            // buttonDeleteLog
            // 
            this.buttonDeleteLog.Location = new System.Drawing.Point(1000, 115);
            this.buttonDeleteLog.Name = "buttonDeleteLog";
            this.buttonDeleteLog.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteLog.TabIndex = 15;
            this.buttonDeleteLog.Text = "DeleteLog";
            this.buttonDeleteLog.UseVisualStyleBackColor = true;
            this.buttonDeleteLog.Click += new System.EventHandler(this.buttonDeleteLog_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1127, 373);
            this.Controls.Add(this.buttonDeleteLog);
            this.Controls.Add(this.buttonAutoSave);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.textBoxVolAndCrt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelUint1);
            this.Controls.Add(this.labelValue1);
            this.Controls.Add(this.textBoxDisplay1);
            this.Controls.Add(this.buttonUart1Sw);
            this.Controls.Add(this.comboBoxUart1);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.labelUint);
            this.Controls.Add(this.textBoxDisplay);
            this.Controls.Add(this.buttonUartSw);
            this.Controls.Add(this.comboBoxUart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPortUart;
        private System.Windows.Forms.ComboBox comboBoxUart;
        private System.Windows.Forms.Button buttonUartSw;
        private System.Windows.Forms.Timer timerComRefresh;
        private System.Windows.Forms.TextBox textBoxDisplay;
        private System.Windows.Forms.Label labelUint;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.ComboBox comboBoxUart1;
        private System.Windows.Forms.Button buttonUart1Sw;
        private System.Windows.Forms.TextBox textBoxDisplay1;
        private System.Windows.Forms.Label labelValue1;
        private System.Windows.Forms.Label labelUint1;
        private System.IO.Ports.SerialPort serialPortUart1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxVolAndCrt;
        private System.Windows.Forms.Timer timerSecond;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonAutoSave;
        private System.Windows.Forms.Button buttonDeleteLog;
    }
}

