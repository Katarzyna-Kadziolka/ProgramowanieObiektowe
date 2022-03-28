using Zadanie3.Documents;

namespace Zadanie3.Devices; 

public class Printer: BaseDevice, IPrinter {
    public int PrintCounter { get; set; }
    public void Print(in IDocument document) {
        if (state == IDevice.State.on) {
            PrintCounter++;
            Console.WriteLine($"{DateOnly.FromDateTime(DateTime.Now)} {TimeOnly.FromDateTime(DateTime.Now)} Print: {document.GetFileName()}" );
        }
    }
}