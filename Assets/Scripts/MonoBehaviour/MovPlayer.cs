using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPlayer : MonoBehaviour
{
    public float Vel = 30.0f;
    Vector2 Mov = new Vector2();
    string estadoAnim = "EstadoAnim";

    Rigidbody2D rb;
    Animator anim;

    enum Estados
    {
        Parado = 0,
        AndaD = 1,
        AndaB = 2,
        AndaE = 3,
        AndaC = 4
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mov.x > 0)
        {
            anim.SetInteger(estadoAnim, (int)Estados.AndaD);
        }
        if (Mov.x < 0)
        {
            anim.SetInteger(estadoAnim, (int)Estados.AndaE);
        }
        if (Mov.y > 0)
        {
            anim.SetInteger(estadoAnim, (int)Estados.AndaC);
        }
        if (Mov.y < 0)
        {
            anim.SetInteger(estadoAnim, (int)Estados.AndaB);
        }
        if (Mov.y == 0 && Mov.x == 0)
        {
            anim.SetInteger(estadoAnim, (int)Estados.Parado);
        }
    }
    void FixedUpdate()
    {
        Mov.x = Input.GetAxisRaw("Horizontal");
        Mov.y = Input.GetAxisRaw("Vertical");
        Mov.Normalize();

        rb.velocity = Mov * Vel * Time.deltaTime;

    }
}
