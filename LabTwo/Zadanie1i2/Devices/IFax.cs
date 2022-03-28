using Zadanie1.Documents;

namespace Zadanie1.Devices; 

public interface IFax : IPrinter, IScanner {
    void Send();
    void Receive(in IDocument document);
}