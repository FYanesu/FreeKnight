using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameSetting : MonoBehaviour
{
    public static GameSetting instance;
    public AudioSource BgmSrc;
    public Slider BgmVolume;

    public AudioSource SeSrc;
    public Slider SeVolume;

    public Toggle FullSetting;

    public Dropdown ScreenSetting;

    public GameObject StartSelect;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        BgmVolume.value = Globel.BGMVolume;
        SeVolume.value = Globel.SEVolume;
        BgmSrc.volume = Globel.BGMVolume;
        SeSrc.volume = Globel.SEVolume;
        ScreenSetting.value = Globel.ScreenSetting;
        switch (ScreenSetting.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, FullSetting.isOn);
                break;
            case 1:
                Screen.SetResolution(1366, 768, FullSetting.isOn);
                break;
            case 2:
                Screen.SetResolution(1280, 720, FullSetting.isOn);
                break;
            case 3:
                Screen.SetResolution(960, 540, FullSetting.isOn);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Roll"))
            ExitSetting();
    }

    public void settingActive()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }
    public void settingUnActive()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }
    public void BGMvolumeChange()
    {
        Globel.BGMVolume = BgmVolume.value;
        BgmSrc.volume = Globel.BGMVolume;
    }
    public void SEvolumeChange()
    {
        Globel.SEVolume = SeVolume.value;
        SeSrc.volume = Globel.SEVolume;
    }
    public void FullScreenSetting()
    {
        SoundManager.PlayP_select();
        if(FullSetting.isOn)
            Screen.fullScreen = true;
        else  
            Screen.fullScreen = false;
    }
    public void ScreenRevolutionSetting()
    {
        switch (ScreenSetting.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, FullSetting.isOn);
                break;
            case 1:
                Screen.SetResolution(1366, 768, FullSetting.isOn);
                break;
            case 2:
                Screen.SetResolution(1280, 720, FullSetting.isOn);
                break;
            case 3:
                Screen.SetResolution(960, 540, FullSetting.isOn);
                break;
        }
        Globel.ScreenSetting = ScreenSetting.value;
        SoundManager.PlayP_select();
    }
    public void ExitSetting()
    {
        //SoundManager.PlayP_select();
        transform.GetChild(7).GetChild(4).GetComponent<Button>().interactable = true;
        transform.GetChild(7).GetChild(1).GetComponent<Button>().interactable = true;
        transform.GetChild(7).GetChild(2).GetComponent<Button>().interactable = true;
        transform.GetChild(7).GetChild(3).GetComponent<Button>().interactable = true;
        EventSystem.current.SetSelectedGameObject(StartSelect);
        transform.GetChild(10).gameObject.SetActive(false);
    }
}
