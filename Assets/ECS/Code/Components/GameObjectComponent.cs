using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct GameObjectComponent : IComponent {
    public GameObject gameObject;

    public void PositionGameObject(Vector3 position){
        gameObject.transform.position = position;
    }
}