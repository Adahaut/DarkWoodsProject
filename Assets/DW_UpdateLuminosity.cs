using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DW_UpdateLuminosity : MonoBehaviour
{
    private void OnEnable()
    {
        this.GetComponent<Slider>().value = GameObject.Find("luminosity").GetComponent<Image>().color.a / 255;
    }
}
