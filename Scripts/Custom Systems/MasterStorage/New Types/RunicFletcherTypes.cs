
namespace Server.Items
{
    public class RegularRunicFletcherTool : RunicFletcherTool
    {
        [Constructable]
        public RegularRunicFletcherTool(int uses): base(CraftResource.RegularWood, uses){}

        public RegularRunicFletcherTool(Serial serial) : base(serial) { }

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

    public class OakRunicFletcherTool : RunicFletcherTool
    {
        [Constructable]
        public OakRunicFletcherTool(int uses) : base(CraftResource.OakWood, uses) { }

        public OakRunicFletcherTool(Serial serial) : base(serial) { }

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

    public class AshRunicFletcherTool : RunicFletcherTool
    {
        [Constructable]
        public AshRunicFletcherTool(int uses) : base(CraftResource.AshWood, uses) { }

        public AshRunicFletcherTool(Serial serial) : base(serial) { }

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

    public class YewRunicFletcherTool : RunicFletcherTool
    {
        [Constructable]
        public YewRunicFletcherTool(int uses) : base(CraftResource.YewWood, uses) { }

        public YewRunicFletcherTool(Serial serial) : base(serial) { }

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

    public class HeartwoodRunicFletcherTool : RunicFletcherTool
    {
        [Constructable]
        public HeartwoodRunicFletcherTool(int uses) : base(CraftResource.Heartwood, uses) { }

        public HeartwoodRunicFletcherTool(Serial serial) : base(serial) { }

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

    public class BloodwoodRunicFletcherTool : RunicFletcherTool
    {
        [Constructable]
        public BloodwoodRunicFletcherTool(int uses) : base(CraftResource.Bloodwood, uses) { }

        public BloodwoodRunicFletcherTool(Serial serial) : base(serial) { }

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

    public class FrostwoodRunicFletcherTool : RunicFletcherTool
    {
        [Constructable]
        public FrostwoodRunicFletcherTool(int uses) : base(CraftResource.Frostwood, uses) { }

        public FrostwoodRunicFletcherTool(Serial serial) : base(serial) { }

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
