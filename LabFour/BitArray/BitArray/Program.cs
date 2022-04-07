
using BitArray;

BitMatrix matrix = new BitMatrix(5);
Console.WriteLine(matrix);
Console.WriteLine(matrix[2, 1]);
matrix[2, 1] = true;
Console.WriteLine(matrix);
foreach (var bit in matrix) {
    Console.WriteLine(bit);
}