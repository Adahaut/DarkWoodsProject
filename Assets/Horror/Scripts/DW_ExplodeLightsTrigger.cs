using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DW_ExplodeLightsTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] lights;
    [SerializeField] private GameObject spawn_point;
    [SerializeField] private GameObject enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(ExplodeLights());
        }
    }

    IEnumerator ExplodeLights()
    {
        //Explode all the lights
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].GetComponent<AudioSource>().Play();
            lights[i].GetComponent<Light>().enabled = false;
            yield return new WaitForSeconds(1);
        }
        
        //desable collider
        gameObject.GetComponent<Collider>().enabled = false;

        //Reactivate the background light in red.
        Light light = lights[0].GetComponent<Light>();
        light.enabled = true;
        light.color = Color.red;
        light.intensity = 9;

        //spawn the enemy
        GameObject enemyToSpawn = Instantiate(enemy);
        enemyToSpawn.transform.parent = spawn_point.transform;
        enemyToSpawn.transform.position = spawn_point.transform.position;

        //play screamer sound
        gameObject.GetComponent<AudioSource>().Play();
    }
}