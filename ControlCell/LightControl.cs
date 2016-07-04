using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCLT.TunnelLighting.LightControl
{
    public class LightControlPWM
    {
        string m_AppPath;
        public LightControlPWM(string AppPath)
        {
            m_AppPath = AppPath;
        }

        private int Id;

        public int id
        {
            get { return Id; }
            set { Id = value; }
        }

        private bool Car_Idle;

        public bool car_Idle
        {
            get { return Car_Idle; }
            set { Car_Idle = value; }
        }

        private int Out_illumination;

        public int out_illumination
        {
            get { return Out_illumination; }
            set { Out_illumination = value; }
        }

        private int In_illumination;

        public int in_illumination
        {
            get { return In_illumination; }
            set { In_illumination = value; }
        }


        public int[] Judge()
        {
            int[] illum = new int[2];
            illum[0] = 0;
            illum[1] = 0;

            int in_illum_level = 0;
            int out_illum_level = 0;

            TIniFile ini = new TIniFile(string.Format("{0}\\SysCfg.ini", m_AppPath));

            int in_illum_level_count = int.Parse(ini.IniReadValue("洞内亮度级别", "数量"));
            int out_illum_level_count = int.Parse(ini.IniReadValue("洞外亮度级别", "数量"));

            for (int i = 0; i < out_illum_level_count; i++)
            {
                if (Out_illumination >= int.Parse(ini.IniReadValue("洞外亮度级别", "级别" + (i + 1).ToString())))
                {
                    out_illum_level = i + 1;
                    break;
                }
            }

            for (int j = 0; j < in_illum_level_count; j++)
            {
                if (In_illumination >= int.Parse(ini.IniReadValue("洞内亮度级别", "级别" + (j + 1).ToString())))
                {
                    in_illum_level = j + 1;
                    break;
                }
            }

            if (!Car_Idle)
            {
                illum[0] = int.Parse(ini.IniReadValue("基本段照明设置", "00"));
                illum[1] = int.Parse(ini.IniReadValue("灯光设置" + Id.ToString().Trim(), "00"));
            }
            else
            {
                illum[0] = int.Parse(ini.IniReadValue("基本段照明设置", out_illum_level.ToString().Trim() + in_illum_level.ToString().Trim()));
                illum[1] = int.Parse(ini.IniReadValue("灯光设置" + Id.ToString().Trim(), out_illum_level.ToString().Trim() + in_illum_level.ToString().Trim()));
            }
            return illum;
        }
    }
}
