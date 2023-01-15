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
        var comparer = new ChanceComparer();
        Upgrade[] sortetUpgrades = upgrades.OrderBy(upgrade => upgrade, comparer).ToArray();
        SetRelativeChoices(sortetUpgrades);
        float currentChance = totalChance;
        Upgrade result = sortetUpgrades.Aggregate((x, y) => x.relativeChance > y.relativeChance ? x : y);
        foreach (var u in upgrades)
        {
            if (r < u.relativeChance && u.relativeChance < currentChance)
            {
                currentChance = u.relativeChance;
                result = u;
            }
        }
        return result;
    }

    private void SetRelativeChoices(Upgrade[] upgrades)
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            float relative = 0;
            for (int y = 0; y < i; y++)
            {
                relative += upgrades[y].chance;
            }
            upgrades[i].relativeChance = relative + upgrades[i].chance;
        }
    }

    private class ChanceComparer : IComparer<Upgrade>
    {
        public int Compare(Upgrade x, Upgrade y)
        {
            return Comparer<float>.Default.Compare(x.chance, y.chance);
        }
    }

    [Serializable]
    public class Upgrade
    {
        public UpgradeType upgradeType;
        public float chance;
        public float increment;
        [NonSerialized]
        public float relativeChance;
    }
}
