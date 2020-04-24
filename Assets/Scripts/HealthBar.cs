using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image Health;
    public float Fill;
    // Start is called before the first frame update
    void Start()
    {
        Fill = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Health.fillAmount = Fill;
    }
}
