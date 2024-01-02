using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hardware_prediction_expert_system
{
    internal class ExpertSystem
    {
        private List<Rule> Rules { get; }
        private List<string> Facts { get; }

        public ExpertSystem()
        {
            Rules = new List<Rule>();
            Facts = new List<string>();
        }

        public void AddRule(string conclusion, List<string> premises)
        {
            Rule rule = new Rule(conclusion, premises);
            Rules.Add(rule);
        }

        public void AddFact(string fact)
        {
            Facts.Add(fact);
        }

        //Thuật toán suy diễn tiến
        public List<string> RunInference()
        {
            bool isUpdated;
            do
            {
                isUpdated = false;
                foreach (Rule rule in Rules)
                {
                    if (!Facts.Contains(rule.Conclusion) && CheckPremises(rule.Premises))
                    {
                        Facts.Add(rule.Conclusion);
                        isUpdated = true;
                    }
                }
            } while (isUpdated);
            List<string> result = this.Facts;
            return result;
        }

        private bool CheckPremises(List<string> premises)
        {
            foreach (string premise in premises)
            {
                if (!Facts.Contains(premise))
                {
                    return false;
                }
            }
            return true;
        }

        public void ClearFacts()
        {
            this.Facts.Clear();
        }
    }
}
