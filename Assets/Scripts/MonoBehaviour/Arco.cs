using System.Collections;
using UnityEngine;
/// <summary>
/// Classe que define um arco para muni��o
/// </summary>
public class Arco : MonoBehaviour
{
    /* Define um arco de trajet�ria para a muni��o atirada, de forma que ela faz um arco entre o player e o destino (clique do mouse). 
     * Al�m disso a trajet�ria tem uma dura��o fixa indepente da dist�ncia*/
    public IEnumerator arcoTrajetoria(Vector3 destino, float duracao)
    {
        var posicaoInicial = transform.position;
        var percentualCompleto = 0.0f;
        while(percentualCompleto < 1.0f)
        {
            percentualCompleto += Time.deltaTime / duracao;
            var alturaCorrente = Mathf.Sin(Mathf.PI * percentualCompleto);
            transform.position = Vector3.Lerp(posicaoInicial, destino, percentualCompleto) + Vector3.up*alturaCorrente;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
