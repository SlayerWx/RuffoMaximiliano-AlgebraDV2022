using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using CustomPlane;
public class Room : MonoBehaviour
{
    public LogicWall[] planes;
    public LogicViewDoor doors;
    public bool ViewPointInTheRoom(Vec3 viewPoint)
    {
        Vec3 aux = new Vec3(transform.position);
        bool isInside = true;
        for (int i = 0; i < planes.Length && isInside; i++)
        {
            isInside = !((planes[i].GetPlane().GetDistanceToPoint(viewPoint) > 0f && planes[i].GetPlane().GetDistanceToPoint(aux) < 0f) ||
            (planes[i].GetPlane().GetDistanceToPoint(viewPoint) < 0f && planes[i].GetPlane().GetDistanceToPoint(aux) > 0f));
        }
        return isInside;
    }
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
            bool seeDoor = false;
            if (doors != null)
            {
                seeDoor = doors.VerifyVectorToNextRoom(origin, point, positionAux, planes[0].GetPlane().normal);
                
            }

            if(seeDoor) transform.gameObject.SetActive(true);
            ////transform.gameObject.SetActive(true);
            //for (int i = 0; i < doors.GetDoors().Length; i++)
            //{
            //    
            //
            //if (doors.VerifyVectorToNextRoom(origin,point, positionAux))
            //{
            //transform.gameObject.SetActive(true);
            // break;
            //  }
            //}
        }
    }
}
