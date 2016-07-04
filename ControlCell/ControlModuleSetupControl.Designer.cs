namespace NCLT.TunnelLighting.ControlCell
{
    partial class ControlModuleSetupControl
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
            this.button_Save = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_HighLevel = new System.Windows.Forms.CheckBox();
            this.numericUpDown_Address = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox_Port2 = new System.Windows.Forms.ComboBox();
            this.comboBox_Port1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox_Power = new System.Windows.Forms.CheckBox();
            this.checkBox_EmergencyPower = new System.Windows.Forms.CheckBox();
            this.checkBox_CarSensor = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_BDZ = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox_QD = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown_PWM = new System.Windows.Forms.NumericUpDown();
            this.radioButton_PWM_Dynamic = new System.Windows.Forms.RadioButton();
            this.radioButton_PWM_Fixup = new System.Windows.Forms.RadioButton();
            this.radioButton_PWM_None = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Address)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PWM)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(96, 384);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(92, 38);
            this.button_Save.TabIndex = 6;
            this.button_Save.Text = "保　存";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("黑体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(69, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 33);
            this.label4.TabIndex = 7;
            this.label4.Text = "模块设置";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_HighLevel);
            this.groupBox1.Controls.Add(this.numericUpDown_Address);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_Name);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 105);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本参数";
            // 
            // checkBox_HighLevel
            // 
            this.checkBox_HighLevel.AutoSize = true;
            this.checkBox_HighLevel.Location = new System.Drawing.Point(32, 78);
            this.checkBox_HighLevel.Name = "checkBox_HighLevel";
            this.checkBox_HighLevel.Size = new System.Drawing.Size(72, 16);
            this.checkBox_HighLevel.TabIndex = 26;
            this.checkBox_HighLevel.Text = "高优先级";
            this.checkBox_HighLevel.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_Address
            // 
            this.numericUpDown_Address.Location = new System.Drawing.Point(92, 49);
            this.numericUpDown_Address.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_Address.Name = "numericUpDown_Address";
            this.numericUpDown_Address.Size = new System.Drawing.Size(58, 21);
            this.numericUpDown_Address.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "设备地址:";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(92, 19);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(162, 21);
            this.textBox_Name.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "模块名称:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox_Port2);
            this.groupBox2.Controls.Add(this.comboBox_Port1);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.checkBox_Power);
            this.groupBox2.Controls.Add(this.checkBox_EmergencyPower);
            this.groupBox2.Controls.Add(this.checkBox_CarSensor);
            this.groupBox2.Location = new System.Drawing.Point(3, 159);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(279, 101);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "端口设置";
            // 
            // comboBox_Port2
            // 
            this.comboBox_Port2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Port2.FormattingEnabled = true;
            this.comboBox_Port2.Items.AddRange(new object[] {
            "无",
            "照度传感器",
            "CO传感器"});
            this.comboBox_Port2.Location = new System.Drawing.Point(46, 49);
            this.comboBox_Port2.Name = "comboBox_Port2";
            this.comboBox_Port2.Size = new System.Drawing.Size(102, 20);
            this.comboBox_Port2.TabIndex = 33;
            // 
            // comboBox_Port1
            // 
            this.comboBox_Port1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Port1.FormattingEnabled = true;
            this.comboBox_Port1.Items.AddRange(new object[] {
            "无",
            "照度传感器",
            "CO传感器"});
            this.comboBox_Port1.Location = new System.Drawing.Point(46, 20);
            this.comboBox_Port1.Name = "comboBox_Port1";
            this.comboBox_Port1.Size = new System.Drawing.Size(102, 20);
            this.comboBox_Port1.TabIndex = 32;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(153, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 31;
            this.label8.Text = "端口5:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(153, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 30;
            this.label7.Text = "端口4:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(153, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 29;
            this.label6.Text = "端口3:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 28;
            this.label5.Text = "端口2:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "端口1:";
            // 
            // checkBox_Power
            // 
            this.checkBox_Power.AutoSize = true;
            this.checkBox_Power.Location = new System.Drawing.Point(200, 76);
            this.checkBox_Power.Name = "checkBox_Power";
            this.checkBox_Power.Size = new System.Drawing.Size(60, 16);
            this.checkBox_Power.TabIndex = 26;
            this.checkBox_Power.Text = "主电源";
            this.checkBox_Power.UseVisualStyleBackColor = true;
            // 
            // checkBox_EmergencyPower
            // 
            this.checkBox_EmergencyPower.AutoSize = true;
            this.checkBox_EmergencyPower.Location = new System.Drawing.Point(200, 48);
            this.checkBox_EmergencyPower.Name = "checkBox_EmergencyPower";
            this.checkBox_EmergencyPower.Size = new System.Drawing.Size(72, 16);
            this.checkBox_EmergencyPower.TabIndex = 25;
            this.checkBox_EmergencyPower.Text = "后备电源";
            this.checkBox_EmergencyPower.UseVisualStyleBackColor = true;
            // 
            // checkBox_CarSensor
            // 
            this.checkBox_CarSensor.AutoSize = true;
            this.checkBox_CarSensor.Location = new System.Drawing.Point(200, 19);
            this.checkBox_CarSensor.Name = "checkBox_CarSensor";
            this.checkBox_CarSensor.Size = new System.Drawing.Size(72, 16);
            this.checkBox_CarSensor.TabIndex = 24;
            this.checkBox_CarSensor.Text = "地感线圈";
            this.checkBox_CarSensor.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.textBox_BDZ);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.comboBox_QD);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.numericUpDown_PWM);
            this.groupBox3.Controls.Add(this.radioButton_PWM_Dynamic);
            this.groupBox3.Controls.Add(this.radioButton_PWM_Fixup);
            this.groupBox3.Controls.Add(this.radioButton_PWM_None);
            this.groupBox3.Location = new System.Drawing.Point(3, 260);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(279, 118);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PWM设置";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(200, 94);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 12);
            this.label12.TabIndex = 39;
            this.label12.Text = "CD/m2";
            // 
            // textBox_BDZ
            // 
            this.textBox_BDZ.Location = new System.Drawing.Point(113, 90);
            this.textBox_BDZ.Name = "textBox_BDZ";
            this.textBox_BDZ.Size = new System.Drawing.Size(81, 21);
            this.textBox_BDZ.TabIndex = 38;
            this.textBox_BDZ.Text = "3500";
            this.textBox_BDZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_BDZ_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 94);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 12);
            this.label11.TabIndex = 37;
            this.label11.Text = "标定值(最大亮度):";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(81, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 36;
            this.label10.Text = "区段";
            // 
            // comboBox_QD
            // 
            this.comboBox_QD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_QD.Enabled = false;
            this.comboBox_QD.FormattingEnabled = true;
            this.comboBox_QD.Items.AddRange(new object[] {
            "1段",
            "2段",
            "3段",
            "4段",
            "5段",
            "6段"});
            this.comboBox_QD.Location = new System.Drawing.Point(113, 65);
            this.comboBox_QD.Name = "comboBox_QD";
            this.comboBox_QD.Size = new System.Drawing.Size(82, 20);
            this.comboBox_QD.TabIndex = 35;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(143, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 34;
            this.label9.Text = "%";
            // 
            // numericUpDown_PWM
            // 
            this.numericUpDown_PWM.Location = new System.Drawing.Point(80, 40);
            this.numericUpDown_PWM.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown_PWM.Name = "numericUpDown_PWM";
            this.numericUpDown_PWM.ReadOnly = true;
            this.numericUpDown_PWM.Size = new System.Drawing.Size(57, 21);
            this.numericUpDown_PWM.TabIndex = 33;
            this.numericUpDown_PWM.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // radioButton_PWM_Dynamic
            // 
            this.radioButton_PWM_Dynamic.AutoSize = true;
            this.radioButton_PWM_Dynamic.Location = new System.Drawing.Point(9, 67);
            this.radioButton_PWM_Dynamic.Name = "radioButton_PWM_Dynamic";
            this.radioButton_PWM_Dynamic.Size = new System.Drawing.Size(71, 16);
            this.radioButton_PWM_Dynamic.TabIndex = 32;
            this.radioButton_PWM_Dynamic.Text = "动态输出";
            this.radioButton_PWM_Dynamic.UseVisualStyleBackColor = true;
            this.radioButton_PWM_Dynamic.CheckedChanged += new System.EventHandler(this.radioButton_PWM_Dynamic_CheckedChanged);
            // 
            // radioButton_PWM_Fixup
            // 
            this.radioButton_PWM_Fixup.AutoSize = true;
            this.radioButton_PWM_Fixup.Location = new System.Drawing.Point(9, 42);
            this.radioButton_PWM_Fixup.Name = "radioButton_PWM_Fixup";
            this.radioButton_PWM_Fixup.Size = new System.Drawing.Size(71, 16);
            this.radioButton_PWM_Fixup.TabIndex = 31;
            this.radioButton_PWM_Fixup.Text = "固定输出";
            this.radioButton_PWM_Fixup.UseVisualStyleBackColor = true;
            this.radioButton_PWM_Fixup.CheckedChanged += new System.EventHandler(this.radioButton_PWM_Fixup_CheckedChanged);
            // 
            // radioButton_PWM_None
            // 
            this.radioButton_PWM_None.AutoSize = true;
            this.radioButton_PWM_None.Checked = true;
            this.radioButton_PWM_None.Location = new System.Drawing.Point(9, 18);
            this.radioButton_PWM_None.Name = "radioButton_PWM_None";
            this.radioButton_PWM_None.Size = new System.Drawing.Size(59, 16);
            this.radioButton_PWM_None.TabIndex = 30;
            this.radioButton_PWM_None.TabStop = true;
            this.radioButton_PWM_None.Text = "无输出";
            this.radioButton_PWM_None.UseVisualStyleBackColor = true;
            // 
            // ControlModuleSetupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_Save);
            this.Name = "ControlModuleSetupControl";
            this.Size = new System.Drawing.Size(285, 430);
            this.Load += new System.EventHandler(this.ControlModuleSetupControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Address)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PWM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_Address;
        private System.Windows.Forms.CheckBox checkBox_HighLevel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBox_Port2;
        private System.Windows.Forms.ComboBox comboBox_Port1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox_Power;
        private System.Windows.Forms.CheckBox checkBox_EmergencyPower;
        private System.Windows.Forms.CheckBox checkBox_CarSensor;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDown_PWM;
        private System.Windows.Forms.RadioButton radioButton_PWM_Dynamic;
        private System.Windows.Forms.RadioButton radioButton_PWM_Fixup;
        private System.Windows.Forms.RadioButton radioButton_PWM_None;
        private System.Windows.Forms.ComboBox comboBox_QD;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_BDZ;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}
