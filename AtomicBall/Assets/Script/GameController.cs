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
    int roundcounter;
    bool goalReached;
    float counter;

    void Start()
    {
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
        else ball.GetComponent<Ball>().NextRound(speedIncrease,0.05f,0.2f);

        if (roundcounter > 6) cube.SetActive(true);
        if (cube.activeSelf)
        {
            point.x = UnityEngine.Random.Range(minX, maxX);
            point.z = UnityEngine.Random.Range(minY, maxY);
            point.y = 1.0f;
            cube.transform.position = point;

            if (roundcounter == 12) cube.GetComponent<Cube>().ActivateMovement();
            cube.GetComponent<Cube>().NextRound();
        }

        if (roundcounter > 12)
        {
            point.x = UnityEngine.Random.Range(minX, maxX);
            point.z = UnityEngine.Random.Range(minY, maxY);
            point.y = 1.5f;
            cone.transform.position = point;

            cone.SetActive(true);
        }

        goal.SetActive(true);
        goalGreen.SetActive(false);
        counter = 0;
        goalReached = false;
    }
}
