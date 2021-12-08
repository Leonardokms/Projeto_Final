using UnityEngine;

[CreateAssetMenu(menuName ="Item")]
///<summary>
/// Define os itens do jogo e suas características
///</summary>
public class Item : ScriptableObject
{
    public string NomeObjeto;       // Armazea o nome do item
    public Sprite sprite;           // Armazena a sprite do item
    public int quantidade;          // Armazena a quantidade que o item representa
    public bool empilhavel;         // Armazena se o item é empilhável ou não

	/* Enumera os tipos de item possíveis */
    public enum TipoItem
    {
        GOLD,
        CORACAO,
        BOTAS,
        ESCUDO,
        ESPADA,
        POCAO
    }

    public TipoItem tipoItem;
}
