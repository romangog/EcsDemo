using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Voody.UniLeo;

public class EcsButtonCLick : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private ConvertToEntity _convert;

    private void Start()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _convert.TryGetEntity().Value.Get<ButtonClickedTag>();
    }
}
