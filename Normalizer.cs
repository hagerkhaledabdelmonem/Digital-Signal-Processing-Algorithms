using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public override void Run()
        {
            float minRange = InputSignal.Samples.Min();
            float maxRange = InputSignal.Samples.Max();

            //throw new NotImplementedException();
            List<float> output_signal = new List<float>();
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                float res = (InputMaxRange - InputMinRange) * ((InputSignal.Samples[i] - minRange) / (maxRange - minRange)) + InputMinRange;
                output_signal.Add(res);
            }
            OutputNormalizedSignal = new Signal(output_signal, false);
        }
    }
}
