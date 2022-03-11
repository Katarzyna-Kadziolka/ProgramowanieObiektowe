using Zadanie1.Documents;

namespace Zadanie1.Devices; 

public interface IScanner : IDevice {
    // dokument jest skanowany, jeśli urządzenie włączone
    // w przeciwnym przypadku nic się dzieje
    void Scan(out IDocument document, IDocument.FormatType formatType);
}