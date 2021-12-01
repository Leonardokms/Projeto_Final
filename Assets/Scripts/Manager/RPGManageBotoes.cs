using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> 
/// Classe que guarda fun��es para os bot�es
/// </summary>
public class RPGManageBotoes : MonoBehaviour 
{
    /* Reinicia o jogo, levando para o inicio do jogo */
    public void ReiniciarJogo()                     
    {
        SceneManager.LoadScene("Cena_Fase1");
    }

    /* Inicia a tela de cr�ditos */
    public void Creditos()
    {
        SceneManager.LoadScene("Cena_Creditos");
    }

    /* Fecha o modo de teste da aplica��o */
    public void Finalizar()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
