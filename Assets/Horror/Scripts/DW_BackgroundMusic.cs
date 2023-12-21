using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_BackgroundMusic : MonoBehaviour
{
    [SerializeField] AudioSource audio_source;
    [SerializeField] AudioClip[] music_clips;
    [SerializeField] DW_PlayerSound player_sound;
    [SerializeField] GameObject steps_behind_the_player;
    [SerializeField] GameObject Lamp;

    private void Start()
    {
        audio_source.clip = music_clips[0];
        audio_source.Play();
    }

    private void Update()
    {
        //steps behind de player active only in the forest
        if(player_sound.inHospital)
        {
            steps_behind_the_player.SetActive(false);
            if(Lamp != null) 
                Lamp.SetActive(false);
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
            player_sound.inHospital = !player_sound.inHospital;

            if(player_sound.inHospital)
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
