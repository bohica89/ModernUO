using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Engines.Quests.Samurai
{
  public class HaochisTreasureChest : WoodenFootLocker
  {
    [Constructible]
    public HaochisTreasureChest()
    {
      Movable = false;

      GenerateTreasure();
    }

    public HaochisTreasureChest(Serial serial) : base(serial)
    {
    }

    public override bool IsDecoContainer => false;

    private void GenerateTreasure()
    {
      for (int i = Items.Count - 1; i >= 0; i--)
        Items[i].Delete();

      for (int i = 0; i < 75; i++)
        switch (Utility.Random(3))
        {
          case 0:
            DropItem(new GoldBracelet());
            break;
          case 1:
            DropItem(new GoldRing());
            break;
          case 2:
            DropItem(Loot.RandomGem());
            break;
        }
    }

    public override bool CheckHold(Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight) => false;

    public override bool CheckItemUse(Mobile from, Item item) => item == this;

    public override bool CheckLift(Mobile from, Item item, ref LRReason reject)
    {
      if (from.AccessLevel >= AccessLevel.GameMaster)
        return true;

      if (from is PlayerMobile player && player.Quest is HaochisTrialsQuest)
      {
        FifthTrialIntroObjective obj = player.Quest.FindObjective<FifthTrialIntroObjective>();
        if (obj?.StolenTreasure == true)
          from.SendLocalizedMessage(
            1063247); // The guard is watching you carefully!  It would be unwise to remove another item from here.
        else
          return true;
      }

      return false;
    }

    public override void OnItemLifted(Mobile from, Item item)
    {
      if (from is PlayerMobile player && player.Quest is HaochisTrialsQuest)
      {
        FifthTrialIntroObjective obj = player.Quest.FindObjective<FifthTrialIntroObjective>();
        if (obj != null)
          obj.StolenTreasure = true;
      }

      Timer.DelayCall(TimeSpan.FromMinutes(2.0), GenerateTreasure);
    }

    public override void Serialize(GenericWriter writer)
    {
      base.Serialize(writer);

      writer.WriteEncodedInt(0); // version
    }

    public override void Deserialize(GenericReader reader)
    {
      base.Deserialize(reader);

      int version = reader.ReadEncodedInt();

      Timer.DelayCall(TimeSpan.Zero, GenerateTreasure);
    }
  }
}