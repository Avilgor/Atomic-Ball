using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject goal;
    [SerializeField] GameObject goalGreen;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject cube;
    [SerializeField] GameObject cone;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] float speedIncrease;
    [SerializeField] AudioClip[] songs;
    AudioSource source;
    int roundcounter;
    bool goalReached;
    float counter;

    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        source.clip = songs[UnityEngine.Random.Range(0, songs.Length)];
        source.Play();

        Application.targetFrameRate = 60;
        Vector3 point = transform.position;
        point.x = UnityEngine.Random.Range(minX,maxX);
        point.z = UnityEngine.Random.Range(minY, maxY);
        point.y = 1;
        ball.transform.position = point;

        point.x = UnityEngine.Random.Range(minX, maxX);
        point.z = UnityEngine.Random.Range(minY, maxY);
        point.y = 0.2f;
        goal.transform.position = point;
        roundcounter = 0;
        goalReached = false;
        counter = 0;
        roundText.SetText(roundcounter.ToString());

        if (cube.activeSelf) cube.SetActive(false);
        if (goalGreen.activeSelf) goalGreen.SetActive(false);
        if (cone.activeSelf) cone.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) GoalReached();
        if (goalReached)
        {
            if (counter < 1.0f) counter += Time.deltaTime;
            else Reposition();
        }

        if (Time.deltaTime == 0) source.Pause();
        else if(!source.isPlaying) source.Play();
    }

    public void GoalReached()
    {
        goalReached = true;
        goal.SetActive(false);
        goalGreen.transform.position = goal.transform.position;
        goalGreen.SetActive(true);
    }

    private void Reposition()
    {
        Vector3 point = transform.position;
        point.x = UnityEngine.Random.Range(minX, maxX);
        point.z = UnityEngine.Random.Range(minY, maxY);
        point.y = 0.5f;
        ball.transform.position = point;

        point.x = UnityEngine.Random.Range(minX, maxX);
        point.z = UnityEngine.Random.Range(minY, maxY);
        point.y = 0.2f;
        goal.transform.position = point;

       

        

        roundcounter++;
        roundText.SetText(roundcounter.ToString());

        if (roundcounter % 3 != 0) ball.GetComponent<Ball>().NextRound(0,0,0);
        else ball.GetComponent<Ball>().NextRound(speedIncrease,1f,0.2f);

        if (roundcounter == 5) cube.SetActive(true);
        if (cube.activeSelf)
        {
            point.x = UnityEngine.Random.Range(minX, maxX);
            point.z = UnityEngine.Random.Range(minY, maxY);
            point.y = 1.0f;
            cube.transform.position = point;

            if (roundcounter == 9) cube.GetComponent<Cube>().ActivateMovement();
            cube.GetComponent<Cube>().NextRound();
        }

        if (roundcounter > 11)
        {
            point.x = UnityEngine.Random.Range(minX, maxX);
            point.z = UnityEngine.Random.Range(minY, maxY);
            point.y = 0.5f;
            cone.transform.position = point;
            if (roundcounter == 12) cone.GetComponent<Cube>().ActivateMovement();
            cone.SetActive(true);
            cone.GetComponent<Cube>().NextRound();
        }

        goal.SetActive(true);
        goalGreen.SetActive(false);
        counter = 0;
        goalReached = false;
    }
}
