using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;

namespace Server.Engines.Quests
{
    public abstract class HumilityQuester : MondainQuester
    {
        public override Type[] Quests { get { return new Type[] { typeof(HumilityCloakQuestFindTheHumble) }; } }

        public virtual int GreetingMessage { get { return 0; } }
        public virtual int ResponseMessage { get { return 0; } }
        public virtual int HintMessage { get { return 0; } }
        public virtual int TradeMessage { get { return 0; } }
        public virtual int ThanksMessage { get { return 0; } }

        public HumilityQuester()
            : base(null)
        {
        }

        public HumilityQuester(string name)
            : this(name, null)
        {
        }

        public HumilityQuester(string name, string title)
            : base(title)
        {
            this.Name = name;
            this.SpeechHue = 0x3B2;
        }

        public HumilityQuester(Serial serial)
            : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            AddNameProperties(list);
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            Mobile m = from;
            PlayerMobile mobile = m as PlayerMobile;
            {
                if (dropped is FriendshipMug)
                {
                    dropped.Delete();
                    mobile.AddToBackpack(new PairOfWorkGloves()); //1075780
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Ah, ‘tis a perfect mug. Please take this pair of gloves in trade.", mobile.NetState);
                    from.SendMessage("For your good deed you are awarded a little karma.");
                    from.Karma += 50;
                    return true;

                }
                else if (dropped.LootType == LootType.Blessed || dropped.LootType == LootType.Newbied || dropped.Insured)
                {
                    from.SendMessage("You cannot offer blessed, newbied, or insured items");
                    return false;
                }
                else
                {
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I have no need for this...", mobile.NetState);
                }
            }
            return false;
        }

        public override void OnTalk(PlayerMobile player)
        {
            if (QuestHelper.DeliveryArrived(player, this))
                return;

            if (QuestHelper.InProgress(player, this))
                return;

            if (QuestHelper.QuestLimitReached(player))
                return;

            // check if this quester can offer any quest chain (already started)
            foreach (KeyValuePair<QuestChain, BaseChain> pair in player.Chains)
            {
                BaseChain chain = pair.Value;

                if (chain != null && chain.Quester != null && chain.Quester == this.GetType())
                {
                    BaseQuest quest = QuestHelper.RandomQuest(player, new Type[] { chain.CurrentQuest }, this);

                    if (quest != null)
                    {
                        player.CloseGump(typeof(MondainQuestGump));
                        player.SendGump(new MondainQuestGump(quest));
                        return;
                    }
                }
            }

            BaseQuest questt = QuestHelper.RandomQuest(player, this.Quests, this);

            if (questt != null)
            {
                player.CloseGump(typeof(MondainQuestGump));
                player.SendGump(new MondainQuestGump(questt));
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

    public class HumilityQuesterGump : BaseQuestGump
    {
        public HumilityQuesterGump(HumilityQuester quester, int response)
            : base(75, 25)
        {
            string name = quester.Name + " " + quester.Title;
            Closable = true;

            AddPage(0);

            AddImageTiled(50, 20, 400, 400, 2624);
            AddAlphaRegion(50, 20, 400, 400);

            AddImage(90, 33, 9005);
            AddHtmlObject(130, 45, 270, 20, name, White, false, false); // Name
            AddImageTiled(130, 65, 175, 1, 9101);

            AddHtmlObject(120, 190, 280, 120, response, White, false, false); // Response

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
            base.OnResponse(sender, info);
        }
    }
}
