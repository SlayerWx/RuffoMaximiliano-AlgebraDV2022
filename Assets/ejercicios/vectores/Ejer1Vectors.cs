using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
public class Ejer1Vectors : MonoBehaviour
{
    public enum Ejercicios
    {
        Uno, Dos, Tres, Cuatro, Cinco, Seis, Siete, Ocho, Nueve, Diez
    };
    public Ejercicios ejer;
    public Color resultVectorColor;
    public Vec3 A;
    public Vec3 B;
    Vec3 C = Vec3.Zero;
    float timer = 0f;
    // Update is called once per frame
    private void Start()
    {
        MathDebbuger.Vector3Debugger.AddVector(A, Color.red, "A");
        MathDebbuger.Vector3Debugger.AddVector(B, Color.yellow, "B");
        MathDebbuger.Vector3Debugger.AddVector(C, resultVectorColor, "C");
    }
    void Update()
    {
        MathDebbuger.Vector3Debugger.UpdateColor("C", resultVectorColor);
        MathDebbuger.Vector3Debugger.UpdatePosition("A", A);
        MathDebbuger.Vector3Debugger.UpdatePosition("B", B);
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
                C = Vec3.Cross(B, A);
                break;
            case Ejercicios.Cinco:
                timer += Time.deltaTime;
                if (timer > 1f) timer = 0f;
                C = Vec3.Lerp(A, B, timer);
                break;
            case Ejercicios.Seis:
                C = Vec3.Max(A, B);
                break;
            case Ejercicios.Siete:
                C = Vec3.Project(A, B);
                break;
            case Ejercicios.Ocho:
                C = A + B;
                C = C.normalized * Vec3.Distance(A, B);
                break;
            case Ejercicios.Nueve:
                C = Vec3.Reflect(A, B);
                break;
            case Ejercicios.Diez:
                timer += Time.deltaTime;
                if (timer > 10f) timer = 0f;
                C = Vec3.LerpUnclamped(B, A, timer);
                break;
        }
        MathDebbuger.Vector3Debugger.UpdatePosition("C", C);
    }
}