using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Items;
using Server.Network;

namespace Server.Engines.Quests
{

    class HumilityRewardGump :BaseQuestGump
    {
        private MondainQuester m_Gareth;
        private HumilityMarker m_Marker;
        private PlainGreyCloak m_Cloak;
        private IronChain m_Chain;

        public HumilityRewardGump(MondainQuester gareth, HumilityMarker marker, PlainGreyCloak cloak, IronChain chain)
            : base(75, 25)
        {
            m_Gareth = gareth;
            m_Marker = marker;
            m_Cloak = cloak;
            m_Chain = chain;

            Closable = false;
            AddPage(0);

            AddImageTiled(50, 20, 400, 400, 2624);
            AddAlphaRegion(50, 20, 400, 400);

            AddImage(90, 33, 9005);
            AddHtmlObject(130, 45, 270, 20, 1075781, White, false, false); // Question
            AddImageTiled(130, 65, 175, 1, 9101);

            /* Noble friend, thou hast performed tremendously! On behalf of the Rise of Britannia I wish to reward
             * thee with this golden shield, a symbol of accomplishment and pride for the many things that thou hast
             * done for our people.<BR><BR><br>Dost thou accept? */
            AddHtmlObject(100, 140, 280, 250, 1075782, White, false, false);

            AddButton(100, 390, 0x2EE2, 0x2EE0, (int)Buttons.Accept, GumpButtonType.Reply, 0);

            AddButton(240, 390, 0x2EF4, 0x2EF2, (int)Buttons.Refuse, GumpButtonType.Reply, 0);

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

        private enum Buttons
        {
            Accept = 100,
            Refuse = 101
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile m = sender.Mobile;
            if (info.ButtonID == (int)Buttons.Accept)
            {
                if (m != null && m_Chain != null && m_Cloak != null && m_Gareth != null && m_Marker != null)
                {
                    m_Chain.Delete();
                    m_Cloak.Delete();
                    m_Marker.Delete();
                    m.AddToBackpack(new ShieldOfRecognition());

                    m.SendGump(new HumilityQuesterGump(m_Gareth, 1075783));
                }
            }
            else if (info.ButtonID==(int)Buttons.Refuse)
            {
                m_Chain.Delete();
                m_Marker.Status = "complete";
                m.SendGump(new HumilityQuesterGump(m_Gareth, 1075784));
            }
        }
    }
}
