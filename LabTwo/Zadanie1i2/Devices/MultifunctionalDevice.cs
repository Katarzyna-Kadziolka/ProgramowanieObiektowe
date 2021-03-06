using Zadanie1.Documents;

namespace Zadanie1.Devices; 

public class MultifunctionalDevice: BaseDevice, IFax {
    
    public int Counter { get; set; }
    public int PrintCounter { get; set; }
    public int ScanCounter { get; set; }
    public int SendCounter { get; set; }
    public int ReceiveCounter { get; set; }
    
    
    public void Print(in IDocument document) {
        if (state == IDevice.State.on) {
            PrintCounter++;
            Console.WriteLine($"{DateOnly.FromDateTime(DateTime.Now)} {TimeOnly.FromDateTime(DateTime.Now)} Print: {document.GetFileName()}" );
        }
    }

    public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG) {
        if (state == IDevice.State.on) {
            ScanCounter++;
            switch (formatType) {
                case IDocument.FormatType.TXT:
                    document = new TextDocument($"TextScan{ScanCounter}.txt");
                    break;
                case IDocument.FormatType.PDF:
                    document = new PDFDocument($"PDFScan{ScanCounter}.pdf");
                    break;
                case IDocument.FormatType.JPG:
                    document = new ImageDocument($"ImageScan{ScanCounter}.jpg");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(formatType), formatType, null);
            }
            Console.WriteLine($"{DateOnly.FromDateTime(DateTime.Now)} {TimeOnly.FromDateTime(DateTime.Now)} Scan: {document.GetFileName()}" );
        }
        else {
            document = null;
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
    
    public void PowerOn() {
        if (state == IDevice.State.off) {
            Counter++;
            base.PowerOn();
        }
    }

    public void PowerOff() {
        if (state == IDevice.State.on) {
            base.PowerOff();
        }
    }
}