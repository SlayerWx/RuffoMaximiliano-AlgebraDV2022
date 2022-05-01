using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using CustomPlane;
public class PlaneTest : MonoBehaviour
{
    MyPlane plane;
    public Vec3 origin;
    public Vec3 normal;
    public Transform test;
    Vec3 aux = Vec3.Zero;
    Material matTest;
    void Start()
    {
        plane = new MyPlane(origin, normal);
        matTest = test.GetComponent<MeshRenderer>().material;

    }
    private void Update()
    {
        aux.Set(test.position.x, test.position.y, test.position.z);
        Debug.Log("distance point: " + plane.GetDistanceToPoint(aux));
        if(plane.GetDistanceToPoint(aux) > 0)
        {
            matTest.color = Color.red;
        }
        else
        {
            matTest.color = Color.blue;
        }
    }

}
