using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingMes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Text>().text = "STAGE " + Globel.stage.ToString();
        Invoke("Load",2.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Load()
    {
        SceneManager.LoadScene(2);
    }
}
