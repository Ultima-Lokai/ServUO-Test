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

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile QuestOwner { get { return m_QuestOwner; } set { m_QuestOwner = value; } }
        
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
            m_Offers.CopyTo(m_Wants);

            m_Wants[6] = typeof(BrassRing);

            AssignTypes();

        }

        private Type[] m_Wants = new Type[7];
        private List<Type> m_Offers;

        private void AssignTypes()
        {
            List<string> offerStrings = new List<string>();

            foreach(Type type in m_Offers)
            {
                if (type == typeof(SeasonedSkillet)) offerStrings.Add("skillet");
                if (type == typeof(VillageCauldron)) offerStrings.Add("cauldron");
                if (type == typeof(ShortStool)) offerStrings.Add("stool");
                if (type == typeof(FriendshipMug)) offerStrings.Add("mug");
                if (type == typeof(WornHammer)) offerStrings.Add("hammer");
                if (type == typeof(PairOfWorkGloves)) offerStrings.Add("pair of gloves");
                if (type == typeof(IronChain)) offerStrings.Add("chain");
            }

            List<string> wantStrings = new List<string>();
            
            foreach(Type type in m_Wants)
            {
                if (type == typeof(SeasonedSkillet)) wantStrings.Add("skillet");
                if (type == typeof(VillageCauldron)) wantStrings.Add("cauldron");
                if (type == typeof(ShortStool)) wantStrings.Add("stool");
                if (type == typeof(FriendshipMug)) wantStrings.Add("mug");
                if (type == typeof(WornHammer)) wantStrings.Add("hammer");
                if (type == typeof(PairOfWorkGloves)) wantStrings.Add("pair of gloves");
                if (type == typeof(BrassRing)) wantStrings.Add("ring");
            }

            List<int> tempInts = new List<int>() { 0, 1, 2, 3, 4, 5 };
            List<int> tempWantInts = new List<int>() { 0, 1, 2, 3, 4, 5 };

            int questerID = 6; // Sean goes first
            int wantID = Utility.Random(6); // He wants something random (not the ring)
            m_Desires[questerID] = new QuestDesire(questerID, m_Wants[wantID], wantStrings[wantID],
                m_Offers[questerID], offerStrings[questerID]);
            tempWantInts.Remove(wantID); // Remove what Sean wants from the want-list

            for (int i = 0; i < 5; i++)
            {
                questerID = wantID; // Now we find out who supplies what Sean wants
                wantID = tempWantInts[Utility.Random(tempWantInts.Count)]; // Then we find out what that person wants, and so on.
                m_Desires[questerID] = new QuestDesire(questerID, m_Wants[wantID], wantStrings[wantID],
                    m_Offers[questerID], offerStrings[questerID]);
                tempInts.Remove(questerID); // Remove each questerID ...
                tempWantInts.Remove(wantID); // and each wantID from their lists ...
            }
            questerID = tempInts[0]; // Until there is only one person left, and that person ...
            wantID = 6; // wants the brass ring.
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
                            humble.SayTo(m_QuestOwner, humble.HintMessage, m_Desires[questerID].DesireName);
                            m_Desires[questerID].Hinted = true;
                            m_Desires[questerID].HintTime = DateTime.UtcNow;
                            break;
                        case "trade":
                            if (questerID == 6) break; // Sean does not give information about the iron chain
                            humble.SayTo(m_QuestOwner, humble.TradeMessage, m_Desires[questerID].OfferName);
                            m_Desires[questerID].Traded = true;
                            m_Desires[questerID].TradeTime = DateTime.UtcNow;
                            break;
                        case "thank":
                            humble.SayTo(m_QuestOwner, humble.ThanksMessage, string.Format("{0}/t{1}", m_Desires[questerID].DesireName, m_Desires[questerID].OfferName));
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
                                m_Desires[questerID].GreetTime < DateTime.UtcNow - TimeSpan.FromMinutes(2.0));
                    case "hint":
                        return (!m_Desires[questerID].Hinted ||
                                m_Desires[questerID].HintTime < DateTime.UtcNow - TimeSpan.FromMinutes(5.0));
                    case "trade":
                        return (!m_Desires[questerID].Traded ||
                                m_Desires[questerID].TradeTime < DateTime.UtcNow - TimeSpan.FromMinutes(12.0));
                    case "thank":
                        return (!m_Desires[questerID].Thanked ||
                                m_Desires[questerID].ThankTime < DateTime.UtcNow - TimeSpan.FromMinutes(15.0));
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
                if (
                    QuestHelper.FindCompletedQuest((PlayerMobile) from, typeof (HumilityCloakQuestFindTheHumble), false) ||
                    QuestHelper.InProgress((PlayerMobile) from, new Type[] {typeof (HumilityCloakQuestFindTheHumble)}))
                {
                    return true; // They can wear it if they are currently in, or have completed, the Quest.
                }
            from.SendMessage("You have no reason to wear this plain grey cloak.");
            return false;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((Mobile)m_QuestOwner);

            foreach (QuestDesire desire in m_Desires)
            {
                try
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
                catch { }
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
