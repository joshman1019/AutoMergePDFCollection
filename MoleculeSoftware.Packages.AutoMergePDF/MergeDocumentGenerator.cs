using iText.Kernel.Pdf;
using iText.Kernel.Utils;

namespace AutoMergePDF
{
    public class MergeDocumentGenerator
    {
        private readonly IPDFMergeDocument m_Document;

        public MergeDocumentGenerator(IPDFMergeDocument document)
        {
            m_Document = document;
        }

        /// <summary>
        /// Generates the merged documents
        /// </summary>
        public void CreateMergedDocument()
        {
            // Guard clauses
            if(string.IsNullOrEmpty(m_Document.OutputPath))
            {
                throw new System.Exception("Exception 1: Output path was null or empty"); 
            }

            if(string.IsNullOrEmpty(m_Document.PDF1Path))
            {
                throw new System.Exception("Exception 2: Document 1 input path was null or empty"); 
            }

            if(string.IsNullOrEmpty(m_Document.PDF2Path))
            {
                throw new System.Exception("Exception 3: Document 2 input path was null or empty"); 
            }

            if(!System.IO.File.Exists(m_Document.PDF1Path))
            {
                throw new System.Exception("Exception 4: Input file number 1 does not exist at the location provided"); 
            }

            if (!System.IO.File.Exists(m_Document.PDF2Path))
            {
                throw new System.Exception("Exception 5: Input file number 2 does not exist at the location provided");
            }

            // Create initial document that will contain all of the merged documents
            PdfDocument document = new PdfDocument(new PdfWriter(m_Document.OutputPath));

            // Create merger that will perform the document merging
            PdfMerger merger = new PdfMerger(document);

            // Source document 1
            PdfDocument document1 = new PdfDocument(new PdfReader(m_Document.PDF1Path));

            // Source document 2
            PdfDocument document2 = new PdfDocument(new PdfReader(m_Document.PDF2Path));

            // Perform merge from document 1
            merger.Merge(document1, 1, document1.GetNumberOfPages());

            // Perform merge from document 2
            merger.Merge(document2, 1, document2.GetNumberOfPages());

            // Close the documents 
            document1.Close();
            document2.Close();
            document.Close(); 
        }
    }
}
