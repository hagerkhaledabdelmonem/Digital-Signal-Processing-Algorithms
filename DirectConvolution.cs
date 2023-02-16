using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            //throw new NotImplementedException();

            List<float> output = new List<float>();
            List<int> indecies = new List<int>();

            int elements = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;
            int index = InputSignal1.SamplesIndices.Min() + InputSignal2.SamplesIndices.Min();

            for (int n = 0; n < elements; n++)
            {
                float sum = 0;
                for (int k = 0; k < InputSignal1.Samples.Count; k++)
                {
                    if ((n - k) >= 0 && (n - k) < InputSignal2.Samples.Count)
                    {
                        sum += InputSignal1.Samples[k] * InputSignal2.Samples[n - k];
                    }
                }

                if (!(n == elements - 1 && sum == 0.0))
                {
                    output.Add(sum);
                    indecies.Add(index);
                    index++;
                }
                
            }

            OutputConvolvedSignal = new Signal(output, indecies, false);

        }
    }
}
