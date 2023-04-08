using UnityEngine;
using Leopotam.Ecs;

public class EnemyHitImpactSystem : IEcsRunSystem
{
    EcsFilter<EnemyTag, HitImpactRequest, RigidbodyComponent> _enemyHitImpactFilter;

    public void Run()
    {
        foreach (var i in _enemyHitImpactFilter)
        {
            ref var entity = ref _enemyHitImpactFilter.GetEntity(i);
            ref var impact = ref _enemyHitImpactFilter.Get2(i);
            ref var rigidbody = ref _enemyHitImpactFilter.Get3(i);
            rigidbody.Rigidbody.AddForce(impact.PushForce, ForceMode2D.Impulse);

            entity.Get<HitThrowbackTimer>().Timer.Set(0.5f);
        }
    }
}
