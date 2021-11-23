using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : BaseClass
{
    [SerializeField] private SceneCollection sceneCollection;

	private void Awake()
	{
		LoadLevels();
	}

	private void Start()
	{
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneCollection.setActiveScene.SceneName));
	}

	private void LoadLevels()
    {		
		foreach(SceneField scene in sceneCollection.loadScenes)
		{
			SceneManager.LoadScene(scene.SceneName, LoadSceneMode.Additive);
		}		
	}
}
