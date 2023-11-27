using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP2_Manager : MonoBehaviour
{
    [Header("Variables")]

    public List<Node_Script> _NodeList= new List<Node_Script>();

    public Node_Script StartNode, EndNode;

    public List<Transform> _Path = new List<Transform>();

    public GameObject _Player;

    public PlayerComp _PlayerComp;

    public Node_Script _NearestPlayerNode;

    private void Start()
    {
        _PlayerComp = _Player.GetComponent<PlayerComp>();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && StartNode != null && EndNode != null) 
        {
            PathFinding(_Path,StartNode,EndNode);
        }

    }
    public List<Transform> PathFinding(List<Transform> _IaPath,Node_Script NodeStart,Node_Script NodeEnd)
    {
        Node_Script CurrentNode = NodeStart;
        Node_Script CameFromNode = NodeStart;

        if (CurrentNode == NodeStart)
        {
            _IaPath.Add(CurrentNode.transform);
        }

        Transform winner = null;

        print("Pathfinding...");
        while (CurrentNode != NodeEnd)
        {
            float MinDistance = float.MaxValue;
            foreach (Transform _neighbor in CurrentNode._Neighbors)
            {
                if(CurrentNode._Neighbors == null)
                {
                    Debug.Log("se trato de hacer un pathfinding con un nodo que no posee neighbors, el nodo es: " + CurrentNode.name);
                    break;
                }
                if(CameFromNode.transform == _neighbor.transform)
                {
                    continue;
                }

                float currentDistance = Vector2.Distance(EndNode.transform.position, _neighbor.transform.position) * _neighbor.GetComponent<Node_Script>()._Weight;
                if (currentDistance < MinDistance)
                {
                    print("wawa");
                    winner = _neighbor;
                    MinDistance = currentDistance;
                    winner.GetComponent<Renderer>().material.color = Color.cyan;
                }
            }
         
            _IaPath.Add(winner);
            CameFromNode = CurrentNode;
            CurrentNode = winner.GetComponent<Node_Script>();
        }

        EndNode.GetComponent<Renderer>().material.color = Color.red;
        return _IaPath;
    }
     

}
