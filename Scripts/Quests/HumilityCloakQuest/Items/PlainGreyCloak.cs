using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Engines.Quests;

namespace Server.Items
{
    public class PlainGreyCloak : Cloak
    {
        private Mobile m_QuestOwner;
        private QuestDesire[] m_Desires;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile QuestOwner { get { return m_QuestOwner; } set { m_QuestOwner = value; } }

        [Constructable]
        public PlainGreyCloak()
            : base(0x1085)
        {
            Hue = 908;
        }

        [Constructable]
        public PlainGreyCloak(Mobile questOwner)
            : base(0x1085)
        {
            m_QuestOwner = questOwner;
            LootType = LootType.Blessed;
            Hue = 908;
            m_Desires = new QuestDesire[]{
            new QuestDesire(typeof(SeasonedSkillet),"skillet that's already been seasoned"),
            new QuestDesire(typeof(VillageCauldron),"cauldron from a nearby village"),
            new QuestDesire(typeof(ShortStool),"short stool to sit on"),
            new QuestDesire(typeof(FriendshipMug),"mug of friendship"),
            new QuestDesire(typeof(BrassRing),"ring made of brass"),
            new QuestDesire(typeof(WornHammer),"well used hammer"),
            new QuestDesire(typeof(PairOfWorkGloves),"sturdy pair of work gloves")
            };

            m_Desires.Shuffle();

        }

        public PlainGreyCloak(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber { get { return 1075789; } } // A Plain Grey Cloak

        public override bool CanEquip(Mobile from)
        {
            if (from is PlayerMobile && from == m_QuestOwner)
                if (QuestHelper.InProgress((PlayerMobile)from, new Type[] { typeof(HumilityCloakQuestFindTheHumble) }) || 
                    QuestHelper.FindCompletedQuest((PlayerMobile)from, typeof(HumilityCloakQuestFindTheHumble), false))
                {
                    return true;  // They can wear it if they are currently in, or have completed, the Quest.
                }
            from.SendMessage("You have no reason to wear this plain grey cloak.");
            return false;
        }

        public override void OnDoubleClick(Mobile from)
        {
            PlainGreyCloak cloak = from.FindItemOnLayer(Layer.Cloak) as PlainGreyCloak;

            if (Parent != from)
            {
                from.SendMessage("You must equip the cloak to perform this action.");
            }
            else
            {
                Item HumilityMarker = from.Backpack.FindItemByType(typeof(HumilityMarker));
                if (HumilityMarker != null && from.Region.IsPartOf(typeof(Regions.HumilityShrineRegion)) && from.Meditating)
                {
                    if (from.AddToBackpack(new HumilityCloak()))
                    {
                        HumilityMarker.Delete();

                        from.PlaySound(0x1F7);
                        from.FixedParticles(0x376A, 9, 32, 5030, EffectLayer.Waist);

                        from.SendLocalizedMessage(1075897); // As you near the shrine a strange energy envelops you. Suddenly, your cloak is transformed into the Cloak of Humility!

                        Delete();
                    }
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((Mobile)m_QuestOwner);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_QuestOwner = reader.ReadMobile();
                        break;
                    }
            }
        }
    }
}
