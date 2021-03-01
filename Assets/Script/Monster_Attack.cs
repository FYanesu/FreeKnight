using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Attack : MonoBehaviour
{
    public int face;
    private GameObject g_Enemy;
    private PolygonCollider2D coll2D;
    // Start is called before the first frame update
    void Start()
    {  
        g_Enemy = transform.parent.gameObject;   
    }

    // Update is called once per frame
    public void Abox1()
    {
        
        if(g_Enemy.GetComponent<Enemy>().g_faceDirection == 1)
            face = 0;
        else
            face = 1;
        coll2D = transform.GetChild(face).GetComponent<PolygonCollider2D>();
        coll2D.enabled = true;
        Invoke("resetAbox1",0.02f);
    }
    public void resetAbox1()
    {
        
        coll2D.enabled = false;
    }
}
