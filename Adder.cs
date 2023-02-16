using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float>out_sig=new List<float>();
            for(int i = 0; i < InputSignals[1].Samples.Count; i++)
            {
                float ADD=InputSignals[0].Samples[i]+InputSignals[1].Samples[i];
                out_sig.Add(ADD);
            }
            OutputSignal=new Signal(out_sig,false);
        }
    }
}