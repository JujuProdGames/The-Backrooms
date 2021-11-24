using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scene Collection", menuName = "Scene/Scene Collection")]
public class SceneCollection : ScriptableObject
{
    [Header("Scenes to Load")]
    public List<SceneField> loadScenes = new List<SceneField>();

    [Header("Set Active Scene")]
    public SceneField setActiveScene;
}
