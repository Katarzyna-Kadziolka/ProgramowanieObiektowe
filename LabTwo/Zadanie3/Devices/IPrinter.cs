using Zadanie3.Documents;

namespace Zadanie3.Devices; 

public interface IPrinter : IDevice {
    /// <summary>
    /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
    /// </summary>
    /// <param name="document">obiekt typu IDocument, różny od `null`</param>
    void Print(in IDocument document);
}