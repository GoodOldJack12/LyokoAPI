using System;
using LyokoAPI.API;
using LyokoAPI.VirtualStructures.Interfaces;

namespace LyokoAPI.Events
{
    public class TowerActivationEvent
    {

        private static event Events.OnTowerEvent TowerActivationE;
        
        public static void Call(ITower tower)
        {
            if (tower.Activated)
            {
                TowerActivationE?.Invoke(tower);
            }
        }

        public static Events.OnTowerEvent Subscribe(Events.OnTowerEvent func)
        {
            TowerActivationE += func;
            return func;
        }

        public static void UnSubscribe(Events.OnTowerEvent func)
        {
           TowerActivationE -= func;
        }
    }
}