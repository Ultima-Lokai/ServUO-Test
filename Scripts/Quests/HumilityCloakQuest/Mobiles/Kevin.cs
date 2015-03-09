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
    public class Kevin : BaseCreature
    {
        private DateTime recoverDelay;
        private static bool m_Talked;  

        string[] Kevinsay = new string[] // things to say while greeting 
		      { 

			 "Art thou hiding under that cloak?", //1075759

             "Greetings, friend. Didst thou know I work all day, preparing and storing all sorts of meat? My cleaver is not dainty or at all particular, but if ye bringeth something to me then I will render it useful for food. Every creature can be thought of as useful in life... or in death. Death comes to us all, my friend. I hath learned that, for certain, in this humble profession.",//1075760

             "Speaking of useful, if ye findeth me a nice skillet, I wilt be grateful.",//Speaking of useful, if ye findeth me a nice ~1_desire~, I wilt be grateful. 1075761

             "I received this cauldron as a gift. I hath no need for it, but wert ye to bring me something interesting, I would trade it, perhaps.",//I received this ~1_gift~ as a gift. I hath no need for it, but wert ye to bring me something interesting, I would trade it, perhaps. 1075762
		};

        [Constructable]
        public Kevin()
            : base(AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4)
        {
                Body = 0x190;
                Name = "Kevin";
                Title = "the Butcher";
                Blessed = true;

            AddItem(new FancyShirt(Utility.RandomNeutralHue()));
            AddItem(new ShortPants(Utility.RandomNeutralHue()));
            AddItem(new HalfApron(Utility.RandomNeutralHue()));
            AddItem(new Shoes());

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
                    SayRandom(Kevinsay, this);
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

        public override bool OnDragDrop(Mobile from, Item dropped)
        {  
            Mobile m = from;
            PlayerMobile mobile = m as PlayerMobile;
            {
                if (dropped is SeasonedSkillet) 
                {
                    dropped.Delete();
                    mobile.AddToBackpack(new VillageCauldron()); //1075775
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Thou broughtest me a skillet That will do nicely. Here, take this cauldron as thanks.", mobile.NetState);//Thou broughtest me a ~1_desire~! That will do nicely. Here, take this ~2_gift~ as thanks. 1075763
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
    



	