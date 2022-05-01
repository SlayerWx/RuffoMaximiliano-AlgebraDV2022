using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CustomMath;
namespace CustomPlane
{
    public class MyPlane : MonoBehaviour
    {
        public Vec3 point = Vec3.Zero;
        public Vec3 normal = Vec3.Zero;
        public float distance = 0f;
        public MyPlane(Vec3 inPoint,Vec3 inNormal)
        {
	    //calculo de un plano: normal.a * punto a + normal.b * punto.b + normal.c * punto.c + distancia = 0
            normal = inNormal;
            point = inPoint;
            distance = -Vec3.Dot(inNormal,inPoint); // distancia al punto que apunta la normal 
            Debug.Log(distance);
        }
        public void Flip()
        {
            normal = -normal;
            distance = -distance;
        }


}
}