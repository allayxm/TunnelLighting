using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NCLT.TunnelLighting.DB;
using NCLT.TunnelLighting.Sensor;

namespace NCLT.TunnelLighting.ControlCell
{
    public partial class SetupForm : Form
    {
        public struct ConfigStruct
        {
            public int ControldID;
            public int PortNumber;
        }
        
        public SetupForm()
        {
            InitializeComponent();
        }

        private void SetupForm_Load(object sender, EventArgs e)
        {
            init();
        }

        ControlModuleManage m_ControlModuleManage = null;
        public ControlModuleManage ControlModuleManag
        {
            set
            {
                m_ControlModuleManage = value;
            }
        }

        void init()
        {
            TreeNode vTunnelNode = new TreeNode();
            using (SystemConfig vSystemConfig = new SystemConfig())
            {
                vTunnelNode.Text = vSystemConfig.TunnelName;
            }

            foreach (ControlModule vTempControlModule in m_ControlModuleManage.ControlModules)
            {
                TreeNode vNewControlModuleNode = new TreeNode();
                vNewControlModuleNode.Text = vTempControlModule.Name;
                vNewControlModuleNode.Name = string.Format("ControlBox_{0}", vTempControlModule.ID);
                vNewControlModuleNode.Tag = vTempControlModule.ID;
                vTunnelNode.Nodes.Add(vNewControlModuleNode);
            }
            treeView_ControlModule.Nodes.Add(vTunnelNode);

            using (SystemConfig m_SystemConfig = new SystemConfig())
            {
                //主洞外照明(入口)
                string vMainLightenessName_RK = "";
                int vMainLightenessID_RK = 0;
                int vMainLightenessPortNumber_RK = 0;
                byte vMainLightenessAddress_RK = 0;
                m_SystemConfig.GetMainLighteness_RK(ref vMainLightenessName_RK,ref vMainLightenessAddress_RK, ref vMainLightenessID_RK, ref vMainLightenessPortNumber_RK );
                if (vMainLightenessID_RK != 0)
                {
                    ConfigStruct vMainLightenessConfigStruct = new ConfigStruct();
                    vMainLightenessConfigStruct.ControldID = vMainLightenessID_RK;
                    vMainLightenessConfigStruct.PortNumber = vMainLightenessPortNumber_RK;
                    textBox_RK_MainLighteness.Text = string.Format("{0}模块 地址:{1} 端口{2}", vMainLightenessName_RK, vMainLightenessAddress_RK, vMainLightenessPortNumber_RK);
                    textBox_RK_MainLighteness.Tag = vMainLightenessConfigStruct;
                }

                //备用洞外照明(入口)
                string vSlaveLightenessName_RK = "";
                int vSlaveLightenessID_RK = 0;
                int vSlaveLightenessPortNumber_RK = 0;
                byte vSlaveLightenessAddress_RK = 0;
                m_SystemConfig.GetSlaveLighteness_RK(ref vSlaveLightenessName_RK, ref vSlaveLightenessAddress_RK,ref vSlaveLightenessID_RK, ref vSlaveLightenessPortNumber_RK);
                if (vSlaveLightenessID_RK != 0)
                {
                    ConfigStruct vSlaveLightenessConfigStruct = new ConfigStruct();
                    vSlaveLightenessConfigStruct.ControldID = vSlaveLightenessID_RK;
                    vSlaveLightenessConfigStruct.PortNumber = vSlaveLightenessPortNumber_RK;
                    textBox_RK_SLaveLighteness.Text = string.Format("{0}模块 地址:{1} 端口{2}", vSlaveLightenessName_RK, vSlaveLightenessAddress_RK, vSlaveLightenessPortNumber_RK);
                    textBox_RK_SLaveLighteness.Tag = vSlaveLightenessConfigStruct;
                }

                //主洞外照明(出口)
                string vMainLightenessName_CK = "";
                int vMainLightenessID_CK      = 0;
                int vMainLightenessPortNumber_CK = 0;
                byte vMainLightenessAddress_CK   = 0;
                m_SystemConfig.GetMainLighteness_CK(ref vMainLightenessName_CK, ref vMainLightenessAddress_CK, ref vMainLightenessID_CK, ref vMainLightenessPortNumber_CK);
                if (vMainLightenessID_CK != 0)
                {
                    ConfigStruct vMainLightenessConfigStruct = new ConfigStruct();
                    vMainLightenessConfigStruct.ControldID = vMainLightenessID_CK;
                    vMainLightenessConfigStruct.PortNumber = vMainLightenessPortNumber_CK;
                    textBox_CK_MainLighteness.Text = string.Format("{0}模块 地址:{1} 端口{2}", vMainLightenessName_CK, vMainLightenessAddress_CK, vMainLightenessPortNumber_CK);
                    textBox_CK_MainLighteness.Tag = vMainLightenessConfigStruct;
                }

                //备用洞外照明(出口)
                string vSlaveLightenessName_CK = "";
                int vSlaveLightenessID_CK = 0;
                int vSlaveLightenessPortNumber_CK = 0;
                byte vSlaveLightenessAddress_CK = 0;
                m_SystemConfig.GetSlaveLighteness_CK(ref vSlaveLightenessName_CK, ref vSlaveLightenessAddress_CK, ref vSlaveLightenessID_CK, ref vSlaveLightenessPortNumber_CK);
                if (vSlaveLightenessID_CK != 0)
                {
                    ConfigStruct vSlaveLightenessConfigStruct = new ConfigStruct();
                    vSlaveLightenessConfigStruct.ControldID = vSlaveLightenessID_CK;
                    vSlaveLightenessConfigStruct.PortNumber = vSlaveLightenessPortNumber_CK;
                    textBox_CK_SLaveLighteness.Text = string.Format("{0}模块 地址:{1} 端口{2}", vSlaveLightenessName_CK, vSlaveLightenessAddress_CK, vSlaveLightenessPortNumber_CK);
                    textBox_CK_SLaveLighteness.Tag = vSlaveLightenessConfigStruct;
                }

                //主车辆传感器
                string vMainCarSensorName = "";
                int vMainCarSensorID = 0;
                byte vMainCarSensorAddress = 0;
                m_SystemConfig.GetMainCarSensor(ref vMainCarSensorName, ref vMainCarSensorAddress, ref vMainCarSensorID );
                if (vMainCarSensorID != 0)
                {
                    ConfigStruct vSlaveLightenessConfigStruct = new ConfigStruct();
                    vSlaveLightenessConfigStruct.ControldID = vMainCarSensorID;
                    textBox_MainCarSensor.Text = string.Format("{0}模块 地址:{1}", vMainCarSensorName, vMainCarSensorAddress);
                    textBox_MainCarSensor.Tag = vSlaveLightenessConfigStruct;
                }

                //从车辆传感器
                string vSlaveCarSensorName = "";
                int vSlaveCarSensorID = 0;
                byte vSlaveCarSensorAddress = 0;
                m_SystemConfig.GetMainCarSensor(ref vSlaveCarSensorName, ref vSlaveCarSensorAddress, ref vSlaveCarSensorID );
                if (vMainCarSensorID != 0)
                {
                    ConfigStruct vSlaveLightenessConfigStruct = new ConfigStruct();
                    vSlaveLightenessConfigStruct.ControldID = vSlaveCarSensorID;
                    textBox_SlaveCarSensor.Text = string.Format("{0}模块 地址:{1}", vSlaveCarSensorName, vSlaveCarSensorAddress);
                    textBox_SlaveCarSensor.Tag = vSlaveLightenessConfigStruct;
                }
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            TreeNode vNewNode = null;
            switch (treeView_ControlModule.SelectedNode.Level)
            {
                case 0:
                    vNewNode = new TreeNode();
                    ControlModule vNewControlModule = m_ControlModuleManage.NewControlModule();
                    vNewNode.Text = vNewControlModule.Name;
                    vNewNode.Name = string.Format("ControlBox_{0}", vNewControlModule.ID);
                    vNewNode.Tag = vNewControlModule.ID;
                    treeView_ControlModule.SelectedNode.Nodes.Add(vNewNode);
                    m_ControlModuleManage.ControlModules.Add(vNewControlModule);
                    break;
            }
            if (vNewNode != null)
            {
                treeView_ControlModule.SelectedNode = vNewNode;
                treeView_ControlModule.Focus();
            }
        }

        private void treeView_ControlModule_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int vControlModuleID;
            switch (treeView_ControlModule.SelectedNode.Level)
            {
                case 0:
                    button_Add.Enabled = true;
                    button_Delete.Enabled = false;
                    button_Add.Text = "增加控制模块";
                    panel_Setup.Controls.Clear();
                    BasicSetupControl vBasicSetupControl = new BasicSetupControl();
                    vBasicSetupControl.Dock = DockStyle.Fill;
                    vBasicSetupControl.TunnelNameNode = treeView_ControlModule.SelectedNode;
                    panel_Setup.Controls.Add(vBasicSetupControl);
                    break;
                case 1:
                    button_Add.Enabled = false;
                    button_Delete.Enabled = true;
                    panel_Setup.Controls.Clear();
                    vControlModuleID = (int)treeView_ControlModule.SelectedNode.Tag;
                    ControlModuleSetupControl vControlModuleSetupControl = new ControlModuleSetupControl();
                    vControlModuleSetupControl.ControlModuleNode = treeView_ControlModule.SelectedNode;
                    vControlModuleSetupControl.ControlModuleInfo = m_ControlModuleManage.FindControlModule(vControlModuleID);
                    vControlModuleSetupControl.ControlModuleManage = m_ControlModuleManage;
                    vControlModuleSetupControl.Dock = DockStyle.Fill;
                    panel_Setup.Controls.Add(vControlModuleSetupControl);
                    break;
               
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            int vControlModuleID;
            switch( treeView_ControlModule.SelectedNode.Level )
            {
                case 0:
                    button_Delete.Enabled = false;
                    break;
                case 1:
                    vControlModuleID = (int)treeView_ControlModule.SelectedNode.Tag;
                    if (m_ControlModuleManage.RemoveControlModule(vControlModuleID))
                    {
                        treeView_ControlModule.SelectedNode.Remove();
                    }
                    break;
            }
        }

        private void button_MainLighteness_Click(object sender, EventArgs e)
        {
            DataTable vLightenessDataTable = m_ControlModuleManage.GetNoUsingLighteness();
            SelectLightenessSensorForm vSelectLightenessSensorForm = new SelectLightenessSensorForm();
            vSelectLightenessSensorForm.PortListTable = vLightenessDataTable;
            if (vSelectLightenessSensorForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int vControlModuleID = vSelectLightenessSensorForm.SelectedContorlModuleID;
                short vPortNumber = vSelectLightenessSensorForm.SelectedControlPortNumber;
                textBox_RK_MainLighteness.Text = vSelectLightenessSensorForm.Memo;
                ConfigStruct vConfigStruct = new ConfigStruct();
                vConfigStruct.ControldID = vControlModuleID;
                vConfigStruct.PortNumber = vPortNumber;
                textBox_RK_MainLighteness.Tag = vConfigStruct;
            }
            vSelectLightenessSensorForm.Dispose();
            vSelectLightenessSensorForm = null;
        }

        private void button_SLaveLighteness_Click(object sender, EventArgs e)
        {
            DataTable vLightenessDataTable = m_ControlModuleManage.GetNoUsingLighteness();
            SelectLightenessSensorForm vSelectLightenessSensorForm = new SelectLightenessSensorForm();
            vSelectLightenessSensorForm.PortListTable = vLightenessDataTable;
            if (vSelectLightenessSensorForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int vControlModuleID = vSelectLightenessSensorForm.SelectedContorlModuleID;
                short vPortNumber = vSelectLightenessSensorForm.SelectedControlPortNumber;
                textBox_RK_SLaveLighteness.Text = vSelectLightenessSensorForm.Memo;
                ConfigStruct vConfigStruct = new ConfigStruct();
                vConfigStruct.ControldID = vControlModuleID;
                vConfigStruct.PortNumber = vPortNumber;
                textBox_RK_SLaveLighteness.Tag = vConfigStruct;
            }
            vSelectLightenessSensorForm.Dispose();
            vSelectLightenessSensorForm = null;
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            using (SystemConfig vSystemConfig = new SystemConfig())
            {
                ConfigStruct vConfigStruct ;
                if (textBox_RK_MainLighteness.Tag != null)
                {
                    vConfigStruct = (ConfigStruct)textBox_RK_MainLighteness.Tag;
                    vSystemConfig.SetMainLighteness_RK(vConfigStruct.ControldID, vConfigStruct.PortNumber);
                }
                else
                {
                    vSystemConfig.SetMainLighteness_RK(0, 0);
                }

                if (textBox_RK_SLaveLighteness.Tag != null)
                {
                    vConfigStruct = (ConfigStruct)textBox_RK_SLaveLighteness.Tag;
                    vSystemConfig.SetSlaveLighteness_RK(vConfigStruct.ControldID, vConfigStruct.PortNumber);
                }
                else
                {
                    vSystemConfig.SetSlaveLighteness_RK(0, 0);
                }

                if (textBox_CK_MainLighteness.Tag != null)
                {
                    vConfigStruct = (ConfigStruct)textBox_CK_MainLighteness.Tag;
                    vSystemConfig.SetMainLighteness_CK(vConfigStruct.ControldID, vConfigStruct.PortNumber);
                }
                else
                {
                    vSystemConfig.SetMainLighteness_CK(0, 0);
                }

                if (textBox_CK_SLaveLighteness.Tag != null)
                {
                    vConfigStruct = (ConfigStruct)textBox_CK_SLaveLighteness.Tag;
                    vSystemConfig.SetSlaveLighteness_CK(vConfigStruct.ControldID, vConfigStruct.PortNumber);
                }
                else
                {
                    vSystemConfig.SetSlaveLighteness_CK(0, 0);
                }

                if (textBox_CK_MainLighteness.Tag != null)
                {
                    vConfigStruct = (ConfigStruct)textBox_CK_MainLighteness.Tag;
                    vSystemConfig.SetMainCarSensor(vConfigStruct.ControldID);
                }
                else
                {
                    vSystemConfig.SetMainCarSensor(0);
                }

                if (textBox_CK_SLaveLighteness.Tag != null)
                {
                    vConfigStruct = (ConfigStruct)textBox_CK_SLaveLighteness.Tag;
                    vSystemConfig.SetSlaveCarSensor(vConfigStruct.ControldID);
                }
                else
                {
                    vSystemConfig.SetSlaveCarSensor(0);
                }
            }
        }

        private void button_MainCarSensor_Click(object sender, EventArgs e)
        {
            DataTable vLightenessDataTable = m_ControlModuleManage.GetNoUsingCarSensor();
            SelectCarSensorForm vSelectCarSensorForm = new SelectCarSensorForm();
            vSelectCarSensorForm.SensorListTable = vLightenessDataTable;
            if (vSelectCarSensorForm.ShowDialog() == DialogResult.OK)
            {
                int vControlModuleID = vSelectCarSensorForm.SelectedContorlModuleID;
                ConfigStruct vConfigStruct = new ConfigStruct();
                vConfigStruct.ControldID   = vControlModuleID;
                textBox_MainCarSensor.Tag  = vConfigStruct;
                textBox_MainCarSensor.Text = vSelectCarSensorForm.Memo;
            }
        }

        private void button_SlaveCarSensor_Click(object sender, EventArgs e)
        {
            DataTable vLightenessDataTable = m_ControlModuleManage.GetNoUsingCarSensor();
            SelectCarSensorForm vSelectCarSensorForm = new SelectCarSensorForm();
            vSelectCarSensorForm.SensorListTable = vLightenessDataTable;
            if (vSelectCarSensorForm.ShowDialog() == DialogResult.OK)
            {
                int vControlModuleID = vSelectCarSensorForm.SelectedContorlModuleID;
                ConfigStruct vConfigStruct  = new ConfigStruct();
                vConfigStruct.ControldID    = vControlModuleID;
                textBox_SlaveCarSensor.Tag  = vConfigStruct;
                textBox_SlaveCarSensor.Text = vSelectCarSensorForm.Memo;
            }
        }

        private void button_CK_MainCarSensor_Click(object sender, EventArgs e)
        {
            DataTable vLightenessDataTable = m_ControlModuleManage.GetNoUsingLighteness();
            SelectLightenessSensorForm vSelectLightenessSensorForm = new SelectLightenessSensorForm();
            vSelectLightenessSensorForm.PortListTable = vLightenessDataTable;
            if (vSelectLightenessSensorForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int vControlModuleID = vSelectLightenessSensorForm.SelectedContorlModuleID;
                short vPortNumber = vSelectLightenessSensorForm.SelectedControlPortNumber;
                textBox_CK_MainLighteness.Text = vSelectLightenessSensorForm.Memo;
                ConfigStruct vConfigStruct = new ConfigStruct();
                vConfigStruct.ControldID = vControlModuleID;
                vConfigStruct.PortNumber = vPortNumber;
                
                textBox_CK_MainLighteness.Tag = vConfigStruct;
            }
            vSelectLightenessSensorForm.Dispose();
            vSelectLightenessSensorForm = null;
        }

        private void button_CK_SlaveCarSensor_Click(object sender, EventArgs e)
        {
            DataTable vLightenessDataTable = m_ControlModuleManage.GetNoUsingLighteness();
            SelectLightenessSensorForm vSelectLightenessSensorForm = new SelectLightenessSensorForm();
            vSelectLightenessSensorForm.PortListTable = vLightenessDataTable;
            if (vSelectLightenessSensorForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int vControlModuleID = vSelectLightenessSensorForm.SelectedContorlModuleID;
                short vPortNumber = vSelectLightenessSensorForm.SelectedControlPortNumber;
                textBox_CK_SLaveLighteness.Text = vSelectLightenessSensorForm.Memo;
                ConfigStruct vConfigStruct = new ConfigStruct();
                vConfigStruct.ControldID = vControlModuleID;
                vConfigStruct.PortNumber = vPortNumber;

                textBox_CK_SLaveLighteness.Tag = vConfigStruct;
            }
            vSelectLightenessSensorForm.Dispose();
            vSelectLightenessSensorForm = null;
        }

        private void button_Clear_MainCarSensor_Click(object sender, EventArgs e)
        {
            textBox_MainCarSensor.Text = "";
            textBox_MainCarSensor.Tag = null;
        }

        private void button_CK_Clear_SLaveLighteness_Click(object sender, EventArgs e)
        {
            button_CK_Setup_SLaveLighteness.Text = "";
            button_CK_Setup_SLaveLighteness.Tag = null;
        }

        private void button_RK_Clear_MainLighteness_Click(object sender, EventArgs e)
        {
            textBox_RK_MainLighteness.Text = "";
            textBox_RK_MainLighteness.Tag = null;
        }

        private void button_RK_Clear_SLaveLighteness_Click(object sender, EventArgs e)
        {
            textBox_RK_SLaveLighteness.Text = "";
            textBox_RK_SLaveLighteness.Tag = null;
        }

        private void button_CK_Clear_MainLighteness_Click(object sender, EventArgs e)
        {
            textBox_CK_MainLighteness.Text = "";
            textBox_CK_MainLighteness.Tag = null;
        }

        private void button_Clear_SlaveCarSensor_Click(object sender, EventArgs e)
        {
            textBox_SlaveCarSensor.Text = "";
            textBox_SlaveCarSensor.Tag = null;
        }

      
    }
}
