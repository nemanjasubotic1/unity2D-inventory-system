using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour, ISavebleData
{
    public Vector2 Move => PlayerInput.Instance.FrameIn.move;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private UI_Inventory uI_Inventory;
    [SerializeField] private UI_CharacterEquipment uI_CharacterEquipment;

    private CharacterEquipment characterEquipment;

    private Rigidbody2D playerRigidBody;

    private Inventory inventory;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        characterEquipment = GetComponent<CharacterEquipment>();

        inventory = new Inventory(UseItem);
        
        uI_Inventory.SetPlayer(this);
        uI_Inventory.SetInventory(inventory);

    }

    private void Start()
    {
        uI_CharacterEquipment.SetCharacterEquipment(characterEquipment);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 moveDir = new Vector2(Move.x * moveSpeed, Move.y * moveSpeed);

        playerRigidBody.velocity = moveDir;
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                Debug.Log("I have used health potion");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                break;

            case Item.ItemType.ManaPotion:
                Debug.Log("I have used mana potion");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        ItemWorld itemWorld = collider2D.GetComponent<ItemWorld>();

        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    public Vector2 GetRigidBodyVelocity()
    {
        return playerRigidBody.velocity;
    }

    public void LoadGame(GameData gameData)
    {
        transform.position = gameData.playerPosition;
    }

    public void SaveGame(ref GameData gameData)
    {
        gameData.playerPosition = transform.position;
    }
}
