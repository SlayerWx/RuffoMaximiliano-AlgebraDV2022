using System;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
namespace CustomMath
{
    public class MyQuaternion : IEquatable<MyQuaternion>
    {
        public const float epsilon = 1E-06F;
        public float x;
        public float y;
        public float z;
        public float w;

        public MyQuaternion(float x, float y, float z, float w) //constructor
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public MyQuaternion(Quaternion q) //constructor
        {
            x = q.x;
            y = q.y;
            z = q.z;
            w = q.w;
        }
        public MyQuaternion(MyQuaternion q) //constructor
        {
            x = q.x;
            y = q.y;
            z = q.z;
            w = q.w;
        }
        public static MyQuaternion identity //default
        {
            get
            {
                return new MyQuaternion(0.0f, 0.0f, 0.0f, 1.0f);
            }
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    case 3:
                        return w;
                    default:
                        throw new IndexOutOfRangeException("OUT RANGE!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                    case 3:
                        w = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("OUT RANGE!");
                }
            }
        }
        public Vec3 EulerAngles
        {
            // cuando quieras recordar porque sufriste esto, solo acordate de estas palabras... GIMBAL LOCK! :'D y mira los bonitos ejes x,y,z
            get
            {
                // se multiplica por 2 ya que al pasar a quaterniones se divide por 2, es una cuenta respetando la dimension -1
                Vec3 qAsVec3 = Vec3.Zero;
                qAsVec3.x = Mathf.Rad2Deg * Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * (x * x) - 2 * (z * z));
                qAsVec3.y = Mathf.Rad2Deg * Mathf.Atan2((2 * y * w) - (2 * x * z), 1 - (2 * (y * y)) - 2 * (z * z));
                qAsVec3.z = Mathf.Rad2Deg * Mathf.Asin(2 * x * y + 2 * z * w);

                return qAsVec3;
            }
            set
            {
                MyQuaternion q = Euler(value);
                x = q.x;
                y = q.y;
                z = q.z;
                w = q.w;
            }
        }
        public MyQuaternion normalize
        {
            get
            {
                float mag = Mathf.Sqrt(x * x + y * y + z * z + w * w); //raiz cuadrada de la multiplicacion de la suma de todos los ejes ya potenciados a la 2
                Quaternion q = new Quaternion(x / mag, y / mag, z / mag, w / mag); 
                return new MyQuaternion(q.x, q.y, q.z, q.w);
            }
        }

        ///Public Methods
        public void Set(float newX, float newY, float newZ, float newW) //setter
        {
            x = newX;
            y = newY;
            z = newZ;
            w = newW;
        }

        public void SetFromToRotation(Vec3 fromDirection, Vec3 toDirection) 
        {
            MyQuaternion q = FromToRotation(fromDirection, toDirection); // hace una rotacion que gira desde from a to
            x = q.x;
            y = q.y;
            z = q.z;
            w = q.w;

        }

        public void SetLookRotation(Vec3 view, Vec3 up)
        {
            MyQuaternion q = LookRotation(view, up);
            x = q.x;
            y = q.y;
            z = q.z;
            w = q.w;
        }

        public void SetLookRotation(Vec3 view)
        {
            MyQuaternion q = LookRotation(view, Vec3.Up);
            x = q.x;
            y = q.y;
            z = q.z;
            w = q.w;
        }
        public override string ToString()
        {
            return ("X = " + x + ", Y = " + y + ", Z = " + z + ", W = " + w);
        }

        public static float Angle(MyQuaternion a, MyQuaternion b)
        {
            //retorna el float que corresponde a la cantidad de grados que hay entre la rotacion del angulo de A y B
            MyQuaternion inv = Inverse(a);
            MyQuaternion result = b * inv;

            float angle = Mathf.Acos(result.w) * 2.0f * Mathf.Rad2Deg;
            return angle;
        }

        public static MyQuaternion AngleAxis(float angle, Vec3 axis)
        {
            /// rota la cantidad de angulo que se indica con una referencia de direccion
            angle *= Mathf.Deg2Rad * 0.5f;
            axis.Normalize();
            MyQuaternion newQ = identity;
            newQ.x = axis.x * Mathf.Sin(angle);
            newQ.y = axis.y * Mathf.Sin(angle);
            newQ.z = axis.z * Mathf.Sin(angle);
            newQ.w = Mathf.Cos(angle);
            return newQ.normalize;
        }

        public static float Dot(MyQuaternion a, MyQuaternion b)
        {
            // producto punto de 2 quaterniones/rotaciones
            return ((a.x * b.x) + (a.y * b.y) + (a.z * b.z) + (a.w * b.w));
        }


        public static MyQuaternion Euler(Vec3 euler)
        {
            // usando un vec3 rota los grados en su eje correspondiente
            MyQuaternion qX = identity;
            MyQuaternion qY = identity;
            MyQuaternion qZ = identity;

            float sin = Mathf.Sin(Mathf.Deg2Rad * euler.x * 0.5f);
            float cos = Mathf.Cos(Mathf.Deg2Rad * euler.x * 0.5f);
            qX.Set(sin, 0.0f, 0.0f, cos);

            sin = Mathf.Sin(Mathf.Deg2Rad * euler.y * 0.5f);
            cos = Mathf.Cos(Mathf.Deg2Rad * euler.y * 0.5f);
            qY.Set(0.0f, sin, 0.0f, cos);

            sin = Mathf.Sin(Mathf.Deg2Rad * euler.z * 0.5f);
            cos = Mathf.Cos(Mathf.Deg2Rad * euler.z * 0.5f);
            qZ.Set(0.0f, 0.0f, sin, cos);

            return new MyQuaternion(qX * qY * qZ);

        }
        public static MyQuaternion Euler(float x, float y, float z)
        {
            return Euler(new Vec3(x, y, z));
        }


        public static MyQuaternion FromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {
            // hace una rotacion que gira desde from a to

            Vec3 cross = Vec3.Cross(fromDirection, toDirection);
            MyQuaternion q = identity;
            q.x = cross.x;
            q.y = cross.y;
            q.z = cross.z;
            q.w = fromDirection.magnitude * toDirection.magnitude + Vec3.Dot(fromDirection, toDirection);
            q.Normalize();
            return q;

        }


        public static MyQuaternion Inverse(MyQuaternion rotation)
        {
            //invierte el quaternion
            return new MyQuaternion(-rotation.x, -rotation.y, -rotation.z, rotation.w);
        }

        public static MyQuaternion Lerp(MyQuaternion a, MyQuaternion b, float t)
        {
            // tipico lerp
            if (t > 1f) t = 1f;
            if (t < 0f) t = 0f;
            return LerpUnclamped(a, b, t);
        }

        public static MyQuaternion LerpUnclamped(MyQuaternion a, MyQuaternion b, float t)
        {

            MyQuaternion diff = new MyQuaternion(b.x - a.x, b.y - a.y, b.z - a.z, b.w - b.w);
            MyQuaternion difLerped = new MyQuaternion(diff.x * t, diff.y * t, diff.z * t, diff.w * t);

            return new MyQuaternion(a.x + difLerped.x, a.y + difLerped.y, a.z + difLerped.z, a.w + difLerped.w).normalize;
        }

        public static MyQuaternion LookRotation(Vec3 forward, Vec3 up)
        {
            // rota el quaternion de manera tal que el forward y el up sean direcciones especificas
            MyQuaternion result;
            if (forward == Vec3.Zero || up == Vec3.Zero || (forward.normalized == up.normalized))
            {
                result = identity;
                return result;
            }
            else
            {
                up.Normalize();
                Vec3 a = forward + up - Vec3.Cross(forward, up); // usa foward y up como identificar que debe modificar
                MyQuaternion q = FromToRotation(Vec3.Forward, a);
                return FromToRotation(a, forward) * q;
            }
        }

        public static MyQuaternion Normalize(MyQuaternion q)
        {
            return new MyQuaternion(q.normalize);
        }

        public static MyQuaternion Slerp(MyQuaternion a, MyQuaternion b, float t)
        {
            // lerp esferico
            if (t > 1f) t = 1f;
            if (t < 0f) t = 0f;
            return SlerpUnclamped(a, b, t);
        }
        public static MyQuaternion SlerpUnclamped(MyQuaternion a, MyQuaternion b, float t)
        {
            float num1;
            float num2;
            MyQuaternion quaternion = identity;
            //calcula el angulo entre ambos
            float dot = (((a.x * b.x) + (a.y * b.y)) + (a.z * b.z)) + (a.w * b.w); //producto punto
            bool neg = false;
            if (dot < 0f) 
            {
                neg = true;
                dot = -dot;
            }
            if (dot >= 1.0f)
            {
                num1 = 1.0f - t;
                if (neg) num2 = -t;
                else num2 = t;
            }
            else
            {
                //calculos del angulo "theta"
                float num3 = (float)Math.Acos(dot);
                float num4 = (float)(1.0 / Math.Sin(num3));
                num1 = ((float)Math.Sin(((1f - t) * num3))) * num4;//lerp
                if (neg)
                    num2 = (((float)-Math.Sin((t * num3))) * num4);
                else
                    num2 = (((float)Math.Sin((t * num3))) * num4);
            }
            quaternion.x = ((num1 * a.x) + (num2 * b.x));
            quaternion.y = ((num1 * a.y) + (num2 * b.y));
            quaternion.z = ((num1 * a.z) + (num2 * b.z));
            quaternion.w = ((num1 * a.w) + (num2 * b.w));
            return quaternion;
        }
        public void Normalize()
        {
            MyQuaternion normalQ = new MyQuaternion(normalize);
            x = normalQ.x;
            y = normalQ.y;
            z = normalQ.z;
            w = normalQ.w;
        }

        public static Vec3 operator *(MyQuaternion rotation, Vec3 point)
        {
            //productos intermedios
            float x2 = rotation.x * 2f; //ejes de rotacion
            float y2 = rotation.y * 2f;
            float z2 = rotation.z * 2f;
            float xx2 = rotation.x * x2; //planos de rotacion
            float yy2 = rotation.y * y2;
            float zz2 = rotation.z * z2;
            float xy2 = rotation.x * y2;
            float xz2 = rotation.x * z2;
            float yz2 = rotation.y * z2;
            float wx2 = rotation.w * x2; //reales
            float wy2 = rotation.w * y2;
            float wz2 = rotation.w * z2;

            // se aplica al vector directo que a su vez da la dirección unitaria
            Vec3 result;
            result.x = (1f - (yy2 + zz2)) * point.x + (xy2 - wz2) * point.y + (xz2 + wy2) * point.z; //"1f -" ignora este eje
            result.y = (xy2 + wz2) * point.x + (1f - (xx2 + zz2)) * point.y + (yz2 - wx2) * point.z;
            result.z = (xz2 - wy2) * point.x + (yz2 + wx2) * point.y + (1f - (xx2 + yy2)) * point.z;
            return result;

        }
        public static MyQuaternion operator *(MyQuaternion a, MyQuaternion b)
        {
            float x = (a.w * b.x) + (a.x * b.w) + (a.y * b.z) - (a.z * b.y);
            float y = (a.w * b.y) - (a.x * b.z) + (a.y * b.w) + (a.z * b.x);
            float z = (a.w * b.z) + (a.x * b.y) - (a.y * b.x) + (a.z * b.w);
            float w = (a.w * b.w) - (a.x * b.x) - (a.y * b.y) - (a.z * b.z);
            return new MyQuaternion(x, y, z, w);

        }
        public static bool operator ==(MyQuaternion lhs, MyQuaternion rhs)
        {
            return (lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w);
        }
        public static bool operator !=(MyQuaternion lhs, MyQuaternion rhs)
        {
            return !(lhs == rhs);
        }

        public static implicit operator Quaternion(MyQuaternion q)
        {
            return new Quaternion(q.x, q.y, q.z, q.w);
        }

        public override bool Equals(object other)
        {
            if (!(other is MyQuaternion)) return false;
            return Equals((MyQuaternion)other);
        }
        public bool Equals(MyQuaternion other)
        {
            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z) && w.Equals(other.w);
        }

        public override int GetHashCode()
        {
            int hashCode = 1585477088;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            hashCode = hashCode * -1521134295 + z.GetHashCode();
            hashCode = hashCode * -1521134295 + w.GetHashCode();
            hashCode = hashCode * -1521134295 + EulerAngles.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<MyQuaternion>.Default.GetHashCode(normalize);
            return hashCode;
        }
    }
}