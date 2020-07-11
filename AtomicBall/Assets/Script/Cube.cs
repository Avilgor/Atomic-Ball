using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] Transform dir;
    bool movement;

    void Start()
    {
        movement = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (movement && Time.deltaTime > 0)
        {
            Vector3 pos = transform.position;
            pos.x += dir.transform.forward.x * speed * Time.deltaTime;
            pos.z += dir.transform.forward.z * speed * Time.deltaTime;
            transform.position = pos;
        }
    }

    public void ActivateMovement() => movement = true;

    public void NextRound()
    {
        if (movement)
        {
            dir.Rotate(0, 180, 0);
            float angle = UnityEngine.Random.Range(-90, 90);
            dir.Rotate(0,angle,0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9) NextRound();
    }
}
