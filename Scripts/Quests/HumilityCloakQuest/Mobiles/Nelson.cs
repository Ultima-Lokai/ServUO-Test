
using Server.Items;

namespace Server.Engines.Quests
{
    public class Nelson : HumilityQuester
    {
        public override int QuesterID { get { return 4; } }
        public override int GreetingMessage { get { return 1075749; } } // I doth have a similar cloak.
        public override int ResponseMessage { get { return 1075750; } } // Greetings. I tend my flock and provide guidance to my fellow citizens. I seek not a life full of profit. For to strive for personal recognition or social position is not a worthy cause. A humble man is happy with his place.
        public override int HintMessage { get { return 1075751; } } // All that I needest is a ~1_desire~.
        public override int TradeMessage { get { return 1075752; } } // I useth this ~1_gift~ not at all. If ye bringeth me the right item, I would make a trade.
        public override int ThanksMessage { get { return 1075753; } } // Ah, ‘tis a perfect ~1_desire~. Please take this ~2_gift~ in trade. 

        [Constructable]
        public Nelson()
            : base("Nelson", "the shepherd")
        {
            Body = 0x190;
            Female = false;
            AddItem(new Robe(Utility.RandomNeutralHue()));
            AddItem(new LongPants(Utility.RandomNeutralHue()));
            AddItem(new Sandals());

            Utility.AssignRandomHair(this);
            Utility.AssignRandomFacialHair(this);
            Hue = Utility.RandomSkinHue();
        }

        public Nelson(Serial serial)
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
    



	