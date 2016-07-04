using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NCLT.TunnelLighting.ControlCell
{
    public partial class SelectCarSensorForm : Form
    {
        public SelectCarSensorForm()
        {
            InitializeComponent();
        }

        DataTable m_SensorListTable = null;
        public DataTable SensorListTable
        {
            set
            {
                m_SensorListTable = value;
            }
        }

        int m_SelectedContorlModuleID;
        public int SelectedContorlModuleID
        {
            get
            {
                return m_SelectedContorlModuleID;
            }
        }

        public string m_Memo;
        public string Memo
        {
            get
            {
                return m_Memo;
            }
        }

        void init()
        {
            if (m_SensorListTable != null)
            {
                listBox_CarSensor.DataSource = m_SensorListTable;
                listBox_CarSensor.ValueMember = "ID";
                listBox_CarSensor.DisplayMember = "说明";
            }
        }

        private void SelectCarSensorForm_Load(object sender, EventArgs e)
        {
            init();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            int vSelectedValueID = (int)listBox_CarSensor.SelectedValue;
            DataTable vDataTable = (DataTable)listBox_CarSensor.DataSource;
            DataRow vFindRow = vDataTable.Rows.Find(vSelectedValueID);
            if (vFindRow != null)
            {
                m_SelectedContorlModuleID = (int)vFindRow["模块ID"];
                m_Memo = (string)vFindRow["说明"];
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

    }
}
