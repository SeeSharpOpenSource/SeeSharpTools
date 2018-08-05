using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;

namespace SeeSharpTools.JY.Report
{
    internal class ErrorHandler
    {
        public static string ErrorParsing(int errNumber)
        {
            try
            {
                Dictionary<int, string> dict = new Dictionary<int, string>();
                MemoryStream ms = new MemoryStream(Properties.Resources.HResultLUT);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(dict.GetType());
                dict = (Dictionary<int, string>)ser.ReadObject(ms);
                string description;
                dict.TryGetValue(errNumber, out description);                
                return description;

            }
            catch (Exception ex )
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
