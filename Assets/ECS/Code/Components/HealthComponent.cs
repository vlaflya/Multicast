using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct HealthComponent : IComponent
{
    public float maxHealth;
    public float healthPoints;
    public bool destroyOnDeath;
    public IHealthController healthController;

    public void DealDamage(float value)
    {
        healthPoints -= value;
        healthController.DealDamage(value);
    }

    public void ResetHealth()
    {
        healthPoints = maxHealth;
    }
}