using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoBase : MonoBehaviour
{
	public int Vida;				// Armazena vida do inimigo
	public int dano;				// Armazena dano do inimigo
	private int VidaAtual;			// Armazena vida atual do inimigo
	private Rigidbody2D thisBody;	// Armazena um rb2d para o inimigo
	private bool paralisado;		// Armazena o estado de paralisia do inimigo
	public int tempoDeParalisia;	// Armazena o tempo de paralisia do inimigo
	private int framesDesdeDano;	// Armazena os frames desde que dano foi tomado
	//private int framesDesdeAtaque;
	private Color startColor;		// Define a cor inicial
	private GameObject p1;			// Armazena o GameObject para a classe Player
	private GameObject mouse;		// Armazena o GameObject para a classe MouseDano

	private SpriteRenderer thisSprite;	// Armazena sprite do inimigo
	Coroutine danoCorountine;			// Armazena coroutine de dano

	/* Define as variáveis a partir dos gameObjects */
	void Start()
    {
        thisBody = this.gameObject.GetComponent<Rigidbody2D>();
		thisSprite = this.gameObject.GetComponent<SpriteRenderer>();
		startColor = thisSprite.color;
		p1 = GameObject.FindGameObjectWithTag("Player");
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
				if(p1.GetComponent<Player>().escudo == true)
                {
					danoCorountine = StartCoroutine(player.DanoCaractere(dano/2, 1.0f));
				}
                else
                {
					danoCorountine = StartCoroutine(player.DanoCaractere(dano, 1.0f));
				}
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
	/* Função para causar dano ao inimigo */
	public void ReceberDano(int danoRecebido)
	{
		if(VidaAtual - danoRecebido > 0)
        {
			VidaAtual -= danoRecebido;
			framesDesdeDano = 0;
			paralisado = true;
			thisSprite.color = Color.red;
		}
		else
        {
			VidaAtual -= danoRecebido;
		}

	}
	
    /* Atualiza as animações e verifica se o inimigo está morto */
    void Update()
    {
		framesDesdeDano++;
		if (framesDesdeDano >= tempoDeParalisia)
		{
			paralisado = false;
			thisSprite.color = startColor;
		}
		if (VidaAtual <= 0)
		{
			Destroy(gameObject);
		}

		if (thisBody.velocity.x > 0)
		{
			thisSprite.flipX = true;
		}
		else
		{
			thisSprite.flipX = false;
		}
	}

	/* Define o objeto como paralisado */
	public void SetParalisia(bool paralisar)
	{
		this.paralisado = paralisar;
	}
	
	/* Retorna se o objeto está paralisado */
	public bool GetParalisia()
	{
		return this.paralisado;
	}
}
