using UnityEngine;
using UnityEngine.Tilemaps;

public class SpriteTilePainter : MonoBehaviour
{
    public Tilemap tilemap;          // Reference to the Tilemap
    public Sprite paintSprite;       // The sprite to paint
    public float paintHeight = 0.1f; // Height to slightly lift the sprite for painting (optional)

    private GameObject currentPaintObject;

    void Update()
    {
        if (tilemap == null || paintSprite == null) return;

        // Get the mouse position in world coordinates
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Convert the world position to a grid position
        Vector3Int gridPosition = tilemap.WorldToCell(worldPoint);

        // Check for painting action
        if (GlobalVariable.selectMode && Input.GetMouseButtonDown(0)) // Left mouse click
        {
            // Remove previous paint object if it exists
            if (currentPaintObject != null)
            {
                Destroy(currentPaintObject);
            }

            // Create the new painted sprite at the grid position
            currentPaintObject = new GameObject("PaintedSprite");
            currentPaintObject.transform.position = tilemap.CellToWorld(gridPosition) + new Vector3(0, 0, paintHeight); // Slight height offset

            // Add a SpriteRenderer component to the new object and assign the paint sprite
            SpriteRenderer spriteRenderer = currentPaintObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = paintSprite;
            spriteRenderer.sortingOrder = 1; // Make sure it’s above the tilemap (optional)
        }
    }
}
