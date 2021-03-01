using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow2 : MonoBehaviour
{
    public int damage;
    private float bornTime;
    public float protectTime;
    public bool unDestory;
    public float scale = 3.0f;
    private float nextScale;
    private float fixSpeed;
    public GameObject fireball02;
    public bool type2 = false;

    // Start is called before the first frame update
    void Start()
    {
        bornTime = Time.time;
        nextScale = scale - 1.0f;
        fixSpeed = 1.0f/scale;
    }
    void Update ()
    {
        transform.Translate(Vector3.up * Time.deltaTime * Globel.Arrowspeed * fixSpeed, Space.Self);
        if(type2)
            Invoke("divide2",0.7f);
        else
            Invoke("divide",0.7f);
        
    }
    void divide()
    {
        if(scale > 1.0f)
        {
            for (float i = 0f; i < 4.0; i++)
            {
                var fireball3 = Instantiate(fireball02,transform.position,Quaternion.identity) as GameObject;
                fireball3.transform.rotation=Quaternion.Euler(0.0f,0.0f,30.0f * scale + i * 90.0f);
                fireball3.GetComponent<Arrow2>().scale = nextScale;
                fireball3.transform.localScale = new Vector3(nextScale,nextScale,nextScale);
            }
            scale -= 1.0f;
            if(scale <= 1.0f)
            {
                Destroy(gameObject);
            }
            transform.localScale = new Vector3(scale,scale,scale);
        }
    }
    void divide2()
    {
        if(scale > 1.0f)
        {
            for (float i = 0f; i < 3.0; i++)
            {
                var fireball3 = Instantiate(fireball02,transform.position,Quaternion.identity) as GameObject;
                fireball3.transform.rotation=Quaternion.Euler(0.0f,0.0f,Random.Range(0.0f,360.0f) + i * 120.0f);
                fireball3.GetComponent<Arrow2>().scale = nextScale;
                fireball3.GetComponent<Arrow2>().type2 = true;
                fireball3.transform.localScale = new Vector3(nextScale,nextScale,nextScale);
            }
            scale -= 1.0f;
            if(scale <= 1.0f)
            {
                Destroy(gameObject);
            }
            transform.localScale = new Vector3(scale,scale,scale);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        unDestory = false;
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<HeroKnight>().beHited(damage * (int)scale,0);
            if(other.GetComponent<HeroKnight>().m_rolling)
                unDestory = true;
        }
        if(other.gameObject.CompareTag("Enemy"))unDestory = true;
        if(Time.time - bornTime >= protectTime && unDestory == false)
            Destroy(gameObject);
    }
}
