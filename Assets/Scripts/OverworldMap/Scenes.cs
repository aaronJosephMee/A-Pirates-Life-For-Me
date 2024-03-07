using System;

namespace DefaultNamespace.OverworldMap
{
    public enum SceneName
    {
        ButtonMashing,
        OverworldMap,
        PiratesVsAristocrats,
        Combat,
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
                default:
                    return "Error";
            }
        }

        public static SceneName GetSceneName(String sceneString)
        {
            if (sceneString == "ButtonMashing")
            {
                return SceneName.ButtonMashing;
            }
            else if (sceneString == "OverworldMap")
            {
                return SceneName.OverworldMap;
            }
            else if (sceneString == "PiratesVsAristocrats")
            {
                return SceneName.PiratesVsAristocrats;
            }
            else if (sceneString == "CombatTest")
            {
                return SceneName.Combat;
            }
            else
            {
                return SceneName.NoScene;
            }
        }
    }
}