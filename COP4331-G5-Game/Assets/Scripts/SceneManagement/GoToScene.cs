using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour {
	public bool transitionOnAwake = false;
	public string sceneName;

	void Awake() {
		if (transitionOnAwake) TransitionToScene();
	}

	public void TransitionToScene() {
		SceneManager.LoadScene(sceneName);
	}
}
