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
    public class Maribel : BaseCreature
    {
        private DateTime recoverDelay;
        private static bool m_Talked;  

        string[] Maribelsay = new string[] // things to say while greeting 
		      { 

			 "You, in the grey cloak, art thou hungry?", //1075754

             "I feedeth any who come here with the means to pay.  Be they noblemen or commoners, peaceful or aggressive, artist or barbarian, tis not my place to judge. I believeth there is value in everyone, and thus serve all.",//1075755

             "All that I wilt ask for, is a hammer.",//All that I wilt ask for, is a ~1_desire~. 1075756

             "This skillet was given to me and I cannot use it. I wilt happily trade it for the right item.",//This ~1_gift~ was given to me and I cannot use it. I wilt happily trade it for the right item. 1075757
		};

        [Constructable]
        public Maribel()
            : base(AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4)
        {
                Body = 0x191;
                Name = "Maribel";
                Title = "the Waitress";
                Blessed = true;

            AddItem(new FancyDress(Utility.RandomNeutralHue()));
            AddItem(new Shoes(Utility.RandomNeutralHue()));
            //AddItem(new HalfApron(Utility.RandomNeutralHue()));

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
                    SayRandom(Maribelsay, this);
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

        public override bool OnDragDrop(Mobile from, Item dropped)
        {  
            Mobile m = from;
            PlayerMobile mobile = m as PlayerMobile;
            {
                if (dropped is WornHammer) 
                {
                    dropped.Delete();
                    mobile.AddToBackpack(new SeasonedSkillet()); //1075774
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Thanks to thee! This hammer is just right. Here, this skillet is for thee.", mobile.NetState);//Thanks to thee! This ~1_desire~ is just right. Here, this ~2_gift~ is for thee. 1075758
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
    



	