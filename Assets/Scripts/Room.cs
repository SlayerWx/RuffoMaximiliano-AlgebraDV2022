﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using CustomPlane;
public class Room : MonoBehaviour
{
    public LogicWall[] planes;
    public LogicViewDoor doors;
    public void SearchPointInsideRoom(Vec3 point,Vec3 origin)
    {
        Vec3 aux = new Vec3(transform.position);
        // Debug.Log("plano 0: " + planes[0].GetPlane().GetDistanceToPoint(point) + " " + planes[0].GetPlane().GetDistanceToPoint(aux));

        bool isInside = true;
        
        for (int i = 0; i < planes.Length && isInside;i++)
        {
            isInside = !((planes[i].GetPlane().GetDistanceToPoint(point) > 0f && planes[i].GetPlane().GetDistanceToPoint(aux) < 0f) ||
            (planes[i].GetPlane().GetDistanceToPoint(point) < 0f && planes[i].GetPlane().GetDistanceToPoint(aux) > 0f));
        }
        Vec3 positionAux = new Vec3(transform.position);
        if (isInside && !transform.gameObject.activeSelf)
        {
           if(doors != null) doors.VerifyVectorToNextRoom(origin, point, positionAux);


            ////transform.gameObject.SetActive(true);
            //for (int i = 0; i < doors.GetDoors().Length; i++)
            //{
            //    
            //
            //if (doors.VerifyVectorToNextRoom(origin,point, positionAux))
            //{

            transform.gameObject.SetActive(true);
                   // break;
              //  }
            //}
        }
    }
}
