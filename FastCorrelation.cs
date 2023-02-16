using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            OutputNonNormalizedCorrelation = new List<float>();
            OutputNormalizedCorrelation = new List<float>();
            Signal out_signal = new Signal(new List<float>(), false);
            float sum_Non;
            float sum1 = 0;
            float sum2 = 0;

            List<float> amp = new List<float>();
            List<float> phase = new List<float>();
            List<float> ferq = new List<float>();

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

            List<float> correlation = new List<float>();

            for (int i = 0; i < InputSignal1.Samples.Count; i++)
            {
                sum1 += InputSignal1.Samples[i] * InputSignal1.Samples[i];
                sum2 += out_signal.Samples[i] * out_signal.Samples[i];
            }

            sum_Non = (float)(Math.Sqrt(sum1 * sum2) / InputSignal1.Samples.Count);

            DiscreteFourierTransform dft1 = new DiscreteFourierTransform();
            DiscreteFourierTransform dft2 = new DiscreteFourierTransform();

            dft1.InputTimeDomainSignal = InputSignal1;
            dft1.Run();
            dft2.InputTimeDomainSignal = out_signal;
            dft2.Run();

            for (int i = 0; i < dft1.OutputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)
            {
                Complex frist = new Complex(dft1.OutputFreqDomainSignal.FrequenciesAmplitudes[i] * Math.Cos(dft1.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]), -1 * dft1.OutputFreqDomainSignal.FrequenciesAmplitudes[i] * Math.Sin(dft1.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]));
                Complex seconed = new Complex(dft2.OutputFreqDomainSignal.FrequenciesAmplitudes[i] * Math.Cos(dft2.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]), dft2.OutputFreqDomainSignal.FrequenciesAmplitudes[i] * Math.Sin(dft2.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]));
                Complex sumcomp = new Complex(0, 0);
                sumcomp = frist * seconed;
                amp.Add((float)sumcomp.Magnitude);
                phase.Add((float)sumcomp.Phase);
                ferq.Add((float)((2 * Math.PI / (dft1.OutputFreqDomainSignal.FrequenciesAmplitudes.Count * (1 / dft1.InputSamplingFrequency))) * i));
            }

            InverseDiscreteFourierTransform idft = new InverseDiscreteFourierTransform();
            idft.InputFreqDomainSignal = new Signal(false, ferq, amp, phase);
            idft.Run();

            for (int i = 0; i < InputSignal1.Samples.Count; i++)
                correlation.Add((float)(idft.OutputTimeDomainSignal.Samples[i] / InputSignal1.Samples.Count));

            OutputNonNormalizedCorrelation = correlation;

            for (int i = 0; i < correlation.Count; i++)
                OutputNormalizedCorrelation.Add(correlation[i] / sum_Non);
        }



    }
}