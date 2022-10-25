using UnityEngine;

public class URLOpener : MonoBehaviour
{  
    public void OpenURL(string URL)
    {
        Application.OpenURL(URL);
    }
}
