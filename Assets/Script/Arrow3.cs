using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow3 : MonoBehaviour
{
    public int damage;
    private float bornTime;
    public float protectTime;
    bool unDestory;

    private GameObject PlayerUnit;
    private Transform  PlayerLocation;//玩家位置
    // Start is called before the first frame update
    void Start()
    {
        bornTime = Time.time;
        PlayerUnit = GameObject.FindGameObjectWithTag("Player");
        PlayerLocation = PlayerUnit.transform.GetChild(6).transform;
        Invoke("Iv_Attack",0.2f);
    }
    void Update ()
    {
        transform.Translate(Vector3.up * Time.deltaTime * Globel.Arrowspeed, Space.Self);
        
    }
    void Iv_Attack()
    {
        transform.up = (PlayerLocation.position - transform.position).normalized;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        unDestory = false;
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<HeroKnight>().beHited(damage,0);
            if(other.GetComponent<HeroKnight>().m_rolling)
                unDestory = true;
        }
        if(other.gameObject.CompareTag("Enemy"))unDestory = true;
        if(Time.time - bornTime >= protectTime && unDestory == false)
            Destroy(gameObject);
    }
}
