using Zadanie3.Documents;

namespace Zadanie3.Devices; 

public class Scanner: BaseDevice, IScanner {
    public void Scan(out IDocument document, IDocument.FormatType formatType) {
        throw new NotImplementedException();
    }
}