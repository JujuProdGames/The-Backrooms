using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scene Collection", menuName = "MultiScene/Scene Collection")]
public class SceneCollection : ScriptableObject
{
    [Header("Scenes to Load")]
    public List<SceneField> loadScenes = new List<SceneField>();

    [Header("Scenes to Unload")]
    public SceneCollection unloadScenes;

    [Header("Set Active Scene")]
    public SceneField setActiveScene;
}
