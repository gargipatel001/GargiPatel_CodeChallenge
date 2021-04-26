using Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOperations
{
    public class Logger : ILogger
    {
        public void Log(LoggerModel loggerModel)
        {
            string fullpath = loggerModel.Path+ "/Log.csv";
            File.AppendAllText(fullpath, string.Format("\n{0}|{1}|{2}|{3}|{4}",loggerModel.FileName,loggerModel.FileSize,DateTime.Now.Date,loggerModel.isSuccess?"Successfully Uploaded":"Upload Failed",loggerModel.Message));
        }
    }
}
