using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> 
/// Classe que guarda funções para os botões
/// </summary>
public class RPGManageBotoes : MonoBehaviour 
{
    /* Reinicia o jogo, levando para o inicio do jogo */
    public void ReiniciarJogo()                     
    {
        SceneManager.LoadScene("Lab5_RPGSetup");
    }

    /* Inicia a tela de créditos */
    public void Creditos()
    {
        SceneManager.LoadScene("Lab5_Creditos");
    }

    /* Fecha o modo de teste da aplicação */
    public void Finalizar()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
