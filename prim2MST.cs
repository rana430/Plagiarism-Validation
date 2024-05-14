using System;
using System.Collections.Generic;
using System.Linq;

public class prim_MST
{
    public List<Node> nodes = new List<Node>();
    public List<Edge> edges = new List<Edge>();
    private Dictionary<int, Node> nodeLookup = new Dictionary<int, Node>();


    public prim_MST(List)
    public void AddNode(Node node)
    {
        nodes.Add(node);
        nodeLookup[node.Id] = node;
    }

    public void AddEdge(Edge edge)
    {
        edges.Add(edge);
    }

    public (List<Edge>, int) GetMST()
    {
        if (nodes.Count == 0)
            return (new List<Edge>(), 0);

        var mstEdges = new List<Edge>();
        int totalCost = 0;
        var edgeQueue = new SortedSet<Tuple<int, Edge>>(Comparer<Tuple<int, Edge>>.Create((a, b) =>
        {
            int compare = b.Item1.CompareTo(a.Item1);  // Higher weights come first
            if (compare == 0)  // If weights are the same, prioritize by line matches
                compare = b.Item2.LineMatches.CompareTo(a.Item2.LineMatches);
            return compare;
        }));

        int startNodeId = nodes[0].Id;
        var inMST = new HashSet<int> { startNodeId };

        AddEdgesToQueue(startNodeId, edgeQueue, inMST);

        while (edgeQueue.Count > 0)
        {
            var currentEdgeTuple = edgeQueue.First();
            edgeQueue.Remove(currentEdgeTuple);
            Edge edge = currentEdgeTuple.Item2;

            if (inMST.Contains(edge.Destination.Id))
                continue;

            mstEdges.Add(edge);
            totalCost += edge.Weight;
            inMST.Add(edge.Destination.Id);

            AddEdgesToQueue(edge.Destination.Id, edgeQueue, inMST);
        }

        return (mstEdges, totalCost);
    }

    private void AddEdgesToQueue(int nodeId, SortedSet<Tuple<int, Edge>> edgeQueue, HashSet<int> inMST)
    {
        foreach (var edge in edges.Where(e => e.Source.Id == nodeId || e.Destination.Id == nodeId))
        {
            if (!inMST.Contains(edge.Destination.Id))
                edgeQueue.Add(new Tuple<int, Edge>(edge.Weight, edge));
        }
    }
}
}
