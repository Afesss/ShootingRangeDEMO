using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Joystick>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ShapeGenerator>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ShotController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<GameOver>().AsSingle();
    }
}