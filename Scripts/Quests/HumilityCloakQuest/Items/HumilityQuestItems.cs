using System;
using Server.Items;

namespace Server.Engines.Quests
{

    public class SeasonedSkillet : BaseQuestItem
    {
        [Constructable]
        public SeasonedSkillet()
            : base(0x09E2)
        {
        }

        public SeasonedSkillet(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests { get { return new Type[] { typeof(HumilityCloakQuestFindTheHumble) }; } }

        public override int LabelNumber { get { return 1075774; } } // Seasoned Skillet

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

    public class VillageCauldron : BaseQuestItem
    {
        [Constructable]
        public VillageCauldron()
            : base(0x09ED)
        {
        }

        public VillageCauldron(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests { get { return new Type[] { typeof(HumilityCloakQuestFindTheHumble) }; } }

        public override int LabelNumber { get { return 1075775; } } // Village Cauldron

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

    public class ShortStool : BaseQuestItem
    {
        [Constructable]
        public ShortStool()
            : base(0xB5E)
        {
        }

        public ShortStool(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests { get { return new Type[] { typeof(HumilityCloakQuestFindTheHumble) }; } }

        public override int LabelNumber { get { return 1075776; } } // Short Stool

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

    public class FriendshipMug : BaseQuestItem
    {
        [Constructable]
        public FriendshipMug()
            : base(0x0999)
        {
        }

        public FriendshipMug(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests { get { return new Type[] { typeof(HumilityCloakQuestFindTheHumble) }; } }

        public override int LabelNumber { get { return 1075777; } } // Friendship Mug

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

    public class BrassRing : BaseQuestItem
    {
        [Constructable]
        public BrassRing()
            : base(0x108A)
        {
        }

        public BrassRing(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests { get { return new Type[] { typeof(HumilityCloakQuestFindTheHumble) }; } }

        public override int LabelNumber { get { return 1075778; } } // Brass Ring 

        public override bool OnEquip(Mobile from)
        {
            from.SendLocalizedMessage(1075896); // The brass ring seems to be too small to slip onto your finger.
            return false;
        }

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

    public class WornHammer : BaseQuestItem
    {
        [Constructable]
        public WornHammer()
            : base(0x102A)
        {
        }

        public WornHammer(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests { get { return new Type[] { typeof(HumilityCloakQuestFindTheHumble) }; } }

        public override int LabelNumber { get { return 1075779; } } // Worn Hammer

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

    public class PairOfWorkGloves : BaseQuestItem
    {
        [Constructable]
        public PairOfWorkGloves()
            : base(0x13C6)
        {
        }

        public PairOfWorkGloves(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests { get { return new Type[] { typeof(HumilityCloakQuestFindTheHumble) }; } }

        public override int LabelNumber { get { return 1075780; } } // Pair of Work Gloves

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

    public class IronChain : BaseQuestItem
    {
        [Constructable]
        public IronChain()
            : base(0x1085)
        {
        }

        public IronChain(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests { get { return new Type[] { typeof(HumilityCloakQuestFindTheHumble) }; } }

        public override int LabelNumber { get { return 1075788; } } // Iron Chain

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

    public class HumilityCrookReplica : BaseQuestItem
    {
        [Constructable]
        public HumilityCrookReplica()
            : base(0x0E82)
        {
            Hue = 1;
        }

        public HumilityCrookReplica(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests { get { return new Type[] { typeof(HumilityCloakQuestVesperMuseum) }; } }
        
        public override int LabelNumber { get { return 1075791; } } // A Replica of the Shepherd's Crook of Humility

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

    public class ForLifeBritanniaSash : BaseQuestItem
    {
        [Constructable]
        public ForLifeBritanniaSash()
            : base(0x1542)
        {
            Hue = 53;
        }

        public ForLifeBritanniaSash(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests { get { return new Type[] { typeof(HumilityCloakQuestMoonglowZoo) }; } }

        public override int LabelNumber { get { return 1075792; } } // For the Life of Britannia Sash

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

    public class SpecialPrintingOfVirtue : BaseQuestItem
    {
        [Constructable]
        public SpecialPrintingOfVirtue()
            : base(0x0FEF)
        {
            Hue = 71;
        }

        public SpecialPrintingOfVirtue(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests { get { return new Type[] { typeof(HumilityCloakQuestBritainLibrary) }; } }

        public override int LabelNumber { get { return 1075793; } } // Special Printing of 'Virtue' by Lord British

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