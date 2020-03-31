using System.Collections.Generic;
using iTextSharp.text.pdf;

namespace PdfHighlightExtractor
{
    public class Extractor
    {
        public static List<string> GetHighlights(string pdfPath)
        {
            PdfReader pdfReader = new PdfReader(pdfPath);

            List<string> data = new List<string>();

            for (int i = 1; i <= pdfReader.NumberOfPages; i++)
            {
                var page = pdfReader.GetPageN(i);
                PdfArray annotations = page.GetAsArray(PdfName.ANNOTS);

                if (annotations != null)
                    foreach (PdfObject annotationDict in annotations.ArrayList)
                    {
                        PdfDictionary annotation = (PdfDictionary)PdfReader.GetPdfObject(annotationDict);
                        PdfString contents = annotation.GetAsString(PdfName.CONTENTS);
                        // now use the String value of contents

                        if (contents != null)
                        {
                            data.Add(contents.ToUnicodeString());
                        }
                    }
            }

            return data;
        }
    }
}