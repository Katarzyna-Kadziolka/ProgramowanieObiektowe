using System;
using NUnit.Framework;
using Zadanie1UnitTests;
using Zadanie3.Devices;
using Zadanie3.Documents;

namespace Zadanie3Tests;

public class PrinterTests {

    [Test]
    public void Printer_GetState_StateOff() {
        var printer = new Printer();
        printer.PowerOff();

        Assert.AreEqual(IDevice.State.off, printer.GetState());
    }

    [Test]
    public void Printer_GetState_StateOn() {
        var printer = new Printer();
        printer.PowerOn();

        Assert.AreEqual(IDevice.State.on, printer.GetState());
    }
    
    [Test]
    public void Printer_Print_DeviceOn() {
        var printer = new Printer();
        printer.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            IDocument doc1 = new PDFDocument("aaa.pdf");
            printer.Print(in doc1);
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
        }

        Assert.AreEqual(currentConsoleOut, Console.Out);
    }

    [Test]
    public void Printer_Print_DeviceOff() {
        var printer = new Printer();
        printer.PowerOff();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            IDocument doc1 = new PDFDocument("aaa.pdf");
            printer.Print(in doc1);
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
        }

        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
    [Test]
    public void Printer_PrintCounter() {
        var printer = new Printer();
        printer.PowerOn();

        IDocument doc1 = new PDFDocument("aaa.pdf");
        printer.Print(in doc1);
        IDocument doc2 = new TextDocument("aaa.txt");
        printer.Print(in doc2);
        IDocument doc3 = new ImageDocument("aaa.jpg");
        printer.Print(in doc3);

        printer.PowerOff();
        printer.Print(in doc3);
        printer.PowerOn();

        printer.Print(in doc3);

        Assert.AreEqual(4, printer.PrintCounter);
    }
    [Test]
    public void Printer_PowerOnCounter() {
        var printer = new Printer();
        printer.PowerOn();
        printer.PowerOn();
        printer.PowerOn();
        printer.PowerOff();
        printer.PowerOff();
        printer.PowerOff();
        printer.PowerOn();

        IDocument doc3 = new ImageDocument("aaa.jpg");
        printer.Print(in doc3);

        printer.PowerOff();
        printer.Print(in doc3);
        printer.PowerOn();

        Assert.AreEqual(3, printer.Counter);
    }
}