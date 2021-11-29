using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe abstrata que n�o pode ser instanciada, apenas herdada. Possui m�todos que s�o aplicados para qualquer elemento caractere do jogo (player ou inimigo)
/// </summary>
public abstract class Caractere : MonoBehaviour
{
    //public int PontosDano;        // vers�o anterior do valor de "dano"    
    //public int MaxPontosDano;     // novo anterior do valor m�ximo de "dano" 
    public float inicioPontosDano;  // valor minimo inicial de  "sa�de" do Player
    public float MaxPontosDano;     // valor m�xmo permitido de "sa�de" do Player
    public abstract void ResetCaractere();
    
	/* Faz com que o caractere fique avermelhado por um d�cimo de segundo */
    public virtual IEnumerator FlickerCaractere()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;

    }

	/* M�todo que deve ser implementado pelo personagem */
    public abstract IEnumerator DanoCaractere(int dano, float intervalo);

	/* Destr�i o objeto quando este m�todo � chamado */
    public virtual void KillCaractere()
    {
        Destroy(gameObject);       
    }
}
