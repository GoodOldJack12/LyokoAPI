using System;
using System.Collections;
using System.ComponentModel;
using LyokoAPI.VirtualStructures;

namespace LyokoAPI.API
{
    public static class LyokoParser
    {
        #region ActivatorParsers
        public static APIActivator ParseActivator(string activatorstring)
        {
            switch (activatorstring.ToUpper())
            {
                    case "XANA": return APIActivator.XANA;
                    case "JEREMIE": return APIActivator.JEREMIE;
                    case "HOPPER" : return APIActivator.HOPPER;
                    case "NONE": return APIActivator.NONE;
            }

            throw new NullReferenceException("Invalid activator: "+ activatorstring);
            
        }

        public static bool TryParseActivator(string activatorstring, out APIActivator activator)
        {
            try
            {
                activator = ParseActivator(activatorstring);
                return true;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
                activator = APIActivator.NONE;
                return false;
            }
        }
        #endregion 

        public static APIActivator Parse(this APIActivator activator,string activatorstring)
        {
            return ParseActivator(activatorstring);
        }

        public static bool TryParse(this Enum activator, string activatorstring, out APIActivator activatorout)
        {
            return TryParseActivator(activatorstring, out activatorout);
           
        }
       
    }
}