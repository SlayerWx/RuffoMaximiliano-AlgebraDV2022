using UnityEngine;
using CustomMath;
public class Test : MonoBehaviour
{
    private void Start()
    {
        Vector3 a = new Vector3(34,-37,4);
        Vec3 b = new Vec3(34, -37, 4);
        Debug.Log("vector3: " + 5*a);
        Debug.Log("vec3:    " + 5*b);
    }
}