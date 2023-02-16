using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }
        public override void Run()
        {
            // throw new NotImplementedException();

            List<float> output_signal = new List<float>();
            List<int> indeces = new List<int>();
          
                for (int i = InputSignal.Samples.Count - 1; i >= 0; i--)
                {
                    output_signal.Add(InputSignal.Samples[i]);
                    indeces.Add(InputSignal.SamplesIndices[i] * -1);
                }
                OutputFoldedSignal = new Signal(output_signal, indeces, !InputSignal.Periodic);
           

            //if (InputSignal.Periodic == true)
            //{
            //    OutputFoldedSignal.Periodic = false;
            //}
            //else
            //{
            //    OutputFoldedSignal.Periodic = true;
            //}

        }
    }
}
