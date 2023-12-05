using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LeaderMovement : MonoBehaviour
{
    [SerializeField] TP2_Manager _Manager;
    public Node_Script NearestNode;
    public float speed = 5f;
    private Vector3 _targetPos;
    private bool isMoving;
    public void Start()
    {
        _Manager = FindObjectOfType<TP2_Manager>();
        _Manager._Player = this.gameObject;

        StartCoroutine(CoorutineFindNearestNode());
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                _targetPos = hit.point;
                isMoving = true;
            }
        }
        if(isMoving && Vector3.Distance(transform.position, _targetPos)>0.1f)
        {
            Vector3 moveDirection = (_targetPos - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
        }
        else
        {
            isMoving = false;
        }

        _Manager._NearestPlayerNode = NearestNode;

    }

    float NearestVal = float.MaxValue;
    IEnumerator CoorutineFindNearestNode()
    {
        float Delay = 0.25f;
        WaitForSeconds wait = new WaitForSeconds(Delay);

        while (true)
        {
            yield return wait;
            NearestVal = float.MaxValue;
            NearestNode = FindNearestNode();
        }


    }
    Node_Script nearest;
    private Node_Script FindNearestNode()
    {

        foreach (Node_Script CurrentNode in _Manager._NodeList)
        {
            float CurrentDis = Vector3.Distance(CurrentNode.NodeTransform.position, transform.position);
            if (CurrentDis < NearestVal)
            {
                NearestVal = CurrentDis;
                nearest = CurrentNode;
            }
        }
        return nearest;

    }

}
