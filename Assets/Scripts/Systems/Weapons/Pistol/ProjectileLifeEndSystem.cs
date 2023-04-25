using UnityEngine;
using Leopotam.Ecs;

public class ProjectileLifeEndSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileTag, MoveForwardComponent, PathLengthAccumulativeComponent> _projectilesLifeFilter;
    private EcsFilter<ProjectileTag, MoveForwardComponent, PathLengthAccumulativeComponent, ProjectileFragmentTag> _fragmentsLifeFilter;

    private GameSettings _gameSettings = null;
    public void Run()
    {
        foreach (var i in _projectilesLifeFilter)
        {
            ref var entity = ref _projectilesLifeFilter.GetEntity(i);
            ref var moveForward = ref _projectilesLifeFilter.Get2(i);
            ref var pathLength = ref _projectilesLifeFilter.Get3(i);

            pathLength.AccumulativeLength += moveForward.Speed * Time.deltaTime;
            if(pathLength.AccumulativeLength >= _gameSettings.BulletsPathLengthMax)
            {
                entity.Get<DeathRequest>();
            }
        }

        foreach (var i in _fragmentsLifeFilter)
        {
            ref var entity = ref _fragmentsLifeFilter.GetEntity(i);
            ref var moveForward = ref _fragmentsLifeFilter.Get2(i);
            ref var pathLength = ref _fragmentsLifeFilter.Get3(i);

            pathLength.AccumulativeLength += moveForward.Speed * Time.deltaTime;
            if (pathLength.AccumulativeLength >= _gameSettings.FragmentsPathLengthMax)
            {
                entity.Get<DeathRequest>();
            }
        }
    }
}



