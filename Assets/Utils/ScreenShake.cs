using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ScreenShake : MonoBehaviour
{

    private CinemachineVirtualCamera CinemachineVirtualCamera;
    private float shakeIntensity = 1.5f;
    private float shakeTime = .3f;

    private float timer;
    private CinemachineBasicMultiChannelPerlin cbmcp;


    #region Singleton
    public static ScreenShake Instance { get; private set; }

    private void Awake()
    {
        // Se não houver uma instância ainda, define esta como a instância e a persiste entre cenas
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // Caso já exista uma instância, destrói o novo objeto para garantir o Singleton
            Destroy(gameObject);
        }
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    #endregion
   
    public void ShakeCamera()
    {
        cbmcp.m_AmplitudeGain = shakeIntensity;

        timer = shakeTime;
    }

    private void StopShake()
    {
        cbmcp.m_AmplitudeGain = 0f;

        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                StopShake();
            }
        }
    }
}
