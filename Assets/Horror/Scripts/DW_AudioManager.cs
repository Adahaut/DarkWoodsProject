using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] audio_sources;
    public int audioToPlay;
    [SerializeField] private bool play_multiple_sound;
    [SerializeField] private bool loop_sound;
    [SerializeField] private float time_before_next_sound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //loop sound
            if(loop_sound)
            {
                StartCoroutine(LoopSound());
            }
            //basic sound
            if(!play_multiple_sound && !loop_sound)
            {
                PlayAudio(audioToPlay);
            }
            //multiple sounds
            else
            {
                StartCoroutine(WaitBeforeNextSound());
            }
        }
    }

    public void PlayAudio(int audioSource)
    {
        audio_sources[audioSource].Play();
    }

    IEnumerator WaitBeforeNextSound()
    {
        //foreach audio to play, play the audio and wait before playing the next one
        for(int i = 0; i < audio_sources.Length; i++)
        {
            PlayAudio(i);
            yield return new WaitForSeconds(time_before_next_sound);
        }
    }

    IEnumerator LoopSound()
    {
        //Play the sound in a loop for 15 seconds
        audio_sources[0].Play();
        audio_sources[0].loop = true;
        yield return new WaitForSeconds(15f);
        audio_sources[0].loop = false;
        audio_sources[0].Stop();
    }
}