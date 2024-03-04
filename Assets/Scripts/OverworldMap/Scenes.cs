using System;

namespace DefaultNamespace.OverworldMap
{
    public enum SceneName
    {
        ButtonMashing,
        OverworldMap,
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
                default:
                    return "Error";
            }
        }
    }
}