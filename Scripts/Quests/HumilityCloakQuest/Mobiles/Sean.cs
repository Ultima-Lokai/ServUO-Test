
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class Sean : HumilityQuester
    {
        public override int QuesterID { get { return 6; } }
        public override int GreetingMessage { get { return 1075769; } } // That grey cloak is very nice.
        public override int ResponseMessage { get { return 1075770; } } // Greetings to thee, friend! Art thou on some sort of quest? Ye have that look about ye, and that cloak looks somewhat familiar. Ah, no matter. A break from my blacksmithing work is always welcome! I canst only talk for a little while though, there are a few things I promised to have done for the township today. After all, a community is much like a long chain, and we can only be as stronger as our weakest link!
        public override int HintMessage { get { return 1075771; } } // I do have a humble desire or two, though. I seem to have trouble finding a ~1_desire~.
        public override int TradeMessage { get { return 0; } }
        public override int ThanksMessage { get { return 1075772; } } // //Wonderul!  A ~1_desire~!  Surely thou hast gone through much trouble to bring this for me. Please take this iron chain that I made for Gareth. ‘Tis something we once talked of for some time, and he had suggested a new method of metalworking that I have only just accomplished. 

        [Constructable]
        public Sean()
            : base("Sean", "the blacksmith")
        {
            Body = 0x190;
            Female = false;
            AddItem(new Shirt(Utility.RandomNeutralHue()));
            AddItem(new LongPants(Utility.RandomNeutralHue()));
            AddItem(new FullApron(Utility.RandomNeutralHue()));
            AddItem(new Boots());

            Utility.AssignRandomHair(this);
            Hue = Utility.RandomSkinHue();
        }

        public Sean(Serial serial)
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

        /* public override bool OnDragDrop(Mobile from, Item dropped)
        {
            Mobile m = from;
            PlayerMobile mobile = m as PlayerMobile;
            {
                if (dropped is ShortStool)
                {
                    dropped.Delete();
                    mobile.AddToBackpack(new IronChain());//1075788
                    mobile.AddToBackpack(new HumilityMarker());
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Wonderul!  A stool!  Surely thou hast gone through much trouble to bring this for me. Please take this iron chain that I made for Gareth. ‘Tis something we once talked of for some time, and he had suggested a new method of metalworking that I have only just accomplished.", mobile.NetState);
                    from.SendMessage("For your good deed you are awarded a little karma.");
                    from.Karma += 50;
                    return true;

                }
                else if (dropped.LootType == LootType.Blessed || dropped.LootType == LootType.Newbied || dropped.Insured)
                {
                    from.SendMessage("You cannot offer blessed, newbied, or insured items");
                    return false;
                }
                else
                {
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I have no need for this...", mobile.NetState);
                }
            }
            return false;
        } */
    }
}
    



	