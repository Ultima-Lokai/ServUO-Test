
using System;

namespace Server.Items 
{
    public class HumilityMarker : Item
    {
        private DateTime m_DecayTime;
        private Timer m_Timer;
        private string m_Status;

        [CommandProperty(AccessLevel.GameMaster)]
        public string Status { get { return m_Status; } set { m_Status = value; InvalidateProperties(); } }

        public HumilityMarker()
            : this("new")
        {
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            list.Add("Status: {0}", m_Status);
            base.AddNameProperties(list);
        }

        [Constructable]
        public HumilityMarker(string status)
            : base(0x176B)
        {
            Weight = 0;
            Name = "Humility Marker";
            Hue = 0;
            LootType = LootType.Blessed;
            Movable = false;
            Visible = false;
            m_Status = status;

            m_DecayTime = DateTime.Now + TimeSpan.FromDays(7);

            m_Timer = new InternalTimer(this, m_DecayTime);
            m_Timer.Start();
        }

        public HumilityMarker(Serial serial)
            : base(serial)
        {
        }

        public override void OnAfterDelete()
        {
            if (m_Timer != null)
                m_Timer.Stop();

            base.OnAfterDelete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);

            writer.WriteDeltaTime(m_DecayTime);

            writer.Write(m_Status);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            m_DecayTime = reader.ReadDeltaTime();
            m_Timer = new InternalTimer(this, m_DecayTime);
            m_Timer.Start();

            m_Status = reader.ReadString();

        }

        private class InternalTimer : Timer
        {
            private Item m_Item;

            public InternalTimer(Item item, DateTime end)
                : base(end - DateTime.Now)
            {
                m_Item = item;
            }

            protected override void OnTick()
            {
                m_Item.Delete();
            }
        }
    }
} 