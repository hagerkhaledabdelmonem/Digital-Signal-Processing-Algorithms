using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class TimeDelay : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public float InputSamplingPeriod { get; set; }
        public float OutputTimeDelay { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();

            List<float> correlation = new List<float>();

            DirectCorrelation corr = new DirectCorrelation();
            corr.InputSignal1 = InputSignal1;
            corr.InputSignal2 = InputSignal2;
            corr.Run();
            correlation = corr.OutputNonNormalizedCorrelation;
            float max_abs_num = Math.Abs(correlation.Max());
            int max_num_index = correlation.IndexOf(max_abs_num);
            OutputTimeDelay = max_num_index * InputSamplingPeriod;

        }
    }
}
