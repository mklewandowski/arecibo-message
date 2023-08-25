using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreciboManager : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField]
    GameObject HUDtitle;
    [SerializeField]
    GameObject HUDbuttons;
    [SerializeField]
    GameObject HUDarecibo;
    [SerializeField]
    GameObject HUDabout;
    [SerializeField]
    GameObject HUDabout2;
    [SerializeField]
    GameObject HUDareciboPanel;
    [SerializeField]
    GameObject AreciboSquarePrefab;
    GameObject[] AreciboSquares;
    int MaxSquares = 1679;

    [SerializeField]
    GameObject AreciboButtonPrefab;
    [SerializeField]
    Transform AreciboContainer;
    [SerializeField]
    TextMeshProUGUI AreciboButtonText;
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
    [SerializeField]
    AudioClip ButtonSound;
    [SerializeField]
    AudioClip SelectSound;

    [SerializeField]
    TextMeshProUGUI LanguageText;

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

    string message = "00000010101010000000000001010000010100000001001000100010001001011001010101010101010100100100000000000000000000000000000000000001100000000000000000001101000000000000000000011010000000000000000001010100000000000000000011111000000000000000000000000000000001100001110001100001100010000000000000110010000110100011000110000110101111101111101111101111100000000000000000000000000100000000000000000100000000000000000000000000001000000000000000001111110000000000000111110000000000000000000000011000011000011100011000100000001000000000100001101000011000111001101011111011111011111011111000000000000000000000000001000000110000000001000000000001100000000000000010000011000000000011111100000110000001111100000000001100000000000001000000001000000001000001000000110000000100000001100001100000010000000000110001000011000000000000000110011000000000000011000100001100000000011000011000000100000001000000100000000100000100000001100000000100010000000011000000001000100000000010000000100000100000001000000010000000100000000000011000000000110000000011000000000100011101011000000000001000000010000000000000010000011111000000000000100001011101001011011000000100111001001111111011100001110000011011100000000010100000111011001000000101000001111110010000001010000011000000100000110110000000000000000000000000000000000011100000100000000000000111010100010101010101001110000000001010101000000000000000010100000000000000111110000000000000000111111111000000000000111000000011100000000011000000000001100000001101000000000101100000110011000000011001100001000101000001010001000010001001000100100010000000010001010001000000000000100001000010000000000001000000000100000000000000100101000000000001111001111101001111000";
    bool isPlayingArecibo = false;
    int areciboPlayIndex = 0;
    float areciboPlayTimer = 0;
    float areciboPlayTimerMax = .25f;

    // Start is called before the first frame update
    void Start()
    {
        AreciboButtons = new GameObject[MaxButtons];

        audioSource = this.GetComponent<AudioSource>();

        for (int x = 0; x < MaxButtons; x++)
        {
            AreciboButtons[x] = Instantiate(AreciboButtonPrefab, AreciboContainer);
        }

        Globals.LoadUserSettings();
        SelectLanguage(Globals.CurrentLanguage);

        InitAboutPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (playTimer > 0)
        {
            playTimer -= Time.deltaTime;
            if (playTimer <= 0)
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

        if (areciboPlayTimer > 0)
        {
            areciboPlayTimer -= Time.deltaTime;
            if (areciboPlayTimer <= 0)
            {
                if (areciboPlayIndex < MaxSquares)
                {
                    if (AreciboSquares[areciboPlayIndex].GetComponent<AreciboSquare>().On)
                    {
                        AreciboSquares[areciboPlayIndex].GetComponent<AreciboSquare>().SquareImage.color = Color.white;
                        audioSource.PlayOneShot(OnSound[0], 1f);
                    }
                    else
                    {
                        AreciboSquares[areciboPlayIndex].GetComponent<AreciboSquare>().SquareImage.color = Color.black;
                        audioSource.PlayOneShot(OffSound, 1f);
                    }
                    areciboPlayTimer = areciboPlayTimerMax;
                }
                else
                {
                    isPlayingArecibo = false;
                    AreciboButtonText.text = "Play";
                }
                areciboPlayIndex++;
            }
        }
    }

    public void PlaySelectSound()
    {
        audioSource.PlayOneShot(SelectSound, 1f);
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
        audioSource.PlayOneShot(ButtonSound, 1f);
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
        audioSource.PlayOneShot(ButtonSound, 1f);
        showColors = !showColors;
        if (showColors)
            ColorPanel.GetComponent<MoveNormal>().MoveRight();
        else
            ColorPanel.GetComponent<MoveNormal>().MoveLeft();
    }

    public void SelectStart()
    {
        audioSource.PlayOneShot(ButtonSound, 1f);
        HUDtitle.GetComponent<MoveNormal>().MoveUp();
        HUDbuttons.GetComponent<MoveNormal>().MoveDown();
        HUDarecibo.GetComponent<MoveNormal>().MoveRight();
        HUDabout2.GetComponent<MoveNormal>().MoveRight();
    }

    public void SelectTutorial()
    {
        audioSource.PlayOneShot(ButtonSound, 1f);
        HUDtitle.GetComponent<MoveNormal>().MoveUp();
        HUDbuttons.GetComponent<MoveNormal>().MoveDown();
        HUDabout.GetComponent<MoveNormal>().MoveLeft();
        HUDarecibo.GetComponent<MoveNormal>().MoveLeft();
    }

    public void SelectTutorialNext()
    {
        audioSource.PlayOneShot(ButtonSound, 1f);
        HUDabout.GetComponent<MoveNormal>().MoveRight();
        HUDabout2.GetComponent<MoveNormal>().MoveLeft();
    }

    public void SelectBack()
    {
        if (isPlayingArecibo)
            SelectAreciboPlayStopButton();
        audioSource.PlayOneShot(ButtonSound, 1f);
        HUDtitle.GetComponent<MoveNormal>().MoveDown();
        HUDbuttons.GetComponent<MoveNormal>().MoveUp();
        HUDabout.GetComponent<MoveNormal>().MoveRight();
    }

    public void SelectHome()
    {
        if (isPlaying)
            SelectPlayStopButton();
        audioSource.PlayOneShot(ButtonSound, 1f);
        HUDtitle.GetComponent<MoveNormal>().MoveDown();
        HUDbuttons.GetComponent<MoveNormal>().MoveUp();
        HUDarecibo.GetComponent<MoveNormal>().MoveLeft();
    }

    public void SelectClear()
    {
        if (isPlaying)
            SelectPlayStopButton();
        audioSource.PlayOneShot(ButtonSound, 1f);

        for (int x = 0; x < AreciboButtons.Length; x++)
        {
            AreciboButtons[x].GetComponent<AreciboButton>().TurnOff();
        }
    }

    void InitAboutPanel()
    {
        AreciboSquares = new GameObject[MaxSquares];

        for (int x = 0; x < MaxSquares; x++)
        {
            char c = message[x];
            AreciboSquares[x] = Instantiate(AreciboSquarePrefab, HUDareciboPanel.transform);
            if (c == '1')
            {
                AreciboSquares[x].GetComponent<AreciboSquare>().On = true;
                AreciboSquares[x].GetComponent<AreciboSquare>().SquareImage.color = Color.white;
            }
            else
            {
                AreciboSquares[x].GetComponent<AreciboSquare>().On = false;
                AreciboSquares[x].GetComponent<AreciboSquare>().SquareImage.color = Color.black;
            }
        }
    }

    public void SelectAreciboPlayStopButton()
    {
        isPlayingArecibo = !isPlayingArecibo;
        if (isPlayingArecibo)
        {
            areciboPlayTimer = areciboPlayTimerMax;
            areciboPlayIndex = 0;
            AreciboButtonText.text = "Stop";
            for (int x = 0; x < MaxSquares; x++)
            {
                AreciboSquares[x].GetComponent<AreciboSquare>().SquareImage.color = new Color(118f/255f, 136f/255f, 169f/255f);
            }
        }
        else
        {
            for (int x = 0; x < MaxSquares; x++)
            {
                if (AreciboSquares[x].GetComponent<AreciboSquare>().On)
                {
                    AreciboSquares[x].GetComponent<AreciboSquare>().SquareImage.color = Color.white;
                }
                else
                {
                    AreciboSquares[x].GetComponent<AreciboSquare>().SquareImage.color = Color.black;
                }
            }
            AreciboButtonText.text = "Play Arecibo";
            areciboPlayTimer = 0;
        }

    }

    public void ToggleLanguage()
    {
        Debug.Log("ToggleLanguage");
        audioSource.PlayOneShot(ButtonSound, 1f);
        if (Globals.CurrentLanguage == Globals.Language.English)
            SelectLanguage(Globals.Language.Spanish);
        else
            SelectLanguage(Globals.Language.English);
    }

    public void SelectLanguage(Globals.Language newLang)
    {
        Globals.CurrentLanguage = newLang;
        if (Globals.CurrentLanguage == Globals.Language.English)
            LanguageText.text = "ESPAÑOL";
        else
            LanguageText.text = "ENGLISH";

        TranslateText[] textObjects = GameObject.FindObjectsOfType<TranslateText>(true);
        for (int i = 0; i < textObjects.Length; i++)
        {
            textObjects[i].UpdateText();
        }

        TranslateImage[] imageObjects = GameObject.FindObjectsOfType<TranslateImage>(true);
        for (int i = 0; i < imageObjects.Length; i++)
        {
            imageObjects[i].UpdateImage();
        }

        Globals.SaveIntToPlayerPrefs(Globals.LanguageStorageKey, (int)newLang);
    }

}
