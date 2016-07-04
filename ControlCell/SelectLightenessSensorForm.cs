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
    public partial class SelectLightenessSensorForm : Form
    {
        public SelectLightenessSensorForm()
        {
            InitializeComponent();
        }

        
        private void SelectLightenessSensorForm_Load(object sender, EventArgs e)
        {
            init();
        }

        DataTable m_PortListTable = null;
        public DataTable PortListTable
        {
            set
            {
                m_PortListTable = value;
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

        short m_SelectedControlPortNumber;
        public short SelectedControlPortNumber
        {
            get
            {
                return m_SelectedControlPortNumber;
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
            if (m_PortListTable != null)
            {
                listBox_Lighteness.DataSource = m_PortListTable;
                listBox_Lighteness.ValueMember = "ID";
                listBox_Lighteness.DisplayMember = "说明";
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            int vSelectedValueID = (int)listBox_Lighteness.SelectedValue;
            DataTable vDataTable = (DataTable)listBox_Lighteness.DataSource;
            DataRow vFindRow = vDataTable.Rows.Find(vSelectedValueID);
            if (vFindRow != null)
            {
                m_SelectedContorlModuleID   = (int)vFindRow["模块ID"];
                m_SelectedControlPortNumber = (Int16)vFindRow["端口号"];
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
