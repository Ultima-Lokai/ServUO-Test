using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{

    public interface IQuestionAnswer
    {
        QuestionScroll[] Scrolls { get; }
        void GiveNextQuestion(PlayerMobile from, int index);
    }
    
    public class HumilityCloakQuest : BaseQuest, IQuestionAnswer
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
        public override Type NextQuest { get { return typeof(HumilityCloakQuestVesperMuseum); } }
        public override bool DoneOnce { get { return false; } }
        public override TimeSpan RestartDelay { get { return TimeSpan.FromDays(1.0); } }

        private readonly QuestionScroll[] m_Scrolls;

        public QuestionScroll[] Scrolls { get { return m_Scrolls; } }

        public HumilityCloakQuest()
        {
            m_Scrolls = new QuestionScroll[]
            {
                new QuestionScroll(this, 0, 1075678, new int[] {1075679, 1075680, 1075681, 1075682}, 1075679),
                new QuestionScroll(this, 1, 1075683, new int[] {1075684, 1075685, 1075686, 1075687}, 1075685),
                new QuestionScroll(this, 2, 1075688, new int[] {1075689, 1075690, 1075691, 1075692}, 1075691),
                new QuestionScroll(this, 3, 1075693, new int[] {1075694, 1075695, 1075696, 1075697}, 1075697),
                new QuestionScroll(this, 4, 1075698, new int[] {1075699, 1075700, 1075701, 1075702}, 1075700),
                new QuestionScroll(this, 5, 1075703, new int[] {1075704, 1075705, 1075706, 1075707}, 1075705),
                new QuestionScroll(this, 6, 1075708, new int[] {1075709, 1075710, 1075711, 1075712}, 1075709)
            };
            AddObjective(new AnswerObjective());
            AddReward(new BaseReward("Virtue is its own reward."));
        }

        public override void OnCompleted()
        {
            base.OnCompleted();
        }

        public void GiveNextQuestion(PlayerMobile from, int index)
        {
            QuestionScroll scroll = (QuestionScroll)Activator.CreateInstance(typeof(QuestionScroll));
            QuestionScroll copy = Scrolls[index];
            scroll.Quest = copy.Quest;
            scroll.QuestionID = copy.QuestionID;
            scroll.QuestionString = copy.QuestionString;
            scroll.QuestionNumber = copy.QuestionNumber;
            if (copy.AnswerStrings == null) scroll.AnswerStrings = null;
            else
            {
                scroll.AnswerStrings = new string[copy.AnswerStrings.Length];
                for (int i = 0; i < copy.AnswerStrings.Length; i++) { scroll.AnswerStrings[i] = copy.AnswerStrings[i]; }
            }
            if (copy.AnswerNumbers == null) scroll.AnswerNumbers = null;
            else
            {
                scroll.AnswerNumbers = new int[copy.AnswerNumbers.Length];
                for (int i = 0; i < copy.AnswerNumbers.Length; i++) { scroll.AnswerNumbers[i] = copy.AnswerNumbers[i]; }
            }
            scroll.AnswerNumbers = copy.AnswerNumbers;
            scroll.CorrectString = copy.CorrectString;
            scroll.CorrectNumber = copy.CorrectNumber;
            scroll.LootType = LootType.Blessed;
            scroll.BlessedFor = from;

            if (!from.PlaceInBackpack(scroll))
                from.Drop(scroll, from.Location);
        }

        public override void OnAccept()
        {
            Owner.SendLocalizedMessage(1075676);
            GiveNextQuestion(Owner, 0);
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
    
    /* Very good! I can see that ye hath more than just a passing interest in our work. There are many trials before thee, 
       * but I have every hope that ye shall have the diligence and fortitude to carry on to the very end. Before we begin, 
       * please prepare thyself by thinking about the virtue of Humility. 
       * Ponder not only its symbols, but also its meanings. Once ye believe that thou art ready, speak with me again. */

    public class HumilityCloakQuestVesperMuseum : BaseQuest
    {
        public override QuestChain ChainID { get { return QuestChain.HumilityCloak; } }
        public override Type NextQuest { get { return typeof(HumilityCloakQuestMoongateZoo); } }
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

        public HumilityCloakQuestVesperMuseum()
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

    public class HumilityCloakQuestMoongateZoo : BaseQuest
    {
        public override QuestChain ChainID { get { return QuestChain.HumilityCloak; } }
        public override Type NextQuest { get { return typeof(HumilityCloakQuestBritainLibrary); } }
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

        public HumilityCloakQuestMoongateZoo()
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

    public class HumilityCloakQuestBritainLibrary : BaseQuest
    {
        public override QuestChain ChainID { get { return QuestChain.HumilityCloak; } }
        public override Type NextQuest { get { return typeof(HumilityCloakQuestFindTheHumble); } }
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

        public HumilityCloakQuestBritainLibrary()
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

    public class HumilityCloakQuestFindTheHumble : BaseQuest
    {
        public override QuestChain ChainID { get { return QuestChain.HumilityCloak; } }
        public override Type NextQuest { get { return typeof(HumilityCloakQuestGoldenShield); } }
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

        public HumilityCloakQuestFindTheHumble()
            : base()
        {
            AddObjective(new ObtainObjective(typeof(IronChain), "an item from the One who best exemplifies Humility.", 1, 0x1085)); //1075788
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

    public class HumilityCloakQuestGoldenShield : BaseQuest
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

        public HumilityCloakQuestGoldenShield()
            : base()
        {
            AddObjective(new ObtainObjective(typeof(PlainGreyCloak), "A Plain Grey Cloak", 1, 0x1085)); //1075789


            AddReward(new BaseReward(typeof(ShieldOfRecognition), 1075851)); // Shield of Recognition
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
