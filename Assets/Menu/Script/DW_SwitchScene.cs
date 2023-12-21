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
    public Vector3 spawn_pos = new Vector3();
    public DW_Character player;
    public bool isInHospital = false;
    public GameObject objects;


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

    private void SetObjectInScene()
    {
        if(objects!=null)
        {
            DW_ObjectDetection.Instance.ClearObject();
            Transform obj = objects.transform;
            for(int i = 0; i< obj.childCount; i++) 
            {
                DW_ObjectDetection.Instance.AddObject(obj.GetChild(i).gameObject);
            }
        }
    }

    public void OnFadeComplete()
    {
        for(int i = 0;  i < Hide_object.Count; i++)
        {
            if (Hide_object[i] != null)
                Hide_object[i].SetActive(false);

        }

        for(int i = 0; i < Show_object.Count; i++) 
        {
            Show_object[i].SetActive(true);
        }

        SetObjectInScene();



        player.PlayerTP(spawn_pos); // put the sound bloc on hospital spawn;
        DW_GridMap.Instance.Grid = new int[,]{
               // 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },  //0          // 1 = world border
                { 1,0,0,0,0,0,0,0,2,0,0,2,0,0,0,0,2,0,0,2,0,0,2,0,2,0,0,1 },  //1          // 0 = innaccessibility
                { 1,0,2,2,4,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,1 },  //2          // 5 = character
                { 1,0,2,0,0,0,0,2,0,0,0,2,0,0,0,0,2,0,0,0,2,0,0,0,0,2,0,1 },  //3          // 6 = spawn
                { 1,0,2,0,0,0,0,2,0,2,2,2,2,2,2,0,2,2,2,0,2,0,0,2,0,2,0,1 },  //4          // 2 = path
                { 1,0,2,0,2,2,0,2,0,2,2,0,2,2,2,2,0,2,2,0,2,2,2,2,0,2,0,1 },  //5          // 9 = exit
                { 1,0,2,0,0,2,2,2,0,2,0,0,0,0,0,0,0,0,2,0,2,0,0,0,0,2,0,1 },  //6          // 3 = statue de paladin
                { 1,0,2,0,0,0,0,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2,2,2,0,2,0,1 },  //7
                { 1,0,2,0,2,2,2,2,0,2,0,0,2,0,0,2,0,0,2,0,2,0,2,2,0,2,0,1 },  //8
                { 1,0,2,0,2,2,0,2,0,2,2,0,2,2,2,2,0,2,2,0,2,0,0,0,0,2,0,1 },  //9
                { 1,0,2,0,0,0,0,0,0,2,2,0,0,2,2,2,0,2,2,0,0,0,2,2,0,2,0,1 },  //0
                { 1,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,0,2,0,1 },  //1
                { 1,0,2,0,0,0,0,0,0,0,0,0,2,2,2,2,2,0,0,0,2,0,2,2,0,2,0,1 },  //2
                { 1,2,2,2,2,0,0,2,2,2,0,0,2,2,2,2,2,0,0,2,2,2,0,0,0,2,2,1 },  //3
                { 1,2,2,2,0,2,2,2,2,2,0,0,2,9,2,2,2,2,2,2,2,2,2,2,4,2,2,1 },  //4
                { 1,2,2,2,2,2,0,2,2,2,0,0,2,2,2,2,2,0,0,2,2,2,0,0,0,2,2,1 },  //5
                { 1,0,2,0,0,0,0,0,0,0,0,0,2,2,2,2,2,0,0,0,2,0,2,0,0,2,0,1 },  //6
                { 1,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,0,2,0,1 },  //7
                { 1,0,2,0,0,0,0,0,0,0,2,0,2,2,2,2,0,2,2,0,0,0,0,0,0,2,0,1 },  //8
                { 1,2,2,0,2,2,0,2,0,2,2,0,2,2,2,2,0,2,2,0,2,0,2,2,0,2,0,1 },  //9
                { 1,0,2,0,0,2,2,2,0,2,0,0,2,0,0,0,0,0,2,0,2,2,2,2,0,2,0,1 },  //0
                { 1,0,2,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,0,2,0,1 },  //1
                { 1,2,2,0,2,2,2,2,0,2,0,0,0,0,0,0,0,0,2,0,2,2,2,2,0,2,0,1 },  //2
                { 1,0,2,0,2,2,0,2,0,2,2,0,2,2,2,2,0,2,2,0,2,0,2,0,0,2,0,1 },  //3
                { 1,0,2,0,0,0,0,2,0,2,2,2,2,0,2,2,2,2,2,0,2,0,0,0,0,2,0,1 },  //4
                { 1,2,2,0,0,0,0,2,0,0,0,2,0,0,0,0,2,0,0,0,2,0,0,0,0,2,0,1 },  //5
                { 1,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,4,2,2,2,2,0,1 },  //6
                { 1,0,0,0,0,0,0,0,2,0,0,2,0,0,0,0,2,0,0,2,0,0,2,0,2,0,0,1 },  //7
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }   //8
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isInHospital = true;
            player = other.gameObject.GetComponent<DW_Character>();
            FadeToNextLevel();
        }
    }
}