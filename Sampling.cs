using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        List<int> index = new List<int>();
        List<float> Sampled_list = new List<float>();

        Signal newsignal;

        public override void Run()
        {
            // throw new NotImplementedException();

            if (M != 0 && L == 0)
            {
                newsignal = low_pass_filter(InputSignal);
                Sampling_Down();
                OutputSignal = new Signal(Sampled_list, index, false);
            }
            else if (M == 0 && L != 0)
            {
                Sampling_Up();
                Signal signal_up = new Signal(Sampled_list, index, false);
                OutputSignal = low_pass_filter(signal_up);
            }
            else if (M != 0 && L != 0)
            {
                Sampling_Up();
                Signal signal_up = new Signal(Sampled_list, index, false);
                newsignal = low_pass_filter(signal_up);
                Sampling_Down();
                OutputSignal = new Signal(Sampled_list, index, false);

            }
            else
                Console.WriteLine("Error please enter L/M correctly!");

        }

        public void Sampling_Down()
        {
            Sampled_list = new List<float>();
            index = new List<int>();
            int k = 0;
            for (int i = 0; i < newsignal.Samples.Count; i += M)
            {
                Sampled_list.Add(newsignal.Samples[i]);
                index.Add(k);
                k++;
            }
        }

        public void Sampling_Up()
        {
            int i = 0;
            int k = 0;
            while (i < InputSignal.Samples.Count - 1)
            {
                Sampled_list.Add(InputSignal.Samples[i]);
                i++;
                index.Add(k);
                for (int j = 1; j <= (L - 1); j++)
                {
                    Sampled_list.Add(0);
                    k++;
                    index.Add(k);
                }
                k++;
            }
            Sampled_list.Add(InputSignal.Samples[InputSignal.Samples.Count - 1]);
            index.Add(k);
        }

        public Signal low_pass_filter(Signal signal)
        {
            FIR filter = new FIR();
            filter.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
            filter.InputFS = 8000;
            filter.InputStopBandAttenuation = 50;
            filter.InputCutOffFrequency = 1500;
            filter.InputTransitionBand = 500;
            filter.InputTimeDomainSignal = signal;
            filter.Run();
            return filter.OutputYn;
        }

    }

}

