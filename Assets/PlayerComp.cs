using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerComp : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.position = this.transform.position + (new Vector3(0,1,0) * Time.deltaTime) * 4f;
        }
        else if(Input.GetKey(KeyCode.S)) 
        {
            this.transform.position = this.transform.position + (new Vector3(0, -1, 0) * Time.deltaTime) *4f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            this.transform.position = this.transform.position + (new Vector3(-1, 0, 0) * Time.deltaTime) *4f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.transform.position = this.transform.position + (new Vector3(1, 0, 0) * Time.deltaTime) * 4f;
        }

    }
}
