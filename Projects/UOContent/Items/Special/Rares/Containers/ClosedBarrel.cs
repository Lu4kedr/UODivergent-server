namespace Server.Items
{
    internal class ClosedBarrel : TrappableContainer
    {
        [Constructible]
        public ClosedBarrel()
            : base(0x0FAE)
        {
        }

        public ClosedBarrel(Serial serial)
            : base(serial)
        {
        }

        public override int DefaultGumpID => 0x3e;

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}
