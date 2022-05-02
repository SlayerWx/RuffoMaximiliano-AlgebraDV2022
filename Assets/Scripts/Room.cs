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
        Vec3 aux = new Vec3(transform.position);
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

        if (isInside && !transform.gameObject.activeSelf)
        {
            transform.gameObject.SetActive(true);
        }
    }
}
