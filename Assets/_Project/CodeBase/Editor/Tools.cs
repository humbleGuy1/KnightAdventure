using UnityEditor;
using UnityEngine;

namespace Codebase.Editor
{
    public class Tools : MonoBehaviour
    {
        [MenuItem("Tools/Clear Prefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}

