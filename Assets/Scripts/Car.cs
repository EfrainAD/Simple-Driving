using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float accelerate;
    [SerializeField] private float turnSpeed;
    
    private int turnDirection;

    void Update()
    {
        transform.Rotate(0f, turnDirection * turnSpeed * Time.deltaTime, 0f);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
            speed += (accelerate * Time.deltaTime);
    }
    
    public void Steer(int direction)
    {
        turnDirection = direction;
        Debug.Log("dir " + direction);
    }
}
