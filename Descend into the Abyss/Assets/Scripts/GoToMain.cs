using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMain : MonoBehaviour
{
    private void Start() => SceneManager.LoadScene("Main");
}
