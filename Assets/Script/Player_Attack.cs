using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    
    public int face;
    public int attacktimes;
    private GameObject m_Player;
   // private Animator anim;
    private PolygonCollider2D coll2D;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = transform.parent.gameObject;
        //anim = m_Player.GetComponent<Animator>();
        
    }

    public void Abox1()
    {
        
        attacktimes = m_Player.GetComponent<HeroKnight>().m_currentAttack;
        if(m_Player.GetComponent<HeroKnight>().m_facingDirection == 1)
            face = 0;
        else
            face = 3;
        coll2D = transform.GetChild(attacktimes+face-1).GetComponent<PolygonCollider2D>();
        coll2D.enabled = true;
        Invoke("resetAbox1",0.02f);
    }
    public void resetAbox1()
    {
        
        coll2D.enabled = false;
    }

}
