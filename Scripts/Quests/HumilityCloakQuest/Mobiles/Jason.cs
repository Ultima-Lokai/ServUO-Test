
using Server.Items;

namespace Server.Engines.Quests
{
    public class Jason : HumilityQuester
    {
        public override int QuesterID { get { return 1; } }
        public override int GreetingMessage { get { return 1075764; } } // Thou looketh like a fellow healer in that cloak.
        public override int ResponseMessage { get { return 1075765; } } // I am the sort of person who wandereth the countryside for weeks, my friend. All that time I spend healing good people who art in need of my services. All of this done without any reward, save the knowledge that I leadeth a life of virtue.
        public override int HintMessage { get { return 1075766; } } // I have my needs, however.  A ~1_desire~ would certainly help me.
        public override int TradeMessage { get { return 1075767; } } // This ~1_gift~ was sent to me by mistake, I wouldest like to trade it for what I need.
        public override int ThanksMessage { get { return 1075768; } } // A ~1_desire~! Many thanks to thee. Please accept this ~2_gift~. 

        [Constructable]
        public Jason()
            : base("Jason", "the healer")
        {
            Body = 0x190;
            Female = false;
            AddItem(new Robe(Utility.RandomNeutralHue()));
            AddItem(new Sandals());

            Utility.AssignRandomHair(this);
            Utility.AssignRandomFacialHair(this);
            Hue = Utility.RandomSkinHue();
        }

        public Jason(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
    



	