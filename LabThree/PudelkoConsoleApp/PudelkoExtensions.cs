using LabThree;

namespace PudelkoConsoleApp; 

public static class PudelkoExtensions {
    public static Pudelko Compress(this Pudelko box) {
        var a = Math.Pow((double) box.Objetosc, (1 / 3));
        var param = Convert.ToDecimal(a);
        return new Pudelko(param, param, param);
    }
}