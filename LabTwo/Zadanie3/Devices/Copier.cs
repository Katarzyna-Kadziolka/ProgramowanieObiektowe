using Zadanie3.Documents;

namespace Zadanie3.Devices; 

public class Copier: BaseDevice {
    public Printer Printer { get; set; }
    public Scanner Scanner { get; set; }

    public Copier() {
        Printer = new Printer();
        Scanner = new Scanner();
    }
    public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG) {
        if (state == IDevice.State.on) {
            Scanner.PowerOn();
            Scanner.Scan(out document, formatType);
            Scanner.PowerOff();
        }
        else {
            document = null;
        }
    }

    public void Print(in IDocument document) {
        if (state == IDevice.State.on) {
            Printer.PowerOn();
            Printer.Print(document);
            Printer.PowerOff();
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