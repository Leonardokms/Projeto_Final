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

    /* Assim que o script é iniciado, verifica se já há uma instância de Camera Manager ativa. Se houver, destrói esta instância. Se não, torna esta a instância compartilhada.
	 * Assinala o componente câmera virtual Cinemachine */
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
