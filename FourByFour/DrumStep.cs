using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourByFour
{
    public enum SubSteps : int
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Six = 6,
        Flam
    }

    public class DrumStep
    {
        int _prob;
        public int Probability
        {
            get => _prob;
           
            set
            {
                _prob = value;
                if (_prob < 0) _prob = 0;
                if (_prob > 100) _prob = 100;            
            }
        }
        public SubSteps SubSteps { get; set; }

        public DrumStep()
        {
            SubSteps = SubSteps.One;
        }

        public DrumStep(int prob, SubSteps subSteps)
        {
            _prob = prob;
            SubSteps = subSteps;
        }

        public override string ToString()
        {
            if (Probability == 0) 
                return "-";
            else 
                return $"{SubSteps}{_prob/100f:0.0}";
        }

        //public void Check(bool check)
        //{
        //    _prob = check ? 100 : 0;
        //    SubSteps = SubSteps.One;
        //}
    }
}
