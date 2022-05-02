using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using CustomPlane;
public class LogicWall : MonoBehaviour
{
    public Vec3 originPoint = Vec3.Zero;
    public Vec3 normal = Vec3.Zero;
    public float width = 1f;
    public float height = 1f;
    MyPlane plane;
    public Transform[] roomsLeft;
    public Transform[] roomsRight;
    void Start()
    {
        plane = new MyPlane(originPoint,normal);
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Quaternion rotation = Quaternion.LookRotation(transform.TransformDirection(plane.normal));
        Matrix4x4 trs = Matrix4x4.TRS(transform.TransformPoint(originPoint), rotation, Vector3.one);
        Gizmos.matrix = trs;
        Color32 color = Color.blue;
        color.a = 125;
        Gizmos.color = color;
        Gizmos.DrawCube(Vector3.zero, new Vector3(width, height, 0.0001f));
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.color = Color.white;
        
    }
    public MyPlane GetPlane()
    {
        return plane;
    }
    public void SetSideActive(bool right,bool active)
    {
        if(right)
        {
            for(int i = 0; i < roomsRight.Length;i++)
            {
                roomsRight[i].gameObject.SetActive(active);
            }
        }
        else
        {
            for (int i = 0; i < roomsLeft.Length; i++)
            {
                roomsLeft[i].gameObject.SetActive(active);
            }
        }
    }
    public void SearchRoom(Vec3 point)
    {
        float distancePoint = plane.GetDistanceToPoint(point);
        Vec3 aux = Vec3.Zero;
        for (int i = 0; i < roomsLeft.Length; i++)
        {

            aux.Set(roomsLeft[i].position.x, roomsLeft[i].position.y, roomsLeft[i].position.z);
            if (plane.GetDistanceToPoint(aux)>0 && distancePoint > 0)
            {
                roomsLeft[i].gameObject.SetActive(true);
            }
            else if (plane.GetDistanceToPoint(aux) < 0 && distancePoint < 0)
            {
                roomsLeft[i].gameObject.SetActive(true);
            }
        }
        for (int i = 0; i < roomsRight.Length; i++)
        {

            aux.Set(roomsRight[i].position.x, roomsRight[i].position.y, roomsRight[i].position.z);
            if (plane.GetDistanceToPoint(aux) > 0 && distancePoint > 0)
            {
                roomsRight[i].gameObject.SetActive(true);
            }
            else if (plane.GetDistanceToPoint(aux) < 0 && distancePoint < 0)
            {
                roomsRight[i].gameObject.SetActive(true);
            }
        }
    }
}