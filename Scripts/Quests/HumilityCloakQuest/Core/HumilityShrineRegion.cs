using System;
using System.Xml;
using Server;
using Server.Items;


namespace Server.Regions
{
    public class HumilityShrineRegion : BaseRegion
    {
        public HumilityShrineRegion(XmlElement xml, Map map, Region parent)
            : base(xml, map, parent)
        {
        }

        public override bool OnSkillUse(Mobile m, int Skill)
        {
            if (Skill == (int) SkillName.Meditation)
            {
                Container pack = m.Backpack;
                if (pack != null)
                {
                    HumilityMarker marker =
                        (HumilityMarker) pack.FindItemByType(typeof (HumilityMarker));
                    if (marker != null && marker.Status == "complete")
                    {
                        PlainGreyCloak cloak =
                            (PlainGreyCloak) pack.FindItemByType(typeof (PlainGreyCloak));
                        if (cloak == null) cloak = (PlainGreyCloak) m.FindItemOnLayer(Layer.Cloak);
                        if (cloak != null)
                        {
                            if (m.AddToBackpack(new HumilityCloak()))
                            {
                                marker.Delete();

                                m.PlaySound(0x1F7);
                                m.FixedParticles(0x376A, 9, 32, 5030, EffectLayer.Waist);

                                m.SendLocalizedMessage(1075897);
                                // As you near the shrine a strange energy envelops you. Suddenly, your cloak is transformed into the Cloak of Humility!

                                cloak.Delete();
                            }
                            else
                            {
                                m.SendMessage("You might want to make some room in your pack.");
                            }
                        }
                        else
                        {
                            m.SendMessage("You should either wear the Plain Grey Cloak or have it in your backpack when you do that.");
                        }
                    }
                }
            }
            return base.OnSkillUse(m, Skill);
        }

        public override bool AllowHousing(Mobile from, Point3D p)
        {
            if (from.AccessLevel < AccessLevel.Administrator)
                return false;
			
            return base.AllowHousing(from, p);
        }
    }
}
	
