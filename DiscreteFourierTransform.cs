using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            List<float> amp = new List<float>();
            List<float> phase = new List<float>();
            List<float> freq = new List<float>();

            double Power_of_e;
            double real;
            double imag;

            for (int k = 0; k < InputTimeDomainSignal.Samples.Count; k++)
            {
                real = 0;
                imag = 0;

                for (int n = 0; n < InputTimeDomainSignal.Samples.Count; n++)
                {
                    Power_of_e = (2 * Math.PI * k * n) / InputTimeDomainSignal.Samples.Count;
                    real += InputTimeDomainSignal.Samples[n] * Math.Cos(Power_of_e);
                    imag += -InputTimeDomainSignal.Samples[n] * Math.Sin(Power_of_e);
                }
                double Amplitudes = Math.Sqrt(Math.Pow(real, 2) + Math.Pow(imag, 2));
                amp.Add((float)Amplitudes);

                float PhaseShifts = (float)Math.Atan2(imag, real);
                phase.Add(PhaseShifts);

                freq.Add((float)Math.Round(((2 * Math.PI / (InputTimeDomainSignal.Samples.Count * (1 / InputSamplingFrequency))) * k), 1));

            }

            OutputFreqDomainSignal = new Signal(InputTimeDomainSignal.Samples, false);
            OutputFreqDomainSignal.Frequencies = freq;
            OutputFreqDomainSignal.FrequenciesAmplitudes = amp;
            OutputFreqDomainSignal.FrequenciesPhaseShifts = phase;
        }
    }
}
