using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Define as caracter�sticas de UI do player (invent�rio e barra de vida), al�m de definir os eventos de colis�o com itens e morte
/// </summary>

public class Player : Caractere
{
    public int Vida;
    public Inventario inventarioPrefab;     // Refer�ncia ao objeto prefab criado do Invent�rio
    Inventario inventario;
    public HealthBar healthBarPrefab;       // Refer�ncia ao objeto prefab criado da HealthBar
    HealthBar healthBar;
    int contador;                           // Contador de itens restantes
    public PontosDano pontosDano;           // Tem o valor da "sa�de" do objeto script
    public bool escudo;                     // Armazena se o jogador tem o escudo ou n�o
    public bool espada;                     // Armazena se o jogador tem a espada ou n�o
    public ParticleSystem particulasAndar;  // Sistema de part�culas para o player andando
	private Renderer particulasRenderer;    // Renderer para part�culas
	
	private bool andando;                   // Armazena se o jogador est� andando ou n�o
	private SpriteRenderer thisSprite;      // Renderer para sprite do jogador
	private Animator thisAnimator;          // Animator para jogador
	private Rigidbody2D thisBody;           // rb2d para jogador
	
	/* Assim que o script inicia, instancia um invent�rio para o jogador, define seus pontos de vida, 
	 * instancia uma barra de vida para o jogador e associa	este caractere � barra de vida criada */
	private void Start()
    {
        inventario = Instantiate(inventarioPrefab);
        pontosDano.valor = inicioPontosDano;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        GameObject[] objetos;
        objetos = GameObject.FindGameObjectsWithTag("Coletavel");
        contador = objetos.Length;
        escudo = false;
		
		particulasRenderer = particulasAndar.gameObject.GetComponent<Renderer>();
		particulasRenderer.sortingLayerName = "Caracteres";
		particulasRenderer.sortingOrder = 1;
		
		thisBody = this.gameObject.GetComponent<Rigidbody2D>();
		thisSprite = this.gameObject.GetComponent<SpriteRenderer>();
		thisAnimator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {		
		if((thisBody.velocity.x != 0 || thisBody.velocity.y != 0)){
			andando = true;
			particulasAndar.Play();
		}else{
			andando = false;
			particulasAndar.Stop();
		}
		if(thisBody.velocity.x > 0){
			thisSprite.flipX = true;
		}else if(thisBody.velocity.x < 0){
			thisSprite.flipX = false;
		}
		if(thisAnimator != null){
			thisAnimator.SetBool("andando", andando);
		}
    }
    /* Ao receber dano, inicia a corrotina FlickerCaractere e diminui a vida do jogador. 
	 * Se a vida cair abaixo de 0, destr�i o objeto e carrega a cena de GameOver. */
    public override IEnumerator DanoCaractere(int dano, float intervalo)
    {
        while(true)
        {
            StartCoroutine(FlickerCaractere());
            pontosDano.valor = pontosDano.valor - dano;
            if(pontosDano.valor <= float.Epsilon)
            {
                if (this != null)
                {
                    KillCaractere();
                }                
                SceneManager.LoadScene("Cena_Game_Over");
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
    
	/* Ao reiniciar o jogador, instancia um invent�rio para o jogador, define seus pontos de vida, instancia uma barra de vida para o jogador e associa
	 * este caractere � barra de vida criada */
	public override void ResetCaractere()
    {
        inventario = Instantiate(inventarioPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        pontosDano.valor = inicioPontosDano;
    }
    
	/* Destr�i o objeto jogador, a barra de vida e o invent�rio associados ao jogador */
	public override void KillCaractere()
    {
        base.KillCaractere();
        Destroy(healthBar.gameObject);
        Destroy(inventario.gameObject);
    }
    
	/* Ao encostar em um item, verifica se � uma moeda ou um cora��o. 
	 * Se for um ouro, adiciona ao invent�rio.
	 * Se for uma bota, aumenta a velocidade em 150
	 * Se for um escudo, diminui o dano recebido
	 * Se for uma espada, aumenta o dano causado
	 * Se for uma po��o, aumenta a vida m�xima em 1 unidade (10 pontos de vida)
	 * Se for um cora��o, cura a vida do personagem.
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
                    case Item.TipoItem.GOLD:
                        DeveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;

                    case Item.TipoItem.BOTAS:
                        this.GetComponent<MovPlayer>().Vel += 150;
                        DeveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;

                    case Item.TipoItem.ESCUDO:
                        escudo = true;
                        DeveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;

                    case Item.TipoItem.ESPADA:
                        espada = true;
                        DeveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;

                    case Item.TipoItem.POCAO:
                        MaxPontosDano += 1;
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
                    SceneManager.LoadScene("Cena_Vitoria");
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
    
	/* Caso os pontos de dano do personagem estejam menores que o m�ximo, soma uma quantidade a eles. */
	public bool AjustePontosDano(int quantidade)
    {
        if (pontosDano.valor < MaxPontosDano)
        {
            pontosDano.valor = pontosDano.valor + quantidade;
            print("Vida ajustada por: " + quantidade*10 + ". Novo valor = " + pontosDano.valor*10);
            return true;
        }
        else return false;
    }
}
