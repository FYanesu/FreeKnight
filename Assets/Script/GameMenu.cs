using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Menu;
    public GameObject StartSelect;
    public GameObject OpenSelect;
    void Start()
    {
        Menu = transform.GetChild(7).gameObject;
        Globel.inMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Menu") && Globel.inMenu == false && Globel.HP > 0)
        {
            Menu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(OpenSelect);
            Globel.inMenu = true;
            Time.timeScale = 0.0f;
        }
        else if(Input.GetButtonDown("Menu") && Globel.inMenu == true)
        {
            Globel.inMenu = false;  
            Time.timeScale = 1.0f;
            Menu.SetActive(false);
            transform.GetChild(10).gameObject.SetActive(false);
        }
        if(Globel.inMenu == true && Globel.HP <= 0)
        {
            Time.timeScale = 1.0f;
            Globel.inMenu = false;  
            Menu.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        SoundManager.PlayP_select();
        Time.timeScale = 1.0f;
        Globel.inMenu = false;  
        Menu.SetActive(false);
    }
    public void mainMenu()
    {
        SoundManager.PlayP_select();
        Time.timeScale = 1.0f;
        Invoke("Iv_mainMenu",0.5f);
    }
    void Iv_mainMenu()
    {
        if(Globel.HP <= 0)
            Globel.stage = 1;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        SoundManager.PlayP_select();
        Time.timeScale = 1.0f;
        Invoke("Iv_QuitGame",0.5f);
    }
    void Iv_QuitGame()
    {
        Application.Quit();
    }
    public void OpenSetting()
    {
        SoundManager.PlayP_select();
        transform.GetChild(7).GetChild(4).GetComponent<Button>().interactable = false;
        transform.GetChild(7).GetChild(1).GetComponent<Button>().interactable = false;
        transform.GetChild(7).GetChild(2).GetComponent<Button>().interactable = false;
        transform.GetChild(7).GetChild(3).GetComponent<Button>().interactable = false;
        transform.GetChild(10).gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(StartSelect);
    }

}
