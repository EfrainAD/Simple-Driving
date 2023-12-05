using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float accelerate = 0.1f;
    
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
            speed += (accelerate * Time.deltaTime);
    }
}
