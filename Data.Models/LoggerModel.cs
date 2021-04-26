using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class LoggerModel
    {
        public string FileName { get; set; }
        public int FileSize { get; set; }        
        public bool isSuccess  { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }

    }
}
