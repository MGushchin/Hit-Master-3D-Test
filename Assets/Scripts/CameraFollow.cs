using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float Smooth = 5;
    private float startX;
    private Transform cameraFollower;
    private Vector3 offset;

    private void Start()
    {
        cameraFollower = gameObject.GetComponent<Transform>(); //Кеширование текущего положения
        startX = cameraFollower.eulerAngles.x; //Сохранение угла камеры по оси x
        offset = Target.transform.position - cameraFollower.position; //Сохранение разницы положений камеры и объекта слежения
    }


    private void LateUpdate()
    {
        moveToTarget();
    }

    private void moveToTarget()
    {
        float desiredAngle = Target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0); //Расчет текущего угла поворота только по оси y
        cameraFollower.position = Vector3.Lerp(cameraFollower.position, Target.transform.position - (rotation * offset), Time.deltaTime * Smooth);
        rotation = Quaternion.Euler(startX, desiredAngle, 0); //Возвращение исходного угла поворота x
        transform.rotation = rotation;
    }

}
