using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float topSpeed;
    [SerializeField] private float accelerate;
    [SerializeField] private float turnSpeed;
    
    private int turnDirection;

    void Update()
    {
        transform.Rotate(0f, turnDirection * turnSpeed * Time.deltaTime, 0f);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
        if (speed < topSpeed)
        {
            speed += (accelerate * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene("Scene_MainMenu");
        }
    }

    public void Steer(int direction)
    {
        turnDirection = direction;
    }
}
