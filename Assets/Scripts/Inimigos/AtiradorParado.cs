using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiradorParado : MonoBehaviour
{
	
	private Player jogador;
	private Transform jogadorTransf;
	private Rigidbody2D thisBody;
	
	
	
	private Vector2 zero = new Vector2(0,0);
	private int framesSinceShot;
	public int shotfreq;
	public Rigidbody2D projectile;
	private Rigidbody2D shot;
	public int shotspeed;
	public float alcance;
	
	
	
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
        framesSinceShot++;
    }
	
	
	void FixedUpdate()
	{

		//Código que controla os tiros do inimigo, instanciando projéteis na direção do jogador
		
		Vector3 distancetoplayer = new Vector3 (((jogadorTransf.position.x) - (gameObject.transform.position.x)), ((jogadorTransf.position.y) - (gameObject.transform.position.y)), 0);
		if(framesSinceShot>=shotfreq && distancetoplayer.magnitude <= alcance)
		{
			framesSinceShot = 0;
			shot = Instantiate(projectile, this.transform.position, this.transform.rotation) as Rigidbody2D;
			shot.gameObject.SetActive(true);
			Vector3 direction = new Vector3(((jogadorTransf.position.x)-(gameObject.transform.position.x)),((jogadorTransf.position.y)-(gameObject.transform.position.y)) ,0);
			direction.Normalize();
			shot.velocity = transform.TransformDirection(direction*shotspeed);
		}
	}
}
