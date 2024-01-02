using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hardware_prediction_expert_system
{
    internal class Rule
    {
        public string Conclusion { get; }
        public List<string> Premises { get; }

        public Rule(string conclusion, List<string> premises)
        {
            Conclusion = conclusion;
            Premises = premises;
        }
    }
}
