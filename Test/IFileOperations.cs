﻿using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOperations
{
    public interface IFileOperations
    {
        string[] Read(string path);

        bool Upload(FileDetailsModel fileDetailsModel);
    }
}
