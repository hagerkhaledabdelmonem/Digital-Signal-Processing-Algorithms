using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }

        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {

            float sum_Non;
            float sum1 = 0;
            float sum2 = 0;

            Signal out_signal = new Signal(new List<float>(), false);

            List<float> correlation = new List<float>();

            OutputNonNormalizedCorrelation = new List<float>();
            OutputNormalizedCorrelation = new List<float>();


            if (InputSignal2 == null)
            {
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    out_signal.Samples.Add(InputSignal1.Samples[i]);
                }
            }
            else
            {
                for (int i = 0; i < InputSignal2.Samples.Count; i++)
                {
                    out_signal.Samples.Add(InputSignal2.Samples[i]);
                }
            }

            for (int i = 0; i < InputSignal1.Samples.Count; i++)
            {
                sum1 += (float)Math.Pow(InputSignal1.Samples[i], 2);
                sum2 += (float)Math.Pow(out_signal.Samples[i], 2);
            }
            sum_Non = (float)Math.Sqrt(sum1 * sum2) / InputSignal1.Samples.Count;

            for (int i = 0; i < out_signal.Samples.Count; i++)
            {
                float sum = 0;
                if (i > 0)
                {
                    float first_element;
                    if (InputSignal1.Periodic == false)

                        first_element = 0;
                    else
                        first_element = out_signal.Samples[0];

                    for (int k = 0; k < out_signal.Samples.Count - 1; k++)
                    {
                        out_signal.Samples[k] = out_signal.Samples[k + 1];
                    }
                    out_signal.Samples[out_signal.Samples.Count - 1] = first_element;


                    for (int j = 0; j < out_signal.Samples.Count; j++)
                    {
                        sum += InputSignal1.Samples[j] * out_signal.Samples[j];
                    }
                }
                else
                {
                    for (int j = 0; j < out_signal.Samples.Count; j++)
                        sum += InputSignal1.Samples[j] * out_signal.Samples[j];
                }
                correlation.Add((float)sum / out_signal.Samples.Count);
            }
            OutputNonNormalizedCorrelation = correlation;

            for (int i = 0; i < OutputNonNormalizedCorrelation.Count; i++)
                OutputNormalizedCorrelation.Add(OutputNonNormalizedCorrelation[i] / sum_Non);

        }
    }
}