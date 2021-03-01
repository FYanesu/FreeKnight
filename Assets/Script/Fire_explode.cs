using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_explode : MonoBehaviour
{
    private PolygonCollider2D coll2D;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        coll2D = transform.GetComponent<PolygonCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<HeroKnight>().beHited(damage,0);
            coll2D.enabled = false;
        }
    }
    void AE_attack()
    {
        SoundManager.PlayB_attack2();
        coll2D.enabled = true;
    }
    void AE_destory()
    {
        Destroy(gameObject);
    }
}
