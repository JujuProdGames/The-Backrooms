using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : SceneClass
{	
	public void PlayGame(SceneCollection sceneCollection)
	{
		LoadScenes(sceneCollection, sceneCollection.unloadScenes);
	}
}
