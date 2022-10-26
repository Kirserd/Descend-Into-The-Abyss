using UnityEngine;

public class ElementLink : MonoBehaviour
{
    public string Link { get; private set; }
    public void SetRefferalLinkAndGoTo()
    {
        GameObject.FindGameObjectWithTag("Foreground").GetComponent<ElementLink>().SetLinkTo(Link);
        GoToLink();
    }
    private void GoToLink()
    {
        Debug.Log(Link);
        GameObject.FindGameObjectWithTag("Content").GetComponent<PageShifter>().GoToDatabasePage();
    }
    public void SetLinkTo(string Link) => this.Link = Link;
}
