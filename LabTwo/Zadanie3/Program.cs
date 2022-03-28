using Zadanie3.Devices;
using Zadanie3.Documents;


var xerox = new Copier();
xerox.PowerOn();
IDocument doc1 = new PDFDocument("aaa.pdf");
xerox.Print(in doc1);

IDocument doc2 = new ImageDocument("aaa.JPG");
xerox.Scan(out doc2, IDocument.FormatType.JPG);

xerox.ScanAndPrint();
Console.WriteLine("Xeros");
Console.WriteLine( "Counter:" + xerox.Counter );
Console.WriteLine( "PrintCounter:" + xerox.Printer.PrintCounter );
Console.WriteLine( "ScanCounter:" + xerox.Scanner.ScanCounter );


var multifunctionalDevice = new MultifunctionalDevice();
multifunctionalDevice.PowerOn();
IDocument doc3 = new PDFDocument("test1.pdf");
multifunctionalDevice.Print(in doc3);

IDocument doc4 = new ImageDocument("test2.JPG");
multifunctionalDevice.Scan(out doc4, IDocument.FormatType.JPG);

multifunctionalDevice.Send();

IDocument doc5 = new ImageDocument("sentFile.jpg");
multifunctionalDevice.Receive(doc5);

Console.WriteLine("Multifunctional Device");
Console.WriteLine( "Counter:" + multifunctionalDevice.Counter );
Console.WriteLine( "PrintCounter:" + multifunctionalDevice.Printer.PrintCounter );
Console.WriteLine( "ScanCounter:" + multifunctionalDevice.Scanner.ScanCounter );
Console.WriteLine( "SendCounter:" + multifunctionalDevice.SendCounter );
Console.WriteLine( "ReceiveCounter:" + multifunctionalDevice.ReceiveCounter );