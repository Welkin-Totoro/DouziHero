using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleColor : MonoBehaviour
{
    private Text txtTitle;
    public Gradient g;

    private void Start()
    {
        txtTitle = GetComponent<Text>();
    }

    private void Update()
    {
        txtTitle.color = g.Evaluate(Time.time % 8 / 8);
    }
}
