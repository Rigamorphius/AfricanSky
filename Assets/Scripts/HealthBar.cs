using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] IHaveHealth haveHealth; //should be set in the inspector to either the player or an enemy
    Image Health;
    float Fill;
    [SerializeField] Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        if(haveHealth == null)
            haveHealth = FindObjectOfType<Player>();
        //Looks for an event of OnHealthChanged in the IHaveHealth variable
        haveHealth.OnHealthChanged += HaveHealth_OnHealthChanged;

        if (slider == null)
            slider = GetComponent<Slider>();

        //Changes the value of the slider to the value of the IHaveHealth variable
        slider.maxValue = haveHealth.Health;
        slider.value = haveHealth.Health;
    }

    private void HaveHealth_OnHealthChanged(object sender, System.EventArgs e)
    {
        slider.value = haveHealth.Health;
    }
}
