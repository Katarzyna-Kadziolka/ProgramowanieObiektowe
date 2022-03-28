using Zadanie3.Documents;

namespace Zadanie3.Devices; 

public class Scanner: BaseDevice, IScanner {
    public int Counter { get; set; }
    public int ScanCounter { get; set; }
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