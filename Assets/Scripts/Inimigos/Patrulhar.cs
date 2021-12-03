using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Patrulhar : MonoBehaviour
{
	
	public float velocidade;
	public float velocidadeBrecar;
	public float raioDePercepcao;
	private bool perseguindo;
	
	private Player jogador;
	private Transform jogadorTransf;
	private Rigidbody2D thisBody;
	private Vector3 DistanciaAoJogador;
	private Vector2 velocidadeAtual;
	private InimigoBase esteInimigo;
	
	private Vector3[] ponto = new Vector3[4];
	public float arestaHorizontal;
	public float arestaVertical;
	private int pontoAtual;
	
	
    // Start is called before the first frame update
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
		
		perseguindo = true;
    }
	
	void Perseguir(){
		if(!esteInimigo.GetParalisia()){
			Vector2 movementAI = new Vector2(((jogadorTransf.position.x)-(gameObject.transform.position.x)),((jogadorTransf.position.y)-(gameObject.transform.position.y)));
			Vector3 DistanciaNormalizada = DistanciaAoJogador;
			DistanciaNormalizada.Normalize();
			velocidadeAtual = new Vector2(thisBody.velocity.x, thisBody.velocity.y);
			Vector2 velocidadeNormalizada = velocidadeAtual;
			velocidadeNormalizada.Normalize();
			
			if(velocidadeAtual.magnitude <= velocidade || ((Math.Abs(DistanciaNormalizada.x - velocidadeNormalizada.x) >= 0.4)  || (Math.Abs(DistanciaNormalizada.y - velocidadeNormalizada.y) >= 0.4)) )
			{
				thisBody.velocity = (DistanciaNormalizada*velocidade);
				//thisBody.AddForce(DistanciaNormalizada*velocidade, ForceMode2D.Impulse);
			}
		}else{
			thisBody.velocity = new Vector2(0,0);
		}
	}
	
	/*
	void FixedUpdate(){
		velocidadeAtual = new Vector2(thisBody.velocity.x, thisBody.velocity.y);
		if(velocidadeAtual.magnitude >= velocidade){
			Vector2 brake = new Vector2(-thisBody.velocity.x, -thisBody.velocity.y);
			thisBody.AddForce(brake*(velocidadeBrecar/10), ForceMode2D.Impulse);
		}
	}
	*/

    // Update is called once per frame
    void Update()
    {
		DistanciaAoJogador = new Vector3(((jogadorTransf.position.x) - (gameObject.transform.position.x)), ((jogadorTransf.position.y) - (gameObject.transform.position.y)), 0f);
		
        if(DistanciaAoJogador.magnitude < raioDePercepcao){
			Perseguir();
		}else{
			Patrulha();
		}
    }
	
	void Patrulha(){
		if(!esteInimigo.GetParalisia()){
			if(pontoAtual == 0){
				Vector3 direcaoPonto = new Vector3(gameObject.transform.position.x - ponto[0].x ,gameObject.transform.position.y - ponto[0].y);
				direcaoPonto.Normalize();
				thisBody.velocity = (-direcaoPonto*velocidade);
				//thisBody.AddForce(-direcaoPonto*velocidade,ForceMode2D.Impulse);
				if((Math.Abs(gameObject.transform.position.x - ponto[0].x) <= 0.1) && (Math.Abs(gameObject.transform.position.y - ponto[0].y) <= 0.1)){
					pontoAtual = 1;
				}
			}else if(pontoAtual == 1){
				Vector3 direcaoPonto = new Vector3(gameObject.transform.position.x - ponto[1].x ,gameObject.transform.position.y - ponto[1].y);
				direcaoPonto.Normalize();
				thisBody.velocity = (-direcaoPonto*velocidade);
				//thisBody.AddForce(-direcaoPonto*velocidade,ForceMode2D.Impulse);
				if((Math.Abs(gameObject.transform.position.x - ponto[1].x) <= 0.1) && (Math.Abs(gameObject.transform.position.y - ponto[1].y) <= 0.1)){
					pontoAtual = 2;
				}
			}else if(pontoAtual == 2){
				Vector3 direcaoPonto = new Vector3(gameObject.transform.position.x - ponto[2].x ,gameObject.transform.position.y - ponto[2].y);
				direcaoPonto.Normalize();
				thisBody.velocity = (-direcaoPonto*velocidade);
				//thisBody.AddForce(-direcaoPonto*velocidade,ForceMode2D.Impulse);
				if((Math.Abs(gameObject.transform.position.x - ponto[2].x) <= 0.1) && (Math.Abs(gameObject.transform.position.y - ponto[2].y) <= 0.1)){
					pontoAtual = 3;
				}
			}else if(pontoAtual == 3){
				Vector3 direcaoPonto = new Vector3(gameObject.transform.position.x - ponto[3].x ,gameObject.transform.position.y - ponto[3].y);
				direcaoPonto.Normalize();
				thisBody.velocity = (-direcaoPonto*velocidade);
				//thisBody.AddForce(-direcaoPonto*velocidade,ForceMode2D.Impulse);
				if((Math.Abs(gameObject.transform.position.x - ponto[3].x) <= 0.1) && (Math.Abs(gameObject.transform.position.y - ponto[3].y) <= 0.1)){
					pontoAtual = 0;
				}
			}
		}
	}
}
