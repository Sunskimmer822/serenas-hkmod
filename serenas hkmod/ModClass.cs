using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;
using SFCore;
using ItemChanger;
using ItemChanger.Modules;
using ItemChanger.Locations;
using ItemChanger.Items;
using ItemChanger.Tags;
using ItemChanger.Placements;
using ItemChanger.UIDefs;

namespace serenas_hkmod
{
    public class Serenas_hkmod : Mod
    {
        private static List<Charm> Charms = new()
        {
            WyrmsGreed.instance
        };

        internal static Serenas_hkmod Instance;

        private Dictionary<(string, string), Action<PlayMakerFSM>> FSMEdits = new();

        new public string GetName() => "Serenas HKmod";
        public override string GetVersion() => "1";
        public override void Initialize()


        {
            ModHooks.HeroUpdateHook += OnHeroUpdate;
            ModHooks.OnEnableEnemyHook += ModHooks_OnEnableEnemyHook;
            foreach (var charm in Charms)
            {
                foreach (var edit in charm.FsmEdits)
                {
                    AddFsmEdit(edit.obj, edit.fsm, edit.edit);
                }
                Tickers.AddRange(charm.Tickers);
                var item = new ItemChanger.Items.CharmItem()
                {
                    charmNum = charm.Num,
                    name = charm.Name.Replace(" ", "_"),
                    UIDef = new MsgUIDef()
                    {
                        name = new LanguageString("UI", $"CHARM_NAME_{charm.Num}"),
                        shopDesc = new LanguageString("UI", $"CHARM_DESC_{charm.Num}"),
                        sprite = new EmbeddedSprite() { key = charm.Sprite }
                    }
                };
            }
        }

        private bool ModHooks_OnEnableEnemyHook(GameObject enemy, bool isAlreadyDead)
        {
            Log($"Found : {enemy.name}, enemy is {(isAlreadyDead ? "dead" : "alive")}");
            return isAlreadyDead;
        }

        public void OnHeroUpdate()
        {
            if (PlayerData.instance.equippedCharm_36 & PlayerData.instance.gotShadeCharm == false)
            {
                if (Input.GetKeyDown(KeyCode.O) | Input.GetKey(KeyCode.O))
                {
                    Log("Player seems to have kingsoul charm");
                    HeroController.instance.AddGeo(69);
                }
            } else if (Input.GetKeyDown(KeyCode.O))
            {
                
                HeroController.instance.AddGeo(69);
                Log("Nice");
            }
        }
    }
}