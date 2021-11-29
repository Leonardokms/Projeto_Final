using UnityEngine;

[CreateAssetMenu(menuName ="Item")]
///<summary>
/// Define os itens do jogo e suas características
///</summary>
public class Item : ScriptableObject
{
    public string NomeObjeto;
    public Sprite sprite;
    public int quantidade;
    public bool empilhavel;

	/* Enumera os tipos de item possíveis */
    public enum TipoItem
    {
        MOEDA,
        HEALTH,
        PEIXE,
        BOMBA,
        DISCO,
        GARRAFA
    }

    public TipoItem tipoItem;
}
