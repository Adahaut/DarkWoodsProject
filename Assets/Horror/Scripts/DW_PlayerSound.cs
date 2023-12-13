using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_PlayerSound : MonoBehaviour
{
    public bool inHospital = false;
    [SerializeField] private AudioClip[] forest_steps_clips;
    [SerializeField] private AudioClip[] hospital_steps_clips;
    [SerializeField] private AudioSource steps_audio_source;

    //choose a random steps sound
    public void PlayerSteps()
    {
        if(!inHospital)
        {
            int clipToPlay = Random.Range(0, forest_steps_clips.Length);
            steps_audio_source.clip = forest_steps_clips[clipToPlay];
        }
        else
        {
            int clipToPlay = Random.Range(0, hospital_steps_clips.Length);
            steps_audio_source.clip = hospital_steps_clips[clipToPlay];
        }        
    }

    public void PlayStepsSound()
    {
        steps_audio_source.Play();
    }
}
