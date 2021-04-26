using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOperations
{
   public interface ILogger
    {
        void Log(LoggerModel loggerModel);
    }
}
