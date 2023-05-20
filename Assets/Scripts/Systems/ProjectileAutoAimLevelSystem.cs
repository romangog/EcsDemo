using Leopotam.Ecs;
using UnityEngine;

public class ProjectileAutoAimLevelSystem : IEcsRunSystem
{
    // Добавить компоненту авто-аима, убрать из системы проверку на аим
    private EcsFilter<ProjectileComponent, MoveForwardComponent, TransformComponent> _projectileFilter;
    private EcsFilter<EnemyTag, TransformComponent>.Exclude<DeadTag> _enemiesFilter;

    private WeaponUpgradeLevels _weaponUpgrades = null;

    public void Run()
    {
        if (_weaponUpgrades.GetProjectileAutoAimLevel() == 0) return;

        float rotateSpeed = _weaponUpgrades.GetProjectileAutoAimRotateSpeedFromLevel();
        float maxAngle = _weaponUpgrades.GetProjectileAutoAimViewAngleFromLevel();

        foreach (var i in _projectileFilter)
        {
            ref var proejctileEntity = ref _projectileFilter.GetEntity(i);
            ref var moveForward = ref _projectileFilter.Get2(i);
            ref var projectileTransform = ref _projectileFilter.Get3(i);

            float minAngle = maxAngle;
            bool targetInSight = false;
            foreach (var j in _enemiesFilter)
            {
                ref var enemyEntity = ref _enemiesFilter.GetEntity(j);
                ref var enemyTransform = ref _enemiesFilter.Get2(j);
                //if (enemyTransform.Transform == null)
                //{
                //    Debug.Log("EnemyTransform is null");
                //    Debug.Log("IS entity alive = " + enemyEntity.IsAlive());
                //}
                //if (projectileTransform.Transform == null)
                //    Debug.Log("ProjectileTransform is null");
                Vector2 distanceToEnemy = enemyTransform.Transform.position - projectileTransform.Transform.position;
                float angle = Vector2.SignedAngle(moveForward.Direction, distanceToEnemy);
                if (Mathf.Abs(angle) < Mathf.Abs(minAngle))
                {
                    targetInSight = true;
                    minAngle = angle;
                }
            }
            if (!targetInSight) continue;
            minAngle = Mathf.Sign(minAngle) * Mathf.Min(Mathf.Abs(minAngle), rotateSpeed * Time.deltaTime);
                projectileTransform.Transform.Rotate(Vector3.forward, minAngle, Space.World);

            moveForward.Direction = projectileTransform.Transform.up;
        }
    }
}
