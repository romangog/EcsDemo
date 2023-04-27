using Leopotam.Ecs;

public class PuddleSpawnFireEffectSystem : IEcsRunSystem
{
    private EcsFilter<PuddleTag, OnSpawnRequest, PuddleParticlesComponent> _spawnedPuddlesFilter;

    private WeaponUpgradeLevels _weaponUpgrades;

    public void Run()
    {
        if (_weaponUpgrades.FireLevel == 0) return;

        foreach (var i in _spawnedPuddlesFilter)
        {
            ref var puddleEntity = ref _spawnedPuddlesFilter.GetEntity(i);
            ref var puddleParticles = ref _spawnedPuddlesFilter.Get3(i);

            puddleEntity.Get<FireEffectTag>();
            puddleParticles.FireFx.Play();
        }
    }
}



