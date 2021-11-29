using UnityEngine;
using Cinemachine;

/// <summary>
/// Define um camera manager que se relaciona com a virtual camera
/// </summary>
public class RPGCameraManager : MonoBehaviour
{
    public static RPGCameraManager instanciaCompartilhada = null;

    [HideInInspector]
    public CinemachineVirtualCamera virtualCamera;

    /* Assim que o script � iniciado, verifica se j� h� uma inst�ncia de Camera Manager ativa. Se houver, destr�i esta inst�ncia. Se n�o, torna esta a inst�ncia compartilhada.
	 * Assinala o componente c�mera virtual Cinemachine */
    private void Awake()
    {
        if (instanciaCompartilhada != null && instanciaCompartilhada != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instanciaCompartilhada = this;
        }
        GameObject vCamGameObject = GameObject.FindWithTag("Virtual Camera");
        virtualCamera = vCamGameObject.GetComponent<CinemachineVirtualCamera>();
    }
}
