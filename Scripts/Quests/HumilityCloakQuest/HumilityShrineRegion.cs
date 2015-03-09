using System;
using System.Xml;
using Server;


namespace Server.Regions
{
    public class HumilityShrineRegion : BaseRegion
    {
        public HumilityShrineRegion(XmlElement xml, Map map, Region parent)
            : base(xml, map, parent)
        {
        }

        public override bool AllowHousing(Mobile from, Point3D p)
        {
            if (from.AccessLevel < AccessLevel.Administrator)
                return false;
			
            return base.AllowHousing(from, p);
        }
    }
}
	
