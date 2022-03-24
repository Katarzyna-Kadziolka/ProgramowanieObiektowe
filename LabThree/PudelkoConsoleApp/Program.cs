using System.Runtime.CompilerServices;
using LabThree;

var boxes = new List<Pudelko> {
    new(),
    new (1.0m, 2.543m, 3.1m),
    new (2.0m, 3.53m, 8m, UnitOfMeasure.Meter),
    new (100.0m, 25.58m, 3.13m, UnitOfMeasure.Centimeter),
    new (2800m, 500m, 385.15m, UnitOfMeasure.Milimeter),
    new (1.001m, 2.599m),
    new (1.001m),
    new (a: 2800m, b: 500m, unit: UnitOfMeasure.Milimeter),
};

foreach (var box in boxes) {
    Console.WriteLine(box.ToString());
}

boxes.Sort((p1, p2) => {
    if (p1.Objetosc > p2.Objetosc) {
        return 1;
    }
    if (p1.Objetosc < p2.Objetosc) {
        return -1;
    }

    if (p1.Pole > p2.Pole) {
        return 1;
    }
    if (p1.Pole < p2.Pole) {
        return -1;
    }

    if (p1.Sum() > p2.Sum()) {
        return 1;
    }
    if (p1.Sum() < p2.Sum()) {
        return -1;
    }

    return 0;
});

Console.WriteLine("Sorted boxes:");
foreach (var box in boxes) {
    Console.WriteLine(box.ToString());
}