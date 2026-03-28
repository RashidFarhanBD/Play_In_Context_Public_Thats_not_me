using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestLocalizerUI : MonoBehaviour
{

    TextMeshProUGUI mytm;
    public string key = "hello_world";
    // Start is called before the first frame update
    void Start()
    {

        mytm = GetComponent<TextMeshProUGUI>();
        mytm.text= LocalizationSystem.GetLocalisedValue(key);

    }

    
}
