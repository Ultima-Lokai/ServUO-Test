using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class Nelson : MondainQuester
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

        string[] Nelsonsay = new string[] // things to say while greeting 
		      { 

			 "I doth have a similar cloak.", //1075749

             "Greetings. I tend my flock and provide guidance to my fellow citizens. I seek not a life full of profit. For to strive for personal recognition or social position is not a worthy cause. A humble man is happy with his place.",//1075750

             "All that I needest is a mug.",//All that I needest is a ~1_desire~. 1075751

             "I useth this pair of gloves not at all. If ye bringeth me the right item, I would make a trade.",//I useth this ~1_gift~ not at all. If ye bringeth me the right item, I would make a trade. 1075752
		};

        [Constructable]
        public Nelson()
            : base("Nelson", "the shepherd")
        {
            Body = 0x190;
            AddItem(new Robe(Utility.RandomNeutralHue()));
            AddItem(new Sandals());
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m_Talked == false)
            {

                if (m.Player && m.FindItemOnLayer(Layer.Cloak) is PlainGreyCloak)

                if (m.InRange(this, 4))
                {
                    m_Talked = true;
                    SayRandom(Nelsonsay, this);
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

        public override bool OnDragDrop(Mobile from, Item dropped)
        {  
            Mobile m = from;
            PlayerMobile mobile = m as PlayerMobile;
            {
                if (dropped is FriendshipMug) 
                {
                    dropped.Delete();
                    mobile.AddToBackpack(new PairOfWorkGloves()); //1075780
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Ah, ‘tis a perfect mug. Please take this pair of gloves in trade.", mobile.NetState);//Ah, ‘tis a perfect ~1_desire~. Please take this ~2_gift~ in trade. 1075753
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
    



	