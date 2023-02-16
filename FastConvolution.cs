using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
namespace DSPAlgorithms.Algorithms
{
    public class FastConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }
        /// <summary> /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H) /// </summary> 
        public override void Run()
        {
            // throw new NotImplementedException();


            DiscreteFourierTransform dft1 = new DiscreteFourierTransform();
            DiscreteFourierTransform dft2 = new DiscreteFourierTransform();

            List<float> signal1 = new List<float>();
            List<float> signal2 = new List<float>();

            List<float> amp = new List<float>();
            List<float> phase = new List<float>();
            List<float> freq = new List<float>();


            for (int i = 0; i < InputSignal1.Samples.Count; i++)
                signal1.Add(InputSignal1.Samples[i]);
            for (int i = 0; i < InputSignal2.Samples.Count; i++)
                signal2.Add(InputSignal2.Samples[i]);

            int max = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;
            for (int i = signal1.Count; i < max; i++)
                signal1.Add(0);
            for (int i = signal2.Count; i < max; i++)
                signal2.Add(0);

            dft1.InputTimeDomainSignal = new Signal(signal1, false);
            dft1.Run();
            dft2.InputTimeDomainSignal = new Signal(signal2, false);
            dft2.Run();


            for (int i = 0; i < dft1.OutputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)
            {
                Complex frist = new Complex(dft1.OutputFreqDomainSignal.FrequenciesAmplitudes[i] * Math.Cos(dft1.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]), dft1.OutputFreqDomainSignal.FrequenciesAmplitudes[i] * Math.Sin(dft1.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]));
                Complex seconed = new Complex(dft2.OutputFreqDomainSignal.FrequenciesAmplitudes[i] * Math.Cos(dft2.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]), dft2.OutputFreqDomainSignal.FrequenciesAmplitudes[i] * Math.Sin(dft2.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]));
                Complex sumcomp = new Complex(0, 0);
                sumcomp = frist * seconed;
                amp.Add((float)sumcomp.Magnitude);
                phase.Add((float)Math.Atan2(sumcomp.Imaginary, sumcomp.Real));
                freq.Add(i);

            }

            InverseDiscreteFourierTransform idft = new InverseDiscreteFourierTransform();
            idft.InputFreqDomainSignal = new Signal(false, freq, amp, phase);
            //idft.InputFreqDomainSignal.FrequenciesAmplitudes = amp;
            //idft.InputFreqDomainSignal.FrequenciesPhaseShifts = phase;
            idft.Run();
            OutputConvolvedSignal = new Signal(idft.OutputTimeDomainSignal.Samples, false);
        }
    }
}









