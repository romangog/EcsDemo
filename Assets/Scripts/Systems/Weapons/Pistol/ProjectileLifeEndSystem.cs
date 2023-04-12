using UnityEngine;
using Leopotam.Ecs;

public class ProjectileLifeEndSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileTag, MoveForwardComponent, PathLengthAccumulativeComponent> _projectilesLifeSystem;

    private GameSettings _gameSettings = null;
    public void Run()
    {
        foreach (var i in _projectilesLifeSystem)
        {
            ref var entity = ref _projectilesLifeSystem.GetEntity(i);
            ref var moveForward = ref _projectilesLifeSystem.Get2(i);
            ref var pathLength = ref _projectilesLifeSystem.Get3(i);

            pathLength.AccumulativeLength += moveForward.Speed * Time.deltaTime;
            if(pathLength.AccumulativeLength >= _gameSettings.BulletsPathLengthMax)
            {
                entity.Get<DeathRequest>();
            }
        }
    }
}

