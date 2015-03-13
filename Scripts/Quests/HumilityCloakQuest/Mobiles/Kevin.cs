
using Server.Items;

namespace Server.Engines.Quests
{
    public class Kevin : HumilityQuester
    {
        public override int QuesterID { get { return 2; } }
        public override int GreetingMessage { get { return 1075759; } } // Art thou hiding under that cloak?
        public override int ResponseMessage { get { return 1075760; } } // Greetings, friend. Didst thou know I work all day, preparing and storing all sorts of meat? My cleaver is not dainty or at all particular, but if ye bringeth something to me then I will render it useful for food. Every creature can be thought of as useful in life... or in death. Death comes to us all, my friend. I hath learned that, for certain, in this humble profession.
        public override int HintMessage { get { return 1075761; } } // Speaking of useful, if ye findeth me a nice ~1_desire~, I wilt be grateful.
        public override int TradeMessage { get { return 1075762; } } // I received this ~1_gift~ as a gift. I hath no need for it, but wert ye to bring me something interesting, I would trade it, perhaps.
        public override int ThanksMessage { get { return 1075763; } } // Thou broughtest me a ~1_desire~! That will do nicely. Here, take this ~2_gift~ as thanks. 

        [Constructable]
        public Kevin()
            : base("Kevin", "the butcher")
        {
            Body = 0x190;
            Female = false;
            AddItem(new FancyShirt(Utility.RandomNeutralHue()));
            AddItem(new ShortPants(Utility.RandomNeutralHue()));
            AddItem(new HalfApron(Utility.RandomNeutralHue()));
            AddItem(new Shoes());

            Utility.AssignRandomHair(this);
            Utility.AssignRandomFacialHair(this);
            Hue = Utility.RandomSkinHue();
        }

        public Kevin(Serial serial)
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
    



	