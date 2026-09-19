using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    void Update()
    {
        // 如果还没设置目标，自动寻找场景里的模型
        if (target == null)
        {
            GameObject obj = GameObject.Find("LoadedOBJModel");
            if (obj != null) target = obj.transform;
            else return;
        }

        // 按住鼠标左键拖动旋转
        if (Input.GetMouseButton(0))
        {
            float h = Input.GetAxis("Mouse X") * speed;
            float v = Input.GetAxis("Mouse Y") * speed;
            transform.RotateAround(target.position, Vector3.up, h);
            transform.RotateAround(target.position, transform.right, -v);
        }
    }
}