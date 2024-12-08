using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera camera;
 

    public void UpdateHealth(float currentValue, float maxValue){
        Debug.Log("bar down\n");
        slider.value = currentValue / maxValue;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
    }
}
