using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_AutoMove : MonoBehaviour
{
    public float speed = 1;
    public Vector3 dir = Vector3.right;//自身坐标的x轴正方向（local坐标的坐标系正方向已经被我们手动改过的）


    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
