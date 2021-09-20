using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnboundLib.Cards;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace infinityStones.Cards
{
    abstract class InfinityStone : CustomCard
    {
        protected void addInfCard(Player player)
        {
            checkAutoWin(player, GetTitle());
            incrementDeathTimer(player);
            InfinityBar.getOrAddInfinitybar(player);
        }
        public void incrementDeathTimer(Player player)
        {
            InfinityDeathTimer idt;
            if (player.gameObject.GetComponent<InfinityDeathTimer>())
            {
                idt = player.gameObject.GetComponent<InfinityDeathTimer>();
            }
            else
                idt = player.gameObject.AddComponent<InfinityDeathTimer>() as InfinityDeathTimer;

            idt.numStones++;
        }

        public void checkAutoWin(Player player, String currentCard)
        {
            List<String> cards = player.data.currentCards.Select(c => c.cardName).ToList();
            cards.Add(currentCard);

            if(checkForCard("Space Stone", cards)
                && checkForCard("Power Stone", cards)
                && checkForCard("Reality Stone", cards)
                && checkForCard("Mind Stone", cards)
                && checkForCard("Soul Stone", cards)
                && checkForCard("Time Stone", cards))
            {
                MethodInfo dynMethod = GM_ArmsRace.instance.GetType().GetMethod("GameOver",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                dynMethod.Invoke(GM_ArmsRace.instance, new object[] { player.teamID });
            }
        }

        private bool checkForCard(String cardname, List<String> cardNames)
        {
            bool truth = cardNames.Contains(cardname);
            return truth;
        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.allowMultiple = false;
        }

        internal static void callback(CardInfo card)
        {
            GauntletVisualEffect effect = card.gameObject.AddComponent<GauntletVisualEffect>();
            effect.cardName = card.cardName;
        }

        private class GauntletVisualEffect : MonoBehaviour
        {
            public string cardName;

            private Tuple<string, string>[] stones = {
                Tuple.Create("Space Stone" , "BlueStone"),
                Tuple.Create("Time Stone", "GreenStone"),
                Tuple.Create("Mind Stone", "YellowStone"),
                Tuple.Create("Power Stone", "PinkStone"),
                Tuple.Create("Reality Stone", "RedStone"),
                Tuple.Create("Soul Stone", "OrangeStone")
                };

            private bool done = false;

            private void Start()
            {
                if (done) return;
                TextMeshProUGUI[] childrenTextMesh = this.gameObject.GetComponentsInChildren<TextMeshProUGUI>();
                TextMeshProUGUI modText = childrenTextMesh.Where(t => t.gameObject.name == "ModNameText").FirstOrDefault();
                if (modText != null) modText.fontSize = 50;

                Image[] allChildrenImages = this.gameObject.GetComponentsInChildren<Image>();
                Image cardArtImage = allChildrenImages.Where(obj => obj.gameObject.name == "Art").FirstOrDefault();


                GameObject cardArt =  InfinityGauntletPlugin.infinityBundle.LoadAsset<GameObject>("gauntlet");
                GameObject newcardArtImage = UnityEngine.Object.Instantiate<GameObject>(cardArt, cardArtImage.transform.transform.position, cardArtImage.transform.transform.rotation, cardArtImage.transform);
                newcardArtImage.transform.localPosition = Vector3.zero;
                newcardArtImage.transform.SetAsFirstSibling();
                newcardArtImage.transform.localScale = Vector3.one;

                newcardArtImage.transform.parent = cardArtImage.transform;

                MethodInfo dynMethod = PlayerManager.instance.GetType().GetMethod("GetPlayerWithID",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                Player player = (Player)dynMethod.Invoke(PlayerManager.instance, new object[] { CardChoice.instance.pickrID });

                foreach (Tuple<string,string> stone in stones)
                {
                    if (player.data.currentCards.Any(c => c.name == stone.Item1))
                    {
                        continue;
                    }
                    addStoneImage(newcardArtImage, stone.Item2);
                }
                done = true;
            }

            private void addStoneImage(GameObject cardArtImage, string stoneName)
            {
                Image[] childrenImages = cardArtImage.GetComponentsInChildren<Image>();

                Component[] components = cardArtImage.GetComponentsInChildren<Component>();
                foreach(Image c in childrenImages)
                {
                    if (c.gameObject.name == stoneName) c.enabled = false;
                }
/*
                Component stone = components.Where(obj => obj.gameObject.name == stoneName).FirstOrDefault();
                UnityEngine.Debug.Log(stone);
                GameObject obj = stone.gameObject;
                UnityEngine.Debug.Log(obj);

                Destroy(stone);*/
            }
        }
    }
}
