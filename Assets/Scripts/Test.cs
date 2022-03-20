using UnityEngine;
using CustomMath;
public class Test : MonoBehaviour
{
    public Vector3 a = new Vector3(34, -37, 4.5f);
    public Vec3 b = new Vec3(34, -37, 4.5f);
    public Vector3 c = new Vector3(12, -4, 30.5f);
    public Vec3 d = new Vec3(12, -4, 30.5f);
    public float equisdeA = 0;
    public float equisdeB = 0;
    private void Start()
    {
        equisdeA = Vector3.Angle(a, c);
        equisdeB = Vec3.Angle(b, d);

    }
}