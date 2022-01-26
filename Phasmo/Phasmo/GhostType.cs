using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phasmo
{
    class GhostType
    {
        private List<string> _Evidence;
        private string _Name;
        private string _Weaknesses;
        private string _Strengths;
        public GhostType(string name, string firstEvidence, string secondEvidence, string thirdEvidence, string weaknesses, string strenghts)
        {
            _Name = name;
            _Evidence = new List<string>() { firstEvidence, secondEvidence, thirdEvidence };
            _Strengths = strenghts;
            _Weaknesses = weaknesses;
        }
        public List<string> Evidence { get { return _Evidence; } set { _Evidence = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Strengths { get { return _Strengths; } set { _Strengths = value; } }
        public string Weaknesses { get { return _Weaknesses; } set {_Weaknesses = value; } }
    }
}
