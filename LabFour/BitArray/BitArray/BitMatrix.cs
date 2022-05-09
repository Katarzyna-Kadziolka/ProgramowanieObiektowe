using System.Collections;

namespace BitArray; 

public class BitMatrix: IEnumerable<bool> {
    private System.Collections.BitArray[] data;
    public int Dimension => data.Length; 
    public BitMatrix(int n) {
        data = new System.Collections.BitArray [n];
        for (int i = 0; i < n; i++) {
            data[i] = new System.Collections.BitArray(n);
        }
    }

    public bool this[int i, int j] {
        get => data[i][j];
        set => data[i][j] = value;
    }

    public override string ToString() {
        string result = "";
        foreach (var row in data) {
            foreach (bool cell in row) {
                result += cell + " ";
            }

            result += "\n";
        }

        return result;
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
    
    public IEnumerator<bool> GetEnumerator() {
        foreach (var row in data) {
            foreach (bool cell in row) {
                yield return cell;
            }
        }
    }

    //public BitMatrix And(BitMatrix other) {
     //   foreach (var row in other) {
     //       foreach (bool cell in row) {
     //           throw new NotImplementedException();
    //        }
    //    }
    //}
    
    public BitMatrix Or(BitMatrix other) {
        throw new NotImplementedException();
    }
    
    public BitMatrix Not(BitMatrix other) {
        throw new NotImplementedException();
    }

    public static BitMatrix operator &(in BitMatrix a, in BitMatrix b) {
        if (a.Dimension != b.Dimension) {
            throw new ArgumentException();
        }

        return new BitMatrix(a.Dimension);
    }
}