using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NCLT.TunnelLighting.DB;

namespace NCLT.TunnelLighting.ControlCell
{
    public partial class ControlModuleSetupControl : UserControl
    {
        public ControlModuleSetupControl()
        {
            InitializeComponent();
        }

        TreeNode m_ControlModuleNode = null;
        public TreeNode ControlModuleNode
        {
            set
            {
                m_ControlModuleNode = value;
            }
        }

        ControlModule m_ControlModule = null;
        public ControlModule ControlModuleInfo
        {
            set
            {
                m_ControlModule = value;
            }
        }

        ControlModuleManage m_ControlModuleManage = null;
        public ControlModuleManage ControlModuleManage
        {
            set
            {
                m_ControlModuleManage = value;
            }
        }

        void init()
        {
            textBox_Name.Text = m_ControlModule.Name;
            numericUpDown_Address.Value = m_ControlModule.Address;
            checkBox_HighLevel.Checked = m_ControlModule.HighLevel;

            if (m_ControlModule.Port1 != null)
                comboBox_Port1.SelectedIndex = (short)m_ControlModule.Port1.SensorType;
            else
                comboBox_Port1.SelectedIndex = 0;
            if (m_ControlModule.Port2 != null)
                comboBox_Port2.SelectedIndex = (short)m_ControlModule.Port2.SensorType;
            else
                comboBox_Port2.SelectedIndex = 0;
            
            if (m_ControlModule.Car != null)
                checkBox_CarSensor.Checked = true;
            else
                checkBox_CarSensor.Checked = false;

            if (m_ControlModule.EmergencyPower != null)
                checkBox_EmergencyPower.Checked = true;
            else
                checkBox_EmergencyPower.Checked = false;

            if (m_ControlModule.Power != null)
                checkBox_Power.Checked = true;
            else
                checkBox_Power.Checked = false;

            if (m_ControlModule.PWMMode == PWMModeEnum.NoPWM)
                radioButton_PWM_None.Checked = true;
            if (m_ControlModule.PWMMode == PWMModeEnum.FixupPWM)
            {
                radioButton_PWM_Fixup.Checked = true;
                numericUpDown_PWM.Value = m_ControlModule.PWMFixupValue;
            }
            if (m_ControlModule.PWMMode == PWMModeEnum.DynamicPWM)
            {
                radioButton_PWM_Dynamic.Checked = true;
                comboBox_QD.SelectedIndex = m_ControlModule.Area-1;
            }
            textBox_BDZ.Text = m_ControlModule.PWMDemarcate.ToString(); ;
        }

        private void ControlModuleSetupControl_Load(object sender, EventArgs e)
        {
            init();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            if (!m_ControlModuleManage.AddressIsExist( m_ControlModule.ID, (byte)numericUpDown_Address.Value))
            {
                string vName = textBox_Name.Text;
                byte vAddress = (byte)numericUpDown_Address.Value;
                short vPort1 = Convert.ToInt16(comboBox_Port1.SelectedIndex);
                short vPort2 = Convert.ToInt16(comboBox_Port2.SelectedIndex);
                bool vDG = checkBox_CarSensor.Checked;
                bool vBYDY = checkBox_EmergencyPower.Checked;
                bool vDY = checkBox_Power.Checked;
                bool vGYXJ = checkBox_HighLevel.Checked;
                PWMModeEnum vPWMMode = PWMModeEnum.NoPWM;
                if (radioButton_PWM_None.Checked)
                    vPWMMode = PWMModeEnum.NoPWM;
                if (radioButton_PWM_Fixup.Checked)
                    vPWMMode = PWMModeEnum.FixupPWM;
                if (radioButton_PWM_Dynamic.Checked)
                {
                    vPWMMode = PWMModeEnum.DynamicPWM;
                    if (comboBox_QD.SelectedIndex == -1)
                    {
                        MessageBox.Show("请选择动态输出区段", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                int vPWMBDZ = int.Parse(textBox_BDZ.Text);
                Int16 vQD = 0;
                if (vPWMMode == PWMModeEnum.DynamicPWM)
                    vQD = (short)( comboBox_QD.SelectedIndex + 1 );
                m_ControlModule.SaveToDB(vAddress, vName, vPort1, vPort2, vDG, vBYDY, vDY, vGYXJ,
                    (byte)vPWMMode, (short)numericUpDown_PWM.Value, vPWMBDZ, vQD);
                m_ControlModuleNode.Text = vName;
            }
            else
                MessageBox.Show("设备地址已经存在,请重新输入设置地址", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void radioButton_PWM_Fixup_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown_PWM.ReadOnly = !radioButton_PWM_Fixup.Checked;
        }

        private void radioButton_PWM_Dynamic_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_QD.Enabled = radioButton_PWM_Dynamic.Checked;
        }

        private void textBox_BDZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
