using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DW_DontDestroy : MonoBehaviour
{
    public bool disableOnStart = false;
    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(this.gameObject);

        if(disableOnStart)
            this.gameObject.SetActive(false);
    }
}
