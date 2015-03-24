
namespace Server.Items
{
    public class IronRunicHammer : RunicHammer
    {
        [Constructable]
        public IronRunicHammer(int uses) : base(CraftResource.Iron, uses) { }

        public IronRunicHammer(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class DullCopperRunicHammer : RunicHammer
    {
        [Constructable]
        public DullCopperRunicHammer(int uses) : base(CraftResource.DullCopper, uses) { }

        public DullCopperRunicHammer(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
    public class ShadowIronRunicHammer : RunicHammer
    {
        [Constructable]
        public ShadowIronRunicHammer(int uses) : base(CraftResource.ShadowIron, uses) { }

        public ShadowIronRunicHammer(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class CopperRunicHammer : RunicHammer
    {
        [Constructable]
        public CopperRunicHammer(int uses) : base(CraftResource.Copper, uses) { }

        public CopperRunicHammer(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class BronzeRunicHammer : RunicHammer
    {
        [Constructable]
        public BronzeRunicHammer(int uses) : base(CraftResource.Bronze, uses) { }

        public BronzeRunicHammer(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class GoldRunicHammer : RunicHammer
    {
        [Constructable]
        public GoldRunicHammer(int uses) : base(CraftResource.Gold, uses) { }

        public GoldRunicHammer(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class AgapiteRunicHammer : RunicHammer
    {
        [Constructable]
        public AgapiteRunicHammer(int uses) : base(CraftResource.Agapite, uses) { }

        public AgapiteRunicHammer(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class VeriteRunicHammer : RunicHammer
    {
        [Constructable]
        public VeriteRunicHammer(int uses) : base(CraftResource.Verite, uses) { }

        public VeriteRunicHammer(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class ValoriteRunicHammer : RunicHammer
    {
        [Constructable]
        public ValoriteRunicHammer(int uses) : base(CraftResource.Valorite, uses) { }

        public ValoriteRunicHammer(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
