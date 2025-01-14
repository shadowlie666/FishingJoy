using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishShuXing : MonoBehaviour
{
    public int maxNum;
    public int maxSpeed;
    public int hp;
    public GameObject diePrefab;
    public GameObject goldPrefab;
    public int exp;
    public int gold;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bordere")
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int value)
    {
        hp -= value;
        if(hp<=0)
        {
            GameController.Instance.gold += gold;
            GameController.Instance.exp += exp;
           GameObject die = Instantiate(diePrefab);
            die.transform.SetParent(gameObject.transform.parent, false);
            die.transform.position = transform.position;
            die.transform.rotation = transform.rotation;
            GameObject goldGo = Instantiate(goldPrefab);
            goldGo.transform.SetParent(gameObject.transform.parent, false);
            goldGo.transform.position = transform.position;
            goldGo.transform.rotation = transform.rotation;
            if(gameObject.GetComponent<Ef_PlayEffect>() != null)
            {
                AudioManager.Instance.PlayEffectSound(AudioManager.Instance.rewardClip);
                gameObject.GetComponent<Ef_PlayEffect>().PlayEffect();
            }
            Destroy(gameObject);
        }
    }
}
