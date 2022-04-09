using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputControl : MonoBehaviour
{
    public UnityEvent<Vector3> OnTap = new UnityEvent<Vector3>(); // Событие при каждом нажатии на экран
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main.gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                OnTap.Invoke(hit.point); //Оповещение с точкой касания мыши 
        }
    }
}


