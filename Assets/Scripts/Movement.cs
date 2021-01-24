using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1) * speed * Time.deltaTime);
        }
    }

}
