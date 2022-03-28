using Zadanie3.Documents;

namespace Zadanie3.Devices; 

public class Copier: BaseDevice {
    private Printer _printer;
    private Scanner _scanner;

    public Copier() {
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

    public void ScanAndPrint() {
        if (state == IDevice.State.on) {
            IDocument doc;
            Scan(out doc, IDocument.FormatType.JPG);
            Print(in doc);
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
}