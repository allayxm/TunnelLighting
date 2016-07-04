using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NCLT.TunnelLighting.ControlCell
{
    public class Debug:IDisposable
    {
        StreamWriter m_StreamWriter = null;
        public Debug()
        {
            string vAppPath = string.Format("{0}dbeug\\", System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
            string vFileName = string.Format("{0}.txt", DateTime.Today.Date.ToShortDateString());
            if (!Directory.Exists(vAppPath))
                Directory.CreateDirectory(vAppPath);
            string vFile = string.Format("{0}{1}", vAppPath, vFileName);
            m_StreamWriter = new StreamWriter(vFile, true);
        }

        public void WriteDebugLog(string DebugInfo)
        {
            if (m_StreamWriter != null)
            {
                m_StreamWriter.WriteLine(string.Format("{0}->{1}", DateTime.Now.ToLongTimeString(), DebugInfo));
                m_StreamWriter.Flush();
            }
        }

        public void Dispose()
        {
            if (m_StreamWriter != null)
            {
                m_StreamWriter.Close();
                m_StreamWriter.Dispose();
                m_StreamWriter = null;
            }
        }
    }
}
