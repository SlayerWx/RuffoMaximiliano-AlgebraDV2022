using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
namespace CustomPlane
{
    public class MyPlane : MonoBehaviour
    {
        Vec3 point;
        Vec3 normal;
        float distance;
        public MyPlane(Vec3 inPoint,Vec3 inNormal)
        {
	    //calculo de un plano: normal.a * punto a + normal.b * punto.b + normal.c * punto.c + distancia = 0
            normal = inNormal;
            point = inPoint;
            distance = -Vec3.Dot(inNormal,inPoint); // distancia al punto que apunta la normal 
            Debug.Log(distance);
        }

    }
}