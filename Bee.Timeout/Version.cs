using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bee.Timeout
{
    public class Version
    {
        public static void version(string fileName, string data)
        {
            try
            {
                using (StreamWriter streamWriter = File.AppendText(Environment.CurrentDirectory + "/" + fileName))
                    streamWriter.WriteLine("[" + DateTime.Now + "] " + data);
            }
            catch
            {

            }
        }
    }
}
