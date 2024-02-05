using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void UpdataHealthBar(float curValue)
    {
        slider.value = curValue / 100;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
