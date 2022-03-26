using Zadanie1.Devices;
using Zadanie1.Documents;

var xerox = new Copier();
xerox.PowerOn();
IDocument doc1 = new PDFDocument("aaa.pdf");
xerox.Print(in doc1);

IDocument doc2 = new ImageDocument("aaa.JPG");
xerox.Scan(out doc2, IDocument.FormatType.JPG);

xerox.ScanAndPrint();
Console.WriteLine( xerox.Counter );
Console.WriteLine( xerox.PrintCounter );
Console.WriteLine( xerox.ScanCounter );
