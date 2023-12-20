using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DW_SwitchScene : MonoBehaviour
{
    //on trigger enter freeze position pour eviter les bugs -> lancer le fade / scene
   
    public Animator animator;
    [SerializeField] private int level_to_load;
    [SerializeField] private int current_level;
    [SerializeField] List<GameObject> maps = new List<GameObject>();
    [SerializeField] List<Vector3> spawn_pos = new List<Vector3>();
    public bool change_map;
    public DW_Character player;


    public void FadeToNextLevel()
    {
        //FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
        OnFadeComplete();
    }

    public void FadeToLevel(int levelIndex)
    {
        level_to_load = levelIndex;
        //animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        if(!change_map)
            SceneManager.LoadScene(level_to_load);
        else 
        {
            maps[current_level].SetActive(false);
            maps[level_to_load].SetActive(true);
            
            player.PlayerTP(spawn_pos[level_to_load]); // put the sound bloc on hospital spawn;
        }
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
