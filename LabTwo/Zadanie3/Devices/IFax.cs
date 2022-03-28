using Zadanie3.Documents;

namespace Zadanie3.Devices; 

public interface IFax : IPrinter, IScanner {
    void Send();
    void Receive(in IDocument document);
}