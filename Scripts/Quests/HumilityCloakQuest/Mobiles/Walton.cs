using System;
using Server.Gumps;
using Server.Items;
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Misc;
using Server.Engines.Quests;

namespace Server.Mobiles
{
    [CorpseName("a human's corpse")]
    public class Walton : BaseCreature
    {
        private DateTime recoverDelay;
        private static bool m_Talked;  

        string[] Waltonsay = new string[] // things to say while greeting 
		      { 

			 "A horse blanket would offer more comfort than thine cloak, mayhaps.", //1075739

             "Hello friend. Yes, I work with yon horses each day. I carry bails of hay, feed, and even shovel manure for those beautiful beasts of burden. Everyone who rideth my animals enjoy their temperament and health. Yet, I do not ask for any recognition.",//1075740

             "All I need is but a simple pair of gloves.",//All I need is but a simple ~1_desire~. 1075741

             "If I ever receive that which I need, I wilt gladly trade it for this old hammer.",//If I ever receive that which I need, I wilt gladly trade it for this ~1_gift~. 1075742
		};

        [Constructable]
        public Walton()
            : base(AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4)
        {
                Body = 0x190;
                Name = "Walton";
                Title = "the Horse Trainer";
                Blessed = true;

            AddItem(new Shirt(Utility.RandomNeutralHue()));
            AddItem(new LongPants(Utility.RandomNeutralHue()));
            AddItem(new HalfApron(Utility.RandomNeutralHue()));
            AddItem(new Boots());

            Utility.AssignRandomHair(this);
            Hue = Utility.RandomSkinHue();
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m_Talked == false)
            {

                if (m.Player && m.FindItemOnLayer(Layer.Cloak) is PlainGreyCloak)
 
                if (m.InRange(this, 4))
                {
                    m_Talked = true;
                    SayRandom(Waltonsay, this);
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

        public override bool OnDragDrop(Mobile from, Item dropped)
        {  
            Mobile m = from;
            PlayerMobile mobile = m as PlayerMobile;
            {
                if (dropped is PairOfWorkGloves) 
                {
                    dropped.Delete();
                    mobile.AddToBackpack(new WornHammer()); //1075779
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Ah, thank you! This pair of work gloves is just what I needed. Please taketh this worn hammer - I hope it will be of use to thee.", mobile.NetState);//Ah, thank you! This ~1_desire~ is just what I needed. Please taketh this ~2_gift~ - I hope it will be of use to thee. 1075743
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
    



	