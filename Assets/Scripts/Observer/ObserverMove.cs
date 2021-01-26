using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverMove : MonoBehaviour
{
    [Tooltip("Скорость передвижения камеры")]
    public float MovementSpeed;

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position+ new Vector3(-1, 0, 0) * MovementSpeed, Time.deltaTime ); 
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(1, 0, 0) * MovementSpeed, Time.deltaTime );
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 0, 1) * MovementSpeed, Time.deltaTime );
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 0, -1) * MovementSpeed, Time.deltaTime ); 
        }
    }
}
