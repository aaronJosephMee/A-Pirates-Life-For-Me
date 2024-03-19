﻿using System;

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
                    return "CombatTest";
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
                default:
                    return "Error";
            }
        }
    }
}