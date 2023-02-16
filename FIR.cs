using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }    // hd * w
        public Signal OutputYn { get; set; }

        float N;
        float Hd;
        float W = 0;

        float newfc;
        float newf1;
        float newf2;

        public override void Run()
        {
            //throw new NotImplementedException();

            int win = 0;

            List<float> samples = new List<float>();
            List<int> index = new List<int>();

            if (InputStopBandAttenuation <= 21)                                  // Rectangular
            {
                N = (float)(0.9 / (InputTransitionBand / InputFS));
                win = 1;
            }
            else if (InputStopBandAttenuation <= 44)                           // Hanning 
            {
                N = (float)(3.1 / (InputTransitionBand / InputFS));
                win = 2;
            }
            else if (InputStopBandAttenuation <= 53)                           // Hamming 
            {
                N = (float)(3.3 / (InputTransitionBand / InputFS));
                win = 3;
            }
            else if (InputStopBandAttenuation <= 74)                          // Blackman 
            {
                N = (float)(5.5 / (InputTransitionBand / InputFS));
                win = 4;
            }

            if (N % 2 == 0)
                N = (float)Math.Ceiling(N) + 1;
            else
                N = (float)Math.Ceiling(N);

            int n = (int)(N - 1) / 2;

            for (int i = -n; i <= n; i++)
            {
                Hd = filter(i);
                W = window(win, i);
                samples.Add(Hd * W);
                index.Add(i);
            }

            OutputHn = new Signal(samples, index, false);

            DirectConvolution DirCon = new DirectConvolution();
            DirCon.InputSignal1 = InputTimeDomainSignal;
            DirCon.InputSignal2 = OutputHn;
            DirCon.Run();
            OutputYn = DirCon.OutputConvolvedSignal;

        }

        public float filter(int i)
        {
            if (InputFilterType == FILTER_TYPES.LOW)
            {
                newfc = (float)(InputCutOffFrequency + InputTransitionBand / 2) / InputFS;
                if (i == 0)
                    Hd = (float)2 * newfc;
                else
                    Hd = (float)(2 * newfc * (Math.Sin(i * 2 * Math.PI * newfc) / (i * 2 * Math.PI * newfc)));
            }

            else if (InputFilterType == FILTER_TYPES.HIGH)
            {
                newfc = (float)(InputCutOffFrequency - InputTransitionBand / 2) / InputFS;
                if (i == 0)
                    Hd = (float)(1 - 2 * newfc);
                else
                    Hd = (float)(-2 * newfc * (Math.Sin(i * 2 * Math.PI * newfc) / (i * 2 * Math.PI * newfc)));
            }

            else if (InputFilterType == FILTER_TYPES.BAND_PASS)
            {
                newf1 = (float)(InputF1 - InputTransitionBand / 2) / InputFS;
                newf2 = (float)(InputF2 + InputTransitionBand / 2) / InputFS;
                if (i == 0)
                    Hd = (float)2 * (newf2 - newf1);
                else
                    Hd = (float)((2 * newf2 * (Math.Sin(i * 2 * Math.PI * newf2) / (i * 2 * Math.PI * newf2))) - (2 * newf1 * (Math.Sin(i * 2 * Math.PI * newf1) / (i * 2 * Math.PI * newf1))));
            }

            else if (InputFilterType == FILTER_TYPES.BAND_STOP)
            {
                newf1 = (float)(InputF1 + InputTransitionBand / 2) / InputFS;
                newf2 = (float)(InputF2 - InputTransitionBand / 2) / InputFS;
                if (i == 0)
                    Hd = (float)1 - (2 * (newf2 - newf1));
                else
                    Hd = (float)((2 * newf1 * (Math.Sin(i * 2 * Math.PI * newf1) / (i * 2 * Math.PI * newf1))) - (2 * newf2 * (Math.Sin(i * 2 * Math.PI * newf2) / (i * 2 * Math.PI * newf2))));
            }

            return Hd;
        }

        public float window(int win, int i)
        {
            if (win == 1)                                                          // Rectangular
            {
                W = 1;
            }
            else if (win == 2)                                                    // Hanning
            {
                W = (float)(0.5 + 0.5 * Math.Cos(2 * Math.PI * i / N));
            }
            else if (win == 3)                                                    // Hamming
            {
                W = (float)(0.54 + 0.46 * Math.Cos(2 * Math.PI * i / N));
            }
            else if (win == 4)                                                   // Blackman
            {
                W = (float)(0.42 + 0.5 * Math.Cos(2 * Math.PI * i / (N - 1)) + 0.08 * Math.Cos(4 * Math.PI * i / (N - 1)));
            }
            return W;
        }
    }
}
