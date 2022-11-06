using UnityEngine;
using System.Collections;

public class DiceThrower : MonoBehaviour
{
    [SerializeField] private GameObject _d4, _d6, _d8, _d10, _d12, _d20;
    [SerializeField] private float _force;
    public void ThrowDie(string Number)
    {
        var Die = Number switch
        { "4" => _d4, "6" => _d6, "8" => _d8, "10" => _d10, "12" => _d12, _ => _d20, };

        var ThrownDie = Instantiate(Die, transform.position + new Vector3(0,80,80), Quaternion.identity);
        ApplyForce(ThrownDie);
    }

    private void ApplyForce(GameObject Die)
    {
        Vector3 Force  = new Vector3(Random.Range(-0.2f, 0.2f) * _force, -4 * _force, _force);
        Rigidbody DieRigidbody = Die.gameObject.GetComponent<Rigidbody>();
        DieRigidbody.AddForce(Force, ForceMode.Impulse);
        DieRigidbody.angularVelocity = new Vector3(Random.Range(-180,180), Random.Range(-180, 180), Random.Range(-180, 180));

        StartCoroutine(FadeOut(Die.transform));
    }

    private IEnumerator FadeOut(Transform DieTransform)
    {
        yield return new WaitForSeconds(6f);
        Destroy(DieTransform.gameObject);
    }
}
