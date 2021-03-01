using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearPlayer : MonoBehaviour
{
    public Vector2 moveSpeed;
    private Animator m_animator;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = new Vector2(90f,0f);
        m_animator = GetComponent<Animator>();
        m_animator.SetInteger("AnimState", 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime / 120f);
        if(transform.position.x >= 80f || Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Roll") || Input.GetButtonDown("Menu"))
            SceneManager.LoadScene(0);

    }
}
