using System;

namespace DefaultNamespace.OverworldMap
{
    public enum SceneName
    {
        OverworldMap,
        ButtonMashing,
        PiratesVsAristocrats,
        Combat,
        TitleScreen,
        HubShip,
        WheelMinigame,
        WoodchopMinigame,
        Shop,
        IsleOfTorrent,
        RustysRetreat,
        Jungle,
        CombatNight,
        CombatIsleOfTorrent,
        CombatRustys,
        CombatJungle,
        ShipCombat,
        Night,
        NoScene
    }
    
    public static class Scenes
    {
        public static String GetSceneString(this SceneName sceneName)
        {
            switch (sceneName)
            {
                case SceneName.OverworldMap:
                    return "OverworldMap";
                case SceneName.ButtonMashing:
                    return "ButtonMashing";
                case SceneName.PiratesVsAristocrats:
                    return "PiratesVsAristocrats";
                case SceneName.Combat:
                    return "Combat2";
                case SceneName.TitleScreen:
                    return "TitleScreen";
                case SceneName.HubShip:
                    return "HubShip";
                case SceneName.WheelMinigame:
                    return "WheelMiniGame";
                case SceneName.WoodchopMinigame:
                    return "WoodchopMinigame";
                case SceneName.Shop:
                    return "Shop";
                case SceneName.IsleOfTorrent:
                    return "Isle of Torrent";
                case SceneName.RustysRetreat:
                    return "Rusty's Retreat";
                case SceneName.Jungle:
                    return "Jungle";
                case SceneName.CombatNight:
                    return "NEW_COMBAT_NIGHT";
                case SceneName.CombatIsleOfTorrent:
                    return "COMBAT_IsleofTorrent";
                case SceneName.CombatRustys:
                    return "COMBAT_Rustys";
                case SceneName.CombatJungle:
                    return "COMBAT_JUNGLE";
                case SceneName.ShipCombat:
                    return "sea combat demo";
                case SceneName.Night:
                    return "NightScene";
                default:
                    return "Error";
            }
        }

        public static bool IsSceneCombat(this SceneName sceneName)
        {
            return sceneName == SceneName.CombatNight || sceneName == SceneName.CombatRustys ||
                   sceneName == SceneName.CombatIsleOfTorrent || sceneName == SceneName.CombatJungle;
        }
    }
}