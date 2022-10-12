using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Shared.Models
{
    public class UploadedFile
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}
