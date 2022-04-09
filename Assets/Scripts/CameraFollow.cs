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
        cameraFollower = gameObject.GetComponent<Transform>(); //����������� �������� ���������
        startX = cameraFollower.eulerAngles.x; //���������� ���� ������ �� ��� x
        offset = Target.transform.position - cameraFollower.position; //���������� ������� ��������� ������ � ������� ��������
    }


    private void LateUpdate()
    {
        moveToTarget();
    }

    private void moveToTarget()
    {
        float desiredAngle = Target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0); //������ �������� ���� �������� ������ �� ��� y
        cameraFollower.position = Vector3.Lerp(cameraFollower.position, Target.transform.position - (rotation * offset), Time.deltaTime * Smooth);
        rotation = Quaternion.Euler(startX, desiredAngle, 0); //����������� ��������� ���� �������� x
        transform.rotation = rotation;
    }

}
