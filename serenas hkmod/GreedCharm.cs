using Modding;
using System;

namespace serenas_hkmod
{
    internal class WyrmsGreed : Charm
    {
        public static readonly Charm Instance = new WyrmsGreed();

        private WyrmsGreed() {}

        public override string Sprite => "Wyrm'sGreed.png";
        public override string Name => "Wyrm's Greed";
        public override string Description => "Some Wyrms found geo irresistble, and found ways to purify their geo to make it more valuable,\n\nWhen gaining geo, you have a chance to gain extra geo.";
        public override int DefaultCost => 3;
        public override string Scene => "Fungus2_04";
        public override float X => 31.9f;
        public override float Y => 12.5f;
        public override CharmSettings Settings(SaveSettings s) => s.WyrmsGreed;


        public override void Hook()
        {
            ModHooks.SetPlayerIntHook += SetInt;
        }
            int SetInt(string name, int orig)
            {
                if (name == "geo")
                {
                    if (PlayerData.instance.geo > orig)
                    {
                       if (new Random().Next(0,2) > 1)
                        {
                            int GeoDiff = PlayerData.instance.geo - orig;
                            int GeoToAdd = 4 * GeoDiff;
                            PlayerData.instance.AddGeo(GeoToAdd);
                        }
                    }
                }
                return orig;
            }
        


    }
}