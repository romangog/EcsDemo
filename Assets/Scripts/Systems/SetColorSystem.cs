using UnityEngine;
using Leopotam.Ecs;

public class SetColorSystem : IEcsRunSystem, IEcsInitSystem
{
    EcsFilter<SetBaseColorRequest,SpriteRendererComponent> _spriteRenderersFilter;

    private MaterialPropertyBlock _propertyBlock;
    public void Init()
    {
        _propertyBlock = new MaterialPropertyBlock();
    }

    public void Run()
    {
        foreach (var i in _spriteRenderersFilter)
        {
            ref var setColorRequest = ref _spriteRenderersFilter.Get1(i);
            ref var spriteRenderer = ref _spriteRenderersFilter.Get2(i);
            spriteRenderer.SpriteRenderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor("_BaseColor", setColorRequest.BaseColor);
            spriteRenderer.SpriteRenderer.SetPropertyBlock(_propertyBlock);
        }
    }
}


