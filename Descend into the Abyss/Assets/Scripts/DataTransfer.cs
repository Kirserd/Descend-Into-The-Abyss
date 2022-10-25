using UnityEngine;

public class DataTransfer : MonoBehaviour
{
    public DataType CurrentDataType;
    public DatabaseElement CurrentElement;

    private void Awake() => DontDestroyOnLoad(gameObject);  
}
