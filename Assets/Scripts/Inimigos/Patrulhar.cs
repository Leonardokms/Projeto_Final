using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
/// <summary>
/// Define informações para o tipo de inimigo que patrulha uma área
/// </summary>

public class Patrulhar : MonoBehaviour
{
	public float velocidade;			// Armazena velocidade do inimigo
	public float velocidadeBrecar;		// Armazena velocidade de parada do inimigo
	public float raioDePercepcao;		// Armazena raio de percepção do inimigo
	
	private Player jogador;				// Armazena jogador
	private Transform jogadorTransf;    // Armazena a transform do jogador
	private Rigidbody2D thisBody;       // Armazena o rb2d
	private Vector3 DistanciaAoJogador; // Armazena a distancia até o jogador
	private Vector2 velocidadeAtual;	// Armazena velocidade atual do inimigo
	private InimigoBase esteInimigo;	// Armazena o inimigo
	
	private Vector3[] ponto = new Vector3[4];	// Armazena um vetor para ponto para patrulha
	public float arestaHorizontal;				// Armazena uma aresta horizontal para patrulha
	public float arestaVertical;                // Armazena uma aresta vertical para patrulha
	private int pontoAtual;						// Armazena o ponto atual para patrulha
	
	
    /* Define os valores das variáveis de acordo com os gameObjects e define os pontos de patrulha */
    void Start()
    {
        thisBody = this.gameObject.GetComponent<Rigidbody2D>();
		esteInimigo = this.gameObject.GetComponent<InimigoBase>();
		
		jogador = GameObject.FindObjectOfType<Player>();
		jogadorTransf = jogador.gameObject.GetComponent<Transform>();
		DistanciaAoJogador = new Vector3 (((jogadorTransf.position.x) - (gameObject.transform.position.x)), ((jogadorTransf.position.y) - (gameObject.transform.position.y)), 0);
		
		pontoAtual = 0;
		ponto[0] = new Vector2(thisBody.position.x - arestaHorizontal, thisBody.position.y+arestaVertical);
		ponto[1] = new Vector2(thisBody.position.x + arestaHorizontal, thisBody.position.y+arestaVertical);
		ponto[2] = new Vector2(thisBody.position.x + arestaHorizontal, thisBody.position.y-arestaVertical);
		ponto[3] = new Vector2(thisBody.position.x - arestaHorizontal, thisBody.position.y-arestaVertical);
    }

	/* Função que faz o inimigo perseguir o player caso ele não esteja paralizado */
	void Perseguir()
	{
		if (!esteInimigo.GetParalisia())
		{
			Vector2 movementAI = new Vector2(((jogadorTransf.position.x) - (gameObject.transform.position.x)), ((jogadorTransf.position.y) - (gameObject.transform.position.y)));
			Vector3 DistanciaNormalizada = DistanciaAoJogador;
			DistanciaNormalizada.Normalize();
			velocidadeAtual = new Vector2(thisBody.velocity.x, thisBody.velocity.y);
			Vector2 velocidadeNormalizada = velocidadeAtual;
			velocidadeNormalizada.Normalize();

			if (velocidadeAtual.magnitude <= velocidade || ((Math.Abs(DistanciaNormalizada.x - velocidadeNormalizada.x) >= 0.4) || (Math.Abs(DistanciaNormalizada.y - velocidadeNormalizada.y) >= 0.4)))
			{
				thisBody.velocity = (DistanciaNormalizada * velocidade);
			}
		}
		else
		{
			thisBody.velocity = new Vector2(0, 0);
		}
	}
	
    /* Verifica se o jogador está na distância para perseguição */
    void Update()
    {
		if(jogador != null)
        {
			DistanciaAoJogador = new Vector3(((jogadorTransf.position.x) - (gameObject.transform.position.x)), ((jogadorTransf.position.y) - (gameObject.transform.position.y)), 0f);

			if (DistanciaAoJogador.magnitude < raioDePercepcao)
			{
				Perseguir();
			}
			else
			{
				Patrulha();
			}
		}
    }
	/* Função que faz o inimigo patrulhar a área caso ele não esteja paralizado */
	void Patrulha()
	{
		if (!esteInimigo.GetParalisia())
		{
			if (pontoAtual == 0)
			{
				Vector3 direcaoPonto = new Vector3(gameObject.transform.position.x - ponto[0].x, gameObject.transform.position.y - ponto[0].y);
				direcaoPonto.Normalize();
				thisBody.velocity = (-direcaoPonto * velocidade);
				if ((Math.Abs(gameObject.transform.position.x - ponto[0].x) <= 0.1) && (Math.Abs(gameObject.transform.position.y - ponto[0].y) <= 0.1))
				{
					pontoAtual = 1;
				}
			}
			else if (pontoAtual == 1)
			{
				Vector3 direcaoPonto = new Vector3(gameObject.transform.position.x - ponto[1].x, gameObject.transform.position.y - ponto[1].y);
				direcaoPonto.Normalize();
				thisBody.velocity = (-direcaoPonto * velocidade);
				if ((Math.Abs(gameObject.transform.position.x - ponto[1].x) <= 0.1) && (Math.Abs(gameObject.transform.position.y - ponto[1].y) <= 0.1))
				{
					pontoAtual = 2;
				}
			}
			else if (pontoAtual == 2)
			{
				Vector3 direcaoPonto = new Vector3(gameObject.transform.position.x - ponto[2].x, gameObject.transform.position.y - ponto[2].y);
				direcaoPonto.Normalize();
				thisBody.velocity = (-direcaoPonto * velocidade);
				if ((Math.Abs(gameObject.transform.position.x - ponto[2].x) <= 0.1) && (Math.Abs(gameObject.transform.position.y - ponto[2].y) <= 0.1))
				{
					pontoAtual = 3;
				}
			}
			else if (pontoAtual == 3)
			{
				Vector3 direcaoPonto = new Vector3(gameObject.transform.position.x - ponto[3].x, gameObject.transform.position.y - ponto[3].y);
				direcaoPonto.Normalize();
				thisBody.velocity = (-direcaoPonto * velocidade);
				if ((Math.Abs(gameObject.transform.position.x - ponto[3].x) <= 0.1) && (Math.Abs(gameObject.transform.position.y - ponto[3].y) <= 0.1))
				{
					pontoAtual = 0;
				}
			}
		}
	}
}
