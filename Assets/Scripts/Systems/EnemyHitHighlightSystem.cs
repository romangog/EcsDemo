using UnityEngine;
using Leopotam.Ecs;

public class EnemyHitHighlightSystem : IEcsRunSystem
{
    EcsFilter<HitImpactRequest, SpriteRendererComponent> _hitImpactFilter;
    EcsFilter<EnemyHitHighlightTimer, SpriteRendererComponent> _hitHighlightFilter;

    public void Run()
    {
        foreach (var i in _hitImpactFilter)
        {
            ref var entity = ref _hitImpactFilter.GetEntity(i);
            ref var sprite = ref _hitImpactFilter.Get2(i);

            sprite.SpriteRenderer.color = Color.white * 0.5f;

            entity.Get<EnemyHitHighlightTimer>().Timer.Set(0.1f);
        }

        foreach (var i in _hitHighlightFilter)
        {
            ref var entity = ref _hitHighlightFilter.GetEntity(i);
            ref var timer = ref _hitHighlightFilter.Get1(i);
            ref var spriteRenderer = ref _hitHighlightFilter.Get2(i);

            timer.Timer.Update();
            if(timer.Timer.IsOver)
            {
                entity.Del<EnemyHitHighlightTimer>();
                spriteRenderer.SpriteRenderer.color = Color.black;
            }
        }
    }
}

