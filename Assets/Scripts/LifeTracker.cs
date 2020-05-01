using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LifeTracker : MonoBehaviour
{
    IHaveLives haveLives;
    [SerializeField] Text textField;

    void Start()
    {
        if (haveLives == null)
            haveLives = FindObjectOfType<Player>();
        haveLives.OnLivesChanged += HaveLives_OnLivesChanged;

        if (textField == null)
            textField = GetComponent<Text>();

        textField.text = haveLives.Lives.ToString();
    }

    private void HaveLives_OnLivesChanged(object sender, System.EventArgs e)
    {
        textField.text = haveLives.Lives.ToString();
    }
}
