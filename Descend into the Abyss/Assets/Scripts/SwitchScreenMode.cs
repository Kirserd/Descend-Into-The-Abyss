using UnityEngine;

public class SwitchScreenMode : MonoBehaviour
{
    private int _resolutionX, _resolutionY;

    private void Start()
    {
        _resolutionX = Screen.width;
        _resolutionY = Screen.height;
    }
    public void SwitchMode()
    {
        if (Screen.fullScreenMode != FullScreenMode.Windowed)
            Screen.SetResolution(960, 540, FullScreenMode.Windowed);

        else
            Screen.SetResolution(_resolutionX,_resolutionY, FullScreenMode.ExclusiveFullScreen);
    } 
}
