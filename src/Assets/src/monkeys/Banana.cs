using UnityEngine;
using System.Collections;
using Assets.Scripts;

[RequireComponent(typeof(Rigidbody2D))]
public class Banana : MonoBehaviour
{

    public GameObject asset;
    public bool onAir;

    void Start()
    {
        transform.localPosition = new Vector3(-7, -3, 0);
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<TrailRenderer>().sortingLayerName = "Foreground";

        GetComponent<Rigidbody2D>().isKinematic = true;

        GetComponent<CircleCollider2D>().radius = Constants.BirdColliderRadiusBig;
        State = BirdState.BeforeThrown;
    }
    private int rotationZ;
    public void OnThrow()
    {
        rotationZ = Random.Range(4, 10);
        if (Random.Range(0, 10) < 5) rotationZ *= -1;
        onAir = true;
        GetComponent<Rigidbody2D>().isKinematic = false;

        GetComponent<CircleCollider2D>().radius = Constants.BirdColliderRadiusNormal;
        State = BirdState.Thrown;

        StartCoroutine(DestroyAfter(5));
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor")
            onAir = false;
    }
    void Update()
    {
        if(onAir)
            asset.transform.localEulerAngles = new Vector3(0, 0, asset.transform.localEulerAngles.z + rotationZ);
    }
    IEnumerator DestroyAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    public BirdState State
    {
        get;
        private set;
    }
}
