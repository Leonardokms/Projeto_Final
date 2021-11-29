using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Atualiza��es referentes � barra de vida do personagem
/// </summary>
public class HealthBar : MonoBehaviour
{
    public PontosDano pontosDano;   // Objeto de leitura dos dados de quantos pontos tem o Player
    public Player caractere;        // receber� o objeto do Player 
    public Image medidorImagem;     // recebe a barra de medi��o
    public Text pdTexto;            // recebe os dados de PD
    float maxPontosDano;            // armazena a vari�vel limite de "s�ude" do Player

    // Start is called before the first frame update
	/* Define a quantidade de vida m�xima do personagem */
    void Start()
    {
        maxPontosDano = caractere.MaxPontosDano;
    }

    // Update is called once per frame
	/* Muda o sprite do medidor de vida baseado em quanto o jogador tem atualmente de vida e atualiza o texto de acordo. */
    void Update()
    {
        if(caractere != null)
        {
            medidorImagem.fillAmount = pontosDano.valor / maxPontosDano;
            pdTexto.text = "PD:" + (medidorImagem.fillAmount * 100); 
        }
    }
}