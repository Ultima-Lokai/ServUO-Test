using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class Gareth : MondainQuester
    {
        public override Type[] Quests
        {
            get
            {
                return new Type[]
                {
                    typeof (HumilityCloakQuest)
                };
            }
        }

        [Constructable]
        public Gareth()
            : base("Gareth", "Emissary of the RBC")
        {
        }

        public override void Advertise()
        {
            this.Say(1075674); // Hail! Care to join our efforts for the Rise of Britannia?
        }

        public Gareth(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile m)
        {
            if (m is PlayerMobile)
            {
                PlayerMobile pm = (PlayerMobile) m;
                Container pack = pm.Backpack;
                if (pack != null && !pack.Deleted)
                {
                    QuestionScroll answer = (QuestionScroll)pack.FindItemByType(typeof(QuestionScroll));
                    if (answer.CorrectAnswerGiven)
                    {
                        if (answer.QuestionID < answer.Quest.Scrolls.Length - 1)
                        {
                            QuestionScroll scroll = (QuestionScroll)Activator.CreateInstance(typeof(QuestionScroll));
                            QuestionScroll copy = answer.Quest.Scrolls[answer.QuestionID + 1];
                        }
                    }
                }
            }
            base.OnDoubleClick(m);
        }

        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Female = false;
            Race = Race.Human;

            Hue = 0x841C;
            HairItemID = 0x203C;
            HairHue = 0xF7;
        }

        public override void InitOutfit()
        {
            AddItem(new LongPants(642));
            AddItem(new FancyShirt(89));
            AddItem(new Boots());
            AddItem(new BodySash(97));

            PackGold(100, 200);
            Blessed = true;
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
}
