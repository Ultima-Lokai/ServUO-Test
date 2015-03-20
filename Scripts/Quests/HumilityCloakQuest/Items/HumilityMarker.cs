
using System;

namespace Server.Items 
{
    public class HumilityMarker : Item
    {
        private DateTime m_DelayTime;
        private string m_Status;

        [CommandProperty(AccessLevel.GameMaster)]
        public string Status { get { return m_Status; } set { m_Status = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime DelayTime { get { return m_DelayTime; } set { m_DelayTime = value; InvalidateProperties(); } }

        public HumilityMarker()
            : this("invalid")
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
            LootType = LootType.Blessed;
            Movable = false;
            Visible = false;
            m_Status = status;

            m_DelayTime = DateTime.UtcNow;
        }

        public HumilityMarker(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);

            writer.WriteDeltaTime(m_DelayTime);
            writer.Write(m_Status);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            m_DelayTime = reader.ReadDeltaTime();
            m_Status = reader.ReadString();

        }
    }
} 