using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PageShifter : MonoBehaviour
{
    public void GoToArtifacts()
    {
        SceneManager.LoadScene("DatabaseOverview", LoadSceneMode.Single);
        GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DataTransfer>().CurrentDataType = DataType.ARTIFACT;
    }
    public void GoToCharacters()
    {
        SceneManager.LoadScene("DatabaseOverview");
        GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DataTransfer>().CurrentDataType = DataType.CHARACTER;
    }
    public void GoToLocations()
    {
        SceneManager.LoadScene("DatabaseOverview");
        GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DataTransfer>().CurrentDataType = DataType.LOCATION;
    }
    public void GoToCampaigns()
    {
        SceneManager.LoadScene("DatabaseOverview");
        GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DataTransfer>().CurrentDataType = DataType.CAMPAIGN;
    }
    public void GoToMusicLibrary()
    {
        SceneManager.LoadScene("DatabaseOverview");
        GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DataTransfer>().CurrentDataType = DataType.MUSIC;
    }
    public void GoToDatabasePage()
    {
        StartCoroutine(ReferralLink());
    }
    private IEnumerator ReferralLink()
    {
        yield return new WaitForFixedUpdate();
        DatabaseElement Link = GameObject.FindGameObjectWithTag("Foreground").GetComponent<ElementLink>().Link;
        SceneManager.LoadScene("DatabasePage");
        GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DataTransfer>().CurrentElement = Link;
        yield break;
    }
    public void GoToDatabase()
    {
        SceneManager.LoadScene("DatabaseChoose");
    }
    public void GoToRules()
    {
        SceneManager.LoadScene("Rules");
    }
    public void GoToContentCreator()
    {
        SceneManager.LoadScene("ContentCreator");
    }
    public void GoToMain()
    {
        SceneManager.LoadScene("Main");
    }
}
