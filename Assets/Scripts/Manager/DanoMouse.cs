using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoMouse : MonoBehaviour
{
	public GameObject DamageZone;
	public ParticleSystem Attack;
	private Renderer AttackRenderer;
	public ParticleSystem Fizzle;
	private Renderer FizzleRenderer;
	public float duracao;
	public float alcance;
	
	public Vector3 mousePosition;
	private Vector3 distanceMouseToPlayer;
	private Player jogador;
	private Transform jogadorTransf;
	
	
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);	
		distanceMouseToPlayer = new Vector3((mousePosition.x - jogadorTransf.position.x),(mousePosition.y - jogadorTransf.position.y),0);
        
		if(Input.GetMouseButtonDown(0) && distanceMouseToPlayer.magnitude <= alcance){
			transform.position = mousePosition;
			DamageZone.SetActive(true);
			Attack.Play();
			Invoke("DespawnZone", duracao);
		}else if(Input.GetMouseButtonDown(0) && distanceMouseToPlayer.magnitude > alcance){
			transform.position = mousePosition;
			Fizzle.Play();
		}
		
		
    }
	
	
	
	void DespawnZone(){
		DamageZone.SetActive(false);
	}
}
