using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using CustomPlane;
public class Room : MonoBehaviour
{
    public LogicWall[] planes;
    
    public void SearchPointInsideRoom(Vec3 point)
    {
        Vec3 aux = Vec3.Zero;
        aux.Set(transform.position.x, transform.position.y, transform.position.z);
        // Debug.Log("plano 0: " + planes[0].GetPlane().GetDistanceToPoint(point) + " " + planes[0].GetPlane().GetDistanceToPoint(aux));

        bool isInside = true;
        
        for (int i = 0; i < planes.Length && isInside;i++)
        {
            if ((planes[i].GetPlane().GetDistanceToPoint(point) > 0f && planes[i].GetPlane().GetDistanceToPoint(aux) < 0f) ||
            (planes[i].GetPlane().GetDistanceToPoint(point) < 0f && planes[i].GetPlane().GetDistanceToPoint(aux) > 0f))
            {
                isInside = false;
            }
        }
       // Debug.Log(isInside);

            //for(int i = 0; i < planes.Length;i++)
            //{
            //    if((!(planes[i].GetPlane().GetDistanceToPoint(point) > 0 &&
            //       planes[i].GetPlane().GetDistanceToPoint(aux) > 0) ||
            //       !(planes[i].GetPlane().GetDistanceToPoint(point) < 0 &&
            //       planes[i].GetPlane().GetDistanceToPoint(aux) < 0)))
            //    { 
            //        pointHere = false;
            //    }
            //}
        if (isInside && !transform.gameObject.activeSelf)
        {
            transform.gameObject.SetActive(true);
        }
    }
}
