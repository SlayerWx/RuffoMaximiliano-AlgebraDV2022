using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
public class Ejer1 : MonoBehaviour
{
    public enum Ejercicios
    {
        Uno,Dos,Tres,Cuatro,Cinco,Seis,Siete,Ocho,Nueve,Diez
    };
    public Ejercicios ejer;
    public Color resultVectorColor;
    public Vec3 A;
    public Vec3 B;
    Vec3 C = Vec3.Zero;
    // Update is called once per frame
    private void Start()
    {
        MathDebbuger.Vector3Debugger.AddVector(A, Color.red, "A");
        MathDebbuger.Vector3Debugger.AddVector(B, Color.yellow, "B");
        MathDebbuger.Vector3Debugger.AddVector(C, resultVectorColor, "C");
    }
    void Update()
    {
        MathDebbuger.Vector3Debugger.UpdateColor("C",resultVectorColor);
        MathDebbuger.Vector3Debugger.UpdatePosition("A", A);
        MathDebbuger.Vector3Debugger.UpdatePosition("B", B);
        MathDebbuger.Vector3Debugger.UpdatePosition("C", C);
        switch (ejer)
        {
            case Ejercicios.Uno:
                C = A + B;
                break;
            case Ejercicios.Dos:
                C = B - A;
                break;
            case Ejercicios.Tres:
                C.x = A.x * B.x;
                C.y = A.y * B.y;
                C.z = A.z * B.z;
                break;
            case Ejercicios.Cuatro:
                C = Vec3.Cross(A,B);
                break;
            case Ejercicios.Cinco:
                break;
            case Ejercicios.Seis:
                break;
            case Ejercicios.Siete:
                break;
            case Ejercicios.Ocho:
                break;
            case Ejercicios.Nueve:
                break;
            case Ejercicios.Diez:
                break;
        }
    }
}
