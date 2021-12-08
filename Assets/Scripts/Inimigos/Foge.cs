using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
/// <summary>
/// Define informa��es para o tipo de inimigo que foge do jogador
/// </summary>
public class Foge : MonoBehaviour
{
	public int velocidade;              // Define velocidade do inimigo
	public float raioDePercepcao;       // Define o raio de percep��o do inimigo

	private Player jogador;             // Armazena a vari�vel do jogador
	private Transform jogadorTransf;    // Armazena a transform do jogador
	private Rigidbody2D thisBody;       // Armazena o rb2d
	private Vector3 DistanciaAoJogador; // Armazena a distancia at� o jogador
	private InimigoBase esteInimigo;    // Armazena o inimigo

	/* Define as vari�veis a partir dos gameObjects */
	void Start()
    {
        thisBody = this.gameObject.GetComponent<Rigidbody2D>();
		esteInimigo = this.gameObject.GetComponent<InimigoBase>();
		
		jogador = GameObject.FindObjectOfType<Player>();
		jogadorTransf = jogador.gameObject.GetComponent<Transform>();
		
		DistanciaAoJogador = new Vector3 (((jogadorTransf.position.x) - (gameObject.transform.position.x)), ((jogadorTransf.position.y) - (gameObject.transform.position.y)), 0);
		
    }
	/* Fun��o que faz o inimigo fugir do player caso ele n�o esteja paralizado */
	void Fugir(){
		if(!esteInimigo.GetParalisia()){
			Vector2 movementAI = new Vector2(((jogadorTransf.position.x)-(gameObject.transform.position.x)),((jogadorTransf.position.y)-(gameObject.transform.position.y)));
			Vector3 DistanciaNormalizada = DistanciaAoJogador;
			DistanciaNormalizada.Normalize();
			Vector2 velocidadeAtual = new Vector2(thisBody.velocity.x, thisBody.velocity.y);
			Vector2 velocidadeNormalizada = velocidadeAtual;
			velocidadeNormalizada.Normalize();
			
			if(velocidadeAtual.magnitude <= velocidade || ((Math.Abs(DistanciaNormalizada.x - velocidadeNormalizada.x) >= 0.4)  || (Math.Abs(DistanciaNormalizada.y - velocidadeNormalizada.y) >= 0.4)) )
			{
				thisBody.velocity = (-(DistanciaNormalizada*velocidade));
			}
		}
		else
		{
			thisBody.velocity = new Vector2(0,0);
		}
	}

    /* Verifica se o inimigo est� na dist�ncia v�lida para fugir do player */
    void Update()
    {
		if(jogador != null)
        {
			DistanciaAoJogador = new Vector3(((jogadorTransf.position.x) - (gameObject.transform.position.x)), ((jogadorTransf.position.y) - (gameObject.transform.position.y)), 0f);

			if (DistanciaAoJogador.magnitude < raioDePercepcao)
			{
				Fugir();
			}
			else
			{
				thisBody.velocity = new Vector2(0, 0);
			}
		}
    }
}
