using UnityEngine;

public class ElementLink : MonoBehaviour
{
    public DatabaseElement Link { get; private set; }
    public void SetRefferalLinkAndGoTo()
    {
        GameObject.FindGameObjectWithTag("Foreground").GetComponent<ElementLink>().SetLinkTo(Link);
        GoToLink();
    }
    private void GoToLink() => GameObject.FindGameObjectWithTag("Content").GetComponent<PageShifter>().GoToDatabasePage();
    public void SetLinkTo(DatabaseElement Link) => this.Link = Link;
}
