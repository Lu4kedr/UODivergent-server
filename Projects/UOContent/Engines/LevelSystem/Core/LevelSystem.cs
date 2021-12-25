using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Server.Mobiles;

namespace Server.Engines.LevelSystem.Core
{

    public static class LevelSystem
    {
        private const int experienceMaxAddition = 500;
        private const int experienceLevelAddition = 250;

        public static void AddExperience(Mobile receiver, Mobile sender)
        {
            if (receiver is PlayerMobile pm)
            {

            }
        }







        public static void Configure()
        {
            GenericPersistence.Register("LevelSystem", Serialize, Deserialize);
        }

        public static void Serialize(IGenericWriter writer)
        {
            // Do serialization here
            writer.WriteEncodedInt(0); // version

            writer.Close();
        }

        public static void Deserialize(IGenericReader reader)
        {
            var version = reader.ReadEncodedInt();

        }
    }
}
