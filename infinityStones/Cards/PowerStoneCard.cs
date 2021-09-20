using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnboundLib.Cards;
using UnityEngine;

namespace infinityStones.Cards
{
    class PowerStoneCard : InfinityStone
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            addInfCard(player);

            gun.damage *= 2;
        }

        public override void OnRemoveCard()
        {
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return "Dread it, run from it, destiny arrives all the same.";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Damage",
                    amount = "Double",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }

        protected override string GetTitle()
        {
            return "Power Stone";
        }

        public override string GetModName()
        {
            return "ZZcomic";
        }
    }
}
