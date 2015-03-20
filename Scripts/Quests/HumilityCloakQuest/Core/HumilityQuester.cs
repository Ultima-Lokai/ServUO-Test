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
            if (m.InLOS(this) && m.InRange(oldLocation, 5))
            {
                PlainGreyCloak cloak = (PlainGreyCloak) m.FindItemOnLayer(Layer.Cloak);
                if (m.Player && cloak != null)
                    cloak.Interact(this, QuesterID, "greet");
            }
        }

        public void OnGumpClose(Mobile m)
        {
            PlainGreyCloak cloak = (PlainGreyCloak)m.FindItemOnLayer(Layer.Cloak);
            if (m.Player && cloak != null && Utility.Random(20) > 12)
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
                Container pack = mobile.Backpack;
                if (pack == null)
                {
                    pack = new Backpack();
                    mobile.EquipItem(pack);
                }
                
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
                        if (this is Sean)
                        {
                            HumilityMarker marker =
                                (HumilityMarker) pack.FindItemByType(typeof (HumilityMarker));
                            if (marker == null)
                            {
                                marker = new HumilityMarker("sean");
                                mobile.AddToBackpack(marker);
                            }
                            mobile.SendGump(new HumilityQuesterGump(this, ThanksMessage, cloak.Desires[QuesterID].DesireName));
                            mobile.AddToBackpack(cloak);
                        }
                        else
                            mobile.SendGump(new HumilityQuesterGump(this, ThanksMessage, cloak.Desires[QuesterID].DesireName + "\t" + cloak.Desires[QuesterID].OfferName));
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
}
