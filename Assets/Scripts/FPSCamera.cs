using Unity.Cinemachine;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    private CinemachineInputAxisController inputAxisController;

    private void Awake()
    {
        inputAxisController = GetComponent<CinemachineInputAxisController>();
    }
    public void SetGain(float gain)
    {
        if (inputAxisController != null)
        {
            foreach (var c in inputAxisController.Controllers)
            {
                if (c.Name == "Look X (Pan)")
                {
                    c.Input.Gain = gain;
                }
                if (c.Name == "Look Y (Tilt)")
                {
                    c.Input.Gain = gain * -1;
                }
            }
        }
    }
}