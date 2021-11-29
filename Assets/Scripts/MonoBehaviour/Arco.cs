using System.Collections;
using UnityEngine;
/// <summary>
/// Classe que define um arco para munição
/// </summary>
public class Arco : MonoBehaviour
{
    /* Define um arco de trajetória para a munição atirada, de forma que ela faz um arco entre o player e o destino (clique do mouse). 
     * Além disso a trajetória tem uma duração fixa indepente da distância*/
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
