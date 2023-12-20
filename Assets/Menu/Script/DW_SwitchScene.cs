using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DW_SwitchScene : MonoBehaviour
{
    //on trigger enter freeze position pour eviter les bugs -> lancer le fade / scene
   
    public Animator animator;
    [SerializeField] private List<GameObject> Show_object;
    [SerializeField] private List<GameObject> Hide_object;
    [SerializeField] private Vector3 spawn_pos = new Vector3();
    public DW_Character player;


    public void FadeToNextLevel()
    {
        //FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
        OnFadeComplete();
    }

    public void FadeToLevel(int levelIndex)
    {
        //level_to_load = levelIndex;
        //animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        for(int i = 0;  i < Hide_object.Count; i++)
        {
            Hide_object[i].SetActive(false);

        }

        for(int i = 0; i < Show_object.Count; i++) 
        {
            Show_object[i].SetActive(true);
        }

            
            player.PlayerTP(spawn_pos); // put the sound bloc on hospital spawn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player = other.gameObject.GetComponent<DW_Character>();
            FadeToNextLevel();
        }
    }
}