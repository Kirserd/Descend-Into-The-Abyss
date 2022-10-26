using UnityEngine;

public class ElementLink : MonoBehaviour
{
    public string Link { get; private set; }
    public void SetRefferalLinkAndGoTo()
    {
        if (Link == null)
            return;
        GameObject.FindGameObjectWithTag("Foreground").GetComponent<ElementLink>().SetLinkTo(Link);
        GoToLink();
    }
    private void GoToLink()
    {
        GameObject.FindGameObjectWithTag("Content").GetComponent<PageShifter>().GoToDatabasePage();
    }
    public void SetLinkTo(string Link) => this.Link = Link;
}
