using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Armazena e processa informa��es do clique do mouse para causar dano aos inimigos
/// </summary>
public class DanoMouse : MonoBehaviour
{
	public GameObject DamageZone;		// Zona que causa dano, mostrada por part�culas ao clicar
	public ParticleSystem Attack;		// Part�culas de ataque
	private Renderer AttackRenderer;	// Renderer para ataque na tela
	public ParticleSystem Fizzle;		// Part�cula de ataque falho
	private Renderer FizzleRenderer;	// Renderar para ataque falho na tela
	public float duracao;				// Vari�vel que armazena dura��o do ataque
	public float alcance;				// Vari�vel que armazena alcance do ataque

	public Vector3 mousePosition;				// Posi��o do mouse
	private Vector3 distanceMouseToPlayer;		// Dist�ncia entre mouse e jogador
	private Player jogador;						// armazena Jogador
	private Transform jogadorTransf;			// armazena transform do jogador
	
	
    /* Inicializa componentes de renderiza��o, mouse e player */
    void Start()
    {
		jogador = GameObject.FindObjectOfType<Player>();
		jogadorTransf = jogador.gameObject.GetComponent<Transform>();
		AttackRenderer = Attack.gameObject.GetComponent<Renderer>();
		FizzleRenderer = Fizzle.gameObject.GetComponent<Renderer>();
		
		AttackRenderer.sortingLayerName = "Caracteres";
		AttackRenderer.sortingOrder = 10;
		FizzleRenderer.sortingLayerName = "Caracteres";
		FizzleRenderer.sortingOrder = 10;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    /* Caso o jogador n�o seja nulo, verifica se existe uma espada coletada e efetua o dano ao inimigo no clique */
    void Update()
    {
		if(jogador != null)
        {
			if (jogador.espada == true)
			{
				DamageZone.GetComponent<Dano>().dano = 4;
			}

			mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			distanceMouseToPlayer = new Vector3((mousePosition.x - jogadorTransf.position.x), (mousePosition.y - jogadorTransf.position.y), 0);

			if (Input.GetMouseButtonDown(0) && distanceMouseToPlayer.magnitude <= alcance)
			{
				transform.position = mousePosition;
				DamageZone.SetActive(true);
				Attack.Play();
				Invoke("DespawnZone", duracao);
			}
			else if (Input.GetMouseButtonDown(0) && distanceMouseToPlayer.magnitude > alcance)
			{
				transform.position = mousePosition;
				Fizzle.Play();
			}
		}
	}
	/* Define a spawnzone como inativa*/
	void DespawnZone()
	{
		DamageZone.SetActive(false);
	}
}
