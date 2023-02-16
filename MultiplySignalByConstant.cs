using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MultiplySignalByConstant : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputConstant { get; set; }
        public Signal OutputMultipliedSignal { get; set; }

        public override void Run()
        {
             List<float>out_sig=new List<float>();
            for(int i = 0; i < InputSignal.Samples.Count; i++)
            {
            
                float MUP=InputSignal.Samples[i]*InputConstant;
                out_sig.Add(MUP);
            }
            OutputMultipliedSignal = new Signal(out_sig,false);
        }
    }
}
