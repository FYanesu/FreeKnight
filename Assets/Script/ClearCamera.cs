using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCamera : MonoBehaviour
{
    public Vector2 moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = new Vector2(90f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < 67.4f)
            transform.Translate(moveSpeed * Time.deltaTime / 120f);
    }
}
