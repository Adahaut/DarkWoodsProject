using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DW_search : MonoBehaviour
{
    public float speed = 0f;
    public Image radialIndicator = null;

    public void Search()
    {
        radialIndicator.enabled = true;
        radialIndicator.fillAmount += Time.deltaTime * speed;
    }
    public void ResetSlider()
    {
        radialIndicator.enabled = false;
        radialIndicator.fillAmount = 0;
    }
}
