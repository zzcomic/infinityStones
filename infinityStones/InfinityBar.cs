using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace infinityStones
{
    class InfinityBar : MonoBehaviour
    {
        private Canvas canvas;
        private List<InfinityStoneIcon> stoneIcons;

        private static string[] stones =
        {
            "Space Stone",
            "Time Stone",
            "Mind Stone",
            "Power Stone",
            "Reality Stone",
            "Soul Stone"
        };

        private static Color[] stoneColors =
        {
            new Color(.2f, .4f, 1f, 1), //Space
            new Color(0f, .69f, .18f, 1),//Time
            new Color(.96f, .95f, .18f, 1),//Mind
            new Color(.75f, .32f, 1, 1),//Power
            new Color(1, .18f, .18f, 1),//Reality
            new Color(1, .5f, 0f, 1),//Soul

        };

        private InfinityBar() { }

        public static InfinityBar getOrAddInfinitybar(Player player)
        {
            InfinityBar existingIBar = player.gameObject.GetComponentInChildren<InfinityBar>();

            if (existingIBar != null) return existingIBar;

            Canvas[] allChildrenRecursive = player.gameObject.GetComponentsInChildren<Canvas>();
            GameObject canvasObj = allChildrenRecursive.Where(obj => obj.gameObject.name == "Canvas" && obj.transform.parent.gameObject.name == "Healthbar").FirstOrDefault().gameObject;

            GameObject IBarObj = new GameObject("InfinityBar");
            InfinityBar IBar = IBarObj.gameObject.AddComponent<InfinityBar>();

            IBarObj.transform.parent = canvasObj.transform;

            UpdateCrownPos(player);

            return IBar;
        }

        private static void UpdateCrownPos(Player player)
        {
            CanvasRenderer[] allObjs = player.gameObject.GetComponentsInChildren<CanvasRenderer>();
            foreach(CanvasRenderer renderer in allObjs)
            {
                if(renderer.gameObject.name == "CrownPos")
                {
                    renderer.gameObject.transform.localPosition = new Vector3(0, 210, 0);
                    break;
                }
            }
        }

        private void Awake()
        {
            stoneIcons = new List<InfinityStoneIcon>();

            canvas = GetComponent<Canvas>();

            int index = 0;
            UnityEngine.Debug.Log("Test");

            foreach (string stone in stones)
            {
                GameObject stoneObj = new GameObject(stone);
                InfinityStoneIcon newstone = stoneObj.AddComponent<InfinityStoneIcon>();
                stoneObj.transform.parent = this.transform;
                newstone.setColor(stoneColors[index]);
                newstone.name = stone;
                newstone.setPos(index);

                stoneIcons.Add(newstone);
                index++;
            }
        }

        private void Start()
        {
            this.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        public void updateStones(Player player)
        {
            if (stoneIcons == null) return;
            foreach (InfinityStoneIcon stoneIcon in stoneIcons)
            {
                if (player.data.currentCards.Any(c => c.name == stoneIcon.name))
                {
                    stoneIcon.setAlpha(1);
                }
                else stoneIcon.setAlpha(.05f);
            }
        }
    }
}
