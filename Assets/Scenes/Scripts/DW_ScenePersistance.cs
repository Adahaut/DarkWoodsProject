using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DW_ScenePersistance : MonoBehaviour
{
    [SerializeField] private GameObject persistentObject;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        persistentObject = this.gameObject;
        if (persistentObject != null)
            TransferObjectToNewScene();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void TransferObjectToNewScene()
    {
        GameObject existingObject = GameObject.Find(persistentObject.name);

        if (existingObject == null)
        {
            GameObject newPersistentObject = Instantiate(persistentObject);
            newPersistentObject.name = persistentObject.name;
        }
    }


    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TransferObjectToNewScene();
    }
}
