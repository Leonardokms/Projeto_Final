using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe para relações de dano para os inimigos
/// </summary>

public class Dano : MonoBehaviour
{
	public int dano;
	private InimigoBase inimigo;

	/* Detecta tags diferentes para variáveis do sistema */
	void OnTriggerEnter2D(Collider2D other)
	{
		 
		if((other.gameObject.CompareTag("Inimigo")))
		{
			inimigo = other.gameObject.GetComponent<InimigoBase>();
			inimigo.ReceberDano(dano);
		}
	}
}
