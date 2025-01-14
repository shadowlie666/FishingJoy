using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFollow : MonoBehaviour
{
    public RectTransform UGUICanvas;
    public Camera mainCamera;
    
    void Update()
    {
        Vector2 mousePos;
        //ר����������������canvas���������ת������Ϊ����������canvas�ڵ�����λ�û���һ�������ţ�����Ҫר�ŵ����������
        //����˼���ǣ���mainCamera�µ�UGUICanvas�����µ��������תΪ�������꣬������mousePos��
        RectTransformUtility.ScreenPointToLocalPointInRectangle(UGUICanvas, new Vector2(Input.mousePosition.x, Input.mousePosition.y), mainCamera, out mousePos);
        float jiaoDu;
        if(mousePos.x>transform.position.x)
        {
            //����ʹ��vector3�µ�angle������ȡһ���Ƕȣ�����Ƕ��Ǻ������������ļнǣ��ڶ�������ʵ���Ͼ�������������γɵ�����
            jiaoDu = -Vector3.Angle(Vector3.up, (Vector3)mousePos - transform.position);
        }
        else
        {
            jiaoDu = Vector3.Angle(Vector3.up, (Vector3)mousePos - transform.position);
        }
        //����canvas��΢С���ţ����ж�canvas��������Ĳ�������Ҫ����local������localposition
        //����� Quaternion.Euler������Ԫ����ת�����������rotationֻ������Ԫ������Ҫ�����ת����������������������xyz����ת�ĽǶ�
        transform.localRotation = Quaternion.Euler(0, 0, jiaoDu);
        
    }
}
