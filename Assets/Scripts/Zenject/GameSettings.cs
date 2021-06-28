using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Installers/GameSettings")]
public class GameSettings : ScriptableObjectInstaller<GameSettings>
{
    public PlayerSettings playerSettings;
    public ShapeSettings shapeSettings;
    public ProjectileSettings projectileSettings;
    public override void InstallBindings()
    {
        Container.BindInstance(playerSettings);
        Container.BindInstance(shapeSettings);
        Container.BindInstance(projectileSettings);
    }
}