using System.Collections.Generic;
using System.Text;

using Server.Mobiles;

namespace Server.Engines.KillCounter
{
    public static class KillCounterSystem
    {
        private static Dictionary<Serial, KillCount> monsterKills = new();

        public static int GetKills(PlayerMobile pm, Mobile target)
        {
            if (monsterKills is null) return -1;
            return monsterKills[pm.Serial].GetKills(target);
        }

        public static void IncrementKills(PlayerMobile pm, Mobile target)
        {
            if (monsterKills is null) monsterKills = new Dictionary<Serial, KillCount>();
            if (monsterKills.ContainsKey(pm.Serial))
            {
                monsterKills[pm.Serial].AddKill(target);
                return;
            }
            monsterKills.Add(pm.Serial, new KillCount());
            monsterKills[pm.Serial].AddKill(target);
            return;
        }


        public static void Configure()
        {
            GenericPersistence.Register("KillCounterSystem", Serialize, Deserialize);
        }

        public static void Serialize(IGenericWriter writer)
        {
            // Do serialization here
            writer.WriteEncodedInt(0); // version
            writer.Write(monsterKills.Keys.Count);
            foreach (var key in monsterKills.Keys)
            {
                writer.Write(key);
                var kc = monsterKills[key].Kills;
                writer.Write(kc.Keys.Count);

                foreach (string monsterName in kc.Keys)
                {
                    var bytesName = Encoding.UTF8.GetBytes(monsterName);
                    writer.Write(bytesName.Length);
                    for (int b = 0; b < bytesName.Length; b++)
                    {
                        writer.Write(bytesName[b]);
                    }
                    writer.Write(kc[monsterName]);
                }
            }
            writer.Close();
        }

        public static void Deserialize(IGenericReader reader)
        {
            var data = new Dictionary<Serial, KillCount>();
            // Do deserialization here
            var version = reader.ReadEncodedInt();
            var totalEntries = reader.ReadInt();

            for (int i = 0; i < totalEntries; i++)
            {
                var serial = reader.ReadSerial();
                var kc = new KillCount();

                var monsterCount = reader.ReadInt();
                for (int m = 0; m < monsterCount; m++)
                {
                    var monsterTypeLenght = reader.ReadInt();
                    var byteMonsterType = new byte[monsterTypeLenght];
                    for (int b = 0; b < monsterTypeLenght; b++)
                    {
                        byteMonsterType[b] = reader.ReadByte();
                    }
                    var monsterType = Encoding.UTF8.GetString(byteMonsterType);
                    var kills = reader.ReadInt();
                    kc.Add(monsterType, kills);
                }
                data.Add(serial, kc);
            }

            monsterKills = data;
        }

    }
    internal class KillCount
    {
        public Dictionary<string, int> Kills { get; private set; } = new();
        public KillCount()
        {

        }
        internal void Add(string monsterType, int kills)
        {
            Kills.Add(monsterType, kills);
        }
        public void AddKill(Mobile killed)
        {
            var key = killed.GetType().ToString();
            if (Kills.ContainsKey(key))
            {
                Kills[key]++;
                return;
            }
            Kills.Add(key, 1);

        }

        public int GetKills(Mobile killed)
        {
            var key = killed.GetType().ToString();
            if (Kills.ContainsKey(key))
            {
                return Kills[key];
            }
            return 0;
        }

    }





}
