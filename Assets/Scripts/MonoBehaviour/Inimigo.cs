using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe contendo informa��es de vida, dano do inimigo, al�m de contato do inimigo com o player
/// </summary>
public class Inimigo : Caractere
{
    float pontosVida;           // equivalente � sa�de do inimigo
    public int forcaDano;       // poder de dano

    Coroutine danoCorountine;

    /* Reinicia a vida do caractere ao habilit�-lo */
    private void OnEnable()
    {
        ResetCaractere();
    }

    /* Ao colidir com o jogador, aplica dano chamando a corrotina DanoCaractere. */
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (danoCorountine == null)
            {
                danoCorountine = StartCoroutine(player.DanoCaractere(forcaDano, 1.0f));
            }
        }
    }

    /* Finaliza a corrotina DanoCaractere quando para de colidir com o jogador */
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (danoCorountine != null)
            {
                StopCoroutine(danoCorountine);
                danoCorountine = null;
            }
        }
    }

    /* Inicia a corrotina de mudan�a de cor quando o inimigo � danificado, e diminui o tanto necess�rio de pontos de vida, destru�ndo o caractere caso
	 * a sua vida fique igual ou menor que zero. */
    public override IEnumerator DanoCaractere(int dano, float intervalo)
    {
        while (true)
        {
            StartCoroutine(FlickerCaractere());
            pontosVida = pontosVida - dano;
            if (pontosVida <= float.Epsilon)
            {
                KillCaractere();
                break;
            }
            if (intervalo > float.Epsilon)
            {
                yield return new WaitForSeconds(intervalo);
            }
            else
            {
                break;
            }
        }
    }

    /* Coloca a vida do inimigo de volta ao m�ximo */
    public override void ResetCaractere()
    {
        pontosVida = inicioPontosDano;
    }
}
