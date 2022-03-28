using Zadanie3.Documents;

namespace Zadanie3.Devices; 

public class Printer: BaseDevice, IPrinter {
    public int PrintCounter { get; set; }
    public int Counter { get; set; }
    public void Print(in IDocument document) {
        if (state == IDevice.State.on) {
            Console.WriteLine($"{DateOnly.FromDateTime(DateTime.Now)} {TimeOnly.FromDateTime(DateTime.Now)} Print: {document.GetFileName()}" );
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