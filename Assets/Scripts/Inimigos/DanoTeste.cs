using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoTeste : MonoBehaviour
{
	public int dano;
	private InimigoBase inimigo;
	private int framesDesdeAtaque;
	
	void OnTriggerEnter2D(Collider2D other)
	{
		 //Detecta tags diferentes para variáveis do sistema
		if((other.gameObject.CompareTag("Inimigo")))
		{
			inimigo = other.gameObject.GetComponent<InimigoBase>();
			inimigo.ReceberDano(dano);
		}
	}
		
	void OnTriggerEnter2D(Collision2D collision)
    {
		framesDesdeAtaque++;
        if (collision.gameObject.CompareTag("Inimigo") && framesDesdeAtaque > 40)
        {
            inimigo.ReceberDano(dano);
			framesDesdeAtaque = 0;
        }	
    }
	
    // Start is called before the first frame update
    void Start()
    {
        framesDesdeAtaque = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
