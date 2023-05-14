using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreciboButton : MonoBehaviour
{
    AreciboManager areciboManager;

    public Image FillImage;
    [SerializeField]
    GameObject HighlightImage;

    public bool isOn = false;

    void Start()
    {
        GameObject go = GameObject.Find("AreciboCanvas");
        areciboManager = go.GetComponent<AreciboManager>();
    }

    public void SelectAreciboButton()
    {
        areciboManager.PlaySelectSound();
        isOn = !isOn;
        FillImage.color = isOn
            ? areciboManager.GetCurrentColor()
            : Color.black;
    }

    public void ToggleImageHighlight(bool show)
    {
        HighlightImage.SetActive(show);
    }
}
