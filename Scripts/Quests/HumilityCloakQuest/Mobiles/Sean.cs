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
    public class Sean: BaseCreature
    {
        private DateTime recoverDelay;
        private static bool m_Talked;  

        string[] Seansay = new string[] // things to say while greeting 
		      { 

			 "That grey cloak is very nice.", //1075769

             "Greetings to thee, friend! Art thou on some sort of quest? Ye have that look about ye, and that cloak looks somewhat familiar. Ah, no matter. A break from my blacksmithing work is always welcome! I canst only talk for a little while though, there are a few things I promised to have done for the township today. After all, a community is much like a long chain, and we can only be as stronger as our weakest link!",//1075770

             "I do have a humble desire or two, though. I seem to have trouble finding a stool.",//I do have a humble desire or two, though. I seem to have trouble finding a ~1_desire~. 1075771
		};

        [Constructable]
        public Sean()
            : base(AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4)
        {
                Body = 0x190;
                Name = "Sean";         
                Title = "the Blacksmith";
                Blessed = true;

            AddItem(new Shirt(Utility.RandomNeutralHue()));
            AddItem(new LongPants(Utility.RandomNeutralHue()));
            AddItem(new FullApron(Utility.RandomNeutralHue()));
            AddItem(new ThighBoots());

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
                    SayRandom(Seansay, this);
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

        public override bool OnDragDrop(Mobile from, Item dropped)
        {  
            Mobile m = from;
            PlayerMobile mobile = m as PlayerMobile;
            {
                if (dropped is ShortStool) 
                {
                    dropped.Delete();
                    mobile.AddToBackpack(new IronChain());//1075788
                    mobile.AddToBackpack(new HumilityMarker());
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Wonderul!  A stool!  Surely thou hast gone through much trouble to bring this for me. Please take this iron chain that I made for Gareth. ‘Tis something we once talked of for some time, and he had suggested a new method of metalworking that I have only just accomplished.", mobile.NetState);//Wonderul!  A ~1_desire~!  Surely thou hast gone through much trouble to bring this for me. Please take this iron chain that I made for Gareth. ‘Tis something we once talked of for some time, and he had suggested a new method of metalworking that I have only just accomplished. 1075772
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
    



	