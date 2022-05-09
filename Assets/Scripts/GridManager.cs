using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour {
    private static GridManager _instance;

    public Tilemap grid;

    Graph graph;
    Dictionary<Team, int> startPositionPerTeam;

    public static GridManager Instance {
        get {
            if (_instance is null) {
                Debug.Log("GridManager is NULL");
            }
            return _instance;
        }
    }

    public Node setSpawnNode(Team team, int formationNumber, int ithUnit) {
        int startIdx = startPositionPerTeam[team];
        int offsetInFormation = nextPositionInFormation(formationNumber, ithUnit);
        return graph.nodes[startIdx+offsetInFormation];
    }

    public Node selectNode(int index) {
        return(graph.nodes[index]);
    }

    public int nextPositionInFormation(int formationNumber, int ithUnit) { // assuming the first node is 0
        // the first sub-array reserved for spawning the player unit before implementing Drag-and-Drop
        int[,] formations = new int[7, 6] {{0,  0,  0,  0,  0,  0 }, 
                                           {42, 45, 49, 54, 56, 63},
                                           {43, 44, 58, 59, 60, 61},
                                           {42, 45, 48, 55, 58, 61},
                                           {43, 44, 51, 52, 59, 60},
                                           {34, 35, 36, 37, 58, 61},
                                           {42, 44, 46, 49, 51, 53}};
    
        return formations[formationNumber, ithUnit];
    }

    // Start is called before the first frame update
    void Awake() {
        // base.Awake();
        _instance = this;

        InitializeGraph();

        startPositionPerTeam = new Dictionary<Team, int>();

        // Set the starting position of both teams. They are currently 
        // hard-coded for the player as a test before drag-and-drop is implemented
        startPositionPerTeam.Add(Team.playerTeam, 3); 
        startPositionPerTeam.Add(Team.enemyTeam, 0);
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Create graph based on size of Tileset. Assign a node for each tile based on their positions.
    private void InitializeGraph() { 
        graph = new Graph();

        for (int x = grid.cellBounds.xMin; x < grid.cellBounds.xMax; x++) {
            for (int y = grid.cellBounds.yMin; y < grid.cellBounds.yMax; y++) {
                Vector3Int localPosition = new Vector3Int(x, y, (int)grid.transform.position.y);
                if (grid.HasTile(localPosition)) {
                    Vector3 worldPosition = grid.CellToWorld(localPosition);
                    Vector3 centeredWorldPosition = new Vector3(worldPosition[0]+0.5f, worldPosition[1]+0.5f, worldPosition[2]);
                    graph.addNode(centeredWorldPosition);
                }
            }
        }

        var allNodes = graph.nodes;

        // Add edges for each nodes that are adjacent to each other
        foreach(Node from in allNodes) {
            foreach(Node to in allNodes) {
                if (Vector3.Distance(from.worldPosition, to.worldPosition) < 1.2f && from != to) {
                    graph.addEdge(from, to);
                }
            }
        }
    }

    public List<Node> GetPath(Node from, Node to)
    {
        return graph.GetPath(from, to);
    }

    public List<Node> GetNodesCloseTo(Node to)
    {
        return graph.neighbors(to);
    }

    // debug stuff
    public int fromIndex = 0;
    public int toIndex = 0;

    // Show a colored circle for each node and lines representing edges.
    public void OnDrawGizmos() {
        if (graph == null) {
            return;
        }

        var allEdges = graph.edges;

        foreach(Edge e in allEdges) {
            Debug.DrawLine(e.from.worldPosition, e.to.worldPosition, Color.black, 200);
        }

        var allNodes = graph.nodes;

        foreach (Node n in allNodes) {
            Gizmos.color = n.isOccupied ? Color.red : Color.green;
            Gizmos.DrawSphere(n.worldPosition, 0.1f);
        }

        // draw the shortest path between two units
        if(fromIndex <= allNodes.Count && toIndex < allNodes.Count)
        {
            List<Node> path = graph.GetPath(allNodes[fromIndex], allNodes[toIndex]);
            if (path.Count > 1)
            {
                for (int i = 1; i < path.Count; i++)
                {
                    Debug.DrawLine(path[i - 1].worldPosition, path[i].worldPosition, Color.red, 10);
                }
            }
        }
    }
}
