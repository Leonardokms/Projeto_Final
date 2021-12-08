using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Define informações sobre inimigos que atiram
/// </summary>
public class AtiradorParado : MonoBehaviour
{
	
	private Player jogador;						// Armazena jogador
	private Transform jogadorTransf;			// Armazena a transform do jogador
	private int framesSinceShot;				// Armazena os frames desde o último tiro
	public int shotfreq;						// Armazena frequência de tiros
	public Rigidbody2D projectile;				// Armazena um rb2d para o projétil
	private Rigidbody2D shot;					// Armazena um rb2d para o tiro (baseado no projétil)
	public int shotspeed;						// Define velocidade do tiro
	public float alcance;						// Define alcance do tiro
	
	
    /* Recebe o Rigidbody2D do gameObject ao qual este componente está conectado. Busca também um objeto que tenha o script Player e seu Transform. */
    void Start()
    {
        //thisBody = this.gameObject.GetComponent<Rigidbody2D>();
		
		jogador = GameObject.FindObjectOfType<Player>();
		jogadorTransf = jogador.gameObject.GetComponent<Transform>();
    }
	
    /* Incrementa o contador de frames desde o último tiro */
    void Update()
    {
        framesSinceShot++;
    }

	/* Código que controla os tiros do inimigo, instanciando projéteis na direção do jogador */
	void FixedUpdate()
	{
		if(jogador != null)
        {
			Vector3 distancetoplayer = new Vector3(((jogadorTransf.position.x) - (gameObject.transform.position.x)), ((jogadorTransf.position.y) - (gameObject.transform.position.y)), 0);
			if (framesSinceShot >= shotfreq && distancetoplayer.magnitude <= alcance)
			{
				framesSinceShot = 0;
				shot = Instantiate(projectile, this.transform.position, this.transform.rotation) as Rigidbody2D;
				shot.gameObject.SetActive(true);
				Vector3 direction = new Vector3(((jogadorTransf.position.x) - (gameObject.transform.position.x)), ((jogadorTransf.position.y) - (gameObject.transform.position.y)), 0);
				direction.Normalize();
				shot.velocity = transform.TransformDirection(direction * shotspeed);
			}
		}

	}
}
