using LyokoAPI.RealWorld.Location;
using LyokoAPI.RealWorld.Location.Abstract;
using LyokoAPI.VirtualStructures.Interfaces;

namespace LyokoAPI.VirtualEntities.LyokoWarrior
{
    public class LyokoWarrior
    {
        public static readonly int MAX_HP = 100;
        public LyokoWarriorName WarriorName { get; internal set; }
        public GenericLocation Location { get; private set; }
        public Status Status { get; private set; }
        public int HP { get; private set; }
        public bool Moving { get; private set; }

        internal LyokoWarrior(LyokoWarriorName warrior)
        {
            WarriorName = warrior;
            Location = APILocations.KADIC;
            Status = Status.EARTH;
            HP = MAX_HP;
        }

        internal int Hurt(int damage)
        {
            if ((HP - damage) < 0 )
            {
                HP -= 0;
            }
            else
            {
                HP -= damage;
            }

            return HP;
        }
        
        internal int Heal(int ammount)
        {
            if ((HP + ammount) > MAX_HP)
            {
                HP = MAX_HP;
            }
            else
            {
                HP += ammount;
            }

            return HP;
        }

        internal int ResetHealth()
        {
            return Heal(MAX_HP);
        }

        internal LyokoWarrior Virtualize(ISector destination)
        {
            Move(destination);
            ResetHealth();
            Status = Status.VIRTUALIZED;
            return this;
        }

        internal LyokoWarrior Frontier()
        {
            Location = APILocations.FRONTIER;
            Status = Status.FRONTIERED;
            return this;
        }

        internal LyokoWarrior CodeEarth(ILocation<APILocation> location)
        {
            Move(location);
            Status = Status.EARTH;
            ResetHealth();
            return this;
        }

        internal LyokoWarrior CodeEarth()
        {
            return CodeEarth(APILocations.SCANNERS);
        }

        internal LyokoWarrior Kill()
        {
            Location = APILocations.DEAD;
            Status = Status.LOST;
            HP = 0;
            return this;
        }

        internal LyokoWarrior Xanafy()
        {
            Status = Status.XANAFIED;
            return this;
        }

        internal LyokoWarrior Dexanafy()
        {
            Location = APILocations.SCANNERS;
            Status = Status.EARTH;
            ResetHealth();
            return this;
        }

        internal LyokoWarrior Translate(ILocation<APILocation> location)
        {
            Move(location);
            Status = Status.TRANSLATED;
            return this;
        }

        internal LyokoWarrior Move(ILocation<APILocation> location)
        {
            //Since this, at least for now, only refers to moving in the real world
            //or a place they can't get out without help,
            //it does not require changing the Move variable
            Location = location.AsGenericLocation();
            return this;
        }

        internal LyokoWarrior Move(ILocation<ISector> location)
        {
            Moving = true;
            Location = location.AsGenericLocation();
            return this;
        }

        internal LyokoWarrior Arrive()
        {
            Moving = false;
            return this;
        }

    }
}