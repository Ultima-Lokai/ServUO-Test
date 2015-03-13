using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Engines.Quests;

namespace Server.Items
{
    public class PlainGreyCloak : Cloak
    {
        private Mobile m_QuestOwner;
        private QuestDesire[] m_Desires;
        private List<Type> m_Offers;
        private List<Type> m_Wants;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile QuestOwner { get { return m_QuestOwner; } set { m_QuestOwner = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public List<Type> Offers { get { return m_Offers; } }
        public List<Type> Wants { get { return m_Wants; } }

        public QuestDesire[] Desires
        {
            get { return m_Desires; }
        }

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
            m_Desires = new QuestDesire[7];

            m_Offers = new List<Type>
            {
                typeof (SeasonedSkillet), typeof (VillageCauldron), typeof (ShortStool), typeof (FriendshipMug),
                typeof (WornHammer), typeof (PairOfWorkGloves)
            };

            m_Offers.Shuffle();
            m_Offers.Add(typeof(IronChain));

            m_Wants = new List<Type>
            {
                typeof (SeasonedSkillet), typeof (VillageCauldron), typeof (ShortStool), typeof (FriendshipMug),
                typeof (WornHammer), typeof (PairOfWorkGloves)
            };

            m_Wants.Shuffle();
            m_Wants.Add(typeof(BrassRing));

            AssignTypes();

        }

        private void AssignTypes()
        {
            List<string> offerStrings = new List<string>();

            foreach(Type type in m_Offers)
            {
                if (type.ToString().ToLower().Contains("skillet")) offerStrings.Add("skillet");
                if (type.ToString().ToLower().Contains("cauldron")) offerStrings.Add("cauldron");
                if (type.ToString().ToLower().Contains("stool")) offerStrings.Add("stool");
                if (type.ToString().ToLower().Contains("mug")) offerStrings.Add("mug");
                if (type.ToString().ToLower().Contains("hammer")) offerStrings.Add("hammer");
                if (type.ToString().ToLower().Contains("gloves")) offerStrings.Add("gloves");
                if (type.ToString().ToLower().Contains("chain")) offerStrings.Add("chain");
            }

            List<string> wantStrings = new List<string>();
            
            foreach(Type type in m_Wants)
            {
                if (type.ToString().ToLower().Contains("skillet")) wantStrings.Add("skillet");
                if (type.ToString().ToLower().Contains("cauldron")) wantStrings.Add("cauldron");
                if (type.ToString().ToLower().Contains("stool")) wantStrings.Add("stool");
                if (type.ToString().ToLower().Contains("mug")) wantStrings.Add("mug");
                if (type.ToString().ToLower().Contains("hammer")) wantStrings.Add("hammer");
                if (type.ToString().ToLower().Contains("gloves")) wantStrings.Add("gloves");
                if (type.ToString().ToLower().Contains("ring")) wantStrings.Add("ring");
            }

            List<int> tempInts = new List<int>() {0, 1, 2, 3, 4, 5, 6};

            int questerID = 6;
            int wantID;

            for (int i = 6; i > 1; i--)
            {
                wantID = Utility.Random(6);
                m_Desires[questerID] = new QuestDesire(questerID, m_Wants[wantID], wantStrings[wantID],
                    m_Offers[questerID], offerStrings[questerID]);
                tempInts.Remove(questerID);
                questerID = tempInts[Utility.Random(tempInts.Count)];
            }
            questerID = tempInts[0];
            wantID = 6;
            m_Desires[questerID] = new QuestDesire(questerID, m_Wants[wantID], wantStrings[wantID],
                m_Offers[questerID], offerStrings[questerID]);
        }

        public bool Interact(HumilityQuester humble, int questerID, string interactionType)
        {
            try
            {
                if (m_QuestOwner != null && CanInteract(questerID, interactionType))
                {
                    switch (interactionType)
                    {
                        case "greet":
                            humble.SayTo(m_QuestOwner, humble.GreetingMessage);
                            m_Desires[questerID].Greeted = true;
                            m_Desires[questerID].GreetTime = DateTime.UtcNow;
                            break;
                        case "hint":
                            humble.SayTo(m_QuestOwner, humble.HintMessage);
                            m_Desires[questerID].Hinted = true;
                            m_Desires[questerID].HintTime = DateTime.UtcNow;
                            break;
                        case "trade":
                            humble.SayTo(m_QuestOwner, humble.TradeMessage);
                            m_Desires[questerID].Traded = true;
                            m_Desires[questerID].TradeTime = DateTime.UtcNow;
                            break;
                        case "thank":
                            humble.SayTo(m_QuestOwner, humble.ThanksMessage);
                            m_Desires[questerID].Thanked = true;
                            m_Desires[questerID].ThankTime = DateTime.UtcNow;
                            break;
                    }
                    return true;
                }
            }
            catch { }
            return false;
        }

        public bool CanInteract(int questerID, string interactionType)
        {
            try
            {
                switch (interactionType)
                {
                    case "greet":
                        return (!m_Desires[questerID].Greeted ||
                                m_Desires[questerID].GreetTime < DateTime.UtcNow + TimeSpan.FromMinutes(2.0));
                    case "hint":
                        return (!m_Desires[questerID].Hinted ||
                                m_Desires[questerID].HintTime < DateTime.UtcNow + TimeSpan.FromMinutes(7.0));
                    case "trade":
                        return (!m_Desires[questerID].Traded ||
                                m_Desires[questerID].TradeTime < DateTime.UtcNow + TimeSpan.FromMinutes(12.0));
                    case "thank":
                        return (!m_Desires[questerID].Thanked ||
                                m_Desires[questerID].ThankTime < DateTime.UtcNow + TimeSpan.FromMinutes(15.0));
                }
            }
            catch { }
            return false;
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

            foreach (QuestDesire desire in m_Desires)
            {
                writer.Write(desire.QuesterID);
                writer.Write(desire.DesireType.ToString());
                writer.Write(desire.DesireName);
                writer.Write(desire.OfferType.ToString());
                writer.Write(desire.OfferName);
                writer.Write(desire.Greeted);
                writer.Write(desire.Hinted);
                writer.Write(desire.Traded);
                writer.Write(desire.Thanked);
            }
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

                        m_Desires = new QuestDesire[]
                        {
                            new QuestDesire(reader.ReadInt(), Type.GetType(reader.ReadString()), reader.ReadString(),
                                Type.GetType(reader.ReadString()), reader.ReadString(), reader.ReadBool(),
                                reader.ReadBool(), reader.ReadBool(), reader.ReadBool()),
                            new QuestDesire(reader.ReadInt(), Type.GetType(reader.ReadString()), reader.ReadString(),
                                Type.GetType(reader.ReadString()), reader.ReadString(), reader.ReadBool(),
                                reader.ReadBool(), reader.ReadBool(), reader.ReadBool()),
                            new QuestDesire(reader.ReadInt(), Type.GetType(reader.ReadString()), reader.ReadString(),
                                Type.GetType(reader.ReadString()), reader.ReadString(), reader.ReadBool(),
                                reader.ReadBool(), reader.ReadBool(), reader.ReadBool()),
                            new QuestDesire(reader.ReadInt(), Type.GetType(reader.ReadString()), reader.ReadString(),
                                Type.GetType(reader.ReadString()), reader.ReadString(), reader.ReadBool(),
                                reader.ReadBool(), reader.ReadBool(), reader.ReadBool()),
                            new QuestDesire(reader.ReadInt(), Type.GetType(reader.ReadString()), reader.ReadString(),
                                Type.GetType(reader.ReadString()), reader.ReadString(), reader.ReadBool(),
                                reader.ReadBool(), reader.ReadBool(), reader.ReadBool()),
                            new QuestDesire(reader.ReadInt(), Type.GetType(reader.ReadString()), reader.ReadString(),
                                Type.GetType(reader.ReadString()), reader.ReadString(), reader.ReadBool(),
                                reader.ReadBool(), reader.ReadBool(), reader.ReadBool()),
                            new QuestDesire(reader.ReadInt(), Type.GetType(reader.ReadString()), reader.ReadString(),
                                Type.GetType(reader.ReadString()), reader.ReadString(), reader.ReadBool(),
                                reader.ReadBool(), reader.ReadBool(), reader.ReadBool())
                        };
                        break;
                    }
            }
        }
    }
}
