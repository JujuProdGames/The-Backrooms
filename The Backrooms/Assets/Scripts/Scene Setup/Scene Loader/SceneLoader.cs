using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : SceneClass
{
    [SerializeField] private SceneCollection sceneCollection;

	private void Awake()
	{
		LoadScenes(sceneCollection);
	}
}
