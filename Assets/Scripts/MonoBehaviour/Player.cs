using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Define as características de UI do player (inventário e barra de vida), além de definir os eventos de colisão com itens e morte
/// </summary>

public class Player : Caractere
{
    public Inventario inventarioPrefab;     // referência ao objeto prefab criado do Inventário
    Inventario inventario;
    public HealthBar healthBarPrefab;       // referência ao objeto prefab criado da HealthBar
    HealthBar healthBar;
    int contador;                           // contador de itens restantes
    public PontosDano pontosDano;           // tem o valor da "saúde" do objeto script
    
	/* Assim que o script inicia, instancia um inventário para o jogador, define seus pontos de vida, 
	 * instancia uma barra de vida para o jogador e associa	este caractere à barra de vida criada */
	private void Start()
    {
        inventario = Instantiate(inventarioPrefab);
        pontosDano.valor = inicioPontosDano;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        GameObject[] objetos;
        objetos = GameObject.FindGameObjectsWithTag("Coletavel");
        contador = objetos.Length;
    }
    
	/* Ao receber dano, inicia a corrotina FlickerCaractere e diminui a vida do jogador. 
	 * Se a vida cair abaixo de 0, destrói o objeto e carrega a cena de GameOver. */
	public override IEnumerator DanoCaractere(int dano, float intervalo)
    {
        while(true)
        {
            StartCoroutine(FlickerCaractere());
            pontosDano.valor = pontosDano.valor - dano;
            if(pontosDano.valor <= float.Epsilon)
            {
                KillCaractere();
                SceneManager.LoadScene("Lab5_GameOver");
                break;
            }
            if(intervalo > float.Epsilon)
            {
                yield return new WaitForSeconds(intervalo);
            }
            else
            {
                break;
            }
        }
    }
    
	/* Ao reiniciar o jogador, instancia um inventário para o jogador, define seus pontos de vida, instancia uma barra de vida para o jogador e associa
	 * este caractere à barra de vida criada */
	public override void ResetCaractere()
    {
        inventario = Instantiate(inventarioPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        pontosDano.valor = inicioPontosDano;
    }
    
	/* Destrói o objeto jogador, a barra de vida e o inventário associados ao jogador */
	public override void KillCaractere()
    {
        base.KillCaractere();
        Destroy(healthBar.gameObject);
        Destroy(inventario.gameObject);
    }
    
	/* Ao encostar em um item, verifica se é uma moeda ou um coração. 
	 * Se for uma moeda, adiciona ao inventário. 
	 * Se for um coração, cura a vida do personagem.
	 * Ao final, faz o item desaparecer se for o caso.*/
	private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coletavel"))
        {

            contador--;
            Item DanoObjeto = collision.gameObject.GetComponent<Consumable>().item;
            if (DanoObjeto != null)
            {
                bool DeveDesaparecer = false;

                switch (DanoObjeto.tipoItem)
                {
                    case Item.TipoItem.MOEDA:
                        //DeveDesaparecer = true;
                        DeveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;

                    case Item.TipoItem.PEIXE:
                        DeveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;

                    case Item.TipoItem.BOMBA:
                        DeveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;

                    case Item.TipoItem.GARRAFA:
                        DeveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;

                    case Item.TipoItem.DISCO:
                        DeveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;

                    default:
                        break;
                }

                if(DeveDesaparecer)
                {
                    collision.gameObject.SetActive(false);
                }    
                
                if(contador == 0)
                {
                    if (SceneManager.GetActiveScene().name == "Lab5_RPGSetup")
                    {
                        SceneManager.LoadScene("Lab5_CenaAdicional");
                    }
                    else if (SceneManager.GetActiveScene().name == "Lab5_CenaAdicional")
                    {
                        SceneManager.LoadScene("Lab5_Vitoria");
                    }
                }
            }
        }

        else if (collision.gameObject.CompareTag("Vida"))
        {
            bool DeveDesaparecer = false;

            Item DanoObjeto = collision.gameObject.GetComponent<Consumable>().item;

            DeveDesaparecer = AjustePontosDano(DanoObjeto.quantidade);

            if (DeveDesaparecer)
            {
                collision.gameObject.SetActive(false);
            }
        }
    }
    
	/* Caso os pontos de dano do personagem estejam menores que o máximo, soma uma quantidade a eles. */
	public bool AjustePontosDano(int quantidade)
    {
        if (pontosDano.valor < MaxPontosDano)
        {
            pontosDano.valor = pontosDano.valor + quantidade;
            print("Ajustado PD por: " + quantidade + ". Novo valor = " + pontosDano.valor);
            return true;
        }
        else return false;
    }
}
