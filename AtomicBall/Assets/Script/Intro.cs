using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    [SerializeField] VideoPlayer player;

    void Update()
    {
        if (player.isPrepared && !player.isPlaying) SceneManager.LoadScene(1);
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(1);
    }
}
