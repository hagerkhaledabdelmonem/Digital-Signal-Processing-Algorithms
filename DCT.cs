using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DCT: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        public float sqrt;
        public override void Run()
        {
            //throw new NotImplementedException();
            List<float> output = new List<float>();
            for (int k = 0; k < InputSignal.Samples.Count; k++)
            {
                double sum = 0;
                for (int n = 0; n < InputSignal.Samples.Count; n++)
                {
                    sum +=InputSignal.Samples[n]* Math.Cos((Math.PI / (4 * InputSignal.Samples.Count))* (2 * n - 1) * (2 * k- 1));
                }

                output.Add((float)(sum * Math.Sqrt(2.0 / InputSignal.Samples.Count())));
                 
            }
            OutputSignal = new Signal(output, false);
        }
    }
}
