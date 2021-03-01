using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBorn : MonoBehaviour
{
    public GameObject BossGb;
    private Vector3 fix;
    void AE_Destory()
    {
        Destroy(gameObject);
    }
    void AE_Born()
    {
        fix = new Vector3(0f,-1.32f+0.68f,0f);
        Instantiate(BossGb,transform.position+fix,Quaternion.identity);
        UIManager.instance.CreatBossHPBar();
    }
}
