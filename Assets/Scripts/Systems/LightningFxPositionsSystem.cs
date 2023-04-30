using Leopotam.Ecs;

public class LightningFxPositionsSystem : IEcsRunSystem
{
    private EcsFilter<AffectedTransformsComponent, LineRendererComponent, LightningFxTag> _lightningsFilter;
    public void Run()
    {
        foreach (var i in _lightningsFilter)
        {
            ref var affectedTargets = ref _lightningsFilter.Get1(i);
            ref var lineRenderer = ref _lightningsFilter.Get2(i);

            for (int j = 0; j < affectedTargets.Transforms.Count; j++)
            {
                if (affectedTargets.Transforms[j].Transform == null) continue;
                lineRenderer.LineRenderer.SetPosition(j, affectedTargets.Transforms[j].Transform.position);
            }
        }
    }
}




