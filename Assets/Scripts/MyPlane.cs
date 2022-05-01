using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CustomMath;
namespace CustomPlane
{
    public class MyPlane : MonoBehaviour
    {
        public Vec3 normal = Vec3.Zero;
        public float distance = 0f;
        public MyPlane(Vec3 inPoint,Vec3 inNormal)
        {
	    //calculo de un plano: normal.a * punto a + normal.b * punto.b + normal.c * punto.c + distancia = 0
            normal = inNormal;
            distance = -Vec3.Dot(inNormal,inPoint); // distancia al punto que apunta la normal 
            Debug.Log("distance: " + distance);
        }
        public MyPlane(Vec3 A, Vec3 B, Vec3 C)
        {
            // B - A y C - A

            normal = Vec3.Cross(B - A, C - A).normalized; // vectores perpendiculares al plano para sacar la normal a partir
                                                          // de 3 puntos
            distance = -Vec3.Dot(normal, A); // distancia al punto que apunta la normal 
        }
        public void Flip()
        {
            normal = -normal;
            distance = -distance;
        }


}
}