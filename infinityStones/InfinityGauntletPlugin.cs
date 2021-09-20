using BepInEx;
using System;
using HarmonyLib;
using UnboundLib.Cards;
using infinityStones.Cards;
using Jotunn.Utils;
using UnityEngine;
using Photon.Pun;

namespace infinityStones
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin("com.zzcomic.ROUNDS.INFINITYSTONES", "Infinity_Stones", "1.0.0")]
    [BepInProcess("Rounds.exe")]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    
    public class InfinityGauntletPlugin :BaseUnityPlugin
    {
        private void Awake()
        {
            var harmony = new Harmony("com.zzcomic.ROUNDS");
            harmony.PatchAll();
        }

        private void Start()
        {
            infinityBundle = AssetUtils.LoadAssetBundleFromResources("infinity_asset_bundle", typeof(InfinityGauntletPlugin).Assembly);

            CustomCard.BuildCard<TimeStoneCard>(InfinityStone.callback);
            CustomCard.BuildCard<PowerStoneCard>(InfinityStone.callback);
            CustomCard.BuildCard<MindStoneCard>(InfinityStone.callback);
            CustomCard.BuildCard<SoulStoneCard>(InfinityStone.callback);
            CustomCard.BuildCard<RealityStoneCard>(InfinityStone.callback);
            CustomCard.BuildCard<SpaceStoneCard>(InfinityStone.callback);
        }

        [PunRPC]
        public void RPCA_GameOver()
        {

        }

        internal static AssetBundle infinityBundle;
    }
}
