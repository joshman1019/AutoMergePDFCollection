using System.Collections.Generic;

namespace AutoMergePDF
{
    public interface IPDFMultipleMergeDocuments
    {
        string IdentificationMarker { get; set; }
        List<string> InputDocumentPaths { get; set; }
        string OutputDocumentPath { get; set; }
    }
}
