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
    void Start()
    {
        plane = new MyPlane(origin, normal);
    }

}
