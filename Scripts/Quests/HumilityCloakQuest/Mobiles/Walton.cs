
using Server.Items;

namespace Server.Engines.Quests
{
    public class Walton : HumilityQuester
    {
        public override int QuesterID { get { return 5; } }
        public override int GreetingMessage { get { return 1075739; } } // A horse blanket would offer more comfort than thine cloak, mayhaps.
        public override int ResponseMessage { get { return 1075740; } } // Hello friend. Yes, I work with yon horses each day. I carry bails of hay, feed, and even shovel manure for those beautiful beasts of burden. Everyone who rideth my animals enjoy their temperament and health. Yet, I do not ask for any recognition.
        public override int HintMessage { get { return 1075741; } } // All I need is but a simple ~1_desire~.
        public override int TradeMessage { get { return 1075742; } } // If I ever receive that which I need, I wilt gladly trade it for this ~1_gift~.
        public override int ThanksMessage { get { return 1075743; } } // Ah, thank you! This ~1_desire~ is just what I needed. Please taketh this ~2_gift~ - I hope it will be of use to thee. 

        [Constructable]
        public Walton()
            : base("Walton", "the horse trainer")
        {
            Body = 0x190;
            Female = false;
            AddItem(new Shirt(Utility.RandomNeutralHue()));
            AddItem(new LongPants(Utility.RandomNeutralHue()));
            AddItem(new BodySash(Utility.RandomNeutralHue()));
            AddItem(new ThighBoots());

            Utility.AssignRandomHair(this);
            Utility.AssignRandomFacialHair(this);
            Hue = Utility.RandomSkinHue();
        }

        public Walton(Serial serial)
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
    



	