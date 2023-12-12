using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_RandomMovementBehindPlayer : MonoBehaviour
{
    private GameObject player;
    private float x_Min, z_Min, x_Max, z_Max;
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
        x_Min = player.transform.position.x - 15f;
        x_Max = player.transform.position.x + 15f;
        z_Min = player.transform.position.z - 3f;
        z_Max = player.transform.position.z - 15f;

        float randomX = Random.Range(x_Min, x_Max);
        float randomZ = Random.Range(z_Min, z_Max);

        target_position = new Vector3(randomX, player.transform.position.y, randomZ);

        //Change the pitch to diversify the enemy's sound
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
