using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform itemWorldTransform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = itemWorldTransform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, +1f)).normalized;

        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 2f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 3f, ForceMode2D.Impulse);
        
        return itemWorld;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    private Light2D light2D;
    private TextMeshPro amountText;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light2D = transform.Find("light").GetComponent<Light2D>();
        amountText = transform.Find("amountText").GetComponent<TextMeshPro>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        light2D.color = item.GetColor();

        if (item.amount == 1)
        {
            amountText.SetText("");
        }
        else
        {
            amountText.SetText(item.amount.ToString());
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
