using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Video;

namespace infinityStones
{

    [HarmonyPatch(typeof(MapManager), "CallInNewMapAndMovePlayers")]
    class infinityGauntletPatches
    {
        public static void Postfix()
        {
            List<Player> players = PlayerManager.instance.players;
            foreach(Player player in players)
            {
                InfinityDeathTimer timer = player.gameObject.GetComponent<InfinityDeathTimer>();
                if (timer != null) timer.resetTimer();

                InfinityBar IBar = player.gameObject.GetComponentInChildren<InfinityBar>();
                if (IBar != null) IBar.updateStones(player);
            }
        }
    }

/*    [HarmonyPatch(typeof(CardChoice), "IDoEndPick")]//DisplayScreenText")]
    class GameOverPatch
    {
        public static void Prefix(UIHandler __instance)
        {
            UnityEngine.Debug.Log("Test");
            Transform[] parentTransforms = __instance.gameObject.GetComponentsInParent<Transform>();
            UnityEngine.Debug.Log("Test2");

            GameObject game = parentTransforms.Where(g => g.gameObject.name == "Game").FirstOrDefault().gameObject;
            UnityEngine.Debug.Log("Test3");

            Camera[] cameras = game.GetComponentsInChildren<Camera>();
            UnityEngine.Debug.Log("Test4");

            Camera MainCamera = cameras.Where(c => c.gameObject.name == "MainCamera").FirstOrDefault();
            UnityEngine.Debug.Log("Test5");

            GameObject thanosClip = InfinityGauntletPlugin.infinityBundle.LoadAsset<GameObject>("ThanosSnapClip");
            UnityEngine.Debug.Log("Test6");

            thanosClip.transform.parent = MainCamera.transform;
            UnityEngine.Debug.Log(thanosClip.GetComponent<VideoPlayer>());
            thanosClip.GetComponent<VideoPlayer>().enabled = true;
            Thread.Sleep(2000);
            thanosClip.GetComponent<VideoPlayer>().Play();
        }
    }*/
}
