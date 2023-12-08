using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_RandomMovementBehindPlayer : MonoBehaviour
{
    private GameObject player;
    private float xMin, zMin, xMax, zMax;
    private Vector3 target_position;
    private float movement_speed = 2f;
    private float new_pitch;
    private AudioSource audio_source;

    void Start()
    {
        player = GameObject.Find("Player");
        audio_source = GetComponent<AudioSource>();
        SetRandomPosition();
    }

    void Update()
    {
        MoveToTargetPosition();
    }

    private void SetRandomPosition()
    {
        xMin = player.transform.position.x - 15f;
        xMax = player.transform.position.x + 15f;
        zMin = player.transform.position.z - 3f;
        zMax = player.transform.position.z - 15f;

        float randomX = Random.Range(xMin, xMax);
        float randomZ = Random.Range(zMin, zMax);

        target_position = new Vector3(randomX, -5f, randomZ);

        new_pitch = Random.Range(0, 10) * 0.1f;
        new_pitch += 2;
        audio_source.pitch = new_pitch;
    }

    private void MoveToTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, target_position, movement_speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target_position) < 0.1f)
        {
            SetRandomPosition();
        }
    }
}
