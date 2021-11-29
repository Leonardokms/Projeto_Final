using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Define a posição para spawn da entidade
/// </summary>
public class PontoSpawn : MonoBehaviour
{
    public GameObject prefabParaSpawn;

    public float intervaloRepeticao;

    // Start is called before the first frame update
    /* Assim que o script inicia, chama o método Spawn0 várias vezes em seguida */
    public void Start()
    {
        if (intervaloRepeticao > 0)
        {
            InvokeRepeating("SpawnO", 0.0f, intervaloRepeticao);
        }
    }

    /* Se não houver um prefab para spawn do jogador instanciado, inicia uma instância do prefab */
    public GameObject SpawnO()
    {
        if (prefabParaSpawn != null)
        {
            return Instantiate(prefabParaSpawn, transform.position, Quaternion.identity);
        }
        return null;
    }
}
