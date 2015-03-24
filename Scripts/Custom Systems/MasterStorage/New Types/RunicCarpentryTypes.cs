
namespace Server.Items
{
    public class RegularRunicDovetailSaw : RunicDovetailSaw
    {
        [Constructable]
        public RegularRunicDovetailSaw(int uses) : base(CraftResource.RegularWood, uses) { }

        public RegularRunicDovetailSaw(Serial serial) : base(serial) { }

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

    public class OakRunicDovetailSaw : RunicDovetailSaw
    {
        [Constructable]
        public OakRunicDovetailSaw(int uses) : base(CraftResource.OakWood, uses) { }

        public OakRunicDovetailSaw(Serial serial) : base(serial) { }

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

    public class AshRunicDovetailSaw : RunicDovetailSaw
    {
        [Constructable]
        public AshRunicDovetailSaw(int uses) : base(CraftResource.AshWood, uses) { }

        public AshRunicDovetailSaw(Serial serial) : base(serial) { }

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

    public class YewRunicDovetailSaw : RunicDovetailSaw
    {
        [Constructable]
        public YewRunicDovetailSaw(int uses) : base(CraftResource.YewWood, uses) { }

        public YewRunicDovetailSaw(Serial serial) : base(serial) { }

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

    public class HeartwoodRunicDovetailSaw : RunicDovetailSaw
    {
        [Constructable]
        public HeartwoodRunicDovetailSaw(int uses) : base(CraftResource.Heartwood, uses) { }

        public HeartwoodRunicDovetailSaw(Serial serial) : base(serial) { }

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

    public class BloodwoodRunicDovetailSaw : RunicDovetailSaw
    {
        [Constructable]
        public BloodwoodRunicDovetailSaw(int uses) : base(CraftResource.Bloodwood, uses) { }

        public BloodwoodRunicDovetailSaw(Serial serial) : base(serial) { }

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

    public class FrostwoodRunicDovetailSaw : RunicDovetailSaw
    {
        [Constructable]
        public FrostwoodRunicDovetailSaw(int uses) : base(CraftResource.Frostwood, uses) { }

        public FrostwoodRunicDovetailSaw(Serial serial) : base(serial) { }

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
