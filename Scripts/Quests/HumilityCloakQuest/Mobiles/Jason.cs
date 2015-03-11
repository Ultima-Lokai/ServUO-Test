using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class Jason : MondainQuester
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

        string[] Jasonsay = new string[] // things to say while greeting 
		      { 

			 "Thou looketh like a fellow healer in that cloak.", //1075764

             "I am the sort of person who wandereth the countryside for weeks, my friend. All that time I spend healing good people who art in need of my services. All of this done without any reward, save the knowledge that I leadeth a life of virtue.",//1075765

             "I have my needs, however.  A cauldron would certainly help me.",//I have my needs, however.  A ~1_desire~ would certainly help me. 1075766

             "This stool was sent to me by mistake, I wouldest like to trade it for what I need.",//This ~1_gift~ was sent to me by mistake, I wouldest like to trade it for what I need. 1075767
		};

        [Constructable]
        public Jason()
            : base("Jason", "the healer")
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
                    SayRandom(Jasonsay, this);
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

        public Jason(Serial serial)
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
                if (dropped is VillageCauldron) 
                {
                    dropped.Delete();
                    mobile.AddToBackpack(new ShortStool()); //1075776
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "A cauldron! Many thanks to thee. Please accept this stool.", mobile.NetState);//A ~1_desire~! Many thanks to thee. Please accept this ~2_gift~. 1075768
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
    



	