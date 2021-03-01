using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem_M : MonoBehaviour
{
    private GameObject          PlayerUnit;
    private float               i_distanceToPlayer;
    public float                i_moveToPlayer;
    private Transform           PlayerLocation;
    private Vector2             i_chaseMove;
    public  float               i_speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        PlayerUnit = GameObject.FindGameObjectWithTag("Player");
        //PlayerLocation = PlayerUnit.transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        i_distanceToPlayer = Vector2.Distance(PlayerUnit.transform.position, transform.position);
        i_chaseMove = PlayerUnit.transform.position - transform.position;
        if(i_distanceToPlayer < i_moveToPlayer)  
            transform.Translate(i_chaseMove * Time.deltaTime * i_speed) ;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player") &&
            other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            SoundManager.PlayP_coin();
            Globel.Coin += 1;
            Destroy(gameObject);
        }    
    }
}
