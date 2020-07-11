using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    [SerializeField] GameObject ballMesh;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip ballHit;
    public float speed = 0.01f;
    float count;
    [SerializeField] float angle;
    float rotationAngle;
    bool locked;
    float time;

    void Start()
    {
        count = 0;
        locked = false;
        time = 1.0f;
    }

    void Update()
    {
        if (Time.deltaTime > 0 && !locked)
        {
            transform.Rotate(0, rotationAngle, 0);

            Vector3 pos = transform.position;        
            pos.z += transform.forward.z * speed;
            pos.x += transform.forward.x * speed;
            transform.position = pos;
            ballMesh.transform.Rotate(0, 0, speed);
            

            if (count < time) count += Time.deltaTime;           
            else
            {
                count = 0;
                rotationAngle = UnityEngine.Random.Range(-angle, angle);
            }
        }
    }

    public void NextRound(float sp,float t,float ag)
    {
        locked = false;
        if(speed < 0.2) speed += sp;
        if (time > 0.5) time -= t;
        if (angle < 3) angle += ag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Goal"))
        {
            GameObject.Find("GameController").GetComponent<GameController>().GoalReached();
            locked = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))        
        {
            if(!source.isPlaying) source.PlayOneShot(ballHit);
        }
        if (collision.gameObject.CompareTag("Triangle"))
        {
            locked = true;
            StartCoroutine(Stunned(3.0f));
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }

    IEnumerator Stunned(float time)
    {
        yield return new WaitForSeconds(time);
        locked = false;
    }
}
