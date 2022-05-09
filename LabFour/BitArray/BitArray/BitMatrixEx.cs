using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace BitArray;

public class BitMatrixEx : IEquatable<BitMatrixEx>, IEnumerable<int>, ICloneable {
    private System.Collections.BitArray data;
    public int NumberOfRows { get; }
    public int NumberOfColumns { get; }
    public bool IsReadOnly => false;

    // tworzy prostokątną macierz bitową wypełnioną `defaultValue`
    public BitMatrixEx(int numberOfRows, int numberOfColumns, int defaultValue = 0) {
        if (numberOfRows < 1 || numberOfColumns < 1)
            throw new ArgumentOutOfRangeException("Incorrect size of matrix");
        data = new System.Collections.BitArray(numberOfRows * numberOfColumns, BitToBool(defaultValue));
        NumberOfRows = numberOfRows;
        NumberOfColumns = numberOfColumns;
    }

    public BitMatrixEx(int numberOfRows, int numberOfColumns, params int[] bits) {
        NumberOfRows = numberOfRows;
        NumberOfColumns = numberOfColumns;
        if (bits == null || bits.Length == 0) {
            data = new System.Collections.BitArray(numberOfRows * numberOfColumns, BitToBool(0));
        }
        else {
            data = new System.Collections.BitArray(numberOfRows * numberOfColumns, BitToBool(0));
            for (int i = 0; i < bits.Length; i++) {
                if (data.Length >= i + 1) {
                    if (bits[i] == 0) {
                        data[i] = false;
                    }
                    else {
                        data[i] = true;
                    }
                }
            }
        }
    }

    public BitMatrixEx(int[,] bits) {
        if (bits == null) {
            throw new NullReferenceException();
        }

        if (bits.Length == 0) {
            throw new ArgumentOutOfRangeException();
        }
        
        NumberOfRows = bits.GetLength(0);
        NumberOfColumns = bits.GetLength(1);
        data = new System.Collections.BitArray(NumberOfRows * NumberOfColumns, BitToBool(0));
        var index = 0;
        for (int i = 0; i < bits.GetLength(0); i++) {
            for (int j = 0; j < bits.GetLength(1); j++) {
                if (data.Length >= bits.Length) {
                    if (bits[i, j] == 0) {
                        data[index] = false;
                    }
                    else {
                        data[index] = true;
                    }
                }

                index++;
            }
        }
    }

    public BitMatrixEx(bool[,] bits) {
        if (bits == null) {
            throw new NullReferenceException();
        }

        if (bits.Length == 0) {
            throw new ArgumentOutOfRangeException();
        }
        
        NumberOfRows = bits.GetLength(0);
        NumberOfColumns = bits.GetLength(1);
        data = new System.Collections.BitArray(NumberOfRows * NumberOfColumns, BitToBool(0));
        var index = 0;
        for (int i = 0; i < bits.GetLength(0); i++) {
            for (int j = 0; j < bits.GetLength(1); j++) {
                if (data.Length >= bits.Length) {
                    data[index] = bits[i, j];
                }

                index++;
            }
        }
    }

    public static int BoolToBit(bool boolValue) => boolValue ? 1 : 0;
    public static bool BitToBool(int bit) => bit != 0;

    public bool Equals(BitMatrixEx other) {
        return Equals((object) other);
    }

    public IEnumerator<int> GetEnumerator() {
        foreach (bool cell in data) {
            yield return BoolToInt(cell);
        }
    }
    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public override bool Equals(object obj) {
        if( obj == null ) return false;
        if( ReferenceEquals(this, obj)) return true;
        if ( !(obj is BitMatrixEx) ) return false;
        if (NumberOfColumns == ((BitMatrixEx)obj).NumberOfColumns
            && NumberOfRows == ((BitMatrixEx)obj).NumberOfRows) {
            for (int i = 0; i < data.Length; i++) {
                if (data[i] != ((BitMatrixEx)obj).data[i]) {
                    return false;
                }
            }

            return true;
        }
        return false;
    }
    
    public override int GetHashCode() {
        return HashCode.Combine(NumberOfColumns, NumberOfRows, data);
    }
    public static bool operator == (BitMatrixEx left, BitMatrixEx right) {
        if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) {
            return true;
        }
        if (ReferenceEquals(left, null) && !ReferenceEquals(right, null)) {
            return false;
        }
        if (!ReferenceEquals(left, null) && ReferenceEquals(right, null)) {
            return false;
        }
        return left.Equals(right);
    }

    public static bool operator != (BitMatrixEx lewy, BitMatrixEx prawy) => !(lewy == prawy);

    public override string ToString() {
        string result = "";
        var columnNum = 0;
        foreach (var element in data) {
            var value = (bool)element;
            if (value == false) {
                result += 0;
            }
            else {
                result += 1;
            }

            columnNum++;
            if (columnNum == NumberOfColumns) {
                result += "\n";
                columnNum = 0;
            }
        }

        return result;
    }

    public object Clone() {
        var newBitMatrix = new BitMatrixEx(NumberOfRows, NumberOfColumns);
        for (int i = 0; i < data.Length; i++) {
            newBitMatrix.data[i] = data[i];
        }
        return newBitMatrix;
    }

    public int this[int i, int j] {
        get => BoolToInt(data[FindIndex(i, j)]);
        set => data[FindIndex(i, j)] = IntToBool(value);
    }

    private bool IntToBool(int value) => value != 0;

    private int BoolToInt(bool value) {
        if (value) return 1;
        return 0;
    }
    public int FindIndex(int i, int j) {
        if (i > NumberOfRows - 1 || j > NumberOfColumns - 1 || i < 0 || j < 0) {
            throw new IndexOutOfRangeException();
        }

        if (i == 0) return j;
        if (j == 0) return i * NumberOfColumns;
        return i * j;
    }


    public static explicit operator BitMatrixEx(int[,] givenValues) {
        if (givenValues == null) throw new NullReferenceException();
        if (givenValues.Length == 0) throw new ArgumentOutOfRangeException();
        return new BitMatrixEx(givenValues);
    }
    public static implicit operator int[,](BitMatrixEx matrix)
    {
        var intArray = new int[matrix.NumberOfRows, matrix.NumberOfColumns];
        for (int i = 0; i < matrix.NumberOfRows; i++)
        {
            for (int j = 0; j < matrix.NumberOfColumns; j++)
            {
                intArray[i, j] = matrix[i, j];
            }
        }
        return intArray;
    }
    
    public static explicit operator BitMatrixEx(bool[,] givenValues)
    {
        if (givenValues == null) throw new NullReferenceException();
        if (givenValues.Length == 0) throw new ArgumentOutOfRangeException();
        return new BitMatrixEx(givenValues);
    }
    

    public static implicit operator bool[,](BitMatrixEx matrix)
    {
        var boolArray = new bool[matrix.NumberOfRows, matrix.NumberOfColumns];
        for (int i = 0; i < matrix.NumberOfRows; i++)
        {
            for (int j = 0; j < matrix.NumberOfColumns; j++)
            {
                boolArray[i, j] = BitToBool(matrix[i, j]);
            }
        }
        return boolArray;
    }

    public static explicit operator System.Collections.BitArray(BitMatrixEx matrix) => new System.Collections.BitArray(matrix.data);

    public BitMatrixEx And(BitMatrixEx other) {
        if (other == null) {
            throw new ArgumentNullException();
        }

        if (NumberOfColumns != other.NumberOfColumns || NumberOfRows != other.NumberOfRows || data.Length != other.data.Length) {
            throw new ArgumentException();
        }

        var newBitMatrix = new BitMatrixEx(NumberOfRows, NumberOfColumns);
        for (int i = 0; i < data.Length; i++) {
            if (data[i] && other.data[i]) {
                newBitMatrix.data[i] = true;
            }
            else {
                newBitMatrix.data[i] = false;
            }
        }
        
        for (int i = 0; i < data.Length; i++) {
            data[i] = newBitMatrix.data[i];
        }

        return this;
    }

    public static BitMatrixEx operator &(in BitMatrixEx a, in BitMatrixEx b) {
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) {
            throw new ArgumentNullException();
        }

        if (a.NumberOfColumns != b.NumberOfColumns || a.NumberOfRows != b.NumberOfRows || a.data.Length != b.data.Length) {
            throw new ArgumentException();
        }

        var newBitMatrix = new BitMatrixEx(a.NumberOfRows, a.NumberOfColumns);
        for (int i = 0; i < a.data.Length; i++) {
            if (a.data[i] && b.data[i]) {
                newBitMatrix.data[i] = true;
            }
            else {
                newBitMatrix.data[i] = false;
            }
        }

        return newBitMatrix;
    }
}