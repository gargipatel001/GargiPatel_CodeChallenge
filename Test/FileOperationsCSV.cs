using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FileOperations
{
    public class FileOperationsCSV : IFileOperations
    {
        // Read CSV file
        public string[]  Read(string path)
        {
            string[] csvData = System.IO.File.ReadAllLines(path);           

            return csvData;
        }

        // CSV File Upload
        public bool Upload(FileDetailsModel fileDetails)
        {
            bool isUploadSuccess;
            try
            {
                // Save uploaded file to UploadedFiles Folder
                fileDetails.FileDetail.SaveAs(fileDetails.SavePath);

                isUploadSuccess = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isUploadSuccess;
        }
    }
}
