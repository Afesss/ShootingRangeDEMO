using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Transform aim;

    private PlayerSettings playerSettings;
    private Joystick joystick;
    private Vector3 aimRotation;
    
    [Inject]
    private void Construct(Joystick joystick, PlayerSettings playerSettings)
    {
        this.joystick = joystick;
        this.playerSettings = playerSettings;
    }
    private void Start()
    {
        aimRotation = new Vector3(aim.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        
    }
    private void Update()
    {
        Aim(joystick.InputDirecion);
    }
    private void Aim(Vector2 input)
    {
        aimRotation += new Vector3(input.y, input.x,0) * playerSettings.aimSpeed * Time.deltaTime;
        aimRotation.x = Mathf.Clamp(aimRotation.x, -playerSettings.minYLimit, playerSettings.maxYLimit);

        transform.rotation = Quaternion.Euler(0, aimRotation.y, 0);
        aim.localRotation = Quaternion.Euler(-aimRotation.x,0 , 0);
    }
    
}
