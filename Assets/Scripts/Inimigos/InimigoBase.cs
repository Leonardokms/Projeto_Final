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
	private Color startColor;
	
	private bool andando;
	private SpriteRenderer thisSprite;
	private Animator thisAnimator;
	Coroutine danoCorountine;
	
    // Start is called before the first frame update
    void Start()
    {
		
        thisBody = this.gameObject.GetComponent<Rigidbody2D>();
		thisSprite = this.gameObject.GetComponent<SpriteRenderer>();
		thisAnimator = this.gameObject.GetComponent<Animator>();
		startColor = thisSprite.color;
		
		VidaAtual = Vida;
		paralisado = false;
    }
	
	    /* Ao colidir com o jogador, aplica dano chamando a corrotina DanoCaractere. */
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (danoCorountine == null)
            {
                print("DANO!!");
                danoCorountine = StartCoroutine(player.DanoCaractere(dano, 1.0f));
            }
        }
    }

    /* Finaliza a corrotina DanoCaractere quando para de colidir com o jogador */
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (danoCorountine != null)
            {
                StopCoroutine(danoCorountine);
                danoCorountine = null;
            }
        }
    }
	
	public void ReceberDano(int danoRecebido){
		VidaAtual = VidaAtual - danoRecebido;
		framesDesdeDano = 0;
		paralisado = true;
		thisSprite.color = Color.red;
	}
	
    // Update is called once per frame
    void Update()
    {
		framesDesdeDano++;
		if(framesDesdeDano >= tempoDeParalisia){
			paralisado = false;
			thisSprite.color = startColor;
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
		if(thisAnimator != null){
			thisAnimator.SetBool("andando", andando);
		}
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
