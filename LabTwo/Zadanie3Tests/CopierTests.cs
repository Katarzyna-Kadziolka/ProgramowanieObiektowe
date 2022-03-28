using System;
using NUnit.Framework;
using Zadanie1UnitTests;
using Zadanie3.Devices;
using Zadanie3.Documents;

namespace Zadanie3Tests; 

public class CopierTests {
    [Test]
    public void Copier_GetState_StateOff() {
        var copier = new Copier();
        copier.PowerOff();

        Assert.AreEqual(IDevice.State.off, copier.GetState());
    }

    [Test]
    public void Copier_GetState_StateOn() {
        var copier = new Copier();
        copier.PowerOn();

        Assert.AreEqual(IDevice.State.on, copier.GetState());
    }
    [Test]
    public void Copier_Print_DeviceOn() {
        var copier = new Copier();
        copier.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            IDocument doc1 = new PDFDocument("aaa.pdf");
            copier.Print(in doc1);
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
        }

        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
    
    [Test]
    public void Copier_Print_DeviceOff() {
        var copier = new Copier();
        copier.PowerOff();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            IDocument doc1 = new PDFDocument("aaa.pdf");
            copier.Print(in doc1);
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
        }

        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
    
    [Test]
    public void Copier_Scan_DeviceOff() {
        var copier = new Copier();
        copier.PowerOff();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            IDocument doc1;
            copier.Scan(out doc1);
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
        }

        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
    
    [Test]
    public void Copier_Scan_DeviceOn() {
        var copier = new Copier();
        copier.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            IDocument doc1;
            copier.Scan(out doc1);
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
        }

        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
    
    [Test]
    public void Copier_Scan_FormatTypeDocument() {
        var copier = new Copier();
        copier.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            IDocument doc1;
            copier.Scan(out doc1, formatType: IDocument.FormatType.JPG);
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

            copier.Scan(out doc1, formatType: IDocument.FormatType.TXT);
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

            copier.Scan(out doc1, formatType: IDocument.FormatType.PDF);
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
        }

        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
    
    [Test]
    public void Copier_ScanAndPrint_DeviceOn() {
        var copier = new Copier();
        copier.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            copier.ScanAndPrint();
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
        }

        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
    
    [Test]
    public void Copier_ScanAndPrint_DeviceOff() {
        var copier = new Copier();
        copier.PowerOff();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            copier.ScanAndPrint();
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
        }

        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
}