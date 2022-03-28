using Zadanie3.Documents;

namespace Zadanie3.Devices; 

public class MultifunctionalDevice: BaseDevice, IFax {
    public int SendCounter { get; set; }
    public int ReceiveCounter { get; set; }
    private Printer _printer;
    private Scanner _scanner;

    public MultifunctionalDevice() {
        _printer = new Printer();
        _scanner = new Scanner();
    }
    public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG) {
        if (state == IDevice.State.on) {
            _scanner.PowerOn();
            _scanner.Scan(out document, formatType);
            _scanner.PowerOff();
        }
        else {
            document = null;
        }
    }

    public void Print(in IDocument document) {
        if (state == IDevice.State.on) {
            _printer.PowerOn();
            _printer.Print(document);
            _printer.PowerOff();
        }
    }

    public void PowerOn() {
        if (state == IDevice.State.off) {
            base.PowerOn();
        }
    }

    public void PowerOff() {
        if (state == IDevice.State.on) {
            base.PowerOff();
        }
    }

    public void Send() {
        if (state == IDevice.State.on) {
            SendCounter++;
            IDocument document;
            Scan(out document);
            Console.WriteLine($"Send: {document.GetFileName()}");
        }
    }

    public void Receive(in IDocument document) {
        if (state == IDevice.State.on) {
            ReceiveCounter++;
            Console.WriteLine($"Receive: {document.GetFileName()}");
            Print(document);
        }
    }
}