using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Node_Script : MonoBehaviour
{
    TP2_Manager _PathManager;
    public List<Transform> _Neighbors= new List<Transform>();

    [SerializeField] LayerMask _NodeLayer;
    [SerializeField] private float _RayLenght;


    public Transform NodeTransform;
    public Collider2D Collider;

    private void Start()
    {
        _PathManager = FindObjectOfType<TP2_Manager>();
        _PathManager._NodeList.Add(this);
        NodeTransform = GetComponent<Transform>();
        FindNeighbors();
    }

    private void FindNeighbors()
    {
        foreach (Node_Script _CurrentNode in _PathManager._NodeList) 
        {
            print(_CurrentNode.gameObject.name);

            if(_CurrentNode.NodeTransform == null || _CurrentNode.NodeTransform.position == this.NodeTransform.position)
            {
                continue;
            }

            RaycastHit2D _RayHit2D = Physics2D.Raycast(this.NodeTransform.position, (_CurrentNode.NodeTransform.position - this.transform.position).normalized, 50f, _NodeLayer);

            if(_RayHit2D.collider != this.Collider)
            {
                _Neighbors.Add(_CurrentNode.NodeTransform);
            }

        }
    }

}

