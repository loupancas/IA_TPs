using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Node_Script : MonoBehaviour
{
    TP2_Manager _PathManager;
    public List<Transform> _Neighbors= new List<Transform>();

    [SerializeField] LayerMask _Obstacles;
    [SerializeField] private float _RayLenght, _ClampMagLenght;
    [SerializeField] private Renderer _Renderer;

    [SerializeField] bool StartingNode, EndingNode;


    public Transform NodeTransform;
    public Collider2D Collider;

    private void Awake()
    {
        _PathManager = FindObjectOfType<TP2_Manager>();
        _PathManager._NodeList.Add(this);
        NodeTransform = GetComponent<Transform>();
        _Renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        if(StartingNode == true)
        {
            SetNodeType("Start");
        }
        else if(EndingNode== true)
        {
            SetNodeType("End");
        }

        FindNeighbors();
    }

    private void FindNeighbors()
    {
        foreach (Node_Script _CurrentNode in _PathManager._NodeList) 
        {

            if(_CurrentNode.NodeTransform == null || _CurrentNode.NodeTransform.position == this.NodeTransform.position)
            {
                continue;
            }
            if(!InLOS(_CurrentNode.NodeTransform))
            {
                continue;
            }
            else
            {
                _Neighbors.Add(_CurrentNode.NodeTransform);
            }
        }
    }
    private bool InLOS(Transform _currennode)
    {
        Vector3 dir = (_currennode.transform.position - this.NodeTransform.position);

        float _DirMag= dir.magnitude;

        float _dirmagclamp = Mathf.Clamp(_DirMag, 0.0f, _ClampMagLenght);

        return !Physics2D.Raycast(this.NodeTransform.position, dir, dir.magnitude, _Obstacles);

        // posible checkeo necesario por si obtengo un valor nulo  
    }
    public void SetNodeType(string NodeType)
    {
        if(NodeType == "Starting" || NodeType == "starting" || NodeType == "start" || NodeType =="Start")
        {
            StartingNode = true;
            _PathManager.StartNode = this;
            _Renderer.material.color = Color.blue;
        }
        else if(NodeType == "Ending" || NodeType == "ending" || NodeType == "end" || NodeType == "End")
        {
            EndingNode = true;
            _PathManager.EndNode = this;
            _Renderer.material.color = Color.red;
        }
        else if(NodeType == "Reset"|| NodeType == "reset")
        {
            StartingNode = false;
            EndingNode = false;
            _Renderer.material.color = Color.green;
        }
        else
        {
            Debug.Log("SetNodeType: invalid String, check the call");
        }
    }
}

