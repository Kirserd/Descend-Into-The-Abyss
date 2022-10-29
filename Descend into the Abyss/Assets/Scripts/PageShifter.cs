using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PageShifter : MonoBehaviour
{
    public void GoToOverview()
    {
        SceneManager.LoadScene("DatabaseOverview");
    }
    public void GoToDatabasePage()
    {
        StartCoroutine(ReferralLink());
    }
    private IEnumerator ReferralLink()
    {
        yield return new WaitForSeconds(0.1f);
        string Link = GameObject.FindGameObjectWithTag("Foreground").GetComponent<ElementLink>().Link;
        if (GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DataTransfer>().CurrentDataType == DataType.PLAYABLE_CHARACTER)
            SceneManager.LoadScene("DatabaseSheet");
        else
            SceneManager.LoadScene("DatabasePage");
        GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DataTransfer>().CurrentElement = Link;
        yield break;
    }
    public void GoToRules()
    {
        SceneManager.LoadScene("Rules");
    }
    public void GoToContentCreator()
    {
        SceneManager.LoadScene("ContentCreator");
    }
    public void GoToRegisterCharacter()
    {
        SceneManager.LoadScene("RegisterCharacter");
    }
    public void GoToMain()
    {
        SceneManager.LoadScene("Main");
    }
}
