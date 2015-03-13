using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Gumps;
using Server.Items;
using Server.Network;

namespace Server.Engines.Quests
{

    public abstract class HumilityQuester : MondainQuester
    {
        public override Type[] Quests { get { return new Type[] { typeof(HumilityCloakQuestFindTheHumble) }; } }

        public virtual int QuesterID { get { return -1; } }
        public virtual int GreetingMessage { get { return 0; } }
        public virtual int ResponseMessage { get { return 0; } }
        public virtual int HintMessage { get { return 0; } }
        public virtual int TradeMessage { get { return 0; } }
        public virtual int ThanksMessage { get { return 0; } }


        public HumilityQuester()
            : base(null)
        {
        }

        public HumilityQuester(string name)
            : this(name, null)
        {
        }

        public HumilityQuester(string name, string title)
            : base(name, title)
        {
        }

        public HumilityQuester(Serial serial)
            : base(serial)
        {
        }

        public override void InitBody()
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            AddNameProperties(list);
        }

        public override bool PropertyTitle
        {
            get { return true; }
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            PlainGreyCloak cloak = (PlainGreyCloak) m.FindItemOnLayer(Layer.Cloak);
            if (m.Player && cloak != null)
                cloak.Interact(this, QuesterID, "greet");
        }

        public void OnGumpClose(Mobile m)
        {
            PlainGreyCloak cloak = (PlainGreyCloak)m.FindItemOnLayer(Layer.Cloak);
            if (m.Player && cloak != null && Utility.Random(20) > 14)
            {
                if (Utility.RandomBool())
                    cloak.Interact(this, QuesterID, "hint");
                else
                    cloak.Interact(this, QuesterID, "trade");
            }
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            PlayerMobile mobile = from as PlayerMobile;
            if (mobile != null)
            {
                PlainGreyCloak cloak = (PlainGreyCloak)mobile.FindItemOnLayer(Layer.Cloak);
                if (cloak != null)
                {
                    Type desireType = cloak.Desires[QuesterID].DesireType;
                    Type offerType = cloak.Desires[QuesterID].OfferType;

                    if (dropped.GetType() == desireType)
                    {
                        dropped.Delete();
                        Item item = (Item) Activator.CreateInstance(offerType);
                        mobile.AddToBackpack(item);
                        SayTo(mobile, ThanksMessage,
                            cloak.Desires[QuesterID].DesireName + "\t" + cloak.Desires[QuesterID].OfferName);
                        return true;

                    }
                    else
                    {
                        mobile.SendGump(new HumilityQuesterGump(this));
                        PrivateOverheadMessage(MessageType.Regular, 1153, false, "I have no need of this...",
                            mobile.NetState);
                    }
                }
            }
            return base.OnDragDrop(from, dropped);
        }

        public override void OnTalk(PlayerMobile player)
        {
            PlainGreyCloak cloak = (PlainGreyCloak)player.FindItemOnLayer(Layer.Cloak);
            if (cloak != null)
            {
                player.SendGump(new HumilityQuesterGump(this));
            }
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

    public class HumilityQuesterGump : BaseQuestGump
    {
        private HumilityQuester mQuester;
        public HumilityQuesterGump(HumilityQuester quester)
            : base(75, 25)
        {
            mQuester = quester;
            string name = quester.Name + " " + quester.Title;
            int response = quester.ResponseMessage;

            Closable = true;

            AddPage(0);

            AddImageTiled(50, 20, 400, 400, 2624);
            AddAlphaRegion(50, 20, 400, 400);

            AddImage(90, 33, 9005);
            AddHtmlObject(130, 45, 270, 20, name, White, false, false); // Name
            AddImageTiled(130, 65, 175, 1, 9101);

            AddHtmlObject(120, 190, 280, 120, response, White, false, false); // Response

            AddButton(340, 390, 247, 248, 1, GumpButtonType.Reply, 0);

            AddImageTiled(50, 29, 30, 390, 10460);
            AddImageTiled(34, 140, 17, 279, 9263);

            AddImage(48, 135, 10411);
            AddImage(-16, 285, 10402);
            AddImage(0, 10, 10421);
            AddImage(25, 0, 10420);

            AddImageTiled(83, 15, 350, 15, 10250);

            AddImage(34, 419, 10306);
            AddImage(442, 419, 10304);
            AddImageTiled(51, 419, 392, 17, 10101);

            AddImageTiled(415, 29, 44, 390, 2605);
            AddImageTiled(415, 29, 30, 390, 10460);
            AddImage(425, 0, 10441);

            AddImage(370, 50, 1417);
            AddImage(379, 60, 0x15A9);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            if (from != null && !from.Deleted)
                mQuester.OnGumpClose(from);
        }
    }
}
