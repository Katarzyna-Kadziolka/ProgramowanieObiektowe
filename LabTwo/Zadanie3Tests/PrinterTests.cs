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
}