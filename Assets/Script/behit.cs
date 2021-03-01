using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behit : MonoBehaviour
{
    private int damage;
    private int face;
    private GameObject m_Player;
    private GameObject attackBox;
    // Start is called before the first frame update
    void Start()
    {
        attackBox = transform.parent.gameObject;
        m_Player = attackBox.transform.parent.gameObject;
        //GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        damage = Globel.ATK;
        
        face = m_Player.GetComponent<HeroKnight>().m_facingDirection;
        //Debug.Log("被打了");
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().beHited(damage,face);
        }
        if(other.gameObject.CompareTag("Boss"))
        {
            other.GetComponent<Boss>().beHited(damage,face);
        }
    }
}
