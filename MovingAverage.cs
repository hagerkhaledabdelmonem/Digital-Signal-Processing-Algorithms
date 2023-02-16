using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            float sum = 0;
            List<float> out_sig = new List<float>();

            for (int k = 0; k < InputSignal.Samples.Count() - (InputWindowSize - 1); k++)
            {
                sum = 0;
                for (int i = 0; i < InputWindowSize; i++)
                    sum += InputSignal.Samples[k + i];

                out_sig.Add(sum / InputWindowSize);
            }

            OutputAverageSignal = new Signal(out_sig, false);
        }
    }
}
