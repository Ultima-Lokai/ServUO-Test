﻿using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Network;

namespace Server.Engines.Quests
{

    public class QuestionAnswerGump : BaseQuestGump
    {
        private QuestionScroll mScroll;
        private object mQuestion;
        private object[] mAnswers;
        private object mCorrectAnswer;

        public QuestionAnswerGump(QuestionScroll scroll, object question, object[] answers, object correctAnswer)
            : base(75, 25)
        {
            mScroll = scroll;
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
                            mScroll.CorrectAnswerGiven = (mAnswers[button] == mCorrectAnswer);
                            mScroll.Name = "an answer scroll";
                            mScroll.ItemID = 0x14EF;
                            mScroll.InvalidateProperties();
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