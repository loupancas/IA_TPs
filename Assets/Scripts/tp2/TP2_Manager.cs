using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP2_Manager : MonoBehaviour
{
    [Header("Variables")]

    public List<Node_Script> _NodeList= new List<Node_Script>();

    public Node_Script StartNode, EndNode;

    public List<Transform> _Path = new List<Transform>();

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && StartNode != null && EndNode != null) 
        {
            PathFinding(_Path);
        }

    }
    public List<Transform> PathFinding(List<Transform> _IaPath)
    {
        Node_Script CurrentNode = StartNode;
        Node_Script CameFromNode = StartNode;

        if (CurrentNode == StartNode)
        {
            _IaPath.Add(CurrentNode.transform);
        }

        Transform winner = null;

        print("Pathfinding...");
        while (CurrentNode != EndNode)
        {
            float MinDistance = float.MaxValue;
            foreach (Transform _neighbor in CurrentNode._Neighbors)
            {
                if(CameFromNode.transform == _neighbor.transform)
                {
                    continue;
                }

                float currentDistance = Vector2.Distance(EndNode.transform.position, _neighbor.transform.position);
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
