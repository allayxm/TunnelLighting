namespace NCLT.TunnelLighting.ControlCell
{
    partial class BasicSetupControl
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label_SDMC = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown_TimeOut = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown_CloseLight = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown_BaudRate = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_PortName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_TunnelName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Save = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown_LightFixTime = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.numericUpDown_AllowError = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBox_Debug = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TimeOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CloseLight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BaudRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LightFixTime)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AllowError)).BeginInit();
            this.SuspendLayout();
            // 
            // label_SDMC
            // 
            this.label_SDMC.AutoSize = true;
            this.label_SDMC.Font = new System.Drawing.Font("黑体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_SDMC.Location = new System.Drawing.Point(71, 29);
            this.label_SDMC.Name = "label_SDMC";
            this.label_SDMC.Size = new System.Drawing.Size(143, 33);
            this.label_SDMC.TabIndex = 0;
            this.label_SDMC.Text = "隧道名称";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_Debug);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.numericUpDown_TimeOut);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericUpDown_CloseLight);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericUpDown_BaudRate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox_PortName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_TunnelName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 195);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本参数";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(183, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "秒";
            // 
            // numericUpDown_TimeOut
            // 
            this.numericUpDown_TimeOut.Location = new System.Drawing.Point(114, 53);
            this.numericUpDown_TimeOut.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericUpDown_TimeOut.Name = "numericUpDown_TimeOut";
            this.numericUpDown_TimeOut.Size = new System.Drawing.Size(63, 21);
            this.numericUpDown_TimeOut.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "模块通讯超时:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(184, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "分钟";
            // 
            // numericUpDown_CloseLight
            // 
            this.numericUpDown_CloseLight.Location = new System.Drawing.Point(114, 146);
            this.numericUpDown_CloseLight.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_CloseLight.Name = "numericUpDown_CloseLight";
            this.numericUpDown_CloseLight.Size = new System.Drawing.Size(64, 21);
            this.numericUpDown_CloseLight.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "延时关灯:";
            // 
            // numericUpDown_BaudRate
            // 
            this.numericUpDown_BaudRate.Location = new System.Drawing.Point(114, 116);
            this.numericUpDown_BaudRate.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_BaudRate.Name = "numericUpDown_BaudRate";
            this.numericUpDown_BaudRate.Size = new System.Drawing.Size(140, 21);
            this.numericUpDown_BaudRate.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "波特率:";
            // 
            // comboBox_PortName
            // 
            this.comboBox_PortName.FormattingEnabled = true;
            this.comboBox_PortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9"});
            this.comboBox_PortName.Location = new System.Drawing.Point(114, 86);
            this.comboBox_PortName.Name = "comboBox_PortName";
            this.comboBox_PortName.Size = new System.Drawing.Size(141, 20);
            this.comboBox_PortName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "通讯端口:";
            // 
            // textBox_TunnelName
            // 
            this.textBox_TunnelName.Location = new System.Drawing.Point(114, 20);
            this.textBox_TunnelName.Name = "textBox_TunnelName";
            this.textBox_TunnelName.Size = new System.Drawing.Size(142, 21);
            this.textBox_TunnelName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "隧道名称:";
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(96, 387);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(92, 38);
            this.button_Save.TabIndex = 7;
            this.button_Save.Text = "保　存";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "照度调校间隔时间:";
            // 
            // numericUpDown_LightFixTime
            // 
            this.numericUpDown_LightFixTime.Location = new System.Drawing.Point(114, 13);
            this.numericUpDown_LightFixTime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_LightFixTime.Name = "numericUpDown_LightFixTime";
            this.numericUpDown_LightFixTime.Size = new System.Drawing.Size(64, 21);
            this.numericUpDown_LightFixTime.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(184, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "分钟";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.numericUpDown_AllowError);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.numericUpDown_LightFixTime);
            this.groupBox2.Location = new System.Drawing.Point(4, 262);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 75);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "照度调校";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(184, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 18;
            this.label11.Text = "%";
            // 
            // numericUpDown_AllowError
            // 
            this.numericUpDown_AllowError.Location = new System.Drawing.Point(113, 39);
            this.numericUpDown_AllowError.Name = "numericUpDown_AllowError";
            this.numericUpDown_AllowError.Size = new System.Drawing.Size(64, 21);
            this.numericUpDown_AllowError.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 12);
            this.label10.TabIndex = 16;
            this.label10.Text = "主备照度允许误差:";
            // 
            // checkBox_Debug
            // 
            this.checkBox_Debug.AutoSize = true;
            this.checkBox_Debug.Location = new System.Drawing.Point(114, 174);
            this.checkBox_Debug.Name = "checkBox_Debug";
            this.checkBox_Debug.Size = new System.Drawing.Size(72, 16);
            this.checkBox_Debug.TabIndex = 13;
            this.checkBox_Debug.Text = "调试模式";
            this.checkBox_Debug.UseVisualStyleBackColor = true;
            // 
            // BasicSetupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label_SDMC);
            this.Name = "BasicSetupControl";
            this.Size = new System.Drawing.Size(285, 430);
            this.Load += new System.EventHandler(this.BasicSetupControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TimeOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CloseLight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BaudRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LightFixTime)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AllowError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_SDMC;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_TunnelName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_PortName;
        private System.Windows.Forms.NumericUpDown numericUpDown_BaudRate;
        private System.Windows.Forms.NumericUpDown numericUpDown_CloseLight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown_TimeOut;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDown_LightFixTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDown_AllowError;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBox_Debug;
    }
}
