using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class AccumulationSum : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            // throw new NotImplementedException();
            float sum = 0;
            List<float> out_sig = new List<float>();

            for (int i=0; i < InputSignal.Samples.Count; i++)
            {
                sum +=InputSignal.Samples[i];
                out_sig.Add(sum);
            }
            OutputSignal = new Signal(out_sig, false);

        }
    }
}
