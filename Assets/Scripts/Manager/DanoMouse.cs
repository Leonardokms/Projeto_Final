using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Armazena e processa informações do clique do mouse para causar dano aos inimigos
/// </summary>
public class DanoMouse : MonoBehaviour
{
	public GameObject DamageZone;		// Zona que causa dano, mostrada por partículas ao clicar
	public ParticleSystem Attack;		// Partículas de ataque
	private Renderer AttackRenderer;	// Renderer para ataque na tela
	public ParticleSystem Fizzle;		// Partícula de ataque falho
	private Renderer FizzleRenderer;	// Renderar para ataque falho na tela
	public float duracao;				// Variável que armazena duração do ataque
	public float alcance;				// Variável que armazena alcance do ataque

	public Vector3 mousePosition;				// Posição do mouse
	private Vector3 distanceMouseToPlayer;		// Distância entre mouse e jogador
	private Player jogador;						// armazena Jogador
	private Transform jogadorTransf;			// armazena transform do jogador
	
	
    /* Inicializa componentes de renderização, mouse e player */
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

    /* Caso o jogador não seja nulo, verifica se existe uma espada coletada e efetua o dano ao inimigo no clique */
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
