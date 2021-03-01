using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_hurt : MonoBehaviour
{
    private int damage;
    public float Swordspeed;
    private float bornTime;
    public float protectTime;
    // Start is called before the first frame update
    void Start()
    {
        bornTime = Time.time;
    }
    void Update ()
    {
        transform.Translate(Vector3.right * Time.deltaTime * Swordspeed, Space.Self);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        damage = Globel.ATK / 3;
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().beHited(damage,0);
        }
        if(other.gameObject.CompareTag("Boss"))
        {
            other.GetComponent<Boss>().beHited(damage,0);
        }
        //if(Time.time - bornTime >= protectTime)
        //    Destroy(gameObject);
    }

}
