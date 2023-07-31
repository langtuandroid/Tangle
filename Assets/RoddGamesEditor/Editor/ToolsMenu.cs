using UnityEditor;
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;

namespace RoddGames.Editor
{
    public static class ToolsMenu
    {
        [MenuItem("Tools/Rodd Games/Create Folders/Create Default Folders")]
        public static void CreateDefaultFolders()
        {
            Dir("_GameFolders", "Arts", "Prefabs", "Scripts");
            Dir("_GameFolders/Scripts", "Concretes", "Abstracts");
            Dir("_GameFolders/Arts", "Models", "Animations", "Sprites");
            Dir("_AssetFolders", "UnityUrpFolders");
            Dir("_Scenes");
            Refresh();
        }

        #region Asset Store

        [MenuItem("Tools/Rodd Games/Assets/Asset Store/Odin")]
        public static void DownloadOdinAssetFile()
        {
            OpenURL(
                @"https://github.com/algebratech/project-creator-template/raw/503be0ed68ff755b53fb24fb2b33acf94184375b/Rodd-Games-Assets/StoreAssets/Odin%202020.3%20and%20Upper.unitypackage");
        }

        [MenuItem("Tools/Rodd Games/Assets/Asset Store/Console Pro")]
        public static void DownloadConsoleProAssetFile()
        {
            OpenURL(
                @"https://github.com/algebratech/project-creator-template/raw/503be0ed68ff755b53fb24fb2b33acf94184375b/Rodd-Games-Assets/StoreAssets/Console%20Pro%202020.3%20and%20Upper.unitypackage");
        }

        [MenuItem("Tools/Rodd Games/Assets/Asset Store/Rainbow 2020.3")]
        public static void DownloadRainbow2020AssetFile()
        {
            OpenURL(
                @"https://github.com/algebratech/project-creator-template/raw/503be0ed68ff755b53fb24fb2b33acf94184375b/Rodd-Games-Assets/StoreAssets/Rainbow%20Asset%202020.3.unitypackage");
        }

        [MenuItem("Tools/Rodd Games/Assets/Asset Store/Rainbow 2021.3 and Upper")]
        public static void DownloadRainbow2021AndUpperAssetFile()
        {
            OpenURL(
                @"https://github.com/algebratech/project-creator-template/raw/503be0ed68ff755b53fb24fb2b33acf94184375b/Rodd-Games-Assets/StoreAssets/Rainbow%20Asset%202021.3%20and%20Upper.unitypackage");
        }

        [MenuItem("Tools/Rodd Games/Assets/Asset Store/A* Pathfinding Pro")]
        public static void DownloadAStarPathfindingPro()
        {
            OpenURL(
                @"https://github.com/algebratech/project-creator-template/raw/503be0ed68ff755b53fb24fb2b33acf94184375b/Rodd-Games-Assets/StoreAssets/AStarPathfinding%20Pro%202020.3%20and%20Upper.unitypackage");
        }

        [MenuItem("Tools/Rodd Games/Assets/Asset Store/DOToween Pro")]
        public static void DownloadDoToWeenPro()
        {
            OpenURL(
                @"https://github.com/algebratech/project-creator-template/raw/503be0ed68ff755b53fb24fb2b33acf94184375b/Rodd-Games-Assets/StoreAssets/DoToweenPro.unitypackage");
        }

        [MenuItem("Tools/Rodd Games/Assets/Asset Store/NSubstitute Tdd")]
        public static void DownloadNSubstituteTdd()
        {
            OpenURL(
                @"https://github.com/algebratech/project-creator-template/raw/503be0ed68ff755b53fb24fb2b33acf94184375b/Rodd-Games-Assets/StoreAssets/NSubstite.unitypackage");
        }

        #endregion

        #region Custom Assets

        [MenuItem("Tools/Rodd Games/Assets/Custom Assets/Game Events")]
        public static void DownloadGameEvent()
        {
            OpenURL(
                @"https://github.com/algebratech/project-creator-template/raw/main/Rodd-Games-Assets/CustomAssets/RoddGamesEvent.unitypackage");
        }
        
        [MenuItem("Tools/Rodd Games/Assets/Custom Assets/Singleton")]
        public static void DownloadSingleton()
        {
            OpenURL(
                @"https://github.com/algebratech/project-creator-template/raw/main/Rodd-Games-Assets/CustomAssets/Singleton.unitypackage");
        }
        
        [MenuItem("Tools/Rodd Games/Assets/Custom Assets/Generic Pool")]
        public static void DownloadGenericPool()
        {
            OpenURL(
                @"https://github.com/algebratech/project-creator-template/raw/main/Rodd-Games-Assets/CustomAssets/GenericPool.unitypackage");
        }
        
        [MenuItem("Tools/Rodd Games/Assets/Custom Assets/Fps Counter")]
        public static void DownloadFpsCounter()
        {
            OpenURL(
                @"https://github.com/algebratech/project-creator-template/raw/main/Rodd-Games-Assets/CustomAssets/Fps%20Counter.unitypackage");
        }
        
        [MenuItem("Tools/Rodd Games/Assets/Custom Assets/Local Save Load")]
        public static void DownloadLocalSaveLoad()
        {
            OpenURL(
                @"https://github.com/algebratech/project-creator-template/raw/main/Rodd-Games-Assets/CustomAssets/RoddLocalSaveLoad.unitypackage");
        }
        
        [MenuItem("Tools/Rodd Games/Assets/Custom Assets/Base Button")]
        public static void DownloadBaseButton()
        {
            OpenURL(
                @"https://github.com/algebratech/project-creator-template/raw/main/Rodd-Games-Assets/CustomAssets/Rodd%20BaseButton.unitypackage");
        }
        
        
        [MenuItem("Tools/Rodd Games/Assets/Custom Assets/Mono Extension Methods")]
        public static void DownloadMonoExtensionMethods()
        {
            OpenURL(
                @"https://github.com/algebratech/project-creator-template/raw/main/Rodd-Games-Assets/CustomAssets/MonoExtensionMethods.unitypackage");
        }
        
        [MenuItem("Tools/Rodd Games/Assets/Custom Assets/Vector Helper")]
        public static void DownloadMonoVectorHelper()
        {
            OpenURL(
                @"https://github.com/algebratech/project-creator-template/raw/main/Rodd-Games-Assets/CustomAssets/VectorHelper.unitypackage");
        }
        
        #endregion

        #region Google SDk

        [MenuItem("Tools/Rodd Games/Assets/Google Assets/Firebase SDK")]
        public static void DownloadFirebaseSDK()
        {
            OpenURL(
                @"https://firebase.google.com/docs/unity/setup#available-libraries");
        }
        
        [MenuItem("Tools/Rodd Games/Assets/Google Assets/Google Play SDK")]
        public static void DownloadGooglePlayService()
        {
            OpenURL(
                @"https://github.com/algebratech/project-creator-template/raw/503be0ed68ff755b53fb24fb2b33acf94184375b/Rodd-Games-Assets/GoogleAssets/GooglePlayGamesPlugin-0.10.14.unitypackage");
        }

        #endregion

        static void Dir(string root, params string[] dir)
        {
            var fullPath = Combine(dataPath, root);
            foreach (var newDirectory in dir)
            {
                CreateDirectory(Combine(fullPath, newDirectory));
            }
        }

        static void Dir(string root)
        {
            var fullPath = Combine(dataPath, root);
            CreateDirectory(fullPath);
        }
    }
}