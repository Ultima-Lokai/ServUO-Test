using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class Maribel : MondainQuester
    {
        public override int GreetingMessage { get { return 1075754; } } // You, in the grey cloak, art thou hungry?
        public override int ResponseMessage { get { return 1075755; } } // I feedeth any who come here with the means to pay.  Be they noblemen or commoners, peaceful or aggressive, artist or barbarian, tis not my place to judge. I believeth there is value in everyone, and thus serve all.
        public override int HintMessage { get { return 1075756; } } // All that I wilt ask for, is a ~1_desire~.
        public override int TradeMessage { get { return 1075757; } } // This ~1_gift~ was given to me and I cannot use it. I wilt happily trade it for the right item.
        public override int ThanksMessage { get { return 1075758; } } // Thanks to thee! This ~1_desire~ is just right. Here, this ~2_gift~ is for thee. 


        [Constructable]
        public Maribel()
            : base("Maribel", "the waitress")
        {
            Body = 0x191;
            AddItem(new HalfApron(Utility.RandomNeutralHue()));
        }

        public Maribel(Serial serial)
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
    



	