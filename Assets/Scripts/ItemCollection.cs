using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItemCollection : MonoBehaviour
{

    private int collecableItems = 0;

    [SerializeField] private TextMeshProUGUI collectableText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
            collecableItems++;
            Debug.Log("Collected " + collecableItems + " collectables");
            collectableText.text = "Items: " + collecableItems;
        }
    }
}
