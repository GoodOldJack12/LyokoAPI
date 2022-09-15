using LyokoAPI.VirtualEntities.LyokoWarrior;

namespace LyokoAPI.Events.LWEvents
{
    public class LW_CodeEarthResolvedEvent
    {
        public static Events.OnLyokoWEvent LwE;

        public static void Call(LyokoWarrior warrior)
        {
            warrior.GiveEarthCode();
            LwE?.Invoke(warrior);
        }

        internal static Events.OnLyokoWEvent Subscribe(Events.OnLyokoWEvent func)
        {

            LwE += func;
            return func;
        }

        internal static void Unsubscribe(Events.OnLyokoWEvent func)
        {
            LwE -= func;
        }
    }
}