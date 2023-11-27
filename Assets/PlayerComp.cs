using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerComp : MonoBehaviour
{
    [SerializeField] TP2_Manager _Manager;
    public Node_Script NearestNode;

    public void Start()
    {
        _Manager = FindObjectOfType<TP2_Manager>();
        _Manager._Player = this.gameObject;

        StartCoroutine(CoorutineFindNearestNode());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position = this.transform.position + (new Vector3(0, 1, 0) * Time.deltaTime) * 4f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.transform.position = this.transform.position + (new Vector3(0, -1, 0) * Time.deltaTime) * 4f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            this.transform.position = this.transform.position + (new Vector3(-1, 0, 0) * Time.deltaTime) * 4f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.transform.position = this.transform.position + (new Vector3(1, 0, 0) * Time.deltaTime) * 4f;
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
            if(CurrentDis < NearestVal)
            {
                NearestVal = CurrentDis;
                nearest = CurrentNode;
            }
        }
        return nearest;

    }

}
