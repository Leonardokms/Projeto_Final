using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define dados sobre o movimento do jogador como velocidade e animação de sua sprite
/// </summary>
public class MovimentaPlayer : MonoBehaviour
{
    public float VelocidadeMovimento = 3.0f;        // Equivale ao momento (impulso) a ser dado ao player
    Vector2 Movimento = new Vector2();              // Detectar movimento pelo teclado

    Animator animator;                              // Guarda a componente do Controlador de Animação
    // string estadoAnimacao = "EstadoAnimacao";    // Guarda o nome do parâmetro de Animação (Desnecessário com a Blend Tree [Andar Tree])
    Rigidbody2D rb2D;                               // Guarda a componente CorpoRigido do Player

    /*                                              // Desnecessário pela Blend Tree (Andar Tree)
    enum EstadosCaractere
    {
        andaLeste = 1,
        andaOeste = 2,
        andaNorte = 3,
        andaSul = 4,
        idle = 5
    }
    */

    // Start is called before the first frame update
	/* Ao habilitar o script, busca os componentes Animator e RigidBody2D */
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
	/* Uma vez por frame, chama a função UpdateEstado */
    void Update()
    {
        UpdateEstado();
    }
	
	/* Chama a função MoveCaractere a cada FixedUpdate */
    private void FixedUpdate()
    {
        MoveCaractere();
    }

	/* Define o vetor movimento recebendo o input dos eixos vertical e horizontal, normalizando o vetor resultante e multiplicando pela velocidade do jogador */
    private void MoveCaractere()
    {
        Movimento.x = Input.GetAxisRaw("Horizontal");
        Movimento.y = Input.GetAxisRaw("Vertical");
        Movimento.Normalize();
        rb2D.velocity = Movimento * VelocidadeMovimento;
    }

    /* Atualiza o estado do jogador para detectar caso esteja andando ou não para o animador e qual a sua velocidade em cada direção */
    void UpdateEstado()
    {
        if (Mathf.Approximately(Movimento.x, 0) && (Mathf.Approximately(Movimento.y, 0)))
        {
            animator.SetBool("Caminhando", false);
        }
        else
        {
            animator.SetBool("Caminhando", true);
        }
        animator.SetFloat("dirX", Movimento.x);
        animator.SetFloat("dirY", Movimento.y);
    }
    /*
    private void UpdateEstado()
    {
        if(Movimento.x > 0)
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.andaLeste);
        }
        else if(Movimento.x < 0)
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.andaOeste);
        }
        else if (Movimento.y > 0)
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.andaNorte);
        }
        else if (Movimento.y < 0)
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.andaSul);
        }
        else if (Movimento.x == 0 && Movimento.y == 0)
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.idle);
        }
    }
    */
}
