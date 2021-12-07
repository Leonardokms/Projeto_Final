using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoBase : MonoBehaviour
{
	public int Vida;
	public int dano;
	private int VidaAtual;
	private Rigidbody2D thisBody;
	private bool paralisado;
	public int tempoDeParalisia;
	private int framesDesdeDano;
	private int framesDesdeAtaque;	
	private bool andando;
	private SpriteRenderer thisSprite;
	private Animator thisAnimator;
	private GameObject p1;
    // Start is called before the first frame update
    void Start()
    {
        thisBody = this.gameObject.GetComponent<Rigidbody2D>();
		thisSprite = this.gameObject.GetComponent<SpriteRenderer>();
		thisAnimator = this.gameObject.GetComponent<Animator>();
		p1 = GameObject.FindGameObjectWithTag("Player");
		VidaAtual = Vida;
		paralisado = false;
    }
	
	void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
			if (p1.GetComponent<Player>().escudo == true)
            {
				player.pontosDano.valor = player.pontosDano.valor - (dano/2);
			}
			else
            {
				player.pontosDano.valor = player.pontosDano.valor - dano;
			}
		}	
    }
	
	void OnCollisionStay2D(Collision2D collision)
    {
		framesDesdeAtaque++;
        if (collision.gameObject.CompareTag("Player") && framesDesdeAtaque > 40)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.Vida = player.Vida - dano;
			framesDesdeAtaque = 0;
        }	
    }
	
	public void ReceberDano(int danoRecebido){
		VidaAtual = VidaAtual - danoRecebido;
		framesDesdeDano = 0;
		paralisado = true;
	}
	
    // Update is called once per frame
    void Update()
    {
		framesDesdeDano++;
		if(framesDesdeDano >= tempoDeParalisia){
			paralisado = false;
		}
        if(VidaAtual <= 0)
		{
			Destroy(this.gameObject);
		}
		if((thisBody.velocity.x != 0 || thisBody.velocity.y != 0) && !paralisado){
			andando = true;
		}else{
			andando = false;
		}
		if(thisBody.velocity.x > 0){
			thisSprite.flipX = true;
		}else{
			thisSprite.flipX = false;
		}
		thisAnimator.SetBool("andando", andando);
    }
	
	public void SetParalisia(bool paralisar){
		this.paralisado = paralisar;
	}
	
	public bool GetParalisia(){
		return this.paralisado;
	}
	
	private void OnEnable()
    {
        //VidaAtual = Vida;
    }
}
