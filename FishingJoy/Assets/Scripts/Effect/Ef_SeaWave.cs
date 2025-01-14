using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_SeaWave : MonoBehaviour
{
    private Vector3 temp;

    private void Start()
    {
        temp = new Vector3(-12, 1, 0);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, temp, 10 * Time.deltaTime);
    }
}
