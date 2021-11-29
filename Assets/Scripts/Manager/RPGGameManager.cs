using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define o in�cio do jogo, com a defini��o da cena e spawn do player
/// </summary>
public class RPGGameManager : MonoBehaviour
{
    public static RPGGameManager instanciaCompartilhada = null;
    public RPGCameraManager cameraManager;

    public PontoSpawn playerPontoSpawn;

    /* Assim que o script � iniciado, verifica se j� h� uma inst�ncia de Game Manager ativa. Se houver, destr�i esta inst�ncia. Se n�o, torna esta a inst�ncia compartilhada. */
    private void Awake()
    {
        if (instanciaCompartilhada != null && instanciaCompartilhada != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instanciaCompartilhada = this;
        }
    }

    // Start is called before the first frame update
    /* Quando jogo � iniciado, executa o m�todo SetupScene */
    void Start()
    {
        SetupScene();
    }

    //Executa o m�todo SpawnPlayer
    public void SetupScene()
    {
        SpawnPlayer();
    }

    /* Se n�o houver um ponto de spawn associado, spawna um jogador no ponto de Spawn inicial
	 * Em seguida, utiliza o script cameraManager para colocar a inst�ncia compartilhada da c�mera para seguir as coordenadas do jogador */
    public void SpawnPlayer()
    {
        if (playerPontoSpawn != null)
        {
            GameObject player = playerPontoSpawn.SpawnO();
            cameraManager.virtualCamera.Follow = player.transform;
        }
    }
}
