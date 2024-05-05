using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiarism_Validation
{
    public class Edge
    {
        public int lineMatches;
        public int firstSimilarity;
        public int secondSimilarity;
        public Node Source;
        public Node Destination;
        public int rowNumber;
        public int maxSimilarity;

        public Edge(int lineMatches, int firstSimilarity, int secondSimilarity, Node Source, Node Destination,int rowNumber)
        {
            this.Source = Source;
            this.Destination = Destination;
            this.secondSimilarity = secondSimilarity;
            this.firstSimilarity = firstSimilarity;
            this.lineMatches = lineMatches;
            this.rowNumber = rowNumber;
            maxSimilarity = Math.Max(firstSimilarity, secondSimilarity);
        }

    }
}
