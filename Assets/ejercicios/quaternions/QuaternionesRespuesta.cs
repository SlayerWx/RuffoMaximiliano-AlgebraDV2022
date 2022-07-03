using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
public class QuaternionesRespuesta : MonoBehaviour
{
    public enum Ejer { Uno, Dos, Tres}
    public Ejer ejercicio;
    public float angle = 0f;
    Vec3 ej1;
    private List<Vector3> listEj2 = new List<Vector3>();
    private List<Vector3> listEj3 = new List<Vector3>();
    void Start()
    {

        MathDebbuger.Vector3Debugger.AddVector(Vector3.zero, Color.red, "Uno");
        ej1 = new Vec3(10f,0,0);
        MathDebbuger.Vector3Debugger.AddVectorsSecuence(listEj2, false, Color.blue, "Dos");
        listEj2.Add(new Vec3(10, 0, 0));
        listEj2.Add(new Vec3(10, 10, 0));
        listEj2.Add(new Vec3(20, 10, 0));

        MathDebbuger.Vector3Debugger.AddVectorsSecuence(listEj3, false, Color.yellow, "Tres");
        listEj3.Add(new Vec3(10, 0, 0));
        listEj3.Add(new Vec3(10, 10, 0));
        listEj3.Add(new Vec3(20, 10, 0));
        listEj3.Add(new Vec3(20, 20, 0));
    }

    void FixedUpdate()
    {
        switch(ejercicio)
        {
            case Ejer.Uno:
                ej1 = MyQuaternion.AngleAxis(angle,Vec3.Up) * ej1;
                MathDebbuger.Vector3Debugger.UpdatePosition("Uno", ej1);
                MathDebbuger.Vector3Debugger.EnableEditorView("Uno");
                break;
            case Ejer.Dos:
                for (int i = 0; i < listEj2.Count; i++)
                {
                    listEj2[i] = MyQuaternion.Euler(new Vec3(0, angle, 0)) * new Vec3(listEj2[i]);
                }
                MathDebbuger.Vector3Debugger.UpdatePositionsSecuence("Dos", listEj2);
                MathDebbuger.Vector3Debugger.EnableEditorView("Dos");
                break;
            case Ejer.Tres:

                for (int i = 0; i < listEj3.Count; i++)
                {
                    if ((i % 2) != 0)
                    {
                        if (i == 3)
                            listEj3[i] = MyQuaternion.Euler(new Vec3(-angle, -angle, 0)) * new Vec3(listEj3[i]);
                        else
                            listEj3[i] = MyQuaternion.Euler(new Vec3(angle, angle, 0)) * new Vec3(listEj3[i]);
                    }
                }
                MathDebbuger.Vector3Debugger.EnableEditorView("Tres");
                MathDebbuger.Vector3Debugger.UpdatePositionsSecuence("Tres", listEj3);
                break;
        }
    }
}
