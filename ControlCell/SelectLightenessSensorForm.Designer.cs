namespace NCLT.TunnelLighting.ControlCell
{
    partial class SelectLightenessSensorForm
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
            this.listBox_Lighteness = new System.Windows.Forms.ListBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox_Lighteness
            // 
            this.listBox_Lighteness.FormattingEnabled = true;
            this.listBox_Lighteness.ItemHeight = 12;
            this.listBox_Lighteness.Location = new System.Drawing.Point(0, 1);
            this.listBox_Lighteness.Name = "listBox_Lighteness";
            this.listBox_Lighteness.Size = new System.Drawing.Size(375, 256);
            this.listBox_Lighteness.TabIndex = 0;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(52, 266);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(93, 29);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "确  定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Exit.Location = new System.Drawing.Point(228, 266);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(93, 29);
            this.button_Exit.TabIndex = 2;
            this.button_Exit.Text = "取  消";
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // SelectLightenessSensorForm
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Exit;
            this.ClientSize = new System.Drawing.Size(375, 301);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.listBox_Lighteness);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectLightenessSensorForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择照度传感器";
            this.Load += new System.EventHandler(this.SelectLightenessSensorForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_Lighteness;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Exit;
    }
}