using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using UnityEngine;
using System.Reflection;

namespace infinityStones
{
    class InfinityDeathTimer : MonoBehaviour
    {
        public int numStones = 0;

        private Player player;
        private bool killedPlayer = false;

        private int SECONDS_TIL_DEATH = 30;
        private int SECONDS_ADDED_PER_CARD = 4;

        private DateTime startTime;

        private void Start()
        {
            player = this.gameObject.GetComponentInParent<Player>();
            startTime = DateTime.MaxValue;
        }

        private void Update()
        {
            if (!killedPlayer && startTime < DateTime.Now.AddSeconds(-1 * (SECONDS_TIL_DEATH - (numStones * SECONDS_ADDED_PER_CARD))))
            {
                killSelf();
                killedPlayer = true;
            }
        }

        private void killSelf()
        {
            if (killedPlayer) return;
            player.data.view.RPC("RPCA_Die", RpcTarget.All, new object[]
                {
                    new Vector2(0, 1f)
                }) ;
        }
        public void resetTimer()
        {
            startTime = DateTime.Now;
            killedPlayer = false;
        }

        public void setTimer()
        {
            SECONDS_TIL_DEATH -= 4;
        }
    }
}
