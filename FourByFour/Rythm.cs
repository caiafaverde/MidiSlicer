using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourByFour
{
    public class Rythm
    {
        bool[] _rythm;
        int _beats;
        int _length;

        public IEnumerable<bool> Steps => _rythm;

        public static Rythm FromBeatString(string rythm) //like x..x..x
        {
            //accept only x and .
            var result = new Rythm();
            result._beats = rythm.Count(c => c == 'x');
            result._length = rythm.Count(c => c == '.') + result._beats;
            if (result._length < 2)
                return null;

            result._rythm = new bool[result._length];
            int i = 0;
            foreach (var c in rythm)
            {
                if (c == 'x')
                    result._rythm[i++] = true;
            }
            return result;
        }

        public static Rythm FromIntervalString(string rythm) //like 3222
        {
            //accept only x and .
            var result = new Rythm();
            var intervals = rythm.ToCharArray().Select(c => Convert.ToInt32(c - '0')).ToList();

            result._beats = intervals.Count;
            result._length = intervals.Sum();
            if (result._length < 2)
                return null;

            result._rythm = new bool[result._length];
            int i = 0;
            foreach (var c in intervals)
            {
                result._rythm[i] = true;
                i += c;
            }
            return result;
        }

        Rythm() { }

        public static Rythm Euclidean(int beats, int length)
        {
            var result = new Rythm();
            result._rythm = new bool[length];
            result._length = length;
            result._beats = beats;

            //Bjorklund sequence generating algo
            //1 phase- handle zeroes
            List<List<bool>> ones = new List<List<bool>>();
            for (int i = 0; i < beats; i++)
            {
                ones.Add(new List<bool>() { true });
            }
            int zeroes = length - beats;
            while (zeroes != 0)
            {
                foreach (var subOne in ones)
                {
                    if (zeroes == 0)
                        break;
                    zeroes--;
                    subOne.Add(false);
                }
            }
            //2 phase - append shortest intervals
            while (true)
            {
                var lengths = ones.Select(o => o.Count).ToList();
                if (lengths.Count(l => l == lengths[0]) == 4)
                    break;
                if (lengths.Count(l => l == lengths[lengths.Count - 1]) == 1)
                    break;
                int j = lengths.IndexOf(lengths[lengths.Count - 1]);

                for (int i = 0; i < j; i++)
                {
                    if (j == ones.Count)
                        break;
                    var atJ = ones[j];
                    ones.RemoveAt(j);
                    ones[i].AddRange(atJ);
                }
            }

            var rr = new List<bool>();
            foreach (var one in ones)
            {
                rr.AddRange(one);
            }
            result._rythm = rr.ToArray();

            return result;
        }

        public override string ToString()
        {
            return String.Concat(_rythm.Select(r => r ? 'x' : '.'));
        }
    }
}
