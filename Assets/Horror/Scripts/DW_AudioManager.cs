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
            if(loop_sound)
            {
                StartCoroutine("LoopSound");
            }
            
            if(!play_multiple_sound && !loop_sound)
            {
                PlayAudio(audioToPlay);
            }
            else
            {
                StartCoroutine("WaitBeforeNextSound");
            }
        }
    }

    public void PlayAudio(int audioSource)
    {
        audio_sources[audioSource].Play();
    }

    IEnumerator WaitBeforeNextSound()
    {
        for(int i = 0; i < audio_sources.Length; i++)
        {
            PlayAudio(i);
            yield return new WaitForSeconds(time_before_next_sound);
            Debug.Log(i);
        }
    }

    IEnumerator LoopSound()
    {
        audio_sources[0].Play();
        audio_sources[0].loop = true;
        yield return new WaitForSeconds(15f);
        audio_sources[0].loop = false;
        audio_sources[0].Stop();
    }
}