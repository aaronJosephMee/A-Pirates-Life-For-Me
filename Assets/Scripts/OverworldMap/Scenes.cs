using System;

namespace DefaultNamespace.OverworldMap
{
    public enum SceneName
    {
        ButtonMashing,
        OverworldMap,
        PiratesVsAristocrats,
        Combat,
        TitleScreen,
        HubShip,
        NoScene
    }
    
    public static class Scenes
    {
        public static String GetSceneString(this SceneName sceneName)
        {
            switch (sceneName)
            {
                case SceneName.ButtonMashing:
                    return "ButtonMashing";
                case SceneName.OverworldMap:
                    return "OverworldMap";
                case SceneName.PiratesVsAristocrats:
                    return "PiratesVsAristocrats";
                case SceneName.Combat:
                    return "CombatTest";
                case SceneName.TitleScreen:
                    return "TitleScreen";
                case SceneName.HubShip:
                    return "HubShip";
                default:
                    return "Error";
            }
        }
    }
}