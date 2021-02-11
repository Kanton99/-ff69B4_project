using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OstomyUpdate : MonoBehaviour
{
    public MainController player;
    public Slider slider;

    private void Start()
    {

        player = GameObject.Find("Bran").GetComponent<MainController>();
    }

    private void Update()
    {
        slider.value = player.bs;
    }
}
