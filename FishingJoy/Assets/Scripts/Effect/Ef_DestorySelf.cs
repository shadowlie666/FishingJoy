using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_DestorySelf : MonoBehaviour
{
    public float delay = 1f;

    private void Start()
    {
        Destroy(gameObject, delay);
    }
}
