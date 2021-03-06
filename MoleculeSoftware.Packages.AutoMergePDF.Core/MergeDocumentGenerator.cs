using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using System.Collections.Generic;

namespace AutoMergePDF
{
    public class MergeDocumentGenerator
    {
        private readonly IPDFMergeDocument m_Document;
        private readonly IPDFMultipleMergeDocuments m_Documents;

        public MergeDocumentGenerator(IPDFMergeDocument document)
        {
            m_Document = document;
        }

        public MergeDocumentGenerator(IPDFMultipleMergeDocuments documents)
        {
            m_Documents = documents;
        }

        public void CreateMultipleMergeDocuments()
        {
            // Guard clauses
            if(m_Documents == null)
            {
                throw new System.Exception("Exception 7: The input documents object was null"); 
            }

            if(m_Documents.InputDocumentPaths.Count == 0)
            {
                throw new System.Exception("Exception: 8: There were no input documents added"); 
            }

            foreach (string inputPath in m_Documents.InputDocumentPaths)
            {
                if(!System.IO.File.Exists(inputPath))
                {
                    throw new System.Exception("Exception 9: One or more of the input file paths do not exist"); 
                }
            }

            // Create initial document that will contain all of the merged documents
            PdfDocument document = new PdfDocument(new PdfWriter(m_Documents.OutputDocumentPath));

            // Create merger that will perform the document merging
            PdfMerger merger = new PdfMerger(document);

            List<PdfDocument> documentsList = new List<PdfDocument>();

            foreach (string filePath in m_Documents.InputDocumentPaths)
            {
                PdfDocument newDoc = new PdfDocument(new PdfReader(filePath));
                documentsList.Add(newDoc); 
            }

            foreach (PdfDocument pdfDocument in documentsList)
            {
                merger.Merge(pdfDocument, 1, pdfDocument.GetNumberOfPages());
                pdfDocument.Close(); 
            }

            document.Close(); 
        }

        /// <summary>
        /// Generates the merged documents
        /// </summary>
        public void CreateMergedDocument()
        {
            // Guard clauses
            if(m_Document == null)
            {
                throw new System.Exception("Exception 1: Single Merge Document was null"); 
            }

            if(string.IsNullOrEmpty(m_Document.OutputPath))
            {
                throw new System.Exception("Exception 2: Output path was null or empty"); 
            }

            if(string.IsNullOrEmpty(m_Document.PDF1Path))
            {
                throw new System.Exception("Exception 3: Document 1 input path was null or empty"); 
            }

            if(string.IsNullOrEmpty(m_Document.PDF2Path))
            {
                throw new System.Exception("Exception 4: Document 2 input path was null or empty"); 
            }

            if(!System.IO.File.Exists(m_Document.PDF1Path))
            {
                throw new System.Exception("Exception 5: Input file number 1 does not exist at the location provided"); 
            }

            if (!System.IO.File.Exists(m_Document.PDF2Path))
            {
                throw new System.Exception("Exception 6: Input file number 2 does not exist at the location provided");
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
