using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace NCLT.TunnelLighting.LightControl
{
    class TIniFile
    {
        public string FileName;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
         string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
         string key, string def, StringBuilder retVal,
         int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileSection(string lpAppName,
         byte[] lpReturnedString, int nSize, string lpFileName);
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileSection(string lpAppName,
         string lpString, string lpFileName);

        public TIniFile(string str)
        {
            FileName = str;
        }

        public void IniWriteValue(string Section, string Key, string Value)//对ini文件进行写操作的函数
        {
            WritePrivateProfileString(Section, Key, Value, FileName);
        }

        public string IniReadValue(string Section, string Key)//对ini文件进行读操作的函数
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, FileName);
            return temp.ToString();

        }
        public StringCollection IniReadSection(string Section)
        {
            StringCollection temp = new StringCollection();

            byte[] buffer = new byte[32768];
            int bufLen = 0;
            bufLen = GetPrivateProfileSection(Section, buffer, buffer.GetUpperBound(0), FileName);
            if (bufLen > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bufLen; i++)
                {
                    //if (buffer[i] != 0)
                    //{
                    sb.Append((char)buffer[i]);
                    //}
                    //else
                    //{
                    //    if (sb.Length > 0)
                    //    {
                    temp.Add(sb.ToString());
                    //temp.Add(sb.ToString());
                    sb = new StringBuilder();
                    //    }
                    //}
                }
            }
            return temp;

        }
    }
}
