using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
This script contains the definition of graphs, nodes, edges and their
respective methods.
*/

public class Graph {
    public List<Node> nodes;
    public List<Edge> edges;

    public List<Node> Nodes => nodes;
    public List<Edge> Edges => edges;

    public Graph() {
        nodes = new List<Node>();
        edges = new List<Edge>();
    }

    public bool adjacent(Node from, Node to) {
        foreach(Edge e in edges) {
            if (e.from == from && e.to == to)
                return true;
        }
        return false;
    }

    public List<Node> neighbors(Node from) {
        List<Node> result = new List<Node>();

        foreach (Edge e in edges) {
            if (e.from == from)
                result.Add(e.to);
        }
        return result;
    }

    public void addNode(Vector3 worldPosition) {
        nodes.Add(new Node(nodes.Count, worldPosition));
    }

    public void addEdge(Node from, Node to) {
        edges.Add(new Edge(from, to, 1));
    }

    public float distance(Node from, Node to) {
        foreach (Edge e in edges) {
            if (e.from == from && e.to == to)
                return e.getWeight();
        }
        return Mathf.Infinity;
    }

    // get the shortest path between two units
    public List<Node> GetPath(Node start, Node end)
    {
        List<Node> path = new List<Node>();

        // If the start and end are same node, we can return the start node
        if (start == end)
        {
            path.Add(start);
            return path;
        }

        // The list of openList nodes
        List<Node> openList = new List<Node>();

        // Previous nodes in optimal path from source
        Dictionary<Node, Node> previous = new Dictionary<Node, Node>();

        // The calculated distances, set all to Infinity at start, except the start Node
        Dictionary<Node, float> distances = new Dictionary<Node, float>();

        for (int i = 0; i < nodes.Count; i++)
        {
            openList.Add(nodes[i]);

            // Setting the node distance to Infinity
            distances.Add(nodes[i], float.MaxValue);
        }

        // Set the starting Node distance to zero
        distances[start] = 0f;
        while (openList.Count != 0)
        {            
            // Getting the Node with smallest distance
            openList = openList.OrderBy(node => distances[node]).ToList();
            Node current = openList[0];
            openList.Remove(current);

            // When the current node is equal to the end node, then we can break and return the path
            if (current == end)
            {
                // Construct the shortest path
                while (previous.ContainsKey(current))
                {
                    // Insert the node onto the final result
                     path.Insert(0, current);
                    //Traverse from start to end
                    current = previous[current];
                }

                //Insert the source onto the final result
                path.Insert(0, current);
                break;
            }

            // Looping through the Node connections (neighbors) and where the connection (neighbor) is available at openList list
            foreach(Node neighbor in neighbors(current))
            {
                // Getting the distance between the current node and the connection (neighbor)
                float length = Vector3.Distance(current.worldPosition, neighbor.worldPosition);

                // The distance from start node to this connection (neighbor) of current node
                float alt = distances[current] + length;

                // A shorter path to the connection (neighbor) has been found
                if (alt < distances[neighbor])
                {
                    distances[neighbor] = alt;
                    previous[neighbor] = current;
                }
            }
        }
        return path;
    }
}

public class Node {
    public int index;
    public Vector3 worldPosition;

    private bool occupied = false;
    public bool isOccupied => occupied;

    public Node(int index, Vector3 worldPosition) {
        this.index = index;
        this.worldPosition = worldPosition;
        occupied = false;
    }

    public void setOccupied(bool val) {
        occupied = val;
    }
}

public class Edge {
    public Node from;
    public Node to;

    private float weight;

    public Edge(Node from, Node to, float weight) {
        this.from = from;
        this.to = to;
        this.weight = weight;
    }

    public float getWeight() {
        if (to.isOccupied) {
            return Mathf.Infinity;
        }
        return weight;
    }
}