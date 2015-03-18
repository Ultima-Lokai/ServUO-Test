using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class Gareth : MondainQuester
    {
        public override Type[] Quests
        {
            get
            {
                return new Type[]
                {
                    typeof (HumilityCloakQuest)
                };
            }
        }

        [Constructable]
        public Gareth()
            : base("Gareth", "Emissary of the RBC")
        {
        }

        public override void Advertise()
        {
            this.Say(1075674); // Hail! Care to join our efforts for the Rise of Britannia?
        }

        public Gareth(Serial serial)
            : base(serial)
        {
        }

        private string[] goodWords = new string[] { "Good. Now try this one...", "Hmm...let me give you another question.", "Good answer. How about this one?", 
            "That's right. Try another...", "Let's see if you know this one.", "What do you think of this one?", "OK. Now how would answer this?" };

        public override void OnDoubleClick(Mobile m)
        {
            if (m is PlayerMobile)
            {
                PlayerMobile pm = (PlayerMobile)m;
                Container pack = pm.Backpack;
                if (pack != null && !pack.Deleted)
                {
                    HumilityMarker marker =
                        (HumilityMarker)pack.FindItemByType(typeof(HumilityMarker));
                    if (marker == null)
                    {
                        base.OnDoubleClick(m);
                        return;
                    }

                    QuestionScroll answer = (QuestionScroll)pack.FindItemByType(typeof(QuestionScroll));
					if (answer != null)
					{
                        if (answer.ItemID == 0x14EE)
                        {
                            if (answer.CorrectAnswerGiven)
                            {
                                if (answer.QuestionID < 6) // questions-answers for this quest are numbered 0 through 6
                                {
                                    marker.Status = string.Format("answering question #{0}", answer.QuestionID + 2); // if index is 0, they are on question 1, so next question is #2.
                                    pm.SendMessage(goodWords[Utility.Random(goodWords.Length)]);
                                    answer.Quest.GiveNextQuestion(pm, answer.QuestionID + 1);
                                    answer.Delete();
                                    return;
                                }
                                else ((HumilityCloakQuest)answer.Quest).GiveRewards();
                            }
                            else
                            {
                                pm.SendLocalizedMessage(1075713);
                                ((HumilityCloakQuest)answer.Quest).RemoveQuest();
                                marker.Delete();
                            }
                            answer.Delete();
                        }
                        else
                            SayTo(pm, "Try to answer the question before speaking to me again.");
					}
					IronChain chain = (IronChain)pack.FindItemByType(typeof(IronChain));
					if (chain != null)
					{
                        try
                        {
                            List<BaseQuest> quests = pm.Quests;
                            foreach (BaseQuest quest in quests)
                            {
                                if (quest is HumilityCloakQuestFindTheHumble)
                                {
                                    PlainGreyCloak cloak = (PlainGreyCloak)pack.FindItemByType(typeof(PlainGreyCloak));
                                    if (cloak != null)
                                    {
                                        marker.Status = "testing";
                                        pm.SendGump(new HumilityRewardGump(this, marker, cloak, chain));
                                    }
                                    else
                                        SayTo(pm, "Speak to me when you have the cloak in your pack also.");
                                }
                            }
                        }
                        catch { }
                        return;
					}
                }
            }
            base.OnDoubleClick(m);
        }

        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Female = false;
            Race = Race.Human;

            Hue = 0x841C;
            HairItemID = 0x203C;
            HairHue = 0xF7;
        }

        public override void InitOutfit()
        {
            AddItem(new LongPants(642));
            AddItem(new FancyShirt(89));
            AddItem(new Boots());
            AddItem(new BodySash(97));

            PackGold(100, 200);
            Blessed = true;
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
