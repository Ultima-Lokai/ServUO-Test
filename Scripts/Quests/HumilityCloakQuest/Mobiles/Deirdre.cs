using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class Deirdre : MondainQuester
    {
        public override Type[] Quests
        {
            get
            {
                return new Type[]
                {
                    typeof (HumilityCloakQuestFindTheHumble)
                };
            }
        }

        private DateTime recoverDelay;
        private static bool m_Talked;  

        string[] Deirdresay = new string[] // things to say while greeting 
		      { 

			 "The cloak thou wearest looks warm.", //1075744

             "Good tidings to thee. I live on scraps in the shadow of Lord British's Castle. I am so close to nothing, that surely, thou canst not help but see I live a humble life.",//1075745

             "One ring wilt make my life so much nicer.",//One ~1_desire~ wilt make my life so much nicer. 1075746

             "I have no need for this mug. For the right item, I would trade it.",//I have no need for this ~1_gift~. For the right item, I would trade it. 1075747
		};

        [Constructable]
        public Deirdre()
            : base("Deirdre", "the beggar")
        {
            Body = 0x191;
            AddItem(new PlainDress(Utility.RandomNeutralHue()));
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m_Talked == false)
            {

                if (m.Player && m.FindItemOnLayer(Layer.Cloak) is PlainGreyCloak)

                if (m.InRange(this, 4))
                {
                    m_Talked = true;
                    SayRandom(Deirdresay, this);
                    this.Move(GetDirectionTo(m.Location));
                 
                    SpamTimer t = new SpamTimer();
                    t.Start();
                }
            }
        }

        private class SpamTimer : Timer
        {
            public SpamTimer()
                : base(TimeSpan.FromSeconds(10))
            {
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                m_Talked = false;
            }
        }

        private static void SayRandom(string[] say, Mobile m)
        {
            m.Say(say[Utility.Random(say.Length)]);
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

        public override bool OnDragDrop(Mobile from, Item dropped)
        {  
            Mobile m = from;
            PlayerMobile mobile = m as PlayerMobile;
            {
                if (dropped is BrassRing) 
                {
                    dropped.Delete();
                    mobile.AddToBackpack(new FriendshipMug()); //1075777
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "*gasp* A ring, 'tis perfect. I doth have plans for this. Here, I have no need for this mug now.", mobile.NetState);//*gasp* A ~1_desire~, 'tis perfect. I doth have plans for this. Here, I have no need for this ~2_gift~ now. 1075748
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
                }
            }
        }
    



	