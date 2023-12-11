using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_BackgroundMusic : MonoBehaviour
{
    [SerializeField] AudioSource audio_source;
    [SerializeField] AudioClip[] music_clips;
    private bool in_hospital = false;
    [SerializeField] GameObject steps_behind_the_player;

    private void Start()
    {
        audio_source.clip = music_clips[0];
        audio_source.Play();
    }

    private void Update()
    {
        //steps behind de player active only in the forest
        if(in_hospital)
        {
            steps_behind_the_player.SetActive(false);
        }
        else
        {
            steps_behind_the_player.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Switch the music from the forest to the hospital and vice versa
        if (other.gameObject.tag == "Player")
        {
            in_hospital = !in_hospital;

            if(in_hospital)
            {
                audio_source.clip = music_clips[1];
                audio_source.Play();
            }
            else
            {
                audio_source.clip = music_clips[0];
                audio_source.Play();
            }
        }
    }
}
