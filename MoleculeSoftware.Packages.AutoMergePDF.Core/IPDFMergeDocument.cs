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
