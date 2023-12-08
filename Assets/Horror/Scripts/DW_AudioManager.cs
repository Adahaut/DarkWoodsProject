using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] audio_sources;
    public int audioToPlay;
    [SerializeField] private bool play_multiple_sound;
    [SerializeField] private bool play_heart_sound;
    [SerializeField] private float time_before_next_sound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(play_heart_sound)
            {
                StartCoroutine("HeartBeat");
            }
            
            if(!play_multiple_sound && !play_heart_sound)
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

    IEnumerator HeartBeat()
    {
        audio_sources[0].Play();
        audio_sources[0].loop = true;
        yield return new WaitForSeconds(9f);
        audio_sources[0].loop = false;
        audio_sources[0].Stop();
    }
}