using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 100f;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += (transform.forward * -1) * (speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * (speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += (transform.right * -1) * (speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime),Space.World);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.down * (rotationSpeed * Time.deltaTime), Space.World);
        }
    }
}
