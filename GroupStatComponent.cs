using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiarism_Validation
{
    public class GroupStatComponent
    {
        public List<int> edges;
        public float avgSimSum = 0f;
        public int edgeCount = 0;

        public GroupStatComponent(List<int> Edges, float AvgSimSum, int EdgeNumber)
        {
            edges = Edges;
            avgSimSum = AvgSimSum;
            edgeCount = EdgeNumber;
        }
    }
}
