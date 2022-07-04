using System;
using UnityEngine;

namespace CustomMath
{
    [Serializable]
    public struct MyMatrix4x4 : IEquatable<MyMatrix4x4>
    {
        public float m00; // Columns
        public float m01; // 0   1   2   3  
        public float m02;
        public float m03; //|00, 01, 02, 03 |  0
        public float m10; //|10, 11, 12, 13 |  1 Rows
        public float m11; //|20, 21, 22, 23 |  2
        public float m12; //|30, 31, 32, 33 |  3
        public float m13; 
        public float m20; 
        public float m21; 
        public float m22;
        public float m23;
        public float m30;
        public float m31;
        public float m32;
        public float m33;

        public float this[int index]
        {
            get
            {
                float val = -1;
                switch (index)
                {
                    case 0:
                        val = m00;
                        break;
                    case 1:
                        val = m10;
                        break;
                    case 2:
                        val = m20;
                        break;
                    case 3:
                        val = m30;
                        break;
                    case 4:
                        val = m01;
                        break;
                    case 5:
                        val = m11;
                        break;
                    case 6:
                        val = m21;
                        break;
                    case 7:
                        val = m31;
                        break;
                    case 8:
                        val = m02;
                        break;
                    case 9:
                        val = m12;
                        break;
                    case 10:
                        val = m22;
                        break;
                    case 11:
                        val = m32;
                        break;
                    case 12:
                        val = m03;
                        break;
                    case 13:
                        val = m13;
                        break;
                    case 14:
                        val = m23;
                        break;
                    case 15:
                        val = m33;
                        break;
                }
                if (val == -1)
                    throw new IndexOutOfRangeException("OUT RANGE!");

                return val;
            }
        }
        public static MyMatrix4x4 Identity
        { //matriz de identidad 
            get
            {
                return new MyMatrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
            }
        }
        public static MyMatrix4x4 Zero
        {
            get
            {
                return new MyMatrix4x4(new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0));
            }
        }
        public MyMatrix4x4 Transpose
        {
            get
            {
               
                return new MyMatrix4x4(new Vector4(m00, m01, m02, m03), new Vector4(m10, m11, m12, m13), new Vector4(m20, m21, m22, m23), new Vector4(m30, m31, m32, m33));
                
            }
        }
        public Quaternion rotation
        {
            get
            {
                // https://answers.unity.com/questions/11363/converting-matrix4x4-to-quaternion-vector3.html
                // Mathf.Max para evitar numeros inapropiados, 
                Quaternion q = Quaternion.identity;
                q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m00 + m11 + m22)) / 2;
                q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m00 - m11 - m22)) / 2;
                q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m00 + m11 - m22)) / 2;
                q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m00 - m11 + m22)) / 2;
                // Sign toma el signo del segundo término y establece el signo del primero sin alterar la magnitud
                q.x = Mathf.Sign(q.x * (m21 - m12));
                q.y = Mathf.Sign(q.y * (m02 - m20));
                q.z = Mathf.Sign(q.z * (m10 - m01));
                return q;
            }
        }
        public Vector3 LossyScale
        {
            get
            {
                return new Vector3(m00, m11, m22);
            }
        }
        public MyMatrix4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
        {
            //X
            m00 = column0.x;
            m01 = column1.x;
            m02 = column2.x;
            m03 = column3.x;
            //Y
            m10 = column0.y;
            m11 = column1.y;
            m12 = column2.y;
            m13 = column3.y;
            //Z
            m20 = column0.z;    
            m21 = column1.z;    
            m22 = column2.z;    
            m23 = column3.z;   
            //W                 
            m30 = column0.w;    
            m31 = column1.w;    
            m32 = column2.w;
            m33 = column3.w;
        }
        public Vector4 GetColumn(int index)
        {
            if (index >= 0 && index <= 3)
            {
                Vector4 column = Vector4.zero;
                switch (index)
                {
                    case 0:
                        column = new Vector4(m00, m10, m20, m30);
                        break;
                    case 1:
                        column = new Vector4(m01, m11, m21, m31);
                        break;
                    case 2:
                        column = new Vector4(m02, m12, m22, m32);
                        break;
                    case 3:
                        column = new Vector4(m03, m13, m23, m33);
                        break;
                }
                return column;
            }
            else
            {
                Debug.LogError("OUT RANGE!");
                return Vector4.zero;
            }
        }
        public Vector4 GetRow(int index)
        {
            if (index >= 0 && index <= 3)
            {
                Vector4 column = Vector4.zero;
                switch (index)
                {
                    case 0:
                        column = new Vector4(m00, m01, m02, m03);
                        break;
                    case 1:
                        column = new Vector4(m10, m11, m12, m13);
                        break;
                    case 2:
                        column = new Vector4(m20, m21, m22, m23);
                        break;
                    case 3:
                        column = new Vector4(m30, m31, m32, m33);
                        break;
                }
                return column;
            }
            else
            {
                Debug.LogError("OUT RANGE!");
                return Vector4.zero;
            }
        }
        public void SetRow(int index, Vector4 row)
        {
            if (index >= 0 && index <= 3)
            {
                switch (index)
                {
                    case 0:
                        m00 = row.x; m01 = row.y; m02 = row.z; m03 = row.w;
                        break;
                    case 1:
                        m10 = row.x; m11 = row.y; m12 = row.z; m13 = row.w;
                        break;
                    case 2:
                        m20 = row.x; m21 = row.y; m22 = row.z; m23 = row.w;
                        break;
                    case 3:
                        m30 = row.x; m31 = row.y; m32 = row.z; m33 = row.w;
                        break;
                }
            }
            else
            {
                Debug.LogError("OUT RANGE!");
            }
        }
        public void SetColumn(int index, Vector4 column)
        {
            if (index >= 0 && index <= 3)
            {
                switch (index)
                {
                    case 0:
                        m00 = column.x; m10 = column.y; m20 = column.z; m30 = column.w;
                        break;
                    case 1:
                        m01 = column.x; m11 = column.y; m21 = column.z; m31 = column.w;
                        break;
                    case 2:
                        m02 = column.x; m12 = column.y; m22 = column.z; m32 = column.w;
                        break;
                    case 3:
                        m03 = column.x; m13 = column.y; m23 = column.z; m33 = column.w;
                        break;
                }
            }
            else
            {
                Debug.LogError("OUT RANGE");
            }
        }
        public void SetTRS(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this = TRS(position, rotation, scale);
        }
        public static MyMatrix4x4 TRS(Vector3 position, Quaternion rotation, Vector3 scale)
        { //esto me recuerda mucho a mvp XD, buen model (?
            return Translate(position) * Rotate(rotation) * Scale(scale);
        }
        public static MyMatrix4x4 Translate(Vector3 vec)
        {
            MyMatrix4x4 auxTranslate = Identity;
            //suma el vec3 a los slots de posicion
            Vector4 vecTranslate = new Vector4(vec.x + auxTranslate.m03, vec.y + auxTranslate.m13, vec.z + auxTranslate.m23, 1);

            auxTranslate.m03 = vecTranslate.x;
            auxTranslate.m13 = vecTranslate.y;
            auxTranslate.m23 = vecTranslate.z;
            auxTranslate.m33 = vecTranslate.w;

            return auxTranslate;
        }
        public static MyMatrix4x4 Scale(Vector3 vec)
        {
            MyMatrix4x4 matScale = Identity;
            //multiplica la escala por lo que ya esta dentro de los slots de la matriz
            Vector4 vecScale = new Vector4(vec.x * matScale.m00, vec.y * matScale.m11, vec.z * matScale.m22, 1);
            // lo reemplaza
            matScale.m00 = vecScale.x;
            matScale.m11 = vecScale.y;
            matScale.m22 = vecScale.z;
            matScale.m33 = vecScale.w;

            return matScale;
        }
        public static MyMatrix4x4 Rotate(Quaternion q)
        {
            // calculo que hace la grafica y rota todo a la vez.. 
            //  p' = q*p*q^-1
            //  Cos(angulo/2) + Sin(angulo)(ijk)

            /*
            m02 = 2 * qx * qz + 2 * qy * qw | m00 = 1 - 2 * qy2 - 2 * qz2     | m01 = 2 * qx * qy - 2 * qz * qw   
            m12 = 2 * qy * qz - 2 * qx * qw | m10 = 2 * qx * qy + 2 * qz * qw | m11 = 1 - 2 * qx2 - 2 * qz2
            m22 = 1 - 2 * qx2 - 2 * qy2     | m20 = 2 * qx * qz - 2 * qy * qw | m21 = 2 * qy * qz + 2 * qx * qw 
            */
            MyMatrix4x4 mat = Identity; // calculo que hace la grafica y rota todo a la vez.. 
            mat.m02 = 2.0f * (q.x * q.z) + 2.0f * (q.y * q.w);
            mat.m12 = 2.0f * (q.y * q.z) - 2.0f * (q.x * q.w);
            mat.m22 = 1 - 2.0f * (q.x * q.x) - 2.0f * (q.y * q.y);

            mat.m00 = 1 - 2.0f * (q.y * q.y) - 2.0f * (q.z * q.z);
            mat.m10 = 2.0f * (q.x * q.y) + 2.0f * (q.z * q.w);
            mat.m20 = 2.0f * (q.x * q.z) - 2.0f * (q.y * q.w);

            mat.m01 = 2.0f * (q.x * q.y) - 2.0f * (q.z * q.w);
            mat.m11 = 1 - 2.0f * (q.x * q.x) - 2.0f * (q.z * q.z);
            mat.m21 = 2.0f * (q.y * q.z) + 2.0f * (q.x * q.w);
            return mat;
        }
        public static MyMatrix4x4 operator *(MyMatrix4x4 mat1, MyMatrix4x4 mat2)
        {
            MyMatrix4x4 matResult = Identity;

            //Row 0
            float m00 = Vector4.Dot(mat1.GetRow(0), mat2.GetColumn(0));
            float m01 = Vector4.Dot(mat1.GetRow(0), mat2.GetColumn(1));
            float m02 = Vector4.Dot(mat1.GetRow(0), mat2.GetColumn(2));
            float m03 = Vector4.Dot(mat1.GetRow(0), mat2.GetColumn(3));
            //Row 1
            float m10 = Vector4.Dot(mat1.GetRow(1), mat2.GetColumn(0));
            float m11 = Vector4.Dot(mat1.GetRow(1), mat2.GetColumn(1));
            float m12 = Vector4.Dot(mat1.GetRow(1), mat2.GetColumn(2));
            float m13 = Vector4.Dot(mat1.GetRow(1), mat2.GetColumn(3));
            //Row 2
            float m20 = Vector4.Dot(mat1.GetRow(2), mat2.GetColumn(0));
            float m21 = Vector4.Dot(mat1.GetRow(2), mat2.GetColumn(1));
            float m22 = Vector4.Dot(mat1.GetRow(2), mat2.GetColumn(2));
            float m23 = Vector4.Dot(mat1.GetRow(2), mat2.GetColumn(3));
            //Row 3
            float m30 = Vector4.Dot(mat1.GetRow(3), mat2.GetColumn(0));
            float m31 = Vector4.Dot(mat1.GetRow(3), mat2.GetColumn(1));
            float m32 = Vector4.Dot(mat1.GetRow(3), mat2.GetColumn(2));
            float m33 = Vector4.Dot(mat1.GetRow(3), mat2.GetColumn(3));

            matResult.SetRow(0, new Vector4(m00, m01, m02, m03));
            matResult.SetRow(1, new Vector4(m10, m11, m12, m13));
            matResult.SetRow(2, new Vector4(m20, m21, m22, m23));
            matResult.SetRow(3, new Vector4(m30, m31, m32, m33));

            return matResult;
        }
        public static Vector4 operator *(MyMatrix4x4 mat, Vector4 vector)
        { // ojo de pez, deformacion de los objetos en transformX, rotacionX y escala, modifica solo la primera fila de la matriz
            float x = Vector4.Dot(mat.GetRow(0), vector);
            float y = Vector4.Dot(mat.GetRow(1), vector);
            float z = Vector4.Dot(mat.GetRow(2), vector);
            float w = Vector4.Dot(mat.GetRow(3), vector);

            return new Vector4(x, y, z, w);
        }
        public override bool Equals(object other)
        {
            if (!(other is MyMatrix4x4)) return false;
            return Equals((MyMatrix4x4)other);
        }
        public bool Equals(MyMatrix4x4 other)
        {
            if (GetColumn(0).Equals(other.GetColumn(0)) && GetColumn(1).Equals(other.GetColumn(1)) &&
                GetColumn(2).Equals(other.GetColumn(2)) && GetColumn(3).Equals(other.GetColumn(3)))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            int hashCode = 1266933948;
            hashCode = hashCode * -1521134295 + m00.GetHashCode();
            hashCode = hashCode * -1521134295 + m01.GetHashCode();
            hashCode = hashCode * -1521134295 + m02.GetHashCode();
            hashCode = hashCode * -1521134295 + m03.GetHashCode();
            hashCode = hashCode * -1521134295 + m10.GetHashCode();
            hashCode = hashCode * -1521134295 + m11.GetHashCode();
            hashCode = hashCode * -1521134295 + m12.GetHashCode();
            hashCode = hashCode * -1521134295 + m13.GetHashCode();
            hashCode = hashCode * -1521134295 + m20.GetHashCode();
            hashCode = hashCode * -1521134295 + m21.GetHashCode();
            hashCode = hashCode * -1521134295 + m22.GetHashCode();
            hashCode = hashCode * -1521134295 + m23.GetHashCode();
            hashCode = hashCode * -1521134295 + m30.GetHashCode();
            hashCode = hashCode * -1521134295 + m31.GetHashCode();
            hashCode = hashCode * -1521134295 + m32.GetHashCode();
            hashCode = hashCode * -1521134295 + m33.GetHashCode();
            hashCode = hashCode * -1521134295 + Transpose.GetHashCode();
            hashCode = hashCode * -1521134295 + rotation.GetHashCode();
            hashCode = hashCode * -1521134295 + LossyScale.GetHashCode();
            return hashCode;
        }
        public override string ToString()
        {
            return (
             "m00  " + m00 +
            "  m01  " + m01 +
            "  m02  " + m02 +
            "  m03  " + m03 +
            "\nm10  " + m10 +
            "  m11  " + m11 +
            "  m12  " + m12 +
            "  m13  " + m13 +
            "\nm20  " + m20 +
            "  m21  " + m21 +
            "  m22  " + m22 +
            "  m23  " + m23 +
            "\nm30  " + m30 +
            "  m31  " + m31 +
            "  m32  " + m32 +
            "  m33  " + m33);
        }
    }
}