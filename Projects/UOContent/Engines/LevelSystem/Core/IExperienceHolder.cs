namespace Server.Engines.LevelSystem.Core
{
    public interface IExperienceHolder
    {
        int Level { get; set; }
        int Experiences { get; set; }
        int ExperiencesMax { get; set; }

    }
}
