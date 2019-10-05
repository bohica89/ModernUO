namespace Server.Items
{
  [Flippable(0x26BB, 0x26C5)]
  public class BoneHarvester : BaseSword
  {
    [Constructible]
    public BoneHarvester() : base(0x26BB) => Weight = 3.0;

    public BoneHarvester(Serial serial) : base(serial)
    {
    }

    public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
    public override WeaponAbility SecondaryAbility => WeaponAbility.MortalStrike;

    public override int AosStrengthReq => 25;
    public override int AosMinDamage => 13;
    public override int AosMaxDamage => 15;
    public override int AosSpeed => 36;
    public override float MlSpeed => 3.00f;

    public override int OldStrengthReq => 25;
    public override int OldMinDamage => 13;
    public override int OldMaxDamage => 15;
    public override int OldSpeed => 36;

    public override int DefHitSound => 0x23B;
    public override int DefMissSound => 0x23A;

    public override int InitMinHits => 31;
    public override int InitMaxHits => 70;

    public override void Serialize(GenericWriter writer)
    {
      base.Serialize(writer);

      writer.Write(0); // version
    }

    public override void Deserialize(GenericReader reader)
    {
      base.Deserialize(reader);

      int version = reader.ReadInt();
    }
  }
}