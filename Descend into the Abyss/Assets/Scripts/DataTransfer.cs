using UnityEngine;

public class DataTransfer : MonoBehaviour
{
    public DataType CurrentDataType;
    public DatabaseElement CurrentElement;
    public string LoadedImagePath;

    private void Awake() => DontDestroyOnLoad(gameObject);  
}
