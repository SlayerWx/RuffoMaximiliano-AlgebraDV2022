using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CustomMath;
namespace CustomPlane
{
    public class MyPlane : MonoBehaviour
    {
        public Vec3 normal = Vec3.One;
        public float distance = 0f;
        public MyPlane(Vec3 inPoint,Vec3 inNormal)
        {
	    //calculo de un plano: normal.a * punto a + normal.b * punto.b + normal.c * punto.c + distancia = 0
            normal = inNormal;
            distance = -Vec3.Dot(inNormal,inPoint); // distancia al punto que apunta la normal ;
            
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

        public float GetDistanceToPoint(Vec3 point) 
        {
            // distancia positiva si el punto esta frente al plano
            // distancia negativa si el punto esta espaldas al plano
            return Vec3.Dot(point, normal) + distance / Vec3.Magnitude(normal);
        }
        public Vec3 GetClosetPoint(Vec3 point) 
        {
            //el punto mas cercano dentro del plano a este punto
            return point - normal * GetDistanceToPoint(point); 
        }
    }

}