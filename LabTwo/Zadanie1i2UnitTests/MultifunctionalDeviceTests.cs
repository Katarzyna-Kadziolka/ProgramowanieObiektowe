using System;
using FluentAssertions;
using NUnit.Framework;
using Zadanie1.Devices;
using Zadanie1.Documents;

namespace Zadanie1UnitTests; 

public class MultifunctionalDeviceTests {
    [Test]
    public void MultifunctionalDevice_GetState_StateOff() {
        var copier = new MultifunctionalDevice();
        copier.PowerOff();

        Assert.AreEqual(IDevice.State.off, copier.GetState());
    }

    [Test]
    public void MultifunctionalDevice_GetState_StateOn() {
        var copier = new MultifunctionalDevice();
        copier.PowerOn();

        Assert.AreEqual(IDevice.State.on, copier.GetState());
    }
    [Test]
    public void SendCounter_ShouldRetunInt() {
        // Arrange
        var multifunctionalDevice = new MultifunctionalDevice();
        // Act
        multifunctionalDevice.PowerOn();

        multifunctionalDevice.Send();
        multifunctionalDevice.Send();
        multifunctionalDevice.Send();

        multifunctionalDevice.PowerOff();
        multifunctionalDevice.Send();
        multifunctionalDevice.Send();
        multifunctionalDevice.PowerOn();
        
        // Assert
        multifunctionalDevice.SendCounter.Should().Be(3);
    }
    [Test]
    public void ReceiveCounter_ShouldReturnInt() {
        // Arrange
        var multifunctionalDevice = new MultifunctionalDevice();
        // Act
        multifunctionalDevice.PowerOn();

        IDocument doc1 = new ImageDocument("SendFile1.png");
        multifunctionalDevice.Receive(doc1);
        IDocument doc2 = new ImageDocument("SendFile2.png");
        multifunctionalDevice.Receive(doc2);
        IDocument doc3 = new ImageDocument("SendFile3.png");
        multifunctionalDevice.Receive(doc3);

        multifunctionalDevice.PowerOff();
        IDocument doc4 = new ImageDocument("SendFile4.png");
        multifunctionalDevice.Receive(doc4);
        IDocument doc5 = new ImageDocument("SendFile5.png");
        multifunctionalDevice.Receive(doc5);
        multifunctionalDevice.PowerOn();
        
        // Assert
        multifunctionalDevice.ReceiveCounter.Should().Be(3);
    }

    [Test]
    public void Send_StateOn() {
        // Arrange
        var device = new MultifunctionalDevice();
        device.PowerOn();
        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();

        // Act
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            device.Send();
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Send"));
        }
        // Assert
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }

    [Test]
    public void Send_StateOff() {
        // Arrange
        var device = new MultifunctionalDevice();
        device.PowerOff();
        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();

        // Act
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            device.Send();
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Send"));
        }
        // Assert
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
    [Test]
    public void Receive_StateOn() {
        // Arrange
        var device = new MultifunctionalDevice();
        device.PowerOn();
        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        var doc = new ImageDocument("SentImage.jpg");

        // Act
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            device.Receive(doc);
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Receive"));
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
        }
        // Assert
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
    [Test]
    public void Receive_StateOff() {
        // Arrange
        var device = new MultifunctionalDevice();
        device.PowerOff();
        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        var doc = new ImageDocument("SentImage.jpg");

        // Act
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            device.Receive(doc);
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Receive"));
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
        }
        // Assert
        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
    [Test]
    public void Print_DeviceOn() {
        var multifunctionalDevice = new MultifunctionalDevice();
        multifunctionalDevice.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            IDocument doc1 = new PDFDocument("aaa.pdf");
            multifunctionalDevice.Print(in doc1);
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
        }

        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
    
    [Test]
    public void Print_DeviceOff() {
        var multifunctionalDevice = new MultifunctionalDevice();
        multifunctionalDevice.PowerOff();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            IDocument doc1 = new PDFDocument("aaa.pdf");
            multifunctionalDevice.Print(in doc1);
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
        }

        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
    
    [Test]
    public void Scan_DeviceOff() {
        var multifunctionalDevice = new MultifunctionalDevice();
        multifunctionalDevice.PowerOff();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            IDocument doc1;
            multifunctionalDevice.Scan(out doc1);
            Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
        }

        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
    
    [Test]
    public void Scan_DeviceOn() {
        var multifunctionalDevice = new MultifunctionalDevice();
        multifunctionalDevice.PowerOn();

        var currentConsoleOut = Console.Out;
        currentConsoleOut.Flush();
        using (var consoleOutput = new ConsoleRedirectionToStringWriter()) {
            IDocument doc1;
            multifunctionalDevice.Scan(out doc1);
            Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
        }

        Assert.AreEqual(currentConsoleOut, Console.Out);
    }
}