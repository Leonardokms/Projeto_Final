using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPlayer : MonoBehaviour
{
    public float Vel;
    Vector2 Mov = new Vector2();
    Rigidbody2D rb;
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
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Mov.x = Input.GetAxisRaw("Horizontal");
        Mov.y = Input.GetAxisRaw("Vertical");
        Mov.Normalize();
        rb.velocity = Mov * Vel * Time.deltaTime;
    }
}
