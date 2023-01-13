using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlayerUpgradeShop : MonoBehaviour
{
    [SerializeField] private Upgrade[] upgrades;
    private PlayerController player = null;
    public enum UpgradeType
    {
        speed,
        radius,
        dps
    }

    public void TryUpgradePlayer()
    {
        if (player == null)
        {
            player = GameObject.FindObjectOfType<PlayerController>();
            if (player == null)
                return;
        }
        var upgrade = GetRandomUpgradeType();
        switch (upgrade.upgradeType)
        {
            case (UpgradeType.dps):
                {
                    player.ChangeDPS(upgrade.increment);
                    break;
                }
            case (UpgradeType.radius):
                {
                    player.ChangeRadius(upgrade.increment);
                    break;
                }
            case (UpgradeType.speed):
                {
                    player.ChangeSpeed(upgrade.increment);
                    break;
                }
        }
    }

    private Upgrade GetRandomUpgradeType()
    {
        float totalChance = 0;
        foreach (var upgrade in upgrades)
        {
            totalChance += upgrade.chance;
        }
        float r = UnityEngine.Random.Range(0, totalChance);
        var closest = upgrades.Aggregate((x, y) => Math.Abs(x.chance - r) < Math.Abs(y.chance - r) ? x : y);
        return closest;
    }

    [Serializable]
    public class Upgrade
    {
        public UpgradeType upgradeType;
        public float chance;
        public float increment;
    }
}
