using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

/// <summary>
/// Define aspectos da arma do player como: munição utilizada, quantidade e velocidade de munição e cálculo para direção da animação de atirar
/// </summary>
public class Armas : MonoBehaviour
{
    public GameObject municaoPrefab;                    // Armazena o prefab da munição
    static List<GameObject> municaoPiscina;             // Pool de munição
    public int tamanhoPiscina;                          // Tamanho da piscina
    public float velocidadeArma;                        // Velocidade da munição

    bool atirando;
    [HideInInspector]
    public Animator animator;

    Camera cameraLocal;

    float slopePositivo;
    float slopeNegativo;

    /* Define os quadrantes existentes */
    enum Quadrante
    {
        Leste,
        Sul,
        Oeste,
        Norte
    }

    /* Define os vetores de posição de acordo com a tela e relaciona eles com valores de slope negativo e positivo para definir direção do "tiro" */
    private void Start()
    {
        animator = GetComponent<Animator>();
        atirando = false;
        cameraLocal = Camera.main;
        Vector2 abaixoEsquerda = cameraLocal.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 acimaDireita = cameraLocal.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 acimaEsquerda = cameraLocal.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 abaixoDireita = cameraLocal.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        slopePositivo = PegaSlope(abaixoEsquerda, acimaDireita);
        slopeNegativo = PegaSlope(acimaEsquerda, abaixoDireita);
    }
    /* Verifica se o clique foi acima do slope positivo */
    bool AcimaSlopePositivo(Vector2 posicaoEntrada)
    {
        Vector2 posicaoPlayer = gameObject.transform.position;
        Vector2 posicaoMouse = cameraLocal.ScreenToWorldPoint(posicaoEntrada);
        float interseccaoY = posicaoPlayer.y - (slopePositivo * posicaoPlayer.x);
        float entradaInterseccao = posicaoMouse.y - (slopePositivo * posicaoMouse.x);
        return entradaInterseccao > interseccaoY;
    }
    /* Verifica se o clique foi acima do slope negativo */
    bool AcimaSlopeNegativo(Vector2 posicaoEntrada)  
    {
        Vector2 posicaoPlayer = gameObject.transform.position;
        Vector2 posicaoMouse = cameraLocal.ScreenToWorldPoint(posicaoEntrada);
        float interseccaoY = posicaoPlayer.y - (slopeNegativo * posicaoPlayer.x);
        float entradaInterseccao = posicaoMouse.y - (slopeNegativo * posicaoMouse.x);
        return entradaInterseccao > interseccaoY;
    }

    /* Retorna o quadrante clicado */
    Quadrante PegaQuadrante()
    {
        Vector2 posicaoMouse = Input.mousePosition;
        Vector2 posicaoPlayer = transform.position;
        bool acimaSlopePositivo = AcimaSlopePositivo(Input.mousePosition);
        bool acimaSlopeNegativo = AcimaSlopeNegativo(Input.mousePosition);
        if (!acimaSlopePositivo && acimaSlopeNegativo)
        {
            return Quadrante.Leste;
        }
        if(!acimaSlopePositivo && !acimaSlopeNegativo)
        {
            return Quadrante.Sul;       
        }
        if (acimaSlopePositivo && !acimaSlopeNegativo)
        {
            return Quadrante.Oeste;
        }
        else
        {
            return Quadrante.Norte;          
        }
    }
    /* Atualiza estado de animação do tiro */
    void UpdateEstado()
    {
        if(atirando)
        {
            Vector2 vetorQuadrante;
            Quadrante quadranteEnum = PegaQuadrante();
            switch(quadranteEnum)
            {
                case Quadrante.Leste:
                    vetorQuadrante = new Vector2(1.0f, 0.0f);
                    break;
                case Quadrante.Sul:
                    vetorQuadrante = new Vector2(0.0f, -1.0f);
                    break;
                case Quadrante.Oeste:
                    vetorQuadrante = new Vector2(-1.0f, 0.0f);
                    break;
                case Quadrante.Norte:
                    vetorQuadrante = new Vector2(0.0f, 1.0f);
                    break;
                default:
                    vetorQuadrante = new Vector2(0.0f, 0.0f);
                    break;
            }
            animator.SetBool("Atirando", true);
            animator.SetFloat("AtiraX", vetorQuadrante.x);
            animator.SetFloat("AtiraY", vetorQuadrante.y);
            //Debug.Log("Atira X: " + vetorQuadrante.x + " AtiraY: " + vetorQuadrante.y);
            atirando = false;
        }
        else
        {
            animator.SetBool("Atirando", false);
        }
    }

    /* Instancia uma pool de munição */
    public void Awake()
    {
        if (municaoPiscina == null)
        {
            municaoPiscina = new List<GameObject>();
        }
        for(int i = 0; i < tamanhoPiscina; i++)
        {
            GameObject municaoO = Instantiate(municaoPrefab);
            municaoO.SetActive(false);
            municaoPiscina.Add(municaoO);
        }
    }

    /* Verifica a cada frame se está atirando */
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            atirando = true;
            DisparaMunicao();
        }
        UpdateEstado();
    }

    /* Retorna o valor entre dois vetores para definir o slope positivo e negativo */
    float PegaSlope(Vector2 ponto1, Vector2 ponto2)
    {
        return (ponto2.y - ponto1.y) / (ponto2.x - ponto1.x);
    }

    /* Gera uma munição na tela quando existe espaço na pool e o player clica */
    public GameObject SpawnMunicao(Vector3 posicao)
    {
        foreach(GameObject municao in municaoPiscina)
        {
            if(municao.activeSelf == false)
            {
                municao.SetActive(true);
                municao.transform.position = posicao;
                return municao;
            }
        }
        return null;
    }

    /* Dispara a munição utilizando a função SpawnMunicao e criando uma trajetória entre o ponto inicial e o local clicado*/
    void DisparaMunicao()
    {
        Vector3 posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject municao = SpawnMunicao(transform.position);
        if(municao != null)
        {
            Arco ArcoScript = municao.GetComponent<Arco>();
            float duracaoTrajetoria = 1.0f / velocidadeArma;
            StartCoroutine(ArcoScript.arcoTrajetoria(posicaoMouse, duracaoTrajetoria));
        }
    }
    /* Torna a pool como null */
    private void OnDestroy()
    {
        municaoPiscina = null;
    }
}
