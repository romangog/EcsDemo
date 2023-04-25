using Leopotam.Ecs;
using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<EcsWorld>().AsSingle().NonLazy();
        Container.Bind<WeaponUpgradeLevels>().AsSingle().NonLazy();
        Container.Bind<BulletSpawner>().AsSingle().NonLazy();
        Container.Bind<LevelData>().AsSingle().NonLazy();
    }
}