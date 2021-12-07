using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
	public float duracaoProjetil;
	public float velocidadeRotacao;
	public int dano;
	
	Coroutine danoCorountine;
	
	private bool collided;
	
	private Player player;
	
    void Start()
	{
		collided = false;
		//Destroy(this.gameObject, duracaoProjetil);
    }
	
	void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collided == false)
        {
			collided = true;
            Player player = collision.gameObject.GetComponentInParent<Player>();
            if (danoCorountine == null)
            {
                print("DANO!!");
                danoCorountine = StartCoroutine(player.DanoCaractere(dano, 1.0f));
            }
			Destroy(this.gameObject, 0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
		transform.Rotate(0,0,velocidadeRotacao);
    }
}
