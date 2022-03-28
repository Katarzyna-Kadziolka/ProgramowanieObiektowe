using Zadanie1.Devices;
using Zadanie1.Documents;

// var xerox = new Copier();
// xerox.PowerOn();
// IDocument doc1 = new PDFDocument("aaa.pdf");
// xerox.Print(in doc1);
//
// IDocument doc2 = new ImageDocument("aaa.JPG");
// xerox.Scan(out doc2, IDocument.FormatType.JPG);
//
// xerox.ScanAndPrint();
// Console.WriteLine( xerox.Counter );
// Console.WriteLine( xerox.PrintCounter );
// Console.WriteLine( xerox.ScanCounter );


var multifunctionalDevice = new MultifunctionalDevice();
multifunctionalDevice.PowerOn();
IDocument doc3 = new PDFDocument("test1.pdf");
multifunctionalDevice.Print(in doc3);

IDocument doc4 = new ImageDocument("test2.JPG");
multifunctionalDevice.Scan(out doc4, IDocument.FormatType.JPG);

multifunctionalDevice.Send();

IDocument doc5 = new ImageDocument("sentFile.jpg");
multifunctionalDevice.Receive(doc5);

Console.WriteLine( "Counter:" + multifunctionalDevice.Counter );
Console.WriteLine( "PrintCounter:" + multifunctionalDevice.PrintCounter );
Console.WriteLine( "ScanCounter:" + multifunctionalDevice.ScanCounter );
Console.WriteLine( "SendCounter:" + multifunctionalDevice.SendCounter );
Console.WriteLine( "ReceiveCounter:" + multifunctionalDevice.ReceiveCounter );