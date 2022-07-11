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

    private void Start()
    {
        if (precisionRayDivisions > 200f) precisionRayDivisions = 200f;
        if (lengthDivision > 200f) lengthDivision = 200f;
    }
    // Update is called once per frame
    void FixedUpdate()
    {


        for (int i = 0; i < rooms.Length; i++)
        {
            if(rooms[i].gameObject.activeSelf)
            rooms[i].gameObject.SetActive(false);
        }
        float frustrumHeight = (cm.nearClipPlane / 2) + cm.farClipPlane * Mathf.Tan(cm.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustrumwidth = frustrumHeight * cm.aspect;
        if (precisionRayDivisions > 0.5f)
        {
            VerifyViewPointRoom(new Vec3(cm.transform.position)); // se queda
            
            SearchPointAtRoom(new Vec3(cm.transform.position + (cm.transform.forward * cm.nearClipPlane)), new Vec3(cm.transform.position));
            for (float i = -frustrumwidth; i < frustrumwidth; i += ((frustrumwidth * 2) / (precisionRayDivisions - 0.5f)))
            {
                float countLengthDivision = 0;
                float start = cm.nearClipPlane;
                float end = (cm.nearClipPlane + cm.farClipPlane);
                //a + (b - a) * t
                do
                {
                    float t = start + ((end - start) / 2);
                    float aux = cm.nearClipPlane + (cm.farClipPlane - cm.nearClipPlane) * (t / end);
                    Vec3 auxRight = new Vec3(cm.transform.right);
                    Vec3 auxFoward = new Vec3(cm.transform.forward);
                    Vec3 auxPosition = new Vec3(cm.transform.position);
                    //Gizmos.DrawCube(cm.transform.position + (cm.transform.forward * aux) + cm.transform.right * (i * (t / end))
                    //   , new Vector3(0.1f, 0.2f, 0.1f));
                    SearchPointAtRoom(auxPosition + (auxFoward * aux) + auxRight * (i * (t / end)),
                        new Vec3(cm.transform.position));
                    start = t;
                    countLengthDivision++;
                } while (countLengthDivision <= lengthDivision);
            }
        }
    }
    void VerifyViewPointRoom(Vec3 viewOriginPoint)
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if(!rooms[i].gameObject.activeSelf)
            rooms[i].gameObject.SetActive(rooms[i].ViewPointInTheRoom(viewOriginPoint));
        }
    }
    void SearchPointAtRoom(Vec3 point, Vec3 viewOriginPoint) 
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if (!rooms[i].gameObject.activeSelf)
                rooms[i].SearchPointInsideRoom(point,viewOriginPoint,rooms[i].gameObject.name);
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float frustrumHeight = (cm.nearClipPlane / 2) + cm.farClipPlane * Mathf.Tan(cm.fieldOfView * 0.5f * Mathf.Deg2Rad);
        //Gizmos.DrawRay(cm.transform.position + (cm.transform.forward * cm.nearClipPlane), (cm.transform.forward * cm.farClipPlane) + cm.transform.up * frustrumHeight);
        //Gizmos.DrawRay(cm.transform.position + (cm.transform.forward * cm.nearClipPlane), (cm.transform.forward * cm.farClipPlane) + cm.transform.up * (frustrumHeight * -1));

        float frustrumwidth = frustrumHeight * cm.aspect;

        //for(float i = frustrumwidth; i < (frustrumwidth *-1)+0.5f;i+= ((frustrumwidth * -2) / precisionRayDivisions))
        if (precisionRayDivisions > 0.5f)
        {
            Gizmos.DrawCube(cm.transform.position + (cm.transform.forward * cm.nearClipPlane), new Vector3(0.1f, 0.2f, 0.1f));
            for (float i = -frustrumwidth; i < frustrumwidth; i += ((frustrumwidth * 2) / (precisionRayDivisions - 0.5f)))
            {
                Gizmos.DrawRay(cm.transform.position + (cm.transform.forward * cm.nearClipPlane), (cm.transform.forward * cm.farClipPlane) + cm.transform.right * i);
                //a + (b - a) * t
                //for (int t = 0; t <= lengthDivision; t++)
                float countLengthDivision = 0;
                float start = cm.nearClipPlane;
                float end = (cm.nearClipPlane + cm.farClipPlane);
                do
                {
                   float t = start +((end -start)/2);
                   float aux = cm.nearClipPlane + (cm.farClipPlane - cm.nearClipPlane) * (t / end);
                   Gizmos.DrawCube(cm.transform.position + (cm.transform.forward * aux) + cm.transform.right * (i * (t / end))
                      , new Vector3(0.1f, 0.2f, 0.1f));
                   start = t;
                   countLengthDivision++;
                } while (countLengthDivision <= lengthDivision);
            
                Gizmos.DrawCube(cm.transform.position + (cm.transform.forward * (cm.nearClipPlane + cm.farClipPlane)) + cm.transform.right * i, 
                    new Vector3(0.1f, 0.2f, 0.1f));
            
            }
        }
        Gizmos.color = Color.white;
        
    }
}
