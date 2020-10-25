using UnityEditor;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Editor
{
    public static class Create2D 
    {
        [MenuItem("Create2D/Create Physic Material 2D")]
        private static void CreatePhysicMaterial2D()
        {
            PhysicsMaterial2D material2D = new PhysicsMaterial2D();
            AssetDatabase.CreateAsset(material2D, "Assets/Media/PhysicMaterials/New PhysicMaterial2D.asset");
            AssetDatabase.Refresh();
        }
    }
}
