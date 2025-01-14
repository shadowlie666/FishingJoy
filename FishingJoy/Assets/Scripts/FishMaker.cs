using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMaker : MonoBehaviour
{
    public Transform fishHolder;//����������Ŀ�����
    public Transform[] genPosition;//�������λ�ã�������16���������λ�ã�
    public GameObject[] fishPrefabs;//���Ԥ���壨��18��������ͼ��

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
        int moveType = Random.Range(0, 2);//0����ֱ�ߣ�1��������ת��
        int angOffset;//ֱ�ߵ���б��
        int angSpeed; //ת��Ľ��ٶ�

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
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;//��ֹ��ͬ�����ص�����ÿ�����ɵ���һ����
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
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;//��ֹ��ͬ�����ص�����ÿ�����ɵ���һ����
            fish.AddComponent<Ef_AutoMove>().speed = speed;
            fish.AddComponent<Ef_AutoRotate>().speed = angSpeed;
            yield return new WaitForSeconds(fishGenWaitTime);
        }
    }

}
