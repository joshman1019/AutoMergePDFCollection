using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMergePDF
{
    public interface IPDFMergeDocument
    {
         string IdentificationMarker { get; set; }
         string PDF1Path { get; set; }
         string PDF2Path { get; set; }
        string OutputPath { get; set; }
    }
}
