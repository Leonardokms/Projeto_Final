using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Define inventário e informações do inventário do player, adicionando itens conforme definido
/// </summary>
public class Inventario : MonoBehaviour
{
    public GameObject slotPrefab;               // objeto que recebe o prefab slot
    public const int numSlots = 5;              // numero fixo de Slots
    Image[] itemImagens = new Image[numSlots];  // array de imagens
    Item[] items = new Item[numSlots];          // array de items
    GameObject[] slots = new GameObject[numSlots]; // array de slots

	/* Assim que o script inicia, chama o método CriaSlots. */
    void Start()
    {
        CriaSlots();
    }

	/* Cria novos slots de inventário até um certo limite pré-definido */
    public void CriaSlots()
    {
        if(slotPrefab != null)
        {
            for(int i = 0; i < numSlots; i++)
            {
                GameObject novoSlot = Instantiate(slotPrefab);
                novoSlot.name = "ItemSlot_" + i;
                novoSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);
                slots[i] = novoSlot;
                itemImagens[i] = novoSlot.transform.GetChild(1).GetComponent<Image>();
            }
        }
    }
	
	/* Verifica todos os slots do inventário; Caso o item a ser adicionado seja empilhável e já esteja presente no inventário, adiciona na quantidade do item
	 * naquele slot. Se não for empilhável ou não estiver presente no inventário, adiciona o item ao primeiro slot vazio que encontrar.	*/
    public bool AddItem(Item itemToAdd)
    {
        for(int i = 0; i<items.Length; i++)
        {
            if (items[i] != null && items[i].tipoItem == itemToAdd.tipoItem && itemToAdd.empilhavel == true)
            {
                items[i].quantidade = items[i].quantidade + 1;
                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();
                Text quantidadeTexto = slotScript.qtdTexto;
                quantidadeTexto.enabled = true;
                quantidadeTexto.text = items[i].quantidade.ToString();
                return true;
            }
            if(items[i] == null)
            {
                items[i] = Instantiate(itemToAdd);
                items[i].quantidade = 1;
                itemImagens[i].sprite = itemToAdd.sprite;
                itemImagens[i].enabled = true;
                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();
                Text quantidadeTexto = slotScript.qtdTexto;
                quantidadeTexto.enabled = true;
                quantidadeTexto.text = items[i].quantidade.ToString();
                return true;
            }
        }
        return false;
    }
}
