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
    public void SearchPointInsideRoom(Vec3 point,Vec3 origin,string name)
    {
        Vec3 aux = new Vec3(transform.position);

        bool isInside = true;
        
        for (int i = 0; i < planes.Length && isInside;i++)
        {
            isInside = !((planes[i].GetPlane().GetDistanceToPoint(point) > 0f && planes[i].GetPlane().GetDistanceToPoint(aux) < 0f) ||
            (planes[i].GetPlane().GetDistanceToPoint(point) < 0f && planes[i].GetPlane().GetDistanceToPoint(aux) > 0f));

            Vec3 positionAux = new Vec3(transform.position);
            if (isInside && !transform.gameObject.activeSelf)
            {
                bool seeDoor = false;
                if (doors != null)
                {
                    seeDoor = doors.VerifyVectorToNextRoom(origin, point, positionAux, planes[i].GetPlane().normal, name);

                }

                if (seeDoor) transform.gameObject.SetActive(true);
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
}
