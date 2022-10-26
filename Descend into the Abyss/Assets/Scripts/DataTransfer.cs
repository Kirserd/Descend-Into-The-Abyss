using UnityEngine;

public class DataTransfer : MonoBehaviour
{
    public DataType CurrentDataType;
    public string CurrentElement;
    public string LoadedImagePath;

    private void Awake() => DontDestroyOnLoad(gameObject);  
}
