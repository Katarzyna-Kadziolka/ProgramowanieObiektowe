using Zadanie1.Documents;

namespace Zadanie1.Devices; 

public class Copier: IPrinter, IScanner {
    public IDevice.State GetState() {
        throw new NotImplementedException();
    }

    public int Counter { get; set; }
    public int PrintCounter { get; set; }
    public int ScanCounter { get; set; }
    public void Scan(out IDocument document, IDocument.FormatType formatType) {
        Counter++;
        ScanCounter++;
        document = null;
    }

    public void Print(in IDocument document) {
        Counter++;
        PrintCounter++;
    }

    public void ScanAndPrint() {
        Counter++;
        ScanCounter++;
        PrintCounter++;
    }
    
    public void PowerOn() {
        throw new NotImplementedException();
    }

    public void PowerOff() {
        throw new NotImplementedException();
    }
    
}