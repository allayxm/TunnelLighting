namespace NCLT.TunnelLighting.ControlCell
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel_Buttons = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox_RX = new System.Windows.Forms.PictureBox();
            this.pictureBox_TX = new System.Windows.Forms.PictureBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label_Title = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel_Module = new System.Windows.Forms.Panel();
            this.label_Communication = new System.Windows.Forms.Label();
            this.label_ModuleName = new System.Windows.Forms.Label();
            this.label_PWM = new System.Windows.Forms.Label();
            this.label_PowerSensor = new System.Windows.Forms.Label();
            this.label_EmergencyPowerSensor = new System.Windows.Forms.Label();
            this.label_LightenessSensor2 = new System.Windows.Forms.Label();
            this.label_LightenessSensor1 = new System.Windows.Forms.Label();
            this.label_CarSensor = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.listBox_Info = new System.Windows.Forms.ListBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.checkBox_Error = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.checkBox_Alert = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBox_Message = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox_Setup = new System.Windows.Forms.PictureBox();
            this.pictureBox_Stop = new System.Windows.Forms.PictureBox();
            this.pictureBox_Run = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel1_PWmCurve = new System.Windows.Forms.Panel();
            this.chart_PWM = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_TX)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel_Module.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Setup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Stop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Run)).BeginInit();
            this.panel9.SuspendLayout();
            this.panel1_PWmCurve.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_PWM)).BeginInit();
            this.SuspendLayout();
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(105, 605);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 12);
            this.label14.TabIndex = 13;
            this.label14.Text = "接收";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(56, 605);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 1;
            this.label13.Text = "发送";
            // 
            // panel_Buttons
            // 
            this.panel_Buttons.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel_Buttons.AutoScroll = true;
            this.panel_Buttons.Location = new System.Drawing.Point(30, 240);
            this.panel_Buttons.Name = "panel_Buttons";
            this.panel_Buttons.Size = new System.Drawing.Size(126, 326);
            this.panel_Buttons.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.pictureBox_RX);
            this.panel4.Controls.Add(this.pictureBox_TX);
            this.panel4.Controls.Add(this.panel_Buttons);
            this.panel4.Controls.Add(this.panel13);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Location = new System.Drawing.Point(0, 118);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(159, 622);
            this.panel4.TabIndex = 12;
            // 
            // pictureBox_RX
            // 
            this.pictureBox_RX.Image = global::NCLT.TunnelLighting.ControlCell.Properties.Resources.LED_RX_Down;
            this.pictureBox_RX.Location = new System.Drawing.Point(108, 580);
            this.pictureBox_RX.Name = "pictureBox_RX";
            this.pictureBox_RX.Size = new System.Drawing.Size(21, 20);
            this.pictureBox_RX.TabIndex = 12;
            this.pictureBox_RX.TabStop = false;
            // 
            // pictureBox_TX
            // 
            this.pictureBox_TX.Image = global::NCLT.TunnelLighting.ControlCell.Properties.Resources.LED_TX_Down;
            this.pictureBox_TX.Location = new System.Drawing.Point(60, 580);
            this.pictureBox_TX.Name = "pictureBox_TX";
            this.pictureBox_TX.Size = new System.Drawing.Size(21, 20);
            this.pictureBox_TX.TabIndex = 11;
            this.pictureBox_TX.TabStop = false;
            // 
            // panel13
            // 
            this.panel13.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel13.BackgroundImage")));
            this.panel13.Location = new System.Drawing.Point(28, 238);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(131, 330);
            this.panel13.TabIndex = 10;
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel7.BackgroundImage")));
            this.panel7.Controls.Add(this.label_Title);
            this.panel7.Location = new System.Drawing.Point(30, 113);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(130, 124);
            this.panel7.TabIndex = 7;
            // 
            // label_Title
            // 
            this.label_Title.AutoSize = true;
            this.label_Title.BackColor = System.Drawing.Color.Transparent;
            this.label_Title.Font = new System.Drawing.Font("黑体", 14F);
            this.label_Title.Location = new System.Drawing.Point(7, 47);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(119, 19);
            this.label_Title.TabIndex = 8;
            this.label_Title.Text = "AH-64Dapaqi";
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel6.BackgroundImage")));
            this.panel6.Location = new System.Drawing.Point(30, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(129, 97);
            this.panel6.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.Font = new System.Drawing.Font("黑体", 13F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(325, 324);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(0, 0);
            this.button1.TabIndex = 10;
            this.button1.Text = "01";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel10
            // 
            this.panel10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel10.BackgroundImage")));
            this.panel10.Controls.Add(this.panel_Module);
            this.panel10.Location = new System.Drawing.Point(165, 511);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(547, 229);
            this.panel10.TabIndex = 15;
            // 
            // panel_Module
            // 
            this.panel_Module.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_Module.BackgroundImage")));
            this.panel_Module.Controls.Add(this.label_Communication);
            this.panel_Module.Controls.Add(this.label_ModuleName);
            this.panel_Module.Controls.Add(this.label_PWM);
            this.panel_Module.Controls.Add(this.label_PowerSensor);
            this.panel_Module.Controls.Add(this.label_EmergencyPowerSensor);
            this.panel_Module.Controls.Add(this.label_LightenessSensor2);
            this.panel_Module.Controls.Add(this.label_LightenessSensor1);
            this.panel_Module.Controls.Add(this.label_CarSensor);
            this.panel_Module.Location = new System.Drawing.Point(23, 38);
            this.panel_Module.Name = "panel_Module";
            this.panel_Module.Size = new System.Drawing.Size(511, 182);
            this.panel_Module.TabIndex = 0;
            // 
            // label_Communication
            // 
            this.label_Communication.AutoSize = true;
            this.label_Communication.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Communication.ForeColor = System.Drawing.Color.Blue;
            this.label_Communication.Location = new System.Drawing.Point(284, 154);
            this.label_Communication.Name = "label_Communication";
            this.label_Communication.Size = new System.Drawing.Size(31, 21);
            this.label_Communication.TabIndex = 8;
            this.label_Communication.Text = "无";
            // 
            // label_ModuleName
            // 
            this.label_ModuleName.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_ModuleName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(255)))));
            this.label_ModuleName.Location = new System.Drawing.Point(5, 7);
            this.label_ModuleName.Name = "label_ModuleName";
            this.label_ModuleName.Size = new System.Drawing.Size(83, 60);
            this.label_ModuleName.TabIndex = 7;
            this.label_ModuleName.Text = "模块";
            this.label_ModuleName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_PWM
            // 
            this.label_PWM.AutoSize = true;
            this.label_PWM.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_PWM.ForeColor = System.Drawing.Color.Blue;
            this.label_PWM.Location = new System.Drawing.Point(30, 154);
            this.label_PWM.Name = "label_PWM";
            this.label_PWM.Size = new System.Drawing.Size(31, 21);
            this.label_PWM.TabIndex = 6;
            this.label_PWM.Text = "无";
            // 
            // label_PowerSensor
            // 
            this.label_PowerSensor.AutoSize = true;
            this.label_PowerSensor.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_PowerSensor.ForeColor = System.Drawing.Color.Blue;
            this.label_PowerSensor.Location = new System.Drawing.Point(284, 127);
            this.label_PowerSensor.Name = "label_PowerSensor";
            this.label_PowerSensor.Size = new System.Drawing.Size(31, 21);
            this.label_PowerSensor.TabIndex = 5;
            this.label_PowerSensor.Text = "无";
            // 
            // label_EmergencyPowerSensor
            // 
            this.label_EmergencyPowerSensor.AutoSize = true;
            this.label_EmergencyPowerSensor.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_EmergencyPowerSensor.ForeColor = System.Drawing.Color.Blue;
            this.label_EmergencyPowerSensor.Location = new System.Drawing.Point(284, 98);
            this.label_EmergencyPowerSensor.Name = "label_EmergencyPowerSensor";
            this.label_EmergencyPowerSensor.Size = new System.Drawing.Size(31, 21);
            this.label_EmergencyPowerSensor.TabIndex = 4;
            this.label_EmergencyPowerSensor.Text = "无";
            // 
            // label_LightenessSensor2
            // 
            this.label_LightenessSensor2.AutoSize = true;
            this.label_LightenessSensor2.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_LightenessSensor2.ForeColor = System.Drawing.Color.Blue;
            this.label_LightenessSensor2.Location = new System.Drawing.Point(284, 69);
            this.label_LightenessSensor2.Name = "label_LightenessSensor2";
            this.label_LightenessSensor2.Size = new System.Drawing.Size(31, 21);
            this.label_LightenessSensor2.TabIndex = 3;
            this.label_LightenessSensor2.Text = "无";
            // 
            // label_LightenessSensor1
            // 
            this.label_LightenessSensor1.AutoSize = true;
            this.label_LightenessSensor1.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_LightenessSensor1.ForeColor = System.Drawing.Color.Blue;
            this.label_LightenessSensor1.Location = new System.Drawing.Point(284, 39);
            this.label_LightenessSensor1.Name = "label_LightenessSensor1";
            this.label_LightenessSensor1.Size = new System.Drawing.Size(31, 21);
            this.label_LightenessSensor1.TabIndex = 1;
            this.label_LightenessSensor1.Text = "无";
            // 
            // label_CarSensor
            // 
            this.label_CarSensor.AutoSize = true;
            this.label_CarSensor.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_CarSensor.ForeColor = System.Drawing.Color.Blue;
            this.label_CarSensor.Location = new System.Drawing.Point(284, 7);
            this.label_CarSensor.Name = "label_CarSensor";
            this.label_CarSensor.Size = new System.Drawing.Size(31, 21);
            this.label_CarSensor.TabIndex = 0;
            this.label_CarSensor.Text = "无";
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
            this.panel5.Controls.Add(this.label15);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 748);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1002, 20);
            this.panel5.TabIndex = 13;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(402, 4);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(233, 12);
            this.label15.TabIndex = 1;
            this.label15.Text = "技术支持：南昌路通高新技术有限责任公司";
            // 
            // panel11
            // 
            this.panel11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel11.BackgroundImage")));
            this.panel11.Controls.Add(this.listBox_Info);
            this.panel11.Controls.Add(this.pictureBox3);
            this.panel11.Controls.Add(this.checkBox_Error);
            this.panel11.Controls.Add(this.pictureBox2);
            this.panel11.Controls.Add(this.checkBox_Alert);
            this.panel11.Controls.Add(this.pictureBox1);
            this.panel11.Controls.Add(this.checkBox_Message);
            this.panel11.Location = new System.Drawing.Point(719, 511);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(280, 229);
            this.panel11.TabIndex = 16;
            // 
            // listBox_Info
            // 
            this.listBox_Info.FormattingEnabled = true;
            this.listBox_Info.HorizontalScrollbar = true;
            this.listBox_Info.ItemHeight = 12;
            this.listBox_Info.Location = new System.Drawing.Point(3, 38);
            this.listBox_Info.Name = "listBox_Info";
            this.listBox_Info.Size = new System.Drawing.Size(274, 160);
            this.listBox_Info.TabIndex = 7;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(200, 206);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(16, 16);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // checkBox_Error
            // 
            this.checkBox_Error.AutoSize = true;
            this.checkBox_Error.Checked = true;
            this.checkBox_Error.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Error.ForeColor = System.Drawing.Color.White;
            this.checkBox_Error.Location = new System.Drawing.Point(217, 207);
            this.checkBox_Error.Name = "checkBox_Error";
            this.checkBox_Error.Size = new System.Drawing.Size(48, 16);
            this.checkBox_Error.TabIndex = 5;
            this.checkBox_Error.Text = "错误";
            this.checkBox_Error.UseVisualStyleBackColor = true;
            this.checkBox_Error.CheckedChanged += new System.EventHandler(this.checkBox_Message_CheckedChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::NCLT.TunnelLighting.ControlCell.Properties.Resources.Alert;
            this.pictureBox2.Location = new System.Drawing.Point(103, 206);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // checkBox_Alert
            // 
            this.checkBox_Alert.AutoSize = true;
            this.checkBox_Alert.Checked = true;
            this.checkBox_Alert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Alert.ForeColor = System.Drawing.Color.White;
            this.checkBox_Alert.Location = new System.Drawing.Point(119, 207);
            this.checkBox_Alert.Name = "checkBox_Alert";
            this.checkBox_Alert.Size = new System.Drawing.Size(48, 16);
            this.checkBox_Alert.TabIndex = 3;
            this.checkBox_Alert.Text = "警告";
            this.checkBox_Alert.UseVisualStyleBackColor = true;
            this.checkBox_Alert.CheckedChanged += new System.EventHandler(this.checkBox_Message_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NCLT.TunnelLighting.ControlCell.Properties.Resources.Info;
            this.pictureBox1.Location = new System.Drawing.Point(10, 206);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // checkBox_Message
            // 
            this.checkBox_Message.AutoSize = true;
            this.checkBox_Message.Checked = true;
            this.checkBox_Message.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Message.ForeColor = System.Drawing.Color.White;
            this.checkBox_Message.Location = new System.Drawing.Point(27, 207);
            this.checkBox_Message.Name = "checkBox_Message";
            this.checkBox_Message.Size = new System.Drawing.Size(48, 16);
            this.checkBox_Message.TabIndex = 1;
            this.checkBox_Message.Text = "消息";
            this.checkBox_Message.UseVisualStyleBackColor = true;
            this.checkBox_Message.CheckedChanged += new System.EventHandler(this.checkBox_Message_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1002, 118);
            this.panel1.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.Controls.Add(this.pictureBox_Setup);
            this.panel3.Controls.Add(this.pictureBox_Stop);
            this.panel3.Controls.Add(this.pictureBox_Run);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(456, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(546, 118);
            this.panel3.TabIndex = 5;
            // 
            // pictureBox_Setup
            // 
            this.pictureBox_Setup.Image = global::NCLT.TunnelLighting.ControlCell.Properties.Resources.Setup_UP;
            this.pictureBox_Setup.Location = new System.Drawing.Point(446, 29);
            this.pictureBox_Setup.Name = "pictureBox_Setup";
            this.pictureBox_Setup.Size = new System.Drawing.Size(44, 68);
            this.pictureBox_Setup.TabIndex = 3;
            this.pictureBox_Setup.TabStop = false;
            this.pictureBox_Setup.Click += new System.EventHandler(this.pictureBox_Setup_Click);
            // 
            // pictureBox_Stop
            // 
            this.pictureBox_Stop.Image = global::NCLT.TunnelLighting.ControlCell.Properties.Resources.Stop_Down;
            this.pictureBox_Stop.Location = new System.Drawing.Point(362, 29);
            this.pictureBox_Stop.Name = "pictureBox_Stop";
            this.pictureBox_Stop.Size = new System.Drawing.Size(44, 68);
            this.pictureBox_Stop.TabIndex = 2;
            this.pictureBox_Stop.TabStop = false;
            this.pictureBox_Stop.Click += new System.EventHandler(this.pictureBox_Stop_Click);
            // 
            // pictureBox_Run
            // 
            this.pictureBox_Run.Image = global::NCLT.TunnelLighting.ControlCell.Properties.Resources.Run_UP;
            this.pictureBox_Run.Location = new System.Drawing.Point(278, 29);
            this.pictureBox_Run.Name = "pictureBox_Run";
            this.pictureBox_Run.Size = new System.Drawing.Size(44, 68);
            this.pictureBox_Run.TabIndex = 1;
            this.pictureBox_Run.TabStop = false;
            this.pictureBox_Run.Click += new System.EventHandler(this.pictureBox_Run_Click);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(458, 118);
            this.panel2.TabIndex = 1;
            // 
            // panel9
            // 
            this.panel9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel9.BackgroundImage")));
            this.panel9.Controls.Add(this.panel1_PWmCurve);
            this.panel9.Location = new System.Drawing.Point(165, 118);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(834, 387);
            this.panel9.TabIndex = 14;
            // 
            // panel1_PWmCurve
            // 
            this.panel1_PWmCurve.BackColor = System.Drawing.Color.White;
            this.panel1_PWmCurve.Controls.Add(this.chart_PWM);
            this.panel1_PWmCurve.Location = new System.Drawing.Point(20, 55);
            this.panel1_PWmCurve.Name = "panel1_PWmCurve";
            this.panel1_PWmCurve.Size = new System.Drawing.Size(794, 317);
            this.panel1_PWmCurve.TabIndex = 0;
            // 
            // chart_PWM
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_PWM.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_PWM.Legends.Add(legend1);
            this.chart_PWM.Location = new System.Drawing.Point(3, 5);
            this.chart_PWM.Name = "chart_PWM";
            this.chart_PWM.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_PWM.Series.Add(series1);
            this.chart_PWM.Size = new System.Drawing.Size(788, 307);
            this.chart_PWM.TabIndex = 0;
            this.chart_PWM.Text = "chart1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.ClientSize = new System.Drawing.Size(1002, 768);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_TX)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel_Module.ResumeLayout(false);
            this.panel_Module.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Setup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Stop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Run)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel1_PWmCurve.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_PWM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_ModuleName;
        private System.Windows.Forms.Label label_PowerSensor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_PWM;
        private System.Windows.Forms.Label label_EmergencyPowerSensor;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel_Module;
        private System.Windows.Forms.Label label_LightenessSensor2;
        private System.Windows.Forms.Label label_LightenessSensor1;
        private System.Windows.Forms.Label label_CarSensor;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox_RX;
        private System.Windows.Forms.Panel panel_Buttons;
        private System.Windows.Forms.PictureBox pictureBox_TX;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel1_PWmCurve;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_PWM;
        private System.Windows.Forms.PictureBox pictureBox_Run;
        private System.Windows.Forms.PictureBox pictureBox_Setup;
        private System.Windows.Forms.PictureBox pictureBox_Stop;
        private System.Windows.Forms.CheckBox checkBox_Message;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.CheckBox checkBox_Error;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox checkBox_Alert;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox listBox_Info;
        private System.Windows.Forms.Label label_Communication;
    }
}