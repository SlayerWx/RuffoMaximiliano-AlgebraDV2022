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
}