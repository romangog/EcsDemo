using Leopotam.Ecs;

public class ProjectilesVampirismDeathSystem : IEcsRunSystem
{
    private EcsFilter<DeathRequest, ProjectileComponent, ElementalParticlesComponent> _dyingVampirismProjectiles;

    public void Run()
    {
        foreach (var i in _dyingVampirismProjectiles)
        {
            ref var particles = ref _dyingVampirismProjectiles.Get3(i);
            particles.VampirismFx.emitting = false;
        }
    }
}


