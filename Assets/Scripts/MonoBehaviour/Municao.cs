using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define o dano de colisão da munição com inimigo
/// </summary>
public class Municao : MonoBehaviour
{
    public int danoCausado;        // poder de dano da munição
	
	/* Ao colidir com um inimigo, aplica dano dele e desativa o objeto */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision is BoxCollider2D)
        {
            Inimigo inimigo = collision.gameObject.GetComponent<Inimigo>();
            StartCoroutine(inimigo.DanoCaractere(danoCausado, 0.0f));
            gameObject.SetActive(false);
        }
    }
}
