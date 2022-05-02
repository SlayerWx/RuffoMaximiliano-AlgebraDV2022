using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using CustomPlane;
public class OcclusionCulling : MonoBehaviour
{
    public float precisionRayDivisions = 10;
    public float lengthDivision = 10;
    public Camera cm;
    public Room[] rooms;

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if(rooms[i].gameObject.activeSelf)
            rooms[i].gameObject.SetActive(false);
        }
        float frustrumHeight = (cm.nearClipPlane / 2) + cm.farClipPlane * Mathf.Tan(cm.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustrumwidth = (frustrumHeight * cm.aspect) * -1;
        for (float i = frustrumwidth; i < (frustrumwidth * -1)+1; i += ((frustrumwidth * -2) / precisionRayDivisions))
        {
            //a + (b - a) * t
            float e = cm.nearClipPlane + (cm.farClipPlane - cm.nearClipPlane) * 0.5f;
            for (int t = 1; t <= lengthDivision; t++)
            {
                float aux = cm.nearClipPlane + (cm.farClipPlane - cm.nearClipPlane) * (t / lengthDivision);
                Vec3 auxRight = new Vec3(cm.transform.right);
                Vec3 auxFoward = new Vec3(cm.transform.forward);
                Vec3 auxPosition = new Vec3(cm.transform.position);

                SearchPointAtRoom((auxPosition + (auxFoward * aux)) + auxRight * (i * (t / lengthDivision)));

            }
        }
    }
    void SearchPointAtRoom(Vec3 point) 
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            rooms[i].SearchPointInsideRoom(point);
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float frustrumHeight = (cm.nearClipPlane / 2) + cm.farClipPlane * Mathf.Tan(cm.fieldOfView * 0.5f * Mathf.Deg2Rad);
        //Gizmos.DrawRay(cm.transform.position + (cm.transform.forward * cm.nearClipPlane), (cm.transform.forward * cm.farClipPlane) + cm.transform.up * frustrumHeight);
        //Gizmos.DrawRay(cm.transform.position + (cm.transform.forward * cm.nearClipPlane), (cm.transform.forward * cm.farClipPlane) + cm.transform.up * (frustrumHeight * -1));

        float frustrumwidth = (frustrumHeight * cm.aspect) * -1;

        for(float i = frustrumwidth; i < (frustrumwidth *-1)+0.5f;i+= ((frustrumwidth * -2) / precisionRayDivisions))
        {
            Gizmos.DrawRay(cm.transform.position + (cm.transform.forward * cm.nearClipPlane), (cm.transform.forward * cm.farClipPlane) + cm.transform.right * i);
            //a + (b - a) * t
            for (int t = 1;t <= lengthDivision;t++)
            {
                float aux = cm.nearClipPlane + (cm.farClipPlane - cm.nearClipPlane) * (t/lengthDivision);
                Gizmos.DrawCube((cm.transform.position + (cm.transform.forward * aux)) + cm.transform.right * (i * (t / lengthDivision)), new Vector3(0.2f, 0.2f, 0.2f));
            }

        }
        Gizmos.color = Color.white;
        
    }
}
