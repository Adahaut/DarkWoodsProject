using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
