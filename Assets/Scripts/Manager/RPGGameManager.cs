using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define o início do jogo, com a definição da cena e spawn do player
/// </summary>
public class RPGGameManager : MonoBehaviour
{
    public static RPGGameManager instanciaCompartilhada = null;
    public RPGCameraManager cameraManager;

    public PontoSpawn playerPontoSpawn;

    /* Assim que o script é iniciado, verifica se já há uma instância de Game Manager ativa. Se houver, destrói esta instância. Se não, torna esta a instância compartilhada. */
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
    /* Quando jogo é iniciado, executa o método SetupScene */
    void Start()
    {
        SetupScene();
    }

    //Executa o método SpawnPlayer
    public void SetupScene()
    {
        SpawnPlayer();
    }

    /* Se não houver um ponto de spawn associado, spawna um jogador no ponto de Spawn inicial
	 * Em seguida, utiliza o script cameraManager para colocar a instância compartilhada da câmera para seguir as coordenadas do jogador */
    public void SpawnPlayer()
    {
        if (playerPontoSpawn != null)
        {
            GameObject player = playerPontoSpawn.SpawnO();
            cameraManager.virtualCamera.Follow = player.transform;
        }
    }
}
