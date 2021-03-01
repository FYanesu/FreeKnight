using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hurt : MonoBehaviour
{
    private int damage;
    private int face;
    private GameObject g_Enemy;
    private GameObject attackBox;
    // Start is called before the first frame update
    void Start()
    {
        attackBox = transform.parent.gameObject;
        g_Enemy = attackBox.transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        damage = g_Enemy.GetComponent<Enemy>().g_attack;
        face = g_Enemy.GetComponent<Enemy>().g_faceDirection;
        
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<HeroKnight>().beHited(damage,face);
        }
    }
}
