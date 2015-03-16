
using Server.Gumps;
using Server.Network;

namespace Server.Engines.Quests
{

    public class HumilityQuesterGump : BaseQuestGump
    {
        private HumilityQuester mQuester;

        public HumilityQuesterGump(HumilityQuester quester)
            : this(quester, quester.ResponseMessage)
        {
        }

        public HumilityQuesterGump(MondainQuester quester, object response)
            : base(75, 25)
        {
            if (quester is HumilityQuester)
                mQuester = (HumilityQuester)quester;
            else
                mQuester = null;
            string name = quester.Name + " " + quester.Title;

            Closable = true;

            AddPage(0);

            AddImageTiled(50, 20, 400, 400, 2624);
            AddAlphaRegion(50, 20, 400, 400);

            AddImage(90, 33, 9005);
            AddHtmlObject(130, 45, 270, 20, name, White, false, false); // Name
            AddImageTiled(130, 65, 175, 1, 9101);

            AddHtmlObject(120, 160, 280, 170, response, White, false, false); // Response

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
            Mobile from = sender.Mobile;
            if (from != null && !from.Deleted && mQuester != null)
                mQuester.OnGumpClose(from);
        }
    }
}
