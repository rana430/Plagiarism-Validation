/*using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plagiarism_Validation
{
    public class Prims
    {
        public List<Node> nodes = new List<Node>();
        private Dictionary<int, Node> nodeLookup = new Dictionary<int, Node>();

        public void AddNode(Node node)
        {
            nodes.Add(node);
            nodeLookup[node.id] = node;
        }

        public (List<Edge>, int) GetMST(List<Edge> edges)
        {
            if (nodes.Count == 0 || edges.Count == 0)
                return (new List<Edge>(), 0);

            var mstEdges = new List<Edge>();
            int totalCost = 0;

            // Use PriorityQueue with Edge as the element and int as the priority
            var edgeQueue = new PriorityQueue<Edge, int>();

            int startNodeId = nodes[0].id;
            var inMST = new HashSet<int> { startNodeId };

            AddEdgesToQueue(startNodeId, edges, edgeQueue, inMST);

            while (edgeQueue.Count > 0)
            {
                // Dequeue the edge with the highest priority (maxSimilarity)
                edgeQueue.TryDequeue(out var edge, out var priority);

                int nodeId = inMST.Contains(edge.Source.id) ? edge.Destination.id : edge.Source.id;

                if (inMST.Contains(nodeId))
                    continue;

                mstEdges.Add(edge);
                totalCost += edge.maxSimilarity;
                inMST.Add(nodeId);

                AddEdgesToQueue(nodeId, edges, edgeQueue, inMST);
            }

            return (mstEdges, totalCost);
        }

        private void AddEdgesToQueue(int nodeId, List<Edge> edges, PriorityQueue<Edge, int> edgeQueue, HashSet<int> inMST)
        {
            foreach (var edge in edges.Where(e => e.Source.id == nodeId || e.Destination.id == nodeId))
            {
                if (!inMST.Contains(edge.Destination.id) || !inMST.Contains(edge.Source.id))
                    edgeQueue.Enqueue(edge, edge.maxSimilarity);
            }
        }
    }
}
*/