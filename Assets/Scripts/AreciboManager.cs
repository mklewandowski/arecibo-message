using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreciboManager : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField]
    GameObject AreciboButtonPrefab;
    [SerializeField]
    Transform AreciboContainer;
    [SerializeField]
    TextMeshProUGUI ButtonText;
    [SerializeField]
    AudioClip[] OnSound;
    [SerializeField]
    AudioClip OffSound;
    [SerializeField]
    GameObject ColorPanel;
    [SerializeField]
    Transform[] ColorButtons;

    Color currentColor = Color.white;

    [SerializeField]
    int MaxButtons = 100;

    GameObject[] AreciboButtons;
    bool isPlaying = false;
    int playIndex = 0;
    int playIndexMax = 99;
    float playTimer = 0;
    float playTimerMax = .33f;

    bool showColors = false;

    // Start is called before the first frame update
    void Start()
    {
        AreciboButtons = new GameObject[MaxButtons];

        audioSource = this.GetComponent<AudioSource>();

        for (int x = 0; x < MaxButtons; x++)
        {
            AreciboButtons[x] = Instantiate(AreciboButtonPrefab, AreciboContainer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playTimer > 0)
        {
            playTimer -= Time.deltaTime;
            if (playTimer < 0)
            {
                AreciboButtons[playIndex].GetComponent<AreciboButton>().ToggleImageHighlight(false);
                playIndex++;
                if (playIndex <= playIndexMax)
                {
                    AreciboButtons[playIndex].GetComponent<AreciboButton>().ToggleImageHighlight(true);
                    playTimer = playTimerMax;
                    if (AreciboButtons[playIndex].GetComponent<AreciboButton>().isOn)
                    {
                        Color buttonColor = AreciboButtons[playIndex].GetComponent<AreciboButton>().FillImage.color;
                        if (buttonColor == Color.white)
                            audioSource.PlayOneShot(OnSound[0], 1f);
                        else if (buttonColor == Color.red)
                            audioSource.PlayOneShot(OnSound[1], 1f);
                        else if (buttonColor == Color.green)
                            audioSource.PlayOneShot(OnSound[2], 1f);
                        else if (buttonColor == Color.blue)
                            audioSource.PlayOneShot(OnSound[3], 1f);
                        else if (buttonColor == Color.yellow)
                            audioSource.PlayOneShot(OnSound[4], 1f);

                    }
                    else
                        audioSource.PlayOneShot(OffSound, 1f);
                }
                else
                {
                    isPlaying = false;
                    ButtonText.text = "Play Message";
                }
            }
        }
    }

    public void SelectPlayStopButton()
    {
        isPlaying = !isPlaying;
        if (isPlaying)
        {
            ButtonText.text = "Stop Message";
            playTimer = playTimerMax;
            playIndex = 0;
            AreciboButtons[playIndex].GetComponent<AreciboButton>().ToggleImageHighlight(true);
            if (AreciboButtons[playIndex].GetComponent<AreciboButton>().isOn)
            {
                Color buttonColor = AreciboButtons[playIndex].GetComponent<AreciboButton>().FillImage.color;
                if (buttonColor == Color.white)
                    audioSource.PlayOneShot(OnSound[0], 1f);
                else if (buttonColor == Color.red)
                    audioSource.PlayOneShot(OnSound[1], 1f);
                else if (buttonColor == Color.green)
                    audioSource.PlayOneShot(OnSound[2], 1f);
                else if (buttonColor == Color.blue)
                    audioSource.PlayOneShot(OnSound[3], 1f);
                else if (buttonColor == Color.yellow)
                    audioSource.PlayOneShot(OnSound[4], 1f);
            }
            else
                audioSource.PlayOneShot(OffSound, 1f);
        }
        else
        {
            ButtonText.text = "Play Message";
            playTimer = 0;
            AreciboButtons[playIndex].GetComponent<AreciboButton>().ToggleImageHighlight(false);
        }
    }

    public void SetCurrentColor(int colorIndex)
    {
        if (colorIndex == 0)
            currentColor = Color.white;
        else if (colorIndex == 1)
            currentColor = Color.red;
        else if (colorIndex == 2)
            currentColor = Color.green;
        else if (colorIndex == 3)
            currentColor = Color.blue;
        else if (colorIndex == 4)
            currentColor = Color.yellow;

        for (int x = 0; x < ColorButtons.Length; x++)
        {
            float scale = x == colorIndex ? 1.1f : 1f;
            ColorButtons[x].localScale = new Vector3(scale, scale, 1f);
        }
    }

    public Color GetCurrentColor()
    {
        return currentColor;
    }

    public void ToggleColorPanel()
    {
        showColors = !showColors;
        if (showColors)
            ColorPanel.GetComponent<MoveNormal>().MoveRight();
        else
            ColorPanel.GetComponent<MoveNormal>().MoveLeft();
    }
}
