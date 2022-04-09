using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform selfTransform;
    private Transform target;
    private void Start()
    {
        target = Camera.main.gameObject.transform; //Кешируем расположение главной камеры
        selfTransform = gameObject.GetComponent<Transform>(); //Кешируем собственное положение 
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main; 
    }

    private void Update()
    {
        selfTransform.LookAt(target);
    }
}
