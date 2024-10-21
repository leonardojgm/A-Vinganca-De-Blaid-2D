using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("O item foi coletado");

        GameManager.Instance.UpdateKeysLeft();

        Destroy(this.gameObject);
    }
}
