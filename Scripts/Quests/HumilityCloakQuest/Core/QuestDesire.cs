using System;

namespace Server.Engines.Quests
{
    public class QuestDesire
    {
        private int m_QuesterID;
        private Type m_DesireType;
        private string m_DesireName;
        private Type m_OfferType;
        private string m_OfferName;
        private bool m_Greeted;
        private DateTime m_GreetTime;
        private bool m_Hinted;
        private DateTime m_HintTime;
        private bool m_Traded;
        private DateTime m_TradeTime;
        private bool m_Thanked;
        private DateTime m_ThankTime;

        public int QuesterID
        {
            get { return m_QuesterID; }
        }

        public Type DesireType
        {
            get { return m_DesireType; }
        }

        public string DesireName
        {
            get { return m_DesireName; }
        }

        public Type OfferType
        {
            get { return m_OfferType; }
        }

        public string OfferName
        {
            get { return m_OfferName; }
        }

        public bool Greeted
        {
            get { return m_Greeted; }
            set { m_Greeted = value; }
        }

        public DateTime GreetTime
        {
            get { return m_GreetTime; }
            set { m_GreetTime = value; }
        }

        public bool Hinted
        {
            get { return m_Hinted; }
            set { m_Hinted = value; }
        }

        public DateTime HintTime
        {
            get { return m_HintTime; }
            set { m_HintTime = value; }
        }

        public bool Traded
        {
            get { return m_Traded; }
            set { m_Traded = value; }
        }

        public DateTime TradeTime
        {
            get { return m_TradeTime; }
            set { m_TradeTime = value; }
        }

        public bool Thanked
        {
            get { return m_Thanked; }
            set { m_Thanked = value; }
        }

        public DateTime ThankTime
        {
            get { return m_ThankTime; }
            set { m_ThankTime = value; }
        }

        public QuestDesire(int questerID, Type desireType, string desireName, Type offerType, string offerName)
            : this(questerID, desireType, desireName, offerType, offerName, false, false, false, false)
        {
        }

        public QuestDesire(int questerID, Type desireType, string desireName, Type offerType, string offerName,
            bool greeted, bool hinted, bool traded, bool thanked)
        {
            m_QuesterID = questerID;
            m_DesireType = desireType;
            m_DesireName = desireName;
            m_OfferType = offerType;
            m_OfferName = offerName;
            m_Greeted = greeted;
            m_GreetTime = new DateTime(1);
            m_Hinted = hinted;
            m_HintTime = new DateTime(1);
            m_Traded = traded;
            m_TradeTime = new DateTime(1);
            m_Thanked = thanked;
            m_ThankTime = new DateTime(1);
        }
    }
}
