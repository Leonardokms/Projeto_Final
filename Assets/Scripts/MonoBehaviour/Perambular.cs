using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]

/// <summary>
/// Define as rotinas e características da função "perambular" dos inimigos
/// </summary>
public class Perambular : MonoBehaviour
{
    public float velocidadePerseguicao;             // Velocidade do "Inimigo" na área de Spot
    public float velocidadePerambular;              // Velocidade do "Inimigo" paseando
    float velocidadeCorrente;                       // Velocidade do "Inimigo" atribuída

    public float intervaloMudancaDirecao;           // Tempo para alterar direção 
    public bool perseguePlayer;                     // Indicador de perseguidor ou não

    Coroutine MoverCoroutine;

    Rigidbody2D rb2D;                               // Armazena o componente rigidbody2D
    Animator animator;                              // Armazena o componente Animator

    Transform alvoTransform = null;                 // Armazena o componente Transform do Alvo 

    Vector3 posicaoFinal;
    float anguloAtual = 0;                          // Ângulo atribuído

    CircleCollider2D circleCollider;                // Armazena o componente de Spot    

    // Start is called before the first frame update
	/* Recebe os componentes Animator, RigidBody2D e CircleCollider2D, define a velocidadeCorrente como a velocidadePerambular e inicia a Corrotina RotinaPerambular */
    void Start()
    {
        animator = GetComponent<Animator>();
        velocidadeCorrente = velocidadePerambular;
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(RotinaPerambular());
        circleCollider = GetComponent<CircleCollider2D>();
    }

	/* Desenha uma esfera ao redor do caractere */
    private void OnDrawGizmos()
    {
        if(circleCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
        }
    }
	
	/* Escolhe um novo ponto final e inicia a corrotina Mover para levar o caractere até este. */
    public IEnumerator RotinaPerambular()
    {
        while(true)
        {
            EscolheNovoPontoFinal();
            if(MoverCoroutine != null)
            {           
                StopCoroutine(MoverCoroutine);
            }
            MoverCoroutine = StartCoroutine(Mover(rb2D, velocidadeCorrente));
            yield return new WaitForSeconds(intervaloMudancaDirecao);
            animator.SetBool("Caminhando", false);
        }
    }
	
	/* Define aleatóriamente um ângulo para o qual o caractere andará */
    void EscolheNovoPontoFinal()
    {        
        anguloAtual += Random.Range(0, 360);
        anguloAtual = Mathf.Repeat(anguloAtual, 360);
        posicaoFinal += Vector3ParaAngulo(anguloAtual);
    }
	
	/* Converte um float de ângulo para um Vector3 representando o ângulo */
    Vector3 Vector3ParaAngulo(float anguloEntradaGraus)
    {
        float anguloEntradaGrausRadianos = anguloEntradaGraus * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(anguloEntradaGrausRadianos), Mathf.Sin(anguloEntradaGrausRadianos), 0);
    }
	
	
	/* Enquanto a distância entre o caractere e a posição final for maior que 0, move o caractere na direção do ponto final */
    public IEnumerator Mover(Rigidbody2D rbParaMover, float velocidade)
    {
        float distanciaFaltante = (transform.position - posicaoFinal).sqrMagnitude;
        while (distanciaFaltante > float.Epsilon)
        {
            if (alvoTransform != null)
            {
                posicaoFinal = alvoTransform.position;
            }
            if(rbParaMover != null)
            {
                animator.SetBool("Caminhando", true);
                Vector3 novaPosicao = Vector3.MoveTowards(rbParaMover.position, posicaoFinal, velocidade * Time.deltaTime);
                rb2D.MovePosition(novaPosicao);
                distanciaFaltante = (transform.position- posicaoFinal).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("Caminhando", false);          

    }

	/* Se o jogador entrar no alcance da esfera desenhada anteriormente, faz com que o caractere persiga o jogador */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && perseguePlayer)
        {
            velocidadeCorrente = velocidadePerseguicao;
            alvoTransform = collision.gameObject.transform;
            if(MoverCoroutine != null)
            {
                StopCoroutine(MoverCoroutine);
            }
            MoverCoroutine = StartCoroutine(Mover(rb2D, velocidadeCorrente));
        }
    }

	/* Quando o jogador sai do alcance da esfera, para o movimento do caractere e reinicia o comportamento normal de perambular */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {   
            animator.SetBool("Caminhando", false);
            velocidadeCorrente = velocidadePerambular;
            if(MoverCoroutine != null)
            {
                StopCoroutine(MoverCoroutine);
            }
            alvoTransform = null;
        }
    }
	
    // Update is called once per frame
	/* Desenha uma linha para debug da posição atual do caractere até a posição final que o caractere está atualmente usando de referência */
    void Update()
    {
        Debug.DrawLine(rb2D.position, posicaoFinal, Color.red);
    }
}
