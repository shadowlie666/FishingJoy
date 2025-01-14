using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_MoveTo : MonoBehaviour
{
    public GameObject goldCollect;

    void Start()
    {
        goldCollect = GameObject.Find("GoldPanel");
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,goldCollect.transform.position,5*Time.deltaTime);
    }
}
