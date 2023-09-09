using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Player;

public class AudioScript : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        bool gameEnd = Player.gameEnd;

        if (gameEnd == true)
        {
            UnityEngine.Debug.Log("audio stop");
            audioSource.Stop();
        }
    }
}
