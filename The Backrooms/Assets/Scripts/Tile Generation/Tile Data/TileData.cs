using UnityEngine;

[CreateAssetMenu (fileName = "New Tile", menuName = "Tile")]
public class TileData : ScriptableObject
{
    public GameObject tilePrefab;

    [Header("Connection Possibilities")]
    public bool connectTop;
    public bool connectRight;
    public bool connectBottom;
    public bool connectLeft;
}
