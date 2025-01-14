using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_waterwave : MonoBehaviour
{
    public Texture[] textures;  //存放要播放的动画图片
    private Material material;  //游戏物体的材质
    private int index = 0;   //播放图片的下标

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        InvokeRepeating("ChangeTexture", 0, 0.1f);
    }

    void ChangeTexture()
    {
        material.mainTexture = textures[index];
        index = (index + 1) % textures.Length;  //在播放完一次图片后让他循环回去从数组第一个元素开始播放
    }
}
