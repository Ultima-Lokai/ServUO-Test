using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Spells;

namespace Server.Items
{

    public class PlainGreyCloak : Cloak
    {
        [Constructable]
        public PlainGreyCloak()
            : base(0x1085)
        {
            this.LootType = LootType.Blessed;
            this.Hue = 908;
        }

        public PlainGreyCloak(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber
        {
            get
            {
                return 1075789; //A Plain Grey Cloak
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            PlainGreyCloak cloak = from.FindItemOnLayer(Layer.Cloak) as PlainGreyCloak;

            if (Parent != from)
            {
                from.SendMessage("You must equip the cloak to perform this action.");
            }
            else
            {
                Item HumilityMarker = from.Backpack.FindItemByType(typeof(HumilityMarker));
                if (HumilityMarker != null && from.Region.IsPartOf(typeof(Regions.HumilityShrineRegion)))
                {
                    HumilityMarker.Delete();

                    from.AddToBackpack(new HumilityCloak());
                    from.PlaySound(0x1F7); 
                    from.FixedParticles(0x376A, 9, 32, 5030, EffectLayer.Waist);
                    //from.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.Waist);
                    from.SendMessage("As you near the shrine a strange energy envelops you. Suddenly, your cloak is transformed into the Cloak of Humility!");//1075897
                    
                    Delete();
                }
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
}
