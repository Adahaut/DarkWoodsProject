using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] steps_clips;
    [SerializeField] private AudioSource steps_audio_source;

    private void Start()
    {
        PlayerSteps();
    }

    //choose a random steps sound
    public void PlayerSteps()
    {
        int clipToPlay = Random.Range(0, steps_clips.Length);
        steps_audio_source.clip = steps_clips[clipToPlay];
    }
}
