using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Atualiza??es referentes ? barra de vida do personagem
/// </summary>
public class HealthBar : MonoBehaviour
{
    public PontosDano pontosDano;   // Objeto de leitura dos dados de quantos pontos tem o Player
    public Player caractere;        // receber? o objeto do Player 
    public Image medidorImagem;     // recebe a barra de medi??o
    public Text pdTexto;            // recebe os dados de PD
    public float maxPontosDano;     // armazena a vari?vel limite de "s?ude" do Player

	/* Define a quantidade de vida m?xima do personagem */
    void Start()
    {
        maxPontosDano = caractere.MaxPontosDano;
    }

	/* Muda o sprite do medidor de vida baseado em quanto o jogador tem atualmente de vida e atualiza o texto de acordo. */
    void Update()
    {
        if(caractere != null)
        {
            maxPontosDano = caractere.MaxPontosDano;
            medidorImagem.fillAmount = pontosDano.valor / maxPontosDano;
            pdTexto.text = "PD:" + (pontosDano.valor * 10); 
        }
    }
}