﻿using System.Reflection;
using LyokoAPI.API;
using LyokoAPI.VirtualStructures;
using LyokoAPI.VirtualStructures.Interfaces;

namespace LyokoAPI.Events
{
    public class VirtualWorldDestructionEvent
    {
        private static event Events.OnVirtualWorldEvent VirtualWorldE;
        public static void Call(IVirtualWorld world)
        {
            if (IsLocked && !Assembly.GetCallingAssembly().Equals(Events.Master))
            {
                return;
            }
            VirtualWorldE?.Invoke(world);
        }

        public static void Call(string world)
        {
            if (IsLocked && !Assembly.GetCallingAssembly().Equals(Events.Master))
            {
                return;
            }
            APIVirtualWorld _world = new APIVirtualWorld(world);
            Call(_world);
        }

        internal static Events.OnVirtualWorldEvent Subscribe(Events.OnVirtualWorldEvent func)
        {
            VirtualWorldE += func;
            return func;
        }

        internal static void Unsubscribe(Events.OnVirtualWorldEvent func)
        {
            VirtualWorldE -= func;
        }

        #region locking
        private static bool _isLocked;
        public static bool IsLocked
        {
            get => _isLocked || Events.AllLocked;
            internal set => _isLocked = value;
        }
        /*
         * Returns true if the lock was successful. 
         */
        public static bool Lock()
        {
            if (Events.hasMaster && !Assembly.GetCallingAssembly().Equals(Events.Master))
            {
                return false;
            }

            IsLocked = true;
            return IsLocked;
        }
        /*
         * Returns true if the unlock was successful
         */
        public static bool UnLock()
        {
            if (Events.hasMaster && !Assembly.GetCallingAssembly().Equals(Events.Master))
            {
                return false;
            }
            IsLocked = false;
            return !IsLocked;
        }
        #endregion
    }
}
