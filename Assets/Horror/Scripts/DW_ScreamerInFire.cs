using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DW_ScreamerInFire : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ennemy;
    [SerializeField] private AudioSource audio_source;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    //if pyromane use capacity -> StartCoroutine(Screamer());

    IEnumerator Screamer()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(ennemy);
        ennemy.transform.position = player.transform.position + new Vector3(0, 0, 10);
        audio_source.Play();
    }

}
