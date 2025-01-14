using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebAttr : MonoBehaviour
{
    public float disappearTime;
    public int damage;

    private void Start()
    {
        Destroy(gameObject, disappearTime);  //destory具有一个重载方法，可以设置多少秒后销毁
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fish")
        {
            collision.SendMessage("TakeDamage", damage);
        }
    }
}
