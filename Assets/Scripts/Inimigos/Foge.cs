using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Foge : MonoBehaviour
{
	
    public int velocidade;
	public float raioDePercepcao;
	private bool perseguindo;
	
	private Player jogador;
	private Transform jogadorTransf;
	private Rigidbody2D thisBody;
	private Vector3 DistanciaAoJogador;
	private InimigoBase esteInimigo;
	private Vector2 velocidadeAtual;
	
	
    // Start is called before the first frame update
    void Start()
    {
        thisBody = this.gameObject.GetComponent<Rigidbody2D>();
		esteInimigo = this.gameObject.GetComponent<InimigoBase>();
		
		jogador = GameObject.FindObjectOfType<Player>();
		jogadorTransf = jogador.gameObject.GetComponent<Transform>();
		
		DistanciaAoJogador = new Vector3 (((jogadorTransf.position.x) - (gameObject.transform.position.x)), ((jogadorTransf.position.y) - (gameObject.transform.position.y)), 0);
		
    }
	
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
				//thisBody.AddForce(DistanciaNormalizada, ForceMode2D.Impulse);
			}
		}else{
			thisBody.velocity = new Vector2(0,0);
		}
	}

    // Update is called once per frame
    void Update()
    {
        DistanciaAoJogador = new Vector3(((jogadorTransf.position.x) - (gameObject.transform.position.x)), ((jogadorTransf.position.y) - (gameObject.transform.position.y)), 0f);
		
        if(DistanciaAoJogador.magnitude < raioDePercepcao){
			Fugir();
		}else{
			thisBody.velocity = new Vector2(0,0);
		}
    }
}
