using System.Collections;
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

    public Door[] GetDoors()
    {
        return doors;
    }
    Vec3 centerAux = new Vec3();
    Vec3 leftAux = new Vec3();
    Vec3 rightAux = new Vec3();
    public bool VerifyVectorToNextRoom(Vec3 viewPosition, Vec3 pointLoader, Vec3 nextRoomPosition)
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

        centerAux = center;
        leftAux = left;
        rightAux = right;
        return false;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        if (centerAux == Vec3.Zero) return;
        Gizmos.color = Color.green;
        Gizmos.DrawCube(centerAux, new Vector3(0.2f, 7.2f, 0.2f));
        Gizmos.color = Color.gray;
        Gizmos.DrawCube(leftAux, new Vector3(0.2f, 7.2f, 0.2f));
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(rightAux, new Vector3(0.2f, 7.2f, 0.2f));
        Gizmos.color = Color.white;
    }
}
