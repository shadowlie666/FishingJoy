using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMaker : MonoBehaviour
{
    public Transform fishHolder;//用于生成鱼的空物体
    public Transform[] genPosition;//鱼的生成位置（边上那16个空物体的位置）
    public GameObject[] fishPrefabs;//鱼的预制体（那18个动画贴图）

    public float fishGenWaitTime = 2f;
    public float waveGenWaitTime = 0.3f;

    void Start()
    {
        InvokeRepeating("MakeFishes", 0, waveGenWaitTime);
    }

    void MakeFishes()
    {
        int genPosIndex = Random.Range(0, genPosition.Length);
        int fishPreIndex = Random.Range(0, fishPrefabs.Length);
        int maxNum = fishPrefabs[fishPreIndex].GetComponent<FishShuXing>().maxNum;
        int maxSpeed = fishPrefabs[fishPreIndex].GetComponent<FishShuXing>().maxSpeed;
        int num = Random.Range((maxNum / 2) + 1, maxNum);
        int speed = Random.Range(maxSpeed / 2, maxSpeed);
        int moveType = Random.Range(0, 2);//0代表直走，1代表曲线转弯
        int angOffset;//直走的倾斜角
        int angSpeed; //转弯的角速度

        if(moveType == 0)
        {
            angOffset = Random.Range(-22, 22);
            StartCoroutine(GenStraightFish(genPosIndex, fishPreIndex, num, speed, angOffset));
        }
        else
        {
            if(Random.Range(0,2) == 0)
            {
                angSpeed = Random.Range(-15, -9);
            }
            else
            {
                angSpeed = Random.Range(9, 15);
            }
            StartCoroutine(GenTrunFish(genPosIndex, fishPreIndex, num, speed, angSpeed));
        }
    }

    IEnumerator GenStraightFish(int genPosIndex, int fishPreIndex, int num,int speed,int angOffset)
    {
        for(int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[fishPreIndex]);
            fish.transform.SetParent(fishHolder, false);
            fish.transform.localPosition = genPosition[genPosIndex].localPosition;
            fish.transform.localRotation = genPosition[genPosIndex].localRotation;
            fish.transform.Rotate(0, 0, angOffset);
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;//防止相同的鱼重叠，给每个生成的鱼一个层
            fish.AddComponent<Ef_AutoMove>().speed = speed; 
            yield return new WaitForSeconds(fishGenWaitTime);
        }
    }

    IEnumerator GenTrunFish(int genPosIndex, int fishPreIndex, int num, int speed, int angSpeed)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[fishPreIndex]);
            fish.transform.SetParent(fishHolder, false);
            fish.transform.localPosition = genPosition[genPosIndex].localPosition;
            fish.transform.localRotation = genPosition[genPosIndex].localRotation;
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;//防止相同的鱼重叠，给每个生成的鱼一个层
            fish.AddComponent<Ef_AutoMove>().speed = speed;
            fish.AddComponent<Ef_AutoRotate>().speed = angSpeed;
            yield return new WaitForSeconds(fishGenWaitTime);
        }
    }

}
