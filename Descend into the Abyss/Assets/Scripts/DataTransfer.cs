using UnityEngine;

public class DataTransfer : MonoBehaviour
{
    public DataType CurrentDataType;
    public string CurrentElement;
    public string LoadedImagePath;

    private void Awake() 
    {
        CurrentDataType = DataType.ALL;
        DontDestroyOnLoad(gameObject);
    }  
}
