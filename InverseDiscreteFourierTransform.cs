using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            float values = 0;
            if (InputFreqDomainSignal.FrequenciesPhaseShifts.Count == 0)
            {
                values = (1f / InputFreqDomainSignal.Samples.Count);

            }
            else
            {
                values = (1f / InputFreqDomainSignal.FrequenciesPhaseShifts.Count);
            }

            List<float> outSignal = new List<float>();
            for (int n = 0; n < InputFreqDomainSignal.FrequenciesAmplitudes.Count; n++)
            {
                Complex c1 = new Complex(0, 0);
                for (int k = 0; k < InputFreqDomainSignal.FrequenciesAmplitudes.Count(); k++)
                {
                    Complex c = new Complex(0, 0);
                    float phase = (float)(2 * Math.PI * k * n) / InputFreqDomainSignal.FrequenciesAmplitudes.Count;
                    c = new Complex(InputFreqDomainSignal.FrequenciesAmplitudes[k] * Math.Cos(InputFreqDomainSignal.FrequenciesPhaseShifts[k]), InputFreqDomainSignal.FrequenciesAmplitudes[k] * Math.Sin(InputFreqDomainSignal.FrequenciesPhaseShifts[k]));
                    c1 += values * c * new Complex(Math.Cos(phase), Math.Sin(phase));
                }
                outSignal.Add((float)c1.Real);
            }
            OutputTimeDomainSignal = new Signal(outSignal, false);
        }
    }
}
