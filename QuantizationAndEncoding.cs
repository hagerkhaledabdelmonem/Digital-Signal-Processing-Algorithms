using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }
        

        public override void Run()
        {
            OutputEncodedSignal = new List<string>();
            OutputIntervalIndices = new List<int>();
            OutputQuantizedSignal = new Signal(new List<float>(), false);
            OutputSamplesError = new List<float>();

            if (InputLevel == 0)
                InputLevel = ((int)Math.Pow(2, InputNumBits));
            else
                InputNumBits = ((int)Math.Log(InputLevel, 2));


            float maxAmp = InputSignal.Samples.Max();
            float minAmp = InputSignal.Samples.Min();

            float delta = (maxAmp - minAmp) / InputLevel;

            float[] levelsinterval = new float[InputLevel + 1];
            for (int i = 0; i < levelsinterval.Length; i++)
            {
                levelsinterval[i] = minAmp + delta * i;
            }

            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                int index;

                for (index = 0; index < levelsinterval.Length - 2; index++)
                {
                    if (InputSignal.Samples[i] >= levelsinterval[index] && InputSignal.Samples[i] <= levelsinterval[index + 1])
                        break;
                }
                
                OutputIntervalIndices.Add(index + 1);
                string binary = Convert.ToString(OutputIntervalIndices[i] - 1, 2).PadLeft(InputNumBits, '0');
                OutputEncodedSignal.Add(binary);

                int midpointIndex = OutputIntervalIndices[i] - 1;
                float mid_value = (levelsinterval[midpointIndex] + levelsinterval[midpointIndex + 1]) / 2;
                OutputQuantizedSignal.Samples.Add(mid_value);

                OutputSamplesError.Add(OutputQuantizedSignal.Samples[i] - InputSignal.Samples[i]);
            }
        }


    }
}

