using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class Deirdre : MondainQuester
    {
        public override int GreetingMessage { get { return 1075744; } } // The cloak thou wearest looks warm.
        public override int ResponseMessage { get { return 1075745; } } // Good tidings to thee. I live on scraps in the shadow of Lord British's Castle. I am so close to nothing, that surely, thou canst not help but see I live a humble life.
        public override int HintMessage { get { return 1075746; } } // One ~1_desire~ wilt make my life so much nicer.
        public override int TradeMessage { get { return 1075747; } } // I have no need for this ~1_gift~. For the right item, I would trade it.
        public override int ThanksMessage { get { return 1075748; } } // *gasp* A ~1_desire~, 'tis perfect. I doth have plans for this. Here, I have no need for this ~2_gift~ now.

        [Constructable]
        public Deirdre()
            : base("Deirdre", "the beggar")
        {
            Body = 0x191;
            AddItem(new PlainDress(Utility.RandomNeutralHue()));
        }

        public Deirdre(Serial serial)
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
    



	