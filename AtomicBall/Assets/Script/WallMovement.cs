using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    [SerializeField] KeyCode Up;
    [SerializeField] KeyCode Down;
    [SerializeField] KeyCode Right;
    [SerializeField] KeyCode Left;
    [SerializeField] float speed;
    
    
    void Update()
    {
        
        if (Time.deltaTime > 0)
        {
            if (Input.GetKey(Up))
            {
                Vector3 pos = transform.position;
                pos.z += speed * Time.deltaTime;
                transform.position = pos;
            }
            else if (Input.GetKey(Down))
            {               
                Vector3 pos = transform.position;
                pos.z -= speed * Time.deltaTime;
                transform.position = pos;                   
            }
            else if (Input.GetKey(Left))
            {              
                Vector3 pos = transform.position;
                pos.x -= speed * Time.deltaTime;
                transform.position = pos;               
            }
            else if (Input.GetKey(Right))
            {
                Vector3 pos = transform.position;
                pos.x += speed * Time.deltaTime;
                transform.position = pos;
            }
        }
    }
}
