using UnityEngine;
using Leopotam.Ecs;

public class ProjectilesDeathSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileTag, DeathRequest, GameObjectComponent> _pistolShotsSystem;

    public void Run()
    {
        foreach (var i in _pistolShotsSystem)
        {
            ref var entity = ref _pistolShotsSystem.GetEntity(i);
            ref var gameObject = ref _pistolShotsSystem.Get3(i);

            GameObject.Destroy(gameObject.GameObject);
            entity.Destroy();
        }
    }
}