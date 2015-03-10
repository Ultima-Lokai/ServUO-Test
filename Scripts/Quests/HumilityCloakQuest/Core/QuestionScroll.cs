using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{

    public class QuestionScroll : Item
    {
        private string m_QuestionString;
        private int m_QuestionNumber;
        private string[] m_AnswerStrings;
        private int[] m_AnswerNumbers;
        private string m_CorrectString;
        private int m_CorrectNumber;
        private bool m_CorrectAnswerGiven;
        private IQuestionAnswer m_Quest;
        private int m_QuestionID;

        [CommandProperty(AccessLevel.GameMaster)]
        public int QuestionID { get { return m_QuestionID; } set { m_QuestionID = value; } }

        public IQuestionAnswer Quest { get { return m_Quest; } set { m_Quest = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool CorrectAnswerGiven { get { return m_CorrectAnswerGiven; } set { m_CorrectAnswerGiven = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestionString { get { return m_QuestionString; } set { m_QuestionString = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int QuestionNumber { get { return m_QuestionNumber; } set { m_QuestionNumber = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Answer_0_String
        {
            get { try { return m_AnswerStrings[0]; } catch { return ""; } }

            set { try { m_AnswerStrings[0] = value; } catch { } }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Answer_1_String
        {
            get { try { return m_AnswerStrings[1]; } catch { return ""; } }

            set { try { m_AnswerStrings[1] = value; } catch { } }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Answer_2_String
        {
            get { try { return m_AnswerStrings[2]; } catch { return ""; } }

            set { try { m_AnswerStrings[2] = value; } catch { } }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Answer_3_String
        {
            get { try { return m_AnswerStrings[3]; } catch { return ""; } }

            set { try { m_AnswerStrings[3] = value; } catch { } }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Answer_4_String
        {
            get { try { return m_AnswerStrings[4]; } catch { return ""; } }

            set { try { m_AnswerStrings[4] = value; } catch { } }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Answer_5_String
        {
            get { try { return m_AnswerStrings[5]; } catch { return ""; } }

            set { try { m_AnswerStrings[5] = value; } catch { } }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Answer_0_Number
        {
            get { try { return m_AnswerNumbers[0]; } catch { return 0; } }

            set { try { m_AnswerNumbers[0] = value; } catch { } }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Answer_1_Number
        {
            get { try { return m_AnswerNumbers[1]; } catch { return 0; } }

            set { try { m_AnswerNumbers[1] = value; } catch { } }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Answer_2_Number
        {
            get { try { return m_AnswerNumbers[2]; } catch { return 0; } }

            set { try { m_AnswerNumbers[2] = value; } catch { } }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Answer_3_Number
        {
            get { try { return m_AnswerNumbers[3]; } catch { return 0; } }

            set { try { m_AnswerNumbers[3] = value; } catch { } }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Answer_4_Number
        {
            get { try { return m_AnswerNumbers[4]; } catch { return 0; } }

            set { try { m_AnswerNumbers[4] = value; } catch { } }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Answer_5_Number
        {
            get { try { return m_AnswerNumbers[5]; } catch { return 0; } }

            set { try { m_AnswerNumbers[5] = value; } catch { } }
        }

        public string[] AnswerStrings
        {
            get { return m_AnswerStrings; }
            set { m_AnswerStrings = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int[] AnswerNumbers { get { return m_AnswerNumbers; } set { m_AnswerNumbers = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public string CorrectString { get { return m_CorrectString; } set { m_CorrectString = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int CorrectNumber { get { return m_CorrectNumber; } set { m_CorrectNumber = value; } }


        [Constructable]
        public QuestionScroll()
            : this(null, 0, string.Empty, 0, null, null, string.Empty, 0)
        {
        }

        public QuestionScroll(IQuestionAnswer quest, int questionID, int question, int[] answers, int correctAnswer)
            : this(quest, questionID, string.Empty, question, null, answers, string.Empty, correctAnswer)
        {
        }
        public QuestionScroll(IQuestionAnswer quest, int questionID, string question, string[] answers, string correctAnswer)
            : this(quest, questionID, question, 0, answers, null, correctAnswer, 0)
        {
        }

        [Constructable]
        public QuestionScroll(IQuestionAnswer quest, int questionID, string questionString, int questionNumber, string[] answerStrings, int[] answerNumbers,
            string correctString, int correctNumber)
            : base(0x14ED)
        {
            Name = "a question scroll";
            m_Quest = quest;
            m_QuestionID = questionID;
            m_QuestionString = questionString;
            m_QuestionNumber = questionNumber;
            m_AnswerStrings = answerStrings;
            m_AnswerNumbers = answerNumbers;
            m_CorrectString = correctString;
            m_CorrectNumber = correctNumber;
        }

        public QuestionScroll(Serial serial)
            : base(serial)
        {

        }

        public override void OnDoubleClick(Mobile from)
        {
            Container pack = from.Backpack;
            if (!(from is PlayerMobile) || pack == null) return;

            if (m_QuestionNumber == 0)
                from.SendGump(new QuestionAnswerGump(this, m_QuestionString, m_AnswerStrings,
                    m_CorrectString));
            else
            {
                object[] answers = new object[m_AnswerNumbers.Length];
                for (int i = 0; i < m_AnswerNumbers.Length; i++)
                {
                    answers[i] = m_AnswerNumbers[i];
                }
                from.SendGump(new QuestionAnswerGump(this, (object)m_QuestionNumber, answers, (object)m_CorrectNumber));
            }
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
}
