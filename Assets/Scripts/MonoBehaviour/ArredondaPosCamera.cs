using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
/// <summary>
/// Classe com funções para correção da posição da câmera
/// </summary>
public class ArredondaPosicao : CinemachineExtension
{
    public float PixelsPerUnit = 32;

	/* Corrige a posição da câmera baseado na posição arredondada do jogador */
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam, 
        CinemachineCore.Stage stage, 
        ref CameraState state, 
        float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            Vector3 pos = state.FinalPosition;
            Vector3 pos2 = new Vector3(Round(pos.x), Round(pos.y), pos.z);
            state.PositionCorrection += pos2 - pos;
        } 
    }
	
	/* Arredonda a posição do jogador considerando uma unidade de 32 píxeis */
    float Round (float x)
    {
        return Mathf.Round(x * PixelsPerUnit) / PixelsPerUnit;
    }
}