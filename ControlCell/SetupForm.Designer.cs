namespace NCLT.TunnelLighting.ControlCell
{
    partial class SetupForm
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
            this.groupBox_Control = new System.Windows.Forms.GroupBox();
            this.button_Delete = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            this.treeView_ControlModule = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_ControlModule = new System.Windows.Forms.TabPage();
            this.panel_Setup = new System.Windows.Forms.Panel();
            this.tabPage_Hypotaxis = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_Setup_SlaveCarSensor = new System.Windows.Forms.Button();
            this.button_Setup_MainCarSensor = new System.Windows.Forms.Button();
            this.textBox_SlaveCarSensor = new System.Windows.Forms.TextBox();
            this.textBox_MainCarSensor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button_Save = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_CK_Setup_SLaveLighteness = new System.Windows.Forms.Button();
            this.button_CK_Setup_MainLighteness = new System.Windows.Forms.Button();
            this.textBox_CK_SLaveLighteness = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_CK_MainLighteness = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_RK_Setup_SLaveLighteness = new System.Windows.Forms.Button();
            this.button_RK_Setup_MainLighteness = new System.Windows.Forms.Button();
            this.textBox_RK_SLaveLighteness = new System.Windows.Forms.TextBox();
            this.textBox_RK_MainLighteness = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_RK_Clear_MainLighteness = new System.Windows.Forms.Button();
            this.button_RK_Clear_SLaveLighteness = new System.Windows.Forms.Button();
            this.button_CK_Clear_MainLighteness = new System.Windows.Forms.Button();
            this.button_CK_Clear_SLaveLighteness = new System.Windows.Forms.Button();
            this.button_Clear_MainCarSensor = new System.Windows.Forms.Button();
            this.button_Clear_SlaveCarSensor = new System.Windows.Forms.Button();
            this.groupBox_Control.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_ControlModule.SuspendLayout();
            this.tabPage_Hypotaxis.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_Control
            // 
            this.groupBox_Control.Controls.Add(this.button_Delete);
            this.groupBox_Control.Controls.Add(this.button_Add);
            this.groupBox_Control.Controls.Add(this.treeView_ControlModule);
            this.groupBox_Control.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox_Control.Location = new System.Drawing.Point(3, 3);
            this.groupBox_Control.Name = "groupBox_Control";
            this.groupBox_Control.Size = new System.Drawing.Size(200, 462);
            this.groupBox_Control.TabIndex = 0;
            this.groupBox_Control.TabStop = false;
            this.groupBox_Control.Text = "控制模块";
            // 
            // button_Delete
            // 
            this.button_Delete.Location = new System.Drawing.Point(106, 397);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(87, 31);
            this.button_Delete.TabIndex = 3;
            this.button_Delete.Text = "删　除";
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(10, 397);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(87, 31);
            this.button_Add.TabIndex = 2;
            this.button_Add.Text = "增　加";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // treeView_ControlModule
            // 
            this.treeView_ControlModule.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView_ControlModule.Location = new System.Drawing.Point(3, 17);
            this.treeView_ControlModule.Name = "treeView_ControlModule";
            this.treeView_ControlModule.Size = new System.Drawing.Size(194, 372);
            this.treeView_ControlModule.TabIndex = 1;
            this.treeView_ControlModule.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_ControlModule_AfterSelect);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_ControlModule);
            this.tabControl1.Controls.Add(this.tabPage_Hypotaxis);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(499, 493);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage_ControlModule
            // 
            this.tabPage_ControlModule.Controls.Add(this.panel_Setup);
            this.tabPage_ControlModule.Controls.Add(this.groupBox_Control);
            this.tabPage_ControlModule.Location = new System.Drawing.Point(4, 21);
            this.tabPage_ControlModule.Name = "tabPage_ControlModule";
            this.tabPage_ControlModule.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_ControlModule.Size = new System.Drawing.Size(491, 468);
            this.tabPage_ControlModule.TabIndex = 0;
            this.tabPage_ControlModule.Text = "控制模块";
            this.tabPage_ControlModule.UseVisualStyleBackColor = true;
            // 
            // panel_Setup
            // 
            this.panel_Setup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Setup.Location = new System.Drawing.Point(203, 3);
            this.panel_Setup.Name = "panel_Setup";
            this.panel_Setup.Size = new System.Drawing.Size(285, 462);
            this.panel_Setup.TabIndex = 1;
            // 
            // tabPage_Hypotaxis
            // 
            this.tabPage_Hypotaxis.Controls.Add(this.groupBox3);
            this.tabPage_Hypotaxis.Controls.Add(this.button_Save);
            this.tabPage_Hypotaxis.Controls.Add(this.groupBox2);
            this.tabPage_Hypotaxis.Controls.Add(this.groupBox1);
            this.tabPage_Hypotaxis.Location = new System.Drawing.Point(4, 21);
            this.tabPage_Hypotaxis.Name = "tabPage_Hypotaxis";
            this.tabPage_Hypotaxis.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Hypotaxis.Size = new System.Drawing.Size(491, 468);
            this.tabPage_Hypotaxis.TabIndex = 1;
            this.tabPage_Hypotaxis.Text = "公共变量";
            this.tabPage_Hypotaxis.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button_Clear_SlaveCarSensor);
            this.groupBox3.Controls.Add(this.button_Clear_MainCarSensor);
            this.groupBox3.Controls.Add(this.button_Setup_SlaveCarSensor);
            this.groupBox3.Controls.Add(this.button_Setup_MainCarSensor);
            this.groupBox3.Controls.Add(this.textBox_SlaveCarSensor);
            this.groupBox3.Controls.Add(this.textBox_MainCarSensor);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 267);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(485, 133);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "雷达";
            // 
            // button_Setup_SlaveCarSensor
            // 
            this.button_Setup_SlaveCarSensor.Location = new System.Drawing.Point(379, 78);
            this.button_Setup_SlaveCarSensor.Name = "button_Setup_SlaveCarSensor";
            this.button_Setup_SlaveCarSensor.Size = new System.Drawing.Size(49, 21);
            this.button_Setup_SlaveCarSensor.TabIndex = 5;
            this.button_Setup_SlaveCarSensor.Text = "设置";
            this.button_Setup_SlaveCarSensor.UseVisualStyleBackColor = true;
            this.button_Setup_SlaveCarSensor.Click += new System.EventHandler(this.button_SlaveCarSensor_Click);
            // 
            // button_Setup_MainCarSensor
            // 
            this.button_Setup_MainCarSensor.Location = new System.Drawing.Point(379, 36);
            this.button_Setup_MainCarSensor.Name = "button_Setup_MainCarSensor";
            this.button_Setup_MainCarSensor.Size = new System.Drawing.Size(49, 21);
            this.button_Setup_MainCarSensor.TabIndex = 4;
            this.button_Setup_MainCarSensor.Text = "设置";
            this.button_Setup_MainCarSensor.UseVisualStyleBackColor = true;
            this.button_Setup_MainCarSensor.Click += new System.EventHandler(this.button_MainCarSensor_Click);
            // 
            // textBox_SlaveCarSensor
            // 
            this.textBox_SlaveCarSensor.Location = new System.Drawing.Point(92, 78);
            this.textBox_SlaveCarSensor.Name = "textBox_SlaveCarSensor";
            this.textBox_SlaveCarSensor.ReadOnly = true;
            this.textBox_SlaveCarSensor.Size = new System.Drawing.Size(277, 21);
            this.textBox_SlaveCarSensor.TabIndex = 3;
            // 
            // textBox_MainCarSensor
            // 
            this.textBox_MainCarSensor.Location = new System.Drawing.Point(92, 37);
            this.textBox_MainCarSensor.Name = "textBox_MainCarSensor";
            this.textBox_MainCarSensor.ReadOnly = true;
            this.textBox_MainCarSensor.Size = new System.Drawing.Size(277, 21);
            this.textBox_MainCarSensor.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "备份传感器:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "主传感器:";
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(188, 413);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(114, 35);
            this.button_Save.TabIndex = 2;
            this.button_Save.Text = "保    存";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_CK_Clear_SLaveLighteness);
            this.groupBox2.Controls.Add(this.button_CK_Clear_MainLighteness);
            this.groupBox2.Controls.Add(this.button_CK_Setup_SLaveLighteness);
            this.groupBox2.Controls.Add(this.button_CK_Setup_MainLighteness);
            this.groupBox2.Controls.Add(this.textBox_CK_SLaveLighteness);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBox_CK_MainLighteness);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(485, 132);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "洞外照明(出口)";
            // 
            // button_CK_Setup_SLaveLighteness
            // 
            this.button_CK_Setup_SLaveLighteness.Location = new System.Drawing.Point(379, 79);
            this.button_CK_Setup_SLaveLighteness.Name = "button_CK_Setup_SLaveLighteness";
            this.button_CK_Setup_SLaveLighteness.Size = new System.Drawing.Size(49, 21);
            this.button_CK_Setup_SLaveLighteness.TabIndex = 7;
            this.button_CK_Setup_SLaveLighteness.Text = "设置";
            this.button_CK_Setup_SLaveLighteness.UseVisualStyleBackColor = true;
            this.button_CK_Setup_SLaveLighteness.Click += new System.EventHandler(this.button_CK_SlaveCarSensor_Click);
            // 
            // button_CK_Setup_MainLighteness
            // 
            this.button_CK_Setup_MainLighteness.Location = new System.Drawing.Point(379, 37);
            this.button_CK_Setup_MainLighteness.Name = "button_CK_Setup_MainLighteness";
            this.button_CK_Setup_MainLighteness.Size = new System.Drawing.Size(49, 21);
            this.button_CK_Setup_MainLighteness.TabIndex = 6;
            this.button_CK_Setup_MainLighteness.Text = "设置";
            this.button_CK_Setup_MainLighteness.UseVisualStyleBackColor = true;
            this.button_CK_Setup_MainLighteness.Click += new System.EventHandler(this.button_CK_MainCarSensor_Click);
            // 
            // textBox_CK_SLaveLighteness
            // 
            this.textBox_CK_SLaveLighteness.Location = new System.Drawing.Point(91, 79);
            this.textBox_CK_SLaveLighteness.Name = "textBox_CK_SLaveLighteness";
            this.textBox_CK_SLaveLighteness.ReadOnly = true;
            this.textBox_CK_SLaveLighteness.Size = new System.Drawing.Size(277, 21);
            this.textBox_CK_SLaveLighteness.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "备份传感器:";
            // 
            // textBox_CK_MainLighteness
            // 
            this.textBox_CK_MainLighteness.Location = new System.Drawing.Point(91, 37);
            this.textBox_CK_MainLighteness.Name = "textBox_CK_MainLighteness";
            this.textBox_CK_MainLighteness.ReadOnly = true;
            this.textBox_CK_MainLighteness.Size = new System.Drawing.Size(277, 21);
            this.textBox_CK_MainLighteness.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "主传感器:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_RK_Clear_SLaveLighteness);
            this.groupBox1.Controls.Add(this.button_RK_Clear_MainLighteness);
            this.groupBox1.Controls.Add(this.button_RK_Setup_SLaveLighteness);
            this.groupBox1.Controls.Add(this.button_RK_Setup_MainLighteness);
            this.groupBox1.Controls.Add(this.textBox_RK_SLaveLighteness);
            this.groupBox1.Controls.Add(this.textBox_RK_MainLighteness);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(485, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "洞外照明(入口)";
            // 
            // button_RK_Setup_SLaveLighteness
            // 
            this.button_RK_Setup_SLaveLighteness.Location = new System.Drawing.Point(379, 78);
            this.button_RK_Setup_SLaveLighteness.Name = "button_RK_Setup_SLaveLighteness";
            this.button_RK_Setup_SLaveLighteness.Size = new System.Drawing.Size(49, 21);
            this.button_RK_Setup_SLaveLighteness.TabIndex = 5;
            this.button_RK_Setup_SLaveLighteness.Text = "设置";
            this.button_RK_Setup_SLaveLighteness.UseVisualStyleBackColor = true;
            this.button_RK_Setup_SLaveLighteness.Click += new System.EventHandler(this.button_SLaveLighteness_Click);
            // 
            // button_RK_Setup_MainLighteness
            // 
            this.button_RK_Setup_MainLighteness.Location = new System.Drawing.Point(379, 37);
            this.button_RK_Setup_MainLighteness.Name = "button_RK_Setup_MainLighteness";
            this.button_RK_Setup_MainLighteness.Size = new System.Drawing.Size(49, 21);
            this.button_RK_Setup_MainLighteness.TabIndex = 4;
            this.button_RK_Setup_MainLighteness.Text = "设置";
            this.button_RK_Setup_MainLighteness.UseVisualStyleBackColor = true;
            this.button_RK_Setup_MainLighteness.Click += new System.EventHandler(this.button_MainLighteness_Click);
            // 
            // textBox_RK_SLaveLighteness
            // 
            this.textBox_RK_SLaveLighteness.Location = new System.Drawing.Point(92, 78);
            this.textBox_RK_SLaveLighteness.Name = "textBox_RK_SLaveLighteness";
            this.textBox_RK_SLaveLighteness.ReadOnly = true;
            this.textBox_RK_SLaveLighteness.Size = new System.Drawing.Size(277, 21);
            this.textBox_RK_SLaveLighteness.TabIndex = 3;
            // 
            // textBox_RK_MainLighteness
            // 
            this.textBox_RK_MainLighteness.Location = new System.Drawing.Point(92, 37);
            this.textBox_RK_MainLighteness.Name = "textBox_RK_MainLighteness";
            this.textBox_RK_MainLighteness.ReadOnly = true;
            this.textBox_RK_MainLighteness.Size = new System.Drawing.Size(277, 21);
            this.textBox_RK_MainLighteness.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "备份传感器:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "主传感器:";
            // 
            // button_RK_Clear_MainLighteness
            // 
            this.button_RK_Clear_MainLighteness.Location = new System.Drawing.Point(433, 37);
            this.button_RK_Clear_MainLighteness.Name = "button_RK_Clear_MainLighteness";
            this.button_RK_Clear_MainLighteness.Size = new System.Drawing.Size(49, 21);
            this.button_RK_Clear_MainLighteness.TabIndex = 6;
            this.button_RK_Clear_MainLighteness.Text = "清除";
            this.button_RK_Clear_MainLighteness.UseVisualStyleBackColor = true;
            this.button_RK_Clear_MainLighteness.Click += new System.EventHandler(this.button_RK_Clear_MainLighteness_Click);
            // 
            // button_RK_Clear_SLaveLighteness
            // 
            this.button_RK_Clear_SLaveLighteness.Location = new System.Drawing.Point(433, 77);
            this.button_RK_Clear_SLaveLighteness.Name = "button_RK_Clear_SLaveLighteness";
            this.button_RK_Clear_SLaveLighteness.Size = new System.Drawing.Size(49, 21);
            this.button_RK_Clear_SLaveLighteness.TabIndex = 7;
            this.button_RK_Clear_SLaveLighteness.Text = "清除";
            this.button_RK_Clear_SLaveLighteness.UseVisualStyleBackColor = true;
            this.button_RK_Clear_SLaveLighteness.Click += new System.EventHandler(this.button_RK_Clear_SLaveLighteness_Click);
            // 
            // button_CK_Clear_MainLighteness
            // 
            this.button_CK_Clear_MainLighteness.Location = new System.Drawing.Point(431, 37);
            this.button_CK_Clear_MainLighteness.Name = "button_CK_Clear_MainLighteness";
            this.button_CK_Clear_MainLighteness.Size = new System.Drawing.Size(49, 21);
            this.button_CK_Clear_MainLighteness.TabIndex = 8;
            this.button_CK_Clear_MainLighteness.Text = "清除";
            this.button_CK_Clear_MainLighteness.UseVisualStyleBackColor = true;
            this.button_CK_Clear_MainLighteness.Click += new System.EventHandler(this.button_CK_Clear_MainLighteness_Click);
            // 
            // button_CK_Clear_SLaveLighteness
            // 
            this.button_CK_Clear_SLaveLighteness.Location = new System.Drawing.Point(430, 79);
            this.button_CK_Clear_SLaveLighteness.Name = "button_CK_Clear_SLaveLighteness";
            this.button_CK_Clear_SLaveLighteness.Size = new System.Drawing.Size(49, 21);
            this.button_CK_Clear_SLaveLighteness.TabIndex = 9;
            this.button_CK_Clear_SLaveLighteness.Text = "清除";
            this.button_CK_Clear_SLaveLighteness.UseVisualStyleBackColor = true;
            this.button_CK_Clear_SLaveLighteness.Click += new System.EventHandler(this.button_CK_Clear_SLaveLighteness_Click);
            // 
            // button_Clear_MainCarSensor
            // 
            this.button_Clear_MainCarSensor.Location = new System.Drawing.Point(430, 36);
            this.button_Clear_MainCarSensor.Name = "button_Clear_MainCarSensor";
            this.button_Clear_MainCarSensor.Size = new System.Drawing.Size(49, 21);
            this.button_Clear_MainCarSensor.TabIndex = 9;
            this.button_Clear_MainCarSensor.Text = "清除";
            this.button_Clear_MainCarSensor.UseVisualStyleBackColor = true;
            this.button_Clear_MainCarSensor.Click += new System.EventHandler(this.button_Clear_MainCarSensor_Click);
            // 
            // button_Clear_SlaveCarSensor
            // 
            this.button_Clear_SlaveCarSensor.Location = new System.Drawing.Point(430, 78);
            this.button_Clear_SlaveCarSensor.Name = "button_Clear_SlaveCarSensor";
            this.button_Clear_SlaveCarSensor.Size = new System.Drawing.Size(49, 21);
            this.button_Clear_SlaveCarSensor.TabIndex = 10;
            this.button_Clear_SlaveCarSensor.Text = "清除";
            this.button_Clear_SlaveCarSensor.UseVisualStyleBackColor = true;
            this.button_Clear_SlaveCarSensor.Click += new System.EventHandler(this.button_Clear_SlaveCarSensor_Click);
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 493);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.SetupForm_Load);
            this.groupBox_Control.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_ControlModule.ResumeLayout(false);
            this.tabPage_Hypotaxis.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_Control;
        private System.Windows.Forms.TreeView treeView_ControlModule;
        private System.Windows.Forms.Button button_Delete;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_ControlModule;
        private System.Windows.Forms.TabPage tabPage_Hypotaxis;
        private System.Windows.Forms.Panel panel_Setup;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_RK_SLaveLighteness;
        private System.Windows.Forms.TextBox textBox_RK_MainLighteness;
        private System.Windows.Forms.Button button_RK_Setup_MainLighteness;
        private System.Windows.Forms.Button button_RK_Setup_SLaveLighteness;
        private System.Windows.Forms.TextBox textBox_CK_SLaveLighteness;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_CK_MainLighteness;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_CK_Setup_MainLighteness;
        private System.Windows.Forms.Button button_CK_Setup_SLaveLighteness;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_Setup_SlaveCarSensor;
        private System.Windows.Forms.Button button_Setup_MainCarSensor;
        private System.Windows.Forms.TextBox textBox_SlaveCarSensor;
        private System.Windows.Forms.TextBox textBox_MainCarSensor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_RK_Clear_MainLighteness;
        private System.Windows.Forms.Button button_RK_Clear_SLaveLighteness;
        private System.Windows.Forms.Button button_CK_Clear_MainLighteness;
        private System.Windows.Forms.Button button_CK_Clear_SLaveLighteness;
        private System.Windows.Forms.Button button_Clear_SlaveCarSensor;
        private System.Windows.Forms.Button button_Clear_MainCarSensor;
    }
}