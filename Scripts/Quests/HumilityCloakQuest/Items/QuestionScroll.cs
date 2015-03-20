using System;
using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;
using OpenUO.Core;

namespace Server.Engines.Quests
{

    public class QuestionScroll : Item
    {
        private string m_QuestionString;
        private int m_QuestionNumber;
        private object[] m_AnswerStrings;
        private int[] m_AnswerNumbers;
        private string m_CorrectString;
        private int m_CorrectNumber;
        private bool m_CorrectAnswerGiven;
        private int m_QuestionID;
        private string m_Title;

        [CommandProperty(AccessLevel.GameMaster)]
        public int QuestionID { get { return m_QuestionID; } set { m_QuestionID = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool CorrectAnswerGiven { get { return m_CorrectAnswerGiven; } set { m_CorrectAnswerGiven = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public string QuestionString { get { return m_QuestionString; } set { m_QuestionString = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int QuestionNumber { get { return m_QuestionNumber; } set { m_QuestionNumber = value; } }

        public object[] AnswerStrings
        {
            get { return m_AnswerStrings; }
            set { m_AnswerStrings = value; }
        }

        public int[] AnswerNumbers { get { return m_AnswerNumbers; } set { m_AnswerNumbers = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public string CorrectString { get { return m_CorrectString; } set { m_CorrectString = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int CorrectNumber { get { return m_CorrectNumber; } set { m_CorrectNumber = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Title { get { return m_Title; } set { m_Title = value; } }

        [Constructable]
        public QuestionScroll()
            : this(0, string.Empty, null, string.Empty, "Invalid Question")
        {
        }

        [Constructable]
        public QuestionScroll(int questionID, object question, object[] answers, object correctAnswer, string title)
            : base(0x14ED)
        {
            Name = "a question scroll";
            m_QuestionID = questionID;
            if (question is string)
            {
                m_QuestionString = (string) question;
                m_QuestionNumber = 0;
            }
            else
            {
                m_QuestionString = string.Empty;
                m_QuestionNumber = (int)question;
            }
            m_AnswerStrings = answers is string[] ? answers : null;
            if (answers.Length > 0 && answers[0] is int)
            {
                m_AnswerNumbers = new int[answers.Length];
                for (int i = 0; i < answers.Length; i++)
                    m_AnswerNumbers[i] = (int) answers[i];
            }
            else
            {
                m_AnswerNumbers = null;
            }
            if (correctAnswer is string)
            {
                m_CorrectString = (string) correctAnswer;
                m_CorrectNumber = 0;
            }
            else
            {
                m_CorrectString = string.Empty;
                m_CorrectNumber = (int)correctAnswer;
            }
            m_Title = title;
        }

        public QuestionScroll(Serial serial)
            : base(serial)
        {

        }

        public override void OnDoubleClick(Mobile from)
        {
            if (ItemID == 0x14EE)
            {
                from.SendMessage("Show this to the one who gave it to you.");
                return;
            }
            Container pack = from.Backpack;
            if (!(from is PlayerMobile) || pack == null) return;

            if(!IsChildOf(pack))
            {
                from.SendMessage("That must be in your pack for you to use it.");
                return;
            }

            if (m_QuestionNumber == 0)
                from.SendGump(new QuestionAnswerGump(this, m_QuestionString, m_AnswerStrings,
                    m_CorrectString, m_Title));
            else
            {
                object[] answers = new object[m_AnswerNumbers.Length];
                for (int i = 0; i < m_AnswerNumbers.Length; i++)
                {
                    answers[i] = m_AnswerNumbers[i];
                }
                from.SendGump(new QuestionAnswerGump(this, (object)m_QuestionNumber, answers, (object)m_CorrectNumber, m_Title));
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            // version 0
            writer.Write(m_Title);
            writer.Write((bool)m_CorrectAnswerGiven);
            writer.Write((int)m_QuestionID);
            writer.Write((string)m_QuestionString);
            writer.Write((int)m_QuestionNumber);
            if (m_AnswerStrings == null) writer.Write(0);
            else
            {
                writer.Write((int) m_AnswerStrings.Length);
                foreach (string answer in m_AnswerStrings) writer.Write((string) answer);
            }
            if (m_AnswerNumbers == null) writer.Write(0);
            else
            {
                writer.Write((int) m_AnswerNumbers.Length);
                foreach (int answer in m_AnswerNumbers) writer.Write((int) answer);
            }
            writer.Write((string)m_CorrectString);
            writer.Write((int)m_CorrectNumber);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                {
                    m_Title = reader.ReadString();
                    m_CorrectAnswerGiven = reader.ReadBool();
                    m_QuestionID = reader.ReadInt();
                    m_QuestionString = reader.ReadString();
                    m_QuestionNumber = reader.ReadInt();

                    int stringNum = reader.ReadInt();
                    if (stringNum == 0) m_AnswerStrings = null;
                    else
                    {
                        m_AnswerStrings = new object[stringNum];
                        for (int i = 0; i < stringNum; i++)
                            m_AnswerStrings[i] = reader.ReadString();
                    }
                    int answerNum = reader.ReadInt();
                    if (answerNum == 0) m_AnswerNumbers = null;
                    else
                    {
                        m_AnswerNumbers = new int[answerNum];
                        for (int i = 0; i < answerNum; i++)
                            m_AnswerNumbers[i] = reader.ReadInt();
                    }
                    m_CorrectString = reader.ReadString();
                    m_CorrectNumber = reader.ReadInt();
                    break;
                }
            }
        }
    }

    public class AnswerObjective : ObtainObjective
    {
        public AnswerObjective()
            : base(typeof(AnswerObjective), "answer to the question...", 1)
        {
        }

        public override void OnFailed()
        {
            this.Quest.Owner.SendLocalizedMessage(1075713);
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
