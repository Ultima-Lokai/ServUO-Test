
namespace Server.Items
{
    public class RegularRunicSewingKit : RunicSewingKit
    {
        [Constructable]
        public RegularRunicSewingKit(int uses) : base(CraftResource.RegularLeather, uses) { }

        public RegularRunicSewingKit(Serial serial) : base(serial) { }

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

    public class SpinedRunicSewingKit : RunicSewingKit
    {
        [Constructable]
        public SpinedRunicSewingKit(int uses) : base(CraftResource.SpinedLeather, uses) { }

        public SpinedRunicSewingKit(Serial serial) : base(serial) { }

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

    public class HornedRunicSewingKit : RunicSewingKit
    {
        [Constructable]
        public HornedRunicSewingKit(int uses) : base(CraftResource.HornedLeather, uses) { }

        public HornedRunicSewingKit(Serial serial) : base(serial) { }

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

    public class BarbedRunicSewingKit : RunicSewingKit
    {
        [Constructable]
        public BarbedRunicSewingKit(int uses) : base(CraftResource.BarbedLeather, uses) { }

        public BarbedRunicSewingKit(Serial serial) : base(serial) { }

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
