using System;
using Server;

namespace Server.Items
{
	public class ExcellentIronMaiden : Item
	{
		public override double DefaultWeight { get { return 5; } }

		[Constructable]
		public ExcellentIronMaiden()
			: this( 0x3f15 )
		{
		}

		public ExcellentIronMaiden( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( ( int )0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}