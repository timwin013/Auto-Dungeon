using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ObjectClick : MonoBehaviour

{
    // Array of all nodes on map
    public List<GameObject> nodes;

    // Array of all connectors on map
    public List<GameObject> connectors;

    // The current node number. Remembered when map reloaded.
    public static int ActiveNode = 0;

    public static int ActiveBranch = 0;

    // Number of units to be awarded when clicking on chest
    private int unitreward;

    void Start()
    {
        Reload();
    }

    void Reload()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].GetComponent<NodeInfo>().NodeNumber < ActiveNode)
            {
                nodes[i].GetComponent<SpriteRenderer>().color = Color.grey;
                nodes[i].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 0;
            }

            if (nodes[i].GetComponent<NodeInfo>().NodeNumber == ActiveNode)
            {
                nodes[i].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 1;
            }

            if (nodes[i].GetComponent<NodeInfo>().NodeNumber == ActiveNode && ActiveBranch == 1)
            {
                for (int j = 0; j < connectors.Count; j++)
                {
                    if (connectors[j].GetComponent<ConnectorInfo>().ConnectorNumber == ActiveNode && connectors[j].GetComponent<ConnectorInfo>().ConnectorSubNumber == 2)
                    {
                        connectors[j].GetComponent<LineRenderer>().endColor = Color.grey;
                    }
                }
            }


            if (nodes[i].GetComponent<NodeInfo>().NodeNumber == ActiveNode && nodes[i].GetComponent<NodeInfo>().BranchNumber == 2 && ActiveBranch == 2)
            {
                for (int j = 0; j < connectors.Count; j++)
                {
                    if (connectors[j].GetComponent<ConnectorInfo>().ConnectorNumber == ActiveNode + 1 && connectors[j].GetComponent<ConnectorInfo>().ConnectorSubNumber == 1)
                    {
                        connectors[j].GetComponent<LineRenderer>().endColor = Color.grey;
                    }
                }
            }

            if (nodes[i].GetComponent<NodeInfo>().NodeNumber == ActiveNode && nodes[i].GetComponent<NodeInfo>().BranchNumber != 2 && ActiveBranch == 2)
            {
                for (int j = 0; j < connectors.Count; j++)
                {
                    if (connectors[j].GetComponent<ConnectorInfo>().ConnectorNumber == ActiveNode && connectors[j].GetComponent<ConnectorInfo>().ConnectorSubNumber == 1)
                    {
                        connectors[j].GetComponent<LineRenderer>().endColor = Color.grey;
                    }
                }
            }
        }

        for (int k = 0; k < connectors.Count; k++)
        {
            if (connectors[k].GetComponent<ConnectorInfo>().ConnectorNumber < ActiveNode)
            {
                connectors[k].GetComponent<LineRenderer>().endColor = Color.grey;
            }
        }
    }

        void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayCastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayCastPosition, Vector2.zero);

            if (hit.collider != null)
            {
                updateObject(hit.collider.gameObject);
            }
        }
    }

    void updateObject(GameObject go)
    {
        int NodeNumber = go.GetComponent<NodeInfo>().NodeNumber;
        int Index = nodes.IndexOf(go);
        int BranchNumber = nodes[Index].GetComponent<NodeInfo>().BranchNumber;
        int AtStartOfBranch = nodes[Index + 1].GetComponent<NodeInfo>().BranchNumber;
        string NodeType = go.GetComponent<NodeInfo>().NodeType;
        ActiveBranch = 0;

        if (NodeNumber == ActiveNode)
        {
            // Always turn off node when clicked
            go.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 0;
            go.GetComponent<SpriteRenderer>().color = Color.grey;
            for (int i = 0; i < connectors.Count; i++)
            {
                if (connectors[i].GetComponent<ConnectorInfo>().ConnectorNumber == ActiveNode)
                {
                    connectors[i].GetComponent<LineRenderer>().endColor = Color.grey;
                }
            }

            // Not in a branch 
            if (BranchNumber == 0 && AtStartOfBranch == 0)
            {
                nodes[Index + 1].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 1;
            }

            // At start of 2 branching paths
            if (BranchNumber == 0 && AtStartOfBranch == 1)
            {
                nodes[Index + 1].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 1;
                nodes[Index + 2].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 1;
            }

            // On first branch with 1 node
            if (BranchNumber == 1)
            {
                nodes[Index + 3].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 1;
                nodes[Index + 1].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 0;
                nodes[Index + 1].GetComponent<SpriteRenderer>().color = Color.grey;
                nodes[Index + 2].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 0;
                nodes[Index + 2].GetComponent<SpriteRenderer>().color = Color.grey;
                for (int i = 0; i < connectors.Count; i++)
                {
                    if ((connectors[i].GetComponent<ConnectorInfo>().ConnectorNumber == ActiveNode + 1) || (connectors[i].GetComponent<ConnectorInfo>().ConnectorNumber == ActiveNode + 2 && connectors[i].GetComponent<ConnectorInfo>().ConnectorSubNumber == 2))
                    {
                        connectors[i].GetComponent<LineRenderer>().endColor = Color.grey;
                    }
                }
                ActiveNode++;
                ActiveBranch = 1;
            }

            // On second branch and at start of branch
            if (BranchNumber == 2 && nodes[Index + 1].GetComponent<NodeInfo>().BranchNumber == 2)
            {
                nodes[Index + 1].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 1;
                nodes[Index - 1].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 0;
                nodes[Index - 1].GetComponent<SpriteRenderer>().color = Color.grey;

                for (int i = 0; i < connectors.Count; i++)
                {
                    if (connectors[i].GetComponent<ConnectorInfo>().ConnectorNumber == ActiveNode + 2 && connectors[i].GetComponent<ConnectorInfo>().ConnectorSubNumber == 1)
                    {
                        connectors[i].GetComponent<LineRenderer>().endColor = Color.grey;
                    }
                }
                ActiveBranch = 2;
            }

            // On second branch and at end of branch
            if (BranchNumber == 2 && nodes[Index + 1].GetComponent<NodeInfo>().BranchNumber == 0)
            {
                nodes[Index + 1].GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 1;
                ActiveBranch = 2;
            }

            ActiveNode++;


            if (NodeType == "Start")
            {
                Popup popup = UIController.Instance.CreatePopup();
                popup.Init(UIController.Instance.MainCanvas, "Welcome! \n Make your way to the star!", "I'm ready!");
            }

            else if (NodeType == "Treasure")
            {
                Popup popup = UIController.Instance.CreatePopup();
                unitreward = Random.Range(1, 7);
                popup.Init(UIController.Instance.MainCanvas, "You have gained an additional " + unitreward + " unit(s)!", "Dismiss");
                popup.transform.SetAsFirstSibling();
                GameManager.playerUnitsCount += unitreward;
            }

            else if (NodeType == "Hard" || NodeType == "Easy")
            {
                if (NodeType == "Hard"){
                    GameManager.enemyDifficulty = "Hard";
                }

                else{
                    GameManager.enemyDifficulty = "Easy";
                }
                
                SceneManager.LoadScene("Grid");
            }

            else if (NodeType == "Key")
            {
                Popup popup = UIController.Instance.CreatePopup();
                popup.Init(UIController.Instance.MainCanvas, "Congratulations, \n you can now enter a new area ", "Continue!");
            }

            else if (NodeType == "End")
            {
                Popup popup = UIController.Instance.CreatePopup();
                popup.Init(UIController.Instance.MainCanvas, "You've won!", "Hooray!");
            }

        }
    }
}