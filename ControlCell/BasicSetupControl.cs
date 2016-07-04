using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NCLT.TunnelLighting.ControlCell
{
    public partial class BasicSetupControl : UserControl
    {
        public BasicSetupControl()
        {
            InitializeComponent();
        }

        TreeNode m_TunnelNameNode = null;
        public TreeNode TunnelNameNode
        {
            get
            {
                return m_TunnelNameNode;
            }

            set
            {
                m_TunnelNameNode = value;
            }
        }

        void init()
        {
            if (m_TunnelNameNode != null)
            {
                textBox_TunnelName.Text = m_TunnelNameNode.Text;
                label_SDMC.Text = m_TunnelNameNode.Text;
            }
            using (SystemConfig vSystemConfig = new SystemConfig())
            {
                checkBox_Debug.Checked = vSystemConfig.DebugMode;
                comboBox_PortName.Text = vSystemConfig.PortName;
                numericUpDown_TimeOut.Value = vSystemConfig.CommunicationTimeOut;
                numericUpDown_BaudRate.Value = vSystemConfig.BaudRate;
                numericUpDown_CloseLight.Value = vSystemConfig.LigthTimeDelay;
                numericUpDown_LightFixTime.Value = vSystemConfig.LightFixTime;
                numericUpDown_AllowError.Value = vSystemConfig.LightAllowError;
            }
        }
        private void button_Save_Click(object sender, EventArgs e)
        {
            using (SystemConfig vSystemConfig = new SystemConfig())
            {
                label_SDMC.Text = textBox_TunnelName.Text;
                vSystemConfig.TunnelName = textBox_TunnelName.Text;
                m_TunnelNameNode.Text = textBox_TunnelName.Text;
                vSystemConfig.CommunicationTimeOut = (int)numericUpDown_TimeOut.Value;
                vSystemConfig.PortName = comboBox_PortName.Text;
                vSystemConfig.BaudRate = (int)numericUpDown_BaudRate.Value;
                vSystemConfig.LigthTimeDelay =  Convert.ToInt32( numericUpDown_CloseLight.Value );
                vSystemConfig.DebugMode = checkBox_Debug.Checked;
                vSystemConfig.LightFixTime = Convert.ToInt32( numericUpDown_LightFixTime.Value );
                vSystemConfig.LightAllowError = Convert.ToInt32( numericUpDown_AllowError.Value );
            }
        }

        private void BasicSetupControl_Load(object sender, EventArgs e)
        {
            init();
        }
    }
}
