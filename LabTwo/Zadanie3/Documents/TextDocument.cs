namespace Zadanie1.Documents; 

public class TextDocument : AbstractDocument {
    public TextDocument(string filename) : base(filename) { }
    public override IDocument.FormatType GetFormatType() => IDocument.FormatType.TXT;
}