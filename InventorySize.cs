using System;
using UnityEngine;

namespace AcceleratedStart
{
    /// <summary>
    /// All valid sizes for the config setting on inventory size modification.
    /// </summary>
    public enum InventorySize
    {
        Tiny,
        Small,
        Vanilla,
        Large,
        Huge,
    }

    public static class InventorySizeExtensions
    {
        public static Vector2 GetSize(this InventorySize size)
        {
            return size switch
            {
                InventorySize.Tiny => new Vector2(4, 4),
                InventorySize.Small => new Vector2(5, 6),
                InventorySize.Vanilla => new Vector2(6, 8),
                InventorySize.Large => new Vector2(6, 10),
                InventorySize.Huge => new Vector2(8, 10),
                _ => throw new ArgumentOutOfRangeException(nameof(size), $"Value not valid for enum: {size}")
            };
        }
    }
}