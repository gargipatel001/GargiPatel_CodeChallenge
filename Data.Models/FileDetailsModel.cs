using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Data.Models
{
    public class FileDetailsModel
    {
        public string FilePath { get; set; }

        public string FileName { get; set; }
        public string SavePath { get; set; }

        public string Content { get; set; }
        public HttpPostedFileBase FileDetail { get; set; }
    }
}
