using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Engines.Quests
{
    public class HumilityCloakQuest : BaseQuest
    {
        /* Greetings my friend! My name is Gareth, and I represent a group of citizens who wish to rejuvenate interest in our kingdom's noble heritage. 
         * 'Tis our belief that one of Britannia's greatest triumphs was the institution of the Virtues, neglected though they be now. To that end I 
         * have a set of tasks prepared for one who would follow a truly Humble path. Art thou interested in joining our effort? */
        public override object Description { get { return 1075675; } }

        /* Very good! I can see that ye hath more than just a passing interest in our work. There are many trials before thee, but I have every hope 
         * that ye shall have the diligence and fortitude to carry on to the very end. Before we begin, please prepare thyself by thinking about the 
         * virtue of Humility. Ponder not only its symbols, but also its meanings. Once ye believe that thou art ready, speak with me again. */
        public override object Complete { get { return 1075716; } }

        /* Ah... no, that is not quite right. Truly, Humility is something that takes time and experience to understand. I wish to challenge thee to 
         * seek out more knowledge concerning this virtue, and tomorrow let us speak again about what thou hast learned.<br> */
        //public override object Uncomplete { get { return base.Uncomplete; } }

        /* I wish that thou wouldest reconsider. */
        public override object Refuse { get { return 1075677; } }

        public override QuestChain ChainID { get { return QuestChain.HumilityCloak; } }
        public override Type NextQuest { get { return typeof(HumilityCloak1Quest); } }
        public override bool DoneOnce { get { return false; } }
        public override TimeSpan RestartDelay { get { return TimeSpan.FromDays(1.0); } }

        public HumilityCloakQuest()
        {
            AddObjective(new AnswerObjective(1075678, new int[] { 1075679, 1075680, 1075681, 1075682 }, 1075679));
            AddObjective(new AnswerObjective(1075683, new int[] { 1075684, 1075685, 1075686, 1075687 }, 1075685));
            AddObjective(new AnswerObjective(1075688, new int[] { 1075689, 1075690, 1075691, 1075692 }, 1075691));
            AddObjective(new AnswerObjective(1075693, new int[] { 1075694, 1075695, 1075696, 1075697 }, 1075697));
            AddObjective(new AnswerObjective(1075698, new int[] { 1075699, 1075700, 1075701, 1075702 }, 1075700));
            AddObjective(new AnswerObjective(1075703, new int[] { 1075704, 1075705, 1075706, 1075707 }, 1075705));
            AddObjective(new AnswerObjective(1075708, new int[] { 1075709, 1075710, 1075711, 1075712 }, 1075709));
        }

        public override void OnAccept()
        {
            Owner.SendLocalizedMessage(1075676);
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
    public class HumilityCloak1Quest : BaseQuest
    {
        public override QuestChain ChainID { get { return QuestChain.HumilityCloak; } }
        public override Type NextQuest { get { return typeof(HumilityCloak2Quest); } }
        public override bool DoneOnce { get { return true; } }

        /* Community Service - Museum */
        public override object Title { get { return 1075716; } }

        /*'Tis time to help out the community of Britannia. Visit the Vesper Museum and donate
         * to their collection, and eventually thou wilt be able to receive a replica of the 
         * Shepherd's Crook of Humility. Once ye have it, return to me. Art thou willing to do this? */
        public override object Description { get { return 1075717; } }

        /* I wish that thou wouldest reconsider. */
        public override object Refuse { get { return 1075719; } }

        /* Hello my friend. The museum sitteth in southern Vesper. If ye go downstairs, ye will 
         * discover a small donation chest. That is the place where ye should leave thy donation. */
        public override object Uncomplete { get { return 1075720; } }

        /* Terrific! The Museum is a worthy cause. Many will benefit from the inspiration and learning
         * that thine donation hath supported. */
        public override object Complete { get { return 1075721; } }

        public HumilityCloak1Quest()
            : base()
        {
            AddObjective(new ObtainObjective(typeof(HumilityCrookReplica), "A Replica of the Shepherd's Crook of Humility", 1, 0x0E82));
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

    public class HumilityCloak2Quest : BaseQuest
    {
        public override QuestChain ChainID { get { return QuestChain.HumilityCloak; } }
        public override Type NextQuest { get { return typeof(HumilityCloak3Quest); } }
        public override bool DoneOnce { get { return true; } }

        /* Community Service – Zoo */
        public override object Title { get { return 1075722; } }

        /* Now, go on and donate to the Moonglow Zoo. Givest thou enough to receive a 
         * 'For the Life of Britannia' sash. Once ye have it, return it to me. Wilt thou continue?*/
        public override object Description { get { return 1075723; } }

        /* I wish that thou wouldest reconsider. */
        public override object Refuse { get { return 1075725; } }

        /* Hello again. The zoo lies a short ways south of Moonglow. Close to the entrance thou wilt 
         * discover a small donation chest. That is where thou shouldest leave thy donation. */
        public override object Uncomplete { get { return 1075726; } }

        /* Wonderful! The Zoo is a very special place from which people young and old canst benefit. 
         * Thanks to thee, it can continue to thrive. */
        public override object Complete { get { return 1075727; } }

        public HumilityCloak2Quest()
            : base()
        {
            AddObjective(new ObtainObjective(typeof(ForLifeBritanniaSash), "For the Life of Britannia Sash", 1, 0x1542));
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

    public class HumilityCloak3Quest : BaseQuest
    {
        public override QuestChain ChainID { get { return QuestChain.HumilityCloak; } }
        public override Type NextQuest { get { return typeof(HumilityCloak4Quest); } }
        public override bool DoneOnce { get { return true; } }

        /* Community Service – Library */
        public override object Title { get { return 1075728; } }

        /* I have one more charity for thee, my diligent friend. Go forth and donate to
         * the Britain Library and do that which is necessary to receive a special printingof ‘Virtue’, 
         * by Lord British. Once in hand, bring the book back with ye. Art thou ready? */
        public override object Description { get { return 1075729; } }

        /* I wish that thou wouldest reconsider. */
        public override object Refuse { get { return 1075731; } }

        /* Art thou having trouble? The Library lieth north of Castle British's gates. I believe the 
         * representatives in charge of the donations are easy enough to find. They await thy visit, 
         * amongst the many tomes of knowledge. */
        public override object Uncomplete { get { return 1075732; } }

        /* Very good! The library is of great import to the people of Britannia. Thou hath done a 
         * worthy deed and this is thy last required donation. I encourage thee to continue contributing
         * to thine community, beyond the obligations of this endeavor. */
        public override object Complete { get { return 1075733; } }

        public HumilityCloak3Quest()
            : base()
        {
            AddObjective(new ObtainObjective(typeof(SpecialPrintingOfVirtue), "Special Printing of 'Virtue' by Lord British", 1, 0x0FEF));

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

    public class HumilityCloak4Quest : BaseQuest
    {
        public override QuestChain ChainID { get { return QuestChain.HumilityCloak; } }
        public override Type NextQuest { get { return typeof(HumilityCloak5Quest); } }
        public override bool DoneOnce { get { return true; } }

        /* Who's Most Humble */
        public override object Title { get { return 1075734; } }

        /* Thou art challenged to find seven citizens spread out among the towns of Britannia: 
         * Skara Brae, Minoc, Britain, and one of the towns upon an isle at sea. Each citizen 
         * wilt reveal some thought concerning Humility. But who doth best exemplify the virtue? 
         * Here, thou needeth wear this plain grey cloak, for they wilt know ye by it. Wilt thou continue? */
        public override object Description { get { return 1075735; } }

        /* 'Tis a difficult quest, but well worth it. Wilt thou reconsider? */
        public override object Refuse { get { return 1075737; } }

        /* There art no less than seven 'humble citizens' spread across the Britannia proper. I know that they
         * can be found in the towns of Minoc, Skara Brae and Britain. Another may be upon an island at sea, 
         * the name of which escapes me at the moment. Thou needeth visit all seven to solve the puzzle. Be 
         * diligent, for they have a tendency to wander about.<BR><BR><br>Dost thou wear the plain grey cloak? */
        public override object Uncomplete { get { return 1075738; } }

        /* Aha! Yes, this is exactly what I was looking for. What think ye of Sean? Of all those I have met, he
         * is the least concerned with others' opinions of him. He excels at his craft, yet always believes he 
         * has something left to learn. *looks at the iron chain necklace* And it shows, does it not? */
        public override object Complete { get { return 1075773; } }

        public HumilityCloak4Quest()
            : base()
        {
            AddObjective(new ObtainObjective(typeof(IronChain), "Iron Chain", 1, 0x1085)); //1075788
        }

        public override void OnAccept()
        {
            base.OnAccept();
            Owner.SendLocalizedMessage(1075736);
            Owner.AddToBackpack(new BrassRing());
            Owner.AddToBackpack(new PlainGreyCloak());
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

    public class HumilityCloak5Quest : BaseQuest
    {
        public override QuestChain ChainID { get { return QuestChain.HumilityCloak; } }
        public override bool DoneOnce { get { return true; } }

        /* Test of Humility */
        public override object Title { get { return 1075781; } }

        /* Noble friend, thou hast performed tremendously! On behalf of the Rise of Britannia I wish to reward
         * thee with this golden shield, a symbol of accomplishment and pride for the many things that thou hast
         * done for our people.<BR><BR><br>Dost thou accept? */
        public override object Description { get { return 1075782; } }

        /* *smiles* I understandeth thy feelings, friend. Ye shall remain anonymous then, to all those who shall 
         * benefit from thine efforts.<BR><br>Yet, through all these trials, perhaps thou hast come a little closer
         * to understanding the true nature of Humility.<br>Thine efforts might seem small compared to the great 
         * world in which we live, but as more of our people work together, stronger shall our people be.<BR><BR>
         * <br>I wish for ye to keep the cloak I gave thee earlier. Thou canst do with it what thou will, but I hope 
         * that it shall serve as a reminder of the days ye spent engaged in our simple cause.<br>And although I have
         * nothing more for thee, I wouldest exhort ye to continue upon this path, always seeking opportunities to 
         * humble thyself to the demands of the world.<br>There is a small island to the south upon which lies the 
         * Shrine of Humility. Seek solace there, and perhaps the answers to thine questions will become clear. */
        public override object Refuse { get { return 1075784; } }

        /* *smiles* Surely thy deeds will be recognized by those who see thee wearing this shield! It shall serve
         * as a reminder of the exalted path that thou hast journeyed upon, and I wish to thank thee on behalf of 
         * all whom thine efforts shall benefit. Ah, let us not forget that old cloak I gavest thee - I shall take 
         * it back now and give thee thine reward. */
        public override object Uncomplete { get { return 1075783; } }

        /* Aha! Yes, this is exactly what I was looking for. What think ye of Sean? Of all those I have met, he
         * is the least concerned with others' opinions of him. He excels at his craft, yet always believes he 
         * has something left to learn. *looks at the iron chain necklace* And it shows, does it not? */
        public override object Complete { get { return 1075773; } }

        public HumilityCloak5Quest()
            : base()
        {
            AddObjective(new ObtainObjective(typeof(PlainGreyCloak), "A Plain Grey Cloak", 1, 0x1085)); //1075789


            AddReward(new BaseReward(typeof(ShieldOfRecognition), 1075851)); // Shield of Recognition
        }

       /* public override void OnAccept()
        {
            base.OnAccept();
            Owner.SendLocalizedMessage(1075736);
            Owner.AddToBackpack(new BrassRing());
            Owner.AddToBackpack(new PlainGreyCloak());
        }*/

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

    public class AnswerObjective : ObtainObjective
    {
        private string m_QuestionString;
        private int m_QuestionNumber;
        private string[] m_AnswerStrings;
        private int[] m_AnswerNumbers;
        private string m_CorrectString;
        private int m_CorrectNumber;

        public string QuestionString { get { return m_QuestionString; } set { m_QuestionString = value; } }
        public int QuestionNumber { get { return m_QuestionNumber; } set { m_QuestionNumber = value; } }
        public string[] AnswerStrings { get { return m_AnswerStrings; } set { m_AnswerStrings = value; } }
        public int[] AnswerNumbers { get { return m_AnswerNumbers; } set { m_AnswerNumbers = value; } }
        public string CorrectString { get { return m_CorrectString; } set { m_CorrectString = value; } }
        public int CorrectNumber { get { return m_CorrectNumber; } set { m_CorrectNumber = value; } }

        public AnswerObjective(int question, int[] answers, int correctAnswer)
            : this(string.Empty, question, null, answers, string.Empty, correctAnswer)
        {
        }

        public AnswerObjective(string question, string[] answers, string correctAnswer)
            : this(question, 0, answers, null, correctAnswer, 0)
        {
        }

        public AnswerObjective(string questionString, int questionNumber, string[] answerStrings, int[] answerNumbers, string correctString, int correctNumber)
            : base(typeof(AnswerObjective), "the correct answer to the question.", 1)
        {
            m_QuestionString = questionString;
            m_QuestionNumber = questionNumber;
            m_AnswerStrings = answerStrings;
            m_AnswerNumbers = answerNumbers;
            m_CorrectString = correctString;
            m_CorrectNumber = correctNumber;
        }
        public override bool Update(object obj)
        {

            return false;
        }

        public override void OnFailed()
        {
            this.Quest.Owner.SendLocalizedMessage(1075713);
        }

        public override void Fail()
        {
            this.Quest.Owner.SendLocalizedMessage(1075713);
        }

        public override void OnAccept()
        {
            if (m_QuestionNumber == 0)
                this.Quest.Owner.SendGump(new QuestionAnswerGump(this, m_QuestionString, m_AnswerStrings,
                    m_CorrectString));
            else
            {
                object[] answers = new object[m_AnswerNumbers.Length];
                for (int i = 0; i < m_AnswerNumbers.Length; i++)
                {
                    answers[i] = m_AnswerNumbers[i];
                }
                this.Quest.Owner.SendGump(new QuestionAnswerGump(this, (object)m_QuestionNumber, answers, (object)m_CorrectNumber));
            }
        }

        public override void OnCompleted()
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class QuestionAnswerGump : BaseQuestGump
    {
        private AnswerObjective mObjective;
        private object mQuestion;
        private object[] mAnswers;
        private object mCorrectAnswer;

        public QuestionAnswerGump(AnswerObjective objective, object question, object[] answers, object correctAnswer)
            : base(75, 25)
        {
            mObjective = objective;
            mQuestion = question;
            answers.Shuffle().CopyTo(mAnswers, 0);
            mCorrectAnswer = correctAnswer;

            int lowY = 385;
            int answerNum = 0;

            Closable = false;

            AddPage(0);

            AddImageTiled(50, 20, 400, 400, 2624);
            AddAlphaRegion(50, 20, 400, 400);

            AddImage(90, 33, 9005);
            AddHtmlObject(130, 45, 270, 20, mQuestion, White, false, false); // Question
            AddImageTiled(130, 65, 175, 1, 9101);

            foreach (object answer in mAnswers)
            {
                AddRadio(85, lowY, 9720, 9723, true, answerNum);
                AddHtmlObject(120, lowY + 6, 280, 20, answer, White, false, false);
                answerNum++;
                lowY -= 30;
            }

            AddButton(340, 390, 247, 248, 1, GumpButtonType.Reply, 0);

            AddImageTiled(50, 29, 30, 390, 10460);
            AddImageTiled(34, 140, 17, 279, 9263);

            AddImage(48, 135, 10411);
            AddImage(-16, 285, 10402);
            AddImage(0, 10, 10421);
            AddImage(25, 0, 10420);

            AddImageTiled(83, 15, 350, 15, 10250);

            AddImage(34, 419, 10306);
            AddImage(442, 419, 10304);
            AddImageTiled(51, 419, 392, 17, 10101);

            AddImageTiled(415, 29, 44, 390, 2605);
            AddImageTiled(415, 29, 30, 390, 10460);
            AddImage(425, 0, 10441);

            AddImage(370, 50, 1417);
            AddImage(379, 60, 0x15A9);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (info.ButtonID == 1)
            {
                for (int button = mAnswers.Length; button >= 0; button--)
                {
                    try
                    {
                        if (info.IsSwitched(button))
                        {
                            if (mAnswers[button] == mCorrectAnswer)
                            {
                                mObjective.CurProgress++;
                            }
                            else
                            {
                                mObjective.Fail();
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
    static class Helper
    {
        static readonly Random rnd = new Random();

        public static IList<T> Shuffle<T>(this IList<T> input)
        {
            for (var top = input.Count - 1; top > 1; --top)
            {
                var swap = rnd.Next(0, top);
                T tmp = input[top];
                input[top] = input[swap];
                input[swap] = tmp;
            }

            return input;
        }
    }
}
