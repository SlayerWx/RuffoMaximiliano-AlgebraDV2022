﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using CustomPlane;
public class LogicViewDoor : MonoBehaviour
{
    public LogicWall[] wallToDoors;
    [System.Serializable]
    public struct Door
    {
        public float distanceFromCenter; //negativo izquierda, positivo derecha
        public float width;
        public float height;
    }
    public Door[] doors;
    Vec3 centerAux = new Vec3(); //Gizmos
    Vec3 leftAux = new Vec3(); //Gizmos
    Vec3 rightAux = new Vec3(); //Gizmos
    public Door[] GetDoors()
    {
        return doors;
    }
    public bool VerifyVectorToNextRoom(Vec3 viewPosition, Vec3 pointLoader, Vec3 nextRoomPosition, Vec3 normalFromPlane)
    {
        //Vec3 left = new Vec3();
        //(pointLoader - viewPosition)
        // nextRoomPosition -+ width (verificar direccion del plano)
        // 
        //
        //if (AxB * AxC >= 0 && CxB * CxA >= 0)
        Vec3 direction = Vec3.Cross(wallToDoors[0].GetPlane().normal, Vec3.Up).normalized;
        Vec3 center = new Vec3(wallToDoors[0].GetPlane().GetClosetPoint(nextRoomPosition)) + (direction * doors[0].distanceFromCenter);
        Vec3 left = center - (direction * doors[0].width);
        Vec3 right = center + (direction * doors[0].width);

        centerAux = center; //Gizmos
        leftAux = left; //Gizmos
        rightAux = right; //Gizmos
        Vec3 cameraToPoint = (pointLoader - viewPosition).normalized;


        float a = Vec3.Dot((center - viewPosition) , normalFromPlane);
        float b = Vec3.Dot(cameraToPoint,normalFromPlane);
        if (b == 0) return false; // vector paralelo al plano
        if (a == 0) return false;// el vector comparte todos sus puntos con el plano
        Vec3 P = (a / b) * cameraToPoint + viewPosition;
        centerAux = new Vec3(P);
        P.y = 0f;
        left.y = 0f;
        right.y = 0f;
        center.y = 0f;

        
        //if (dis < Vec3.epsilon) dis = Vec3.epsilon;
        if(Vec3.Distance(left,right)/2 > Vec3.Distance(P, center))
        {
            Debug.Log("entro");
            return true;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        if (centerAux == Vec3.Zero) return;
        Gizmos.color = Color.green;
        Gizmos.DrawCube(centerAux, new Vector3(0.2f, 10.2f, 0.2f));
        Gizmos.color = Color.gray;
        Gizmos.DrawCube(leftAux, new Vector3(0.2f, 7.2f, 0.2f));
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(rightAux, new Vector3(0.2f, 7.2f, 0.2f));
        Gizmos.color = Color.white;
    }
}