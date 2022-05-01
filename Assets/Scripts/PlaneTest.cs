using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using CustomPlane;
public class PlaneTest : MonoBehaviour
{
    public Transform test;
    Vec3 aux = Vec3.Zero;
    Material matTest;
    public LogicWall wall;
    void Start()
    {
        matTest = test.GetComponent<MeshRenderer>().material;

    }
    private void Update()
    {
        aux.Set(test.position.x, test.position.y, test.position.z);
        if(wall.GetPlane().GetDistanceToPoint(aux) > 0)
        {
            matTest.color = Color.red;
        }
        else
        {
            matTest.color = Color.blue;
        }
    }

}
