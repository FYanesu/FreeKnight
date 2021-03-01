using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Hurt : MonoBehaviour
{
    public int damage;
    private float bornTime;
    public float protectTime;
    bool unDestory;
    // Start is called before the first frame update
    void Start()
    {
        bornTime = Time.time;
    }
    void Update ()
    {
        transform.Translate(Vector3.up * Time.deltaTime * Globel.Arrowspeed, Space.Self);
        
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
