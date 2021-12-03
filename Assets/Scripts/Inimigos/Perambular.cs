using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
	
	public int velocidade;
	private bool perseguindo;
	
	private Player jogador;
	private Transform jogadorTransf;
	private Rigidbody2D thisBody;
	
    // Start is called before the first frame update
    void Start()
    {
        thisBody = this.gameObject.GetComponent<Rigidbody2D>();
		
		jogador = GameObject.FindObjectOfType<Player>();
		jogadorTransf = jogador.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
