using UnityEngine;
using Leopotam.Ecs;

public class ProjectilesDestroyDeathSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileComponent, DeathRequest, GameObjectComponent> _pistolShotsSystem;

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

public class ProjectilesPoolDeathSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileComponent, DeathRequest, GameObjectComponent> _pistolShotsSystem;

    private IObjectPool<Projectile> _projectilesPool;

    public void Run()
    {
        foreach (var i in _pistolShotsSystem)
        {
            ref var entity = ref _pistolShotsSystem.GetEntity(i);
            ref var gameObject = ref _pistolShotsSystem.Get3(i);
            ref var projectile = ref _pistolShotsSystem.Get1(i);

            projectile.Projectile.ProjectileHitbox.HitEnityRecievedEvent = null;
            _projectilesPool.Return(gameObject.GameObject);
            entity.Destroy();
        }
    }
}