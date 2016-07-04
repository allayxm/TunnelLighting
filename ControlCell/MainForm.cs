using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using NCLT.TunnelLighting.DB;
using NCLT.TunnelLighting.Sensor;
using NCLT.TunnelLighting.LightControl;

namespace NCLT.TunnelLighting.ControlCell
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region 私有变量
        ControlModuleManage m_ControlModuleManage = null;
        //Modbus状态线程
        Thread m_ModbusStateThread = null; 
        //模块监试线程
        Thread m_ModuleWatchThread = null; 
        bool m_ModuleWaterThreadStopFlag = true;
        //PWM曲线线程
        Thread m_PWMCurveThread = null;
        //bool m_PWMCurveThreadStopFlag = true;
        
        DataTable m_MessageTableInfo = null;
        Thread m_MessageThread = null;
        #endregion

        void init()
        {
            if (m_ControlModuleManage != null)
                m_ControlModuleManage.Dispose();
            m_ControlModuleManage = new ControlModuleManage(Application.StartupPath);
            
            //初始化标题
            using (SystemConfig vSystemConfig = new SystemConfig())
            {
                label_Title.Text = vSystemConfig.TunnelName;
            }

            //初始化模块按钮
            initButtons();
            if (m_ControlModuleManage.ControlModules.Count > 0)
            {
                panel_Module.Tag = m_ControlModuleManage.ControlModules[0].ID;
            }

            //初始化输出信息
            initMessage();
            
       }

        void initPWMChar()
        {
            List<int> vPWMValueList = new List<int>();
            List<string> vPWMNameList = new List<string>();
            foreach (ControlModule vTempControlModule in m_ControlModuleManage.ControlModules)
            {
                if (vTempControlModule.PWMMode != PWMModeEnum.NoPWM)
                {
                    vPWMValueList.Add(vTempControlModule.GetPWM());
                    vPWMNameList.Add(vTempControlModule.Name);
                }
            }

            if (vPWMValueList.Count != 0 && chart_PWM.Series.Count > 0)
            {
                chart_PWM.Series[0].Points.DataBindXY(vPWMNameList.ToArray(), vPWMValueList.ToArray());
                chart_PWM.Series[0].Name = "PWM输出值";
                System.Diagnostics.Debug.WriteLine("PWM数量:{0}", vPWMNameList.Count);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("曲线图被清空");
            }
        }

        void m_ControlModuleManage_OnReceiveDataEventArgsEvent(object sender, ControlModuleManage.ReceiveDataEventArgs e)
        {
            if (e.ReceiveData.PWM != null)
            {
                List<int> vPWMValueList = new List<int>();
                List<string> vPWMNameList = new List<string>();
                foreach (ControlModule vTempControlModule in m_ControlModuleManage.ControlModules)
                {
                    if (vTempControlModule.PWMMode != PWMModeEnum.NoPWM)
                    {
                        vPWMValueList.Add(vTempControlModule.GetPWM());
                        vPWMNameList.Add(vTempControlModule.Name);
                    }
                }

                if (vPWMValueList.Count != 0 && chart_PWM.Series.Count > 0 )
                {
                    chart_PWM.Series[0].Points.DataBindXY(vPWMNameList.ToArray(), vPWMValueList.ToArray());
                    chart_PWM.Series[0].Name = "PWM输出值";
                    System.Diagnostics.Debug.WriteLine("PWM数量:{0}", vPWMNameList.Count);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("曲线图被清空");
                }
            }
        }

        #region 输出信息
        void initMessage()
        {
            //初始化信息表
            m_MessageTableInfo = createTableInfoStruct();
            m_MessageTableInfo.DefaultView.Sort = "ID desc";
            listBox_Info.DataSource = m_MessageTableInfo.DefaultView;
            listBox_Info.ValueMember = "ID";
            listBox_Info.DisplayMember = "信息";
        }


        DataTable createTableInfoStruct()
        {
            DataTable vDataTable = new DataTable();
            vDataTable.Columns.Add("ID", typeof(int));
            vDataTable.Columns["ID"].AutoIncrement = true;
            vDataTable.Columns.Add("等级", typeof(Int16));

            vDataTable.Columns.Add("信息", typeof(string));
            vDataTable.AcceptChanges();
            return vDataTable;
        }

        void messageThread()
        {
            m_ControlModuleManage.OnOutputMessageEvent += new ControlModuleManage.OutputMessageEventHandler(m_ControlModuleManage_OnOutputMessageEvent);
        }

        void m_ControlModuleManage_OnOutputMessageEvent(object sender, ControlModuleManage.OutputMessageEventArgs e)
        {
            DataTable vDataTable = ((DataView)listBox_Info.DataSource).Table;
            DataRow vNewRow = vDataTable.NewRow();
            vNewRow["等级"] = e.Level;
            string vInfoLevel = "";
            switch (e.Level)
            {
                case InfoLevel.Message:
                    vInfoLevel = "消息";
                    break;
                case InfoLevel.Alert:
                    vInfoLevel = "警告";
                    break;
                case InfoLevel.Error:
                    vInfoLevel = "错误";
                    break;
            }
            vNewRow["信息"] = string.Format("{0}:{1}→{2}", vInfoLevel,e.Time, e.Message);
            vDataTable.Rows.Add(vNewRow);
            vDataTable.AcceptChanges();
        }
        #endregion

        #region 模块按钮
        void initButtons()
        {
            int vSpacing = 5;
            int x = 5, y = 3;
            panel_Buttons.Controls.Clear();
            for (int i = 0; i < m_ControlModuleManage.ControlModules.Count; i++)
            {
                int vID = m_ControlModuleManage.ControlModules[i].ID;
                string vName = m_ControlModuleManage.ControlModules[i].Name;
                Button vNewButton = createNewButton(vID, vName, x, y);
                panel_Buttons.Controls.Add(vNewButton);
                y += vSpacing + vNewButton.Height;
            }
        }

        Button createNewButton( int ID, string name,int x,int y )
        {
            Button vNewButton = new Button();
            vNewButton.BackgroundImage = NCLT.TunnelLighting.ControlCell.Properties.Resources.ButtonBackgroundImage;
            vNewButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            vNewButton.Font = new System.Drawing.Font("黑体", 12F);
            vNewButton.ForeColor = System.Drawing.Color.White;
            vNewButton.Location = new System.Drawing.Point(x, y);
            vNewButton.Name = string.Format("Button_{0}",ID);
            vNewButton.Tag = ID;
            vNewButton.Size = new System.Drawing.Size(104, 35);
            vNewButton.TabIndex = 15;
            vNewButton.Text = name;
            vNewButton.UseVisualStyleBackColor = true;
            vNewButton.Click += new EventHandler(vNewButton_Click);
            return vNewButton;
        }

        void vNewButton_Click(object sender, EventArgs e)
        {
            Button vButton = (Button)sender;
            int vID = (int)vButton.Tag;
            panel_Module.Tag = vID;
        }
        #endregion

        #region 窗体事件
        private void MainForm_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Form.CheckForIllegalCrossThreadCalls = false;
            init();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_ControlModuleManage != null)
            {
                m_ControlModuleManage.Dispose();
                m_ControlModuleManage = null;
            }
            if (m_ModbusStateThread != null)
            {
                m_ModbusStateThread.Abort();
                m_ModbusStateThread = null;
            }
        }
        #endregion

        #region Modbus状态线程
        void modeBusThread()
        {
            m_ControlModuleManage.OnModbusActivityStateEventArgsEvent += new ControlModuleManage.ModbusActivityStateEventHandler(m_ControlModuleManage_OnModbusActivityStateEventArgsEvent);
        }

        void m_ControlModuleManage_OnModbusActivityStateEventArgsEvent(object sender, ControlModuleManage.ModbusActivityStateEventArgs e)
        {
            if (e.Activity == Protocol.ModeActivityEnum.RX)
            {
                pictureBox_RX.Image = NCLT.TunnelLighting.ControlCell.Properties.Resources.LED_RX_UP;
                Thread.Sleep(70);
                pictureBox_RX.Image = NCLT.TunnelLighting.ControlCell.Properties.Resources.LED_RX_Down;
            }

            if (e.Activity == Protocol.ModeActivityEnum.TX)
            {
                pictureBox_TX.Image = NCLT.TunnelLighting.ControlCell.Properties.Resources.LED_TX_UP;
                Thread.Sleep(70);
                pictureBox_TX.Image = NCLT.TunnelLighting.ControlCell.Properties.Resources.LED_TX_Down;
            }
        }
        #endregion

        #region 模块监视线程
        void moduleWatchThread()
        {
            while (!m_ModuleWaterThreadStopFlag)
            {
                int vModuleID = panel_Module.Tag==null?0:(int)panel_Module.Tag;
                foreach (ControlModule vTempControlModule in m_ControlModuleManage.ControlModules)
                {
                    if (vTempControlModule.ID == vModuleID)
                    {
                        if (vTempControlModule.State == ModuleState.Normal)
                        {
                            label_Communication.Text = "通讯正常";
                            label_Communication.ForeColor = Color.Green;
                            label_ModuleName.Text = vTempControlModule.Name;

                            //地感线圈
                            label_CarSensor.Enabled = true;
                            if (vTempControlModule.Car == null)
                            {
                                label_CarSensor.Text = "无";
                                label_CarSensor.ForeColor = Color.Blue;
                            }
                            else if (vTempControlModule.Car.LoopPowerDown)
                            {
                                label_CarSensor.Text = "地感线圈掉电";
                                label_CarSensor.ForeColor = Color.Red;
                            }
                            else
                            {
                                label_CarSensor.Text = vTempControlModule.Car.CarNumber.ToString();
                                label_CarSensor.ForeColor = Color.Green;
                            }

                            //照度传感器1
                            label_LightenessSensor1.Enabled = true;
                            if (vTempControlModule.Port1 == null)
                            {
                                label_LightenessSensor1.Text = "无";
                                label_LightenessSensor1.ForeColor = Color.Blue;
                            }
                            else
                            {
                                if (vTempControlModule.Port1.SensorType == SensorTypeEnum.LightenessSensor)
                                {
                                    if (((LightenessSensor)vTempControlModule.Port1).Lighteness > 0)
                                    {
                                        label_LightenessSensor1.Text = ((LightenessSensor)vTempControlModule.Port1).Lighteness.ToString();
                                        label_LightenessSensor1.ForeColor = Color.Green;
                                    }
                                    else
                                    {
                                        label_LightenessSensor1.Text = "0";
                                        label_LightenessSensor1.ForeColor = Color.Red;
                                    }
                                }
                            }

                            //照度传感器２
                            label_LightenessSensor2.Enabled = true;
                            if (vTempControlModule.Port2 == null)
                            {
                                label_LightenessSensor2.Text = "无";
                                label_LightenessSensor2.ForeColor = Color.Blue;
                            }
                            else
                            {
                                if (vTempControlModule.Port2.SensorType == SensorTypeEnum.LightenessSensor)
                                {
                                    if (((LightenessSensor)vTempControlModule.Port2).Lighteness > 0)
                                    {
                                        label_LightenessSensor2.Text = ((LightenessSensor)vTempControlModule.Port2).Lighteness.ToString();
                                        label_LightenessSensor2.ForeColor = Color.Green;
                                    }
                                    else
                                    {
                                        label_LightenessSensor2.Text = "0";
                                        label_LightenessSensor2.ForeColor = Color.Red;
                                    }
                                }
                            }

                            //备用电源
                            label_EmergencyPowerSensor.Enabled = true;
                            if (vTempControlModule.EmergencyPower == null)
                            {
                                label_EmergencyPowerSensor.Text = "无";
                                label_EmergencyPowerSensor.ForeColor = Color.Blue;
                            }
                            else
                            {
                                label_EmergencyPowerSensor.Text = vTempControlModule.EmergencyPower.Voltage.ToString();
                                if (vTempControlModule.EmergencyPower.Voltage > 0)
                                    label_EmergencyPowerSensor.ForeColor = Color.Green;
                                else
                                    label_EmergencyPowerSensor.ForeColor = Color.Red;
                            }

                            //主电源
                            label_PowerSensor.Enabled = true;
                            if (vTempControlModule.Power == null)
                            {
                                label_PowerSensor.Text = "无";
                                label_PowerSensor.ForeColor = Color.Blue;
                            }
                            else if (vTempControlModule.Power.PowerDown)
                            {
                                label_PowerSensor.Text = "电源掉电";
                                label_PowerSensor.ForeColor = Color.Red;
                            }
                            else
                            {
                                label_PowerSensor.Text = "电源正常";
                                label_PowerSensor.ForeColor = Color.Green;
                            }

                            //PWM
                            label_PWM.Enabled = true;
                            if (vTempControlModule.PWMMode == PWMModeEnum.NoPWM)
                            {
                                label_PWM.Text = "无";
                                label_PWM.ForeColor = Color.Blue;
                            }
                            else
                            {
                                label_PWM.Text = vTempControlModule.GetPWM().ToString() + "%";
                                label_PWM.ForeColor = Color.Green;
                            }
                        }
                        else
                        {
                            label_Communication.Text = "通讯超时";
                            label_Communication.ForeColor = Color.Red;
                            label_ModuleName.Text = vTempControlModule.Name;

                            label_CarSensor.Enabled = false;
                            label_LightenessSensor1.Enabled = false;
                            label_LightenessSensor2.Enabled = false;
                            label_EmergencyPowerSensor.Enabled = false;
                            label_PowerSensor.Enabled = false;
                            label_PWM.Enabled = false;

                        }
                    }
                }
                Thread.Sleep( 300 );
            }
        }
        #endregion

        #region PWM曲线线程
        void pwmCurveThread()
        {
            m_ControlModuleManage.OnReceiveDataEventArgsEvent += new ControlModuleManage.ReceiveDataEventArgsEventHandler(m_ControlModuleManage_OnReceiveDataEventArgsEvent);
            //while (!m_PWMCurveThreadStopFlag)
            //{
            //    List<int> vPWMValueList = new List<int>();
            //    List<string> vPWMNameList = new List<string>();
            //    foreach (ControlModule vTempControlModule in m_ControlModuleManage.ControlModules)
            //    {
            //        if ( vTempControlModule.PWMMode != PWMModeEnum.NoPWM )
            //        {
            //            vPWMValueList.Add(vTempControlModule.GetPWM());
            //            vPWMNameList.Add(vTempControlModule.Name);
            //        }
            //    }

            //    if (vPWMValueList.Count != 0)
            //    {
            //        chart_PWM.Series[0].Points.DataBindXY(vPWMNameList.ToArray(), vPWMValueList.ToArray());
            //        chart_PWM.Series[0].Name = "PWM输出值";
            //        System.Diagnostics.Debug.WriteLine( "PWM数量:{0}", vPWMNameList.Count );
            //    }
            //    else
            //    {
            //        System.Diagnostics.Debug.WriteLine("曲线图被清空");
            //    }
            //    Thread.Sleep(5000);
            //}
        }
        #endregion

        #region 按钮
        private void pictureBox_Setup_Click(object sender, EventArgs e)
        {
            if (!m_ControlModuleManage.IsRun)
            {
                SetupForm vSetupForm = new SetupForm();
                vSetupForm.ControlModuleManag = m_ControlModuleManage;
                vSetupForm.ShowDialog();
                init();
                vSetupForm.Dispose();
                vSetupForm = null;
            }
        }

        private void pictureBox_Run_Click(object sender, EventArgs e)
        {
            if (!m_ControlModuleManage.IsRun)
            {
                m_ControlModuleManage.Run();
                //ModBus
                m_ModbusStateThread = new Thread(new ThreadStart(modeBusThread));
                m_ModbusStateThread.Start();
                //模块状态线程
                m_ModuleWatchThread = new Thread(new ThreadStart(moduleWatchThread));
                m_ModuleWaterThreadStopFlag = false;
                m_ModuleWatchThread.Start();
                //输出信息线程
                m_MessageThread = new Thread(new ThreadStart(messageThread));
                m_MessageThread.Start();
                //PWM曲线线程
                m_PWMCurveThread = new Thread(new ThreadStart(pwmCurveThread));
                //m_PWMCurveThreadStopFlag = false;
                m_PWMCurveThread.Start();
                //初始化PWM图
                initPWMChar();

                pictureBox_Run.Image = NCLT.TunnelLighting.ControlCell.Properties.Resources.Run_Down;
                pictureBox_Stop.Image = NCLT.TunnelLighting.ControlCell.Properties.Resources.Stop_UP;
                pictureBox_Setup.Image = NCLT.TunnelLighting.ControlCell.Properties.Resources.Setup_Down;
            }
        }

        private void pictureBox_Stop_Click(object sender, EventArgs e)
        {
            if (m_ControlModuleManage.IsRun )
            {
                m_ControlModuleManage.Stop();

                if (m_ModbusStateThread != null)
                {
                    m_ModbusStateThread.Abort();
                    m_ModbusStateThread = null;
                }

                if (m_ModuleWatchThread != null)
                {
                    m_ModuleWaterThreadStopFlag = true;
                    m_ModuleWatchThread.Abort();
                    m_ModuleWatchThread = null;
                }

                if (m_MessageThread != null)
                {
                    m_MessageThread.Abort();
                    m_MessageThread = null;
                }

                if (m_PWMCurveThread != null)
                {
                    //m_PWMCurveThreadStopFlag = true;
                    m_PWMCurveThread.Abort();
                    m_PWMCurveThread = null;
                }

                pictureBox_Run.Image = NCLT.TunnelLighting.ControlCell.Properties.Resources.Run_UP;
                pictureBox_Stop.Image = NCLT.TunnelLighting.ControlCell.Properties.Resources.Stop_Down;
                pictureBox_Setup.Image = NCLT.TunnelLighting.ControlCell.Properties.Resources.Setup_UP;

                pictureBox_RX.Image = NCLT.TunnelLighting.ControlCell.Properties.Resources.LED_RX_Down;
                pictureBox_TX.Image = NCLT.TunnelLighting.ControlCell.Properties.Resources.LED_TX_Down;
            }
        }
        #endregion

        private void checkBox_Message_CheckedChanged(object sender, EventArgs e)
        {
            string vFilter = "";
            if (checkBox_Error.Checked)
                vFilter = string.Format("{0}",(short)InfoLevel.Error );
            if (checkBox_Alert.Checked)
            {
                if (vFilter == "")
                    vFilter += string.Format("{0}", (short)InfoLevel.Alert);
                else
                    vFilter += string.Format(",{0}", (short)InfoLevel.Alert);
            }

            if (checkBox_Message.Checked)
            {
                if (vFilter == "")
                    vFilter += string.Format("{0}", (short)InfoLevel.Message);
                else
                    vFilter += string.Format(",{0}", (short)InfoLevel.Message);
            }

            if ( vFilter != "" )
                m_MessageTableInfo.DefaultView.RowFilter = string.Format("等级 in ({0})", vFilter);
            else
                m_MessageTableInfo.DefaultView.RowFilter = string.Format("等级 in (100)");
        }
    }
}
