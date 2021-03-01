using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_miniFix : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fixVector = new Vector3(player.transform.position.x , player.transform.position.y , -10f);
        transform.position = fixVector;
    }
}
