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
        //专门用于外面物体与canvas物体坐标的转换，因为外面物体与canvas内的物体位置会有一定的缩放，所以要专门调用这个方法
        //其意思就是，将mainCamera下的UGUICanvas物体下的鼠标坐标转为世界坐标，保存在mousePos下
        RectTransformUtility.ScreenPointToLocalPointInRectangle(UGUICanvas, new Vector2(Input.mousePosition.x, Input.mousePosition.y), mainCamera, out mousePos);
        float jiaoDu;
        if(mousePos.x>transform.position.x)
        {
            //就是使用vector3下的angle方法获取一个角度，这个角度是后面两个向量的夹角，第二个参数实际上就是两个点相减形成的向量
            jiaoDu = -Vector3.Angle(Vector3.up, (Vector3)mousePos - transform.position);
        }
        else
        {
            jiaoDu = Vector3.Angle(Vector3.up, (Vector3)mousePos - transform.position);
        }
        //由于canvas的微小缩放，所有对canvas里面物体的操作都需要加上local，比如localposition
        //这里的 Quaternion.Euler就是四元数的转换，由于这个rotation只接受四元数所以要用这个转换，后面三个参数就是绕xyz轴旋转的角度
        transform.localRotation = Quaternion.Euler(0, 0, jiaoDu);
        
    }
}
