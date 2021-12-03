using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneClass : BaseClass
{
	public void LoadScenes(SceneCollection loadScenes, SceneCollection unloadScenes = null)
	{
		foreach (SceneField scene in loadScenes.loadScenes)
		{
			SceneManager.LoadScene(scene.SceneName, LoadSceneMode.Additive);
		}

		StartCoroutine(SetActiveScene(loadScenes));
		
		if (unloadScenes != null)
		{
			StartCoroutine(UnloadScenes(unloadScenes));
		}
	}

	private IEnumerator SetActiveScene(SceneCollection loadScenes)
	{
		string loadSceneName = loadScenes.setActiveScene.SceneName;
		Debug.Log("Current active scene " + SceneManager.GetActiveScene().name);

		//yield return new WaitUntil(() => SceneManager.GetSceneByName(loadSceneName).isLoaded == true);
		yield return new WaitForSecondsRealtime(.01f);

		SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadSceneName));
		Debug.Log("New active scene " + SceneManager.GetActiveScene().name);
	}

	private IEnumerator UnloadScenes(SceneCollection unloadScenes)
	{
		yield return new WaitForSecondsRealtime(.02f);

		foreach (SceneField scene in unloadScenes.loadScenes)
		{
			SceneManager.UnloadSceneAsync(scene.SceneName);
		}
	}
}
