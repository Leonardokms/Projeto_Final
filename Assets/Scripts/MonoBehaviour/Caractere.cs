using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe abstrata que não pode ser instanciada, apenas herdada. Possui métodos que são aplicados para qualquer elemento caractere do jogo (player ou inimigo)
/// </summary>
public abstract class Caractere : MonoBehaviour
{
    public float inicioPontosDano;  // valor minimo inicial de  "saúde" do Player
    public float MaxPontosDano;     // valor máxmo permitido de "saúde" do Player
    public abstract void ResetCaractere();
    
	/* Faz com que o caractere fique avermelhado por um décimo de segundo */
    public virtual IEnumerator FlickerCaractere()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

	/* Método que deve ser implementado pelo personagem */
    public abstract IEnumerator DanoCaractere(int dano, float intervalo);

	/* Destrói o objeto quando este método é chamado */
    public virtual void KillCaractere()
    {
        Destroy(gameObject);       
    }
}
