using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform player;

    private PlayerMovementConfig _playerMovementConfig;

    private float SensMultiplie = 1f;

    private bool isLimit;
    private float rotationCenter;
    private float rotationLim;

    private float _rotationTarget;
    private bool isRotation;

    public float pitch;
    public float yaw;

    public void Initialize(PlayerMovementConfig playerMovementConfig)
    {
        _playerMovementConfig = playerMovementConfig;
    }

    public void SetSensMultiplie(float multi)
    {
        SensMultiplie = multi;
    }

    public void SetRotationLimit(float lim)
    {
        isLimit = true;
        rotationLim = lim;
        rotationCenter = yaw;
    }

    public void ClearRotationLimit()
    {
        isLimit = false;
    }

    public void Rotate180()
    {
        if (isRotation)
            return;

        _rotationTarget =  yaw + 180;
        isRotation = true;
    }
    
    private void Update()
    {
        if (isRotation) 
        {
            yaw = Mathf.MoveTowards(yaw, _rotationTarget, 720f * Time.deltaTime);

            player.rotation = Quaternion.Euler(0f, yaw, 0f);

            if (Mathf.Approximately(yaw, _rotationTarget))
                isRotation = false;

            return;
        }

        HandleRotation();
    }

    private void HandleRotation()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        yaw += mouseDelta.x * _playerMovementConfig.SensMouse * SensMultiplie;

        if (isLimit)
        {
            float delta = Mathf.DeltaAngle(rotationCenter, yaw);
            delta = Mathf.Clamp(delta, -rotationLim, rotationLim);
             
            yaw = rotationCenter + delta;
        }

        player.rotation = Quaternion.Euler(0, yaw, 0);

        pitch -= mouseDelta.y * _playerMovementConfig.SensMouse * SensMultiplie;

        pitch = Mathf.Clamp(pitch, -70f, 70f);

        
        
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
