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
        private IQuestionAnswer m_Quest;
        private int m_QuestionID;
        private string m_Title;

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
            get { try { return (string)m_AnswerStrings[0]; } catch { return ""; } }

            set { try { m_AnswerStrings[0] = value; } catch { } }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Answer_1_String
        {
            get { try { return (string)m_AnswerStrings[1]; } catch { return ""; } }

            set { try { m_AnswerStrings[1] = value; } catch { } }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Answer_2_String
        {
            get { try { return (string)m_AnswerStrings[2]; } catch { return ""; } }

            set { try { m_AnswerStrings[2] = value; } catch { } }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Answer_3_String
        {
            get { try { return (string)m_AnswerStrings[3]; } catch { return ""; } }

            set { try { m_AnswerStrings[3] = value; } catch { } }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Answer_4_String
        {
            get { try { return (string)m_AnswerStrings[4]; } catch { return ""; } }

            set { try { m_AnswerStrings[4] = value; } catch { } }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Answer_5_String
        {
            get { try { return (string)m_AnswerStrings[5]; } catch { return ""; } }

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
            : this(null, 0, string.Empty, 0, null, null, string.Empty, 0)
        {
        }

        public QuestionScroll(IQuestionAnswer quest, int questionID, object question, object[] answers, object correctAnswer, string title)
            : base(0x14ED)
        {
            Name = "a question scroll";
            m_Quest = quest;
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

            writer.Write((int)2); // version

            // version 2
            writer.Write(m_Title);

            // version 1
            writer.Write((bool)m_CorrectAnswerGiven);
            writer.WriteMobile(BlessedFor);

            // version 0
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

            Mobile questMobile = BlessedFor;

            switch (version)
            {
                case 2:
                {
                    m_Title = reader.ReadString();
                    goto case 1;
                }
                case 1:
                {
                    m_CorrectAnswerGiven = reader.ReadBool();
                    questMobile = reader.ReadMobile();
                    goto case 0;
                }
                case 0:
                {
                    if (version < 2) m_Title = "Question/Answer";
                    try
                    {
                        BaseQuest basequest = QuestHelper.RandomQuest(((PlayerMobile)questMobile),
                            new Type[] {typeof (HumilityCloakQuest)}, null);
                        m_Quest = (IQuestionAnswer) basequest;
                        if (m_Quest == null)
                        {
                            if (Core.Debug) Console.WriteLine("m_Quest was null. Line 265 in QuestionScroll.cs");
                            m_Quest = new HumilityCloakQuest(); // temporary fix
                        }
                    }
                    catch
                    {
                        if (Core.Debug) Console.WriteLine("Try failed (Lines 263-269), so forced to Catch, in QuestionScroll.cs");
                        m_Quest = new HumilityCloakQuest();
                    } // temporary fix
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
            : base(typeof(AnswerObjective), "answer to the questions...", 1)
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
