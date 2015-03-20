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
                    typeof (HumilityCloakQuest),
                    typeof(HumilityCloakQuestVesperMuseum)
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

        private static object[][] m_AnswerChoices = new object[][]
        {
            new object[] {1075679, 1075680, 1075681, 1075682},
            new object[] {1075684, 1075685, 1075686, 1075687}, 
            new object[] {1075689, 1075690, 1075691, 1075692},
            new object[] {1075694, 1075695, 1075696, 1075697},
            new object[] {1075699, 1075700, 1075701, 1075702},
            new object[] {1075704, 1075705, 1075706, 1075707},
            new object[] {1075709, 1075710, 1075711, 1075712}
        };

        private static object[] m_Questions = new object[] { 1075678, 1075683, 1075688, 1075693, 1075698, 1075703, 1075708 };

        private static object[] m_Answers = new object[] { 1075679, 1075685, 1075691, 1075697, 1075700, 1075705, 1075709 };

        public static object[][] AnswerChoices { get { return m_AnswerChoices; } }

        public static object[] Questions { get { return m_Questions; } }

        public static object[] Answers { get { return m_Answers; } }

        public static void GiveNextQuestion(PlayerMobile from, int index)
        {
            QuestionScroll scroll = new QuestionScroll(index, Questions[index], AnswerChoices[index],
                Answers[index], string.Format("Question #{0}", index + 1));
            scroll.LootType = LootType.Blessed;
            scroll.BlessedFor = from;

            if (!from.PlaceInBackpack(scroll))
                from.Drop(scroll, from.Location);
        }

        private BaseQuest getQuest(PlayerMobile pm, Type type)
        {
            foreach (BaseQuest quest in pm.Quests)
            {
                if (quest.GetType() == type)
                    return quest;
            }
            return null;
        }

        public override void OnDoubleClick(Mobile m)
        {
            if (m is PlayerMobile)
            {
                PlayerMobile pm = (PlayerMobile) m;
                Container pack = pm.Backpack;

                if (pack != null && !pack.Deleted)
                {
                    HumilityMarker marker =
                        (HumilityMarker) pack.FindItemByType(typeof (HumilityMarker));

                    BaseQuest hcQuest = getQuest(pm, typeof (HumilityCloakQuest));

                    if (hcQuest == null) // If they are not currently doing the Question-Answer part of the Quest
                    {
                        if (QuestHelper.FindCompletedQuest(pm, typeof (HumilityCloakQuest), false) && marker != null)
                        {
                            if (marker.Status == "wait before Vesper")
                            {
                                if (marker.DelayTime > DateTime.UtcNow)
                                {
                                    TimeSpan ts = marker.DelayTime - DateTime.UtcNow;
                                    SayTo(pm, 1042808, ts.Minutes.ToString());
                                    // You must rest ~1_MINUTES~ minutes before we set out on this journey.
                                }
                                else
                                {
                                    marker.Status = "vesper";
                                    base.OnDoubleClick(pm);
                                }
                                return;
                            }

                            if (marker.Status == "vesper")
                            {
                                base.OnDoubleClick(pm);
                                return;
                            }

                            if (marker.Status == "moonglow")
                            {
                                base.OnDoubleClick(pm);
                                return;
                            }

                            if (marker.Status == "britain")
                            {
                                base.OnDoubleClick(pm);
                                return;
                            }

                            if (marker.Status == "humble")
                            {
                                IronChain chain = (IronChain) pack.FindItemByType(typeof (IronChain));
                                if (chain != null)
                                {
                                    try
                                    {
                                        List<BaseQuest> quests = pm.Quests;
                                        foreach (BaseQuest quest in quests)
                                        {
                                            if (quest is HumilityCloakQuestFindTheHumble)
                                            {
                                                PlainGreyCloak cloak =
                                                    (PlainGreyCloak) pack.FindItemByType(typeof (PlainGreyCloak));
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
                                    catch
                                    {
                                    }

                                    return;

                                } // END OF -- if (chain != null)

                                base.OnDoubleClick(pm);
                                return;
                            }

                        }
                        else
                        {
                            Console.WriteLine("HumilityCloakQuest was not found!");
                            base.OnDoubleClick(pm);
                            return;
                        }

                    } // END OF -- if (hcQuest == null)
                    else
                    {
                        if (marker != null)
                        {

                            QuestionScroll answer = (QuestionScroll) pack.FindItemByType(typeof (QuestionScroll));
                            if (answer != null)
                            {
                                if (answer.ItemID == 0x14EE)
                                {
                                    if (answer.CorrectAnswerGiven)
                                    {
                                        if (answer.QuestionID < 6)
                                            // questions-answers for this quest are numbered 0 through 6
                                        {
                                            // EXAMPLE: if index is 0, they are on question 1, so next question is #2, so we add 2 here.
                                            marker.Status = string.Format("answering question #{0}",
                                                answer.QuestionID + 2);
                                            SayTo(pm, goodWords[Utility.Random(goodWords.Length)]);
                                            GiveNextQuestion(pm, answer.QuestionID + 1);
                                        }
                                        else
                                        {
                                            pm.SendGump(new HumilityQuesterGump(this, 1075714));
                                            marker.Status = "wait before Vesper";
                                            marker.DelayTime = DateTime.UtcNow + TimeSpan.FromHours(1.0);
                                            hcQuest.RemoveQuest(true);
                                        }
                                        answer.Delete();
                                        return;
                                    }
                                    else
                                    {
                                        SayTo(pm, 1075713);
                                        hcQuest.RemoveQuest(true);
                                        marker.Delete();
                                    }
                                    answer.Delete();
                                }
                                else
                                {
                                    SayTo(pm, "Try to answer the question before speaking to me again.");
                                    return;
                                }
                            } // END OF -- if (answer != null)

                        } // END OF -- if (marker != null)

                    } // END OF -- else

                } // END OF -- if (pack != null && !pack.Deleted)

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
