using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_AutoMove : MonoBehaviour
{
    public float speed = 1;
    public Vector3 dir = Vector3.right;//���������x��������local���������ϵ�������Ѿ��������ֶ��Ĺ��ģ�


    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
