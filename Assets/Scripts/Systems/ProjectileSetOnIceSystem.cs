using Leopotam.Ecs;
using UnityEngine;

public class ProjectileSetOnIceSystem : IEcsRunSystem
{
    private EcsFilter<SetBaseColorRequest, ProjectileShotRequest> _projectileShotFilter;

    private WeaponUpgradeLevels _weaponUpgrade = null;
    private GameSettings _gameSettings = null;

    public void Run()
    {
        if (_weaponUpgrade.IceLevel == 0) return;
        foreach (var i in _projectileShotFilter)
        {
            ref var setBaseColor = ref _projectileShotFilter.Get1(i);

            setBaseColor.BaseColor = _gameSettings.IceColor;
        }
    }
}
public class ProjectileSetVampirismSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileVampirismComponent, ElementalParticlesComponent, TransformComponent, ProjectileShotRequest > _projectileShotFilter;

    private WeaponUpgradeLevels _weaponUpgrade = null;
    private GameSettings _gameSettings = null;

    public void Run()
    {
        if (_weaponUpgrade.VampirismLevel == 0) return;
        foreach (var i in _projectileShotFilter)
        {
            ref var entity = ref _projectileShotFilter.GetEntity(i);
            ref var vampirism = ref _projectileShotFilter.Get1(i);
            ref var particles = ref _projectileShotFilter.Get2(i);
            ref var transform = ref _projectileShotFilter.Get3(i);
            if (entity.Has<ProjectileFragmentTag>())
            {
                if (vampirism.VampirismTimer.TimeLeft > 0)
                    particles.VampirismFx.emitting = true;

            }
            else
            {
                vampirism.VampirismTimer.Set(_gameSettings.VampirismTimer);
                particles.VampirismFx.emitting = true;
            }
            var positions = new Vector3[particles.VampirismFx.positionCount];
            for (int j = 0; j < positions.Length; j++)
            {
                positions[j] = transform.Transform.position;
            }
            particles.VampirismFx.SetPositions(positions);
        }
    }
}

public class ProjectileVampirismTimerSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileVampirismComponent, ElementalParticlesComponent> _projectileShotFilter;

    private WeaponUpgradeLevels _weaponUpgrade = null;

    public void Run()
    {
        if (_weaponUpgrade.VampirismLevel == 0) return;
        foreach (var i in _projectileShotFilter)
        {
            ref var vampirism = ref _projectileShotFilter.Get1(i);
            ref var particles = ref _projectileShotFilter.Get2(i);
            vampirism.VampirismTimer.Update();
            if (vampirism.VampirismTimer.IsOver)
            {
                ref var projectileEntity = ref _projectileShotFilter.GetEntity(i);
                projectileEntity.Del<ProjectileVampirismComponent>();
                particles.VampirismFx.emitting = false;
            }
        }
    }
}

