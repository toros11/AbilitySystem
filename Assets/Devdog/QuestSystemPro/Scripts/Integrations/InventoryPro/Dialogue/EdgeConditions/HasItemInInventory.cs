#if INVENTORY_PRO

using Devdog.General;
using Devdog.InventoryPro;
using Devdog.QuestSystemPro.Dialogue;

namespace Devdog.QuestSystemPro.Integration.InventoryPro
{
    public class HasItemInInventory : SimpleEdgeCondition
    {
        [Required]
        public InventoryItemBase item;

        public override bool CanUse(Dialogue.Dialogue dialogue)
        {
            return InventoryManager.GetItemCountLike(item, false) > 0;
        }

        public override string FormattedString()
        {
            if(item == null)
            {
                return "(No item set)";
            }

            if (InventoryManager.GetItemCountLike(item, false) > 0)
            {
                return "Has item " + item.name;
            }

            return "Does not have item " + item.name;
        }
    }
}

#endif