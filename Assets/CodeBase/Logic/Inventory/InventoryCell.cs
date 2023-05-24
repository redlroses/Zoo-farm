﻿using CodeBase.Logic.Items;
using UnityEngine;

namespace CodeBase.Logic.Inventory
{
    public sealed class InventoryCell : IReadOnlyInventoryCell
    {
        public int Count { get; private set; }
        public IItem Item { get; }

        public bool IsEmpty => Count <= 0;

        public InventoryCell(IItem item)
        {
            Count = 1;
            Item = item;
        }

        public void Increase() =>
            Count++;

        public void Decrease() =>
            Count--;
    }
}