using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_waterwave : MonoBehaviour
{
    public Texture[] textures;  //���Ҫ���ŵĶ���ͼƬ
    private Material material;  //��Ϸ����Ĳ���
    private int index = 0;   //����ͼƬ���±�

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        InvokeRepeating("ChangeTexture", 0, 0.1f);
    }

    void ChangeTexture()
    {
        material.mainTexture = textures[index];
        index = (index + 1) % textures.Length;  //�ڲ�����һ��ͼƬ������ѭ����ȥ�������һ��Ԫ�ؿ�ʼ����
    }
}
