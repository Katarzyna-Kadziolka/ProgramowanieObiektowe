﻿using Zadanie1.Devices;
using Zadanie1.Documents;

var xerox = new Copier();
xerox.PowerOn();
IDocument doc1 = new PDFDocument("aaa.pdf");
xerox.Print(in doc1);

IDocument doc2 = new ImageDocument("aaa.JPG");
xerox.Scan(out doc2, IDocument.FormatType.JPG);

xerox.ScanAndPrint();
System.Console.WriteLine( xerox.Counter );
System.Console.WriteLine( xerox.PrintCounter );
System.Console.WriteLine( xerox.ScanCounter );
